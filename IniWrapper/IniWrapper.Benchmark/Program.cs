using BenchmarkDotNet.Running;
using IniWrapper.Benchmark.Read;
using IniWrapper.Benchmark.Write;
using System;

namespace IniWrapper.Benchmark
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<WriteBenchmark>();
            BenchmarkRunner.Run<ReadBenchmark>();
            BenchmarkRunner.Run<ReadToOneBufferBenchmark>();
            BenchmarkRunner.Run<ReadFromSectionBenchmark>();

            Console.ReadKey();
        }
    }
}
