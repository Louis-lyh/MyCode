using System;
using System.Collections;
using System.Collections.Generic;

namespace _1_Code.AStart
{
    public class CustomPriorityQueue<TElement, TPriority>
    {
        private readonly List<(TElement Element, TPriority Priority)> _heap;
        private readonly IComparer<TPriority> _priorityComparer;

        public int Count => _heap.Count;
        public IComparer<TPriority> Comparer => _priorityComparer;

        public CustomPriorityQueue() : this(Comparer<TPriority>.Default) { }

        public CustomPriorityQueue(IComparer<TPriority>? comparer)
        {
            _priorityComparer = comparer ?? Comparer<TPriority>.Default;
            _heap = new List<(TElement, TPriority)>();
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            _heap.Add((element, priority));
            BubbleUp(_heap.Count - 1);
        }

        public TElement Dequeue()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            var result = _heap[0];
            Swap(0, _heap.Count - 1);
            _heap.RemoveAt(_heap.Count - 1);
            BubbleDown(0);
            return result.Element;
        }

        public TElement Peek()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            return _heap[0].Element;
        }

        public bool TryDequeue(out TElement element, out TPriority priority)
        {
            if (_heap.Count == 0)
            {
                element = default!;
                priority = default!;
                return false;
            }

            var result = _heap[0];
            Swap(0, _heap.Count - 1);
            _heap.RemoveAt(_heap.Count - 1);
            BubbleDown(0);

            element = result.Element;
            priority = result.Priority;
            return true;
        }

        public bool TryPeek(out TElement element, out TPriority priority)
        {
            if (_heap.Count == 0)
            {
                element = default!;
                priority = default!;
                return false;
            }

            element = _heap[0].Element;
            priority = _heap[0].Priority;
            return true;
        }

        public void Clear()
        {
            _heap.Clear();
        }

        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (ComparePriorities(index, parent) >= 0)
                    break;

                Swap(index, parent);
                index = parent;
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                int minIndex = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;

                if (left < _heap.Count && ComparePriorities(left, minIndex) < 0)
                    minIndex = left;

                if (right < _heap.Count && ComparePriorities(right, minIndex) < 0)
                    minIndex = right;

                if (minIndex == index) break;

                Swap(index, minIndex);
                index = minIndex;
            }
        }

        private int ComparePriorities(int indexA, int indexB)
        {
            return _priorityComparer.Compare(
                _heap[indexA].Priority, 
                _heap[indexB].Priority);
        }

        private void Swap(int i, int j)
        {
            (_heap[i], _heap[j]) = (_heap[j], _heap[i]);
        }
    }
}