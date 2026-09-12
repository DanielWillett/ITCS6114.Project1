using ITCS6114.Project1.Impl;

namespace ITCS6114.Project1.Tests;

// ReSharper disable AsyncMethodWithoutAwait

public class ModifiedQuickSortTests
{
    /// <summary>
    /// Test that the median of three function is working correctly.
    /// </summary>
    [Test]
    [Arguments(new int[] { 1, 7, 48, 19, 5, 18, 14, 172, 90 }, 5)]
    [Arguments(new int[] { 90, 7, 48, 19, 5, 18, 14, 172, 1 }, 5)]
    [Arguments(new int[] { 8, 7, 48, 19, 1, 18, 14, 172, 90 }, 8)]
    [Arguments(new int[] { 8, 7, 48, 19, 90, 18, 14, 172, 1 }, 8)]
    [Arguments(new int[] { 4, 7, 48, 19, 7, 18, 14, 172, 6 }, 6)]
    [Arguments(new int[] { 7, 7, 48, 19, 4, 18, 14, 172, 6 }, 6)]
    public async Task MedianOfThreeSelection(int[] array, int expectedValue)
    {
        CommonHelpers.PrintIntArray("Original : {0}", array);

        int index = QuickSort<int>.GetMedianPivotIndex(array);

        CommonHelpers.PrintIntArray("Modified : {0}", array);

        int value = array[index];

        await Assert.That(value).IsEqualTo(expectedValue);
        await Assert.That(index).IsEqualTo(array.Length - 2);
    }

    /// <summary>
    /// Test that an empty array doesn't fail.
    /// </summary>
    [Test]
    public async Task Sort_Empty()
    {
        int[] array = Array.Empty<int>();

        CommonHelpers.PrintIntArray("Unsorted : {0}", array);

        QuickSort<int>.ModifiedSort(array);

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

        QuickSort<int>.ModifiedSort(array);

        CommonHelpers.PrintIntArray("Sorted   : {0}", array);

        await Assert.That(array[0]).IsEqualTo(value);
    }

    /// <summary>
    /// Test various different arrays with multiple elements.
    /// </summary>
    [Test]
    [Arguments<int[]>([ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 ])] // already sorted
    [Arguments<int[]>([ 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 ])]
    [Arguments<int[]>([ 8, 0, 918, 284, -193, int.MinValue, 94, -575, int.MaxValue, 298, -31, 5821, -4, 75, 927, 6, 84 ])]
    [Arguments<int[]>([ 0, 1, 2, 3, 4, 5, 6, 7, 8, 8, 8, 8, 9, 10, 11, 12, 13 ])]
    public async Task Sort_Multiple(int[] testValues)
    {
        CommonHelpers.PrintIntArray("Unsorted : {0}", testValues);

        QuickSort<int>.ModifiedSort(testValues);

        CommonHelpers.PrintIntArray("Sorted   : {0}", testValues);

        await Assert.That(testValues).IsInOrder();
    }

    [Test]
    [MethodDataSource(typeof(CommonHelpers), nameof(CommonHelpers.TestInputSizes))]
    public async Task Sort_StressTest(int amount, CancellationToken token)
    {
        const int runs = 16;

        decimal timeElapsed = CommonHelpers.RunTestBenchmark(
            runs, token,
            sw =>
            {
                int[] array = CommonHelpers.CreateRandomIntArray(amount);

                // CommonHelpers.PrintIntArray("Unsorted : {0} ...", array.AsSpan(0, 100));

                sw.Restart();
                QuickSort<int>.ModifiedSort(array);
                sw.Stop();

                // CommonHelpers.PrintIntArray("Sorted   : {0} ...", array.AsSpan(0, 100));

                if (!CommonHelpers.IsSorted(array))
                    Assert.Fail($"n = {amount} not sorted.");
            }
        );

        Console.WriteLine($"n = {runs}, t(avg) = {timeElapsed * 1000m} ms");
    }
}