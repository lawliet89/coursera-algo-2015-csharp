using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace TwoSum
{
    public static class TwoSum
    {
        public static void Main(string[] args)
        {
            var data = ReadInput($"{AppDomain.CurrentDomain.BaseDirectory}/../../../Blobs/TwoSum.txt");
            var targets = TargetValues(data, -10000, 10000);
            Console.WriteLine("Answer: {0}", targets.Count());
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        public static SortedDictionary<long, long> ReadInput(string path)
        {
            return new SortedDictionary<long, long>(Files.ReadFileToCollection<long>(path)
                .Distinct()
                .ToDictionary(v => v, v => v));
        }

        public static IEnumerable<long> TargetValues(SortedDictionary<long, long> data, long lowerBound, long upperBound)
        {
            if (upperBound <= lowerBound)
                throw new ArgumentException("Lower bound must be strictly smaller than upper bound");
            var smallest = data.First().Value;
            var largest = data.Last().Value;
            var result = new HashSet<long>();
            foreach (var a in data.Keys)
            {
                for (var target = lowerBound; target <= upperBound; ++target)
                {
                    var b = target - a;
                    if (b > largest || b < smallest)
                    {
                        continue;
                    }
                    if (b != a && data.ContainsKey(b))
                    {
                        result.Add(target);
                    }
                }
            }

            return result;
        } 
    }
}
