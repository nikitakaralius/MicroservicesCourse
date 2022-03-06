namespace CommandsService.Controllers;

[ApiController, Route("api/c/[controller]")]
public class PlatformsController : ControllerBase
{
    [HttpPost]
    public IActionResult TestInboundConnection()
    {
        Console.WriteLine("==> Inbound POST # Command service");
        return Ok("Inbound test from Platforms Controller");
    }
}
