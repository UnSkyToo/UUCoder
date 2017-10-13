using System;
using System.Collections.Generic;
using System.Text;

using UUCoder.CodeBase;

namespace UUCoder.CodeRender
{
    public abstract class IRenderer
    {
        /// <summary>
        /// 字母的宽度
        /// </summary>
        public float CharWidth { get; protected set; }
        /// <summary>
        /// 字母的高度
        /// </summary>
        public float CharHeight { get; protected set; }
        /// <summary>
        /// 汉字的宽度
        /// </summary>
        public float WordWidth { get; protected set; }
        /// <summary>
        /// 汉字的高度
        /// </summary>
        public float WordHeight { get; protected set; }

        /// <summary>
        /// 渲染区域的宽度
        /// </summary>
        public int RenderWidth { get; protected set; }

        /// <summary>
        /// 渲染区域的高度
        /// </summary>
        public int RenderHeight { get; protected set; }

        /// <summary>
        /// 清除区域为背景色
        /// </summary>
        /// <param name="backColor"></param>
        public abstract void Clear(UColor backColor);

        /// <summary>
        /// 画直线
        /// </summary>
        /// <param name="startPos">开始位置</param>
        /// <param name="endPos">结束位置</param>
        /// <param name="color">颜色</param>
        public abstract void DrawLine(UPoint startPos, UPoint endPos, UColor color);

        /// <summary>
        /// 画虚线
        /// </summary>
        /// <param name="startPos">开始位置</param>
        /// <param name="endPos">结束位置</param>
        /// <param name="color">颜色</param>
        public abstract void DrawDash(UPoint startPos, UPoint endPos, UColor color);

        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="rect">大小</param>
        /// <param name="color">颜色</param>
        /// <param name="fill">是否填充</param>
        public abstract void DrawRect(URect rect, UColor color, bool fill);

        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="text">文字</param>
        /// <param name="color">颜色</param>
        public abstract void DrawText(float x, float y, string text, UColor color);
    }
}
