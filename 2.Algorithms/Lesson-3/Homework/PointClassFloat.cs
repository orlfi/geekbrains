using System;
using System.Collections.Generic;
using System.Text;

namespace Homework
{
    public class PointClassFloat
    {
        public float X;
        public float Y;

        public PointClassFloat(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static float PointDistance(PointClassFloat pointOne, PointClassFloat pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }
    }
}
