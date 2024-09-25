using jaranilla_09252024.application.Implementation.Services.Implementation;
using jaranilla_09252024.application.Implementation.Services.Interfaces;
using jaranilla_09252024.application.Interfaces.Repositories;
using jaranilla_09252024.application.Interfaces.Services;
using jaranilla_09252024.domain.Domain;
using jaranilla_09252024.Models.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jaranilla_09252024.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzaController : Controller
    {
        private readonly IPizzaRepositoryService _pizzaRepositoryService;
        private readonly ILoggingService _loggingService;

        public PizzaController(IPizzaRepositoryService pizzaRepositoryService, ILoggingService loggingService)
        {
            _pizzaRepositoryService = pizzaRepositoryService;
            _loggingService = loggingService;
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
