using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;

using UUCoder.CodeBase;
using UUCoder.CodeRender;

namespace UUCoder
{
    public class GDIRenderer : IRenderer
    {
        public Bitmap Screen
        {
            get
            {
                return screen;
            }
        }

        private Graphics g;
        private Bitmap screen;
        private Font font;

        private Pen dashPen;

        public GDIRenderer(int width, int height)
        {
            this.screen = new Bitmap(width, height);
            this.g = Graphics.FromImage(screen);

            this.font = new Font("新宋体", 10);

            this.dashPen = new Pen(Color.Gray, 1);
            this.dashPen.DashStyle = DashStyle.Custom;
            this.dashPen.DashPattern = new float[] { 2f, 2f };

            base.RenderWidth = width;
            base.RenderHeight = height;

            SizeF ss = g.MeasureString("1", font);

            /*base.CharWidth = 6.875f;
            base.CharHeight = 16;
            base.WordWidth = base.CharWidth * 2;
            base.WordHeight = 16;*/
            base.CharWidth = ss.Width / 1.646f;
            base.CharHeight = ss.Height + 1;
            base.WordWidth = base.CharWidth * 2;
            base.WordHeight = base.CharHeight;
        }

        public override void Clear(UColor backColor)
        {
            g.Clear(ParseColor(backColor));
        }

        public override void DrawLine(UPoint startPos, UPoint endPos, UColor color)
        {
            g.DrawLine(new Pen(ParseColor(color)), startPos.X, startPos.Y, endPos.X, endPos.Y);
        }

        public override void DrawDash(UPoint startPos, UPoint endPos, UColor color)
        {
            dashPen.Color = ParseColor(color);

            g.DrawLine(dashPen, new PointF(startPos.X, startPos.Y), new PointF(endPos.X, endPos.Y));
        }

        public override void DrawRect(URect rect, UColor color, bool fill)
        {
            if (fill)
            {
                g.FillRectangle(new SolidBrush(ParseColor(color)), rect.X, rect.Y, rect.Width, rect.Height);
            }
            else
            {
                g.DrawRectangle(new Pen(ParseColor(color)), rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        public override void DrawText(float x, float y, string text, UColor color)
        {
            g.DrawString(text, font, new SolidBrush(ParseColor(color)), x, y);
        }

        private Color ParseColor(UColor color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public void SetFont(Font f)
        {
            this.font = f;

            SizeF ss = g.MeasureString("1", font);

            base.CharWidth = ss.Width / 1.646f;
            base.CharHeight = ss.Height + 1;
            base.WordWidth = base.CharWidth * 2;
            base.WordHeight = base.CharHeight;
        }

        public void SetSize(int width, int height)
        {
            this.screen = new Bitmap(width, height);
            this.g = Graphics.FromImage(screen);
            base.RenderWidth = width;
            base.RenderHeight = height;
        }
    }
}
