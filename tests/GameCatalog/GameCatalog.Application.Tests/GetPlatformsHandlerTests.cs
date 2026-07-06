using GameCatalog.Application;
using Moq;

namespace GameCatalog.Application.Tests;

public class GetPlatformsHandlerTests
{
    private readonly Mock<IGameRepository> _repository = new();

    [Fact]
    public async Task Handle_ShouldReturnDistinctPlatforms()
    {
        var platforms = new List<string> { "SNES", "NES", "Game Boy" };
        _repository.Setup(r => r.GetDistinctPlatformsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(platforms);

        var handler = new GetPlatforms.Handler(_repository.Object);
        var query = new GetPlatforms.Query();

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value.Count);
        Assert.Contains("SNES", result.Value);
    }
}
