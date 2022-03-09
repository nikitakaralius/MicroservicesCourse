namespace CommandsService.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile() => CreateMap<Platform, PlatformTorRead>();
}
