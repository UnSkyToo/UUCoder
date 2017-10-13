using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeBase
{
    /// <summary>
    /// 行列
    /// </summary>
    public struct URank
    {
        public static URank Zero = new URank(0, 0);
        public static URank None = new URank(-1, -1);

        /// <summary>
        /// 行
        /// </summary>
        public int Row;
        /// <summary>
        /// 列
        /// </summary>
        public int Col;

        public URank(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override string ToString()
        {
            return "{" + Row.ToString() + "," + Col.ToString() + "}";
        }

        public override int GetHashCode()
        {
            return Row.GetHashCode() + Col.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is URank))
            {
                return false;
            }

            URank rank = (URank)obj;

            if (object.Equals(rank, null))
            {
                return false;
            }

            return Row == rank.Row && Col == rank.Col;
        }

        public static bool operator ==(URank rank1, URank rank2)
        {
            if (object.Equals(rank1, null) || object.Equals(rank2, null))
            {
                return object.Equals(rank1, rank2);
            }

            return rank1.Row == rank2.Row && rank1.Col == rank2.Col;
        }

        public static bool operator !=(URank rank1, URank rank2)
        {
            if (object.Equals(rank1, null) || object.Equals(rank2, null))
            {
                return !object.Equals(rank1, rank2);
            }

            return rank1.Row != rank2.Row || rank1.Col != rank2.Col;
        }

        public static bool operator >(URank rank1, URank rank2)
        {
            if (object.Equals(rank1, null) || object.Equals(rank2, null))
            {
                return false;
            }

            if (rank1.Row == rank2.Row)
            {
                return rank1.Col > rank2.Col;
            }

            return rank1.Row > rank2.Row;
        }

        public static bool operator <(URank rank1, URank rank2)
        {
            if (object.Equals(rank1, null) || object.Equals(rank2, null))
            {
                return false;
            }

            if (rank1.Row == rank2.Row)
            {
                return rank1.Col < rank2.Col;
            }

            return rank1.Row < rank2.Row;
        }

        public static bool operator >=(URank rank1, URank rank2)
        {
            if (object.Equals(rank1, null) || object.Equals(rank2, null))
            {
                return false;
            }

            if (rank1.Row == rank2.Row)
            {
                return rank1.Col >= rank2.Col;
            }

            return rank1.Row > rank2.Row;
        }
        public static bool operator <=(URank rank1, URank rank2)
        {
            if (object.Equals(rank1, null) || object.Equals(rank2, null))
            {
                return false;
            }

            if (rank1.Row == rank2.Row)
            {
                return rank1.Col <= rank2.Col;
            }

            return rank1.Row < rank2.Row;
        }

    }
}
