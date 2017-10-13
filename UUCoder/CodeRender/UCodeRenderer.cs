using System;
using System.Collections.Generic;
using System.Text;

using UUCoder.CodeBase;
using UUCoder.CodeParser;

namespace UUCoder.CodeRender
{
    public class UCodeRenderer : CodePlugin.IPlugin
    {
        #region Variable

        private UPoint mRenderOffset; // 渲染代码的区域在主显示区的偏移量（像素）
        private URank mRenderSize; // 渲染的范围（几行几列）

        private int mRenderStartRow; // 从第几行开始渲染
        private int mRenderStartCol; // 从第几列开始渲染

        private URank mCursor; // 光标所在位置

        public int CursorRow
        {
            get
            {
                return mCursor.Row;
            }
        }

        public int CursorCol
        {
            get
            {
                return mCursor.Col;
            }
        }

        /// <summary>
        /// 渲染开始的行
        /// </summary>
        public int RenderStartRow
        {
            get
            {
                return mRenderStartRow;
            }
        }

        /// <summary>
        /// 渲染开始的列
        /// </summary>
        public int RenderStartCol
        {
            get
            {
                return mRenderStartCol;
            }
        }

        /// <summary>
        /// 渲染的偏移位置X
        /// </summary>
        public int RenderOffsetX
        {
            get
            {
                return mRenderOffset.X;
            }
        }

        /// <summary>
        /// 渲染的偏移位置Y
        /// </summary>
        public int RenderOffsetY
        {
            get
            {
                return mRenderOffset.Y;
            }
        }

        /// <summary>
        /// 渲染的行数
        /// </summary>
        public int RenderRow
        {
            get
            {
                return mRenderSize.Row;
            }
        }

        /// <summary>
        /// 渲染的列数
        /// </summary>
        public int RenderCol
        {
            get
            {
                return mRenderSize.Col;
            }
        }

        #endregion

        public UCodeRenderer(UUCoder coder)
            : base(coder)
        {
            Reset();
        }

        public override void Initialize()
        {
        }

        public override void Reset()
        {
            this.mRenderOffset = UPoint.Zero;

            this.mRenderStartRow = 0;

            this.mRenderSize = new URank(
                (int)((float)Coder.Renderer.RenderHeight / Coder.Renderer.CharHeight),
                (int)((float)Coder.Renderer.RenderWidth / Coder.Renderer.CharWidth));

            this.mCursor = URank.Zero;
        }

        #region Set Or Get

        public void SetRenderStartRow(int row)
        {
            if (row < 0 || row >= Coder.CodeManager.GetCodeLineCount())
            {
                return;
            }

            mRenderStartRow = row;
        }

        public void SetRenderStartCol(int col)
        {
            if (col < 0)
            {
                return;
            }

            mRenderStartCol = col;
        }

        public void SetRenderOffset(UPoint offset)
        {
            mRenderOffset = offset;
        }

        public void IncreaseCursorCol()
        {
            // 光标横向移动一个位置
            mCursor.Col++;

            // 判断光标是否大于当前行的长度
            if (mCursor.Col >= Coder.CodeManager.GetCodeLength(mCursor.Row))
            {
                // 跳转到下一行
                mCursor.Col = 0;
                mRenderStartCol = 0;

                IncreaseCursorRow();
            }

            FixCursorPosition(false);
        }

        public void IncreaseCursorRow()
        {
            mCursor.Row++;

            if (mCursor.Row >= Coder.CodeManager.GetCodeCount())
            {
                mCursor.Row = Coder.CodeManager.GetCodeCount() - 1;
            }

            if (mCursor.Col >= Coder.CodeManager.GetCodeLength(mCursor.Row))
            {
                mCursor.Col = Coder.CodeManager.GetCodeLength(mCursor.Row) - 1;
            }

            FixCursorPosition(false);
        }

        public void DecreaseCursorCol()
        {
            mCursor.Col--;

            if (mCursor.Col < 0)
            {
                mCursor.Col = 0;

                DecreaseCursorRow();

                mCursor.Col = Coder.CodeManager.GetCodeLength(mCursor.Row) - 1;

                if (mCursor.Col > mRenderStartCol + mRenderSize.Col)
                {
                    mRenderStartCol = mCursor.Col - mRenderSize.Col;
                }
            }

            FixCursorPosition(true);
        }

