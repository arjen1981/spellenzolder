using GameCatalog.Application;
using GameCatalog.Domain;
using Moq;

namespace GameCatalog.Application.Tests;

public class GetGamesHandlerTests
{
    private readonly Mock<IGameRepository> _repository = new();

    [Fact]
    public async Task Handle_ShouldReturnPagedResult()
    {
        var games = new List<Game>
        {
            new("Mario", "SNES", new ConditionRating(4, 3, 5)),
            new("Zelda", "SNES", new ConditionRating(5, 5, 5))
        };
        var pagedResult = new PagedResult<Game>(games, 2, 1, 20);
        _repository.Setup(r => r.GetAllAsync(It.IsAny<GameQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagedResult);

        var handler = new GetGames.Handler(_repository.Object);
        var query = new GetGames.Query(Search: "mario");

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.TotalCount);
        Assert.Equal(2, result.Value.Items.Count);
    }
}
