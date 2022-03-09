namespace CommandsService.Mappings.Profiles;

public class CommandsProfile : Profile
{
    public CommandsProfile()
    {
        CreateMap<Command, CommandToRead>();
        CreateMap<CommandToCreate, Command>()
            .ConvertUsing(c => new Command(0, c.HowTo, c.Line, 0, Platform.NullObject));
    }
}
