using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MergeSort
{
    [TestFixture]
    public class MergeSortTest
    {
        [Test]
        public void SortBaseCase()
        {
            CollectionAssert.IsOrdered(MergeSort.Sort(new[] { 1 }));
        }

        [Test]
        public void SortCorrectly()
        {
            CollectionAssert.IsOrdered(MergeSort.Sort(new[] { 3, 4, 2, 1, 7, 5, 8, 9, 0, 6}));
        }

        [Test]
        public void MergesAndCountsSplitInversionsCorrectly()
        {
            var left = new[] {1, 3, 5};
            var right = new[] {2, 4, 6};

            int splitInversionCount;
            var merged = MergeSort.Merge(left, right, out splitInversionCount);

            CollectionAssert.IsOrdered(merged);
            Assert.AreEqual(3, splitInversionCount);
        }
    }
}
