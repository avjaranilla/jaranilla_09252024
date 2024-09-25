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

        public async Task<bool> ProcessJsonAsync(Stream jsonStream, string fileName)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var pizzas = await JsonSerializer.DeserializeAsync<List<Pizza>>(jsonStream, options);

                if (pizzas != null && pizzas.Count > 0)
                {
                    await _pizzaRepositoryService.AddPizzasAsync(pizzas, fileName); // Call the bulk add method with the file name
                }
                return true;
            }
            catch (Exception ex) 
            {
                _loggerService.LogErrorAsync($"Something went wrong while processing JSON File: {fileName}. {ex.Message}", ex);
                throw;
            }          
        }
    }
}
