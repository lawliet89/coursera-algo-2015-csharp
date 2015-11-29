using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utilities;

namespace TwoSum
{
    [TestFixture]
    public class TwoSumTest
    {
        [TestCase(new long[] { 10000, -10000, 5464564, 134, 1344, 899, 0, 1 }, Result = 17)]
        [TestCase(new long[] { 0, 1, 10, 10, -10, 10000, -10000, 10001, 9999, -10001, -9999, 10000, 5000, 5000, -5000, -1, 1000, 2000, -1000, -2000  }, Result= 73)]
        public int CorrectNumberOfTargets(IEnumerable<long> inputs)
        {
            var dictionary = new SortedDictionary<long, long>(inputs.Distinct().ToDictionary(v => v, v => v));
            return TwoSum.TargetValues(dictionary, -10000, 10000).Count();
        }

        [TestCase("100", Result = 42)]
        [TestCase("1000", Result = 486)]
        [TestCase("10000", Result = 496)]
//        [TestCase("100000", Result = 519)]
        public int CorrectNumberOfTargetsForBlobFixtures(string filename)
        {
            var dictionary =
                new SortedDictionary<long, long>(Files.ReadFileToCollection<long>(
                    $"{AppDomain.CurrentDomain.BaseDirectory}/../../../Blobs/Fixtures/TwoSum/{filename}.txt")
                    .Distinct()
                    .ToDictionary(v => v, v => v));
            return TwoSum.TargetValues(dictionary, -10000, 10000).Count();
        }
    }
}
