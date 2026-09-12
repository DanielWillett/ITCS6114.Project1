using ITCS6114.Project1.Impl;

namespace ITCS6114.Project1.Tests;

// ReSharper disable AsyncMethodWithoutAwait

public class HeapTests
{
    [Test]
    public async Task Insert_SingleItem()
    {
        Heap<int> heap = new Heap<int>();

        await Assert.That(heap.Count).IsZero();

        Heap<int>.Node rootNode = heap.Add(1);

        await Assert.That(heap.Count).IsEqualTo(1);

        await Assert.That(heap.Root.Value).IsEqualTo(1);
        
        await Assert.That(heap.Root.IsRoot).IsTrue();
        await Assert.That(heap.Root.Parent.Exists).IsFalse();
       
        await Assert.That(heap.Root.HasLeft).IsFalse();
        await Assert.That(heap.Root.Left.Exists).IsFalse();
        
        await Assert.That(heap.Root.HasRight).IsFalse();
        await Assert.That(heap.Root.Right.Exists).IsFalse();

        await Assert.That(heap.Root).IsEqualTo(rootNode);
    }

    [Test]
    public async Task Remove_SingleItem()
    {
        Heap<int> heap = new Heap<int>();
        heap.Add(1);

        int popped = heap.Pop();

        await Assert.That(heap.Count).IsZero();

        await Assert.That(popped).IsEqualTo(1);
        
        await Assert.That(heap.Root.Exists).IsFalse();
    }

    [Test]
    public async Task EnqueueAndDequeueInOrder()
    {
        int[] test = [ 1, 8, 12, 4, 8, 28, -4, -1 ];

        Heap<int> heap = new Heap<int>();
        foreach (int v in test)
        {
            Heap<int>.Node n = heap.Add(v);
            await Assert.That(n.Value).IsEqualTo(v);
        }

        await Assert.That(heap.Contains(1)).IsTrue();
        await Assert.That(heap.Contains(8)).IsTrue();
        await Assert.That(heap.Contains(12)).IsTrue();
        await Assert.That(heap.Contains(4)).IsTrue();
        await Assert.That(heap.Contains(28)).IsTrue();
        await Assert.That(heap.Contains(-1)).IsTrue();
        await Assert.That(heap.Contains(-4)).IsTrue();
        await Assert.That(heap.Find(1).Value).IsEqualTo(1);
        await Assert.That(heap.Find(8).Value).IsEqualTo(8);
        await Assert.That(heap.Find(12).Value).IsEqualTo(12);
        await Assert.That(heap.Find(4).Value).IsEqualTo(4);
        await Assert.That(heap.Find(28).Value).IsEqualTo(28);
        await Assert.That(heap.Find(-1).Value).IsEqualTo(-1);
        await Assert.That(heap.Find(-4).Value).IsEqualTo(-4);

        await Assert.That(heap.Contains(-2)).IsFalse();
        await Assert.That(heap.Contains(-9)).IsFalse();
        await Assert.That(heap.Contains(48)).IsFalse();
        await Assert.That(heap.Contains(21)).IsFalse();
        await Assert.That(heap.Contains(16)).IsFalse();
        await Assert.That(heap.Contains(2)).IsFalse();
        await Assert.That(heap.Contains(0)).IsFalse();
        await Assert.That(heap.Contains(5)).IsFalse();

        await Assert.That(heap.Count).IsEqualTo(test.Length);

        int prev = heap.Pop();
        while (heap.Count > 0)
        {
            int p = heap.Pop();
            await Assert.That(prev <= p).IsTrue();
            prev = p;
        }

        await Assert.That(heap.Count).IsZero();
    }

    [Test]
    public async Task Resize()
    {
        int[] test = [ 1, 8, 12, 4, 8, 28, -1 ];

        Heap<int> heap = new Heap<int>(capacity: 4);
        foreach (int v in test)
        {
            Heap<int>.Node n = heap.Add(v);
            await Assert.That(n.Value).IsEqualTo(v);
        }

        await Assert.That(heap.Count).IsEqualTo(test.Length);

        int prev = heap.Pop();
        while (heap.Count > 0)
        {
            int p = heap.Pop();
            await Assert.That(prev <= p).IsTrue();
            prev = p;
        }

        await Assert.That(heap.Count).IsZero();
    }

    [Test]
    public async Task ToArray()
    {
        int[] test = [ 1, 8, 12, 4, 8, 28, -1 ];

        Heap<int> heap = new Heap<int>(capacity: 4);
        foreach (int v in test)
        {
            Heap<int>.Node n = heap.Add(v);
            await Assert.That(n.Value).IsEqualTo(v);
        }

        int[] sorted = heap.ToArray();
        await Assert.That(sorted).IsInOrder();
        await Assert.That(sorted.Length).IsEqualTo(test.Length);

        CommonHelpers.PrintIntArray("Unsorted : {0}", test);
        CommonHelpers.PrintIntArray("Sorted   : {0}", sorted);
    }
}