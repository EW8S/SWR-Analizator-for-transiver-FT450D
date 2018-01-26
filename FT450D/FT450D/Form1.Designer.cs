namespace FT450D
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea14 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend14 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbStart = new System.Windows.Forms.TextBox();
            this.tbStop = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStep = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(12, 12);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(120, 23);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "Найти трансивер";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Диапазон измерения, кГц";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(576, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Шаг, кГц";
            // 
            // tbStart
            // 
            this.tbStart.Location = new System.Drawing.Point(348, 14);
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(100, 20);
            this.tbStart.TabIndex = 5;
            this.tbStart.Text = "7000";
            this.tbStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStart_KeyPress);
            // 
            // tbStop
            // 
            this.tbStop.Location = new System.Drawing.Point(470, 14);
            this.tbStop.Name = "tbStop";
            this.tbStop.Size = new System.Drawing.Size(100, 20);
            this.tbStop.TabIndex = 6;
            this.tbStop.Text = "7200";
            this.tbStop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStop_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(454, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "-";
            // 
            // tbStep
            // 
            this.tbStep.Location = new System.Drawing.Point(633, 14);
            this.tbStep.Name = "tbStep";
            this.tbStep.Size = new System.Drawing.Size(67, 20);
            this.tbStep.TabIndex = 8;
            this.tbStep.Text = "10";
            this.tbStep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStep_KeyPress);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(706, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Начать";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(879, 11);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(93, 23);
            this.btnSettings.TabIndex = 10;
            this.btnSettings.Text = "Настройки";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // chart1
            // 
            chartArea14.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea14);
            legend14.Enabled = false;
            legend14.Name = "Legend1";
            this.chart1.Legends.Add(legend14);
            this.chart1.Location = new System.Drawing.Point(12, 50);
            this.chart1.Name = "chart1";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series14.Legend = "Legend1";
            series14.Name = "Series1";
            this.chart1.Series.Add(series14);
            this.chart1.Size = new System.Drawing.Size(960, 300);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "chart1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 362);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tbStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbStop);
            this.Controls.Add(this.tbStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFind);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "КСВ FT450D";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbStart;
        private System.Windows.Forms.TextBox tbStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStep;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
    }
}

