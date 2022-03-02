namespace PlatformService.Controllers;

[ApiController, Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepository repository, IMapper mapper) =>
        (_repository, _mapper) = (repository, mapper);

    [HttpGet]
    public ActionResult<IEnumerable<PlatformToRead>> AllPlatforms() =>
        Ok(_mapper.Map<IEnumerable<PlatformToRead>>(
            _repository.AllPlatforms()));

    [HttpGet("{id:int}", Name = nameof(PlatformBy))]
    public ActionResult<PlatformToRead> PlatformBy(int id)
    {
        Platform? platform = _repository.PlatformBy(id);
        return platform is null ? NotFound() : Ok(_mapper.Map<PlatformToRead>(platform));
    }

    [HttpPost]
    public ActionResult<PlatformToRead> Create(PlatformToCreate? platformToCreate)
    {
        if (platformToCreate is null)
            return BadRequest();
        Platform platform = _mapper.Map<Platform>(platformToCreate);
        _repository.Create(platform);
        _repository.SaveChanges();
        PlatformToRead platformToRead = _mapper.Map<PlatformToRead>(platform);
        return CreatedAtRoute(nameof(PlatformBy), new {Id = platformToRead.Id}, platformToRead);
    }
}
