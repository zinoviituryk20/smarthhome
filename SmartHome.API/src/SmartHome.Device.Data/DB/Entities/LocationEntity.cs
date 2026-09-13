using MongoDB.Bson.Serialization.Attributes;

namespace SmartHome.Device.Data.DB.Entities;

internal class LocationEntity
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string[] LocationDeviceIds { get; set; } = [];

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}
