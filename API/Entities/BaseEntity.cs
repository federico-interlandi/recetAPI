using System.Text.Json.Serialization;

namespace API.Entities;

public class BaseEntity{
    [JsonIgnore]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime Created {get;set;} = DateTime.UtcNow;

}