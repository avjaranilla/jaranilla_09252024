using jaranilla_09252024.application.Services.Interfaces;
using jaranilla_09252024.domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace jaranilla_09252024.application.Services.Implementation
{
    public class JsonReaderService : IJsonReaderService
    {

        private readonly IPizzaRepositoryService _pizzaRepositoryService;
        private readonly ILoggingService _loggerService;

        public JsonReaderService(IPizzaRepositoryService pizzaRepositoryService, ILoggingService loggerService)
        {
            _pizzaRepositoryService = pizzaRepositoryService;
            _loggerService = loggerService;
        }

        public async Task<bool> ProcessJsonAsync(Stream jsonStream)
        {
            try
            {
                // Deserialize JSON file content into Pizza list
                var pizzas = await JsonSerializer.DeserializeAsync<List<Pizza>>(jsonStream);

         
                if (pizzas == null || !pizzas.Any())
                {
                    throw new Exception("JSON File is empty.");
                }

                // Iterate through each pizza and add to the system
                foreach (var pizza in pizzas)
                {
                    await _pizzaRepositoryService.AddPizzaAsync(pizza);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Optionally log the error
                _loggerService.LogErrorAsync($"Error processing JSON: {ex.Message}", ex);
                throw;
            }
        }
    }
}
