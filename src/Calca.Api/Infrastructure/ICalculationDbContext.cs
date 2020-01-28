using Calca.Api.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Calca.Api.Infrastructure
{
    public interface ICalculationDbContext
    {
        DbSet<CalculationResult> Results { get; set; }
    }
}