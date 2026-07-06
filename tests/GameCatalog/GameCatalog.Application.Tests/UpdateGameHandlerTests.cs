using GameCatalog.Application;
using GameCatalog.Domain;
using Moq;

namespace GameCatalog.Application.Tests;

public class UpdateGameHandlerTests
{
    private readonly Mock<IGameRepository> _repository = new();

    [Fact]
    public async Task Handle_ExistingGame_ShouldUpdateAndReturn()
    {
        var game = new Game("Old Name", "NES", new ConditionRating(1, 1, 1));
        _repository.Setup(r => r.GetByIdAsync(game.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(game);
        _repository.Setup(r => r.UpdateAsync(game, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateGame.Handler(_repository.Object);
        var command = new UpdateGame.Command(game.Id, "New Name", "SNES", 5, 4, 3);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("New Name", result.Value.Name);
        Assert.Equal("SNES", result.Value.Platform);
        _repository.Verify(r => r.UpdateAsync(game, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentGame_ShouldReturnFailure()
    {
        _repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Game?)null);

        var handler = new UpdateGame.Handler(_repository.Object);
        var command = new UpdateGame.Command(Guid.NewGuid(), "Name", "SNES", 3, 3, 3);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.IsType<KeyNotFoundException>(result.Error);
    }
}
