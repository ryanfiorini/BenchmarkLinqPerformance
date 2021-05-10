using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkLinqPerformance
{
    [MemoryDiagnoser]
    public class ArraryBenchmarks
    {
        private int[] _myArray;

        [Params(10, 1000, 10000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _myArray = new int[Size];
            for (int i = 0; i < Size; i++)
            {
                _myArray[i] = i;
            }
        }

        [Benchmark(Baseline = true)]
        public void Original() =>
            _myArray.Skip(Size / 2).Take(Size / 4).ToList();

        [Benchmark]
        public int[] ArrayCopy()
        {
            var newArray = new int[Size / 4];
            Array.Copy(_myArray, Size / 2, newArray, 0, Size / 4);
            return newArray;
        }

        [Benchmark]
        public int[] NewArray()
        {
            var newArray = new int[Size / 4];
            for (int i = 0; i < Size/4; i++)
            {
                newArray[i] = _myArray[(Size / 2) + 1];
            }

            return newArray;
        }

        [Benchmark]
        public Span<int> Span() => _myArray.AsSpan().Slice(Size / 2, Size / 4);
    }
}
