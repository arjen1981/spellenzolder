using GameCatalog.Application;
using GameCatalog.Domain;
using Moq;

namespace GameCatalog.Application.Tests;

public class AddGameHandlerTests
{
    private readonly Mock<IGameRepository> _repository = new();

    [Fact]
    public async Task Handle_ValidCommand_ShouldReturnGame()
    {
        _repository.Setup(r => r.AddAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new AddGame.Handler(_repository.Object);
        var command = new AddGame.Command("Super Mario World", "SNES", 4, 3, 5);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("Super Mario World", result.Value.Name);
        Assert.Equal("SNES", result.Value.Platform);
        Assert.Equal(4, result.Value.Condition.Booklet);
        Assert.Equal(3, result.Value.Condition.Box);
        Assert.Equal(5, result.Value.Condition.Media);
        _repository.Verify(r => r.AddAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidRating_ShouldThrow()
    {
        var handler = new AddGame.Handler(_repository.Object);
        var command = new AddGame.Command("Test", "SNES", 0, 3, 3);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => handler.Handle(command, CancellationToken.None));
    }
}
