using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Post
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; } = default!;
        [JsonPropertyName("body")] public string Body { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = new DateTime();
    }
}