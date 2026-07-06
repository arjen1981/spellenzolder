using MediatR;

namespace GameCatalog.Application;

using Result = Result<bool, Exception>;

public static class DeleteGame
{
    public sealed record Command(Guid Id) : IRequest<Result>;

    public sealed class Handler(IGameRepository repository) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var game = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (game is null)
                return new KeyNotFoundException($"Game with id '{request.Id}' was not found.");

            await repository.DeleteAsync(game, cancellationToken);

            return true;
        }
    }
}
