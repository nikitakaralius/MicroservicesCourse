namespace CommandsService.Mappings.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        CreateMap<Platform, PlatformToRead>();
        CreateMap<PlatformToReceive, Platform>()
            .ForMember(p => p.ExternalId, src => src.MapFrom(x => x.Id));
    }
}
