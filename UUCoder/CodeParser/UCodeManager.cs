using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using UUCoder.CodeBase;

namespace UUCoder.CodeParser
{
    public delegate void UCodeManagerEventHandler();

    public class UCodeManager
    {
        private UUCoder mCoder;
        private IParser mParser;
        private List<UCodeLine> mCodeLines;
        private List<List<byte>> mCodeData;

        private Stack<IUndoCommand> mUndoCommands;
        private Stack<IUndoCommand> mRedoCommands;

        /// <summary>
        /// 文本改变事件
        /// </summary>
        public event UCodeManagerEventHandler TextChanged;

        /// <summary>
        /// 最多保存的可撤销指令个数
        /// </summary>
        public int UndoCount { get; set; }

        public UCodeManager(UUCoder coder)
        {
            this.mCoder = coder;
            this.mParser = new UParser(this);
            this.mCodeLines = new List<UCodeLine>();

            this.mCodeData = new List<List<byte>>();

            this.mUndoCommands = new Stack<IUndoCommand>();
            this.mRedoCommands = new Stack<IUndoCommand>();

            this.UndoCount = 64;
        }

        public void Dispose()
        {
            mCodeLines.Clear();
            mCodeData.Clear();

            mUndoCommands.Clear();
            mRedoCommands.Clear();
        }

        #region Get Or Set

        /// <summary>
        /// 返回CodeLine的总个数
        /// </summary>
        /// <returns></returns>
        public int GetCodeLineCount()
        {
            return mCodeLines.Count;
        }

        /// <summary>
        /// 返回代码的总行数
        /// </summary>
        /// <returns></returns>
        public int GetCodeCount()
        {
            return mCodeData.Count;
        }

        /// <summary>
        /// 返回指定行的代码文本长度
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetCodeLength(int index)
        {
            if (index < 0 || index >= mCodeData.Count)
            {
                throw new Exception("GetCodeLength : Index Invalid {" + index.ToString() + "}");
            }

            return mCodeData[index].Count;
        }

        /// <summary>
        /// 返回指定行的Byte数组
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte[] GetCodeData(int index)
        {
            if (index < 0 || index >= mCodeData.Count)
            {
                throw new Exception("GetCodeData : Index Invalid {" + index.ToString() + "}");
            }

            return mCodeData[index].ToArray();
        }

        /// <summary>
        /// 返回指定行的CodeLine
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public UCodeLine GetCodeLine(int index)
        {
            if (index < 0 || index >= mCodeLines.Count)
            {
                throw new Exception("GetCodeLine : Index Invalid {" + index.ToString() + "}");
            }

            return mCodeLines[index];
        }

        /// <summary>
        /// 返回指定位置所在的CodeCut
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public UCodeCut GetCodeCut(URank pos)
        {
            if (pos.Row < 0 || pos.Row >= mCodeLines.Count)
            {
                throw new Exception("GetCodeCut : Row Invalid { " + pos.Row.ToString() + "}");
            }

            return mCodeLines[pos.Row].GetCutByCol(pos.Col);
        }

        /// <summary>
        /// 返回指定位置的一个字符
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public byte GetCodeByte(URank pos)
        {
            if (pos.Row < 0 || pos.Row >= mCodeData.Count)
            {
                throw new Exception("GetCodeCharacter : Row Invalid {" + pos.Row.ToString() + "}");
            }

            if (pos.Col < 0 || pos.Col >= mCodeData[pos.Row].Count)
            {
                throw new Exception("GetCodeCharacter : Col Invalid {" + pos.Col.ToString() + "}");
            }

            return mCodeData[pos.Row][pos.Col];
        }

        /// <summary>
        /// 返回指定位置指定长度的字符串
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public string GetCodeString(URank pos, int len)
        {
            if (pos.Row < 0 || pos.Row >= mCodeData.Count)
            {
                throw new Exception("GetCodeString : Row Invalid {" + pos.Row.ToString() + "}");
            }

            if (pos.Col < 0 || pos.Col + len >= mCodeData[pos.Row].Count)
            {
                throw new Exception("GetCodeString : Col Invalid {" + pos.Col.ToString() + "}");
            }

            return UHelper.GetStringByBytes(mCodeData[pos.Row].GetRange(pos.Col, len).ToArray());
        }

        /// <summary>
        /// 返回指定区域的字符串
        /// </summary>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns></returns>
        public string GetCodeString(URank start, URank end)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<byte> lineByte = new List<byte>();

                int startRow = start.Row;
                int startCol = start.Col;
                int endRow = end.Row;
                int endCol = end.Col;

