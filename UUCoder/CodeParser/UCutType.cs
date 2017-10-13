using System;
using System.Collections.Generic;
using System.Text;

using UUCoder.CodeBase;

namespace UUCoder.CodeParser
{
    /// <summary>
    /// 片段的类型
    /// </summary>
    public class UCutType
    {
        public static UCutType End = new UCutType(-2, UColor.White, "End");
        public static UCutType None = new UCutType(-1, UColor.White, "None");

        public static UCutType Normal = new UCutType(0, UColor.White, "Normal");
        public static UCutType String = new UCutType(1, UColor.Red, "String");
        public static UCutType KeyWord = new UCutType(2, UColor.Green, "Key");
        public static UCutType Symbol = new UCutType(3, UColor.Yellow, "Symbol");
        public static UCutType Operator = new UCutType(4, UColor.White, "Operator");
        public static UCutType Digit = new UCutType(5, UColor.Cyan, "Digit");

        public static UCutType ClassName = new UCutType(6, UColor.Cyan, "ClassName");
        public static UCutType FunctionName = new UCutType(7, UColor.Cyan, "FunctionName");
        public static UCutType VariableName = new UCutType(8, UColor.Cyan, "VariableName");

        public static UCutType Annotation = new UCutType(100, UColor.Gray, "Annotation");

        public static UCutType NewLine = new UCutType(250, UColor.White, "NewLine");
        public static UCutType Space = new UCutType(251, UColor.White, "Space");
        public static UCutType Tab = new UCutType(252, UColor.White, "Tab");

        public static UCutType Error = new UCutType(255, UColor.Red, "Error");
        
        private int TypeId;
        private string Name;

        public UColor CutColor;

        public UCutType(int id, UColor color, string name)
        {
            this.TypeId = id;
            this.CutColor = color;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            UCutType type = obj as UCutType;

            if (object.Equals(type, null))
            {
                return false;
            }

            return this.TypeId == type.TypeId;
        }

        public override int GetHashCode()
        {
            return this.TypeId;
        }

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(UCutType type1, UCutType type2)
        {
            if (object.Equals(type1, null) || object.Equals(type2, null))
            {
                return object.Equals(type1, type2);
            }

            return type1.TypeId == type2.TypeId;
        }

        public static bool operator !=(UCutType type1, UCutType type2)
        {
            if (object.Equals(type1, null) || object.Equals(type2, null))
            {
                return !object.Equals(type1, type2);
            }

            return type1.TypeId != type2.TypeId;
        }
    }
}
