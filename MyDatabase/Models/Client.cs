using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyDatabase.Models
{
    public class Client
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonIgnore]
        public double Discount { get; set; } = 0;

        [JsonIgnore]
        public string? Notes { get; set; }
    }
}
