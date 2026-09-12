// ReSharper disable TailRecursiveCall

namespace ITCS6114.Project1.Impl;

public static class MergeSort<T> where T : IComparable<T>
{
    /// <summary>
    /// Implementation of merge sort for a span of elements implementing <see cref="IComparable{T}"/>.
    /// </summary>
    public static T[] Sort(Span<T> array)
    {
        if (array.Length <= 1)
        {
            return array.ToArray();
        }

        T[] sortBuffer = new T[array.Length];
        T[] output = new T[array.Length];
        SortTo(array, sortBuffer, output);
        return output;
    }

    private static void SortTo(Span<T> input, Span<T> sortBuffer, Span<T> output)
    {
        switch (input.Length)
        {
            case 0: return;
            case 1:
                sortBuffer[0] = input[0];
                return;
        }

        int partition = input.Length / 2;

        SortTo(input[..partition], sortBuffer[..partition], output[..partition]);
        SortTo(input[partition..], sortBuffer[partition..], output[partition..]);

        MergeSorted(sortBuffer[..partition], sortBuffer[partition..], output);
        output.CopyTo(sortBuffer);
    }

    private static void MergeSorted(Span<T> left, Span<T> right, Span<T> output)
    {
        int leftIndex = 0, rightIndex = 0;
        int writeIndex = 0;
        while (leftIndex < left.Length && rightIndex < right.Length)
        {
            T l = left[leftIndex];
            T r = right[rightIndex];

            int cmp = l.CompareTo(r);
            if (cmp < 0)
            {
                output[writeIndex] = l;
                ++leftIndex;
            }
            else
            {
                output[writeIndex] = r;
                ++rightIndex;
            }
            
            ++writeIndex;
        }

        if (leftIndex < left.Length)
        {
            left[leftIndex..].CopyTo(output[writeIndex..]);
        }
        else if (rightIndex < right.Length)
        {
            right[rightIndex..].CopyTo(output[writeIndex..]);
        }
    }
}