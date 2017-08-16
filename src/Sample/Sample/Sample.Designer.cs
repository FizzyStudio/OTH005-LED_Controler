namespace Sample
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudFreq = new System.Windows.Forms.NumericUpDown();
            this.nudRatio = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbDuration = new System.Windows.Forms.Label();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 126);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "Duty Ratio(%)";
            // 
            // nudFreq
            // 
            this.nudFreq.Font = new System.Drawing.Font("Georgia", 14.25F);
            this.nudFreq.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudFreq.Location = new System.Drawing.Point(178, 75);
            this.nudFreq.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFreq.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudFreq.Name = "nudFreq";
            this.nudFreq.ReadOnly = true;
            this.nudFreq.Size = new System.Drawing.Size(66, 29);
            this.nudFreq.TabIndex = 18;
            this.nudFreq.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // nudRatio
            // 
            this.nudRatio.Font = new System.Drawing.Font("Georgia", 14.25F);
            this.nudRatio.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRatio.Location = new System.Drawing.Point(178, 120);
            this.nudRatio.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRatio.Name = "nudRatio";
            this.nudRatio.ReadOnly = true;
            this.nudRatio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudRatio.Size = new System.Drawing.Size(66, 29);
            this.nudRatio.TabIndex = 19;
            this.nudRatio.Value = new decimal(new int[] {
            10,
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
            // lbDuration
            // 
            this.lbDuration.AutoSize = true;
            this.lbDuration.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDuration.Location = new System.Drawing.Point(16, 165);
            this.lbDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDuration.Name = "lbDuration";
            this.lbDuration.Size = new System.Drawing.Size(109, 23);
            this.lbDuration.TabIndex = 21;
            this.lbDuration.Text = "Duration(s)";
            // 
            // nudDuration
            // 
            this.nudDuration.Font = new System.Drawing.Font("Georgia", 14.25F);
            this.nudDuration.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudDuration.Location = new System.Drawing.Point(178, 163);
            this.nudDuration.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudDuration.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.ReadOnly = true;
            this.nudDuration.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudDuration.Size = new System.Drawing.Size(66, 29);
            this.nudDuration.TabIndex = 22;
            this.nudDuration.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.nudDuration);
            this.Controls.Add(this.lbDuration);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.nudRatio);
            this.Controls.Add(this.nudFreq);
            this.Controls.Add(this.label1);
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
            this.Text = "Sample";
            this.Load += new System.EventHandler(this.Sample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPortName;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbxPortName;
        private System.Windows.Forms.Label lbFreq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudFreq;
        private System.Windows.Forms.NumericUpDown nudRatio;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lbDuration;
        private System.Windows.Forms.NumericUpDown nudDuration;
    }
}

