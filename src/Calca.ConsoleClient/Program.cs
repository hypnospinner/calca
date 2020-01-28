using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calca.Api.Protos;
using Grpc.Core;
using Grpc.Net.Client;

namespace Calca.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var factorialClient = new Factorial.FactorialClient(channel);
            var fibonacciClient = new Fibonacci.FibonacciClient(channel);

            Console.WriteLine(
                "Welcome to the simpe calculation application powered by gRPC & ASP.NET Core 3.1");
            Console.WriteLine(
                "E - exit \n 1 - Calculate Factorial \n 2 - Calculate Fibonnaci number (each new input should be on the new line)"
            );

            int result;

            while (int.TryParse(Console.ReadLine(), out result))
            {
                if (result == 1)
                {
                    int.TryParse(Console.ReadLine(), out result);
                    var t = await factorialClient.CalculateAsync(new FactorialRequest { N = result });

                    Console.WriteLine($"Factorial is {t.Value}");
                }
                else if (result == 2)
                {
                    int.TryParse(Console.ReadLine(), out result);
                    var t = await fibonacciClient.CalculateAsync(new FibonacciRequest { N = result });

                    Console.WriteLine($"Fibonacci is {t.Value}");
                }
                else Console.WriteLine("Invalid Result Input");
            }

            Console.WriteLine("Goodbye");
            Console.ReadLine();
        }
    }
}
