using jaranilla_09252024.domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace jaranilla_09252024.infrastracture.DBContext
{
    public class AppDbContext : DbContext
    {

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<FileProcessingLog> FileProcessingLogs {get; set;}

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

    }
}
