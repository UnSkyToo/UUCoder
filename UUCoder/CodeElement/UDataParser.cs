using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UUCoder.CodeElement
{
    public class UDataParser
    {
        private List<string> mData;
        private int mRow;
        private int mCol;
        private int mOldRow;
        private int mOldCol;

        private int mSaveRow;
        private int mSaveCol;
        private int mSaveOldRow;
        private int mSaveOldCol;

        private bool mEndOfData;

        public UDataParser()
        {
            this.mData = new List<string>();
            this.mRow = 0;
            this.mCol = 0;
            this.mOldRow = 0;
            this.mOldCol = 0;
            this.mEndOfData = false;
        }

        public void Load(string filePath)
        {
            try
            {
                StreamReader fp = new StreamReader(filePath, Encoding.Default);
                string line = string.Empty;

                this.mData.Clear();
                this.mRow = 0;
                this.mCol = 0;
                this.mOldRow = 0;
                this.mOldCol = 0;
                this.mEndOfData = false;

                while (!fp.EndOfStream)
                {
                    line = fp.ReadLine();

                    if (line == string.Empty)
                    {
                        continue;
                    }

                    mData.Add(line);
                }

                fp.Close();
                fp.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNextData()
        {
            StringBuilder result = new StringBuilder();
            bool inString = false;

            while (!mEndOfData)
            {
                char ch = GetNextChar();

                if (ch == (char)0)
                {
                    break;
                }

                switch (ch)
                {
                    case '<':
                    case '>':
                    case '[':
                    case ']':
                    case ' ':
                    case '=':
                    case '/':
                        if (inString)
                        {
                            result.Append(ch);
                        }
                        else
                        {
                            if (result.Length == 0)
                            {
                                return ch.ToString();
                            }
                            else
                            {
                                BackToLast();

                                if (result[0] == '"')
                                {
                                    result.Remove(0, 1);
                                }
                                if (result[result.Length - 1] == '"')
                                {
                                    result.Remove(result.Length - 1, 1);
                                }

                                return result.ToString();
                            }
                        }
                        break;
                    case '\t':
                        break;
                    case '\n':
                        break;
                    case '"':
                        inString = !inString;
                        result.Append(ch);
                        break;
                    case '\\':
                        result.Append(GetNextChar());
                        break;
                    default:
                        result.Append(ch);
                        break;
                }
            }

            return result.ToString();
        }

        public string GetNextData(string data)
        {
            string str = GetNextData();

            if (str == data)
            {
                return str;
            }

            throw new Exception("Unexpect " + data);
        }

        public string PeekNextData()
        {
            int col = mCol;
            int row = mRow;
            int oldCol = mOldCol;
            int oldRow = mOldRow;

            string data = GetNextData();

            mCol = col;
            mRow = row;
            mOldCol = oldCol;
            mOldRow = oldRow;

            return data;
        }

        public void Save()
        {
            mSaveCol = mCol;
            mSaveRow = mRow;
            mSaveOldCol = mOldCol;
            mSaveOldRow = mOldRow;
        }

        public void Load()
        {
            mCol = mSaveCol;
            mRow = mSaveRow;
            mOldCol = mSaveOldCol;
            mOldRow = mSaveOldRow;
        }

        private char GetNextChar()
        {
            if (mEndOfData)
            {
                return (char)0;
            }

            mOldCol = mCol;
            mOldRow = mRow;

            if (mRow >= mData.Count)
            {
                mEndOfData = true;
                return (char)0;
            }

            if (mCol >= mData[mRow].Length)
            {
                mCol = 0;
                mRow++;

                if (mRow >= mData.Count)
                {
                    mEndOfData = true;
                    return (char)0;
                }
            }

            return mData[mRow][mCol++];
        }

        private void BackToLast()
        {
            mCol = mOldCol;
            mRow = mOldRow;
        }
    }
}
