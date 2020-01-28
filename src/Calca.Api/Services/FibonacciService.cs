using System.Globalization;
using System;
using System.Threading.Tasks;
using Calca.Api.Infrastructure;
using Calca.Api.Infrastructure.Models;
using Calca.Api.Protos;
using Grpc.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Calca.Api.Services
{
    public class FibonacciService : Fibonacci.FibonacciBase
    {
        private ICalculationDbContext _database;
        public FibonacciService(CalculationDbContextResolver resolve)
        {
            _database = resolve(typeof(FibonacciDbContext));
        }
        // considering Fibonacci sqeuence is: 0 1 1 2 3 ...
        public override Task<FibonacciReply> Calculate(FibonacciRequest request, ServerCallContext context)
        {
            if (request.N < 1)
                return Task.FromResult(new FibonacciReply { Value = (-1).ToString() });

            if (request.N < 3)
                return Task.FromResult(
                    new FibonacciReply { Value = (request.N - 1).ToString() }
                );

            ulong[] seq = new ulong[request.N];
            seq[0] = 0;
            seq[1] = 1;

            for (int i = 2; i < request.N; ++i)
                seq[i] = seq[i - 1] + seq[i - 2];

            return Task.FromResult(new FibonacciReply { Value = seq[request.N - 1].ToString() });
        }
    }
}