namespace PlatformService.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        CreateMap<Platform, PlatformToRead>();
        CreateMap<PlatformToCreate, Platform>();
    }
}
