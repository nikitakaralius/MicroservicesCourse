﻿namespace PlatformService.Controllers;

[ApiController, Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;

    public PlatformsController(IPlatformRepository repository, IMapper mapper, ICommandDataClient commandDataClient)
    {
        _repository = repository;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
    }

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
    public async Task<ActionResult<PlatformToRead>> Create(PlatformToCreate? platformToCreate)
    {
        if (platformToCreate is null)
            return BadRequest();
        Platform platform = _mapper.Map<Platform>(platformToCreate);
        _repository.Create(platform);
        _repository.SaveChanges();
        PlatformToRead platformToRead = _mapper.Map<PlatformToRead>(platform);

        try
        {
            await _commandDataClient.SendPlatformToCommandAsync(platformToRead);
        }
        catch (Exception e)
        {
            Console.WriteLine($"==> Could not send synchronously: {e.Message}");
        }

        return CreatedAtRoute(nameof(PlatformBy), new {Id = platformToRead.Id}, platformToRead);
    }
}
