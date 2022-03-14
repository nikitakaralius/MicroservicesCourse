namespace CommandsService.Extensions;

public static class RepositoryExtensions
{
    public static bool ExternalPlatformsExists(this ICommandRepository repository, Platform platform) =>
        repository.ExternalPlatformsExists(platform.ExternalId);
}
