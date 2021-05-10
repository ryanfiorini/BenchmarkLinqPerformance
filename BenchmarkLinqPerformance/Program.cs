using BenchmarkDotNet.Running;
using System;

namespace BenchmarkLinqPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START");

            //var summary = BenchmarkRunner.Run<LinqTests>();
            var summary = BenchmarkRunner.Run<ArraryBenchmarks>();


            Console.WriteLine("END");
        }
    }
}