                if (start == end)
                {
                    return GetCodeString(start, endCol - startCol);
                }

                // 复制第一行
                int len = GetCodeLength(startRow);
                for (int i = startCol; i < len; i++)
                {
                    lineByte.Add(GetCodeByte(new URank(startRow, i)));
                }

                sb.Append(UHelper.GetStringByBytes(lineByte.ToArray()));

                // 复制中间部分
                for (int i = startRow + 1; i < endRow; i++)
                {
                    sb.Append(UHelper.GetStringByBytes(mCodeData[i].ToArray()));
                }

                lineByte.Clear();
                // 复制最后一行
                for (int i = 0; i < endCol; i++)
                {
                    lineByte.Add(GetCodeByte(new URank(endRow, i)));
                }
                sb.Append(UHelper.GetStringByBytes(lineByte.ToArray()));
                
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Parse

        /// <summary>
        /// 载入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void LoadFile(string filePath)
        {
            try
            {
                mCodeData.Clear();

                GC.Collect();

                StreamReader fp = new StreamReader(filePath, Encoding.Default);
                string line = string.Empty;

                while (!fp.EndOfStream)
                {
                    line = fp.ReadLine();

                    if (line == string.Empty || line[line.Length - 1] != '\n')
                    {
                        line += '\n';
                    }

                    List<byte> lineData = new List<byte>();
                    lineData.AddRange(UHelper.TanslateString(line));

                    mCodeData.Add(lineData);
                }

                fp.Close();
                fp.Dispose();

                if (mCodeData.Count == 0)
                {
                    List<byte> oneLine = new List<byte>();
                    oneLine.Add(UConfig.NewLine);
                    mCodeData.Add(oneLine);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 分析代码
        /// </summary>
        public void ParseCode()
        {
            try
            {
                UCodeCut cut;
                UCodeLine line = new UCodeLine();

                mCodeLines.Clear();
                mParser.Reset();

                GC.Collect();

                while (true)
                {
                    cut = mParser.GetNextCut();

                    if (cut.CutType == UCutType.End)
                    {
                        break;
                    }

                    line.AddCut(cut);

                    if (cut.CutType == UCutType.NewLine)
                    {
                        mCodeLines.Add(line);
                        line = new UCodeLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 分析指定行
        /// </summary>
        /// <param name="index">行号</param>
        public void ParseLine(int index)
        {
            try
            {
                mCodeLines[index] = mParser.ParseLine(index);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Command

        private void TextChangedEvent()
        {
            if (TextChanged != null)
            {
                TextChanged.Invoke();
            }
        }

        public void Execute(ICommand cmd)
        {
            cmd.Execute();

            mRedoCommands.Clear();

            if (cmd is IUndoCommand)
            {
                mUndoCommands.Push(cmd as IUndoCommand);
            }
            else
            {
                mUndoCommands.Clear();
            }

            TextChangedEvent();
        }

        public void Undo()
        {
            if (mUndoCommands.Count == 0)
            {
                return;
            }

            IUndoCommand cmd = mUndoCommands.Pop();

            cmd.Undo();

            mRedoCommands.Push(cmd);

            TextChangedEvent();
        }

        public void Redo()
        {
            if (mRedoCommands.Count == 0)
            {
                return;
            }

            IUndoCommand cmd = mRedoCommands.Pop();

            cmd.Execute();

            mUndoCommands.Push(cmd);

            TextChangedEvent();
        }

        /// <summary>
        /// 插入换行符到指定位置
        /// </summary>
        /// <param name="pos"></param>
        public void InsertNewLineByte(URank pos)
        {
            if (pos.Row < 0 || pos.Row >= mCodeData.Count)
            {
                throw new Exception("InsertNewLineByte : Row Invalid {" + pos.Row.ToString() + "}");
            }

            if (pos.Col < 0 || pos.Col >= mCodeData[pos.Row].Count)
            {
                throw new Exception("InsertNewLineByte : Col Invalid {" + pos.Col.ToString() + "}");
            }

            List<byte> newLineBytes = new List<byte>();

            // 插入回车，然后把后面的字节添加到新的一行
            mCodeData[pos.Row].Insert(pos.Col, UConfig.NewLine);

            for (int i = pos.Col + 1; i < mCodeData[pos.Row].Count; i++)
            {
                newLineBytes.Add(mCodeData[pos.Row][i]);
            }

            // 删除换行的内容，并添加到下一行中
            mCodeData[pos.Row].RemoveRange(pos.Col + 1, mCodeData[pos.Row].Count - pos.Col - 1);
            mCodeData.Insert(pos.Row + 1, newLineBytes);

            // 分析这两行
            mCodeLines[pos.Row] = mParser.ParseLine(pos.Row);
            mCodeLines.Insert(pos.Row + 1, mParser.ParseLine(pos.Row + 1));

            mCoder.SetCursorPos(new URank(pos.Row + 1, mCodeLines[pos.Row + 1].VisibleStartCol));
        }

        /// <summary>
        /// 删除指定位置的换行符
        /// </summary>
        /// <param name="pos"></param>
        public void RemoveNewlineByte(URank pos)
        {
            if (pos.Row < 0 || pos.Row >= mCodeData.Count)
            {
                throw new Exception("RemoveNewLineByte : Row Invalid {" + pos.Row.ToString() + "}");
            }

            if (pos.Col < 0 || pos.Col >= mCodeData[pos.Row].Count)
            {
                throw new Exception("RemoveNewLineByte : Col Invalid {" + pos.Col.ToString() + "}");
            }

            if (mCodeData[pos.Row][pos.Col] != UConfig.NewLine)
            {
                return;
            }

            // 删除当前的的换行符后，如果还有下一行，则合并到当前行
            if (pos.Row + 1 < mCodeData.Count)
            {
                // 如果下一行有可见字符，才合并
                // 否则的话，就直接删除下一行就行了
                if (mCodeLines[pos.Row + 1].HasVisibleCharacter)
                {
                    mCodeData[pos.Row].RemoveAt(pos.Col);
                    mCodeData[pos.Row].AddRange(mCodeData[pos.Row + 1]);
                    mCodeLines[pos.Row] = mParser.ParseLine(pos.Row);
                }

                mCodeData.RemoveAt(pos.Row + 1);
                mCodeLines.RemoveAt(pos.Row + 1);

                mCoder.SetCursorPos(new URank(pos.Row, pos.Col));
            }
        }
        
        /// <summary>
        /// 在指定位置插入一个字符
        /// </summary>
        /// <param name="pos">开始位置</param>
        /// <param name="b">要插入的字符</param>
        public void InsertCharacter(URank pos, byte b)
        {
            if (pos.Row < 0 || pos.Row >= mCodeData.Count)
            {
                throw new Exception("InsertCharacter : Row Invalid {" + pos.Row.ToString() + "}");
            }

            if (pos.Col < 0 || pos.Col >= mCodeData[pos.Row].Count )
            {
                throw new Exception("InsertCharacter : Col Invalid {" + pos.Col.ToString() + "}");
            }

            mCodeData[pos.Row].Insert(pos.Col, b);

            mCodeLines[pos.Row] = mParser.ParseLine(pos.Row);

            mCoder.SetCursorPos(new URank(pos.Row, pos.Col + 1));
        }

        /// <summary>
        /// 删除指定位置的一个字符
        /// </summary>
        /// <param name="pos">开始位置</param>
        public void RemoveCharacter(URank pos)
        {
            if (pos.Row < 0 || pos.Row >= mCodeData.Count)
            {
                throw new Exception("RemoveCharacter : Row Invalid {" + pos.Row.ToString() + "}");
            }

            if (pos.Col < 0 || pos.Col >= mCodeData[pos.Row].Count)
            {
                throw new Exception("RemoveCharacter : Col Invalid {" + pos.Col.ToString() + "}");
            }

            if (UHelper.IsChinesePart(mCodeData[pos.Row].ToArray(), pos.Col))
            {
                mCodeData[pos.Row].RemoveRange(pos.Col - 1, 2);
                mCoder.SetCursorPos(new URank(pos.Row, pos.Col - 2));
            }
            else
            {
                mCodeData[pos.Row].RemoveAt(pos.Col);
                mCoder.SetCursorPos(new URank(pos.Row, pos.Col));
            }

            mCodeLines[pos.Row] = mParser.ParseLine(pos.Row);
        }

        /// <summary>
        /// 在指定位置插入一个字符串
        /// </summary>
        /// <param name="pos">开始位置</param>
        /// <param name="text">要插入的字符串</param>
        public void InsertString(URank pos, string str)
        {
            if (pos.Row < 0 || pos.Row >= mCodeData.Count)
            {
                throw new Exception("InsertString : Row Invalid {" + pos.Row.ToString() + "}");
            }

            if (pos.Col < 0 || pos.Col >= mCodeData[pos.Row].Count )
            {
                throw new Exception("InsertString : Col Invalid {" + pos.Col.ToString() + "}");
            }

            mCodeData[pos.Row].InsertRange(pos.Col, UHelper.GetBytesByString(str));

            mCodeLines[pos.Row] = mParser.ParseLine(pos.Row);

            mCoder.SetCursorPos(new URank(pos.Row, pos.Col + UHelper.GetAbsoluteLength(str)));
        }

        /// <summary>
        /// 删除指定位置的一个字符串
        /// </summary>
        /// <param name="pos">开始位置</param>
        /// <param name="len">长度</param>
        public void RemoveString(URank pos, int len)
        {
            if (pos.Row < 0 || pos.Row >= mCodeData.Count)
            {
                throw new Exception("RemoveString : Row Invalid {" + pos.Row.ToString() + "}");
            }

            if (pos.Col < 0 || pos.Col + len >= mCodeData[pos.Row].Count)
            {
                throw new Exception("RemoveString : Col Invalid {" + pos.Col.ToString() + "}");
            }

            mCodeData[pos.Row].RemoveRange(pos.Col, len);

            mCodeLines[pos.Row] = mParser.ParseLine(pos.Row);

            mCoder.SetCursorPos(pos);
        }

        /// <summary>
        /// 替换指定位置的字符
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="b"></param>
        public void ReplaceCharacter(URank pos, byte b)
        {
            RemoveCharacter(pos);
            InsertCharacter(pos, b);
        }

        /// <summary>
        /// 替换指定位置的字符串
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="len"></param>
        /// <param name="str"></param>
        public void ReplaceString(URank pos, int len, string str)
        {
            RemoveString(pos, len);
            InsertString(pos, str);
        }

        /// <summary>
        /// 插入多行字符串
        /// </summary>
        /// <param name="start">开始位置</param>
        /// <param name="rangeStr">字符串</param>
        public void InsertRangeString(URank start, string rangeStr)
        {
            int row = start.Row;
            int col = start.Col;

            byte[] data = UHelper.TanslateString(rangeStr);

            for (int i = 0; i < data.Length; i++)
            {
                mCodeData[row].Insert(col, data[i]);
                col++;

                if (data[i] == UConfig.NewLine)
                {
                    // 遇到换行符，首先剪切之后的数据到下一行
                    List<byte> newLine = new List<byte>();

                    for (int n = col; n < mCodeData[row].Count; n++)
                    {
                        newLine.Add(mCodeData[row][n]);
                    }

                    mCodeData[row].RemoveRange(col, mCodeData[row].Count - col);
                    mCodeLines[row] = mParser.ParseLine(row);

                    row++;
                    col = 0;

                    mCodeData.Insert(row, newLine);
                    mCodeLines.Insert(row, new UCodeLine());
                    mCodeLines[row] = mParser.ParseLine(row);

                    continue;
                }

            }

            mCodeLines[row] = mParser.ParseLine(row);
            mCoder.SetCursorPos(new URank(row, col));
        }

        /// <summary>
        /// 删除指定范围的字符串
        /// </summary>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        public void RemoveRangeString(URank start, URank end)
        {
            try
            {
                int startRow = start.Row;
                int startCol = start.Col;
                int endRow = end.Row;
                int endCol = end.Col;

                if (start == end)
                {
                    return;
                }

                if (startRow == endRow)
                {
                    for (int i = startCol; i < endCol; i++)
                    {
                        //RemoveCharacter(new URank(startRow, startCol));
                        mCodeData[startRow].RemoveAt(startCol);
                    }

                    mCodeLines[startRow] = mParser.ParseLine(startRow);
                    mCoder.SetCursorPos(start);

                    return;
                }
                else
                {
                    for (int i = 0; i < endCol; i++)
                    {
                        //RemoveCharacter(new URank(endRow, 0));
                        mCodeData[endRow].RemoveAt(0);
                    }
                    mCodeLines[endRow] = mParser.ParseLine(endRow);

                    for (int i = startRow + 1; i < endRow; i++)
                    {
                        mCodeData.RemoveAt(startRow + 1);
                        mCodeLines.RemoveAt(startRow + 1);
                    }

                    int len = GetCodeLength(startRow);
                    for (int i = startCol; i < len - 1; i++)
                    {
                        //RemoveCharacter(new URank(startRow, startCol));
                        mCodeData[startRow].RemoveAt(startCol);
                    }
                    RemoveNewlineByte(new URank(startRow, GetCodeLength(startRow) - 1));

                    mCodeLines[startRow] = mParser.ParseLine(startRow);
                    mCoder.SetCursorPos(start);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
