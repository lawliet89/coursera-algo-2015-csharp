using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeSort
{
    public static class MergeSort
    {
        public static IEnumerable<T> Sort<T>(IEnumerable<T> numbers)
            where T: IComparable<T>
        {
            var list = numbers as IList<T> ?? numbers.ToList();
            var size = list.Count;

            if (size < 2)
                return list;

            var left = list.Take(size/2);
            var right = list.Skip(size/2);

            var sortedLeft = Sort(left);
            var sortedRight = Sort(right);
            return Merge(sortedLeft, sortedRight);
        }

        public static IEnumerable<T> Merge<T>(IEnumerable<T> left, IEnumerable<T> right)
            where T : IComparable<T>
        {
            int _; // We are throwing this away
            return Merge(left, right, out _);
        }

        public static IEnumerable<T> Merge<T>(IEnumerable<T> left, IEnumerable<T> right, out int splitInversionCount)
            where T : IComparable<T>
        {
            splitInversionCount = 0;

            var leftList = left as IList<T> ?? left.ToList();
            var rightList = right as IList<T> ?? right.ToList();
            var leftCount = leftList.Count;
            var result = new List<T>(leftCount + rightList.Count);

            using (IEnumerator<T> leftIterator = leftList.GetEnumerator(), rightIterator = rightList.GetEnumerator())
            {
                var leftHasNext = leftIterator.MoveNext();
                var rightHasNext = rightIterator.MoveNext();
                var leftIndex = 0;
                while (leftHasNext && rightHasNext)
                {
                    // i.e. left <= right
                    if (leftIterator.Current.CompareTo(rightIterator.Current) <= 0)
                    {
                        result.Add(leftIterator.Current);
                        leftHasNext = leftIterator.MoveNext();
                        leftIndex++;
                    }
                    else // i.e. left > right and we have split inversion
                    {
                        splitInversionCount += (leftCount - leftIndex);
                        result.Add(rightIterator.Current);
                        rightHasNext = rightIterator.MoveNext();
                    }
                }

                if (leftHasNext || rightHasNext)
                {
                    var hasNext = true;
                    var iterator = leftHasNext ? leftIterator : rightIterator;

                    while (hasNext)
                    {
                        result.Add(iterator.Current);
                        hasNext = iterator.MoveNext();
                    }
                }
            }
            return result;
        }
    }
}
