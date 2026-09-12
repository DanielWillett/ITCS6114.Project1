# Project 1

Written by Daniel Willett on 09/11/2026 for UNCC class ITCS-6114-U02.

No AI was used on this project.

## Assignment
(copied from Canvas)

Implement the following sorting algorithms.

* Insertion sort
* Merge sort
* Heapsort [vector based, and insert one item at a time]
* In-place quicksort (any random item or the first or the last item of your input can be pivot).
* Modified quicksort
  * Use median-of-three as pivot.
  * For small sub-problem of size <= 16, you must use insertion sort.

Execution instructions:

* Run these algorithms for different input sizes (i.e. = 1000, 2000, 3K, 4K, 5K, 10K, 20K, 40K, 50K, 60K, 80K, 90K, 100K). You will randomly generate input for execution. Record the execution time (need to take the average of several runs) and plot them all in a single graph against various input sizes. Note that, you will compare all sorting algorithms for the same data set. 
* Also observe and present performance of the following two special cases:
  * Input array is already sorted.
  * Input array is reversely sorted.

## Running Instructions
Install the [.NET SDK](https://dotnet.microsoft.com/en-us/download) if you don't already have it. Install .NET 10 SDK or higher

### Run Unit Tests
Unit tests run all sorting algorithms and also test the Heap class.
```bash
dotnet test -c Release
```

Unit tests use the [TUnit](https://tunit.dev/) framework.

### Run Benchmarks
Benchmarks measure the performance of each algorithm and export the data to `./BenchmarkDotNet.Artifacts/results`.
```bash
dotnet run -c Release --project ITCS6114.Project1.Benchmarks
```

Benchmarking is done with [BenchmarkDotNet](https://benchmarkdotnet.org/).

## Relevant Code

### Projects
* [ITCS6114.Project1.Impl](https://github.com/DanielWillett/ITCS6114.Project1/tree/main/ITCS6114.Project1.Impl) contains all the actual implementations for the algorithms.
* [ITCS6114.Project1.Tests](https://github.com/DanielWillett/ITCS6114.Project1/tree/main/ITCS6114.Project1.Tests) contains all my unit tests.
* [ITCS6114.Project1.Benchmarks](https://github.com/DanielWillett/ITCS6114.Project1/tree/main/ITCS6114.Project1.Benchmarks) contains code for running the benchmarks.

### Algorithms
* [Insertion Sort](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/InsertionSort.cs)
* [Merge Sort](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/MergeSort.cs)
* [Heap Sort](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/HeapSort.cs)
  * [Heap data structure](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/Heap.cs)
* [Quick Sorts](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/QuickSort.cs)

I used unit tests for 'running the project', so use the `dotnet test` command above to run those, or run them from Visual Studio.

### Data Structures Used
* [Heap](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/Heap.cs) - made by me, backed by an array.
* Arrays
  * Most of my code uses `Span<>`s instead of arrays. Spans are just sections of memory and are basically equivalent to an array but can be sliced into smaller spans without copying the data.
* IComparable<> - similar to Java's Comparable interface. Used to make sorting methods generic.

## Complexity Analysis

### [Insertion Sort](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/InsertionSort.cs)
Worst-case (reverse sorted) is `O(n^2)`.
To improve this slightly I use a bulk copy instead of replacing each element one at a time, but still `O(n * n)`.

### [Merge Sort](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/MergeSort.cs)

Running time is `O(n*log(n))`. There are log2(n) recursions, each with an O(n) time.

### [Heap Sort](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/HeapSort.cs)

Running time is `O(n*log(n))`. `Heap.Add` and `Heap.Pop` are `O(log(n))`, which are ran `2n` times, meaning `2n * log(n)` or just `O(n*log(n))`.

### [Quick Sort](https://github.com/DanielWillett/ITCS6114.Project1/blob/main/ITCS6114.Project1.Impl/QuickSort.cs)

Typical running time is `O(n*log(n))`, but worst case could technically be `O(n^2)` in specific scenerios. The modified quick-sort algorithm makes this much less likely.

## Results

### Insertion Sort

The insertion sort clearly follows the expected `O(n^2)` run time, and performs significantly better on a sorted array.

![Insertion Sort Benchmark Graph](https://raw.githubusercontent.com/DanielWillett/ITCS6114.Project1/refs/heads/main/Assets/Benchmark_InsertionSort.png)

#### Benchmark Results

| Size   | Data Type     | Mean               | Error            | StdDev           | Median             | Min                | Max                |
|------- |-------------- |-------------------:|-----------------:|-----------------:|-------------------:|-------------------:|-------------------:|
| 32     | Randomized    |         3,672.4 ns |        146.59 ns |        427.60 ns |         3,600.0 ns |         2,700.0 ns |         4,600.0 ns |
| 32     | Sorted        |           852.0 ns |         42.98 ns |        125.37 ns |           800.0 ns |           600.0 ns |         1,100.0 ns |
| 32     | Reversed      |         4,308.2 ns |        253.13 ns |        734.37 ns |         4,200.0 ns |         3,300.0 ns |         6,000.0 ns |
| 64     | Randomized    |         8,415.4 ns |        401.71 ns |      1,126.44 ns |         8,300.0 ns |         5,400.0 ns |        11,400.0 ns |
| 64     | Sorted        |         1,262.0 ns |         27.01 ns |         66.26 ns |         1,300.0 ns |         1,200.0 ns |         1,400.0 ns |
| 64     | Reversed      |         7,896.9 ns |        282.00 ns |        813.63 ns |         7,750.0 ns |         6,600.0 ns |        10,300.0 ns |
| 256    | Randomized    |        17,742.7 ns |        356.68 ns |        901.38 ns |        17,500.0 ns |        16,400.0 ns |        20,300.0 ns |
| 256    | Sorted        |         4,534.3 ns |        161.27 ns |        472.98 ns |         4,400.0 ns |         3,700.0 ns |         5,600.0 ns |
| 256    | Reversed      |        21,685.5 ns |        434.87 ns |        990.42 ns |        21,500.0 ns |        19,900.0 ns |        23,900.0 ns |
| 1000   | Randomized    |       111,866.7 ns |      2,232.14 ns |      2,087.95 ns |       111,900.0 ns |       107,000.0 ns |       115,300.0 ns |
| 1000   | Sorted        |         9,126.4 ns |        220.76 ns |        604.34 ns |         9,100.0 ns |         8,000.0 ns |        11,000.0 ns |
| 1000   | Reversed      |       188,800.0 ns |        931.47 ns |        777.82 ns |       188,800.0 ns |       187,800.0 ns |       190,400.0 ns |
| 2000   | Randomized    |       387,007.7 ns |      6,482.49 ns |      5,413.17 ns |       387,800.0 ns |       376,300.0 ns |       396,400.0 ns |
| 2000   | Sorted        |        11,363.9 ns |        374.60 ns |      1,086.78 ns |        11,300.0 ns |         9,800.0 ns |        14,000.0 ns |
| 2000   | Reversed      |       695,434.9 ns |      7,710.53 ns |     14,291.97 ns |       697,900.0 ns |       681,600.0 ns |       773,600.0 ns |
| 4000   | Randomized    |     1,437,292.3 ns |     28,418.56 ns |     58,689.25 ns |     1,412,650.0 ns |     1,358,600.0 ns |     1,615,400.0 ns |
| 4000   | Sorted        |        15,180.4 ns |        348.10 ns |      1,009.91 ns |        14,900.0 ns |        13,700.0 ns |        17,500.0 ns |
| 4000   | Reversed      |     2,670,300.0 ns |     25,963.98 ns |     21,681.10 ns |     2,658,900.0 ns |     2,651,100.0 ns |     2,715,200.0 ns |
| 10000  | Randomized    |     8,504,800.0 ns |    165,472.91 ns |    154,783.47 ns |     8,460,900.0 ns |     8,315,700.0 ns |     8,799,500.0 ns |
| 10000  | Sorted        |        28,139.1 ns |        563.55 ns |      1,306.10 ns |        27,800.0 ns |        25,100.0 ns |        31,100.0 ns |
| 10000  | Reversed      |    16,225,513.3 ns |    240,226.26 ns |    224,707.79 ns |    16,110,200.0 ns |    15,949,600.0 ns |    16,596,200.0 ns |
| 20000  | Randomized    |    32,967,280.0 ns |    509,725.78 ns |    476,797.81 ns |    32,877,200.0 ns |    32,371,100.0 ns |    33,721,000.0 ns |
| 20000  | Sorted        |        48,227.3 ns |        944.99 ns |      1,160.53 ns |        48,600.0 ns |        45,700.0 ns |        50,400.0 ns |
| 20000  | Reversed      |    66,559,740.0 ns |    873,898.76 ns |    817,445.44 ns |    66,416,800.0 ns |    65,760,300.0 ns |    68,346,900.0 ns |
| 40000  | Randomized    |   133,498,413.3 ns |    665,202.94 ns |    622,231.24 ns |   133,498,500.0 ns |   132,357,000.0 ns |   134,531,700.0 ns |
| 40000  | Sorted        |        88,530.8 ns |        617.22 ns |        515.40 ns |        88,300.0 ns |        88,000.0 ns |        89,700.0 ns |
| 40000  | Reversed      |   268,100,500.0 ns |  1,258,206.70 ns |  1,176,927.33 ns |   268,231,700.0 ns |   266,368,200.0 ns |   270,312,500.0 ns |
| 80000  | Randomized    |   534,337,900.0 ns |  2,143,202.63 ns |  2,004,753.07 ns |   534,716,100.0 ns |   530,454,500.0 ns |   537,762,800.0 ns |
| 80000  | Sorted        |       167,185.7 ns |        702.19 ns |        622.47 ns |       166,850.0 ns |       166,500.0 ns |       168,300.0 ns |
| 80000  | Reversed      | 1,072,764,220.0 ns |  2,206,447.48 ns |  2,063,912.34 ns | 1,073,535,400.0 ns | 1,069,598,000.0 ns | 1,075,414,900.0 ns |
| 100000 | Randomized    |   835,795,033.3 ns |  3,033,537.37 ns |  2,837,572.74 ns |   836,489,600.0 ns |   830,347,500.0 ns |   839,696,400.0 ns |
| 100000 | Sorted        |       204,575.0 ns |      4,014.61 ns |      5,220.13 ns |       206,100.0 ns |       187,500.0 ns |       208,500.0 ns |
| 100000 | Reversed      | 1,675,388,566.7 ns |  2,905,866.22 ns |  2,718,149.06 ns | 1,675,470,200.0 ns | 1,671,945,200.0 ns | 1,680,429,400.0 ns |
| 200000 | Randomized    | 3,359,733,842.9 ns | 14,902,495.95 ns | 13,210,674.50 ns | 3,358,438,050.0 ns | 3,342,596,100.0 ns | 3,385,299,800.0 ns |
| 200000 | Sorted        |       405,392.3 ns |      2,918.85 ns |      2,437.37 ns |       404,600.0 ns |       403,700.0 ns |       411,300.0 ns |
| 200000 | Reversed      | 6,748,619,528.6 ns | 55,618,928.40 ns | 49,304,731.34 ns | 6,730,898,900.0 ns | 6,697,654,800.0 ns | 6,863,595,300.0 ns |

### Merge Sort

The merge sort appears more logarithmic, but it's hard to tell from this benchmark since a merge sort is usually used with larger data sets.

![Merge Sort Benchmark Graph](https://raw.githubusercontent.com/DanielWillett/ITCS6114.Project1/refs/heads/main/Assets/Benchmark_MergeSort.png)

#### Benchmark Results

| Size   | Data Type     | Mean               | Error            | StdDev           | Median             | Min                | Max                |
|------- |-------------- |-------------------:|-----------------:|-----------------:|-------------------:|-------------------:|-------------------:|
| 32     | Randomized    |         4,009.1 ns |        133.67 ns |        392.04 ns |         4,000.0 ns |         3,400.0 ns |         4,800.0 ns |
| 32     | Sorted        |         2,765.7 ns |         89.31 ns |        261.92 ns |         2,700.0 ns |         2,400.0 ns |         3,700.0 ns |
| 32     | Reversed      |         3,139.8 ns |         94.27 ns |        274.98 ns |         3,100.0 ns |         2,800.0 ns |         3,900.0 ns |
| 64     | Randomized    |         7,944.0 ns |        187.90 ns |        554.02 ns |         8,000.0 ns |         6,700.0 ns |         8,900.0 ns |
| 64     | Sorted        |         5,155.1 ns |        108.53 ns |        316.59 ns |         5,200.0 ns |         4,500.0 ns |         5,800.0 ns |
| 64     | Reversed      |         6,201.0 ns |        206.07 ns |        607.61 ns |         6,200.0 ns |         5,100.0 ns |         7,500.0 ns |
| 256    | Randomized    |        29,259.2 ns |        581.18 ns |      1,425.64 ns |        29,100.0 ns |        27,300.0 ns |        33,700.0 ns |
| 256    | Sorted        |        17,995.3 ns |        346.84 ns |        642.89 ns |        17,800.0 ns |        17,100.0 ns |        20,100.0 ns |
| 256    | Reversed      |        20,001.1 ns |        649.75 ns |      1,853.77 ns |        20,150.0 ns |        17,200.0 ns |        26,500.0 ns |
| 1000   | Randomized    |       123,876.9 ns |        703.66 ns |        587.59 ns |       123,800.0 ns |       122,700.0 ns |       125,000.0 ns |
| 1000   | Sorted        |        62,508.6 ns |      1,180.49 ns |      1,939.58 ns |        62,100.0 ns |        59,100.0 ns |        67,600.0 ns |
| 1000   | Reversed      |        69,024.1 ns |      1,366.06 ns |      2,881.49 ns |        68,550.0 ns |        62,800.0 ns |        78,700.0 ns |
| 2000   | Randomized    |       251,843.8 ns |      4,966.75 ns |      7,732.63 ns |       248,600.0 ns |       242,300.0 ns |       272,700.0 ns |
| 2000   | Sorted        |       124,755.2 ns |      2,476.21 ns |      3,629.60 ns |       125,000.0 ns |       118,900.0 ns |       133,300.0 ns |
| 2000   | Reversed      |       135,800.0 ns |      2,686.53 ns |      3,852.95 ns |       135,150.0 ns |       129,300.0 ns |       143,100.0 ns |
| 4000   | Randomized    |       479,638.5 ns |      6,579.85 ns |      5,494.47 ns |       481,400.0 ns |       472,000.0 ns |       488,900.0 ns |
| 4000   | Sorted        |       237,442.9 ns |      4,740.50 ns |      4,202.33 ns |       236,550.0 ns |       232,000.0 ns |       246,500.0 ns |
| 4000   | Reversed      |       260,125.0 ns |      4,651.66 ns |      3,631.71 ns |       259,550.0 ns |       256,300.0 ns |       270,000.0 ns |
| 10000  | Randomized    |     1,338,018.0 ns |     44,746.82 ns |    131,937.03 ns |     1,260,050.0 ns |     1,202,300.0 ns |     1,636,600.0 ns |
| 10000  | Sorted        |       619,574.7 ns |     22,461.49 ns |     64,446.24 ns |       593,200.0 ns |       569,700.0 ns |       799,600.0 ns |
| 10000  | Reversed      |       652,983.3 ns |     11,518.74 ns |     12,324.93 ns |       647,300.0 ns |       642,900.0 ns |       683,600.0 ns |
| 20000  | Randomized    |     2,521,900.0 ns |     32,381.55 ns |     28,705.40 ns |     2,519,900.0 ns |     2,482,200.0 ns |     2,595,100.0 ns |
| 20000  | Sorted        |     1,159,715.4 ns |     12,590.68 ns |     10,513.79 ns |     1,158,000.0 ns |     1,147,200.0 ns |     1,182,800.0 ns |
| 20000  | Reversed      |     1,334,617.1 ns |     25,872.13 ns |     42,508.65 ns |     1,329,300.0 ns |     1,286,700.0 ns |     1,430,500.0 ns |
| 40000  | Randomized    |     5,253,942.9 ns |     86,727.63 ns |     76,881.78 ns |     5,228,050.0 ns |     5,102,800.0 ns |     5,380,700.0 ns |
| 40000  | Sorted        |     2,334,426.7 ns |     27,542.77 ns |     25,763.53 ns |     2,322,900.0 ns |     2,302,700.0 ns |     2,385,100.0 ns |
| 40000  | Reversed      |     2,749,727.6 ns |     54,931.88 ns |    139,819.17 ns |     2,679,050.0 ns |     2,592,300.0 ns |     3,094,700.0 ns |
| 80000  | Randomized    |     4,792,341.2 ns |     80,698.91 ns |    130,313.71 ns |     4,771,800.0 ns |     4,630,500.0 ns |     5,081,700.0 ns |
| 80000  | Sorted        |     4,700,426.7 ns |     73,250.96 ns |     68,518.99 ns |     4,702,000.0 ns |     4,606,400.0 ns |     4,805,900.0 ns |
| 80000  | Reversed      |     5,554,933.3 ns |     93,340.95 ns |     87,311.18 ns |     5,543,000.0 ns |     5,398,200.0 ns |     5,710,000.0 ns |
| 100000 | Randomized    |     6,047,631.6 ns |    118,713.84 ns |    131,950.12 ns |     5,973,100.0 ns |     5,897,300.0 ns |     6,311,400.0 ns |
| 100000 | Sorted        |     5,935,758.3 ns |     19,717.04 ns |     15,393.77 ns |     5,932,000.0 ns |     5,909,300.0 ns |     5,965,200.0 ns |
| 100000 | Reversed      |     6,771,092.3 ns |     31,667.23 ns |     26,443.57 ns |     6,762,600.0 ns |     6,738,300.0 ns |     6,825,600.0 ns |
| 200000 | Randomized    |    12,754,600.0 ns |    171,841.42 ns |    152,332.95 ns |    12,764,000.0 ns |    12,460,000.0 ns |    13,004,500.0 ns |
| 200000 | Sorted        |     3,002,327.9 ns |     59,573.85 ns |    142,735.16 ns |     2,948,800.0 ns |     2,771,300.0 ns |     3,317,200.0 ns |
| 200000 | Reversed      |     2,980,102.2 ns |     58,414.46 ns |    112,544.83 ns |     2,942,950.0 ns |     2,840,500.0 ns |     3,279,100.0 ns |

### Heap Sort

The heap sort clearly follows the expected logarithmic runtime, and appears to perform better on a reversed list, likely due to how the heap is constructed.

![Heap Sort Benchmark Graph](https://raw.githubusercontent.com/DanielWillett/ITCS6114.Project1/refs/heads/main/Assets/Benchmark_HeapSort.png)

#### Benchmark Results

| Size   | Data Type     | Mean               | Error            | StdDev           | Median             | Min                | Max                |
|------- |-------------- |-------------------:|-----------------:|-----------------:|-------------------:|-------------------:|-------------------:|
| 32     | Randomized    |         3,955.6 ns |        101.50 ns |        297.69 ns |         3,900.0 ns |         3,300.0 ns |         4,700.0 ns |
| 32     | Sorted        |         3,237.0 ns |         87.12 ns |        256.89 ns |         3,200.0 ns |         2,900.0 ns |         3,800.0 ns |
| 32     | Reversed      |         3,914.1 ns |         99.35 ns |        291.38 ns |         3,900.0 ns |         3,000.0 ns |         4,500.0 ns |
| 64     | Randomized    |         6,667.7 ns |        186.71 ns |        538.71 ns |         6,700.0 ns |         5,600.0 ns |         7,700.0 ns |
| 64     | Sorted        |         6,395.6 ns |        128.23 ns |        243.98 ns |         6,400.0 ns |         5,900.0 ns |         6,900.0 ns |
| 64     | Reversed      |         7,294.0 ns |        154.02 ns |        454.12 ns |         7,400.0 ns |         6,200.0 ns |         8,200.0 ns |
| 256    | Randomized    |        30,426.3 ns |      1,361.12 ns |      3,991.92 ns |        30,400.0 ns |        21,200.0 ns |        39,600.0 ns |
| 256    | Sorted        |        28,958.6 ns |        721.05 ns |      2,114.72 ns |        28,700.0 ns |        23,800.0 ns |        33,900.0 ns |
| 256    | Reversed      |        39,262.6 ns |        830.91 ns |      2,436.90 ns |        39,100.0 ns |        35,600.0 ns |        46,500.0 ns |
| 1000   | Randomized    |       102,616.0 ns |      3,507.06 ns |     10,005.84 ns |       102,950.0 ns |        82,500.0 ns |       127,400.0 ns |
| 1000   | Sorted        |       108,576.9 ns |      2,151.70 ns |      3,768.53 ns |       107,600.0 ns |       103,300.0 ns |       119,600.0 ns |
| 1000   | Reversed      |       165,304.9 ns |      3,294.13 ns |      8,735.57 ns |       162,050.0 ns |       155,900.0 ns |       203,100.0 ns |
| 2000   | Randomized    |       168,815.4 ns |      7,362.00 ns |     20,643.84 ns |       169,400.0 ns |       121,400.0 ns |       211,600.0 ns |
| 2000   | Sorted        |       206,485.2 ns |      4,006.47 ns |      5,616.51 ns |       205,600.0 ns |       198,700.0 ns |       223,000.0 ns |
| 2000   | Reversed      |       290,786.4 ns |      5,774.37 ns |     12,795.60 ns |       286,300.0 ns |       271,900.0 ns |       324,700.0 ns |
| 4000   | Randomized    |       334,755.3 ns |     23,068.73 ns |     65,816.40 ns |       322,450.0 ns |       230,100.0 ns |       510,100.0 ns |
| 4000   | Sorted        |       395,840.0 ns |      7,524.99 ns |     10,045.65 ns |       394,400.0 ns |       383,600.0 ns |       425,300.0 ns |
| 4000   | Reversed      |       678,308.7 ns |     22,046.18 ns |     62,181.64 ns |       649,250.0 ns |       621,600.0 ns |       850,800.0 ns |
| 10000  | Randomized    |       811,726.3 ns |     48,600.33 ns |    142,536.38 ns |       801,500.0 ns |       559,700.0 ns |     1,141,400.0 ns |
| 10000  | Sorted        |       857,245.9 ns |     17,134.95 ns |     29,096.44 ns |       857,600.0 ns |       798,200.0 ns |       952,300.0 ns |
| 10000  | Reversed      |     1,744,372.2 ns |     33,510.72 ns |     35,856.10 ns |     1,742,100.0 ns |     1,696,400.0 ns |     1,820,000.0 ns |
| 20000  | Randomized    |     1,557,956.2 ns |     97,239.30 ns |    280,557.63 ns |     1,559,100.0 ns |       744,800.0 ns |     2,272,900.0 ns |
| 20000  | Sorted        |     1,775,295.7 ns |     35,348.06 ns |     85,369.55 ns |     1,770,800.0 ns |     1,632,000.0 ns |     1,990,700.0 ns |
| 20000  | Reversed      |     2,365,766.0 ns |    457,299.41 ns |  1,348,357.71 ns |     1,287,050.0 ns |     1,077,900.0 ns |     4,079,400.0 ns |
| 40000  | Randomized    |     2,774,447.0 ns |    352,587.78 ns |  1,039,613.10 ns |     2,609,350.0 ns |       919,000.0 ns |     4,917,200.0 ns |
| 40000  | Sorted        |     3,648,560.6 ns |     72,277.15 ns |    114,639.23 ns |     3,625,700.0 ns |     3,509,000.0 ns |     3,913,200.0 ns |
| 40000  | Reversed      |     7,647,680.0 ns |    144,627.44 ns |    135,284.60 ns |     7,629,700.0 ns |     7,464,500.0 ns |     7,970,700.0 ns |
| 80000  | Randomized    |     3,555,144.9 ns |    572,043.82 ns |  1,668,678.80 ns |     3,051,400.0 ns |     1,176,900.0 ns |     7,865,500.0 ns |
| 80000  | Sorted        |     7,324,155.6 ns |    141,500.56 ns |    151,404.01 ns |     7,338,200.0 ns |     7,077,700.0 ns |     7,596,800.0 ns |
| 80000  | Reversed      |     5,443,442.6 ns |    108,057.28 ns |    227,929.51 ns |     5,351,600.0 ns |     5,142,700.0 ns |     5,949,200.0 ns |
| 100000 | Randomized    |     3,207,978.5 ns |    433,666.45 ns |  1,230,239.63 ns |     2,656,700.0 ns |     1,853,100.0 ns |     6,667,100.0 ns |
| 100000 | Sorted        |     2,993,811.3 ns |     59,530.46 ns |    124,262.26 ns |     2,952,800.0 ns |     2,876,200.0 ns |     3,477,100.0 ns |
| 100000 | Reversed      |     6,805,843.1 ns |    135,230.87 ns |    276,240.86 ns |     6,710,300.0 ns |     6,384,300.0 ns |     7,363,300.0 ns |
| 200000 | Randomized    |     6,748,200.0 ns |    963,799.39 ns |  2,780,781.81 ns |     5,378,750.0 ns |     2,735,800.0 ns |    12,747,400.0 ns |
| 200000 | Sorted        |     6,175,738.5 ns |    122,781.55 ns |    168,064.79 ns |     6,124,300.0 ns |     5,960,000.0 ns |     6,500,700.0 ns |
| 200000 | Reversed      |    13,844,678.9 ns |    269,950.13 ns |    300,048.85 ns |    13,911,600.0 ns |    13,398,900.0 ns |    14,376,000.0 ns |

### Standard Quick Sort

The non-modified quick sort performs best when the array is sorted or reversed, but still appears logarithmic for random data.

![Standard Quick Sort Benchmark Graph](https://raw.githubusercontent.com/DanielWillett/ITCS6114.Project1/refs/heads/main/Assets/Benchmark_QuickSort_Standard.png)

#### Benchmark Results

| Size   | Data Type     | Mean               | Error            | StdDev           | Median             | Min                | Max                |
|------- |-------------- |-------------------:|-----------------:|-----------------:|-------------------:|-------------------:|-------------------:|
| 32     | Randomized    |         2,532.0 ns |         59.02 ns |        174.01 ns |         2,500.0 ns |         2,000.0 ns |         2,900.0 ns |
| 32     | Sorted        |         1,684.8 ns |         51.17 ns |        150.08 ns |         1,600.0 ns |         1,400.0 ns |         2,100.0 ns |
| 32     | Reversed      |         1,630.0 ns |         42.16 ns |        124.32 ns |         1,600.0 ns |         1,400.0 ns |         2,000.0 ns |
| 64     | Randomized    |         5,729.3 ns |        283.20 ns |        830.57 ns |         5,600.0 ns |         4,600.0 ns |         7,900.0 ns |
| 64     | Sorted        |         4,017.0 ns |        203.15 ns |        599.00 ns |         4,100.0 ns |         3,100.0 ns |         5,600.0 ns |
| 64     | Reversed      |         3,183.9 ns |         63.48 ns |         96.94 ns |         3,200.0 ns |         3,000.0 ns |         3,400.0 ns |
| 256    | Randomized    |        26,356.7 ns |        765.49 ns |      2,220.83 ns |        26,000.0 ns |        22,800.0 ns |        32,700.0 ns |
| 256    | Sorted        |        14,249.5 ns |        613.97 ns |      1,741.73 ns |        13,700.0 ns |        12,000.0 ns |        19,300.0 ns |
| 256    | Reversed      |        14,683.3 ns |        605.36 ns |      1,746.62 ns |        14,350.0 ns |        11,600.0 ns |        19,600.0 ns |
| 1000   | Randomized    |       102,138.7 ns |      2,037.22 ns |      5,148.31 ns |       101,000.0 ns |        95,200.0 ns |       118,100.0 ns |
| 1000   | Sorted        |        47,994.7 ns |      1,291.23 ns |      3,683.95 ns |        46,500.0 ns |        43,300.0 ns |        59,500.0 ns |
| 1000   | Reversed      |        48,792.6 ns |      1,518.35 ns |      4,356.44 ns |        47,500.0 ns |        42,500.0 ns |        61,600.0 ns |
| 2000   | Randomized    |       206,909.4 ns |      4,112.19 ns |      6,402.19 ns |       204,100.0 ns |       199,300.0 ns |       222,400.0 ns |
| 2000   | Sorted        |        91,218.6 ns |      1,825.29 ns |      4,044.73 ns |        90,300.0 ns |        84,400.0 ns |       102,900.0 ns |
| 2000   | Reversed      |        93,413.7 ns |      1,699.82 ns |      3,472.29 ns |        93,200.0 ns |        86,900.0 ns |       105,400.0 ns |
| 4000   | Randomized    |       420,538.5 ns |      8,299.82 ns |     11,360.88 ns |       416,850.0 ns |       407,700.0 ns |       463,100.0 ns |
| 4000   | Sorted        |       179,511.5 ns |      3,438.10 ns |      4,706.11 ns |       179,600.0 ns |       169,500.0 ns |       190,200.0 ns |
| 4000   | Reversed      |       185,900.0 ns |      3,556.94 ns |      3,327.16 ns |       185,000.0 ns |       181,100.0 ns |       193,600.0 ns |
| 10000  | Randomized    |     1,143,031.0 ns |     42,803.95 ns |    126,208.42 ns |     1,063,300.0 ns |     1,026,000.0 ns |     1,411,600.0 ns |
| 10000  | Sorted        |       427,066.7 ns |      8,210.94 ns |     11,510.60 ns |       427,100.0 ns |       405,400.0 ns |       447,500.0 ns |
| 10000  | Reversed      |       437,923.1 ns |      7,391.70 ns |      6,172.41 ns |       436,600.0 ns |       426,700.0 ns |       449,200.0 ns |
| 20000  | Randomized    |     1,897,500.0 ns |    199,807.02 ns |    589,135.54 ns |     2,188,950.0 ns |       994,000.0 ns |     2,542,000.0 ns |
| 20000  | Sorted        |       844,700.0 ns |     16,153.43 ns |     19,837.87 ns |       846,650.0 ns |       802,200.0 ns |       883,800.0 ns |
| 20000  | Reversed      |       865,030.0 ns |      6,157.78 ns |     10,945.44 ns |       865,800.0 ns |       833,800.0 ns |       888,400.0 ns |
| 40000  | Randomized    |     4,451,983.3 ns |     85,124.47 ns |     91,082.23 ns |     4,434,700.0 ns |     4,307,700.0 ns |     4,584,000.0 ns |
| 40000  | Sorted        |     1,718,483.3 ns |     34,328.82 ns |     57,355.75 ns |     1,708,100.0 ns |     1,612,900.0 ns |     1,890,900.0 ns |
| 40000  | Reversed      |     1,738,318.8 ns |     20,909.60 ns |     20,536.02 ns |     1,734,050.0 ns |     1,704,700.0 ns |     1,786,500.0 ns |
| 80000  | Randomized    |     4,435,291.3 ns |     72,438.63 ns |    139,564.65 ns |     4,453,450.0 ns |     4,206,300.0 ns |     4,717,100.0 ns |
| 80000  | Sorted        |     3,439,350.0 ns |     59,547.54 ns |     96,158.19 ns |     3,400,850.0 ns |     3,302,700.0 ns |     3,644,100.0 ns |
| 80000  | Reversed      |     1,999,310.0 ns |    508,564.05 ns |  1,499,512.69 ns |       745,500.0 ns |       524,700.0 ns |     3,789,500.0 ns |
| 100000 | Randomized    |    10,938,860.0 ns |    189,889.96 ns |    177,623.18 ns |    10,904,500.0 ns |    10,729,100.0 ns |    11,329,600.0 ns |
| 100000 | Sorted        |     4,337,355.0 ns |     82,884.68 ns |     95,450.12 ns |     4,320,750.0 ns |     4,194,900.0 ns |     4,491,500.0 ns |
| 100000 | Reversed      |     4,371,260.0 ns |     86,162.34 ns |     80,596.30 ns |     4,327,400.0 ns |     4,266,700.0 ns |     4,505,700.0 ns |
| 200000 | Randomized    |    11,509,085.7 ns |    108,222.91 ns |     95,936.79 ns |    11,516,950.0 ns |    11,360,100.0 ns |    11,632,400.0 ns |
| 200000 | Sorted        |     1,314,233.3 ns |     18,740.51 ns |     40,740.32 ns |     1,301,900.0 ns |     1,261,200.0 ns |     1,487,500.0 ns |
| 200000 | Reversed      |     1,343,467.3 ns |     19,985.89 ns |     42,591.57 ns |     1,336,800.0 ns |     1,312,400.0 ns |     1,587,200.0 ns |

### Modified Quick Sort

The modified quick also performs better when the array is sorted or reversed, and also appears logarithmic.

The modified algorithm seems to better bridge the gap between random data and sorted or reversed data.

![Modified Quick Sort Benchmark Graph](https://raw.githubusercontent.com/DanielWillett/ITCS6114.Project1/refs/heads/main/Assets/Benchmark_QuickSort_Modified.png)

#### Benchmark Results

| Size   | Data Type     | Mean               | Error            | StdDev           | Median             | Min                | Max                |
|------- |-------------- |-------------------:|-----------------:|-----------------:|-------------------:|-------------------:|-------------------:|
| 32     | Randomized    |         2,726.0 ns |         81.04 ns |        238.95 ns |         2,700.0 ns |         2,000.0 ns |         3,300.0 ns |
| 32     | Sorted        |         1,232.3 ns |         50.58 ns |        148.34 ns |         1,200.0 ns |           900.0 ns |         1,500.0 ns |
| 32     | Reversed      |         1,833.0 ns |         80.24 ns |        236.58 ns |         1,800.0 ns |         1,500.0 ns |         2,400.0 ns |
| 64     | Randomized    |         5,255.6 ns |        178.79 ns |        524.35 ns |         5,200.0 ns |         4,100.0 ns |         6,200.0 ns |
| 64     | Sorted        |         2,238.5 ns |         46.56 ns |         63.73 ns |         2,200.0 ns |         2,100.0 ns |         2,300.0 ns |
| 64     | Reversed      |         3,681.6 ns |         98.37 ns |        286.95 ns |         3,700.0 ns |         3,100.0 ns |         4,500.0 ns |
| 256    | Randomized    |        23,657.7 ns |        653.79 ns |      1,896.75 ns |        23,400.0 ns |        19,600.0 ns |        27,500.0 ns |
| 256    | Sorted        |        11,109.1 ns |        582.40 ns |      1,708.09 ns |        11,300.0 ns |         8,700.0 ns |        14,500.0 ns |
| 256    | Reversed      |        15,742.7 ns |        501.27 ns |      1,446.28 ns |        15,900.0 ns |        13,300.0 ns |        19,000.0 ns |
| 1000   | Randomized    |        86,228.0 ns |      1,719.72 ns |      4,560.47 ns |        84,600.0 ns |        79,600.0 ns |       102,600.0 ns |
| 1000   | Sorted        |        33,383.9 ns |        962.07 ns |      2,729.24 ns |        32,700.0 ns |        29,800.0 ns |        41,100.0 ns |
| 1000   | Reversed      |        52,626.9 ns |      1,994.47 ns |      5,657.99 ns |        50,600.0 ns |        46,400.0 ns |        68,800.0 ns |
| 2000   | Randomized    |       179,663.6 ns |      3,570.08 ns |      6,705.47 ns |       177,350.0 ns |       171,700.0 ns |       204,000.0 ns |
| 2000   | Sorted        |        65,934.0 ns |      1,314.34 ns |      2,743.52 ns |        65,000.0 ns |        62,500.0 ns |        74,600.0 ns |
| 2000   | Reversed      |       104,546.3 ns |      1,894.33 ns |      3,995.79 ns |       103,350.0 ns |        99,400.0 ns |       117,100.0 ns |
| 4000   | Randomized    |       355,096.1 ns |      7,101.43 ns |     14,506.34 ns |       352,300.0 ns |       336,600.0 ns |       396,200.0 ns |
| 4000   | Sorted        |       131,852.0 ns |      2,624.29 ns |      3,503.35 ns |       131,800.0 ns |       126,400.0 ns |       139,100.0 ns |
| 4000   | Reversed      |       212,533.3 ns |      4,245.44 ns |      6,733.71 ns |       212,100.0 ns |       202,200.0 ns |       227,700.0 ns |
| 10000  | Randomized    |       962,229.0 ns |     40,148.35 ns |    118,378.33 ns |       893,750.0 ns |       849,500.0 ns |     1,273,700.0 ns |
| 10000  | Sorted        |       357,362.5 ns |      6,914.02 ns |      8,990.18 ns |       355,450.0 ns |       346,600.0 ns |       384,100.0 ns |
| 10000  | Reversed      |       547,406.2 ns |     10,788.37 ns |     10,595.63 ns |       545,950.0 ns |       532,300.0 ns |       574,000.0 ns |
| 20000  | Randomized    |     1,824,285.7 ns |     26,643.35 ns |     23,618.63 ns |     1,825,600.0 ns |     1,786,100.0 ns |     1,874,800.0 ns |
| 20000  | Sorted        |       658,167.0 ns |     74,409.85 ns |    204,946.35 ns |       711,400.0 ns |        94,700.0 ns |       918,200.0 ns |
| 20000  | Reversed      |     1,072,058.3 ns |     17,168.93 ns |     13,404.37 ns |     1,074,900.0 ns |     1,045,000.0 ns |     1,088,900.0 ns |
| 40000  | Randomized    |     2,638,673.0 ns |    340,073.84 ns |  1,002,715.46 ns |     2,064,500.0 ns |     1,685,000.0 ns |     5,107,700.0 ns |
| 40000  | Sorted        |     1,426,265.0 ns |     27,803.11 ns |     32,018.11 ns |     1,421,750.0 ns |     1,384,100.0 ns |     1,495,500.0 ns |
| 40000  | Reversed      |     2,156,871.4 ns |     42,059.89 ns |     50,069.31 ns |     2,142,500.0 ns |     2,088,300.0 ns |     2,318,600.0 ns |
| 80000  | Randomized    |     7,574,433.3 ns |    102,705.33 ns |     96,070.63 ns |     7,596,100.0 ns |     7,313,700.0 ns |     7,747,600.0 ns |
| 80000  | Sorted        |     2,833,792.9 ns |     34,764.24 ns |     30,817.59 ns |     2,828,800.0 ns |     2,777,200.0 ns |     2,890,500.0 ns |
| 80000  | Reversed      |     4,370,515.4 ns |     42,546.24 ns |     35,528.04 ns |     4,357,300.0 ns |     4,334,400.0 ns |     4,449,900.0 ns |
| 100000 | Randomized    |     4,780,312.5 ns |     60,971.34 ns |    108,376.50 ns |     4,789,750.0 ns |     4,589,700.0 ns |     5,023,000.0 ns |
| 100000 | Sorted        |     3,383,000.0 ns |     66,422.89 ns |    105,353.75 ns |     3,346,500.0 ns |     3,234,500.0 ns |     3,575,000.0 ns |
| 100000 | Reversed      |     5,415,145.0 ns |    101,830.91 ns |    117,268.63 ns |     5,386,650.0 ns |     5,250,300.0 ns |     5,666,900.0 ns |
| 200000 | Randomized    |    10,129,678.0 ns |    161,040.15 ns |    214,983.99 ns |    10,075,650.0 ns |     9,792,850.0 ns |    10,688,350.0 ns |
| 200000 | Sorted        |     6,712,500.0 ns |     75,447.35 ns |     66,882.11 ns |     6,718,850.0 ns |     6,556,100.0 ns |     6,821,700.0 ns |
| 200000 | Reversed      |     1,730,650.6 ns |     34,307.22 ns |     93,915.47 ns |     1,692,600.0 ns |     1,624,400.0 ns |     1,974,500.0 ns |

## Conclusion
It's obvious by the results that each algorithm has its place in the computing world. For example, insertion sort looks bad at first but is even used in other sorting algorithms like the modified quick sort because it's fast and has a low memory footprint for smaller arrays. Also, it's great if the array is already likely to be nearly sorted.

Merge sort seems to be best for very large data sets, but not so great for smaller sets like we're dealing with here. Same with heap-sort.

Modified quick-sort seems to generally be the winner when it comes to fast sorting algorithms for low to mid-sized data sets.

## Code
Public on GitHub at https://github.com/DanielWillett/ITCS6114.Project1.