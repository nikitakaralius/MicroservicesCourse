namespace PlatformService.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        CreateMap<Platform, PlatformToRead>();
        CreateMap<PlatformToCreate, Platform>()
            .ConvertUsing(p => new Platform(0, p.Name, p.Publisher, p.Cost));
        CreateMap<Platform, PlatformToPublish>();
    }
}
