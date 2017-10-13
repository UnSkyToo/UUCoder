using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodePlugin
{
    public abstract class IPlugin : CodeParser.ICodeElement
    {
        public UUCoder Coder { get; private set; }

        public IPlugin(UUCoder coder)
        {
            this.Coder = coder;
        }

        public abstract void Initialize();
        public abstract void Reset();
        public abstract void Render(CodeRender.IRenderer renderer);
    }
}
