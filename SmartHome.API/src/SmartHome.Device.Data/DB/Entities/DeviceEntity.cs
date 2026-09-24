using MongoDB.Bson.Serialization.Attributes;

namespace SmartHome.Device.Data.DB.Entities;

internal class DeviceEntity
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = false;

    public string IpAdress { get; set; } = null!;

    public string? LocationId { get; set; } = null;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}
