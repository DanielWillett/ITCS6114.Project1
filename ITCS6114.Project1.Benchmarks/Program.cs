using BenchmarkDotNet.Running;

namespace ITCS6114.Project1.Benchmarks;

internal static class Program
{
    private static void Main()
    {
        BenchmarkRunner.Run<SortingAlgorithms>();
    }
}