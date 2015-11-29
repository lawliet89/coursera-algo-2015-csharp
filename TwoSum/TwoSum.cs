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

        public static IEnumerable<long> TargetValues(IDictionary<long, long> data, long lowerBound, long upperBound)
        {
            if (upperBound <= lowerBound)
                throw new ArgumentException("Lower bound must be strictly smaller than upper bound");
            var result = new List<long>();
            foreach (var a in data.Keys)
            {
                for (var i = lowerBound; i <= upperBound; ++i)
                {
                    var target = lowerBound - a;
                    if (target != a && data.ContainsKey(target))
                    {
                        result.Add(i);
                    }
                }
            }

            return result;
        } 
    }
}
