using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace ITCS6114.Project1.Tests;

internal class CommonHelpers
{
    public static IEnumerable<Func<int>> TestInputSizes()
    {
        yield return () =>   1_000;
        yield return () =>   2_000;
        yield return () =>   3_000;
        yield return () =>   4_000;
        yield return () =>   5_000;
        yield return () =>  10_000;
        yield return () =>  20_000;
        yield return () =>  40_000;
        yield return () =>  50_000;
        yield return () =>  60_000;
        yield return () =>  80_000;
        yield return () =>  90_000;
        yield return () => 100_000;
        yield return () => 10_000_000;
    }

    public static int[] CreateRandomIntArray(int count)
    {
        int[] array = new int[count];

        RandomNumberGenerator.Fill(MemoryMarshal.Cast<int, byte>(array.AsSpan()));

        return array;
    }

    public static decimal RunTestBenchmark(
        int runs,
        CancellationToken token,
        Action<Stopwatch> runTest
    )
    {
        Stopwatch sw = new Stopwatch();

        long[] ticks = new long[runs];

        for (int i = 0; i < runs; ++i)
        {
            token.ThrowIfCancellationRequested();
            runTest(sw);
            ticks[i] = sw.ElapsedTicks;
        }

        // remove outliers
        Array.Sort(ticks);
        long q1 = ticks[runs / 8 * 3];
        long q3 = ticks[runs / 8 * 5];
        long iqr = q3 - q1;
        long lowerFence = q1 - iqr;
        long upperFence = q3 + iqr;

        long total = 0;
        int ct = 0;
        foreach (long l in ticks)
        {
            if (l < lowerFence || l > upperFence)
                continue;

            ++ct;
            total += l;
        }

        return (decimal)total / ct / Stopwatch.Frequency;
    }

    public static void PrintIntArray(string message, Span<int> array)
    {
        string fmt = !array.IsEmpty ? "[ " + string.Join(", ", array.ToArray().Select(x => x.ToString())) + " ]" : "[ ]";
        Console.WriteLine(message, fmt);
    }

    public static bool IsSorted<T>(Span<T> array) where T : IComparable<T>
    {
        if (array.Length <= 1)
            return true;

        T last = array[0];
        for (int i = 1; i < array.Length; ++i)
        {
            if (last.CompareTo(array[i]) > 0)
                return false;
        }

        return true;
    }
}