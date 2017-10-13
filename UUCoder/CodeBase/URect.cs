using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeBase
{
    public struct URect
    {
        public static URect Zero = new URect(0, 0, 0, 0);

        public int X;
        public int Y;
        public int Width;
        public int Height;

        public int Top
        {
            get
            {
                return Y;
            }
        }

        public int Bottom
        {
            get
            {
                return Y + Height;
            }
        }

        public int Left
        {
            get
            {
                return X;
            }
        }

        public int Right
        {
            get
            {
                return X + Width;
            }
        }

        public URect(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public URect(UPoint point, USize size)
        {
            this.X = (int)point.X;
            this.Y = (int)point.Y;
            this.Width = (int)size.Width;
            this.Height = (int)size.Height;
        }

        public override string ToString()
        {
            return "{" + X.ToString() + "," + Y.ToString() + "," + Width.ToString() + "," + Height.ToString() + "}";
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Width.GetHashCode() + Height.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is URect))
            {
                return false;
            }

            URect rect = (URect)obj;

            if (object.Equals(rect, null))
            {
                return false;
            }

            if (X == rect.X && Y == rect.Y)
            {
                return Width == rect.Width && Height == rect.Height;
            }

            return false;
        }

        public static bool operator ==(URect rect1, URect rect2)
        {
            if (object.Equals(rect1, null) || object.Equals(rect2, null))
            {
                return object.Equals(rect1, rect2);
            }

            if (rect1.X == rect2.X && rect1.Y == rect2.Y)
            {
                return rect1.Width == rect2.Width && rect1.Height == rect2.Height;
            }

            return false;
        }

        public static bool operator !=(URect rect1, URect rect2)
        {
            if (object.Equals(rect1, null) || object.Equals(rect2, null))
            {
                return !object.Equals(rect1, rect2);
            }

            return rect1.X != rect2.X || rect1.Y != rect2.Y || rect1.Width != rect2.Width || rect1.Height != rect2.Height;
        }
    }
}
