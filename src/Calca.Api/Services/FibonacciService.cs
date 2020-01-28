using System.Threading.Tasks;
using Calca.Api.Infrastructure;
using Calca.Api.Protos;
using Grpc.Core;

namespace Calca.Api.Services
{
    public class FibonacciService : Fibonacci.FibonacciBase
    {
        private ICalculationDbContext _database;

        public FibonacciService(CalculationDbContextResolver resolve)
        {
            _database = resolve(typeof(FibonacciDbContext));
        }

        public override Task<FibonacciReply> Calculate(FibonacciRequest request, ServerCallContext context)
        {
            return base.Calculate(request, context);
        }
    }
}