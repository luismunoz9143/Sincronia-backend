using System.Text.Json;

namespace Sincronia.Services
{
    public class RecipeService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _config;

        public RecipeService(IHttpClientFactory factory, IConfiguration config)
        {
            _factory = factory;
            _config = config;
        }

        public async Task<string> GetRecipeAsync(string diet, int maxTime)
        {
            var apiKey = _config["Spoonacular:ApiKey"];
            var client = _factory.CreateClient();

            // Construcción de URL para la API pública (Fuente externa 1)
            var url = $"https://api.spoonacular.com/recipes/complexSearch?diet={diet}&maxReadyTime={maxTime}&number=1&apiKey={apiKey}";

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return "No se encontró una receta específica.";

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            // Verificamos si la propiedad 'results' existe para evitar excepciones
            if (doc.RootElement.TryGetProperty("results", out var results))
            {
                return results.GetArrayLength() > 0
                    ? results[0].GetProperty("title").GetString() ?? "una comida misteriosa"
                    : "una comida ligera y rápida";
            }

            return "una opción saludable";
        }
    }
}