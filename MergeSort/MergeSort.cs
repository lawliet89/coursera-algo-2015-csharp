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
            var size = list.Count();

            if (size < 2)
                return list;

            var left = list.Take(size/2);
            var right = list.Skip(size/2);

            var sortedLeft = Sort(left);
            var sortedRight = Sort(right);
            return Merge(sortedLeft, sortedRight);
        }

        private static IEnumerable<T> Merge<T>(IEnumerable<T> left, IEnumerable<T> right)
            where T : IComparable<T>
        {
            var leftList = left as IList<T> ?? left.ToList();
            var rightList = right as IList<T> ?? right.ToList();
            var result = new List<T>(leftList.Count() + rightList.Count());

            using (IEnumerator<T> leftIterator = leftList.GetEnumerator(), rightIterator = rightList.GetEnumerator())
            {
                var leftHasNext = leftIterator.MoveNext();
                var rightHasNext = rightIterator.MoveNext();
                while (leftHasNext || rightHasNext)
                {
                    if (!leftHasNext)
                    {
                        result.Add(rightIterator.Current);
                        rightHasNext = rightIterator.MoveNext();
                    }
                    else if (!rightHasNext)
                    {
                        result.Add(leftIterator.Current);
                        leftHasNext = leftIterator.MoveNext();
                    }
                    // i.e. left <= right
                    else if (leftIterator.Current.CompareTo(rightIterator.Current) <= 0)
                    {
                        result.Add(leftIterator.Current);
                        leftHasNext = leftIterator.MoveNext();
                    }
                    else
                    {
                        result.Add(rightIterator.Current);
                        rightHasNext = rightIterator.MoveNext();
                    }
                }
            }
            return result;
        }
    }
}
