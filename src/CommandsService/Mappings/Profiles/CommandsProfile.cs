namespace CommandsService.Mappings.Profiles;

public class CommandsProfile : Profile
{
    public CommandsProfile()
    {
        CreateMap<Command, CommandToRead>();
        CreateMap<CommandToCreate, Command>();
    }
}
