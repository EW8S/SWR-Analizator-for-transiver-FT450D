namespace FT450D
{
    partial class Form2
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
            this.btnSetSettings = new System.Windows.Forms.Button();
            this.cbRate = new System.Windows.Forms.ComboBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnSetSettings
            // 
            this.btnSetSettings.Location = new System.Drawing.Point(37, 189);
            this.btnSetSettings.Name = "btnSetSettings";
            this.btnSetSettings.Size = new System.Drawing.Size(121, 26);
            this.btnSetSettings.TabIndex = 0;
            this.btnSetSettings.Text = "Применить";
            this.btnSetSettings.UseVisualStyleBackColor = true;
            this.btnSetSettings.Click += new System.EventHandler(this.btnSetSettings_Click);
            // 
            // cbRate
            // 
            this.cbRate.FormattingEnabled = true;
            this.cbRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400"});
            this.cbRate.Location = new System.Drawing.Point(37, 113);
            this.cbRate.Name = "cbRate";
            this.cbRate.Size = new System.Drawing.Size(121, 21);
            this.cbRate.TabIndex = 1;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(37, 163);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(121, 20);
            this.tbPort.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Скорость COM-порта";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Порт сервера";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Язык";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Русский",
            "English"});
            this.comboBox1.Location = new System.Drawing.Point(37, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 227);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.cbRate);
            this.Controls.Add(this.btnSetSettings);
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetSettings;
        private System.Windows.Forms.ComboBox cbRate;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}