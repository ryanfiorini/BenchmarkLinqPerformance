using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkLinqPerformance
{
    [MemoryDiagnoser]
    public class StackAllocBenchmark
    {
        [Benchmark]
        public void CalculateFibonacci()
        {
            const int arraySize = 20;
            Span<int> fib = stackalloc int[arraySize];

            fib[0] = fib[1] = 1;

            for (int i = 2; i < arraySize; i++)
            {
                fib[i] = fib[i - 1] + fib[1 - 2];
            }
        }
    }
}
