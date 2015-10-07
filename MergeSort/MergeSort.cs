using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MergeSort
{
    public static class MergeSort<T> where T : IComparable<T>
    {
        public static IEnumerable<T> Sort(IEnumerable<T> numbers)
        {
            IEnumerable<T> sorted;
            CountInversions(numbers, out sorted);
            return sorted;
        }

        public static int CountInversions(IEnumerable<T> numbers)
        {
            IEnumerable<T> _; // We are going to throw this away
            return CountInversions(numbers, out _);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static int CountInversions(IEnumerable<T> numbers, out IEnumerable<T> sortedNumbers)
        {
            var list = numbers as IList<T> ?? numbers.ToList();
            var size = list.Count;

            if (size < 2)
            {
                sortedNumbers = numbers;
                return 0;
            }

            var left = list.Take(size/2);
            var right = list.Skip(size/2);

            IEnumerable<T> sortedLeft, sortedRight;
            var leftInversion = CountInversions(left, out sortedLeft);
            var rightInversion = CountInversions(right, out sortedRight);
            int splitInversion;
            sortedNumbers = Merge(sortedLeft, sortedRight, out splitInversion);

            return leftInversion + rightInversion + splitInversion;
        }

        public static IEnumerable<T> Merge(IEnumerable<T> left, IEnumerable<T> right)
        {
            int _; // We are throwing this away
            return Merge(left, right, out _);
        }

        public static IEnumerable<T> Merge(IEnumerable<T> left, IEnumerable<T> right, out int splitInversionCount)
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
