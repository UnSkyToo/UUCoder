using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeParser
{
    /// <summary>
    /// 代码的最小片段（符号、关键字、字符串等）
    /// 包括切片的内容和切片的类型
    /// </summary>
    public class UCodeCut
    {
        /// <summary>
        /// 代码片段的内容
        /// </summary>
        public string Data;

        /// <summary>
        /// 片段的类型
        /// </summary>
        public UCutType CutType;

        public UCodeCut()
        {
            Data = string.Empty;
            
            CutType = UCutType.Normal;
        }

        public UCodeCut(UCodeCut cut)
        {
            this.CutType = cut.CutType;
            this.Data = cut.Data;
        }
    }
}
