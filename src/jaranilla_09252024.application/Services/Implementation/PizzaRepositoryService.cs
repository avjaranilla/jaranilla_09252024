﻿using jaranilla_09252024.application.Interfaces.Repositories;
using jaranilla_09252024.application.Models;
using jaranilla_09252024.application.Services.Interfaces;
using jaranilla_09252024.domain.Domain;
using System.Diagnostics;


namespace jaranilla_09252024.application.Services.Implementation
{
    public class PizzaRepositoryService : IPizzaRepositoryService
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IFileProcessingLogRepositoryService _fileProcessingLogRepositoryService;
        private readonly ILoggingService _loggingService;

        public PizzaRepositoryService(IPizzaRepository pizzaRepository, IFileProcessingLogRepositoryService fileProcessingLogRepository, ILoggingService logger)
        {
            _pizzaRepository = pizzaRepository;
            _fileProcessingLogRepositoryService = fileProcessingLogRepository;
            _loggingService = logger;
        }

        public async Task<AddPizzasReturnModel> AddPizzasAsync(List<Pizza> pizzas, string fileName)
        {
            var processedPizzas = new List<Pizza>();
            var stopwatch = new Stopwatch();

            foreach (var pizza in pizzas)
            {
                try
                {
                    // Start measuring the processing time for each pizza
                    stopwatch.Restart();

                    // Log the attempt to add pizza
                    await _loggingService.LogInformationAsync($"Adding pizza: {pizza.Name}");

                    // Add pizza to the processed list (no immediate save to DB)
                    processedPizzas.Add(pizza);

                    // Stop the stopwatch and set processing time
                    stopwatch.Stop();
                    pizza.ProcessingTime = stopwatch.Elapsed;

                }
                catch (Exception ex)
                {
                    // Log the error
                    await _loggingService.LogErrorAsync($"Error adding pizza: {pizza.Name}", ex);
                    throw;
                }
            }

            // Bulk add pizzas to the repository
            await _pizzaRepository.AddPizzasAsync(processedPizzas);

            // Bulk update pizzas with processing time
            await _pizzaRepository.UpdatePizzasAsync(processedPizzas);

            // Sum the total processing time across all pizzas
            var totalProcessingTime = TimeSpan.FromMilliseconds(processedPizzas.Sum(p => p.ProcessingTime.TotalMilliseconds));

            // Log the processed file and total processing time for the bulk operation
            await _loggingService.LogInformationAsync($"Logging processed file: {fileName}");
            
            var processLogDetails = await _fileProcessingLogRepositoryService.AddLogAsync(fileName, totalProcessingTime, processedPizzas.Count());

            // build the return model
            var returnModel = new AddPizzasReturnModel
            {
                FileProcessingLog = processLogDetails,
                Pizzas = processedPizzas
            };


            return returnModel;
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
    }
}
