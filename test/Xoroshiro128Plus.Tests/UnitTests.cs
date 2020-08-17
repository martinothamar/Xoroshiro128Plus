using MathNet.Numerics.Distributions;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;

[module: SkipLocalsInit]

namespace Xoroshiro.Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Xoroshiro128Plus_Distribution_Similar_To_Std_Random(bool useRandomSeed)
        {
            var rnd = new Random();
            var xoroshiro128plus = new Xoroshiro128Plus(useRandomSeed ? new Random() : null);

            var iterations = 10_000_000;

            var stdRand = new double[iterations];
            var testRand = new double[iterations];

            for (int i = 0; i < iterations; i++)
            {
                stdRand[i] = rnd.NextDouble();
                testRand[i] = xoroshiro128plus.NextDouble();
            }

            var stdAvg = stdRand.Average();
            var testAvg = testRand.Average();

            var avgDiff = Math.Abs(stdAvg - testAvg);
            Assert.True(avgDiff < 0.001d, "RNG should have simular distributions as BCL version. Diff: " + avgDiff);

            Test_Uniform_Distribution(stdRand);
            Test_Uniform_Distribution(testRand);

            static void Test_Uniform_Distribution(double[] numbers)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    Assert.Equal(1.0d, ContinuousUniform.PDF(0.0d, 1.0d, numbers[i]));
                }
            }

        }
    }
}
