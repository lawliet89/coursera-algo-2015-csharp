using System.Collections.Generic;
using NUnit.Framework;

namespace MergeSort
{
    [TestFixture]
    public class MergeSortTest
    {
        [Test]
        public void SortBaseCase()
        {
            CollectionAssert.IsOrdered(MergeSort<int>.Sort(new[] { 1 }));
        }

        [Test]
        public void SortCorrectly()
        {
            CollectionAssert.IsOrdered(MergeSort<int>.Sort(new[] { 3, 4, 2, 1, 7, 5, 8, 9, 0, 6}));
        }

        [Test]
        public void MergesAndCountsSplitInversionsCorrectly()
        {
            var left = new[] {1, 3, 5};
            var right = new[] {2, 4, 6};

            long splitInversionCount;
            var merged = MergeSort<int>.Merge(left, right, out splitInversionCount);

            CollectionAssert.IsOrdered(merged);
            Assert.AreEqual(3, splitInversionCount);
        }

        [TestCase(new[] { 2 }, Result = 0)]
        [TestCase(new[] { 1, 2 }, Result = 0)]
        [TestCase(new[] { 3, 2, 1 }, Result = 3)]
        [TestCase(new[] { 0, 0, 0, 0 }, Result = 0)]
        [TestCase(new[] { 1, 2, 3, 5, 4 }, Result = 1)]
        [TestCase(new[] { 3, 1, 6, 5, 2, 4 }, Result = 7)]
        [TestCase(new[] { 5, 2, 10, 8, 1, 9, 4, 3, 6, 7 }, Result = 22)]
        public long SortsAndCountsInversionsCorrectly(int[] numbers)
        {
            IEnumerable<int> sorted;
            var inversions = MergeSort<int>.CountInversions(numbers, out sorted);

            CollectionAssert.IsOrdered(sorted);
            return inversions;
        }
    }
}
