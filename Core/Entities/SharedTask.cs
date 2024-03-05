using System.Text.Json.Serialization;

namespace Core.Entities;

public class SharedTask
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("taskId")]

    public int TaskId { get; set; }
    [JsonPropertyName("sharedBy")]

    public int SharedBy { get; set; }
    [JsonPropertyName("sharedWith")]

    public int SharedWith { get; set; }
    [JsonPropertyName("isEditable")]

    public bool IsEditable { get; set; }
    [JsonPropertyName("sharedDate")]

    public DateTime SharedDate { get; set; }
}