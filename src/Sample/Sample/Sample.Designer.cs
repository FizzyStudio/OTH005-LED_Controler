﻿namespace Sample
{
    partial class Sample
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sample));
            this.lbPortName = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbxPortName = new System.Windows.Forms.ComboBox();
            this.lbFreq = new System.Windows.Forms.Label();
            this.lbRation = new System.Windows.Forms.Label();
            this.nudFreq = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbRatio = new System.Windows.Forms.TrackBar();
            this.lbRationValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRatio)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPortName
            // 
            this.lbPortName.AutoSize = true;
            this.lbPortName.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPortName.Location = new System.Drawing.Point(16, 30);
            this.lbPortName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPortName.Name = "lbPortName";
            this.lbPortName.Size = new System.Drawing.Size(96, 23);
            this.lbPortName.TabIndex = 14;
            this.lbPortName.Text = "SerialPort";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Image = global::Sample.Properties.Resources.Run;
            this.btnConnect.Location = new System.Drawing.Point(238, 27);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(33, 31);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbxPortName
            // 
            this.cbxPortName.BackColor = System.Drawing.SystemColors.Control;
            this.cbxPortName.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPortName.FormattingEnabled = true;
            this.cbxPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.cbxPortName.Location = new System.Drawing.Point(120, 27);
            this.cbxPortName.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cbxPortName.Name = "cbxPortName";
            this.cbxPortName.Size = new System.Drawing.Size(96, 31);
            this.cbxPortName.TabIndex = 13;
            // 
            // lbFreq
            // 
            this.lbFreq.AutoSize = true;
            this.lbFreq.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFreq.Location = new System.Drawing.Point(16, 77);
            this.lbFreq.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFreq.Name = "lbFreq";
            this.lbFreq.Size = new System.Drawing.Size(86, 23);
            this.lbFreq.TabIndex = 15;
            this.lbFreq.Text = "Freq(Hz)";
            // 
            // lbRation
            // 
            this.lbRation.AutoSize = true;
            this.lbRation.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRation.Location = new System.Drawing.Point(16, 126);
            this.lbRation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbRation.Name = "lbRation";
            this.lbRation.Size = new System.Drawing.Size(132, 23);
            this.lbRation.TabIndex = 16;
            this.lbRation.Text = "Duty Ratio(%)";
            // 
            // nudFreq
            // 
            this.nudFreq.Enabled = false;
            this.nudFreq.Font = new System.Drawing.Font("Georgia", 14.25F);
            this.nudFreq.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFreq.Location = new System.Drawing.Point(168, 75);
            this.nudFreq.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFreq.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFreq.Name = "nudFreq";
            this.nudFreq.ReadOnly = true;
            this.nudFreq.Size = new System.Drawing.Size(76, 29);
            this.nudFreq.TabIndex = 18;
            this.nudFreq.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Georgia", 14.25F);
            this.btnStart.Location = new System.Drawing.Point(68, 216);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(148, 33);
            this.btnStart.TabIndex = 20;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbRatio
            // 
            this.tbRatio.Location = new System.Drawing.Point(12, 165);
            this.tbRatio.Maximum = 100;
            this.tbRatio.Name = "tbRatio";
            this.tbRatio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbRatio.RightToLeftLayout = true;
            this.tbRatio.Size = new System.Drawing.Size(259, 45);
            this.tbRatio.SmallChange = 5;
            this.tbRatio.TabIndex = 23;
            this.tbRatio.TickFrequency = 5;
            this.tbRatio.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbRatio.Value = 1;
            this.tbRatio.ValueChanged += new System.EventHandler(this.tbRatio_ValueChanged);
            // 
            // lbRationValue
            // 
            this.lbRationValue.AutoSize = true;
            this.lbRationValue.Font = new System.Drawing.Font("Georgia", 14.25F);
            this.lbRationValue.Location = new System.Drawing.Point(164, 126);
            this.lbRationValue.Name = "lbRationValue";
            this.lbRationValue.Size = new System.Drawing.Size(22, 23);
            this.lbRationValue.TabIndex = 24;
            this.lbRationValue.Text = "0";
            // 
            // Sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lbRationValue);
            this.Controls.Add(this.tbRatio);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.nudFreq);
            this.Controls.Add(this.lbRation);
            this.Controls.Add(this.lbFreq);
            this.Controls.Add(this.lbPortName);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cbxPortName);
            this.Font = new System.Drawing.Font("Georgia", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Sample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fizzy Led Controler Sample";
            this.Load += new System.EventHandler(this.Sample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRatio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPortName;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbxPortName;
        private System.Windows.Forms.Label lbFreq;
        private System.Windows.Forms.Label lbRation;
        private System.Windows.Forms.NumericUpDown nudFreq;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TrackBar tbRatio;
        private System.Windows.Forms.Label lbRationValue;
    }
}

