using jaranilla_09252024.application.Interfaces.Repositories;
using jaranilla_09252024.domain.Domain;
using jaranilla_09252024.infrastracture.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Implementation.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly PizzaDbContext _context;

        public PizzaRepository(PizzaDbContext context)
        {
            _context = context;
        }

        public async Task<Pizza> AddPizzaAsync(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();
            return pizza;

        }

        public async Task<IEnumerable<Pizza>> GetPizzasByStatus(bool isActive)
        {
            return await _context.Pizzas.Where(p => p.IsActive == isActive).ToListAsync();
        }

        public async Task<List<Pizza>> GetProcessedPizzasAsync()
        {
            return await _context.Pizzas.ToListAsync();
        }

        public async Task<Pizza> UpdatePizzaAsync(Pizza pizza)
        {
            _context.Pizzas.Update(pizza);
            await _context.SaveChangesAsync();
            return pizza;
        }
    }
}
