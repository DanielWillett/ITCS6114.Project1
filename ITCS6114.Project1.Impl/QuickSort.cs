// ReSharper disable TailRecursiveCall

namespace ITCS6114.Project1.Impl;

public static class QuickSort<T> where T : IComparable<T>
{
    /// <summary>
    /// Performs an in-place quicksort for values implementing <see cref="IComparable{T}"/>, using the middle element as the pivot.
    /// </summary>
    public static void StandardSort(Span<T> array)
    {
        if (array.Length <= 1)
            return;

        int pivotIndex = array.Length / 2;
        Partition(array, array[pivotIndex], out int lastLower, out int firstGreater, left: 0, right: 0);

        Span<T> lower = array.Slice(0, lastLower + 1);
        Span<T> greater = array.Slice(firstGreater);

        StandardSort(lower);
        StandardSort(greater);
    }

    /// <summary>
    /// Performs an in-place modified quicksort using a median of 3 pivot for values implementing <see cref="IComparable{T}"/>.
    /// </summary>
    public static void ModifiedSort(Span<T> array)
    {
        if (array.Length <= 16)
        {
            InsertionSort<T>.Sort(array);
            return;
        }

        int pivotIndex = GetMedianPivotIndex(array);

        Partition(array, array[pivotIndex], out int lastLower, out int firstGreater, left: 1, right: 2);
        
        // swap pivot with last element
        (array[firstGreater], array[^2]) = (array[^2], array[firstGreater]);

        Span<T> lower = array.Slice(0, lastLower + 1);
        Span<T> greater = array.Slice(firstGreater);

        ModifiedSort(lower);
        ModifiedSort(greater);
    }

    private static void Partition(Span<T> array, T pivot, out int lastLower, out int firstGreater, int left, int right)
    {
        int j = left - 1, k = array.Length - right;

        while (true)
        {
            while (array[++j].CompareTo(pivot) < 0) ;
            while (pivot.CompareTo(array[--k]) < 0) ;
            if (j >= k)
                break;

            (array[j], array[k]) = (array[k], array[j]);
        }

        lastLower = j - 1;
        firstGreater = j;
    }

    internal static int GetMedianPivotIndex(Span<T> array)
    {
        // array.Length > 16

        // [ A, ..., B, ..., C ]
        //           ^ midIndex
        int midIndex = array.Length / 2;
        
        // C < A
        if (array[^1].CompareTo(array[0]) < 0)
        {
            (array[0], array[^1]) = (array[^1], array[0]);
        }
        // B < A
        if (array[midIndex].CompareTo(array[0]) < 0)
        {
            (array[0], array[midIndex]) = (array[midIndex], array[0]);
        }
        // C < B
        if (array[^1].CompareTo(array[midIndex]) < 0)
        {
            (array[^1], array[midIndex]) = (array[midIndex], array[^1]);
        }

        (array[^2], array[midIndex]) = (array[midIndex], array[^2]);
        return array.Length - 2;
    }
}