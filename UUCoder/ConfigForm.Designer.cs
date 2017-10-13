namespace UUCoder
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabEnvironment = new System.Windows.Forms.TabPage();
            this.cbExitAutoSave = new System.Windows.Forms.CheckBox();
            this.tabEditor = new System.Windows.Forms.TabPage();
            this.tabLanguage = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabKeyWords = new System.Windows.Forms.TabPage();
            this.butDeleteKeyWord = new System.Windows.Forms.Button();
            this.txtNewKeyWord = new System.Windows.Forms.TextBox();
            this.butAddKeyWord = new System.Windows.Forms.Button();
            this.listKeyWords = new System.Windows.Forms.ListBox();
            this.butKeyWordColor = new System.Windows.Forms.Button();
            this.tabSymbols = new System.Windows.Forms.TabPage();
            this.butDeleteSymbol = new System.Windows.Forms.Button();
            this.butAddSymbol = new System.Windows.Forms.Button();
            this.txtNewSymbol = new System.Windows.Forms.TextBox();
            this.listSymbols = new System.Windows.Forms.ListBox();
            this.butSymbolColor = new System.Windows.Forms.Button();
            this.tabVariableType = new System.Windows.Forms.TabPage();
            this.butDeleteVariableType = new System.Windows.Forms.Button();
            this.butAddVariableType = new System.Windows.Forms.Button();
            this.txtNewVariableType = new System.Windows.Forms.TextBox();
            this.listVariableType = new System.Windows.Forms.ListBox();
            this.gbOther = new System.Windows.Forms.GroupBox();
            this.butAnnotationColor = new System.Windows.Forms.Button();
            this.butVariableNameColor = new System.Windows.Forms.Button();
            this.butFunctionNameColor = new System.Windows.Forms.Button();
            this.butClassNameColor = new System.Windows.Forms.Button();
            this.butDigitColor = new System.Windows.Forms.Button();
            this.butStringColor = new System.Windows.Forms.Button();
            this.butNormalColor = new System.Windows.Forms.Button();
            this.gbLanguage = new System.Windows.Forms.GroupBox();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.lClass = new System.Windows.Forms.Label();
            this.txtNewLanguage = new System.Windows.Forms.TextBox();
            this.butAddLanguage = new System.Windows.Forms.Button();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.butDefault = new System.Windows.Forms.Button();
            this.butFont = new System.Windows.Forms.Button();
            this.lFont = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabEnvironment.SuspendLayout();
            this.tabLanguage.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabKeyWords.SuspendLayout();
            this.tabSymbols.SuspendLayout();
            this.tabVariableType.SuspendLayout();
            this.gbOther.SuspendLayout();
            this.gbLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabEnvironment);
            this.tabControl1.Controls.Add(this.tabEditor);
            this.tabControl1.Controls.Add(this.tabLanguage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(752, 414);
            this.tabControl1.TabIndex = 0;
            // 
            // tabEnvironment
            // 
            this.tabEnvironment.BackColor = System.Drawing.SystemColors.Control;
            this.tabEnvironment.Controls.Add(this.lFont);
            this.tabEnvironment.Controls.Add(this.butFont);
            this.tabEnvironment.Controls.Add(this.cbExitAutoSave);
            this.tabEnvironment.Location = new System.Drawing.Point(4, 22);
            this.tabEnvironment.Name = "tabEnvironment";
            this.tabEnvironment.Padding = new System.Windows.Forms.Padding(3);
            this.tabEnvironment.Size = new System.Drawing.Size(744, 388);
            this.tabEnvironment.TabIndex = 0;
            this.tabEnvironment.Text = "环境";
            // 
            // cbExitAutoSave
            // 
            this.cbExitAutoSave.AutoSize = true;
            this.cbExitAutoSave.Location = new System.Drawing.Point(8, 6);
            this.cbExitAutoSave.Name = "cbExitAutoSave";
            this.cbExitAutoSave.Size = new System.Drawing.Size(96, 16);
            this.cbExitAutoSave.TabIndex = 0;
            this.cbExitAutoSave.TabStop = false;
            this.cbExitAutoSave.Text = "ExitAutoSave";
            this.cbExitAutoSave.UseVisualStyleBackColor = true;
            // 
            // tabEditor
            // 
            this.tabEditor.BackColor = System.Drawing.SystemColors.Control;
            this.tabEditor.Location = new System.Drawing.Point(4, 22);
            this.tabEditor.Name = "tabEditor";
            this.tabEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tabEditor.Size = new System.Drawing.Size(744, 388);
            this.tabEditor.TabIndex = 1;
            this.tabEditor.Text = "编辑器";
            // 
            // tabLanguage
            // 
            this.tabLanguage.BackColor = System.Drawing.SystemColors.Control;
            this.tabLanguage.Controls.Add(this.tabControl2);
            this.tabLanguage.Controls.Add(this.gbOther);
            this.tabLanguage.Controls.Add(this.gbLanguage);
            this.tabLanguage.Location = new System.Drawing.Point(4, 22);
            this.tabLanguage.Name = "tabLanguage";
            this.tabLanguage.Padding = new System.Windows.Forms.Padding(3);
            this.tabLanguage.Size = new System.Drawing.Size(744, 388);
            this.tabLanguage.TabIndex = 2;
            this.tabLanguage.Text = "语言";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabKeyWords);
            this.tabControl2.Controls.Add(this.tabSymbols);
            this.tabControl2.Controls.Add(this.tabVariableType);
            this.tabControl2.Location = new System.Drawing.Point(121, 6);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(461, 376);
            this.tabControl2.TabIndex = 0;
            // 
            // tabKeyWords
            // 
            this.tabKeyWords.BackColor = System.Drawing.SystemColors.Control;
            this.tabKeyWords.Controls.Add(this.butDeleteKeyWord);
            this.tabKeyWords.Controls.Add(this.txtNewKeyWord);
            this.tabKeyWords.Controls.Add(this.butAddKeyWord);
            this.tabKeyWords.Controls.Add(this.listKeyWords);
            this.tabKeyWords.Controls.Add(this.butKeyWordColor);
            this.tabKeyWords.Location = new System.Drawing.Point(4, 22);
            this.tabKeyWords.Name = "tabKeyWords";
            this.tabKeyWords.Padding = new System.Windows.Forms.Padding(3);
            this.tabKeyWords.Size = new System.Drawing.Size(453, 350);
            this.tabKeyWords.TabIndex = 0;
            this.tabKeyWords.Text = "关键字";
            // 
            // butDeleteKeyWord
            // 
            this.butDeleteKeyWord.Location = new System.Drawing.Point(355, 265);
            this.butDeleteKeyWord.Name = "butDeleteKeyWord";
            this.butDeleteKeyWord.Size = new System.Drawing.Size(90, 75);
            this.butDeleteKeyWord.TabIndex = 0;
            this.butDeleteKeyWord.TabStop = false;
            this.butDeleteKeyWord.Text = "删除关键字";
            this.butDeleteKeyWord.UseVisualStyleBackColor = true;
            this.butDeleteKeyWord.Click += new System.EventHandler(this.butDeleteKeyWord_Click);
            // 
            // txtNewKeyWord
            // 
            this.txtNewKeyWord.Location = new System.Drawing.Point(260, 265);
            this.txtNewKeyWord.Name = "txtNewKeyWord";
            this.txtNewKeyWord.Size = new System.Drawing.Size(90, 21);
            this.txtNewKeyWord.TabIndex = 0;
            this.txtNewKeyWord.TabStop = false;
            // 
            // butAddKeyWord
            // 
            this.butAddKeyWord.Location = new System.Drawing.Point(260, 290);
            this.butAddKeyWord.Name = "butAddKeyWord";
            this.butAddKeyWord.Size = new System.Drawing.Size(90, 50);
            this.butAddKeyWord.TabIndex = 0;
            this.butAddKeyWord.TabStop = false;
            this.butAddKeyWord.Text = "添加关键字";
            this.butAddKeyWord.UseVisualStyleBackColor = true;
            this.butAddKeyWord.Click += new System.EventHandler(this.butAddKeyWord_Click);
            // 
            // listKeyWords
            // 
            this.listKeyWords.FormattingEnabled = true;
            this.listKeyWords.ItemHeight = 12;
            this.listKeyWords.Location = new System.Drawing.Point(5, 5);
            this.listKeyWords.Name = "listKeyWords";
            this.listKeyWords.Size = new System.Drawing.Size(440, 256);
            this.listKeyWords.TabIndex = 0;
            this.listKeyWords.TabStop = false;
            this.listKeyWords.SelectedIndexChanged += new System.EventHandler(this.listKeyWords_SelectedIndexChanged);
            // 
            // butKeyWordColor
            // 
            this.butKeyWordColor.Location = new System.Drawing.Point(5, 265);
            this.butKeyWordColor.Name = "butKeyWordColor";
            this.butKeyWordColor.Size = new System.Drawing.Size(250, 80);
            this.butKeyWordColor.TabIndex = 0;
            this.butKeyWordColor.TabStop = false;
            this.butKeyWordColor.Text = "颜色";
            this.butKeyWordColor.UseVisualStyleBackColor = true;
            this.butKeyWordColor.Click += new System.EventHandler(this.butKeyWordColor_Click);
            // 
            // tabSymbols
            // 
            this.tabSymbols.BackColor = System.Drawing.SystemColors.Control;
            this.tabSymbols.Controls.Add(this.butDeleteSymbol);
            this.tabSymbols.Controls.Add(this.butAddSymbol);
            this.tabSymbols.Controls.Add(this.txtNewSymbol);
            this.tabSymbols.Controls.Add(this.listSymbols);
            this.tabSymbols.Controls.Add(this.butSymbolColor);
            this.tabSymbols.Location = new System.Drawing.Point(4, 22);
            this.tabSymbols.Name = "tabSymbols";
            this.tabSymbols.Padding = new System.Windows.Forms.Padding(3);
            this.tabSymbols.Size = new System.Drawing.Size(453, 350);
            this.tabSymbols.TabIndex = 1;
            this.tabSymbols.Text = "符号表";
            // 
            // butDeleteSymbol
            // 
            this.butDeleteSymbol.Location = new System.Drawing.Point(355, 265);
            this.butDeleteSymbol.Name = "butDeleteSymbol";
            this.butDeleteSymbol.Size = new System.Drawing.Size(90, 75);
            this.butDeleteSymbol.TabIndex = 0;
            this.butDeleteSymbol.TabStop = false;
            this.butDeleteSymbol.Text = "删除符号";
            this.butDeleteSymbol.UseVisualStyleBackColor = true;
            this.butDeleteSymbol.Click += new System.EventHandler(this.butDeleteSymbol_Click);
            // 
            // butAddSymbol
            // 
            this.butAddSymbol.Location = new System.Drawing.Point(260, 290);
            this.butAddSymbol.Name = "butAddSymbol";
            this.butAddSymbol.Size = new System.Drawing.Size(90, 50);
            this.butAddSymbol.TabIndex = 0;
            this.butAddSymbol.TabStop = false;
            this.butAddSymbol.Text = "添加符号";
            this.butAddSymbol.UseVisualStyleBackColor = true;
            this.butAddSymbol.Click += new System.EventHandler(this.butAddSymbol_Click);
            // 
            // txtNewSymbol
            // 
            this.txtNewSymbol.Location = new System.Drawing.Point(260, 265);
            this.txtNewSymbol.Name = "txtNewSymbol";
            this.txtNewSymbol.Size = new System.Drawing.Size(90, 21);
            this.txtNewSymbol.TabIndex = 0;
            this.txtNewSymbol.TabStop = false;
            // 
            // listSymbols
            // 
            this.listSymbols.FormattingEnabled = true;
            this.listSymbols.ItemHeight = 12;
            this.listSymbols.Location = new System.Drawing.Point(5, 5);
            this.listSymbols.Name = "listSymbols";
            this.listSymbols.Size = new System.Drawing.Size(440, 256);
            this.listSymbols.TabIndex = 0;
            this.listSymbols.TabStop = false;
            this.listSymbols.SelectedIndexChanged += new System.EventHandler(this.listSymbols_SelectedIndexChanged);
            // 
            // butSymbolColor
            // 
            this.butSymbolColor.Location = new System.Drawing.Point(5, 265);
            this.butSymbolColor.Name = "butSymbolColor";
            this.butSymbolColor.Size = new System.Drawing.Size(250, 80);
            this.butSymbolColor.TabIndex = 0;
            this.butSymbolColor.TabStop = false;
            this.butSymbolColor.Text = "颜色";
            this.butSymbolColor.UseVisualStyleBackColor = true;
            this.butSymbolColor.Click += new System.EventHandler(this.butSymbolColor_Click);
            // 
            // tabVariableType
            // 
            this.tabVariableType.BackColor = System.Drawing.SystemColors.Control;
            this.tabVariableType.Controls.Add(this.butDeleteVariableType);
            this.tabVariableType.Controls.Add(this.butAddVariableType);
            this.tabVariableType.Controls.Add(this.txtNewVariableType);
            this.tabVariableType.Controls.Add(this.listVariableType);
            this.tabVariableType.Location = new System.Drawing.Point(4, 22);
            this.tabVariableType.Name = "tabVariableType";
            this.tabVariableType.Size = new System.Drawing.Size(453, 350);
            this.tabVariableType.TabIndex = 2;
            this.tabVariableType.Text = "变量类型";
            // 
            // butDeleteVariableType
            // 
            this.butDeleteVariableType.Location = new System.Drawing.Point(355, 265);
            this.butDeleteVariableType.Name = "butDeleteVariableType";
            this.butDeleteVariableType.Size = new System.Drawing.Size(90, 75);
            this.butDeleteVariableType.TabIndex = 0;
            this.butDeleteVariableType.TabStop = false;
            this.butDeleteVariableType.Text = "删除变量类型";
            this.butDeleteVariableType.UseVisualStyleBackColor = true;
            this.butDeleteVariableType.Click += new System.EventHandler(this.butDeleteVariableType_Click);
            // 
            // butAddVariableType
            // 
            this.butAddVariableType.Location = new System.Drawing.Point(260, 290);
            this.butAddVariableType.Name = "butAddVariableType";
            this.butAddVariableType.Size = new System.Drawing.Size(90, 50);
            this.butAddVariableType.TabIndex = 0;
            this.butAddVariableType.TabStop = false;
            this.butAddVariableType.Text = "添加变量类型";
            this.butAddVariableType.UseVisualStyleBackColor = true;
            this.butAddVariableType.Click += new System.EventHandler(this.butAddVariableType_Click);
            // 
            // txtNewVariableType
            // 
            this.txtNewVariableType.Location = new System.Drawing.Point(260, 265);
            this.txtNewVariableType.Name = "txtNewVariableType";
            this.txtNewVariableType.Size = new System.Drawing.Size(90, 21);
            this.txtNewVariableType.TabIndex = 0;
            this.txtNewVariableType.TabStop = false;
            // 
            // listVariableType
            // 
            this.listVariableType.FormattingEnabled = true;
            this.listVariableType.ItemHeight = 12;
            this.listVariableType.Location = new System.Drawing.Point(5, 5);
            this.listVariableType.Name = "listVariableType";
            this.listVariableType.Size = new System.Drawing.Size(440, 256);
            this.listVariableType.TabIndex = 0;
            this.listVariableType.TabStop = false;
            // 
            // gbOther
            // 
            this.gbOther.Controls.Add(this.butAnnotationColor);
            this.gbOther.Controls.Add(this.butVariableNameColor);
            this.gbOther.Controls.Add(this.butFunctionNameColor);
            this.gbOther.Controls.Add(this.butClassNameColor);
            this.gbOther.Controls.Add(this.butDigitColor);
            this.gbOther.Controls.Add(this.butStringColor);
            this.gbOther.Controls.Add(this.butNormalColor);
            this.gbOther.Location = new System.Drawing.Point(588, 6);
            this.gbOther.Name = "gbOther";
            this.gbOther.Size = new System.Drawing.Size(150, 376);
            this.gbOther.TabIndex = 0;
            this.gbOther.TabStop = false;
            this.gbOther.Text = "其它";
            // 
            // butAnnotationColor
            // 
            this.butAnnotationColor.Location = new System.Drawing.Point(6, 225);
            this.butAnnotationColor.Name = "butAnnotationColor";
            this.butAnnotationColor.Size = new System.Drawing.Size(140, 30);
            this.butAnnotationColor.TabIndex = 6;
            this.butAnnotationColor.Text = "注释";
            this.butAnnotationColor.UseVisualStyleBackColor = true;
            this.butAnnotationColor.Click += new System.EventHandler(this.butAnnotationColor_Click);
            // 
            // butVariableNameColor
            // 
            this.butVariableNameColor.Location = new System.Drawing.Point(6, 190);
            this.butVariableNameColor.Name = "butVariableNameColor";
            this.butVariableNameColor.Size = new System.Drawing.Size(140, 30);
            this.butVariableNameColor.TabIndex = 5;
            this.butVariableNameColor.Text = "变量名";
            this.butVariableNameColor.UseVisualStyleBackColor = true;
            this.butVariableNameColor.Click += new System.EventHandler(this.butVariableNameColor_Click);
            // 
            // butFunctionNameColor
            // 
            this.butFunctionNameColor.Location = new System.Drawing.Point(6, 155);
            this.butFunctionNameColor.Name = "butFunctionNameColor";
            this.butFunctionNameColor.Size = new System.Drawing.Size(140, 30);
            this.butFunctionNameColor.TabIndex = 4;
            this.butFunctionNameColor.Text = "函数名";
            this.butFunctionNameColor.UseVisualStyleBackColor = true;
            this.butFunctionNameColor.Click += new System.EventHandler(this.butFunctionNameColor_Click);
            // 
            // butClassNameColor
            // 
            this.butClassNameColor.Location = new System.Drawing.Point(6, 120);
            this.butClassNameColor.Name = "butClassNameColor";
            this.butClassNameColor.Size = new System.Drawing.Size(140, 30);
            this.butClassNameColor.TabIndex = 3;
            this.butClassNameColor.Text = "类名";
            this.butClassNameColor.UseVisualStyleBackColor = true;
            this.butClassNameColor.Click += new System.EventHandler(this.butClassNameColor_Click);
            // 
            // butDigitColor
            // 
            this.butDigitColor.Location = new System.Drawing.Point(6, 85);
            this.butDigitColor.Name = "butDigitColor";
            this.butDigitColor.Size = new System.Drawing.Size(140, 30);
            this.butDigitColor.TabIndex = 2;
            this.butDigitColor.Text = "数字";
            this.butDigitColor.UseVisualStyleBackColor = true;
            this.butDigitColor.Click += new System.EventHandler(this.butDigitColor_Click);
            // 
            // butStringColor
            // 
            this.butStringColor.Location = new System.Drawing.Point(6, 50);
            this.butStringColor.Name = "butStringColor";
            this.butStringColor.Size = new System.Drawing.Size(140, 30);
            this.butStringColor.TabIndex = 1;
            this.butStringColor.Text = "字符串";
            this.butStringColor.UseVisualStyleBackColor = true;
            this.butStringColor.Click += new System.EventHandler(this.butStringColor_Click);
            // 
            // butNormalColor
            // 
            this.butNormalColor.Location = new System.Drawing.Point(6, 15);
            this.butNormalColor.Name = "butNormalColor";
            this.butNormalColor.Size = new System.Drawing.Size(140, 30);
            this.butNormalColor.TabIndex = 0;
            this.butNormalColor.Text = "文本";
            this.butNormalColor.UseVisualStyleBackColor = true;
            this.butNormalColor.Click += new System.EventHandler(this.butNormalColor_Click);
            // 
            // gbLanguage
            // 
            this.gbLanguage.Controls.Add(this.txtClass);
            this.gbLanguage.Controls.Add(this.lClass);
            this.gbLanguage.Controls.Add(this.txtNewLanguage);
            this.gbLanguage.Controls.Add(this.butAddLanguage);
            this.gbLanguage.Controls.Add(this.cmbLanguage);
            this.gbLanguage.Location = new System.Drawing.Point(8, 6);
            this.gbLanguage.Name = "gbLanguage";
            this.gbLanguage.Size = new System.Drawing.Size(106, 376);
            this.gbLanguage.TabIndex = 0;
            this.gbLanguage.TabStop = false;
            this.gbLanguage.Text = "语言";
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(30, 44);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(66, 21);
            this.txtClass.TabIndex = 0;
            this.txtClass.TabStop = false;
            // 
            // lClass
            // 
            this.lClass.AutoSize = true;
            this.lClass.Location = new System.Drawing.Point(7, 47);
            this.lClass.Name = "lClass";
            this.lClass.Size = new System.Drawing.Size(17, 12);
            this.lClass.TabIndex = 1;
            this.lClass.Text = "类";
            // 
            // txtNewLanguage
            // 
            this.txtNewLanguage.Location = new System.Drawing.Point(9, 310);
            this.txtNewLanguage.Name = "txtNewLanguage";
            this.txtNewLanguage.Size = new System.Drawing.Size(91, 21);
            this.txtNewLanguage.TabIndex = 0;
            this.txtNewLanguage.TabStop = false;
            // 
            // butAddLanguage
            // 
            this.butAddLanguage.Location = new System.Drawing.Point(9, 337);
            this.butAddLanguage.Name = "butAddLanguage";
            this.butAddLanguage.Size = new System.Drawing.Size(91, 32);
            this.butAddLanguage.TabIndex = 0;
            this.butAddLanguage.TabStop = false;
            this.butAddLanguage.Text = "添加新语言";
            this.butAddLanguage.UseVisualStyleBackColor = true;
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(6, 20);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(91, 20);
            this.cmbLanguage.TabIndex = 0;
            this.cmbLanguage.TabStop = false;
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(571, 420);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(83, 38);
            this.butOk.TabIndex = 0;
            this.butOk.TabStop = false;
            this.butOk.Text = "确定";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(660, 420);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(83, 38);
            this.butCancel.TabIndex = 0;
            this.butCancel.TabStop = false;
            this.butCancel.Text = "取消";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butDefault
            // 
            this.butDefault.Location = new System.Drawing.Point(12, 420);
            this.butDefault.Name = "butDefault";
            this.butDefault.Size = new System.Drawing.Size(83, 38);
            this.butDefault.TabIndex = 0;
            this.butDefault.TabStop = false;
            this.butDefault.Text = "默认";
            this.butDefault.UseVisualStyleBackColor = true;
            this.butDefault.Click += new System.EventHandler(this.butDefault_Click);
            // 
            // butFont
            // 
            this.butFont.Location = new System.Drawing.Point(8, 63);
            this.butFont.Name = "butFont";
            this.butFont.Size = new System.Drawing.Size(66, 33);
            this.butFont.TabIndex = 1;
            this.butFont.Text = "字体";
            this.butFont.UseVisualStyleBackColor = true;
            this.butFont.Click += new System.EventHandler(this.butFont_Click);
            // 
            // lFont
            // 
            this.lFont.AutoSize = true;
            this.lFont.Location = new System.Drawing.Point(8, 38);
            this.lFont.Name = "lFont";
            this.lFont.Size = new System.Drawing.Size(29, 12);
            this.lFont.TabIndex = 2;
            this.lFont.Text = "字体";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 462);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butDefault);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选项";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabEnvironment.ResumeLayout(false);
            this.tabEnvironment.PerformLayout();
            this.tabLanguage.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabKeyWords.ResumeLayout(false);
            this.tabKeyWords.PerformLayout();
            this.tabSymbols.ResumeLayout(false);
            this.tabSymbols.PerformLayout();
            this.tabVariableType.ResumeLayout(false);
            this.tabVariableType.PerformLayout();
            this.gbOther.ResumeLayout(false);
            this.gbLanguage.ResumeLayout(false);
            this.gbLanguage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabEnvironment;
        private System.Windows.Forms.TabPage tabEditor;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.TabPage tabLanguage;
        private System.Windows.Forms.CheckBox cbExitAutoSave;
        private System.Windows.Forms.GroupBox gbLanguage;
        private System.Windows.Forms.TextBox txtNewLanguage;
        private System.Windows.Forms.Button butAddLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.Label lClass;
        private System.Windows.Forms.GroupBox gbOther;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabKeyWords;
        private System.Windows.Forms.TextBox txtNewKeyWord;
        private System.Windows.Forms.Button butAddKeyWord;
        private System.Windows.Forms.ListBox listKeyWords;
        private System.Windows.Forms.Button butKeyWordColor;
        private System.Windows.Forms.TabPage tabSymbols;
        private System.Windows.Forms.Button butAddSymbol;
        private System.Windows.Forms.TextBox txtNewSymbol;
        private System.Windows.Forms.ListBox listSymbols;
        private System.Windows.Forms.Button butSymbolColor;
        private System.Windows.Forms.TabPage tabVariableType;
        private System.Windows.Forms.Button butAddVariableType;
        private System.Windows.Forms.TextBox txtNewVariableType;
        private System.Windows.Forms.ListBox listVariableType;
        private System.Windows.Forms.Button butAnnotationColor;
        private System.Windows.Forms.Button butVariableNameColor;
        private System.Windows.Forms.Button butFunctionNameColor;
        private System.Windows.Forms.Button butClassNameColor;
        private System.Windows.Forms.Button butDigitColor;
        private System.Windows.Forms.Button butStringColor;
        private System.Windows.Forms.Button butNormalColor;
        private System.Windows.Forms.Button butDefault;
        private System.Windows.Forms.Button butDeleteKeyWord;
        private System.Windows.Forms.Button butDeleteSymbol;
        private System.Windows.Forms.Button butDeleteVariableType;
        private System.Windows.Forms.Label lFont;
        private System.Windows.Forms.Button butFont;
    }
}