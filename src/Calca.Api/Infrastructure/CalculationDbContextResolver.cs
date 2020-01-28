using System;
namespace Calca.Api.Infrastructure
{
    public delegate ICalculationDbContext CalculationDbContextResolver(Type type);
}