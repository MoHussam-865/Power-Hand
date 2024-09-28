

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyDatabase.Models
{
    public class Post
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("imagePath")]
        public string? ImagePath { get; set; }

    }
}
