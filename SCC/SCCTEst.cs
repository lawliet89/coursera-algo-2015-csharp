using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SCC
{
    [TestFixture]
    public class SCCTest
    {
        [TestCase("1.txt", new[] {3, 3, 3})]
        [TestCase("2.txt", new[] { 3, 3, 2 })]
        [TestCase("3.txt", new[] { 6, 3, 2, 1 })]
        public void CountsNumberOfSCCCorrectly(string filename, IEnumerable<int> expectedSizes)
        {
            var graph = SCC.MakeGraphFromFile(
                        $"{AppDomain.CurrentDomain.BaseDirectory}/../../../Blobs/Fixtures/SCC/{filename}");
            var actualSizes = SCC.TopFiveSCCSize(graph);
            CollectionAssert.AreEqual(expectedSizes, actualSizes);
        }
    }
}
