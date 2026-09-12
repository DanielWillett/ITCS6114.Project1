using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable TailRecursiveCall

namespace ITCS6114.Project1.Impl;

/// <summary>
/// Vector-based binary tree implementation.
/// </summary>
public sealed class Heap<T> where T : IComparable<T>
{
    private static readonly Node InvalidNode = default;

    private HeapElement[] _items;
    private int _size;

    /// <summary>
    /// The root element in the heap.
    /// </summary>
    public Node Root => new Node(this, 1);

    /// <summary>
    /// Number of nodes in the heap.
    /// </summary>
    public int Count => _size;

    public Heap(int capacity = 64)
    {
        _items = capacity <= 0 ? Array.Empty<HeapElement>() : new HeapElement[capacity];
    }

    /// <summary>
    /// Insert a <paramref name="value"/> into the heap.
    /// </summary>
    /// <returns>The node at which it was inserted.</returns>
    public Node Add(T value)
    {
        int newSize = _size + 1;
        if (_items.Length < newSize)
        {
            // extend underlying array
            int capacity = Math.Max(newSize, unchecked ( (int)Math.Ceiling(_items.Length * 1.5) ));
            HeapElement[] newArray = new HeapElement[capacity];
            Array.Copy(_items, newArray, _items.Length);
            _items = newArray;
        }

        int index = newSize;
        ref HeapElement element = ref GetItem(index);
        element.Value = value;
        element.IsPresent = true;

        for (int i = index; i > 1; i /= 2)
        {
            ref HeapElement parent = ref GetItem(i / 2);
            ref HeapElement current = ref GetItem(i);
            if (parent.Value.CompareTo(current.Value) <= 0)
            {
                break;
            }

            (parent.Value, current.Value) = (current.Value, parent.Value);
            index = i / 2;
        }

        _size = newSize;
        return new Node(this, index);
    }

    /// <summary>
    /// Whether or not the given <paramref name="value"/> is in the heap.
    /// </summary>
    public bool Contains(T value) => Find(value).Exists;

    /// <summary>
    /// Find a node equal to the given <paramref name="value"/>.
    /// </summary>
    public Node Find(T value)
    {
        Node node = InvalidNode;
        FindRecursive(Root, value, ref node);
        return node;
    }

    private static bool FindRecursive(Node parent, T value, ref Node node)
    {
        if (!parent.Exists)
            return false;

        int comparison = parent.Value.CompareTo(value);
        switch (comparison)
        {
            case 0:
                node = parent;
                return true;
            case > 0:
                return false;
            default:
                return FindRecursive(parent.Left, value, ref node) || FindRecursive(parent.Right, value, ref node);
        }
    }

    /// <summary>
    /// Removes the minimum value from the heap.
    /// </summary>
    public T Pop()
    {
        ref HeapElement root = ref GetItem(1);
        int newSize = _size - 1;
        T minimum = root.Value;
        root.IsPresent = false;
        root.Value = default;
        root = _items[newSize];

        _size = newSize;

        for (int i = 1; i < newSize;)
        {
            ref HeapElement element = ref GetItem(i);
            int leftIndex = i * 2;
            int rightIndex = i * 2 + 1;
            if (HasItem(rightIndex) && GetItem(rightIndex).IsPresent)
            {
                // left and right children
                ref HeapElement left = ref GetItem(leftIndex);
                ref HeapElement right = ref GetItem(rightIndex); 
                if (element.Value.CompareTo(left.Value) <= 0 && element.Value.CompareTo(right.Value) <= 0)
                {
                    return minimum;
                }

                // swap element with smallest of both children
                switch (left.Value.CompareTo(right.Value))
                {
                    case < 0:
                        (left.Value, element.Value) = (element.Value, left.Value);
                        i = leftIndex;
                        break;
                    case > 0:
                        (right.Value, element.Value) = (element.Value, right.Value);
                        goto default;
                    default:
                        i = rightIndex;
                        break;
                }
                continue;
            }

            if (HasItem(leftIndex) && GetItem(leftIndex).IsPresent)
            {
                // left child but no right child
                ref HeapElement left = ref GetItem(leftIndex);
                if (element.Value.CompareTo(left.Value) > 0)
                {
                    (left.Value, element.Value) = (element.Value, left.Value);
                }
            }

            return minimum;
        }

        return minimum;
    }

