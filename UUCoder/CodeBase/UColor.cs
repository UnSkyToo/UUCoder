using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeBase
{
    /// <summary>
    /// 颜色类
    /// </summary>
    public class UColor
    {
        /// <summary>
        /// 白色（255,255,255）
        /// </summary>
        public static UColor White = new UColor(255, 255, 255);
        /// <summary>
        /// 黑色（0,0,0）
        /// </summary>
        public static UColor Black = new UColor(0, 0, 0);
        /// <summary>
        /// 红色（255,0,0）
        /// </summary>
        public static UColor Red = new UColor(255, 0, 0);
        /// <summary>
        /// 绿色（0,255,0）
        /// </summary>
        public static UColor Green = new UColor(0, 255, 0);
        /// <summary>
        /// 蓝色（0,0,255）
        /// </summary>
        public static UColor Blue = new UColor(0, 0, 255);

        /// <summary>
        /// 黄色（255,255,0）
        /// </summary>
        public static UColor Yellow = new UColor(255, 255, 0);
        /// <summary>
        /// 紫色（255,0,255）
        /// </summary>
        public static UColor Purple = new UColor(255, 0, 255);
        /// <summary>
        /// 蓝绿色（0,255,255）
        /// </summary>
        public static UColor Cyan = new UColor(0, 255, 255);

        /// <summary>
        /// 金色（255,215,0）
        /// </summary>
        public static UColor Gold = new UColor(255, 215, 0);
        /// <summary>
        /// 灰色（128,128,128）
        /// </summary>
        public static UColor Gray = new UColor(128, 128, 128);
        /// <summary>
        /// 橘黄色（255,165,0）
        /// </summary>
        public static UColor Orange = new UColor(255, 165, 0);

        public int A;
        public int R;
        public int G;
        public int B;


        public UColor(int r, int g, int b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = 255;
        }

        public UColor(int r, int g, int b, int a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public static UColor Parse(string s)
        {
            try
            {
                string[] ss = s.Split(',');

                return new UColor(int.Parse(ss[0]), int.Parse(ss[1]), int.Parse(ss[2]));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
