using System;
using System.Collections.Generic;
using System.Text;

namespace Homework
{
    public struct PointStructDouble
    {
        public double X;
        public double Y;

        public PointStructDouble(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static double PointDistance(PointStructDouble pointOne, PointStructDouble pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }

    }
}
