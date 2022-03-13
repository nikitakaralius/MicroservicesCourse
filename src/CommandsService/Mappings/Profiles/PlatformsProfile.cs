namespace CommandsService.Mappings.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        CreateMap<Platform, PlatformToRead>();
        CreateMap<PlatformToReceive, Platform>()
            .ConvertUsing(p => new Platform
            {
                ExternalId = p.Id,
                Name = p.Name
            });
    }
}
