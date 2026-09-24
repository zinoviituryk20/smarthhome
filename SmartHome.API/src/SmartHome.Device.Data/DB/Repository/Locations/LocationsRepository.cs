using MongoDB.Driver;
using SmartHome.Device.Data.Configurations;
using SmartHome.Device.Data.DB.Entities;
using SmartHome.Device.Domain.Models.Locations;

namespace SmartHome.Device.Data.DB.Repository.Locations;

internal class LocationsRepository(IMongoDatabase mongoDatabase) : ILocationsRepository
{
    private readonly IMongoCollection<LocationEntity> _locationsCollection = mongoDatabase.GetCollection<LocationEntity>(MongoDevicesDatabaseCollection.Locations);

    public async Task<Location> CreateAsync(Location location)
    {
        if (location == null)
            return new();

        var locationEntity = new LocationEntity
        {
            Name = location.Name,
            LocationDeviceIds = location.LocationDevicesIds,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        await _locationsCollection.InsertOneAsync(locationEntity);

        return location;
    }

    public async Task<bool> DeleteAsync(string locationId)
    {
        var result = await _locationsCollection.DeleteOneAsync(l => l.Id == locationId);

        return result.DeletedCount > 0;
    }

    public async Task<ICollection<Location>> GetAllAsync()
    {
        var locationEntityCollections = await _locationsCollection
            .Find(_ => true)
            .ToListAsync();

        if (locationEntityCollections == null || !locationEntityCollections.Any())
        {
            return [];
        }

        var collection = locationEntityCollections.Select(le => new Location
        {
            Id = le.Id,
            Name = le.Name,
            LocationDevicesIds = le.LocationDeviceIds,
            CreatedDate = le.CreatedDate,
            UpdateDate = le.UpdatedDate
        })
            .ToList();

        return collection;
    }

    public async Task<Location?> GetAsync(string locationId)
    {
        var locationEntity = await _locationsCollection
            .Find(l => l.Id == locationId)
            .FirstOrDefaultAsync();

        if (locationEntity is null)
            return null;

        return new()
        {
            Id = locationEntity.Id,
            Name = locationEntity.Name,
            LocationDevicesIds = locationEntity.LocationDeviceIds,
            CreatedDate = locationEntity.CreatedDate,
            UpdateDate = locationEntity.UpdatedDate
        };
    }

    public async Task<Location> UpdateAsync(Location location)
    {
        await _locationsCollection.UpdateOneAsync(
            l => l.Id == location.Id,
            Builders<LocationEntity>.Update
            .Set(l => l.Name, location.Name)
            .Set(l => l.LocationDeviceIds, location.LocationDevicesIds)
            .Set(l => l.UpdatedDate, DateTime.UtcNow));

        return location;
    }
}
