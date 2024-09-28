using MyDatabase.Models;
using System.Text.Json.Serialization;

namespace CablesMarketAPI.Models
{
    public class Message
    {
        [JsonPropertyName("lastUpdate")]
        public int LastUpdate { get; set; }

        [JsonPropertyName("items")]
        public List<Item>? Items { get; set; }

        /*[JsonPropertyName("invoice")]
        public Invoice? Invoice { get; set; }*/
        
        [JsonPropertyName("posts")]
        public List<Post>? Post {  get; set; }

        [JsonPropertyName("message")]
        public string? MyMessage { get; set; }
        


        public Message(int lastUpdate, List<Item> items,
            List<Post> posts, Invoice? invoice = null,
            string? message = null) 
        {
            LastUpdate = lastUpdate;
            Items = items;
            Post = posts;
            //Invoice = invoice;
            MyMessage = message;
        }

        public Message() { }
    }
}
