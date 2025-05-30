using System.Text.Json.Serialization;

namespace StatFinder.Models.PokeApi
{
    public class PokeApiResponse
    {
        public PokeApiSpriteContainer Sprites { get; set; }
    }

    public class PokeApiSpriteContainer
    {
        public OtherSprites Other { get; set; }
    }

    public class OtherSprites
    {
        [JsonPropertyName("official-artwork")]
        public OfficialArtwork OfficialArtwork { get; set; }
    }

    public class OfficialArtwork
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }
    }
}
