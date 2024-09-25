using jaranilla_09252024.domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Interfaces.Repositories
{
    public interface IPizzaRepository
    {
        Task<Pizza> AddPizzaAsync(Pizza pizza);
        Task<List<Pizza>> AddPizzasAsync(List<Pizza> pizzas);
        Task<List<Pizza>> GetProcessedPizzasAsync();
        Task<IEnumerable<Pizza>> GetPizzasByStatus(bool isActive);

        Task<Pizza> UpdatePizzaAsync(Pizza pizza);
        Task<List<Pizza>> UpdatePizzasAsync(List<Pizza> pizza);
    }
}
