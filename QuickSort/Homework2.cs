using System;
using System.Linq;
using Utilities;

namespace QuickSort
{
    class Homework2
    {
        static int Main(string[] args)
        {
            var integers = Files.ReadFileToCollection<int>(
                $"{AppDomain.CurrentDomain.BaseDirectory}/../../../Blobs/QuickSort.txt").ToList();
            Console.WriteLine("First Pivot: {0}", QuickSort.SortPivotFirst(integers.Copy())); // 162085
            Console.WriteLine("Last Pivot: {0}", QuickSort.SortPivotLast(integers.Copy())); // 164123
            Console.WriteLine("Median Pivot: {0}", QuickSort.SortPivotMedian(integers.Copy())); // 149130
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
            return 0;
        }
    }
}
