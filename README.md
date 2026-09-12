# Project 1

Daniel Willett - 09/11/2026

Implement the following sorting algorithms.

* Insertion sort
* Merge sort
* Heapsort [vector based, and insert one item at a time]
* In-place quicksort (any random item or the first or the last item of your input can be pivot).
* Modified quicksort
  * Use median-of-three as pivot.
  * For small sub-problem of size  , you must use insertion sort.

Execution instructions:

* Run these algorithms for different input sizes (i.e. = 1000, 2000, 3K, 4K, 5K, 10K, 20K, 40K, 50K, 60K, 80K, 90K, 100K). You will randomly generate input for execution. Record the execution time (need to take the average of several runs) and plot them all in a single graph against various input sizes. Note that, you will compare all sorting algorithms for the same data set. 
* Also observe and present performance of the following two special cases:
  * Input array is already sorted.
  * Input array is reversely sorted.

## Testing Instructions

Install the [.NET SDK](https://dotnet.microsoft.com/en-us/download) if you don't already have it.

### Run Unit Tests
Unit tests run all sorting algorithms and also test the Heap class.
```bash
dotnet test -c Release
```

### Run Benchmarks
Benchmarks measure the performance of each algorithm and export the data to `./BenchmarkDotNet.Artifacts/results`.
```bash
dotnet run -c Release --project ITCS6114.Project1.Benchmarks
```

## Code
Heap sort: 