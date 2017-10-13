using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeBase
{
    /// <summary>
    /// 点
    /// </summary>
    public struct UPoint
    {
        public static UPoint Zero = new UPoint(0, 0);

        public int X;
        public int Y;

        public UPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "{" + X.ToString() + "," + Y.ToString() + "}";
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UPoint))
            {
                return false;
            }

            UPoint point = (UPoint)obj;

            if (object.Equals(point, null))
            {
                return false;
            }

            return X == point.X && Y == point.Y;
        }

        public static bool operator ==(UPoint point1, UPoint point2)
        {
            if (object.Equals(point1, null) || object.Equals(point2, null))
            {
                return object.Equals(point1, point2);
            }

            return point1.X == point2.X && point1.Y == point2.Y;
        }

        public static bool operator !=(UPoint point1, UPoint point2)
        {
            if (object.Equals(point1, null) || object.Equals(point2, null))
            {
                return !object.Equals(point1, point2);
            }

            return point1.X != point2.X || point1.Y != point2.Y;
        }
    }
}
