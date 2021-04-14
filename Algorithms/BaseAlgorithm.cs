using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuresAndAlgorithms.Algorithms
{
    public class BaseAlgorithm<T> where T : IComparable
    {
        public List<T> Items { get; set; } = new List<T>();
        public uint SwapCount { get; protected set; } = 0;
        public uint CompareCount { get; protected set; } = 0;
        public event EventHandler<Tuple<T, T>> CompereEvent;
        public event EventHandler<Tuple<T, T>> SwapEvent;
        public BaseAlgorithm() { }
        public BaseAlgorithm(IEnumerable<T> items)
        {
            Items.AddRange(items);
        }
        protected void Swap(int left, int right)
        {
            if (left < Items.Count && right < Items.Count)
            {
                SwapEvent?.Invoke(this, new Tuple<T, T>(Items[left], Items[right]));
                SwapCount++;
                (Items[left], Items[right]) = (Items[right], Items[left]);
            }
        }
        protected virtual void DoSort()
        {
            Items.Sort();
        }
        public TimeSpan Sort()
        {
            SwapCount = 0;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            DoSort();
            timer.Stop();
            return timer.Elapsed;

        }
        protected int Compare(T left, T right)
        {
            CompereEvent?.Invoke(this, new Tuple<T, T>(left, right));
            CompareCount++;
            return left.CompareTo(right);
        }
    }
}
