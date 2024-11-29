using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase{
    [HttpGet("hello")]
    public ActionResult<string> HelloWorld()
    {
        return Ok("Hello World");
    }
}