using Microsoft.AspNetCore.Mvc;
using SmartHome.Device.Data.DB.Repository.Devices;

using DeviceModel = SmartHome.Device.Domain.Models.Devices.Device;
namespace SmartHome.DeviceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevicesController(
    IDevicesRepository devicesRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var devices = await devicesRepository.GetAllAsync();
        return Ok(devices);
    }

    [HttpGet("id")]
    public async Task<IActionResult> Get(string id)
    {
        var device = await devicesRepository.GetAsync(id);

        if (device == null)
            return NotFound();

        return Ok(device);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DeviceModel device)
    {
        await devicesRepository.CreateAsync(device);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DeviceModel device)
    {
        await devicesRepository.UpdateAsync(device);

        return Ok();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(string id)
    {
        var isDeleted = await devicesRepository.DeleteAsync(id);

        return isDeleted ? Ok() : NotFound();
    }
}
