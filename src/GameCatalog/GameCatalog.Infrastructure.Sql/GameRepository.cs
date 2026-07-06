using GameCatalog.Application;
using GameCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Infrastructure.Sql;

public sealed class GameRepository(GameCatalogDbContext context) : IGameRepository
{
    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Games.FindAsync([id], cancellationToken);
    }

    public async Task<PagedResult<Game>> GetAllAsync(GameQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = context.Games.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
            queryable = queryable.Where(g => g.Name.Contains(query.Search));

        if (query.Platforms is { Count: > 0 })
            queryable = queryable.Where(g => query.Platforms.Contains(g.Platform));

        if (query.MinBooklet.HasValue)
            queryable = queryable.Where(g => g.Condition.Booklet >= query.MinBooklet.Value);

        if (query.MinBox.HasValue)
            queryable = queryable.Where(g => g.Condition.Box >= query.MinBox.Value);

        if (query.MinMedia.HasValue)
            queryable = queryable.Where(g => g.Condition.Media >= query.MinMedia.Value);

        var totalCount = await queryable.CountAsync(cancellationToken);

        queryable = query.SortBy.ToLowerInvariant() switch
        {
            "platform" => query.SortDescending ? queryable.OrderByDescending(g => g.Platform) : queryable.OrderBy(g => g.Platform),
            "registrationdate" => query.SortDescending ? queryable.OrderByDescending(g => g.RegistrationDate) : queryable.OrderBy(g => g.RegistrationDate),
            "booklet" => query.SortDescending ? queryable.OrderByDescending(g => g.Condition.Booklet) : queryable.OrderBy(g => g.Condition.Booklet),
            "box" => query.SortDescending ? queryable.OrderByDescending(g => g.Condition.Box) : queryable.OrderBy(g => g.Condition.Box),
            "media" => query.SortDescending ? queryable.OrderByDescending(g => g.Condition.Media) : queryable.OrderBy(g => g.Condition.Media),
            _ => query.SortDescending ? queryable.OrderByDescending(g => g.Name) : queryable.OrderBy(g => g.Name)
        };

        var items = await queryable
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Game>(items, totalCount, query.Page, query.PageSize);
    }

    public async Task<List<string>> GetDistinctPlatformsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Games
            .Select(g => g.Platform)
            .Distinct()
            .OrderBy(p => p)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Game game, CancellationToken cancellationToken = default)
    {
        context.Games.Add(game);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Game game, CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Game game, CancellationToken cancellationToken = default)
    {
        context.Games.Remove(game);
        await context.SaveChangesAsync(cancellationToken);
    }
}
