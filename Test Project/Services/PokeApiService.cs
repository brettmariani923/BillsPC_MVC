using System.Text.Json;                       
using StatFinder.Models.PokeApi;    


namespace StatFinder.Services
{
    public class PokeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _env;
        private readonly string _imageCacheFolder;

        public PokeApiService(HttpClient httpClient, IWebHostEnvironment env)
        {
            _httpClient = httpClient;
            _env = env;
            _imageCacheFolder = Path.Combine(_env.WebRootPath, "images", "pokemon");

            // Ensure the folder exists
            if (!Directory.Exists(_imageCacheFolder))
                Directory.CreateDirectory(_imageCacheFolder);
        }

        public async Task<string> GetPokemonImageUrlAsync(string name)
        {
            var fileName = $"{name.ToLower()}.png";
            var localFilePath = Path.Combine(_imageCacheFolder, fileName);

            // Use cached image if it already exists
            if (File.Exists(localFilePath))
            {
                return $"/images/pokemon/{fileName}";
            }

            // Get image from PokeAPI
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<PokeApiResponse>(json, options);

            var imageUrl = data?.Sprites?.Other?.OfficialArtwork?.FrontDefault;
            if (string.IsNullOrEmpty(imageUrl))
                return null;

            // Download and save the image locally
            var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
            await File.WriteAllBytesAsync(localFilePath, imageBytes);

            return $"/images/pokemon/{fileName}";
        }
    }
}
