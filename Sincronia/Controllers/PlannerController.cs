using Microsoft.AspNetCore.Mvc;
using Sincronia.Models;
using Sincronia.Services;

namespace Sincronia.Controllers
{
    [ApiController]
    [Route("api/planner")]
    public class PlannerController : ControllerBase
    {
        private readonly RecipeService _recipeService;
        private readonly PlannerAiService _aiService;

        public PlannerController(RecipeService recipeService, PlannerAiService aiService)
        {
            _recipeService = recipeService;
            _aiService = aiService;
        }

        [HttpPost("organize-day")]
        public async Task<ActionResult<PlannerResponse>> OrganizedDay([FromBody] PlannerRequest request)
        {
            try
            {
                // 1. Obtener la receta (API Externa 1)
                var recipe = await _recipeService.GetRecipeAsync(request.DietType, request.MaxReadyTime);

                // 2. Generar el plan con IA (API Externa 2)
                var smartPlan = await _aiService.GenerateSmartPlanAsync(recipe, request.Tasks);

                // 3. Retornar el DTO limpio (Corrección aplicada)
                return Ok(new PlannerResponse
                {
                    SugerenciaAlimentacion = recipe,
                    PlanMaestro = smartPlan,
                    Status = "Transformación exitosa"
                });
            }
            catch (Exception ex)
            {
                // Manejo de error controlado
                return StatusCode(500, new { error = "Error al procesar el plan", detalle = ex.Message });
            }
        }
    }
}