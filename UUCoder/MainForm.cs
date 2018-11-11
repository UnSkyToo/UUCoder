using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using UUCoder.CodeBase;

namespace UUCoder
{
    public partial class MainForm : Form
    {
        private UUCoder coder;
        private GDIRenderer render;

        private string mCurrFile = "";

        private UKeyEvents ParseKey(Keys key)
        {
            UKeys uk = UKeys.None;
            bool shift = false;
            bool ctrl = false;
            bool alt = false;

            //MessageBox.Show(key.ToString());

            if ((key & Keys.Shift) == Keys.Shift)
            {
                shift = true;

                key = key ^ Keys.Shift;
            }
            if ((key & Keys.Control) == Keys.Control)
            {
                ctrl = true;

                key = key ^ Keys.Control;
            }
            if ((key & Keys.Alt) == Keys.Alt)
            {
                alt = true;

                key = key ^ Keys.Alt;
            }

            switch (key)
            {
                case Keys.Enter:
                    uk = UKeys.Enter;
                    break;
                case Keys.Back:
                    uk = UKeys.Back;
                    break;
                case Keys.Space:
                    uk = UKeys.Space;
                    break;
                case Keys.ShiftKey:
                    uk = UKeys.LShift;
                    break;
                case Keys.ControlKey:
                    uk = UKeys.LCtrl;
                    break;
                case Keys.Menu:
                    uk = UKeys.LAlt;
                    break;
                case Keys.Insert:
                    uk = UKeys.Insert;
                    break;
                case Keys.Delete:
                    uk = UKeys.Delete;
                    break;
                case Keys.Home:
                    uk = UKeys.Home;
                    break;
                case Keys.End:
                    uk = UKeys.End;
                    break;
                case Keys.PageUp:
                    uk = UKeys.PageUp;
                    break;
                case Keys.PageDown:
                    uk = UKeys.PageDown;
                    break;
                case Keys.Left:
                    uk = UKeys.Left;
                    break;
                case Keys.Right:
                    uk = UKeys.Right;
                    break;
                case Keys.Up:
                    uk = UKeys.Up;
                    break;
                case Keys.Down:
                    uk = UKeys.Down;
                    break;
                case Keys.Oemtilde:
                    uk = UKeys.Tilde;
                    break;
                case Keys.Tab:
                    uk = UKeys.Tab;
                    break;
                case Keys.Capital:
                    uk = UKeys.Caps;
                    break;
                case Keys.D1:
                    uk = UKeys.D1;
                    break;
                case Keys.D2:
                    uk = UKeys.D2;
                    break;
                case Keys.D3:
                    uk = UKeys.D3;
                    break;
                case Keys.D4:
                    uk = UKeys.D4;
                    break;
                case Keys.D5:
                    uk = UKeys.D5;
                    break;
                case Keys.D6:
                    uk = UKeys.D6;
                    break;
                case Keys.D7:
                    uk = UKeys.D7;
                    break;
                case Keys.D8:
                    uk = UKeys.D8;
                    break;
                case Keys.D9:
                    uk = UKeys.D9;
                    break;
                case Keys.D0:
                    uk = UKeys.D0;
                    break;
                case Keys.OemMinus:
                    uk = UKeys.Minus;
                    break;
                case Keys.Oemplus:
                    uk = UKeys.Plus;
                    break;
                case Keys.Oem5:
                    uk = UKeys.Pipeline;
                    break;
                case Keys.OemOpenBrackets:
                    uk = UKeys.LBracket;
                    break;
                case Keys.Oem6:
                    uk = UKeys.RBracket;
                    break;
                case Keys.Oem1:
                    uk = UKeys.Semicolon;
                    break;
                case Keys.Oem7:
                    uk = UKeys.Quote;
                    break;
                case Keys.Oemcomma:
                    uk = UKeys.Comma;
                    break;
                case Keys.OemPeriod:
                    uk = UKeys.Period;
                    break;
                case Keys.OemQuestion:
                    uk = UKeys.Question;
                    break;
                case Keys.F1:
                    uk = UKeys.F1;
                    break;
                case Keys.F2:
                    uk = UKeys.F2;
                    break;
                case Keys.F3:
                    uk = UKeys.F3;
                    break;
                case Keys.F4:
                    uk = UKeys.F4;
                    break;
                case Keys.F5:
                    uk = UKeys.F5;
                    break;
                case Keys.F6:
                    uk = UKeys.F6;
                    break;
                case Keys.F7:
                    uk = UKeys.F7;
                    break;
                case Keys.F8:
                    uk = UKeys.F8;
                    break;
                case Keys.F9:
                    uk = UKeys.F9;
                    break;
                case Keys.F10:
                    uk = UKeys.F10;
                    break;
                case Keys.F11:
                    uk = UKeys.F11;
                    break;
                case Keys.F12:
                    uk = UKeys.F12;
                    break;
                case Keys.A:
                    uk = UKeys.A;
                    break;
                case Keys.B:
                    uk = UKeys.B;
                    break;
                case Keys.C:
                    uk = UKeys.C;
                    break;
                case Keys.D:
                    uk = UKeys.D;
                    break;
                case Keys.E:
                    uk = UKeys.E;
                    break;
                case Keys.F:
                    uk = UKeys.F;
                    break;
                case Keys.G:
                    uk = UKeys.G;
                    break;
                case Keys.H:
                    uk = UKeys.H;
                    break;
                case Keys.I:
                    uk = UKeys.I;
                    break;
                case Keys.J:
                    uk = UKeys.J;
                    break;
                case Keys.K:
                    uk = UKeys.K;
                    break;
                case Keys.L:
                    uk = UKeys.L;
                    break;
                case Keys.M:
                    uk = UKeys.M;
                    break;
                case Keys.N:
                    uk = UKeys.N;
                    break;
                case Keys.O:
                    uk = UKeys.O;
                    break;
                case Keys.P:
                    uk = UKeys.P;
                    break;
                case Keys.Q:
                    uk = UKeys.Q;
                    break;
                case Keys.R:
                    uk = UKeys.R;
                    break;
                case Keys.S:
                    uk = UKeys.S;
                    break;
                case Keys.T:
                    uk = UKeys.T;
                    break;
                case Keys.U:
                    uk = UKeys.U;
                    break;
                case Keys.V:
                    uk = UKeys.V;
                    break;
                case Keys.W:
                    uk = UKeys.W;
                    break;
                case Keys.X:
                    uk = UKeys.X;
                    break;
                case Keys.Y:
                    uk = UKeys.Y;
                    break;
                case Keys.Z:
                    uk = UKeys.Z;
                    break;
                case Keys.NumPad0:
                    uk = UKeys.Numpad0;
                    break;
                case Keys.NumPad1:
                    uk = UKeys.Numpad1;
                    break;
                case Keys.NumPad2:
                    uk = UKeys.Numpad2;
                    break;
                case Keys.NumPad3:
                    uk = UKeys.Numpad3;
                    break;
                case Keys.NumPad4:
                    uk = UKeys.Numpad4;
                    break;
                case Keys.NumPad5:
                    uk = UKeys.Numpad5;
                    break;
                case Keys.NumPad6:
                    uk = UKeys.Numpad6;
                    break;
                case Keys.NumPad7:
                    uk = UKeys.Numpad7;
                    break;
                case Keys.NumPad8:
                    uk = UKeys.Numpad8;
                    break;
                case Keys.NumPad9:
                    uk = UKeys.Numpad9;
                    break;
                case Keys.Decimal:
                    uk = UKeys.NumpadPoint;
                    break;
                case Keys.Add:
                    uk = UKeys.NumpadPlus;
                    break;
                case Keys.Subtract:
                    uk = UKeys.NumpadMinus;
                    break;
                case Keys.Multiply:
                    uk = UKeys.NumpadMultiply;
                    break;
                case Keys.Divide:
                    uk = UKeys.NumpadDivide;
                    break;
                default:
                    break;
            }

            return new UKeyEvents(uk, shift, ctrl, alt);
        }

