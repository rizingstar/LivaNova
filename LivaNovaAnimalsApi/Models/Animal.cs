using Newtonsoft.Json;

namespace LivaNovaAnimalsApi.Models
{
    public class Animal
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "animalName")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
        [JsonProperty(PropertyName = "details")]
        public string Details { get; set; }
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "animalType")]
        public string AnimalType { get; set; }
        [JsonProperty(PropertyName = "animalCategory")]
        public string AnimalCategory { get; set; }
    }
}
