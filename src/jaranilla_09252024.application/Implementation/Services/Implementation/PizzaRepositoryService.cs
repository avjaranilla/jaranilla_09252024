using jaranilla_09252024.application.Implementation.Services.Interfaces;
using jaranilla_09252024.application.Interfaces.Repositories;
using jaranilla_09252024.domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Implementation.Services.Implementation
{
    public class PizzaRepositoryService : IPizzaRepositoryService
    {

        private readonly IPizzaRepository _pizzaRepository;

        public PizzaRepositoryService(IPizzaRepository pizzaRepository)
        {
            this._pizzaRepository = pizzaRepository;
        }

        public async Task<Pizza> AddPizzaAsync(Pizza pizza)
        {
            // Start measuring the processing time
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Add the pizza to the repository
            var result = await _pizzaRepository.AddPizzaAsync(pizza);

            // Stop measuring the processing time
            stopwatch.Stop();

            // Calculate and save the processing time
            result.ProcessingTime = stopwatch.Elapsed;

            // Update the pizza with the processing time
            await _pizzaRepository.UpdatePizzaAsync(result);

            return result;
        }

        public async Task<IEnumerable<Pizza>> GetPizzasByStatus(bool isActive)
        {
            return await _pizzaRepository.GetPizzasByStatus(isActive);
        }

        public async Task<List<Pizza>> GetProcessedPizzasAsync()
        {
            return await _pizzaRepository.GetProcessedPizzasAsync();
        }

        public async Task<Pizza> UpdatePizzaAsync(Pizza pizza)
        {
           throw new NotImplementedException();
        }
    }
}
