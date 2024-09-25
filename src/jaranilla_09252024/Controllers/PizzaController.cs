using jaranilla_09252024.application.Services.Interfaces;
using jaranilla_09252024.domain.Domain;
using jaranilla_09252024.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace jaranilla_09252024.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
    [ApiKey]
    public class PizzaController : Controller
    {
        private readonly IPizzaRepositoryService _pizzaRepositoryService;
        private readonly ILoggingService _loggingService;
        private readonly IJsonReaderService _jsonReaderService;

        public PizzaController(IPizzaRepositoryService pizzaRepositoryService, ILoggingService loggingService, IJsonReaderService jsonReaderService)
        {
            _pizzaRepositoryService = pizzaRepositoryService;
            _loggingService = loggingService;
            _jsonReaderService = jsonReaderService;
        }


        [HttpPost]
        [Route("upload - json")]
        public async Task<IActionResult> UploadJson(IFormFile jsonFile)
        {
            if (jsonFile == null || jsonFile.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            try
            {
                using (var stream = jsonFile.OpenReadStream())
                {
                    // Call the JSON reader service to process the file
                    var result = await _jsonReaderService.ProcessJsonAsync(stream, jsonFile.FileName);

                    if (result == null)
                    {
                        return BadRequest("Failed to process JSON file. Please check the file format or details.");
                    }
                    return Ok(result);
                }               
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync("Error in uploadJson endpoint", ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return null;
        }       

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<List<Pizza>>> GetProcessedPizzas()
        {
            try
            {
                var pizzas = await _pizzaRepositoryService.GetProcessedPizzasAsync();
                if (pizzas.Count() <= 0)
                {
                    return NoContent();
                }
                return Ok(pizzas);
            }
            catch (Exception ex) 
            {
                await _loggingService.LogErrorAsync("Error in GetProcessedPizza endpoint", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
            
        }

        [HttpGet]
        [Route("get-byStatus")]
        public async Task<ActionResult<List<Pizza>>> GetPizzasByStatus([FromQuery] bool isActive)
        {
            try 
            {
                var pizzas = await _pizzaRepositoryService.GetPizzasByStatus(isActive);

                if (pizzas.Count() <= 0)
                {
                    return NoContent();
                }

                return Ok(pizzas);
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync("Error in GetPizzaByStatus endpoint", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
           
        }

    }
}
