using System;
using System.Collections.Generic;
using System.Text;

using UUCoder.CodeBase;
using UUCoder.CodeRender;
using UUCoder.CodePlugin;
using UUCoder.CodeParser;

namespace UUCoder
{
    public class UUCoder
    {
        private IRenderer mRenderer;
        /// <summary>
        /// 渲染器
        /// </summary>
        public IRenderer Renderer
        {
            get
            {
                return mRenderer;
            }
        }

        private UCodeManager mCodeManager;
        /// <summary>
        /// 代码管理器
        /// </summary>
        public UCodeManager CodeManager
        {
            get
            {
                return mCodeManager;
            }
        }
        
        private UCodeRenderer mCodeRenderer;
        /// <summary>
        /// 代码渲染器
        /// </summary>
        public UCodeRenderer CodeRenderer
        {
            get
            {
                return mCodeRenderer;
            }
        }

        private UIntelligentSence mIntelligentSence;
        private ULineNumber mLineNumber;
        private USelection mSelection;

        private UPluginManager mPluginManager;

        private bool mIsBlankWindow; // 是否为空白窗体

        /// <summary>
        /// 光标所在行
        /// </summary>
        public int CursorRow
        {
            get
            {
                return mCodeRenderer.CursorRow;
            }
        }
        /// <summary>
        /// 光标所在列
        /// </summary>
        public int CursorCol
        {
            get
            {
                return mCodeRenderer.CursorCol;
            }
        }

        private bool mMouseLeftDown = false;

        public UUCoder(IRenderer renderer)
        {
            this.mRenderer = renderer;
            this.mCodeManager = new UCodeManager(this);

            this.mCodeRenderer = new UCodeRenderer(this);
            this.mIntelligentSence = new UIntelligentSence(this);

            this.mLineNumber = new ULineNumber(this);

            this.mSelection = new USelection(this);

            this.mPluginManager = new UPluginManager(this);
            // 默认的插件，顺序不能变
            this.mPluginManager.AddPlugin(this.mSelection); // 0
            this.mPluginManager.AddPlugin(this.mCodeRenderer); // 1
            this.mPluginManager.AddPlugin(this.mIntelligentSence); // 2
            this.mPluginManager.AddPlugin(this.mLineNumber); // 3

            this.mPluginManager.Initialize();
            this.mPluginManager.Reset();

            this.mIsBlankWindow = true;
        }

