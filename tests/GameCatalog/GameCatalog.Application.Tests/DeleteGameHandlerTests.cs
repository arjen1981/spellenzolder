using GameCatalog.Application;
using GameCatalog.Domain;
using Moq;

namespace GameCatalog.Application.Tests;

public class DeleteGameHandlerTests
{
    private readonly Mock<IGameRepository> _repository = new();

    [Fact]
    public async Task Handle_ExistingGame_ShouldDeleteAndReturnSuccess()
    {
        var game = new Game("Test", "SNES", new ConditionRating(3, 3, 3));
        _repository.Setup(r => r.GetByIdAsync(game.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(game);
        _repository.Setup(r => r.DeleteAsync(game, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteGame.Handler(_repository.Object);
        var command = new DeleteGame.Command(game.Id);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _repository.Verify(r => r.DeleteAsync(game, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentGame_ShouldReturnFailure()
    {
        _repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Game?)null);

        var handler = new DeleteGame.Handler(_repository.Object);
        var command = new DeleteGame.Command(Guid.NewGuid());

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.IsType<KeyNotFoundException>(result.Error);
    }
}
