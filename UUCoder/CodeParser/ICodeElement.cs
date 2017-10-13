using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeParser
{
    /// <summary>
    /// 抽象的单个元素
    /// </summary>
    public interface ICodeElement
    {
        void Render(CodeRender.IRenderer renderer);
    }
}
