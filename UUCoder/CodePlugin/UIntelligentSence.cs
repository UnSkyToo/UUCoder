using System;
using System.Collections.Generic;
using System.Text;

using UUCoder.CodeBase;
using UUCoder.CodeParser;

namespace UUCoder.CodePlugin
{
    public class UIntelligentSence : IPlugin
    {
        private List<string> mPrometStrings;
        private List<string> mMatchingResult;
        private int mSelectIndex;

        public bool HasMatching
        {
            get
            {
                return mMatchingResult.Count != 0;
            }
        }

        public UIntelligentSence(UUCoder coder)
            : base(coder)
        {
            this.mPrometStrings = new List<string>();
            this.mMatchingResult = new List<string>();

            this.Coder.CodeManager.TextChanged += TextChangedEvent;

            ParseCode();
        }

        public void ParseCode()
        {
            mPrometStrings.Clear();

            mPrometStrings.AddRange(UConfig.KeyWords);

            int count = Coder.CodeManager.GetCodeCount();

            for (int i = 0; i < count; i++)
            {
                UCodeLine line = Coder.CodeManager.GetCodeLine(i);

                int cutCount = line.GetCutCount();

                for (int n = 0; n < cutCount; n++)
                {
                    UCodeCut cut = line.GetCutByIndex(n);

                    if (cut.CutType == UCutType.VariableName ||
                        cut.CutType == UCutType.ClassName ||
                        cut.CutType == UCutType.FunctionName)
                    {
                        if (!mPrometStrings.Contains(cut.Data))
                        {
                            mPrometStrings.Add(cut.Data);
                        }
                    }
                }
            }
        }

        public override void Initialize()
        {
            Reset();
        }

        /// <summary>
        /// 清空匹配列表
        /// </summary>
        public override void Reset()
        {
            mMatchingResult.Clear();
            mSelectIndex = 0;
        }

        public void IncreaseIndex()
        {
            mSelectIndex++;

            if (mSelectIndex >= mMatchingResult.Count)
            {
                mSelectIndex = 0;
            }
        }

        public void DecreaseIndex()
        {
            mSelectIndex--;

            if (mSelectIndex < 0)
            {
                mSelectIndex = mMatchingResult.Count - 1;
            }
        }

        /// <summary>
        /// 选中当前匹配项
        /// </summary>
        public void Selected()
        {
            if (mMatchingResult.Count > 0)
            {
                string inputString = Coder.CodeManager.GetCodeCut(new URank(Coder.CursorRow, Coder.CursorCol - 1)).Data;

                ReplaceStringCommand rsc = new ReplaceStringCommand(
                    Coder.CodeManager,
                    new URank(Coder.CursorRow, Coder.CursorCol - UHelper.GetAbsoluteLength(inputString)),
                    UHelper.GetAbsoluteLength(inputString),
                    mMatchingResult[mSelectIndex]);

                Coder.CodeManager.Execute(rsc);

                Reset();
            }
        }

        public override void Render(CodeRender.IRenderer renderer)
        {
            if (mMatchingResult.Count == 0)
            {
                return;
            }

            float startX = (Coder.CursorCol - Coder.CodeRenderer.RenderStartCol + 1) *
                Coder.Renderer.CharWidth + Coder.CodeRenderer.RenderOffsetX;
            float startY = (Coder.CursorRow - Coder.CodeRenderer.RenderStartRow + 1) *
                Coder.Renderer.CharHeight + Coder.CodeRenderer.RenderOffsetY;
            int height = (int)Coder.Renderer.CharHeight + 4;
            int width = (int)Coder.Renderer.CharWidth * 20;

            renderer.DrawRect(new URect(
                (int)startX,
                (int)startY,
                width,
                height * 10),
                UColor.White,
                true);
            renderer.DrawRect(new URect(
                (int)startX,
                (int)startY + mSelectIndex * height,
                width,
                height),
                UColor.Orange,
                true);

            for (int i = 0; i < mMatchingResult.Count; i++)
            {
                renderer.DrawText(startX, startY + i * height + 2, mMatchingResult[i], UColor.Black);
            }
        }

        private void MatchingString()
        {
            Reset();
            
            if (Coder.CursorCol == 0)
            {
                return;
            }

            string inputString = Coder.CodeManager.GetCodeCut(new URank(Coder.CursorRow, Coder.CursorCol - 1)).Data;

            if (inputString.Length == 0)
            {
                return;
            }

            inputString = inputString.ToLower();

            foreach (string str in mPrometStrings)
            {
                if (str.ToLower().StartsWith(inputString) && inputString.Length <= str.Length)
                {
                    mMatchingResult.Add(str);
                }
            }
        }

        private void TextChangedEvent()
        {
            MatchingString();
        }
    }
}
