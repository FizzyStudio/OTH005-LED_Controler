using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sample
{
    public partial class Sample : Form
    {
        private Device device;
        private Boolean isRunning;

        public Sample()
        {
            InitializeComponent();
        }

        private void InitVariables()
        {
            this.device = new Device();
            this.isRunning = false;
        }

        private void InitControls()
        {
            cbxPortName.SelectedIndex = cbxPortName.FindString(device.PortName);
        }

        private void Start()
        {   
            //  参数设置：55 AA 01 脉宽（10-10000us）周期（100-1000ms）                 应答：AA 55 01 (脉宽2000us 周期500ms: 55AA01 07D0 01F4)

            //  55 AA 03 脉宽（10-1000us 2B）测量光出现时间（10-2000 2B）光化光下降沿到测量光上升沿（100-1000us 2B）测量光下降沿到新的光化光上升沿（1000-10000us 2B）  
            //  应答 AA 55 03（脉宽500us 测量光出现时间50 光化光下降沿到测量光上升沿500us 测量光下降沿到新的光化光上升沿5000us: 55AA03 01F4 0032 01F4 1388） 

            int freq = (int)nudFreq.Value;
            int ratio = (int)tbRatio.Value;
            // int ratio = (int)nudRatio.Value;

            int usPeriod = 1000000 / freq;
            int usPulse = usPeriod / 100 * ratio;
            int usLow = usPeriod - usPulse;
            
            if (ratio >= 100)
            {
                usLow = 1;
            }
            else if (ratio <= 0)
            {
                Stop();
                this.isRunning = true;
                return;
            }

            int N = 2000;

            // config meas width
            byte[] paras = new byte[7];
            paras[0] = 0x55;
            paras[1] = 0xAA;
            paras[2] = 0x01;
            paras[3] = (byte)(usPulse >> 8);
            paras[4] = (byte)(usPulse % 256);
            paras[5] = (byte)(1000 >> 8);
            paras[6] = (byte)(1000 % 256);
            device.WriteCommand(paras);

            paras = new byte[11];
            paras[0] = 0x55;
            paras[1] = 0xAA;
            paras[2] = 0x03;
            paras[3] = (byte)(usPulse >> 8);
            paras[4] = (byte)(usPulse % 256);
            paras[5] = (byte)(N >> 8);
            paras[6] = (byte)(N % 256);
            paras[7] = (byte)(usLow >> 8);
            paras[8] = (byte)(usLow % 256);
            paras[9] = (byte)(usLow >> 8);
            paras[10] = (byte)(usLow % 256);

            device.WriteCommand(paras);

            //2 进入光化光模式
            byte[] mode = new byte[] { 0x55, 0xAA, 0x09, 0x02 };
            device.WriteCommand(mode);

            this.isRunning = true;
        }

        private void Stop()
        {
            byte[] mode = new byte[] { 0x55, 0xAA, 0x0D };
            device.WriteCommand(mode);
            this.isRunning = false;
        }

        private void Sample_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControls();
        }

        private void RefreshControlers()
        {
            cbxPortName.Enabled = !device.IsOpen;

            btnConnect.Enabled = !this.isRunning;
            btnConnect.Image = device.IsOpen ? global::Sample.Properties.Resources.Stop : global::Sample.Properties.Resources.Run;

            btnStart.Text = !this.isRunning ? "Start" : "Stop";
            btnStart.Enabled = this.device.IsOpen;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!this.device.IsOpen)
            {
                MessageBox.Show("Please connect serial port firstly.");
            }
            else
            {
                if (this.isRunning)
                {
                    Stop();
                }
                else
                {
                    Start();
                }
                RefreshControlers();
            }
        }

        /// <summary>
        /// Connect or DisConnect Device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (device.IsOpen)
            {
                device.DisConnect();
            }
            else
            {
                device.PortName = cbxPortName.SelectedItem.ToString();
                device.Connect();
            }
            RefreshControlers();
        }

        private void nudRatio_ValueChanged(object sender, EventArgs e)
        {
            if (this.isRunning)
            {
                Start();
            }
        }

        private void tbRatio_ValueChanged(object sender, EventArgs e)
        {
            if (this.isRunning)
            {
                Start();
            }
        }   

    }
}
