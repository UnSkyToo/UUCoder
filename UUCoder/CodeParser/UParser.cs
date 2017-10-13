using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using UUCoder.CodeBase;

namespace UUCoder.CodeParser
{
    /// <summary>
    /// 代码分析器
    /// 并且逐个分解为CodeCut
    /// </summary>
    public class UParser : IParser
    {
        public bool EndOfCode { get; private set; }

        private UCodeManager mCodeManager;

        private int RowIndex; // 列的索引
        private int ColIndex; // 行的索引

        private int OldRowIndex;
        private int OldColIndex;

        private UCodeCut LastCut1;
        private UCodeCut LastCut2;

        public UParser(UCodeManager codeManager)
        {
            this.mCodeManager = codeManager;

            this.LastCut1 = new UCodeCut();
            this.LastCut2 = new UCodeCut();

            Reset();
        }

        /// <summary>
        /// 重置分析器
        /// </summary>
        public void Reset()
        {
            EndOfCode = false;

            RowIndex = 0;
            ColIndex = 0;

            OldRowIndex = 0;
            OldColIndex = 0;

            LastCut1 = new UCodeCut();
            LastCut2 = new UCodeCut();
        }

        /// <summary>
        /// 返回下一个代码切块
        /// </summary>
        /// <returns></returns>
        public UCodeCut GetNextCut()
        {
            try
            {
                UCodeCut cut = new UCodeCut();
                List<byte> cutData = new List<byte>();
                byte b;
                UCutType currType = UCutType.None;

                bool inString = false; // 是否解析字符串

                if (EndOfCode == true)
                {
                    cut.CutType = UCutType.End;
                    return cut;
                }

                while (!EndOfCode)
                {
                    b = GetNextByte();
                    cutData.Add(b);

                    #region UCutType.None
                    if (currType == UCutType.None)
                    {
                        if (b == UConfig.Space)
                        {
                            currType = UCutType.Space;
                            continue;
                        }

                        if (b == UConfig.Tab)
                        {
                            currType = UCutType.Tab;
                            break;
                        }

                        if (b == UConfig.DoubleQuote)
                        {
                            currType = UCutType.String;
                            inString = true;
                            continue;
                        }

                        if (b == UConfig.NewLine)
                        {
                            currType = UCutType.NewLine;

                            // 跳过回车符
                            if (PeekNextByte() == UConfig.Enter)
                            {
                                GetNextByte();
                            }

                            break;
                        }

                        if (b == UConfig.BackSlash)
                        {
                            if (PeekNextByte() == UConfig.BackSlash)
                            {
                                currType = UCutType.Annotation;
                                continue;
                            }
                        }

                        if (UHelper.IsSymbol(b))
                        {
                            currType = UCutType.Symbol;
                            break;
                        }

                        if (UHelper.IsCharacter(b))
                        {
                            currType = UCutType.Normal;
                            continue;
                        }

                        if (UHelper.IsDigit(b))
                        {
                            currType = UCutType.Digit;
                            continue;
                        }
                    }
                    #endregion

                    #region UCutType.Normal
                    if (currType == UCutType.Normal)
                    {
                        if (UHelper.IsCutEnd(b))
                        {
                            BackToLastByte();

                            cutData.RemoveAt(cutData.Count - 1);
                            break;
                        }
                    }
                    #endregion

                    #region UCutType.String
                    if (currType == UCutType.String)
                    {
                        if (b == UConfig.NewLine)
                        {
                            BackToLastByte();

                            currType = UCutType.Normal;
                            break;
                        }

                        if (b == UConfig.DoubleQuote)
                        {
                            inString = false;
                            break;
                        }

                        if (b == UConfig.Slash)
                        {
                            // 添加 \ 后的字符
                            if (inString)
                            {
                                //ch = (char)GetNextChar();
                                //cutData.Add((byte)ch);
                                cutData.Add(GetNextByte());
                                continue;
                            }
                        }
                    }
                    #endregion

                    #region UCutType.Space
                    if (currType == UCutType.Space)
                    {
                        if (b != UConfig.Space)
                        {
                            BackToLastByte();

                            cutData.RemoveAt(cutData.Count - 1);

                            break;
                        }
                    }
                    #endregion

                    #region UCutType.Digit
                    if (currType == UCutType.Digit)
                    {
                        if (UHelper.IsCutEnd(b))
                        {
                            BackToLastByte();

                            cutData.RemoveAt(cutData.Count - 1);
                            break;
                        }

                        if (!UHelper.IsDigit(b))
                        {
                            BackToLastByte();

                            cutData.RemoveAt(cutData.Count - 1);
                            break;
                        }
                    }
                    #endregion

                    #region UCutType.Annotation
                    if (currType == UCutType.Annotation)
                    {
                        if (b == UConfig.NewLine)
                        {
                            BackToLastByte();

                            cutData.RemoveAt(cutData.Count - 1);

                            break;
                        }
                    }
                    #endregion
                }

                cut.CutType = currType;

                // 替换Tab为Space
                if (currType == UCutType.Tab)
                {
                    cut.Data = UConfig.TabString;
                }
                else
                {
                    cut.Data = UHelper.GetStringByBytes(cutData.ToArray());
                }

                return ParseCodeCutType(cut);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 分析指定行的代码
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public UCodeLine ParseLine(int index)
        {
            try
            {
                UCodeLine line = new UCodeLine();
                UCodeCut cut;

                Reset();

                RowIndex = index;
                OldRowIndex = index;

                while (true)
                {
                    cut = GetNextCut();

                    if (cut.CutType == UCutType.End)
                    {
                        break;
                    }

                    if (RowIndex != index)
                    {
                        break;
                    }

                    line.AddCut(cut);
                }

                return line;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 回退到上一个位置
        private void BackToLastByte()
        {
            RowIndex = OldRowIndex;
            ColIndex = OldColIndex;
        }

        // 读取下一个字符
        private byte GetNextByte()
        {
            try
            {
                if (EndOfCode)
                {
                    return 0;
                }

                OldRowIndex = RowIndex;
                OldColIndex = ColIndex;

                if (ColIndex >= mCodeManager.GetCodeLength(RowIndex))
                {
                    RowIndex++;
                    ColIndex = 0;
                }

                if (RowIndex >= mCodeManager.GetCodeCount())
                {
                    EndOfCode = true;
                    return 0;
                }

                byte b = mCodeManager.GetCodeByte(new URank(RowIndex, ColIndex));
                ColIndex++;

                return b;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 读取下一个字符，但指针不移动
        private byte PeekNextByte()
        {
            try
            {
                byte b = GetNextByte();

                BackToLastByte();

                return b;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 分析cut的类型
        private UCodeCut ParseCodeCutType(UCodeCut cut)
        {
            // 如果上一个Cut是错误的，之后都是错误
            if (LastCut1.CutType == UCutType.Error)
            {
                cut.CutType = UCutType.Error;
                return cut;
            }

            // 如果是普通的一段文本，判断是否为关键字
            if (cut.CutType == UCutType.Normal)
            {
                if (UHelper.IsKeyWord(cut.Data))
                {
                    cut.CutType = UCutType.KeyWord;
                    // 新版：设置不同关键字的颜色
                    cut.CutType.CutColor = UHelper.GetKeyWordColor(cut.Data);
                }
            }

            // 如果是以关键字开头，空格结尾，那么检查是否为类定义符
            if (LastCut2.CutType == UCutType.KeyWord && LastCut1.CutType == UCutType.Space)
            {
                // 类的定义
                if (LastCut2.Data == UConfig.ClassString)
                {
                    cut.CutType = UCutType.ClassName;

                    foreach (string str in UConfig.KeyWords)
                    {
                        if (str == cut.Data)
                        {
                            cut.CutType = UCutType.Error;
                            break;
                        }
                    }
                }
            }
            else if ( LastCut1.CutType == UCutType.Space )
            {
                // 函数名和变量的定义
                foreach (string str in UConfig.VariableType)
                {
                    if (str == LastCut2.Data)
                    {
                        if (PeekNextByte() == (byte)'(')
                        {
                            cut.CutType = UCutType.FunctionName;
                        }
                        else
                        {
                            cut.CutType = UCutType.VariableName;
                        }

                        foreach (string s in UConfig.KeyWords)
                        {
                            if (s == cut.Data)
                            {
                                cut.CutType = UCutType.Error;
                                break;
                            }
                        }

                        break;
                    }
                }
            }

            // 引用类型，直接赋值是地址
            //LastCut2 = LastCut1;
            //LastCut1 = cut;
            LastCut2 = new UCodeCut(LastCut1);
            LastCut1 = new UCodeCut(cut);

            return cut;
        }
    }
}
