using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeParser
{
    public interface IParser
    {
        void Reset();
        UCodeCut GetNextCut();
        UCodeLine ParseLine(int index);
    }
}
