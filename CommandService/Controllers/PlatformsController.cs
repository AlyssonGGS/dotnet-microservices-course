using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controller;

[Route("api/command/[controller]")]
[ApiController]
public class PlatformsController: ControllerBase
{
    public PlatformsController()
    {
        
    }

    [HttpPost]
    public ActionResult TestInBoundConnect()
    {
        Console.WriteLine("--> Inboud POST");
        return Ok("inbound test from Platforms Controller");
    }
    
}