        private UMouseEvents ParseMouse(MouseEventArgs e)
        {
            UMouseButton ub = UMouseButton.None;
            int x = e.X;
            int y = e.Y;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    ub = UMouseButton.Left;
                    break;
                case MouseButtons.Middle:
                    ub = UMouseButton.Middle;
                    break;
                case MouseButtons.Right:
                    ub = UMouseButton.Right;
                    break;
                default:
                    break;
            }

            return new UMouseEvents(ub, x, y);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            coder.KeyDown(ParseKey(keyData));

            RenderScreen();

            return true;
        }

        private void RenderScreen()
        {
            try
            {
                coder.Render();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            picScreen.Image = render.Screen;

            this.lCursor.Text = "行:" + coder.CodeRenderer.CursorRow.ToString() + "    列:" + coder.CodeRenderer.CursorCol.ToString();
        }

        private void ResetLayer()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            this.picScreen.Location = new Point(this.ClientRectangle.Left, this.menuStrip1.Bottom);
            this.picScreen.Size = new Size(
                this.ClientSize.Width - 20,
                this.ClientSize.Height - this.menuStrip1.Height - this.statusStrip1.Height - 20);

            this.vScrollBar1.Location = new Point(this.picScreen.Right, this.menuStrip1.Bottom);
            this.vScrollBar1.Size = new Size(20, this.picScreen.Height);

            this.hScrollBar1.Location = new Point(this.picScreen.Left, this.picScreen.Bottom);
            this.hScrollBar1.Size = new Size(this.picScreen.Width, 20);

            this.render.SetSize(picScreen.ClientSize.Width, picScreen.ClientSize.Height);
            this.coder.ResetDrawing();
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                render = new GDIRenderer(picScreen.ClientSize.Width, picScreen.ClientSize.Height);

                coder = new UUCoder(render);
                coder.SetLineNumberVisible(false);

                /*coder.ParseFile(mCurrFile);
                coder.LoadConfig(Application.StartupPath + "\\config.txt");

                vScrollBar1.Maximum = coder.CodeManager.GetCodeLineCount();*/

                ResetLayer();

                RenderScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResetLayer();

            RenderScreen();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            coder.Shutdown();
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            coder.SetRenderStartRow(vScrollBar1.Value);
            RenderScreen();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            coder.SetRenderStartCol(hScrollBar1.Value);
            RenderScreen();
        }

