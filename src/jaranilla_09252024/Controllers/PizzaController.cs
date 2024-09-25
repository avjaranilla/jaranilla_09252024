using jaranilla_09252024.application.Implementation.Services.Interfaces;
using jaranilla_09252024.application.Interfaces.Repositories;
using jaranilla_09252024.application.Interfaces.Services;
using jaranilla_09252024.domain.Domain;
using jaranilla_09252024.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jaranilla_09252024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : Controller
    {
        private readonly IPizzaRepositoryService _pizzaRepositoryService;

        public PizzaController(IPizzaRepositoryService pizzaRepositoryService)
        {
            _pizzaRepositoryService = pizzaRepositoryService;
        }


        // Endpoint for adding a single pizza
        [HttpPost]
        [Route("add-single-record")]
        [ApiKey]
        public async Task<IActionResult> AddPizza([FromBody] PizzaRequestModel pizzaRequest)
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

        [HttpGet]
        [Route("get")]
        [ApiKey]
        public async Task<ActionResult<List<Pizza>>> GetProcessedPizzas()
        {
            var pizzas = await _pizzaRepositoryService.GetProcessedPizzasAsync();
            if (pizzas.Count() <= 0)
            {
                return NoContent();
            }
            return Ok(pizzas);
        }

        [HttpGet]
        [Route("get-active")]
        [ApiKey]
        public async Task<ActionResult<List<Pizza>>> GetPizzasByStatus([FromQuery] bool isActive)
        {
            var pizzas = await _pizzaRepositoryService.GetPizzasByStatus(isActive);

            if (pizzas.Count() <= 0)
            {
                return NoContent();
            }

            return Ok(pizzas);
        }

    }
}
