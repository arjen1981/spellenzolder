using GameCatalog.Domain;
using MediatR;

namespace GameCatalog.Application;

using Result = Result<Game, Exception>;

public static class AddGame
{
    public sealed record Command(
        string Name,
        string Platform,
        int BookletRating,
        int BoxRating,
        int MediaRating) : IRequest<Result>;

    public sealed class Handler(IGameRepository repository) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var condition = new ConditionRating(request.BookletRating, request.BoxRating, request.MediaRating);
            var game = new Game(request.Name, request.Platform, condition);

            await repository.AddAsync(game, cancellationToken);

            return game;
        }
    }
}
