using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeBase
{
    public enum UMouseButton
    {
        None = 0,
        /// <summary>
        /// 鼠标左键
        /// </summary>
        Left = 1,
        /// <summary>
        /// 鼠标中键
        /// </summary>
        Middle = 2,
        /// <summary>
        /// 鼠标右键
        /// </summary>
        Right = 4,
    }

    public class UMouseEvents
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public UMouseButton Button { get; private set; }

        public UMouseEvents(UMouseButton button, int x, int y)
        {
            this.Button = button;
            this.X = x;
            this.Y = y;
        }
    }
}
