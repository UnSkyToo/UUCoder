using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeBase
{
    /// <summary>
    /// 大小
    /// </summary>
    public struct USize
    {
        public static USize Zero = new USize(0, 0);

        public int Width;
        public int Height;

        public USize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public override string ToString()
        {
            return "{" + Width.ToString() + "," + Height.ToString() + "}";
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() + Height.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is USize))
            {
                return false;
            }

            USize size = (USize)obj;

            if (object.Equals(size, null))
            {
                return false;
            }

            return Width == size.Width && Height == size.Height;
        }

        public static bool operator ==(USize size1, USize size2)
        {
            if (object.Equals(size1, null) || object.Equals(size2, null))
            {
                return object.Equals(size1, size2);
            }

            return size1.Width == size2.Width && size1.Height == size2.Height;
        }

        public static bool operator !=(USize size1, USize size2)
        {
            if (object.Equals(size1, null) || object.Equals(size2, null))
            {
                return !object.Equals(size1, size2);
            }

            return size1.Width != size2.Width || size1.Height != size2.Height;
        }
    }
}
