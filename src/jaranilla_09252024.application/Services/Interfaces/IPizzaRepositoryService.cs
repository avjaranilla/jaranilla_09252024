using jaranilla_09252024.application.Models;
using jaranilla_09252024.domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Services.Interfaces
{
    public interface IPizzaRepositoryService
    {
        Task<AddPizzasReturnModel> AddPizzasAsync(List<Pizza> pizzas, string fileName);
        Task<List<Pizza>> GetProcessedPizzasAsync();
        Task<IEnumerable<Pizza>> GetPizzasByStatus(bool isActive);
    }
}
