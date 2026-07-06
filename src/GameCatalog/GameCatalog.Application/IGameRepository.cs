using GameCatalog.Domain;

namespace GameCatalog.Application;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Game>> GetAllAsync(GameQuery query, CancellationToken cancellationToken = default);
    Task<List<string>> GetDistinctPlatformsAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Game game, CancellationToken cancellationToken = default);
    Task UpdateAsync(Game game, CancellationToken cancellationToken = default);
    Task DeleteAsync(Game game, CancellationToken cancellationToken = default);
}

public sealed record GameQuery(
    string? Search = null,
    List<string>? Platforms = null,
    int? MinBooklet = null,
    int? MinBox = null,
    int? MinMedia = null,
    string SortBy = "Name",
    bool SortDescending = false,
    int Page = 1,
    int PageSize = 20);

public sealed record PagedResult<T>(
    List<T> Items,
    int TotalCount,
    int Page,
    int PageSize)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
}
