using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;

[module: SkipLocalsInit]

namespace Xoroshiro.Benchmarks
{
    static class Program
    {
        public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
