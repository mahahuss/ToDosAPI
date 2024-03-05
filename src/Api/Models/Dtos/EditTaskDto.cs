using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Api.Models.Entities;

namespace Api.Models.Dtos;

public class EditTaskDto
{
    [JsonPropertyName("id")]
    [Required] public int Id { get; set; }
    [JsonPropertyName("createdBy")]
    [Required] public int CreatedBy { get; set; }
    [JsonPropertyName("taskContent")]
    [Required] public string TaskContent { get; set; } = default!;
    [JsonPropertyName("status")]
    [Required] public bool Status { get; set; }
    [JsonPropertyName("files")]
    public List<TaskAttachment> Files { get; set; } = [];
    [JsonPropertyName("sharedTasks")]
    public List<SharedTask> SharedTasks { get; set; } = [];

}

public class EditTaskFormDto
{

    [Required] public string Task { get; set; } = default!;
    public List<IFormFile> Files { get; set; } = [];
}