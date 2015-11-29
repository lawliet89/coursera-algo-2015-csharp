using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace MedianMaintenance
{
    public static class MedianMaintenance
    {
        public static void Main(string[] args)
        {
            var data = ReadFile($"{AppDomain.CurrentDomain.BaseDirectory}/../../../Blobs/Median.txt");
            var medians = RollingMedian(data);
            var sum = medians.Select(Convert.ToInt64)
                .Sum();
            var answer = sum%10000;
            Console.WriteLine("Answer: {0}", answer); // 1213
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        public static IEnumerable<int> ReadFile(string path)
        {
            return Files.ReadFileToCollection<int>(path);
        }

        public static IEnumerable<int> RollingMedian(IEnumerable<int> numbers)
        {
            var result = new List<int>();
            var sortedNumbers = new SortedList<int, int>(new DuplicateKeyComparer<int>());
            foreach (var number in numbers)
            {
                sortedNumbers.Add(number, number);
                var size = sortedNumbers.Count;
                var medianIndex = 0;
                if (size%2 == 0) // even
                {
                    medianIndex = size/2 - 1;
                }
                else
                {
                    medianIndex = (size + 1)/2 - 1;
                }
                result.Add(sortedNumbers.ElementAt(medianIndex).Value);
            }
            return result;
        }  
    }
}
