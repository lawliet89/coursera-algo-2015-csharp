using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosestPair
{
    public static class ClosestPair
    {
        public static Tuple<Point, Point> CloestPair(IEnumerable<Point> points)
        {
            var pointsList = points as IList<Point> ?? points.ToList();

            if (pointsList.Count < 4)
            {
                return NaiveClosest(pointsList);
            }

            var px = pointsList.OrderBy(point => point.X).ToList();
            var py = pointsList.OrderBy(point => point.Y).ToList();

            return CloestPair(px, py);
        }

        private static Tuple<Point, Point> CloestPair(IList<Point> px, IList<Point> py)
        {
            if (px.Count != py.Count)
            {
                throw new ArgumentException("Px and Py must be of the length");
            }

            if (px.Count < 4)
            {
                return NaiveClosest(px);
            }

            var qx = px.Take(px.Count/2).ToList();
            var rx = px.Skip(px.Count/2).ToList();

            var qy = py.Take(py.Count/2).ToList();
            var ry = py.Skip(py.Count/2).ToList();

            var leftClosest = CloestPair(qx, qy);
            var rightCloset = CloestPair(rx, ry);

            var delta = Math.Min(leftClosest.Distance(), rightCloset.Distance());

            var xBar = qx.Last().X;
            var sy = py.Where(point => Math.Abs(point.X - xBar) < delta).ToList();

            var bestDistance = delta;
            Tuple<Point, Point> bestSplitPair = null;

            for (var i = 0; i < sy.Count - 7; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    var p = sy[i];
                    var q = sy[i + j];
                    var distance = Point.Distance(p, q);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestSplitPair = Tuple.Create(p, q);
                    }
                }
            }

            return new[] {leftClosest, rightCloset, bestSplitPair}
                .OrderBy(pair => pair.Distance()).First();
        }

        private static Tuple<Point, Point> NaiveClosest(IEnumerable<Point> points)
        {
            var pointsList = points as IList<Point> ?? points.ToList();
            if (pointsList.Count > 3)
            {
                throw new ArgumentException("Use this only with legnth < 4");
            }

            Tuple<Point, Point> bestPair = null;
            var bestDistance = double.PositiveInfinity;
            foreach (var a in pointsList)
            {
                foreach (var b in pointsList)
                {
                    var distance = Point.Distance(a, b);
                    if (distance < bestDistance && a != b)
                    {
                        bestDistance = distance;
                        bestPair = Tuple.Create(a, b);
                    }
                }
            }
            return bestPair;
        }
    }
}
