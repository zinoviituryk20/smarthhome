using MongoDB.Driver;
using SmartHome.Device.Data.Configurations;
using SmartHome.Device.Data.DB.Entities;
using DeviceModel = SmartHome.Device.Domain.Models.Devices.Device;

namespace SmartHome.Device.Data.DB.Repository.Devices;

internal class DevicesRepository(
    IMongoDatabase mongoDatabase) : IDevicesRepository
{
    private readonly IMongoCollection<DeviceEntity> _devicesCollection = mongoDatabase.GetCollection<DeviceEntity>(MongoDevicesDatabaseCollection.Devices);
    public async Task<DeviceModel> CreateAsync(DeviceModel device)
    {
        var deviceEntity = new DeviceEntity
        {
            Name = device.Name,
            IsActive = device.IsActive,
            LocationId = device.LocationId,
            IpAdress = device.IpAdress,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        await _devicesCollection.InsertOneAsync(deviceEntity);

        return device;
    }

    public async Task<bool> DeleteAsync(string deviceId)
    {
        var deletionResult = await _devicesCollection.DeleteOneAsync(deviceId);

        return deletionResult.DeletedCount > 0;
    }

    public async Task<ICollection<DeviceModel>> GetAllAsync()
    {
        var deviceEntities = await _devicesCollection
            .Find(_ => true)
            .ToListAsync();

        var devicesCollection = deviceEntities.Select(d => new DeviceModel
        {
            Id = d.Id,
            Name = d.Name,
            IpAdress = d.IpAdress,
            IsActive = d.IsActive,
            LocationId = d.LocationId,
        })
            .ToList();

        return devicesCollection;
    }

    public async Task<DeviceModel> GetAsync(string deviceId)
    {
        var deviceEntity = await _devicesCollection
            .Find(d => d.Id == deviceId)
            .FirstOrDefaultAsync();

        return new()
        {
            Id = deviceEntity.Id,
            Name = deviceEntity.Name,
            IpAdress = deviceEntity.IpAdress,
            IsActive = deviceEntity.IsActive,
            LocationId = deviceEntity.LocationId
        };
    }

    public async Task<DeviceModel> UpdateAsync(DeviceModel device)
    {
        await _devicesCollection.UpdateOneAsync(
            d => d.Id == device.Id,
            Builders<DeviceEntity>.Update
            .Set(d => d.Name, device.Name)
            .Set(d => d.IpAdress, device.IpAdress)
            .Set(d => d.IsActive, device.IsActive)
            .Set(d => d.LocationId, device.LocationId)
            .Set(d => d.UpdatedDate, DateTime.UtcNow));

        return device;
    }

    public async Task UpdateDeviceStatus(string deviceId, bool isActive)
    {
        await _devicesCollection.UpdateOneAsync(
            d => d.Id == deviceId,
            Builders<DeviceEntity>.Update
            .Set(d => d.IsActive, isActive)
            .Set(d => d.UpdatedDate, DateTime.UtcNow));
    }
}
