using System.Collections.Generic;
using NUnit.Framework;

namespace QuickSort
{
    [TestFixture]
    public class QuickSortTest
    {
        public static IList<int>[] Lists =
        {
            new []{3, 4, 2, 1, 7, 5, 8, 9, 0, 6},
            new []{ 5, 2, 10, 8, 1, 9, 4, 3, 6, 7 }
        };

        [Test, TestCaseSource(nameof(Lists))]
        public void SortByFirstPivot(IList<int> list)
        {
            var actualList = new List<int>(list);
            QuickSort.SortPivotFirst(actualList);
            CollectionAssert.IsOrdered(actualList);
        }

        [Test, TestCaseSource(nameof(Lists))]
        public void SortByLastPivot(IList<int> list)
        {
            var actualList = new List<int>(list);
            QuickSort.SortPivotLast(actualList);
            CollectionAssert.IsOrdered(actualList);
        }

        [Test, TestCaseSource(nameof(Lists))]
        public void SortByMedianPivot(IList<int> list)
        {
            var actualList = new List<int>(list);
            QuickSort.SortPivotMedian(actualList);
            CollectionAssert.IsOrdered(actualList);
        }
    }
}
