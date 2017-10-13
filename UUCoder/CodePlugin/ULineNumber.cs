using System;
using System.Collections.Generic;
using System.Text;

using UUCoder.CodeBase;

namespace UUCoder.CodePlugin
{
    public class ULineNumber : IPlugin
    {
        private bool mVisible;

        public ULineNumber(UUCoder coder)
            : base(coder)
        {
            this.mVisible = true;
        }

        public override void Initialize()
        {
            return;
        }

        public override void Reset()
        {
            return;
        }

        public void SetVisible(bool visible)
        {
            mVisible = visible;

            if (mVisible == false)
            {
                Coder.SetRenderOffset(UPoint.Zero);
            }
        }

        public override void Render(CodeRender.IRenderer renderer)
        {
            if (!mVisible)
            {
                return;
            }

            int width = (int)((Coder.CodeManager.GetCodeCount().ToString().Length + 1) * renderer.CharWidth);
            //float renderX = 0;
            float renderY = 0;

            Coder.SetRenderOffset(new UPoint(width, 0));

            renderer.DrawRect(new URect(0, 0, width, renderer.RenderHeight), UColor.Black, true);

            for (int i = 0; i < Coder.CodeRenderer.RenderRow; i++)
            {
                renderer.DrawText(0, renderY, (i + Coder.CodeRenderer.RenderStartRow).ToString(), UColor.Purple);
                renderY += renderer.CharHeight;
            }
        }
    }
}
