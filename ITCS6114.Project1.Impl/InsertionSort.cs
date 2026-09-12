namespace ITCS6114.Project1.Impl;

public static class InsertionSort<T> where T : IComparable<T>
{
    /// <summary>
    /// In-place insertion sort implementation for values implementing <see cref="IComparable{T}"/>.
    /// </summary>
    public static void Sort(Span<T> array)
    {
        for (int j = 1; j < array.Length; ++j)
        {
            T key = array[j];
            int i = j - 1;
            for (; i >= 0; --i)
            {
                if (key.CompareTo(array[i]) >= 0) break;
            }

            // shift by 1
            array.Slice(i + 1, j - i - 1).CopyTo(array.Slice(i + 2));
            array[i + 1] = key;
        }
    }
}