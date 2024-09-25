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
        Task AddPizzaAsync(Pizza pizza);
        Task<List<Pizza>> GetProcessedPizzasAsync();
    }
}
