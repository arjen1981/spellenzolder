using GameCatalog.Domain;
using MediatR;

namespace GameCatalog.Application;

using Result = Result<Game, Exception>;

public static class UpdateGame
{
    public sealed record Command(
        Guid Id,
        string Name,
        string Platform,
        int BookletRating,
        int BoxRating,
        int MediaRating) : IRequest<Result>;

    public sealed class Handler(IGameRepository repository) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var game = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (game is null)
                return new KeyNotFoundException($"Game with id '{request.Id}' was not found.");

            var condition = new ConditionRating(request.BookletRating, request.BoxRating, request.MediaRating);
            game.Update(request.Name, request.Platform, condition);

            await repository.UpdateAsync(game, cancellationToken);

            return game;
        }
    }
}
