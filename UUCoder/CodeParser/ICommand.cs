using System;
using System.Collections.Generic;
using System.Text;

using UUCoder.CodeBase;

namespace UUCoder.CodeParser
{
    /// <summary>
    /// 命令的统一接口
    /// </summary>
    public interface ICommand
    {
        void Execute();
    }

    /// <summary>
    /// 可撤销命令的接口
    /// </summary>
    public interface IUndoCommand : ICommand
    {
        void Undo();
    }

    public class InsertCharacterCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mPosition;
        private byte mInsertCharacter;

        public InsertCharacterCommand(UCodeManager codeManager, URank pos, byte ch)
        {
            this.mCodeManager = codeManager;
            this.mPosition = pos;
            this.mInsertCharacter = ch;
        }

        public void Execute()
        {
            mCodeManager.InsertCharacter(mPosition, mInsertCharacter);
        }

        public void Undo()
        {
            mCodeManager.RemoveCharacter(mPosition);
        }
    }

    public class RemoveCharacterCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mPosition;
        private byte mRemovedCharacter;

        public RemoveCharacterCommand(UCodeManager codeManager, URank pos)
        {
            this.mCodeManager = codeManager;
            this.mPosition = pos;
            this.mRemovedCharacter = 0;
        }

        public void Execute()
        {
            mRemovedCharacter = mCodeManager.GetCodeByte(mPosition);
            mCodeManager.RemoveCharacter(mPosition);
        }

        public void Undo()
        {
            mCodeManager.InsertCharacter(mPosition, mRemovedCharacter);
        }
    }

    public class InsertStringCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mPosition;
        private string mInsertString;

        public InsertStringCommand(UCodeManager codeManager, URank pos, string str)
        {
            this.mCodeManager = codeManager;
            this.mPosition = pos;
            this.mInsertString = str;
        }

        public void Execute()
        {
            mCodeManager.InsertString(mPosition, mInsertString);
        }

        public void Undo()
        {
            mCodeManager.RemoveString(mPosition, UHelper.GetAbsoluteLength(mInsertString));
        }
    }

    public class RemoveStringCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mPosition;
        private int mLength;
        private string mRemoveString;

        public RemoveStringCommand(UCodeManager codeManager, URank pos, int len)
        {
            this.mCodeManager = codeManager;
            this.mPosition = pos;
            this.mLength = len;
            this.mRemoveString = string.Empty;
        }

        public void Execute()
        {
            mRemoveString = mCodeManager.GetCodeString(mPosition, mLength);
            mCodeManager.RemoveString(mPosition, mLength);
        }

        public void Undo()
        {
            mCodeManager.InsertString(mPosition, mRemoveString);
        }
    }

    public class InsertNewLineByteCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mPosition;

        public InsertNewLineByteCommand(UCodeManager codeManager, URank pos)
        {
            this.mCodeManager = codeManager;
            this.mPosition = pos;
        }

        public void Execute()
        {
            mCodeManager.InsertNewLineByte(mPosition);
        }

        public void Undo()
        {
            mCodeManager.RemoveNewlineByte(mPosition);
        }
    }

    public class RemoveNewlineByteCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mPosition;

        public RemoveNewlineByteCommand(UCodeManager codeManager, URank pos)
        {
            this.mCodeManager = codeManager;
            this.mPosition = pos;
        }

        public void Execute()
        {
            mCodeManager.RemoveNewlineByte(mPosition);
        }

        public void Undo()
        {
            mCodeManager.InsertNewLineByte(mPosition);
        }
    }

    public class ReplaceCharacterCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mPosition;
        private byte mNewCharacter;
        private byte mOldCharacter;

        public ReplaceCharacterCommand(UCodeManager codeManager, URank pos, byte newCh)
        {
            this.mCodeManager = codeManager;
            this.mPosition = pos;
            this.mNewCharacter = newCh;
            this.mOldCharacter = 0;
        }

        public void Execute()
        {
            mOldCharacter = mCodeManager.GetCodeByte(mPosition);

            mCodeManager.ReplaceCharacter(mPosition, mNewCharacter);
        }

        public void Undo()
        {
            mCodeManager.ReplaceCharacter(mPosition, mOldCharacter);
        }
    }

    public class ReplaceStringCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mPosition;
        private int mLength;
        private string mNewString;
        private string mOldString;

        public ReplaceStringCommand(UCodeManager codeManager, URank pos, int len, string newString)
        {
            this.mCodeManager = codeManager;
            this.mPosition = pos;
            this.mLength = len;
            this.mNewString = newString;
            this.mOldString = string.Empty;
        }

        public void Execute()
        {
            mOldString = mCodeManager.GetCodeCut(mPosition).Data;

            mCodeManager.ReplaceString(mPosition, mLength, mNewString);
        }

        public void Undo()
        {
            mCodeManager.ReplaceString(mPosition, mNewString.Length, mOldString);
        }
    }

    public class InsertRangeStringCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mStart;
        private URank mEnd;
        private string mInsertString;

        public InsertRangeStringCommand(UCodeManager codeManager, URank start, string insertString)
        {
            this.mCodeManager = codeManager;
            this.mStart = start;
            this.mEnd = start;
            this.mInsertString = insertString;

            byte[] data = UHelper.TanslateString(insertString);

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == UConfig.NewLine)
                {
                    this.mEnd.Row++;
                    this.mEnd.Col = 0;
                    continue;
                }

                this.mEnd.Col++;
            }
        }

        public void Execute()
        {
            mCodeManager.InsertRangeString(mStart, mInsertString);
        }

        public void Undo()
        {
            mCodeManager.RemoveRangeString(mStart, mEnd);
        }
    }

    public class RemoveRangeStringCommand : IUndoCommand
    {
        private UCodeManager mCodeManager;
        private URank mStart;
        private URank mEnd;
        private string mRemoveString;

        public RemoveRangeStringCommand(UCodeManager codeManager, URank start, URank end)
        {
            this.mCodeManager = codeManager;
            this.mStart = start;
            this.mEnd = end;
            this.mRemoveString = string.Empty;
        }

        public void Execute()
        {
            mRemoveString = mCodeManager.GetCodeString(mStart, mEnd);

            mCodeManager.RemoveRangeString(mStart, mEnd);
        }

        public void Undo()
        {
            mCodeManager.InsertRangeString(mStart, mRemoveString);
        }
    }
}
