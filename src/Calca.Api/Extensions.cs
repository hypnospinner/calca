using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using Calca.Api.Infrastructure;
using Calca.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public static class Extensions
{
    public static void MapCalcaGrpcServices(this IEndpointRouteBuilder endpoints)
    {
        typeof(FibonacciService).Assembly
            .GetTypes()
            .Where(type =>
                type.Name.EndsWith("Service") &&
                type.IsClass &&
                !type.IsAbstract)
            .ToList()
            .ForEach(type =>
                        typeof(GrpcEndpointRouteBuilderExtensions)
                            .GetMethod("MapGrpcService", new[] { typeof(IEndpointRouteBuilder) })
                            .MakeGenericMethod(new Type[] { type })
                            .Invoke(null, new[] { endpoints }));
    }

    public static void AddInMemoryDbContexts(this IServiceCollection services)
    {

        // inject db contexts

        typeof(FibonacciDbContext).Assembly
            .GetTypes()
            .Where(type =>
                type.Name.EndsWith("DbContext") &&
                type.IsSubclassOf(typeof(DbContext)) &&
                type.IsClass &&
                !type.IsAbstract)
            .ToList()
            .ForEach(dbContext =>
                typeof(EntityFrameworkServiceCollectionExtensions)
                    .GetMethod(
                        name: "AddDbContext",
                        genericParameterCount: 1,
                        types: new[]
                        {
                            typeof(IServiceCollection),
                            typeof(Action<DbContextOptionsBuilder>),
                            typeof(ServiceLifetime),
                            typeof(ServiceLifetime)
                        })
                    .MakeGenericMethod(new[] { dbContext })
                    .Invoke(
                        null,
                        new object[]
                        {
                                services,
                                new Action<DbContextOptionsBuilder>(options => options.UseInMemoryDatabase(dbContext.Name)),
                                ServiceLifetime.Scoped,
                                ServiceLifetime.Scoped
                        }));

        // add transcient resolver for db context that will automatically 
        // find db context for corresponding service

        var method = typeof(ServiceCollectionServiceExtensions)
            .GetMethod(
                "AddScoped",
                0,
                new Type[] {
                    typeof(IServiceCollection),
                    typeof(Type),
                    typeof(Func<IServiceProvider, object>)
                });

        method.Invoke(null, new object[] {
                services,
                typeof(CalculationDbContextResolver),
                new Func<IServiceProvider, object>(serviceProvider =>
                {
                    return new CalculationDbContextResolver(dbContextType =>
                        {
                            if(typeof(FibonacciDbContext).Assembly
                                .GetTypes()
                                .Any(type => type == dbContextType))
                            {
                                return typeof(ServiceProviderServiceExtensions)
                                    .GetMethod("GetService", 1, new[] {typeof(IServiceProvider)})
                                    .MakeGenericMethod(new[] {dbContextType})
                                    .Invoke(null, new[] {serviceProvider})
                                    as ICalculationDbContext;
                            }
                            else throw new KeyNotFoundException();
                        });
                })
            });
    }
}