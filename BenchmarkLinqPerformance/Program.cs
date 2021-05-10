using BenchmarkDotNet.Running;
using System;

namespace BenchmarkLinqPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START");

            var summary = BenchmarkRunner.Run<LinqTests>();


            Console.WriteLine("END");
        }
    }
}