    /// <summary>
    /// Copies this entire heap to an array.
    /// </summary>
    public T[] ToArray()
    {
        T[] array = new T[_size];
        for (int i = 0; i < array.Length; ++i)
        {
            array[i] = Pop();
        }

        return array;
    }

    // 0 is ignored so we just shift every index access by 1
    private bool HasItem(int index)
    {
        return index - 1 < _items.Length;
    }
    private ref HeapElement GetItem(int index)
    {
        return ref _items[index - 1];
    }

#nullable disable
    [DebuggerDisplay("{(IsPresent ? Value.ToString() : \"- empty -\"),nq}")]
    private struct HeapElement
    {
        public T Value;
        public bool IsPresent;
    }
#nullable restore

    /// <summary>
    /// A node in a <see cref="Heap{T}"/>.
    /// </summary>
    [DebuggerDisplay("{(Exists ? Value.ToString() : \"- empty -\"),nq}")]
    public readonly struct Node
    {
        private readonly Heap<T>? _heap;
        private readonly int _index;

        [MemberNotNullWhen(false, nameof(_heap))]
        internal bool IsDefault => _heap == null;

        /// <summary>
        /// Whether or not this node exists.
        /// </summary>
        [MemberNotNullWhen(true, nameof(_heap))]
        public bool Exists => _index > 0 && _heap != null && _heap.HasItem(_index) && _heap.GetItem(_index).IsPresent;

        /// <summary>
        /// The value stored in this node.
        /// </summary>
        public ref readonly T Value
        {
            get
            {
                if (_heap != null && _heap.HasItem(_index))
                {
                    ref HeapElement element = ref _heap.GetItem(_index);
                    if (element.IsPresent)
                        return ref element.Value;
                }

                throw new InvalidOperationException("This node does not exist.");
            }
        }

        /// <summary>
        /// Whether or not a left node is present.
        /// </summary>
        public bool HasLeft
        {
            get
            {
                if (_heap == null)
                    return false;

                int left = _index * 2;
                return _heap.HasItem(left) && _heap.GetItem(left).IsPresent;
            }
        }

        /// <summary>
        /// Whether or not a right node is present.
        /// </summary>
        public bool HasRight
        {
            get
            {
                if (_heap == null)
                    return false;

                int right = _index * 2 + 1;
                return _heap.HasItem(right) && _heap.GetItem(right).IsPresent;
            }
        }

        /// <summary>
        /// Whether or not this node is the root node.
        /// </summary>
        public bool IsRoot
        {
            get
            {
                if (_heap == null)
                    return false;

                return _index == 1;
            }
        }

        /// <summary>
        /// This node's left node.
        /// </summary>
        public Node Left
        {
            get
            {
                if (_heap == null)
                    return InvalidNode;

                int left = _index * 2;
                return new Node(_heap, left);
            }
        }

        /// <summary>
        /// This node's right node.
        /// </summary>
        public Node Right
        {
            get
            {
                if (_heap == null)
                    return InvalidNode;

                int right = _index * 2 + 1;
                return new Node(_heap, right);
            }
        }

        /// <summary>
        /// This node's parent node. Invalid if called on the root.
        /// </summary>
        public Node Parent
        {
            get
            {
                if (_heap == null || _index == 1)
                    return InvalidNode;

                int parent = _index / 2;
                return new Node(_heap, parent);
            }
        }

        internal Node(Heap<T> heap, int index)
        {
            _heap = heap;
            _index = index;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is Node n && n._heap == _heap && n._index == _index;
        }

        public override int GetHashCode() => HashCode.Combine(_heap, _index);
    }
}