namespace Company.Shorts.Infrastructure.Http.ExternalApi.Internal.Models
{
    using System.Text.Json.Serialization;

    public class PetDto
    {
        public PetDto() { }

        public PetDto(int id, string name, string tag)
        {
            Id = id;
            Name = name;
            Tag = tag;
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("tag")]
        public string Tag { get; set; } = default!;
    }
}
