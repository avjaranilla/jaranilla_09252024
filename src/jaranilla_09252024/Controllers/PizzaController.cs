using jaranilla_09252024.application.Services.Interfaces;
using jaranilla_09252024.domain.Domain;
using jaranilla_09252024.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace jaranilla_09252024.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
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


        [HttpPost("upload-json")]
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

        // Endpoint for adding a single pizza
        [HttpPost]
        [Route("add-singleRecord")]
        [ApiKey]
        public async Task<IActionResult> AddPizza([FromBody] PizzaRequestModel pizzaRequest)
        {
            try
            {
                var pizza = new Pizza
                {
                    Name = pizzaRequest.Name,
                    Type = pizzaRequest.Type,
                    Description = pizzaRequest.Description,
                    Size = pizzaRequest.Size,
                    Amount = pizzaRequest.Amount,
                    IsActive = pizzaRequest.IsActive,
                    DateCreated = DateTime.UtcNow
                };

                await _pizzaRepositoryService.AddPizzaAsync(pizza);
                return CreatedAtAction(nameof(GetProcessedPizzas), new { id = pizza.Id }, pizza);
            }
            catch (Exception ex) 
            {
                await _loggingService.LogErrorAsync("Error in AddPizza endpoint", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }        
        }

        [HttpGet]
        [Route("get")]
        [ApiKey]
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
        [ApiKey]
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
