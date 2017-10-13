using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace UUCoder.CodeBase
{
    /// <summary>
    /// 静态类，配置编辑器的参数
    /// 可从配置文件读取参数
    /// </summary>
    public static class UConfig
    {
        /// <summary>
        /// 字体
        /// </summary>
        public static Font EditorFont = new Font("新宋体", 10);

        /// <summary>
        /// 所有的符号列表
        /// </summary>
        public static char[] Symbols = new char[]
        {
            '+', '-', '*', '/',
            '>', '<', '!', '=',
            '(', ')', '[', ']', '{', '}',
            '\'', '"', ';', ',', '.', ':',
            '@', '#', '$', '%', '^', '&', '_', '|', '\\'
        };

        /// <summary>
        /// 关键字列表
        /// </summary>
        public static string[] KeyWords = new string[]
        {
            "void", "char", "int", "long", "float", "double", "string", "bool",
            "if", "else", "break", "continue", "while", "for", "return",
            "public", "private", "protected", "this", "new", "override",
            "static", "class", "interface", "using", "namespace"
        };

        public static UColor[] KeyWordsColor = new UColor[]
        {
            UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green,
            UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green,
            UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green,
            UColor.Green, UColor.Green, UColor.Green, UColor.Green, UColor.Green,
        };

        /// <summary>
        /// 定义函数的字符串
        /// </summary>
        public static string[] VariableType = new string[]
        {
            "void", "char", "int", "long", "float", "double", "string", "bool"
        };

        /// <summary>
        /// 定义类的字符串
        /// </summary>
        public static string ClassString = "class";

        /// <summary>
        /// 换行符 \n
        /// </summary>
        public static byte NewLine = 10;

        /// <summary>
        /// 回车符 \r
        /// </summary>
        public static byte Enter = 13;

        /// <summary>
        /// 空白符 ' '
        /// </summary>
        public static byte Space = 32;

        /// <summary>
        /// Tab符 \t
        /// </summary>
        public static byte Tab = 9;

        /// <summary>
        /// 单引号符 '
        /// </summary>
        public static byte Quote = 39;

        /// <summary>
        /// 双引号符 "
        /// </summary>
        public static byte DoubleQuote = 34;

        /// <summary>
        /// 斜杠 \
        /// </summary>
        public static byte Slash = 92;
        /// <summary>
        /// 反斜杠 /
        /// </summary>
        public static byte BackSlash = 47;

        /// <summary>
        /// 左括号 {
        /// </summary>
        public static byte LBracket = 123;
        /// <summary>
        /// 右括号 {
        /// </summary>
        public static byte RBracket = 125;

        public static string TabString = "    ";

        /// <summary>
        /// 一个Tab相当于多少个Space
        /// </summary>
        public static int TabNumberOfSpace = 4;

        public static void LoadConfigFromFile(string filePath)
        {
            try
            {
                CodeElement.UDataDocument doc = new CodeElement.UDataDocument();

                doc.Load(filePath);

                #region 环境设置
                string[] fontParam = doc.Items[0].Items[1].Value.Split(',');
                EditorFont = new Font(fontParam[0], float.Parse(fontParam[1]));
                #endregion

                #region 编辑器设置
                foreach (CodeElement.UDataElement element in doc.Items[1].Items)
                {
                    switch (element.Name)
                    {
                        case "Tab":
                            Tab = byte.Parse(element.Value);
                            break;
                        case "NewLine":
                            NewLine = byte.Parse(element.Value);
                            break;
                        case "Enter":
                            Enter = byte.Parse(element.Value);
                            break;
                        case "Space":
                            Space = byte.Parse(element.Value);
                            break;
                        case "DoubleQuote":
                            DoubleQuote = byte.Parse(element.Value);
                            break;
                        case "Quote":
                            Quote = byte.Parse(element.Value);
                            break;
                        case "BackSlash":
                            BackSlash = byte.Parse(element.Value);
                            break;
                        case "Slash":
                            Slash = byte.Parse(element.Value);
                            break;
                        case "LBracket":
                            LBracket = byte.Parse(element.Value);
                            break;
                        case "RBracket":
                            RBracket = byte.Parse(element.Value);
                            break;
                        case "TabNumberOfSpace":
                            TabNumberOfSpace = int.Parse(element.Value);
                            TabString = string.Empty;

                            for (int i = 0; i < TabNumberOfSpace; i++)
                            {
                                TabString = TabString + " ";
                            }
                            break;
                        default:
                            break;
                    }
                }
                #endregion

                #region 语言设置
                foreach (CodeElement.UDataElement element in doc.Items[2].Items[0].Items)
                {
                    switch (element.Name)
                    {
                        case "ClassString":
                            ClassString = element.Value;
                            break;
                        case "Normal":
                            CodeParser.UCutType.Normal.CutColor = UColor.Parse(element.Value);
                            break;
                        case "String":
                            CodeParser.UCutType.String.CutColor = UColor.Parse(element.Value);
                            break;
                        case "Digit":
                            CodeParser.UCutType.Digit.CutColor = UColor.Parse(element.Value);
                            break;
                        case "ClassName":
                            CodeParser.UCutType.ClassName.CutColor = UColor.Parse(element.Value);
                            break;
                        case "FunctionName":
                            CodeParser.UCutType.FunctionName.CutColor = UColor.Parse(element.Value);
                            break;
                        case "VariableName":
                            CodeParser.UCutType.VariableName.CutColor = UColor.Parse(element.Value);
                            break;
                        case "Annotation":
                            CodeParser.UCutType.Annotation.CutColor = UColor.Parse(element.Value);
                            break;
                        default:
                            break;
                    }
                }
                #endregion

                #region 关键字设置
                List<string> keywords = new List<string>();
                List<UColor> keywordsColor = new List<UColor>();
                foreach (CodeElement.UDataElement element in doc.Items[2].Items[0].Items[8].Items)
                {
                    keywords.Add(element.Name);
                    keywordsColor.Add(UColor.Parse(element.Value));
                }
                KeyWords = keywords.ToArray();
                KeyWordsColor = keywordsColor.ToArray();
                #endregion

                #region 符号表设置
                #endregion

                #region 变量类型设置
                List<string> variableType = new List<string>();
                foreach (CodeElement.UDataElement element in doc.Items[2].Items[0].Items[10].Items)
                {
                    variableType.Add(element.Name);
                }
                VariableType = variableType.ToArray();
                #endregion

                doc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
