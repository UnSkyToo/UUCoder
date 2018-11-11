using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace UUCoder
{
    public partial class ConfigForm : Form
    {
        private static ConfigForm configForm = new ConfigForm();

        private CodeElement.UDataDocument mDataDocument = new CodeElement.UDataDocument();

        private const int EnvironmentId = 0; // 环境设置页索引
        private const int Environment_ExitAutoSave = 0; // 自动保存
        private const int Environment_Font = 1; // 字体

        private const int EditorId = 1; // 编辑设置页索引
        private const int LanguageId = 2; // 语言设置页索引

        private const int Language_Class = 0; // 类定义
        private const int Language_Normal = 1; // 文本
        private const int Language_String = 2; // 字符串
        private const int Language_Digit = 3; // 数字
        private const int Language_ClassName = 4; // 类名
        private const int Language_FunctionName = 5; // 函数名
        private const int Language_VariableName = 6; // 变量名
        private const int Language_Annotation = 7; // 注释
        private const int Language_KeyWords = 8; // 关键字
        private const int Language_Symbols = 9; // 符号表
        private const int Language_VariableType = 10; // 变量类型

        private int mCurrLanguageIndex = 0;

        public static ConfigForm GetShareForm()
        {
            return configForm;
        }

        private Color ParseColor(string str)
        {
            string[] rgb = str.Split(',');

            return Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
        }

        private string ParseColor(Color color)
        {
            return color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString();
        }

        private void SaveConfig(string filePath)
        {
            // 环境
            mDataDocument.Items[EnvironmentId].Items[Environment_ExitAutoSave].Value = cbExitAutoSave.Checked == true ? "true" : "false";

            // 编辑器
            foreach (Control c in tabEditor.Controls)
            {
                if (c is TextBox)
                {
                    mDataDocument.Items[EditorId].SetData(c.Name.Substring(3), c.Text);
                }
            }

            // 语言
            mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_Class].Value = txtClass.Text;

            mDataDocument.Save("F:\\config.txt");
        }

        #region Element
        private CodeElement.UDataElement GetKeyWord(int languageIndex, string name)
        {
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_KeyWords].Items)
            {
                if (element.Name == name)
                {
                    return element;
                }
            }

            return null;
        }

        private CodeElement.UDataElement GetSymbol(int languageIndex, string name)
        {
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_Symbols].Items)
            {
                if (element.Name == name)
                {
                    return element;
                }
            }

            return null;
        }

        private CodeElement.UDataElement GetVariableType(int languageIndex, string name)
        {
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_VariableType].Items)
            {
                if (element.Name == name)
                {
                    return element;
                }
            }

            return null;
        }

        private void SetKeyWord(int languageIndex, string name, string value)
        {
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_KeyWords].Items)
            {
                if (element.Name == name)
                {
                    element.Value = value;
                    return;
                }
            }
        }

        private void SetSymbol(int languageIndex, string name, string value)
        {
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_Symbols].Items)
            {
                if (element.Name == name)
                {
                    element.Value = value;
                    return;
                }
            }
        }

        private void SetVariableType(int languageIndex, string name, string value)
        {
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_VariableType].Items)
            {
                if (element.Name == name)
                {
                    element.Value = value;
                    return;
                }
            }
        }

        private void AddKeyWord(int languageIndex, string name, string value)
        {
            if (GetKeyWord(languageIndex, name) != null)
            {
                MessageBox.Show("关键字已存在");
                return;
            }

            CodeElement.UDataElement element = new CodeElement.UDataElement();
            element.Name = name;
            element.Tag = string.Empty;
            element.Value = value;

            mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_KeyWords].Items.Add(element);

            listKeyWords.Items.Add(name);
            listKeyWords.SelectedIndex = listKeyWords.Items.Count - 1;

            butKeyWordColor.BackColor = ParseColor(value);
        }

        private void AddSymbol(int languageIndex, string name, string value)
        {
            if (GetSymbol(languageIndex, name) != null)
            {
                MessageBox.Show("符号已存在");
                return;
            }

            CodeElement.UDataElement element = new CodeElement.UDataElement();
            element.Name = name;
            element.Tag = string.Empty;
            element.Value = value;

            mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_Symbols].Items.Add(element);

            listSymbols.Items.Add(name);
            listSymbols.SelectedIndex = listSymbols.Items.Count - 1;

            butSymbolColor.BackColor = ParseColor(value);
        }

        private void AddVariableType(int languageIndex, string name, string value)
        {
            if (GetVariableType(languageIndex, name) != null)
            {
                MessageBox.Show("变量类型已存在");
                return;
            }

            CodeElement.UDataElement element = new CodeElement.UDataElement();
            element.Name = name;
            element.Tag = string.Empty;
            element.Value = value;

            mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_VariableType].Items.Add(element);

            listVariableType.Items.Add(name);
            listVariableType.SelectedIndex = listVariableType.Items.Count - 1;
        }
        #endregion

        #region 设置
        private void SetTabData()
        {
            SetEnvironmentTab();
            SetEditorTab();
            SetLanguageTab(0);
        }

        private void SetEnvironmentTab()
        {
            CodeElement.UDataElement element = mDataDocument.Items[EnvironmentId].Items[Environment_ExitAutoSave];

            CheckBox cb = tabEnvironment.Controls["cbExitAutoSave"] as CheckBox;

            cb.Text = element.Tag;
            cb.Checked = element.Value == "true" ? true : false;

            element = mDataDocument.Items[EnvironmentId].Items[Environment_Font];

            lFont.Text = element.Value;
        }

        private void SetEditorTab()
        {
            tabEditor.Controls.Clear();

            int x = 0;
            int y = 0;

            foreach (CodeElement.UDataElement element in mDataDocument.Items[EditorId].Items)
            {
                Label l = new Label();
                l.AutoSize = true;
                l.Name = "l" + element.Name;
                l.Text = element.Tag;
                l.Location = new Point(x, y);

                TextBox txt = new TextBox();
                txt.Name = "txt" + element.Name;
                txt.Text = element.Value;
                txt.Location = new Point(x + l.Text.Length * 10, y);
                txt.Size = new Size(40, 21);

                tabEditor.Controls.Add(l);
                tabEditor.Controls.Add(txt);

                y = y + 25;
                if (y >= tabEditor.Height)
                {
                    y = 0;
                    x = x + 200;
                }
            }
        }

        private void SetLanguageTab(int languageIndex)
        {
            // 添加语言列表
            cmbLanguage.Items.Clear();
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items)
            {
                cmbLanguage.Items.Add(element.Name);
            }
            cmbLanguage.SelectedIndex = 0;

            // Class
            txtClass.Text = mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_Class].Value;

            // Normal
            butNormalColor.BackColor = ParseColor(mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_Normal].Value);

            // String
            butStringColor.BackColor = ParseColor(mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_String].Value);

            // Digit
            butDigitColor.BackColor = ParseColor(mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_Digit].Value);

            // ClassName
            butClassNameColor.BackColor = ParseColor(mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_ClassName].Value);

            // FunctionName
            butFunctionNameColor.BackColor = ParseColor(mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_FunctionName].Value);

            // VariableName
            butVariableNameColor.BackColor = ParseColor(mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_VariableName].Value);

            // Annotation
            butAnnotationColor.BackColor = ParseColor(mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_Annotation].Value);

            // 关键字
            listKeyWords.Items.Clear();
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_KeyWords].Items)
            {
                listKeyWords.Items.Add(element.Name);
            }

            if (listKeyWords.Items.Count > 0)
            {
                listKeyWords.SelectedIndex = 0;

                butKeyWordColor.BackColor = ParseColor(GetKeyWord(languageIndex, (string)listKeyWords.Items[0]).Value);
            }

            // 符号表
            listSymbols.Items.Clear();
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_Symbols].Items)
            {
                listSymbols.Items.Add(element.Name);
            }

            if (listSymbols.Items.Count > 0)
            {
                listSymbols.SelectedIndex = 0;

                butSymbolColor.BackColor = ParseColor(GetSymbol(languageIndex, (string)listSymbols.Items[0]).Value);
            }

            // 变量类型
            listVariableType.Items.Clear();
            foreach (CodeElement.UDataElement element in mDataDocument.Items[LanguageId].Items[languageIndex].Items[Language_VariableType].Items)
            {
                listVariableType.Items.Add(element.Name);
            }
            listVariableType.SelectedIndex = 0;

            // 其它

        }
        #endregion

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            this.mDataDocument.Load(Application.StartupPath + "\\config.txt");

            this.mCurrLanguageIndex = 0;

            SetTabData();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            SaveConfig(Application.StartupPath + "\\config.txt");

            DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Close();
        }

        private void butDefault_Click(object sender, EventArgs e)
        {
            mDataDocument.Load(Application.StartupPath + "\\default.txt");

            this.mCurrLanguageIndex = 0;

            SetTabData();
        }

        private void butFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            string[] fontParams = mDataDocument.Items[EnvironmentId].Items[Environment_Font].Value.Split(',');

            fd.Font = new Font(fontParams[0], int.Parse(fontParams[1]));

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lFont.Text = fd.Font.Name + "," + fd.Font.Size.ToString();
                mDataDocument.Items[EnvironmentId].Items[Environment_Font].Value = lFont.Text;
            }
        }


        #region 颜色设置
        private void butKeyWordColor_Click(object sender, EventArgs e)
        {
            if (listKeyWords.SelectedIndex == -1)
            {
                return;
            }

            ColorDialog cd = new ColorDialog();
            cd.Color = butKeyWordColor.BackColor;

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                SetKeyWord(mCurrLanguageIndex, (string)listKeyWords.Items[listKeyWords.SelectedIndex], ParseColor(cd.Color));
                butKeyWordColor.BackColor = cd.Color;
            }
        }

        private void listKeyWords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listKeyWords.SelectedIndex == -1)
            {
                return;
            }

            butKeyWordColor.BackColor = ParseColor(GetKeyWord(mCurrLanguageIndex, (string)listKeyWords.Items[listKeyWords.SelectedIndex]).Value);
        }

        private void butSymbolColor_Click(object sender, EventArgs e)
        {
            if (listSymbols.SelectedIndex == -1)
            {
                return;
            }

            ColorDialog cd = new ColorDialog();
            cd.Color = butSymbolColor.BackColor;

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetSymbol(mCurrLanguageIndex, (string)listSymbols.Items[listSymbols.SelectedIndex], ParseColor(cd.Color));
                butSymbolColor.BackColor = cd.Color;
            }
        }

        private void listSymbols_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSymbols.SelectedIndex == -1)
            {
                return;
            }

            butSymbolColor.BackColor = ParseColor(GetSymbol(mCurrLanguageIndex, (string)listSymbols.Items[listSymbols.SelectedIndex]).Value);
        }

        private void butNormalColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = butNormalColor.BackColor;

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_Normal].Value = ParseColor(cd.Color);
                butNormalColor.BackColor = cd.Color;
            }
        }

        private void butStringColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = butStringColor.BackColor;

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_String].Value = ParseColor(cd.Color);
                butStringColor.BackColor = cd.Color;
            }
        }

        private void butDigitColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = butDigitColor.BackColor;

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_Digit].Value = ParseColor(cd.Color);
                butDigitColor.BackColor = cd.Color;
            }
        }

        private void butClassNameColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = butClassNameColor.BackColor;

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_ClassName].Value = ParseColor(cd.Color);
                butClassNameColor.BackColor = cd.Color;
            }
        }

        private void butFunctionNameColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = butFunctionNameColor.BackColor;

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_FunctionName].Value = ParseColor(cd.Color);
                butFunctionNameColor.BackColor = cd.Color;
            }
        }

        private void butVariableNameColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = butVariableNameColor.BackColor;

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_VariableName].Value = ParseColor(cd.Color);
                butVariableNameColor.BackColor = cd.Color;
            }
        }

        private void butAnnotationColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = butAnnotationColor.BackColor;

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_Annotation].Value = ParseColor(cd.Color);
                butAnnotationColor.BackColor = cd.Color;
            }
        }
        #endregion

        #region 添加删除
        private void butAddKeyWord_Click(object sender, EventArgs e)
        {
            if (txtNewKeyWord.Text != string.Empty)
            {
                AddKeyWord(mCurrLanguageIndex, txtNewKeyWord.Text, ParseColor(Color.White));
                txtNewKeyWord.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("关键字不能为空");
                return;
            }
        }

        private void butAddSymbol_Click(object sender, EventArgs e)
        {
            if (txtNewSymbol.Text.Length == 1)
            {
                AddSymbol(mCurrLanguageIndex, txtNewSymbol.Text, ParseColor(Color.White));
                txtNewSymbol.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("符号只能为单字节符号");
                return;
            }
        }

        private void butAddVariableType_Click(object sender, EventArgs e)
        {
            if (txtNewVariableType.Text != string.Empty)
            {
                AddVariableType(mCurrLanguageIndex, txtNewVariableType.Text, string.Empty);
                txtNewVariableType.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("变量类型不能为空");
                return;
            }
        }

        private void butDeleteKeyWord_Click(object sender, EventArgs e)
        {
            int index = listKeyWords.SelectedIndex;

            if (index < 0 || index >= listKeyWords.Items.Count)
            {
                return;
            }

            mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_KeyWords].DelData((string)listKeyWords.Items[index]);

            listKeyWords.Items.RemoveAt(index);

            if (index > 0)
            {
                listKeyWords.SelectedIndex = index - 1;
            }
            else
            {
                listKeyWords.SelectedIndex = 0;
            }

            if (listKeyWords.Items.Count > 0)
            {
                butKeyWordColor.BackColor = ParseColor(GetKeyWord(mCurrLanguageIndex, (string)listKeyWords.Items[listKeyWords.SelectedIndex]).Value);
            }
            else
            {
                listKeyWords.SelectedIndex = -1;
                butKeyWordColor.BackColor = Color.Transparent;
            }
        }

        private void butDeleteSymbol_Click(object sender, EventArgs e)
        {
            int index = listSymbols.SelectedIndex;

            if (index < 0 || index >= listSymbols.Items.Count)
            {
                return;
            }

            mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_Symbols].DelData((string)listSymbols.Items[index]);

            listSymbols.Items.RemoveAt(index);

            if (index > 0)
            {
                listSymbols.SelectedIndex = index - 1;
            }
            else
            {
                listSymbols.SelectedIndex = 0;
            }

            if (listSymbols.Items.Count > 0)
            {
                butSymbolColor.BackColor = ParseColor(GetSymbol(mCurrLanguageIndex, (string)listSymbols.Items[listSymbols.SelectedIndex]).Value);
            }
            else
            {
                listSymbols.SelectedIndex = -1;
                butSymbolColor.BackColor = Color.Transparent;
            }
        }

        private void butDeleteVariableType_Click(object sender, EventArgs e)
        {
            int index = listVariableType.SelectedIndex;

            if (index < 0 || index >= listVariableType.Items.Count)
            {
                return;
            }

            mDataDocument.Items[LanguageId].Items[mCurrLanguageIndex].Items[Language_VariableType].DelData((string)listVariableType.Items[index]);

            listVariableType.Items.RemoveAt(index);
        }
        #endregion
    }
}
