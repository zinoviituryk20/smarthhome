using SmartHome.Device.Domain.Models.Locations;

namespace SmartHome.Device.Data.DB.Repository.Locations;

public interface ILocationsRepository
{
    Task<ICollection<Location>> GetAllAsync();

    Task<Location?> GetAsync(string locationId);

    Task<Location> CreateAsync(Location location);

    Task<Location> UpdateAsync(Location location);

    Task<bool> DeleteAsync(string locationId);
}
