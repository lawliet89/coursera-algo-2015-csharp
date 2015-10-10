using System;
using System.Collections.Generic;

namespace ClosestPair
{
    // Just a wrapper around Tuple<double, double>
    public class Point : Tuple<double, double>
    {
        public Point(double x, double y) : base(x, y)
        {
        }

        public double X => Item1;
        public double Y => Item2;

        public static Point Create(double x, double y)
        {
            return new Point(x, y);
        }

        public double Distance(Point p2)
        {
            return Distance(this, p2);
        }

        public static double Distance(Tuple<Point, Point> points)
        {
            return points == null ? double.PositiveInfinity : Distance(points.Item1, points.Item2);
        }

        public static double Distance(Point p1, Point p2)
        {
            if (p1 == null || p2 == null)
            {
                return double.PositiveInfinity;;
            }
            return Math.Sqrt(((p1.X*p1.X) - (p2.X*p2.X)) + ((p1.Y*p1.Y) - (p2.Y*p2.Y)));
        }
    }

    public static class TupleExtensions
    {
        public static double Distance(this Tuple<Point, Point> pair)
        {
            return Point.Distance(pair);
        }

        public static IEnumerable<Point> ToEnumerable(this Tuple<Point, Point> pair)
        {
            yield return pair.Item1;
            yield return pair.Item2;
        } 
    }
}
