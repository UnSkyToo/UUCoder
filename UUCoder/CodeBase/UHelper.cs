using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeBase
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class UHelper
    {
        /// <summary>
        /// 字符是否为字母
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsCharacter(byte b)
        {
            char ch = (char)b;

            if (ch >= 'a' && ch <= 'z')
            {
                return true;
            }
            if (ch >= 'A' && ch <= 'Z')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 字符是否为数字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsDigit(byte b)
        {
            if (b >= (byte)'0' && b <= (byte)'9')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 字符是否为空白符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsBlank(byte b)
        {
            if (b == UConfig.Space || b == UConfig.Enter || b == UConfig.Tab || b == UConfig.NewLine)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 字符是否为符号表中的符号
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsSymbol(byte b)
        {
            int len = UConfig.Symbols.Length;

            for (int i = 0; i < len; i++)
            {
                if (b == UConfig.Symbols[i])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 字符串是否为关键字
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool IsKeyWord(string word)
        {
            int len = UConfig.KeyWords.Length;

            for (int i = 0; i < len; i++)
            {
                if (word == UConfig.KeyWords[i])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 字符是否为片段结束符号（如逗号、换号、括号等等）
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsCutEnd(byte b)
        {
            if (IsSymbol(b))
            {
                return true;
            }

            /*
            if (ch == ' ')
            {
                return true;
            }

            if (ch == '\n' || ch == '\r' || ch == '\t')
            {
                return true;
            }*/

            if (IsBlank(b))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断指定位置的字符是否为中文的一部分
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool IsChinesePart(byte[] bytes, int index)
        {
            int len = bytes.Length;
            int count = index;

            if (index >= len)
            {
                return false;
            }

            for (int i = 0; i < index; i++)
            {
                if (bytes[i] < 128)
                {
                    count--;
                }
            }

            return (count % 2) == 1 ? true : false;
        }

        public static char ParseKey(UKeyEvents e)
        {
            switch (e.KeyCode)
            {
                case UKeys.A:
                    return e.Shift == true ? 'A' : 'a';
                case UKeys.B:
                    return e.Shift == true ? 'B' : 'b';
                case UKeys.C:
                    return e.Shift == true ? 'C' : 'c';
                case UKeys.D:
                    return e.Shift == true ? 'D' : 'd';
                case UKeys.E:
                    return e.Shift == true ? 'E' : 'e';
                case UKeys.F:
                    return e.Shift == true ? 'F' : 'f';
                case UKeys.G:
                    return e.Shift == true ? 'G' : 'g';
                case UKeys.H:
                    return e.Shift == true ? 'H' : 'h';
                case UKeys.I:
                    return e.Shift == true ? 'I' : 'i';
                case UKeys.J:
                    return e.Shift == true ? 'J' : 'j';
                case UKeys.K:
                    return e.Shift == true ? 'K' : 'k';
                case UKeys.L:
                    return e.Shift == true ? 'L' : 'l';
                case UKeys.M:
                    return e.Shift == true ? 'M' : 'm';
                case UKeys.N:
                    return e.Shift == true ? 'N' : 'n';
                case UKeys.O:
                    return e.Shift == true ? 'O' : 'o';
                case UKeys.P:
                    return e.Shift == true ? 'P' : 'p';
                case UKeys.Q:
                    return e.Shift == true ? 'Q' : 'q';
                case UKeys.R:
                    return e.Shift == true ? 'R' : 'r';
                case UKeys.S:
                    return e.Shift == true ? 'S' : 's';
                case UKeys.T:
                    return e.Shift == true ? 'T' : 't';
                case UKeys.U:
                    return e.Shift == true ? 'U' : 'u';
                case UKeys.V:
                    return e.Shift == true ? 'V' : 'v';
                case UKeys.W:
                    return e.Shift == true ? 'W' : 'w';
                case UKeys.X:
                    return e.Shift == true ? 'X' : 'x';
                case UKeys.Y:
                    return e.Shift == true ? 'Y' : 'y';
                case UKeys.Z:
                    return e.Shift == true ? 'Z' : 'z';
                case UKeys.D1:
                    return e.Shift == true ? '!' : '1';
                case UKeys.D2:
                    return e.Shift == true ? '@' : '2';
                case UKeys.D3:
                    return e.Shift == true ? '#' : '3';
                case UKeys.D4:
                    return e.Shift == true ? '$' : '4';
                case UKeys.D5:
                    return e.Shift == true ? '%' : '5';
                case UKeys.D6:
                    return e.Shift == true ? '^' : '6';
                case UKeys.D7:
                    return e.Shift == true ? '&' : '7';
                case UKeys.D8:
                    return e.Shift == true ? '*' : '8';
                case UKeys.D9:
                    return e.Shift == true ? '(' : '9';
                case UKeys.D0:
                    return e.Shift == true ? ')' : '0';
                case UKeys.Tilde:
                    return e.Shift == true ? '~' : '`';
                case UKeys.Minus:
                    return e.Shift == true ? '_' : '-';
                case UKeys.Plus:
                    return e.Shift == true ? '+' : '=';
                case UKeys.Pipeline:
                    return e.Shift == true ? '|' : '\\';
                case UKeys.LBracket:
                    return e.Shift == true ? '{' : '[';
                case UKeys.RBracket:
                    return e.Shift == true ? '}' : ']';
                case UKeys.Semicolon:
                    return e.Shift == true ? ':' : ';';
                case UKeys.Quote:
                    return e.Shift == true ? '"' : '\'';
                case UKeys.Comma:
                    return e.Shift == true ? '<' : ',';
                case UKeys.Period:
                    return e.Shift == true ? '>' : '.';
                case UKeys.Question:
                    return e.Shift == true ? '?' : '/';
                case UKeys.Space:
                    return ' ';
                case UKeys.Numpad0:
                    return '0';
                case UKeys.Numpad1:
                    return '1';
                case UKeys.Numpad2:
                    return '2';
                case UKeys.Numpad3:
                    return '3';
                case UKeys.Numpad4:
                    return '4';
                case UKeys.Numpad5:
                    return '5';
                case UKeys.Numpad6:
                    return '6';
                case UKeys.Numpad7:
                    return '7';
                case UKeys.Numpad8:
                    return '8';
                case UKeys.Numpad9:
                    return '9';
            }

            return (char)0;
        }

        public static UColor GetKeyWordColor(string name)
        {
            for (int i = 0; i < UConfig.KeyWords.Length; i++)
            {
                if (name == UConfig.KeyWords[i])
                {
                    return UConfig.KeyWordsColor[i];
                }
            }

            return UColor.White;
        }

        /// <summary>
        /// 取字符串的绝对长度（汉字算两个）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static int GetAbsoluteLength(string str)
        {
            /*int len = 0;

            for (int i = 0; i < code.Length; i++)
            {
                if (IsChinese(code[i]))
                {
                    len = len + 2;
                }
                else
                {
                    len = len + 1;
                }
            }

            return len;*/

            return GetBytesByString(str).Length;
        }

        /// <summary>
        /// 把字符串转为Byte数组
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] GetBytesByString(string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        /// <summary>
        /// 把Byte数组转为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string GetStringByBytes(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// 把字符串翻译为编辑器能设别的数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] TanslateString(string str)
        {
            List<byte> source = new List<byte>();
            List<byte> data = new List<byte>();

            source.AddRange(GetBytesByString(str));

            for (int i = 0; i < source.Count; i++)
            {
                if (source[i] == UConfig.Enter)
                {
                    continue;
                }
                if (source[i] == UConfig.Tab)
                {
                    for ( int n = 0; n < UConfig.TabNumberOfSpace; n++)
                    {
                        data.Add(UConfig.Space);
                    }

                    continue;
                }

                data.Add(source[i]);
            }

            return data.ToArray();
        }

        /// <summary>
        /// 在字符串中查找指定字符首次出现的位置
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ch"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int FindIndexOf(string str, char ch, int start)
        {
            int len = str.Length;

            for (int i = start; i < len; i++)
            {
                if (str[i] == ch)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 在字符串中查找指定字符首次出现的位置
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int FindIndexOf(string str, char ch)
        {
            return FindIndexOf(str, ch, 0);
        }

        /// <summary>
        /// 在字符串中查找指定字符最后出现的位置
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ch"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int FindLastOf(string str, char ch, int start)
        {
            if (start >= str.Length || start < 0)
            {
                return -1;
            }

            for (int i = start; i >= 0; i--)
            {
                if (str[i] == ch)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 在字符串中查找指定字符最后出现的位置
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int FindLastOf(string str, char ch)
        {
            return FindLastOf(str, ch, str.Length - 1);
        }
    }
}
