using System;
using System.Collections.Generic;
using System.Text;

using UUCoder.CodeBase;

namespace UUCoder.CodeParser
{
    /// <summary>
    /// 代码行
    /// 保存一行代码（包括若干CodeCut）
    /// </summary>
    public class UCodeLine
    {
        private List<UCodeCut> mCuts;
        private List<int> mCutStartColList; // 每个Cut开始的列位置
        private int mCutStartCol = 0;

        /// <summary>
        /// 可见代码在这一行的开始位置
        /// </summary>
        public int VisibleStartCol { get; private set; }

        /// <summary>
        /// 这一行是否有可见字符
        /// </summary>
        public bool HasVisibleCharacter { get; private set; }

        public UCodeLine()
        {
            this.mCuts = new List<UCodeCut>();
            this.mCutStartColList = new List<int>();
            this.mCutStartCol = 0;

            this.VisibleStartCol = 0;
            this.HasVisibleCharacter = false;
        }

        public int GetCutCount()
        {
            return mCuts.Count;
        }

        public void AddCut(UCodeCut cut)
        {
            int len = UHelper.GetAbsoluteLength(cut.Data);

            if (HasVisibleCharacter == false && cut.CutType == UCutType.Space)
            {
                VisibleStartCol = VisibleStartCol + len;// UHelper.GetAbsoluteLength(cut.Data);
            }
            else
            {
                if (cut.CutType != UCutType.NewLine)
                {
                    HasVisibleCharacter = true;
                }
            }

            mCutStartColList.Add(mCutStartCol);
            mCuts.Add(cut);
            mCutStartCol += len;// UHelper.GetAbsoluteLength(cut.Data);
        }

        /// <summary>
        /// 范围指定列数所在的CodeCut
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public UCodeCut GetCutByCol(int col)
        {
            if (col < 0 || col >= mCutStartCol)
            {
                return null;
            }

            for (int i = mCutStartColList.Count - 1; i >= 0; i--)
            {
                if (col >= mCutStartColList[i])
                {
                    return mCuts[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 返回指定索引的CodeCut
        /// </summary>
        /// <returns></returns>
        public UCodeCut GetCutByIndex(int index)
        {
            if (index < 0 || index >= mCuts.Count)
            {
                throw new Exception("GetCutByIndex : Index Invalid {" + index.ToString() + "}");
            }

            return mCuts[index];
        }
    }
}
