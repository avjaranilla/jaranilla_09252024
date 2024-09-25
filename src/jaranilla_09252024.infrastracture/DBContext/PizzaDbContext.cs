using jaranilla_09252024.domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace jaranilla_09252024.infrastracture.DBContext
{
    public class PizzaDbContext : DbContext
    {

        public DbSet<Pizza> Pizzas { get; set; }
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base(options) { }

    }
}
