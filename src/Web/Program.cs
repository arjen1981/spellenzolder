using GameCatalog.Application;
using GameCatalog.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddSqlServerDbContext<GameCatalogDbContext>("gamecatalog");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddGameCatalogApplication();
builder.Services.AddScoped<IGameRepository, GameRepository>();

var app = builder.Build();

app.UseCors();
app.MapDefaultEndpoints();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GameCatalogDbContext>();
    await db.Database.EnsureCreatedAsync();
}

// Game endpoints
var games = app.MapGroup("/api/games");

games.MapPost("/", async (AddGameRequest request, IMediator mediator) =>
{
    var command = new AddGame.Command(request.Name, request.Platform, request.BookletRating, request.BoxRating, request.MediaRating);
    var result = await mediator.Send(command);

    return result.IsSuccess
        ? Results.Created($"/api/games/{result.Value.Id}", MapToResponse(result.Value))
        : Results.Problem(result.Error.Message, statusCode: 400);
});

games.MapGet("/", async (
    string? search,
    string? platforms,
    int? minBooklet,
    int? minBox,
    int? minMedia,
    string? sortBy,
    bool? sortDescending,
    int? page,
    int? pageSize,
    IMediator mediator) =>
{
    var platformList = platforms?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
    var query = new GetGames.Query(
        Search: search,
        Platforms: platformList,
        MinBooklet: minBooklet,
        MinBox: minBox,
        MinMedia: minMedia,
        SortBy: sortBy ?? "Name",
        SortDescending: sortDescending ?? false,
        Page: page ?? 1,
        PageSize: pageSize ?? 20);

    var result = await mediator.Send(query);

    return result.IsSuccess
        ? Results.Ok(new PagedResponse(
            result.Value.Items.Select(MapToResponse).ToList(),
            result.Value.TotalCount,
            result.Value.Page,
            result.Value.PageSize,
            result.Value.TotalPages))
        : Results.Problem(result.Error.Message, statusCode: 500);
});

games.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var query = new GetGames.Query(Page: 1, PageSize: 1);
    // Use repository directly for single item lookup
    using var scope = app.Services.CreateScope();
    var repository = scope.ServiceProvider.GetRequiredService<IGameRepository>();
    var game = await repository.GetByIdAsync(id);

    return game is not null
        ? Results.Ok(MapToResponse(game))
        : Results.NotFound();
});

games.MapPut("/{id:guid}", async (Guid id, UpdateGameRequest request, IMediator mediator) =>
{
    var command = new UpdateGame.Command(id, request.Name, request.Platform, request.BookletRating, request.BoxRating, request.MediaRating);
    var result = await mediator.Send(command);

    if (result.IsSuccess)
        return Results.Ok(MapToResponse(result.Value));

    return result.Error is KeyNotFoundException
        ? Results.NotFound()
        : Results.Problem(result.Error.Message, statusCode: 400);
});

games.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var command = new DeleteGame.Command(id);
    var result = await mediator.Send(command);

    if (result.IsSuccess)
        return Results.NoContent();

    return result.Error is KeyNotFoundException
        ? Results.NotFound()
        : Results.Problem(result.Error.Message, statusCode: 500);
});

// Platforms endpoint
app.MapGet("/api/platforms", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetPlatforms.Query());

    return result.IsSuccess
        ? Results.Ok(result.Value)
        : Results.Problem(result.Error.Message, statusCode: 500);
});

app.Run();

static GameResponse MapToResponse(GameCatalog.Domain.Game game) => new(
    game.Id,
    game.Name,
    game.Platform,
    game.Condition.Booklet,
    game.Condition.Box,
    game.Condition.Media,
    game.RegistrationDate);

// Request/Response records
public record AddGameRequest(string Name, string Platform, int BookletRating, int BoxRating, int MediaRating);
public record UpdateGameRequest(string Name, string Platform, int BookletRating, int BoxRating, int MediaRating);
public record GameResponse(Guid Id, string Name, string Platform, int BookletRating, int BoxRating, int MediaRating, DateOnly RegistrationDate);
public record PagedResponse(List<GameResponse> Items, int TotalCount, int Page, int PageSize, int TotalPages);

public partial class Program { }
