using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeSort
{
    public class HomeWork1
    {
        public static int Main(string[] args)
        {
            var integers = Utilities.Files.ReadFileToCollection<int>(
                $"{AppDomain.CurrentDomain.BaseDirectory}/../../../Blobs/IntegerArray.txt");
            var list = integers as IList<int> ?? integers.ToList();
            Console.WriteLine("{0} integers read", list.Count());
            Console.WriteLine("{0} inversions counted", MergeSort<int>.CountInversions(list));
            // Answer: 2407905288
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
            return 0;
        }
    }
}
