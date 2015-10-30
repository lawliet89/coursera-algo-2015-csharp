using System;
using System.Collections.Generic;
using Utilities;

namespace QuickSort
{
    public static class QuickSort
    {
        private enum PivotChoice
        {
            First, Last, Median, Random
        }

        public static long SortPivotFirst<T>(IList<T> list)
            where T : IComparable<T>
        {
            return Sort(list, PivotChoice.First, 0, list.Count);
        }

        public static long SortPivotLast<T>(IList<T> list)
            where T : IComparable<T>
        {
            return Sort(list, PivotChoice.Last, 0, list.Count);
        }

        public static long SortPivotMedian<T>(IList<T> list)
            where T : IComparable<T>
        {
            return Sort(list, PivotChoice.Median, 0, list.Count);
        }

        public static long SortPivotRandom<T>(IList<T> list)
            where T : IComparable<T>
        {
            return Sort(list, PivotChoice.Random, 0, list.Count);
        }

        // Returns the number of comparisons
        private static long Sort<T>(IList<T> list, PivotChoice pivotChoice, int firstIndex, int lastIndex)
            where T : IComparable<T>
        {
            var length = lastIndex - firstIndex;
            if (length < 2)
                return 0;

            var pivotIndex = firstIndex;
            switch (pivotChoice)
            {
                case PivotChoice.Last:
                    pivotIndex = lastIndex - 1;
                    break;
                case PivotChoice.Median:
                    pivotIndex = firstIndex + Convert.ToInt32(Math.Round(length/2.0));
                    break;
                case PivotChoice.Random:
                    // Unimplemented
                    break;
            }

            if (pivotIndex != firstIndex)
            {
                list = list.Swap(pivotIndex, firstIndex);
            }

            var pivot = list[firstIndex];
            var i = 1;
            for (var j = 1; j < length; ++j)
            {
                if (list[firstIndex + j].CompareTo(pivot) < 1)
                {
                    list.Swap(firstIndex + j, firstIndex + i);
                    ++i;
                }
            }

            list.Swap(firstIndex, firstIndex + i - 1);
            var count = (long) length - 1;
            count += Sort(list, pivotChoice, firstIndex, firstIndex + i - 1);
            count += Sort(list, pivotChoice, firstIndex + i, lastIndex);

            return count;
        }
    }
}
