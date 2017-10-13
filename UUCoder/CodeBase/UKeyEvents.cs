using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeBase
{
    public enum UKeys
    {
        None = 0,

        /// <summary>
        /// 退格键
        /// </summary>
        Back = 8,
        /// <summary>
        /// Tab键
        /// </summary>
        Tab = 9,
        /// <summary>
        /// 回车键
        /// </summary>
        Enter = 13,
        /// <summary>
        /// 大写锁定
        /// </summary>
        Caps = 14,

        /// <summary>
        /// F1键
        /// </summary>
        F1 = 15,
        /// <summary>
        /// F2键
        /// </summary>
        F2 = 16,
        /// <summary>
        /// F3键
        /// </summary>
        F3 = 17,
        /// <summary>
        /// F4键
        /// </summary>
        F4 = 18,
        /// <summary>
        /// F5键
        /// </summary>
        F5 = 19,
        /// <summary>
        /// F6键
        /// </summary>
        F6 = 20,
        /// <summary>
        /// F7键
        /// </summary>
        F7 = 21,
        /// <summary>
        /// F8键
        /// </summary>
        F8 = 22,
        /// <summary>
        /// F9键
        /// </summary>
        F9 = 23,
        /// <summary>
        /// F10键
        /// </summary>
        F10 = 24,
        /// <summary>
        /// F11键
        /// </summary>
        F11 = 25,
        /// <summary>
        /// F12键
        /// </summary>
        F12 = 26,

        /// <summary>
        /// Esc键
        /// </summary>
        Esc = 27,
        /// <summary>
        /// 空格键
        /// </summary>
        Space = 32,

        /// <summary>
        /// 插入建
        /// </summary>
        Insert = 33,
        /// <summary>
        /// 删除键
        /// </summary>
        Delete = 34,
        /// <summary>
        /// Home键
        /// </summary>
        Home = 35,
        /// <summary>
        /// End键
        /// </summary>
        End = 36,
        /// <summary>
        /// 翻页键
        /// </summary>
        PageUp = 37,
        /// <summary>
        /// 翻页键
        /// </summary>
        PageDown = 38,

        /// <summary>
        /// 向上键
        /// </summary>
        Up = 39,
        /// <summary>
        /// 向下键
        /// </summary>
        Down = 40,
        /// <summary>
        /// 向左键
        /// </summary>
        Left = 41,
        /// <summary>
        /// 向右键
        /// </summary>
        Right = 42,

        /// <summary>
        /// 左括号键
        /// </summary>
        LBracket = 44,
        /// <summary>
        /// 右括号键
        /// </summary>
        RBracket = 45,
        /// <summary>
        /// 斜杠键
        /// </summary>
        Pipeline = 46,
        /// <summary>
        /// 波浪键
        /// </summary>
        Tilde = 47,

        /// <summary>
        /// 0键
        /// </summary>
        D0 = 48,
        /// <summary>
        /// 1键
        /// </summary>
        D1 = 49,
        /// <summary>
        /// 2键
        /// </summary>
        D2 = 50,
        /// <summary>
        /// 3键
        /// </summary>
        D3 = 51,
        /// <summary>
        /// 4键
        /// </summary>
        D4 = 52,
        /// <summary>
        /// 5键
        /// </summary>
        D5 = 53,
        /// <summary>
        /// 6键
        /// </summary>
        D6 = 54,
        /// <summary>
        /// 7键
        /// </summary>
        D7 = 55,
        /// <summary>
        /// 8键
        /// </summary>
        D8 = 56,
        /// <summary>
        /// 9键
        /// </summary>
        D9 = 57,

        /// <summary>
        /// 减号键
        /// </summary>
        Minus = 58,
        /// <summary>
        /// 加号键
        /// </summary>
        Plus = 59,
        /// <summary>
        /// 逗号键
        /// </summary>
        Comma = 60,
        /// <summary>
        /// 句号键
        /// </summary>
        Period = 61,
        /// <summary>
        /// 问号键
        /// </summary>
        Question = 62,
        /// <summary>
        /// 分号键
        /// </summary>
        Semicolon = 63,
        /// <summary>
        /// 引号键
        /// </summary>
        Quote = 64,

        /// <summary>
        /// A键
        /// </summary>
        A = 65,
        /// <summary>
        /// B键
        /// </summary>
        B = 66,
        /// <summary>
        /// C键
        /// </summary>
        C = 67,
        /// <summary>
        /// D键
        /// </summary>
        D = 68,
        /// <summary>
        /// E键
        /// </summary>
        E = 69,
        /// <summary>
        /// F键
        /// </summary>
        F = 70,
        /// <summary>
        /// G键
        /// </summary>
        G = 71,
        /// <summary>
        /// H键
        /// </summary>
        H = 72,
        /// <summary>
        /// I键
        /// </summary>
        I = 73,
        /// <summary>
        /// J键
        /// </summary>
        J = 74,
        /// <summary>
        /// K键
        /// </summary>
        K = 75,
        /// <summary>
        /// L键
        /// </summary>
        L = 76,
        /// <summary>
        /// M键
        /// </summary>
        M = 77,
        /// <summary>
        /// N键
        /// </summary>
        N = 78,
        /// <summary>
        /// O键
        /// </summary>
        O = 79,
        /// <summary>
        /// P键
        /// </summary>
        P = 80,
        /// <summary>
        /// Q键
        /// </summary>
        Q = 81,
        /// <summary>
        /// R键
        /// </summary>
        R = 82,
        /// <summary>
        /// S键
        /// </summary>
        S = 83,
        /// <summary>
        /// T键
        /// </summary>
        T = 84,
        /// <summary>
        /// U键
        /// </summary>
        U = 85,
        /// <summary>
        /// V键
        /// </summary>
        V = 86,
        /// <summary>
        /// W键
        /// </summary>
        W = 87,
        /// <summary>
        /// X键
        /// </summary>
        X = 88,
        /// <summary>
        /// Y键
        /// </summary>
        Y = 89,
        /// <summary>
        /// Z键
        /// </summary>
        Z = 90,

        /// <summary>
        /// 小键盘0键
        /// </summary>
        Numpad0 = 91,
        /// <summary>
        /// 小键盘1键
        /// </summary>
        Numpad1 = 92,
        /// <summary>
        /// 小键盘2键
        /// </summary>
        Numpad2 = 93,
        /// <summary>
        /// 小键盘3键
        /// </summary>
        Numpad3 = 94,
        /// <summary>
        /// 小键盘4键
        /// </summary>
        Numpad4 = 95,
        /// <summary>
        /// 小键盘5键
        /// </summary>
        Numpad5 = 96,
        /// <summary>
        /// 小键盘6键
        /// </summary>
        Numpad6 = 97,
        /// <summary>
        /// 小键盘7键
        /// </summary>
        Numpad7 = 98,
        /// <summary>
        /// 小键盘8键
        /// </summary>
        Numpad8 = 99,
        /// <summary>
        /// 小键盘9键
        /// </summary>
        Numpad9 = 100,
        /// <summary>
        /// 小键盘小数点键
        /// </summary>
        NumpadPoint = 102,
        /// <summary>
        /// 小键盘回车键
        /// </summary>
        NumpadEnter = 103,
        /// <summary>
        /// 小键盘加号键
        /// </summary>
        NumpadPlus = 104,
        /// <summary>
        /// 小键盘减号键
        /// </summary>
        NumpadMinus = 105,
        /// <summary>
        /// 小键盘乘号键
        /// </summary>
        NumpadMultiply = 106,
        /// <summary>
        /// 小键盘除号键
        /// </summary>
        NumpadDivide = 107,

        /// <summary>
        /// 左Shift键
        /// </summary>
        LShift = 200,
        /// <summary>
        /// 右Shift键
        /// </summary>
        RShift = 201,
        /// <summary>
        /// 左Ctrl键
        /// </summary>
        LCtrl = 202,
        /// <summary>
        /// 右Ctrl键
        /// </summary>
        RCtrl = 203,
        /// <summary>
        /// 左Alt键
        /// </summary>
        LAlt = 204,
        /// <summary>
        /// 右Alt键
        /// </summary>
        RAlt = 205,
    }

    public class UKeyEvents
    {
        public bool Shift { get; private set; }
        public bool Ctrl { get; private set; }
        public bool Alt { get; private set; }

        public UKeys KeyCode { get; private set; }

        public UKeyEvents(UKeys key, bool shift = false, bool ctrl = false, bool alt = false)
        {
            this.KeyCode = key;
            this.Shift = shift;
            this.Ctrl = ctrl;
            this.Alt = alt;
        }
    }
}
