using System;
using NUnit.Framework;

namespace ClosestPair
{
    [TestFixture]
    public class ClosestPairTest
    {
        public static CloestPairTestData[] TestData =
        {
            new CloestPairTestData
            {
                Points = new[]
                {
                    Point.Create(0, 0),
                    Point.Create(0, 1),
                    Point.Create(100, 45),
                    Point.Create(2, 2),
                    Point.Create(9, 9)
                },
                CloestPair = Tuple.Create(Point.Create(0, 0), Point.Create(0, 1))
            },
            new CloestPairTestData
            {
                Points = new[]
                {
                    Point.Create(0, 0),
                    Point.Create(-4, 1),
                    Point.Create(-7, -2),
                    Point.Create(4, 5),
                    Point.Create(1, 1)
                },
                CloestPair = Tuple.Create(Point.Create(0, 0), Point.Create(1, 1))
            }
        };

        [TestCaseSource(nameof(TestData))]
        public void ReturnsClosestPair(CloestPairTestData testData)
        {
            var actualCloest = ClosestPair.CloestPair(testData.Points);
            CollectionAssert.AreEquivalent(testData.CloestPair.ToEnumerable(), actualCloest.ToEnumerable());
        }
    }

    public struct CloestPairTestData
    {
        public Point[] Points;
        public Tuple<Point, Point> CloestPair;
    }
}
