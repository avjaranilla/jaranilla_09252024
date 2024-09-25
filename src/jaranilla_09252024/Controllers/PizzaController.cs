using jaranilla_09252024.application.Interfaces.Repositories;
using jaranilla_09252024.application.Interfaces.Services;
using jaranilla_09252024.domain.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jaranilla_09252024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaController(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        // Endpoint for adding a single pizza
        [HttpPost]
        [Route("add-single-record")]
        [ApiKey]
        public async Task<IActionResult> AddPizza([FromBody] Pizza pizza)
        {
            await _pizzaRepository.AddPizzaAsync(pizza);
            return CreatedAtAction(nameof(GetProcessedPizzas), new { id = pizza.Id }, pizza);

        }

        [HttpGet]
        [Route("get")]
        [ApiKey]
        public async Task<ActionResult<List<Pizza>>> GetProcessedPizzas()
        {
            var pizzas = await _pizzaRepository.GetProcessedPizzasAsync();
            return Ok(pizzas);
        }

    }
}
