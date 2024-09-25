using jaranilla_09252024.application.Implementation.Services.Interfaces;
using jaranilla_09252024.application.Interfaces.Repositories;
using jaranilla_09252024.domain.Domain;
using jaranilla_09252024.domain.Repositories;
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
        private readonly IFileProcessingLogRepositoryService _fileProcessingLogRepositoryService;
        private readonly ILoggingService _loggingService;

        public PizzaRepositoryService(IPizzaRepository pizzaRepository, IFileProcessingLogRepositoryService fileProcessingLogRepository, ILoggingService logger)
        {
            this._pizzaRepository = pizzaRepository;
            this._fileProcessingLogRepositoryService = fileProcessingLogRepository;
            _loggingService = logger;
        }

        public async Task<Pizza> AddPizzaAsync(Pizza pizza)
        {
            try
            {
                

                // Start measuring the processing time
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Add the pizza to the repository
                await _loggingService.LogInformationAsync($"Adding pizza: {pizza.Name}");
                var result = await _pizzaRepository.AddPizzaAsync(pizza);

                // Stop measuring the processing time
                stopwatch.Stop();

                // Calculate and save the processing time
                result.ProcessingTime = stopwatch.Elapsed;

                // Update the pizza with the processing time
                await _pizzaRepository.UpdatePizzaAsync(result);

                // Log the processedFile
                await _loggingService.LogInformationAsync($"Logging Processed File: {"filename here..."}");
                await _fileProcessingLogRepositoryService.AddLogAsync("FileName Here...", result.ProcessingTime);

                return result;
            }
            catch (Exception ex) 
            {
                await _loggingService.LogErrorAsync($"Error adding pizza: {pizza.Name}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Pizza>> GetPizzasByStatus(bool isActive)
        {
            try 
            {
                return await _pizzaRepository.GetPizzasByStatus(isActive);
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error Getting Pizzas", ex);
                throw;
            }
            
        }

        public async Task<List<Pizza>> GetProcessedPizzasAsync()
        {
            try
            {
                return await _pizzaRepository.GetProcessedPizzasAsync();
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error GettingProccessed Pizzas", ex);
                throw;
            }

        }

        public async Task<Pizza> UpdatePizzaAsync(Pizza pizza)
        {
           throw new NotImplementedException();
        }
    }
}