        public void ParseFile(string filePath)
        {
            try
            {
                mCodeManager.LoadFile(filePath);
                mCodeManager.ParseCode();

                mIntelligentSence.ParseCode();

                this.mIsBlankWindow = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReParseCode()
        {
            mCodeManager.ParseCode();
        }
        
        public void Render()
        {
            try
            {
                mRenderer.Clear(UColor.Black);

                mPluginManager.Render(this.Renderer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetRenderStartRow(int row)
        {
            mCodeRenderer.SetRenderStartRow(row);
        }

        public void SetRenderStartCol(int col)
        {
            mCodeRenderer.SetRenderStartCol(col);
        }

        public void SetRenderOffset(UPoint offset)
        {
            mCodeRenderer.SetRenderOffset(offset);
        }

        public void SetCursorPos(URank pos)
        {
            mCodeRenderer.SetCursorPos(pos);
        }

        public void SetLineNumberVisible(bool visible)
        {
            mLineNumber.SetVisible(visible);
        }

        public void LoadConfig(string filePath)
        {
            UConfig.LoadConfigFromFile(filePath);
            ReParseCode();
        }

        #region Command

        private void InsertRangeString(URank start, string str)
        {
            InsertRangeStringCommand irs = new InsertRangeStringCommand(mCodeManager, start, str);

            mCodeManager.Execute(irs);
        }

        private void RemoveSelection()
        {
            RemoveRangeStringCommand rrs = new RemoveRangeStringCommand(mCodeManager, mSelection.Start, mSelection.End);

            mCodeManager.Execute(rrs);

            mSelection.Reset();
        }

        #endregion

        #region Event
        public void KeyDown(UKeyEvents e)
        {
            if (mIsBlankWindow)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case UKeys.Left:
                    mCodeRenderer.DecreaseCursorCol();
                    mIntelligentSence.Reset();
                    return;
                case UKeys.Right:
                    mCodeRenderer.IncreaseCursorCol();
                    mIntelligentSence.Reset();
                    return;
                case UKeys.Up:
                    if (mIntelligentSence.HasMatching)
                    {
                        mIntelligentSence.DecreaseIndex();
                    }
                    else
                    {
                        mCodeRenderer.DecreaseCursorRow();
                    }
                    return;
                case UKeys.Down:
                    if (mIntelligentSence.HasMatching)
                    {
                        mIntelligentSence.IncreaseIndex();
                    }
                    else
                    {
                        mCodeRenderer.IncreaseCursorRow();
                    }
                    return;
                case UKeys.Back:
                    // 删除键是删除鼠标前一个位置的字符
                    if (mSelection.HasSelection)
                    {
                        RemoveSelection();
                        return;
                    }

                    if (mCodeRenderer.CursorCol == 0) // 在行首删除，合并当上一行
                    {
                        if (mCodeRenderer.CursorRow > 0)
                        {
                            RemoveNewlineByteCommand rnc = new RemoveNewlineByteCommand(
                                mCodeManager,
                                new URank(mCodeRenderer.CursorRow - 1,
                                    mCodeManager.GetCodeLength(mCodeRenderer.CursorRow - 1) - 1));

                            mCodeManager.Execute(rnc);
                        }
                    }
                    else
                    {
                        RemoveCharacterCommand rcc = new RemoveCharacterCommand(
                            mCodeManager,
                            new URank(mCodeRenderer.CursorRow, mCodeRenderer.CursorCol - 1));

                        mCodeManager.Execute(rcc);
                    }
                    return;
                case UKeys.Space:
                    {
                        // 这里不要return，留到下面处理space添加一个空格
                        if (mIntelligentSence.HasMatching)
                        {
                            mIntelligentSence.Selected();
                        }
                        else if (mSelection.HasSelection)
                        {
                            RemoveSelection();
                        }
                    }
                    break;
                case UKeys.Enter:
                    {
                        if (mIntelligentSence.HasMatching)
                        {
                            mIntelligentSence.Selected();
                            return;
                        }

                        URank pos = mCodeRenderer.GetCursorPos();

                        InsertNewLineByteCommand inc = new InsertNewLineByteCommand(mCodeManager, pos);

                        mCodeManager.Execute(inc);

                        // 缩进处理
                        // 计算新的一行需要额外填充的空格
                        // 从上一行的可见字符开始
                        int padSpaceCount = mCodeManager.GetCodeLine(pos.Row).VisibleStartCol;

                        // 按回车后，检查是否是在“{”或“}”后按的回车
                        if (pos.Col - 1 >= 0)
                        {
                            byte lastByte = mCodeManager.GetCodeData(pos.Row)[pos.Col - 1]; // 最后一个字符是回车，所以应该减一

                            // 如果是左括号“{”
                            if (lastByte == UConfig.LBracket)
                            {
                                padSpaceCount += UConfig.TabNumberOfSpace;
                            }
                        }

                        if (padSpaceCount > 0)
                        {
                            // 如果只是在一行的空白部分中间按下回车
                            // 那么padSpaceCount = 上一行的空白部分
                            // 并且新的一行本身就包含了一部分
                            // 加起来刚好
                            byte[] spaceBytes = new byte[padSpaceCount];

                            for (int i = 0; i < padSpaceCount; i++)
                            {
                                spaceBytes[i] = UConfig.Space;
                            }

                            InsertStringCommand isc = new InsertStringCommand(mCodeManager, new URank(pos.Row + 1, 0), UHelper.GetStringByBytes(spaceBytes));

                            mCodeManager.Execute(isc);
                        }

                        return;
                    }
                case UKeys.A:
                    if (e.Ctrl)
                    {
                        SelectAll();
                        return;
                    }
                    break;
                case UKeys.C:
                    if (e.Ctrl)
                    {
                        Copy();
                        return;
                    }
                    break;
                case UKeys.V:
                    if (e.Ctrl)
                    {
                        Paste();
                        return;
                    }
                    break;
                case UKeys.X:
                    if (e.Ctrl)
                    {
                        Cut();
                        return;
                    }
                    break;
                case UKeys.Y:
                    if (e.Ctrl)
                    {
                        mCodeManager.Redo();
                        return;
                    }
                    break;
                case UKeys.Z:
                    if (e.Ctrl)
                    {
                        mCodeManager.Undo();
                        return;
                    }
                    break;
                default:
                    break;
            }

            char ch = UHelper.ParseKey(e);

            if (ch == (char)0)
            {
                return;
            }

            if (mSelection.HasSelection)
            {
                RemoveSelection();
            }

            InsertCharacterCommand iccd = new InsertCharacterCommand(mCodeManager, mCodeRenderer.GetCursorPos(), (byte)ch);

            mCodeManager.Execute(iccd);

            // 如果输入的字符是“}”，那么缩进一个Tab
            // 处理输入字符“{”，是在按回车处理
            if ((byte)ch == UConfig.RBracket)
            {
                URank pos = mCodeRenderer.GetCursorPos();
                int visibleStartCol = mCodeManager.GetCodeLine(pos.Row).VisibleStartCol;

                if (visibleStartCol >= UConfig.TabNumberOfSpace)
                {
                    // 缩进一个Tab，先找到最靠近第一个可见字符的空白字符，然后删除往前的空格
                    RemoveStringCommand rsc = new RemoveStringCommand(
                        mCodeManager,
                        new URank(pos.Row, visibleStartCol - UConfig.TabNumberOfSpace),
                        UConfig.TabNumberOfSpace);

                    mCodeManager.Execute(rsc);

                    mCodeRenderer.SetCursorPos(
                        new URank(
                            pos.Row,
                            mCodeManager.GetCodeLength(pos.Row) - 1));
                }
            }
        }

        public void MouseDown(UMouseEvents e)
        {
            if (mIsBlankWindow)
            {
                return;
            }

            if (e.Button == UMouseButton.Left)
            {
                mCodeRenderer.SetCursorPos(e.X, e.Y);
                mIntelligentSence.Reset();

                mMouseLeftDown = true;

                mSelection.Reset();
            }
        }

        public void MouseUp(UMouseEvents e)
        {
            if (mIsBlankWindow)
            {
                return;
            }

            if (e.Button == UMouseButton.Left)
            {
                mMouseLeftDown = false;
            }
        }

        public void MouseMove(UMouseEvents e)
        {
            if (mIsBlankWindow)
            {
                return;
            }

            if (mMouseLeftDown == true)
            {
                mCodeRenderer.SetCursorPos(e.X, e.Y);

                URank pos = mCodeRenderer.GetCursorPos();

                if (mSelection.Start == URank.None)
                {
                    mSelection.SetStart(pos);
                    mSelection.SetEnd(pos);
                }
                else
                {
                    if (pos <= mSelection.Start)
                    {
                        mSelection.SetStart(pos);
                    }
                    else// if (pos > mSelection.End)
                    {
                        mSelection.SetEnd(pos);
                    }
                }
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void Shutdown()
        {
            mCodeManager.Dispose();
        }

        public void ResetDrawing()
        {
            if (mIsBlankWindow)
            {
                return;
            }

            mCodeRenderer.Reset();
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public void Undo()
        {
            if (mIsBlankWindow)
            {
                return;
            }

            mCodeManager.Undo();
        }

        /// <summary>
        /// 重做
        /// </summary>
        public void Redo()
        {
            if (mIsBlankWindow)
            {
                return;
            }

            mCodeManager.Redo();
        }

        /// <summary>
        /// 全选
        /// </summary>
        public void SelectAll()
        {
            if (mIsBlankWindow)
            {
                return;
            }

            mSelection.SetStart(URank.Zero);
            mSelection.SetEnd(new URank(
                mCodeManager.GetCodeCount() - 1,
                mCodeManager.GetCodeLength(mCodeManager.GetCodeCount() - 1)));
        }

        /// <summary>
        /// 复制
        /// </summary>
        public void Copy()
        {
            if (mIsBlankWindow)
            {
                return;
            }

            if (mSelection.HasSelection)
            {
                System.Windows.Forms.Clipboard.SetText(mCodeManager.GetCodeString(mSelection.Start, mSelection.End));
            }
        }

        /// <summary>
        /// 剪切
        /// </summary>
        public void Cut()
        {
            if (mIsBlankWindow)
            {
                return;
            }

            if (mSelection.HasSelection)
            {
                Copy();
                RemoveSelection();
            }
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        public void Paste()
        {
            if (mIsBlankWindow)
            {
                return;
            }

            if (mSelection.HasSelection)
            {
                RemoveSelection();
            }

            if (System.Windows.Forms.Clipboard.ContainsText())
            {
                InsertRangeString(mCodeRenderer.GetCursorPos(), System.Windows.Forms.Clipboard.GetText());
            }
        }
        #endregion

        public void Debug()
        {
            if (mIsBlankWindow)
            {
                return;
            }

            RemoveRangeStringCommand rrs = new RemoveRangeStringCommand(CodeManager, URank.Zero, new URank(3, 4));

            CodeManager.Execute(rrs);
        }
    }
}
