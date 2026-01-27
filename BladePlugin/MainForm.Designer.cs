namespace GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.BladeLengthLabel = new System.Windows.Forms.Label();
            this.TextBoxLength = new System.Windows.Forms.TextBox();
            this.TextBoxWidth = new System.Windows.Forms.TextBox();
            this.BladeWidthLabel = new System.Windows.Forms.Label();
            this.BladeTypeLabel = new System.Windows.Forms.Label();
            this.TextBoxBladeThickness = new System.Windows.Forms.TextBox();
            this.BladeThickLabel = new System.Windows.Forms.Label();
            this.TextBoxEdgeWidth = new System.Windows.Forms.TextBox();
            this.EdgeWidthLabel = new System.Windows.Forms.Label();
            this.PeakExistanceLabel = new System.Windows.Forms.Label();
            this.BindingLabel = new System.Windows.Forms.Label();
            this.TextBoxPeakLength = new System.Windows.Forms.TextBox();
            this.PeakLengthLabel = new System.Windows.Forms.Label();
            this.ComboBoxTypeBlade = new System.Windows.Forms.ComboBox();
            this.CheckBoxPeakBlade = new System.Windows.Forms.CheckBox();
            this.ComboBoxTypeBinding = new System.Windows.Forms.ComboBox();
            this.TextBoxBindingLength = new System.Windows.Forms.TextBox();
            this.BindingLengthLabel = new System.Windows.Forms.Label();
            this.ButtonBuild = new System.Windows.Forms.Button();
            this.TextBoxError = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Max_Min_Value = new System.Windows.Forms.ToolTip(this.components);
            this.SerreitorLengthTextBox = new System.Windows.Forms.TextBox();
            this.serreitorCheckBox = new System.Windows.Forms.CheckBox();
            this.SerreitorExistenceLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SerreitorLengthLabel = new System.Windows.Forms.Label();
            this.SerreitorTypeComboBox = new System.Windows.Forms.ComboBox();
            this.SerreitorTypeLabel = new System.Windows.Forms.Label();
            this.SerreitorToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SerreitorNumberTextBox = new System.Windows.Forms.TextBox();
            this.SerreitorNumberLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Параметры клинка";
            // 
            // BladeLengthLabel
            // 
            this.BladeLengthLabel.AutoSize = true;
            this.BladeLengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BladeLengthLabel.ForeColor = System.Drawing.Color.Black;
            this.BladeLengthLabel.Location = new System.Drawing.Point(12, 49);
            this.BladeLengthLabel.Name = "BladeLengthLabel";
            this.BladeLengthLabel.Size = new System.Drawing.Size(210, 25);
            this.BladeLengthLabel.TabIndex = 1;
            this.BladeLengthLabel.Text = "Длина клинка        L1:";
            // 
            // TextBoxLength
            // 
            this.TextBoxLength.Location = new System.Drawing.Point(235, 53);
            this.TextBoxLength.Name = "TextBoxLength";
            this.TextBoxLength.Size = new System.Drawing.Size(118, 22);
            this.TextBoxLength.TabIndex = 3;
            this.TextBoxLength.Text = "300";
            this.Max_Min_Value.SetToolTip(this.TextBoxLength, "Допустимые значения: 80..1200мм");
            this.TextBoxLength.Leave += new System.EventHandler(this.TextBoxLengthLeave);
            // 
            // TextBoxWidth
            // 
            this.TextBoxWidth.Location = new System.Drawing.Point(235, 172);
            this.TextBoxWidth.Name = "TextBoxWidth";
            this.TextBoxWidth.Size = new System.Drawing.Size(121, 22);
            this.TextBoxWidth.TabIndex = 5;
            this.TextBoxWidth.Text = "40";
            this.Max_Min_Value.SetToolTip(this.TextBoxWidth, "Допустимые значения: 9..60мм");
            this.TextBoxWidth.Leave += new System.EventHandler(this.TextBoxWidthLeave);
            // 
            // BladeWidthLabel
            // 
            this.BladeWidthLabel.AutoSize = true;
            this.BladeWidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BladeWidthLabel.Location = new System.Drawing.Point(12, 168);
            this.BladeWidthLabel.Name = "BladeWidthLabel";
            this.BladeWidthLabel.Size = new System.Drawing.Size(208, 25);
            this.BladeWidthLabel.TabIndex = 4;
            this.BladeWidthLabel.Text = "Ширина клинка    W1:";
            // 
            // BladeTypeLabel
            // 
            this.BladeTypeLabel.AutoSize = true;
            this.BladeTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BladeTypeLabel.Location = new System.Drawing.Point(12, 274);
            this.BladeTypeLabel.Name = "BladeTypeLabel";
            this.BladeTypeLabel.Size = new System.Drawing.Size(121, 25);
            this.BladeTypeLabel.TabIndex = 8;
            this.BladeTypeLabel.Text = "Тип клинка:";
            // 
            // TextBoxBladeThickness
            // 
            this.TextBoxBladeThickness.Location = new System.Drawing.Point(235, 205);
            this.TextBoxBladeThickness.Name = "TextBoxBladeThickness";
            this.TextBoxBladeThickness.Size = new System.Drawing.Size(121, 22);
            this.TextBoxBladeThickness.TabIndex = 7;
            this.TextBoxBladeThickness.Text = "2";
            this.Max_Min_Value.SetToolTip(this.TextBoxBladeThickness, "Допустимые значения:1..3мм");
            this.TextBoxBladeThickness.Leave += new System.EventHandler(this.TextBoxBladeThicknessLeave);
            // 
            // BladeThickLabel
            // 
            this.BladeThickLabel.AutoSize = true;
            this.BladeThickLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BladeThickLabel.Location = new System.Drawing.Point(12, 201);
            this.BladeThickLabel.Name = "BladeThickLabel";
            this.BladeThickLabel.Size = new System.Drawing.Size(207, 25);
            this.BladeThickLabel.TabIndex = 6;
            this.BladeThickLabel.Text = "Толщина клинка W2:";
            // 
            // TextBoxEdgeWidth
            // 
            this.TextBoxEdgeWidth.Location = new System.Drawing.Point(235, 236);
            this.TextBoxEdgeWidth.Name = "TextBoxEdgeWidth";
            this.TextBoxEdgeWidth.Size = new System.Drawing.Size(121, 22);
            this.TextBoxEdgeWidth.TabIndex = 13;
            this.TextBoxEdgeWidth.Text = "14";
            this.Max_Min_Value.SetToolTip(this.TextBoxEdgeWidth, "Для отображения введите значение в поле \"Ширина Клинка\"");
            this.TextBoxEdgeWidth.Leave += new System.EventHandler(this.TextBoxEdgeWidthLeave);
            // 
            // EdgeWidthLabel
            // 
            this.EdgeWidthLabel.AutoSize = true;
            this.EdgeWidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.EdgeWidthLabel.Location = new System.Drawing.Point(12, 232);
            this.EdgeWidthLabel.Name = "EdgeWidthLabel";
            this.EdgeWidthLabel.Size = new System.Drawing.Size(210, 25);
            this.EdgeWidthLabel.TabIndex = 12;
            this.EdgeWidthLabel.Text = "Ширина лезвия    W3:";
            // 
            // PeakExistanceLabel
            // 
            this.PeakExistanceLabel.AutoSize = true;
            this.PeakExistanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PeakExistanceLabel.Location = new System.Drawing.Point(12, 341);
            this.PeakExistanceLabel.Name = "PeakExistanceLabel";
            this.PeakExistanceLabel.Size = new System.Drawing.Size(168, 25);
            this.PeakExistanceLabel.TabIndex = 10;
            this.PeakExistanceLabel.Text = "Наличие острия:";
            // 
            // BindingLabel
            // 
            this.BindingLabel.AutoSize = true;
            this.BindingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BindingLabel.Location = new System.Drawing.Point(12, 308);
            this.BindingLabel.Name = "BindingLabel";
            this.BindingLabel.Size = new System.Drawing.Size(156, 25);
            this.BindingLabel.TabIndex = 16;
            this.BindingLabel.Text = "Тип крепления:";
            // 
            // TextBoxPeakLength
            // 
            this.TextBoxPeakLength.Location = new System.Drawing.Point(235, 87);
            this.TextBoxPeakLength.Name = "TextBoxPeakLength";
            this.TextBoxPeakLength.Size = new System.Drawing.Size(118, 22);
            this.TextBoxPeakLength.TabIndex = 15;
            this.TextBoxPeakLength.Text = "45";
            this.Max_Min_Value.SetToolTip(this.TextBoxPeakLength, "Для отображения введите значение в поле \"Длина клинка\"");
            this.TextBoxPeakLength.Leave += new System.EventHandler(this.TextBoxPeakLengthLeave);
            // 
            // PeakLengthLabel
            // 
            this.PeakLengthLabel.AutoSize = true;
            this.PeakLengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PeakLengthLabel.Location = new System.Drawing.Point(12, 83);
            this.PeakLengthLabel.Name = "PeakLengthLabel";
            this.PeakLengthLabel.Size = new System.Drawing.Size(212, 25);
            this.PeakLengthLabel.TabIndex = 14;
            this.PeakLengthLabel.Text = "Длина острия        L3:";
            // 
            // ComboBoxTypeBlade
            // 
            this.ComboBoxTypeBlade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTypeBlade.FormattingEnabled = true;
            this.ComboBoxTypeBlade.Items.AddRange(new object[] {
            "Односторонний",
            "Двусторонний"});
            this.ComboBoxTypeBlade.Location = new System.Drawing.Point(235, 278);
            this.ComboBoxTypeBlade.Name = "ComboBoxTypeBlade";
            this.ComboBoxTypeBlade.Size = new System.Drawing.Size(121, 24);
            this.ComboBoxTypeBlade.TabIndex = 18;
            this.ComboBoxTypeBlade.Leave += new System.EventHandler(this.ComboBoxTypeBladeLeave);
            // 
            // CheckBoxPeakBlade
            // 
            this.CheckBoxPeakBlade.AutoSize = true;
            this.CheckBoxPeakBlade.Location = new System.Drawing.Point(235, 349);
            this.CheckBoxPeakBlade.Name = "CheckBoxPeakBlade";
            this.CheckBoxPeakBlade.Size = new System.Drawing.Size(18, 17);
            this.CheckBoxPeakBlade.TabIndex = 19;
            this.CheckBoxPeakBlade.UseVisualStyleBackColor = true;
            this.CheckBoxPeakBlade.CheckedChanged += new System.EventHandler(this.CheckBoxEndBladeCheckedChanged);
            // 
            // ComboBoxTypeBinding
            // 
            this.ComboBoxTypeBinding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTypeBinding.FormattingEnabled = true;
            this.ComboBoxTypeBinding.Items.AddRange(new object[] {
            "Всадное",
            "Сквозное",
            "Накладное",
            "Отсутствует"});
            this.ComboBoxTypeBinding.Location = new System.Drawing.Point(235, 312);
            this.ComboBoxTypeBinding.Name = "ComboBoxTypeBinding";
            this.ComboBoxTypeBinding.Size = new System.Drawing.Size(121, 24);
            this.ComboBoxTypeBinding.TabIndex = 20;
            this.ComboBoxTypeBinding.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTypeSelectedIndexChanged);
            // 
            // TextBoxBindingLength
            // 
            this.TextBoxBindingLength.Location = new System.Drawing.Point(235, 121);
            this.TextBoxBindingLength.Name = "TextBoxBindingLength";
            this.TextBoxBindingLength.Size = new System.Drawing.Size(118, 22);
            this.TextBoxBindingLength.TabIndex = 22;
            this.TextBoxBindingLength.Text = "300";
            this.Max_Min_Value.SetToolTip(this.TextBoxBindingLength, "Для отображения введите значение в поле \"Длина клинка\" и выберете \"Тип Крепления\"" +
        "");
            this.TextBoxBindingLength.Leave += new System.EventHandler(this.TextBoxBindingLengthLeave);
            // 
            // BindingLengthLabel
            // 
            this.BindingLengthLabel.AutoSize = true;
            this.BindingLengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BindingLengthLabel.Location = new System.Drawing.Point(12, 117);
            this.BindingLengthLabel.Name = "BindingLengthLabel";
            this.BindingLengthLabel.Size = new System.Drawing.Size(210, 25);
            this.BindingLengthLabel.TabIndex = 21;
            this.BindingLengthLabel.Text = "Длина крепления L4:";
            // 
            // ButtonBuild
            // 
            this.ButtonBuild.Location = new System.Drawing.Point(9, 606);
            this.ButtonBuild.Name = "ButtonBuild";
            this.ButtonBuild.Size = new System.Drawing.Size(344, 40);
            this.ButtonBuild.TabIndex = 23;
            this.ButtonBuild.Text = "Построить";
            this.ButtonBuild.UseVisualStyleBackColor = true;
            this.ButtonBuild.Click += new System.EventHandler(this.ButtonBuildClick);
            // 
            // TextBoxError
            // 
            this.TextBoxError.ForeColor = System.Drawing.Color.Red;
            this.TextBoxError.Location = new System.Drawing.Point(374, 590);
            this.TextBoxError.Name = "TextBoxError";
            this.TextBoxError.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.TextBoxError.Size = new System.Drawing.Size(427, 87);
            this.TextBoxError.TabIndex = 25;
            this.TextBoxError.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(374, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(442, 542);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // Max_Min_Value
            // 
            this.Max_Min_Value.AutoPopDelay = 5000;
            this.Max_Min_Value.InitialDelay = 15;
            this.Max_Min_Value.ReshowDelay = 50;
            // 
            // SerreitorLengthTextBox
            // 
            this.SerreitorLengthTextBox.Location = new System.Drawing.Point(235, 473);
            this.SerreitorLengthTextBox.Name = "SerreitorLengthTextBox";
            this.SerreitorLengthTextBox.Size = new System.Drawing.Size(118, 22);
            this.SerreitorLengthTextBox.TabIndex = 30;
            this.SerreitorLengthTextBox.Text = "90";
            this.Max_Min_Value.SetToolTip(this.SerreitorLengthTextBox, "Для отображения введите значение в поле \"Длина клинка\"\r\n");
            this.SerreitorLengthTextBox.Leave += new System.EventHandler(this.SerreitorLengthTextBox_Leave);
            // 
            // serreitorCheckBox
            // 
            this.serreitorCheckBox.AutoSize = true;
            this.serreitorCheckBox.Location = new System.Drawing.Point(235, 384);
            this.serreitorCheckBox.Name = "serreitorCheckBox";
            this.serreitorCheckBox.Size = new System.Drawing.Size(18, 17);
            this.serreitorCheckBox.TabIndex = 27;
            this.serreitorCheckBox.UseVisualStyleBackColor = true;
            this.serreitorCheckBox.CheckedChanged += new System.EventHandler(this.serreitorCheckBox_CheckedChanged);
            // 
            // SerreitorExistenceLabel
            // 
            this.SerreitorExistenceLabel.AutoSize = true;
            this.SerreitorExistenceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SerreitorExistenceLabel.Location = new System.Drawing.Point(12, 376);
            this.SerreitorExistenceLabel.Name = "SerreitorExistenceLabel";
            this.SerreitorExistenceLabel.Size = new System.Drawing.Size(211, 25);
            this.SerreitorExistenceLabel.TabIndex = 26;
            this.SerreitorExistenceLabel.Text = "Наличие серрейтора:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.Location = new System.Drawing.Point(12, 426);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(287, 29);
            this.label3.TabIndex = 28;
            this.label3.Text = "Параметры серрейтора";
            // 
            // SerreitorLengthLabel
            // 
            this.SerreitorLengthLabel.AutoSize = true;
            this.SerreitorLengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SerreitorLengthLabel.ForeColor = System.Drawing.Color.Black;
            this.SerreitorLengthLabel.Location = new System.Drawing.Point(12, 469);
            this.SerreitorLengthLabel.Name = "SerreitorLengthLabel";
            this.SerreitorLengthLabel.Size = new System.Drawing.Size(187, 25);
            this.SerreitorLengthLabel.TabIndex = 29;
            this.SerreitorLengthLabel.Text = "Длина серрейтора";
            // 
            // SerreitorTypeComboBox
            // 
            this.SerreitorTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerreitorTypeComboBox.FormattingEnabled = true;
            this.SerreitorTypeComboBox.Items.AddRange(new object[] {
            "/\\^^/\\",
            "/\\/\\/\\/\\",
            "^^^^^"});
            this.SerreitorTypeComboBox.Location = new System.Drawing.Point(235, 514);
            this.SerreitorTypeComboBox.Name = "SerreitorTypeComboBox";
            this.SerreitorTypeComboBox.Size = new System.Drawing.Size(121, 24);
            this.SerreitorTypeComboBox.TabIndex = 32;
            this.SerreitorToolTip.SetToolTip(this.SerreitorTypeComboBox, "Чередующийся /\\^^/\\\r\nПостоянный крупный /\\/\\/\\/\\\r\nПостоянный мелкий ^^^^");
            this.SerreitorTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.SerreitorTypeComboBox_SelectedIndexChanged);
            // 
            // SerreitorTypeLabel
            // 
            this.SerreitorTypeLabel.AutoSize = true;
            this.SerreitorTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SerreitorTypeLabel.Location = new System.Drawing.Point(12, 510);
            this.SerreitorTypeLabel.Name = "SerreitorTypeLabel";
            this.SerreitorTypeLabel.Size = new System.Drawing.Size(160, 25);
            this.SerreitorTypeLabel.TabIndex = 31;
            this.SerreitorTypeLabel.Text = "Тип серрейтора";
           
            // 
            // SerreitorToolTip
            // 
            this.SerreitorToolTip.AutoPopDelay = 5000;
            this.SerreitorToolTip.InitialDelay = 15;
            this.SerreitorToolTip.ReshowDelay = 100;
            // 
            // SerreitorNumberTextBox
            // 
            this.SerreitorNumberTextBox.Location = new System.Drawing.Point(235, 557);
            this.SerreitorNumberTextBox.Name = "SerreitorNumberTextBox";
            this.SerreitorNumberTextBox.Size = new System.Drawing.Size(118, 22);
            this.SerreitorNumberTextBox.TabIndex = 34;
            this.SerreitorNumberTextBox.Text = "8";
            this.Max_Min_Value.SetToolTip(this.SerreitorNumberTextBox, "Допустимые значения: 8..32\r\n(ПРИМЕЧАНИЕ - Если выбран мелкий серрейтор, то зубьев" +
        " будет  в 2 раза больше!)\r\n");
            this.SerreitorNumberTextBox.Leave += new System.EventHandler(this.SerreitorNumberTextBox_Leave);
            // 
            // SerreitorNumberLabel
            // 
            this.SerreitorNumberLabel.AutoSize = true;
            this.SerreitorNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SerreitorNumberLabel.ForeColor = System.Drawing.Color.Black;
            this.SerreitorNumberLabel.Location = new System.Drawing.Point(12, 553);
            this.SerreitorNumberLabel.Name = "SerreitorNumberLabel";
            this.SerreitorNumberLabel.Size = new System.Drawing.Size(190, 25);
            this.SerreitorNumberLabel.TabIndex = 33;
            this.SerreitorNumberLabel.Text = "Количество зубьев";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(813, 695);
            this.Controls.Add(this.SerreitorNumberTextBox);
            this.Controls.Add(this.SerreitorNumberLabel);
            this.Controls.Add(this.SerreitorTypeComboBox);
            this.Controls.Add(this.SerreitorTypeLabel);
            this.Controls.Add(this.SerreitorLengthTextBox);
            this.Controls.Add(this.SerreitorLengthLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.serreitorCheckBox);
            this.Controls.Add(this.SerreitorExistenceLabel);
            this.Controls.Add(this.TextBoxError);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ButtonBuild);
            this.Controls.Add(this.TextBoxBindingLength);
            this.Controls.Add(this.BindingLengthLabel);
            this.Controls.Add(this.ComboBoxTypeBinding);
            this.Controls.Add(this.CheckBoxPeakBlade);
            this.Controls.Add(this.ComboBoxTypeBlade);
            this.Controls.Add(this.BindingLabel);
            this.Controls.Add(this.TextBoxPeakLength);
            this.Controls.Add(this.PeakLengthLabel);
            this.Controls.Add(this.TextBoxEdgeWidth);
            this.Controls.Add(this.EdgeWidthLabel);
            this.Controls.Add(this.PeakExistanceLabel);
            this.Controls.Add(this.BladeTypeLabel);
            this.Controls.Add(this.TextBoxBladeThickness);
            this.Controls.Add(this.BladeThickLabel);
            this.Controls.Add(this.TextBoxWidth);
            this.Controls.Add(this.BladeWidthLabel);
            this.Controls.Add(this.TextBoxLength);
            this.Controls.Add(this.BladeLengthLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Клинок для ножа";
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label BladeLengthLabel;
        private System.Windows.Forms.TextBox TextBoxLength;
        private System.Windows.Forms.TextBox TextBoxWidth;
        private System.Windows.Forms.Label BladeWidthLabel;
        private System.Windows.Forms.Label BladeTypeLabel;
        private System.Windows.Forms.TextBox TextBoxBladeThickness;
        private System.Windows.Forms.Label BladeThickLabel;
        private System.Windows.Forms.TextBox TextBoxEdgeWidth;
        private System.Windows.Forms.Label EdgeWidthLabel;
        private System.Windows.Forms.Label PeakExistanceLabel;
        private System.Windows.Forms.Label BindingLabel;
        private System.Windows.Forms.TextBox TextBoxPeakLength;
        private System.Windows.Forms.Label PeakLengthLabel;
        private System.Windows.Forms.ComboBox ComboBoxTypeBlade;
        private System.Windows.Forms.CheckBox CheckBoxPeakBlade;
        private System.Windows.Forms.ComboBox ComboBoxTypeBinding;
        private System.Windows.Forms.TextBox TextBoxBindingLength;
        private System.Windows.Forms.Label BindingLengthLabel;
        private System.Windows.Forms.Button ButtonBuild;
        private System.Windows.Forms.RichTextBox TextBoxError;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip Max_Min_Value;
        private System.Windows.Forms.CheckBox serreitorCheckBox;
        private System.Windows.Forms.Label SerreitorExistenceLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SerreitorLengthTextBox;
        private System.Windows.Forms.Label SerreitorLengthLabel;
        private System.Windows.Forms.ComboBox SerreitorTypeComboBox;
        private System.Windows.Forms.Label SerreitorTypeLabel;
        private System.Windows.Forms.ToolTip SerreitorToolTip;
        private System.Windows.Forms.TextBox SerreitorNumberTextBox;
        private System.Windows.Forms.Label SerreitorNumberLabel;
    }
}