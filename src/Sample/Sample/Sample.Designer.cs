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
            this.lbPortName = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbxPortName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbPortName
            // 
            this.lbPortName.AutoSize = true;
            this.lbPortName.Font = new System.Drawing.Font("Georgia", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPortName.Location = new System.Drawing.Point(16, 13);
            this.lbPortName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPortName.Name = "lbPortName";
            this.lbPortName.Size = new System.Drawing.Size(72, 17);
            this.lbPortName.TabIndex = 14;
            this.lbPortName.Text = "SerialPort";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Georgia", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(216, 11);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(33, 34);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // cbxPortName
            // 
            this.cbxPortName.BackColor = System.Drawing.SystemColors.Control;
            this.cbxPortName.Font = new System.Drawing.Font("Georgia", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPortName.FormattingEnabled = true;
            this.cbxPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cbxPortName.Location = new System.Drawing.Point(111, 11);
            this.cbxPortName.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cbxPortName.Name = "cbxPortName";
            this.cbxPortName.Size = new System.Drawing.Size(96, 25);
            this.cbxPortName.TabIndex = 13;
            // 
            // Sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 359);
            this.Controls.Add(this.lbPortName);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cbxPortName);
            this.Font = new System.Drawing.Font("Georgia", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Sample";
            this.Text = "Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPortName;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbxPortName;
    }
}

