using System;
using System.Collections.Generic;
using System.Text;

namespace Homework
{
    public struct PointStructFloat
    {
        public float X;
        public float Y;

        public PointStructFloat(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static float PointDistance(PointStructFloat pointOne, PointStructFloat pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }

        public static float PointDistanceShort(PointStructFloat pointOne, PointStructFloat pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return (x * x) + (y * y);
        }
    }
}
