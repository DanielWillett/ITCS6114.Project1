using ITCS6114.Project1.Impl;
using TUnit.Assertions.Exceptions;

namespace ITCS6114.Project1.Tests;

// ReSharper disable AsyncMethodWithoutAwait

public class InsertionSortTests
{
    /// <summary>
    /// Test that an empty array doesn't fail.
    /// </summary>
    [Test]
    public async Task Sort_Empty()
    {
        int[] array = Array.Empty<int>();

        CommonHelpers.PrintIntArray("Unsorted : {0}", array);

        InsertionSort<int>.Sort(array);

        CommonHelpers.PrintIntArray("Sorted   : {0}", array);
    }

    /// <summary>
    /// Test that an array with one element doesn't fail.
    /// </summary>
    [Test]
    public async Task Sort_Single()
    {
        const int value = 4;
        int[] array = [ value ];

        CommonHelpers.PrintIntArray("Unsorted : {0}", array);

        InsertionSort<int>.Sort(array);

        CommonHelpers.PrintIntArray("Sorted   : {0}", array);

        await Assert.That(array[0]).IsEqualTo(value);
    }

    /// <summary>
    /// Test various different arrays with multiple elements.
    /// </summary>
    [Test]
    [Arguments<int[]>([ 1, 2, 3, 4 ])] // already sorted
    [Arguments<int[]>([ 9, 8 ])]
    [Arguments<int[]>([ 4, 3, 2, 1 ])]
    [Arguments<int[]>([ 8, 0, 918, 284, -193, int.MinValue, 94, -575, int.MaxValue ])]
    public async Task Sort_Multiple(int[] testValues)
    {
        CommonHelpers.PrintIntArray("Unsorted : {0}", testValues);

        InsertionSort<int>.Sort(testValues);

        CommonHelpers.PrintIntArray("Sorted   : {0}", testValues);

        await Assert.That(testValues).IsInOrder();
    }

    [Test]
    [MethodDataSource(typeof(CommonHelpers), nameof(CommonHelpers.TestInputSizes))]
    public async Task Sort_StressTest(int amount, CancellationToken token)
    {
        if (amount > 100_000)
            return;

        const int runs = 16;

        decimal timeElapsed = CommonHelpers.RunTestBenchmark(
            runs, token,
            sw =>
            {
                int[] array = CommonHelpers.CreateRandomIntArray(amount);

                // CommonHelpers.PrintIntArray("Unsorted : {0} ...", array.AsSpan(0, 100));

                sw.Restart();
                InsertionSort<int>.Sort(array);
                sw.Stop();

                // CommonHelpers.PrintIntArray("Sorted   : {0} ...", array.AsSpan(0, 100));

                if (!CommonHelpers.IsSorted(array))
                    Assert.Fail($"n = {amount} not sorted.");
            }
        );

        Console.WriteLine($"n = {runs}, t(avg) = {timeElapsed * 1000m} ms");
    }
}