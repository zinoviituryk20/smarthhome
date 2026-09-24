using Microsoft.AspNetCore.Mvc;
using SmartHome.Device.Data.DB.Repository.Locations;
using SmartHome.Device.Domain.Models.Locations;

namespace SmartHome.DeviceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController(ILocationsRepository locationsRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var locations = await locationsRepository.GetAllAsync();
        return Ok(locations);
    }

    [HttpGet("id")]
    public async Task<IActionResult> Get(string id)
    {
        var location = await locationsRepository.GetAsync(id);

        if (location == null)
            return NotFound();

        return Ok(location);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Location location)
    {
        if (location == null)
            return BadRequest();

        await locationsRepository.CreateAsync(location);

        return Ok(location);
    }

    [HttpPut()]
    public async Task<IActionResult> Put([FromBody] Location location)
    {
        if (location == null)
            return BadRequest(location);

        await locationsRepository.UpdateAsync(location);

        return Ok(location);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(string id)
    {
        var isDeleted = await locationsRepository.DeleteAsync(id);

        if (!isDeleted)
            return NotFound();

        return Ok();

    }
}