        private void picScreen_MouseDown(object sender, MouseEventArgs e)
        {
            coder.MouseDown(ParseMouse(e));

            RenderScreen();
        }

        private void picScreen_MouseUp(object sender, MouseEventArgs e)
        {
            coder.MouseUp(ParseMouse(e));

            RenderScreen();
        }

        private void picScreen_MouseMove(object sender, MouseEventArgs e)
        {
            coder.MouseMove(ParseMouse(e));

            RenderScreen();
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coder.Debug();
            RenderScreen();
        }

        private void 说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("UUCoder代码编辑器 V0.0.1\r\nCopy right 201002 UnSkyToo\r\n\t2013.7.3", "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coder.Undo();
        }

        private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coder.Redo();
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coder.SelectAll();
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSharp(*.cs)|*.cs|文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Title = "打开";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mCurrFile = openFileDialog1.FileName;
                coder.ParseFile(openFileDialog1.FileName);
                vScrollBar1.Maximum = coder.CodeManager.GetCodeLineCount();
                RenderScreen();
            }
        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Title = "新建";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mCurrFile = openFileDialog1.FileName;
                FileStream fp = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                fp.Close();
                fp.Dispose();

                coder.ParseFile(saveFileDialog1.FileName);
            }
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mCurrFile == string.Empty)
            {
                return;
            }

            StreamWriter fp = new StreamWriter(mCurrFile, false, Encoding.Default);

            int len = coder.CodeManager.GetCodeCount();

            for (int i = 0; i < len; i++)
            {
                fp.WriteLine(UHelper.GetStringByBytes(coder.CodeManager.GetCodeData(i)));
            }

            fp.Close();

            fp.Dispose();

            MessageBox.Show(mCurrFile + "\r\n保存成功");
        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mCurrFile == string.Empty)
            {
                return;
            }

            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Title = "另存为";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter fp = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default);
                mCurrFile = saveFileDialog1.FileName;

                int len = coder.CodeManager.GetCodeCount();

                for (int i = 0; i < len; i++)
                {
                    fp.WriteLine(UHelper.GetStringByBytes(coder.CodeManager.GetCodeData(i)));
                }

                fp.Close();

                fp.Dispose();

                MessageBox.Show(saveFileDialog1.FileName + "\r\n另存为成功");
            }
        }

        private void 退出QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 显示行号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.显示行号ToolStripMenuItem.Text == "隐藏行号")
            {
                this.显示行号ToolStripMenuItem.Text = "显示行号";

                coder.SetLineNumberVisible(false);
            }
            else
            {
                this.显示行号ToolStripMenuItem.Text = "隐藏行号";

                coder.SetLineNumberVisible(true);
            }

            RenderScreen();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coder.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coder.Paste();

            RenderScreen();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coder.Cut();

            RenderScreen();
        }

        private void 配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 运用单例模式，保证只有一个实例
            if (ConfigForm.GetShareForm().ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                coder.LoadConfig(Application.StartupPath + "\\config\\config.txt");

                render.SetFont(UConfig.EditorFont);

                coder.ResetDrawing();

                RenderScreen();
            }
        }

    }
}
