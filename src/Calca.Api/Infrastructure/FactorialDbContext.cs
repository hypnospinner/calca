using Calca.Api.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Calca.Api.Infrastructure
{
    public class FactorialDbContext : DbContext
    {
        public FactorialDbContext(DbContextOptions<FactorialDbContext> options) : base(options) { }
        public DbSet<CalculationResult> Results { get; set; }
    }
}