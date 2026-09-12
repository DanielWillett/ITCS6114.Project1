using BenchmarkDotNet.Attributes;
using ITCS6114.Project1.Impl;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace ITCS6114.Project1.Benchmarks;

[MinColumn, MaxColumn]
[CsvExporter]
public class SortingAlgorithms
{
    [Params(32, 64, 256, 1_000, 2_000, 4_000, 10_000, 20_000, 40_000, 80_000, 100_000, 200_000)]
    public int Size { get; set; }

    [Params(SpecialCase.None, SpecialCase.Sorted, SpecialCase.InverseSorted)]
    public SpecialCase SpecialCase { get; set; }

#nullable disable

    private int[] _data;

#nullable restore

    [IterationSetup]
    public void Setup()
    {
        _data = new int[Size];
        switch (SpecialCase)
        {
            case SpecialCase.None:
                RandomNumberGenerator.Fill(MemoryMarshal.Cast<int, byte>(_data.AsSpan()));
                break;

            case SpecialCase.Sorted:
                for (int i = 0; i < Size; ++i)
                    _data[i] = i;
                break;

            case SpecialCase.InverseSorted:
                for (int i = 0; i < Size; ++i)
                    _data[i] = Size - i - 1;
                break;
        }
    }

    [Benchmark]
    public void InsertionSort()
    {
        RunSortingAlgorithm(arr => InsertionSort<int>.Sort(arr));
    }

    [Benchmark]
    public void HeapSort()
    {
        RunSortingAlgorithm(arr => HeapSort<int>.Sort(arr));
    }

    [Benchmark]
    public void MergeSort()
    {
        RunSortingAlgorithm(arr => MergeSort<int>.Sort(arr));
    }

    [Benchmark]
    public void QuickSort_Standard()
    {
        RunSortingAlgorithm(arr => QuickSort<int>.StandardSort(arr));
    }

    [Benchmark]
    public void QuickSort_Modified()
    {
        RunSortingAlgorithm(arr => QuickSort<int>.ModifiedSort(arr));
    }

    private void RunSortingAlgorithm(Action<int[]> algorithm)
    {
        algorithm(_data);
    }
}

public enum SpecialCase
{
    None,
    Sorted,
    InverseSorted
}