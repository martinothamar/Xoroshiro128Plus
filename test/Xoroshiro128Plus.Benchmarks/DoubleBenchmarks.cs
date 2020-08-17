using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using System;

namespace Xoroshiro.Benchmarks
{
    [MemoryDiagnoser/*, HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.CacheMisses, HardwareCounter.TotalCycles)*/, DisassemblyDiagnoser]
    public class DoubleBenchmarks
    {
        [Params(1, 100)]
        public int Iterations;

        private Random _stdRng;
        private Xoroshiro128Plus _rng;

        [GlobalSetup]
        public void Setup()
        {
            _stdRng = new Random();
            _rng = new Xoroshiro128Plus(new Random());
        }

        [Benchmark(Baseline = true)]
        public void StdRandom()
        {
            for (int i = 0; i < Iterations; i++)
                _ = _stdRng.NextDouble();
        }

        [Benchmark]
        public void Random()
        {
            for (int i = 0; i < Iterations; i++)
                _ = _rng.NextDouble();
        }
    }
}
