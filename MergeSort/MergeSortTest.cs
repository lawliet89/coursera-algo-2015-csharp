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
    }
}
