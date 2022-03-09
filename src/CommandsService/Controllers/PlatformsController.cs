namespace CommandsService.Controllers;

[ApiController, Route("api/c/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly ICommandRepository _repository;
    private readonly IMapper _mapper;

    public PlatformsController(ICommandRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult TestInboundConnection()
    {
        Console.WriteLine("==> Inbound POST # Command service");
        return Ok("Inbound test from Platforms Controller");
    }
}
