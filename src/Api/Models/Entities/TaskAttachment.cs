using System.Text.Json.Serialization;

namespace Api.Models.Entities;

public class TaskAttachment
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("taskId")]
    public int TaskId { get; set; }
    [JsonPropertyName("fileName")]
    public string FileName { get; set; } = default!;
}