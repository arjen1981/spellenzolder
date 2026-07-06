using GameCatalog.Domain;
using MediatR;

namespace GameCatalog.Application;

using Result = Result<PagedResult<Game>, Exception>;

public static class GetGames
{
    public sealed record Query(
        string? Search = null,
        List<string>? Platforms = null,
        int? MinBooklet = null,
        int? MinBox = null,
        int? MinMedia = null,
        string SortBy = "Name",
        bool SortDescending = false,
        int Page = 1,
        int PageSize = 20) : IRequest<Result>;

    public sealed class Handler(IGameRepository repository) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            var gameQuery = new GameQuery(
                Search: request.Search,
                Platforms: request.Platforms,
                MinBooklet: request.MinBooklet,
                MinBox: request.MinBox,
                MinMedia: request.MinMedia,
                SortBy: request.SortBy,
                SortDescending: request.SortDescending,
                Page: request.Page,
                PageSize: request.PageSize);

            var result = await repository.GetAllAsync(gameQuery, cancellationToken);

            return result;
        }
    }
}
