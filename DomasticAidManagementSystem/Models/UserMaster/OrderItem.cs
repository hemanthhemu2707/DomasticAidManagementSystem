using System.Text.Json.Serialization;

namespace DomasticAidManagementSystem.Models.UserMaster
{
    public class OrderItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("qty")]
        public int Qty { get; set; }
    }
}
