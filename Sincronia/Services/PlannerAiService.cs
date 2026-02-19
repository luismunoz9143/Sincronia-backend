using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Sincronia.Services
{
    public class PlannerAiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PlannerAiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GenerateSmartPlanAsync(string recipeName, List<string> tasks)
        {
            var tasksList = string.Join(", ", tasks);

            // PROMPT MEJORADO: Reglas estrictas de tiempo, espacio y seguridad.
            var prompt = $@"Eres un experto planificador de productividad llamado 'Sincronía'.
                 DATOS DEL USUARIO:
                 1. Receta del día sugerida: {recipeName}
                 2. Tareas pendientes a realizar hoy: {tasksList}

                 TU MISIÓN:
                 Genera una rutina lógica y realista que organice el día del usuario, incluyendo un momento para cocinar esta receta.

                 REGLAS ESTRICTAS E INQUEBRANTABLES:
                 1. SEGURIDAD FÍSICA Y LÓGICA: ¡NUNCA sugieras salir de casa (ej. ir a clases de conducción, hacer visitas, ir al supermercado) mientras se está cocinando! Las tareas paralelas a la cocina SOLO pueden ser dentro de casa (ej. barrer, lavar platos, leer).
                 2. DISTRIBUCIÓN DEL DÍA: NO intentes meter todas las tareas en el tiempo que toma cocinar. Crea una línea de tiempo para todo el día (Mañana, Tarde, Noche) y distribuye las tareas de forma holgada y realista.
                 3. Sé directo y estructurado. Usa un formato claro de horas o bloques del día.
                 
                 REGLAS DE SEGURIDAD (Obligatorias):
                 - NO des consejos médicos, nutricionales ni psicológicos.
                 - Si la receta es poco saludable, NO la juzgues, solo sugiere equilibrio.
                 - Usa lenguaje neutral (ej: 'Se sugiere que...', 'Podrías intentar...').";

            return await GetAiResponseAsync(prompt);
        }

        private async Task<string> GetAiResponseAsync(string prompt)
        {
            var apiKey = _configuration["Groq:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Groq API key is not configured in appsettings.json.");
            }

            var client = _httpClientFactory.CreateClient();
            var requestBody = new
            {
                model = "llama-3.1-8b-instant",
                messages = new[] {
                    new { role = "user", content = prompt }
                },
                temperature = 0.6,
                // AQUÍ ESTABA EL ERROR: Aumentamos de 500 a 1500 para que no se corte la respuesta
                max_tokens = 1500
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions")
            {
                Content = httpContent
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error de Groq: {error}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(responseJson);

            return document.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? string.Empty;
        }
    }
}