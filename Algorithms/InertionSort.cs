using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuresAndAlgorithms.Algorithms
{
    class InsertionSort<T> : BaseAlgorithm<T> where T: IComparable
    {
        public InsertionSort() { }
        public InsertionSort(IEnumerable<T> items) : base(items) { }

        protected override void DoSort()
        {
            for (int i = 1; i < Items.Count; i++)
            {
                var temp = Items[i];
                int j = i;
                while (j > 0 && Compare(temp, Items[j - 1]) == -1)
                {
                    Swap(j, j - 1);
                    j--;
                }
                Items[j] = temp;
            }
        }
    }
}
