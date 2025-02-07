using System.Text.Json.Serialization;

namespace ChatbotApi.Models
{
    public class GitHubRepository
    {
        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

    }
}
