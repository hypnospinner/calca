using Calca.Api.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Calca.Api.Infrastructure
{
    public class FibonacciDbContext : DbContext, ICalculationDbContext
    {
        public FibonacciDbContext(DbContextOptions<FibonacciDbContext> options) : base(options) { }
        public DbSet<CalculationResult> Results { get; set; }
    }
}