        public void DecreaseCursorRow()
        {
            mCursor.Row--;

            if (mCursor.Row < 0)
            {
                mCursor.Row = 0;
            }

            if (mCursor.Col >= Coder.CodeManager.GetCodeLength(mCursor.Row))
            {
                mCursor.Col = Coder.CodeManager.GetCodeLength(mCursor.Row) - 1;
            }

            FixCursorPosition(false);
        }

        public void SetCursorPos(int mouseX, int mouseY)
        {
            if (mouseX < mRenderOffset.X || mouseY < mRenderOffset.Y)
            {
                return;
            }

            float cursorX = mouseX - mRenderOffset.X;
            float cursorY = mouseY - mRenderOffset.Y;

            cursorY = cursorY / Coder.Renderer.CharHeight + mRenderStartRow;
            cursorX = cursorX / Coder.Renderer.CharWidth + mRenderStartCol;

            if (cursorY >= Coder.CodeManager.GetCodeCount())
            {
                cursorY = Coder.CodeManager.GetCodeCount() - 1;
            }

            int len = Coder.CodeManager.GetCodeLength((int)cursorY);

            if (cursorX >= len) // 不能等于的原因是，最后一个字符是换行符
            {
                cursorX = len - 1;
            }

            mCursor.Row = (int)cursorY;
            mCursor.Col = (int)cursorX;

            FixCursorPosition(true);
        }

        public void SetCursorPos(URank pos)
        {
            mCursor = pos;
            FixCursorPosition(true);
        }

        public URank GetCursorPos()
        {
            return mCursor;
        }

        #endregion

        #region Render

        public override void Render(IRenderer renderer)
        {
            try
            {
                float renderX = mRenderOffset.X - mRenderStartCol * renderer.CharWidth;
                float renderY = mRenderOffset.Y;

                int maxCount = Coder.CodeManager.GetCodeLineCount();

                for (int i = 0; i < mRenderSize.Row; i++)
                {
                    if (i + mRenderStartRow >= maxCount)
                    {
                        break;
                    }

                    RenderCodeLine(renderer, renderX, renderY, Coder.CodeManager.GetCodeLine(i + mRenderStartRow));
                    renderY += renderer.CharHeight;
                }

                renderer.DrawText(
                    (mCursor.Col - mRenderStartCol) * renderer.CharWidth + mRenderOffset.X - renderer.CharWidth / 2,
                    (mCursor.Row - mRenderStartRow) * renderer.CharHeight + mRenderOffset.Y,
                    "|",
                    UColor.White);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RenderCodeLine(IRenderer renderer, float x, float y, UCodeLine line)
        {
            try
            {
                float renderX = x;

                int count = line.GetCutCount();

                for (int i = 0; i < count; i++)
                {
                    UCodeCut cut = line.GetCutByIndex(i);

                    RenderCodeCut(renderer, renderX, y, cut);

                    renderX = renderX + UHelper.GetAbsoluteLength(cut.Data) * renderer.CharWidth;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RenderCodeCut(IRenderer renderer, float x, float y, UCodeCut cut)
        {
            try
            {
                if (cut.CutType == UCutType.Space || cut.CutType == UCutType.NewLine)
                {
                    return;
                }

                renderer.DrawText(x, y, cut.Data, cut.CutType.CutColor);

                if (cut.CutType == UCutType.Error)
                {
                    renderer.DrawDash(
                        new UPoint((int)x, (int)(y + renderer.CharHeight)),
                        new UPoint((int)(x + UHelper.GetAbsoluteLength(cut.Data) * renderer.CharWidth),
                            (int)(y + renderer.CharHeight)),
                        UColor.Red);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FixCursorPosition(bool back)
        {
            if (UHelper.IsChinesePart(Coder.CodeManager.GetCodeData(mCursor.Row), mCursor.Col))
            {
                if (back)
                {
                    mCursor.Col--;
                }
                else
                {
                    mCursor.Col++;
                }
            }

            if (mCursor.Col < mRenderStartCol)
            {
                mRenderStartCol = mCursor.Col;
            }
            if (mCursor.Col >= mRenderStartCol + mRenderSize.Col)
            {
                mRenderStartCol = mCursor.Col - mRenderSize.Col + 1;
            }
            if (mCursor.Row < mRenderStartRow)
            {
                mRenderStartRow = mCursor.Row;
            }
            if (mCursor.Row > mRenderStartRow + mRenderSize.Row)
            {
                mRenderStartRow = mCursor.Row - mRenderSize.Row + 1;
            }
        }

        #endregion
    }
}
