using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuresAndAlgorithms.Algorithms
{
    public class BubbleSort<T> : BaseAlgorithm<T> where T : IComparable
    {
        public BubbleSort(IEnumerable<T> items) : base(items) { }
        public BubbleSort() { }
        protected override void DoSort()
        {
            var count = Items.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count - 1 - i; j++)
                {
                    var a = Items[j];
                    var b = Items[j + 1];
                    if (Compare(a, b) == 1)
                    {
                        Swap(j, j + 1);
                    }
                }
            }
        }
    }
}
