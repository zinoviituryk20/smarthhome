using DeviceModel = SmartHome.Device.Domain.Models.Devices.Device;

namespace SmartHome.Device.Data.DB.Repository.Devices;

public interface IDevicesRepository
{
    Task<DeviceModel> GetAsync(string deviceId);

    Task<ICollection<DeviceModel>> GetAllAsync();

    Task<DeviceModel> CreateAsync(DeviceModel device);

    Task<DeviceModel> UpdateAsync(DeviceModel device);

    Task<bool> DeleteAsync(string deviceId);

    Task UpdateDeviceStatus(string deviceId, bool isActive);
}
