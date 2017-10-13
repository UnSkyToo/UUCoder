using System;
using System.Collections.Generic;
using System.Text;

using UUCoder.CodeBase;

namespace UUCoder.CodePlugin
{
    public class USelection : IPlugin
    {
        public URank Start { get; private set; }
        public URank End { get; private set; }

        /// <summary>
        /// 是否存在选择区域
        /// </summary>
        public bool HasSelection
        {
            get
            {
                return !(Start == URank.None || End == URank.None);
            }
        }

        public USelection(UUCoder coder)
            : base(coder)
        {
        }

        public override void Initialize()
        {
            Reset();
        }

        public override void Reset()
        {
            Start = URank.None;
            End = URank.None;
        }

        public void SetStart(URank start)
        {
            this.Start = start;
        }

        public void SetEnd(URank end)
        {
            this.End = end;
        }

        public override void Render(CodeRender.IRenderer renderer)
        {
            int startRow = Start.Row;
            int endRow = End.Row;

            int startCol = Start.Col;
            int endCol = End.Col;

            float WordWidth = Coder.Renderer.CharWidth;
            float WordHeight = Coder.Renderer.CharHeight;

            UColor color = UColor.Orange;

            if (Start == End)
            {
                return;
            }

            if (Start == URank.None || End == URank.None)
            {
                return;
            }

            if (endRow < Coder.CodeRenderer.RenderStartRow ||
                startRow > Coder.CodeRenderer.RenderStartRow + Coder.CodeRenderer.RenderRow)
            {
                return;
            }

            // 第一种情况，选中的区域在同一行
            if (startRow == endRow)
            {
                renderer.DrawRect(new URect(
                    (int)((startCol - Coder.CodeRenderer.RenderStartCol) * WordWidth + Coder.CodeRenderer.RenderOffsetX + WordWidth / 2 - 1),
                    (int)((startRow - Coder.CodeRenderer.RenderStartRow) * WordHeight + Coder.CodeRenderer.RenderOffsetY),
                    (int)((endCol - startCol) * WordWidth),
                    (int)WordHeight),
                    color,
                    true);
                return;
            }
            else
            {
                // 第一行
                renderer.DrawRect(new URect(
                    (int)((startCol - Coder.CodeRenderer.RenderStartCol) * WordWidth + Coder.CodeRenderer.RenderOffsetX + WordWidth / 2 - 1),
                    (int)((startRow - Coder.CodeRenderer.RenderStartRow) * WordHeight + Coder.CodeRenderer.RenderOffsetY),
                    (int)((Coder.CodeManager.GetCodeLength(startRow) - startCol) * WordWidth),
                    (int)WordHeight),
                    color,
                    true);

                // 最后一行
                renderer.DrawRect(new URect(
                    (int)(Coder.CodeRenderer.RenderOffsetX + WordWidth / 2 - 1),
                    (int)((endRow - Coder.CodeRenderer.RenderStartRow) * WordHeight + Coder.CodeRenderer.RenderOffsetY),
                    (int)(endCol * WordWidth),
                    (int)WordHeight),
                    color,
                    true);

                for (int i = startRow + 1; i < endRow; i++)
                {
                    if (i > Coder.CodeRenderer.RenderStartRow + Coder.CodeRenderer.RenderRow)
                    {
                        break;
                    }

                    renderer.DrawRect(new URect(
                        (int)(Coder.CodeRenderer.RenderOffsetX + WordWidth / 2 - 1),
                        (int)((i - Coder.CodeRenderer.RenderStartRow) * WordHeight + Coder.CodeRenderer.RenderOffsetY),
                        (int)(Coder.CodeManager.GetCodeLength(i) * WordWidth),
                        (int)WordHeight),
                        color,
                        true);
                }
            }
        }
    }
}
