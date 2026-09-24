using DeviceModel = SmartHome.Device.Domain.Models.Devices.Device;

namespace SmartHome.Device.Domain.Models.Locations;

public class Location
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string[] LocationDevicesIds { get; set; } = [];

    public DateTime CreatedDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public ICollection<DeviceModel> Devices { get; set; } = [];
}
