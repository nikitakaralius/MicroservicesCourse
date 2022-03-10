namespace CommandsService.Controllers;

[ApiController, Route("api/c/platforms/{platformId:int}/[controller]")]
public class CommandsController : ControllerBase
{
    private readonly ICommandRepository _repository;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandToRead>> AllCommandsFor(int platformId)
    {
        if (_repository.PlatformExists(platformId) == false)
        {
            return NotFound();
        }
        IEnumerable<Command> commands = _repository.CommandsForPlatform(platformId);
        IEnumerable<CommandToRead> commandsToRead = _mapper.Map<IEnumerable<CommandToRead>>(commands);
        return Ok(commandsToRead);
    }

    [HttpGet("{commandId:int}",Name = nameof(CommandBy))]
    public ActionResult<CommandToRead> CommandBy(int platformId, int commandId)
    {
        if (_repository.PlatformExists(platformId) == false)
        {
            return NotFound();
        }
        Command? command = _repository.CommandBy(platformId, commandId);
        if (command is null)
        {
            return NotFound();
        }
        CommandToRead commandToRead = _mapper.Map<CommandToRead>(command);
        return Ok(commandToRead);
    }

    [HttpPost]
    public ActionResult<CommandToRead> Create(int platformId, CommandToCreate commandToCreate)
    {
        if (_repository.PlatformExists(platformId) == false)
        {
            return BadRequest();
        }
        Command command = _mapper.Map<Command>(commandToCreate);
        _repository.Create(platformId, command);
        _repository.SaveChanges();
        CommandToRead commandToRead = _mapper.Map<CommandToRead>(command);
        return CreatedAtRoute(nameof(CommandBy),
            new {PlatformId = platformId, CommandId = commandToRead.Id},
            commandToRead);
    }
}
