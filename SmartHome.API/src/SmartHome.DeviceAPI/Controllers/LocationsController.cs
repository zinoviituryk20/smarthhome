using Microsoft.AspNetCore.Mvc;

namespace SmartHome.DeviceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet("id")]
    public IActionResult Get(string id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Create()
    {
        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult Delete(string id)
    {
        return Ok();
    }
}
