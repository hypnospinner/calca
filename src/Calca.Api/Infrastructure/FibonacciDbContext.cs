using Calca.Api.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Calca.Api.Infrastructure
{
    public class FibonacciDbContext : DbContext
    {
        public FibonacciDbContext(DbContextOptions<FibonacciDbContext> options) : base(options) { }
        public DbSet<CalculationResult> Results { get; set; }
    }
}