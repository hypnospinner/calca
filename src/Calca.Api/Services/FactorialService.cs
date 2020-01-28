using System.Threading.Tasks;
using Calca.Api.Infrastructure;
using Calca.Api.Protos;
using Grpc.Core;
using static Calca.Api.Protos.Factorial;

namespace Calca.Api.Services
{
    public class FactorialService : FactorialBase
    {
        private ICalculationDbContext _database;
        public FactorialService(CalculationDbContextResolver resolve)
        {
            _database = resolve(typeof(FactorialDbContext));
        }

        public override Task<FactorialReply> Calculate(FactorialRequest request, ServerCallContext context)
        {
            if (request.N < 1)
                return Task.FromResult(new FactorialReply { Value = (-1).ToString() });

            ulong result = 1;

            for (uint i = 1; i <= request.N; i++)
                result *= i;    

            return Task.FromResult(new FactorialReply { Value = result.ToString() });
        }
    }
}