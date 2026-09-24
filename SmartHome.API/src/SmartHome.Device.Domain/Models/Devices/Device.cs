using SmartHome.Device.Domain.Models.Locations;

namespace SmartHome.Device.Domain.Models.Devices;

public class Device
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = false;

    public string? LocationId { get; set; } = null;

    public string IpAdress { get; set; } = string.Empty;

    public Location Location { get; set; } = null!;
}
