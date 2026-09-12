namespace ITCS6114.Project1.Impl;

public static class HeapSort<T> where T : IComparable<T>
{
    /// <summary>
    /// Performs a vector-based heapsort for values implementing <see cref="IComparable{T}"/>.
    /// </summary>
    public static void Sort(Span<T> array)
    {
        // Heap implemented in Heap.cs
        Heap<T> heap = new Heap<T>(array.Length);

        foreach (T element in array)
        {
            heap.Add(element);
        }

        for (int i = 0; i < array.Length; ++i)
        {
            array[i] = heap.Pop();
        }
    }
}