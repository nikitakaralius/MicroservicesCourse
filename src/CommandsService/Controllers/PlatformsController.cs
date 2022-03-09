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

    [HttpGet]
    public ActionResult<IEnumerable<Platform>> AllPlatforms()
    {
        IEnumerable<Platform> platforms = _repository.AllPlatforms();
        IEnumerable<PlatformTorRead> platformsTorRead = _mapper.Map<IEnumerable<PlatformTorRead>>(platforms);
        return platformsTorRead.Any()
            ? Ok(platformsTorRead)
            : NoContent();
    }

    [HttpPost]
    public IActionResult TestInboundConnection()
    {
        Console.WriteLine("==> Inbound POST # Command service");
        return Ok("Inbound test from Platforms Controller");
    }
}
