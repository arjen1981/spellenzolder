using MediatR;

namespace GameCatalog.Application;

using Result = Result<List<string>, Exception>;

public static class GetPlatforms
{
    public sealed record Query : IRequest<Result>;

    public sealed class Handler(IGameRepository repository) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            var platforms = await repository.GetDistinctPlatformsAsync(cancellationToken);

            return platforms;
        }
    }
}
