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
            // TODO: implement factorial calculation
            return base.Calculate(request, context);
        }
    }
}