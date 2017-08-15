using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Vieworks;
using CFD_Camera;
using System.Runtime.InteropServices;

namespace CFD_V21_Debug
{
    public partial class Main : Form
    {
        Device device;          //设备类
        Camera camera;
        byte[] imageData;
        Bitmap currentImage;

        private delegate void DispImageDelegate();

        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControls();
        }

        /// <summary>
        /// 调试界面关闭前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (device.IsOpen)
                {
                    //关闭LED
                }
                device.DisConnect();
                if (camera.IsOpen)
                    camera.CloseCamera();
            }
            catch { }
        }


        private void InitVariables()
        {
            this.device = new Device();
            this.camera = new Camera();
            camera.pCallback += new VwGigE.ImageCallbackFn(GetImageEvent);
            if (camera.IsFound)
                camera.OpenCamera();
        }

        private void InitControls()
        {
            cbxPortName.SelectedIndex = cbxPortName.FindString(device.PortName);
            cbxMeasFreq.SelectedIndex = 0;
            btnConnect.BackgroundImage = device.IsOpen ? global::CFD_V21_Debug.Properties.Resources.Stop :
                global::CFD_V21_Debug.Properties.Resources.Run;

            byte gray;
            byte r = 0, g = 0, b = 0;
            Bitmap bmp = new Bitmap(pbxRainbow.Width, pbxRainbow.Height, PixelFormat.Format24bppRgb);
            for (int i = 0; i < pbxRainbow.Width; i++)
            {

                gray = (byte)(i * 255 / pbxRainbow.Width);
                GrayTORGB(gray, ref r, ref g, ref b);
                for (int j = 0; j < pbxRainbow.Height; j++)
                {
                    bmp.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
            pbxRainbow.Image = bmp;
            Refresh_pnlCamera();
        }

        /// <summary>
        /// 连接或断开设端口
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
            btnConnect.BackgroundImage = device.IsOpen ? global::CFD_V21_Debug.Properties.Resources.Stop :
                global::CFD_V21_Debug.Properties.Resources.Run;
        }

        /// <summary>
        /// 停止运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!device.IsOpen)
            {
                MessageBox.Show("设备端口未打开，请先打开设备端口");
                return;
            }
            Stop();
        }

        /// <summary>
        /// 运行测量光
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMeasRun_Click(object sender, EventArgs e)
        {
            if (!device.IsOpen)
            {
                MessageBox.Show("设备端口未打开，请先打开设备端口");
                return;
            }
            StartToMeas();
            camera.StartExternalTriggerGrabbing();
        }

        /// <summary>
        /// 运行光化光
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActRun_Click(object sender, EventArgs e)
        {
            if (!device.IsOpen)
            {
                MessageBox.Show("设备端口未打开，请先打开设备端口");
                return;
            }
            StartToAct();
            camera.StartExternalTriggerGrabbing();
        }

        /// <summary>
        /// 运行饱和光
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSatRun_Click(object sender, EventArgs e)
        {
            if (!device.IsOpen)
            {
                MessageBox.Show("设备端口未打开，请先打开设备端口");
                return;
            }
            StartToSat();
            camera.StartExternalTriggerGrabbing();
        }

        /// <summary>
        /// 运行GFP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGFPRun_Click(object sender, EventArgs e)
        {
            if (!device.IsOpen)
            {
                MessageBox.Show("设备端口未打开，请先打开设备端口");
                return;
            }
            StartToGFP();
            camera.StartExternalTriggerGrabbing();
        }

        /// <summary>
        /// 运行测量光
        /// </summary>
        private void StartToMeas()
        {
            //1 设置测量光参数 
            //  参数设置：55 AA 01 脉宽（10-10000us）周期（100-1000ms）                 应答：AA 55 01 (脉宽2000us 周期500ms: 55AA01 07D0 01F4)
            int measWidth = (Int32)nudMeasWidth.Value;
            int measPeriod = 1000 / (int.Parse(cbxMeasFreq.SelectedItem.ToString()));
            byte[] paras = new byte[7];
            paras[0] = 0x55;
            paras[1] = 0xAA;
            paras[2] = 0x01;
            paras[3] = (byte)(measWidth >> 8);
            paras[4] = (byte)(measWidth % 256);
            paras[5] = (byte)(measPeriod >> 8);
            paras[6] = (byte)(measPeriod % 256);

            device.WriteCommand(paras);

            //2 进入测量光模式
            byte[] mode = new byte[] { 0x55, 0xAA, 0x09, 0x01 };
            device.WriteCommand(mode);

            CmdShow(paras, mode);
        }

        /// <summary>
        /// 运行光化光
        /// </summary>
        private void StartToAct()
        {
            //1 设置光化光参数
            //  55 AA 03 脉宽（10-1000us 2B）测量光出现时间（10-2000 2B）光化光下降沿到测量光上升沿（100-1000us 2B）测量光下降沿到新的光化光上升沿（1000-10000us 2B）  
            //  应答 AA 55 03（脉宽500us 测量光出现时间50 光化光下降沿到测量光上升沿500us 测量光下降沿到新的光化光上升沿5000us: 55AA03 01F4 0032 01F4 1388） 
            int actWidth = (Int32)nudActWidth.Value;            
            int actDelay1 = (Int32)nudActDelay1.Value;
            int actDelay2 = (Int32)nudActDelay2.Value;
            int measPeriod = 1000 / (int.Parse(cbxMeasFreq.SelectedItem.ToString()));   //ms
            int N = (measPeriod - actDelay1 / 1000 - actDelay2 / 1000);

            byte[] paras = new byte[11];
            paras[0] = 0x55;
            paras[1] = 0xAA;
            paras[2] = 0x03;
            paras[3] = (byte)(actWidth >> 8);
            paras[4] = (byte)(actWidth % 256);
            paras[5] = (byte)(N >> 8);
            paras[6] = (byte)(N % 256);
            paras[7] = (byte)(actDelay1 >> 8);
            paras[8] = (byte)(actDelay1 % 256);
            paras[9] = (byte)(actDelay2 >> 8);
            paras[10] = (byte)(actDelay2 % 256);

            device.WriteCommand(paras);

            //2 进入光化光模式
            byte[] mode = new byte[] { 0x55, 0xAA, 0x09, 0x02 };
            device.WriteCommand(mode);

            CmdShow(paras, mode);

        }

        /// <summary>
        /// 饱和光
        /// </summary>
        private void StartToSat()
        { 
            //1 设置饱和光参数
            //  参数设置：55 AA 05 脉宽（100-1000us 2B）测量光出现时间（10-2000 2B）饱和光下降沿到测量光上升沿（100-1000us 2B）测量光下降沿到新的饱和光上升沿（1000-10000us 2B）        
            //  应答：AA 55 05（脉宽700us 测量光出现时间90 饱和光下降沿到测量光上升沿500us 测量光下降沿到新的饱和光上升沿5000us: 55AA05 02BC 005A 01F4 1388）    
            int satWidth = (Int32)nudSatWidth.Value;
            int satDelay1 = (Int32)nudSatDelay1.Value;
            int satDelay2 = (Int32)nudSatDelay2.Value;
            int measPeriod = 1000 / (int.Parse(cbxMeasFreq.SelectedItem.ToString()));   //ms
            int N = (measPeriod - satDelay1 / 1000 - satDelay2 / 1000);

            byte[] paras = new byte[11];
            paras[0] = 0x55;
            paras[1] = 0xAA;
            paras[2] = 0x05;
            paras[3] = (byte)(satWidth >> 8);
            paras[4] = (byte)(satWidth % 256);
            paras[5] = (byte)(N >> 8);
            paras[6] = (byte)(N % 256);
            paras[7] = (byte)(satDelay1 >> 8);
            paras[8] = (byte)(satDelay1 % 256);
            paras[9] = (byte)(satDelay2 >> 8);
            paras[10] = (byte)(satDelay2 % 256);
            device.WriteCommand(paras);

            //2 进入饱和光模式
            byte[] mode = new byte[] { 0x55, 0xAA, 0x09, 0x03 };
            device.WriteCommand(mode);

            CmdShow(paras, mode);

        }

        /// <summary>
        /// GFP
        /// </summary>
        private void StartToGFP()
        {
            //1 设置GFP参数 
            //  55 AA 03 脉宽（10-1000us 2B）测量光出现时间（10-2000 2B）光化光下降沿到测量光上升沿（100-1000us 2B）测量光下降沿到新的光化光上升沿（1000-10000us 2B）  
            //  应答 AA 55 03（脉宽500us 测量光出现时间50 光化光下降沿到测量光上升沿500us 测量光下降沿到新的光化光上升沿5000us: 55AA03 01F4 0032 01F4 1388） 
            int gfpWidth = (Int32)nudGFPWidth.Value;

            int gfpDelay1 = (Int32)nudGFPDelay1.Value;
            int gfpDelay2 = (Int32)nudGFPDelay2.Value;
            int measPeriod = 1000 / (int.Parse(cbxMeasFreq.SelectedItem.ToString()));
            int N = (measPeriod - gfpDelay1 / 1000 - gfpDelay2 / 1000);

            byte[] paras = new byte[11];
            paras[0] = 0x55;
            paras[1] = 0xAA;
            paras[2] = 0x03;
            paras[3] = (byte)(gfpWidth >> 8);
            paras[4] = (byte)(gfpWidth % 256);
            paras[5] = (byte)(N >> 8);
            paras[6] = (byte)(N % 256);
            paras[7] = (byte)(gfpDelay1 >> 8);
            paras[8] = (byte)(gfpDelay1 % 256);
            paras[9] = (byte)(gfpDelay2 >> 8);
            paras[10] = (byte)(gfpDelay2 % 256);

            device.WriteCommand(paras);

            //2 进入GFP模式
            byte[] mode = new byte[] { 0x55, 0xAA, 0x09, 0x04 };
            device.WriteCommand(mode);

            CmdShow(paras, mode);
           
        }

        /// <summary>
        /// 停止
        /// </summary>
        private void Stop()
        {
            byte[] mode = new byte[] { 0x55, 0xAA, 0x0D };
            device.WriteCommand(mode);
            CmdShow(mode);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfig_Click(object sender, EventArgs e)
        {
            // 配置CCD延迟量
            // 命令格式 55 AA 07 延时符号（1 提前，0 滞后）延时量（0-100us）
            int cameraDelay = (Int32)nudLEDDelay.Value;
            byte[] paras = new byte[] { 0x55, 0xAA, 0x07, 0x01, (byte)cameraDelay };
            device.WriteCommand(paras);
            CmdShow(paras);
        }

        /// <summary>
        /// 显示发送的命令
        /// </summary>
        /// <param name="cmd"></param>
        private void CmdShow(byte[] cmd)
        {
            string cmdString = String.Empty;
            foreach (byte data in cmd)
            {
                cmdString += string.Concat(' ', data.ToString());
            }
            tbxSend.Text = cmdString;
        }

        /// <summary>
        /// 显示发送的命令
        /// </summary>
        /// <param name="cmd"></param>
        private void CmdShow(byte[] cmd1, byte[] cmd2)
        {
            string cmdString = String.Empty;
            foreach (byte data in cmd1)
            {
                cmdString += string.Concat(' ', data.ToString());
            }
            foreach (byte data in cmd2)
            {
                cmdString += string.Concat(' ', data.ToString());
            }
            tbxSend.Text = cmdString;
        }

        /// <summary>
        /// 灰度值转RGB值
        /// </summary>
        /// <param name="gray"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public void GrayTORGB(byte gray, ref byte r, ref byte g, ref byte b)
        {
            double fgray, fr, fg, fb;
            fgray = gray / (255.0);
            double mesh = 0.020;
            if (fgray <= mesh)
            {
                fr = 0;
                fg = 0;
                fb = 0;
            }
            else if (fgray <= mesh + 0.25)
            {
                fr = 1;
                fg = 4.0 * (fgray - mesh);
                fb = 0;
            }
            else if (fgray <= mesh + 0.5)
            {
                fr = 4.0 * (0.5 - (fgray - mesh));
                fg = 1;
                fb = 0;
            }
            else if (fgray <= mesh + 0.75)
            {
                fr = 0;
                fg = 4.0 * (0.75 - (fgray - mesh));
                fb = 4.0 * ((fgray - mesh) - 0.5);
            }
            else
            {
                fr = 0;
                fg = 0;
                fb = 4.0 * (1 - (fgray - mesh));
            }
            Byte maxByte = Byte.MaxValue;
            r = Convert.ToByte(fr * maxByte);
            g = Convert.ToByte(fg * maxByte);
            b = Convert.ToByte(fb * maxByte);
        }

        /// <summary>
        /// 图像获取事件
        /// </summary>
        /// <param name="pObjectInfo"></param>
        /// <param name="pImageInfo"></param>
        public unsafe void GetImageEvent(IntPtr pObjectInfo, ref IMAGE_INFO pImageInfo)
        {
            if (IntPtr.Zero == pImageInfo.pImage)
            {
                System.Diagnostics.Trace.WriteLine("Image pointer is NULL!");
                return;
            }
            if (pImageInfo.pixelFormat != PIXEL_FORMAT.PIXEL_FORMAT_MONO12)
            {
                Console.WriteLine("PIXEL_FORMAT is not PIXEL_FORMAT_MONO12~");
                return;
            }
            this.imageData = camera.GetImageData(pImageInfo.pImage);
            ShowImage();
        }


        /// <summary>
        /// 刷新图像显示区
        /// </summary>
        private void ShowImage()
        {
            if (this.pbxImage.InvokeRequired == false)
            {
                if (currentImage != null)
                    currentImage.Dispose();
                currentImage = GetColorImage();
                //currentImage = GetGrayImage();
                if (currentImage != null)
                    pbxImage.Image = currentImage;
            }
            else
            {
                DispImageDelegate disp = new DispImageDelegate(ShowImage);
                this.pbxImage.Invoke(disp);
            }
        }

        /// <summary>
        /// 刷新CCD栏相关控件
        /// </summary>
        private void Refresh_pnlCamera()
        {
            tbxCameraName.Text = camera.IsFound ? camera.Name : "";
            btnCameraSearch.Enabled = !camera.IsOpen;      //搜索在相机未打开时使能
            nudCameraGain.Value = camera.CurGain;
            nudCameraGain.Enabled = camera.IsOpen;         //增益在相机打开时使能
            btnCameraOpen.Enabled = camera.IsFound && !camera.IsGrabbing;
            btnCameraOpen.Text = camera.IsOpen ? "关闭" : "打开";
        }

        /// <summary>
        /// 搜索相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCameraSearch_Click(object sender, EventArgs e)
        {
            if (camera.IsOpen || camera.IsFound)
                return;
            camera.SearchCamera();
            Refresh_pnlCamera();
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCameraOpen_Click(object sender, EventArgs e)
        {
            if (camera.IsOpen)
                camera.CloseCamera();
            else
                camera.OpenCamera();
            Refresh_pnlCamera();
        }

        /// <summary>
        /// 设置相机增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudCameraGain_ValueChanged(object sender, EventArgs e)
        {
            if (camera.IsOpen)
                camera.SetGain((int)nudCameraGain.Value);
        }

        /// <summary>
        /// byte[]数据转化为Bitmap图像
        /// </summary>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="RawData"></param>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        private Bitmap CreateBitmap(int nWidth, int nHeight, Byte[] RawData, PixelFormat pixelFormat)
        {
            try
            {
                Bitmap Canvas = new Bitmap(nWidth, nHeight, pixelFormat);
                BitmapData CanvasData = Canvas.LockBits(new Rectangle(0, 0, nWidth, nHeight),
                                                         ImageLockMode.WriteOnly, pixelFormat);
                IntPtr ptr = CanvasData.Scan0;
                Marshal.Copy(RawData, 0, ptr, RawData.Length);
                Canvas.UnlockBits(CanvasData);
                return Canvas;
            }
            catch (Exception e)
            {
                Console.WriteLine("CreateBitmap:" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取彩色图
        /// </summary>
        /// <param name="array"></param>
        public Bitmap GetColorImage()
        {
            Byte[] data = new byte[camera.Width * camera.Height * 3];
            PixelFormat DrawPixelFormat = PixelFormat.Format24bppRgb;
            int len = camera.Width * camera.Height;
            int i, j;
            try
            {
                imageData.CopyTo(data, 0);
                for (i = 0; i < len; i++)
                {
                    j = 3 * i;
                    GrayTORGB(data[j], ref data[j + 2], ref data[j + 1], ref data[j]);
                }
                Bitmap bmpColor = CreateBitmap(camera.Width, camera.Height, data, DrawPixelFormat);
                if (bmpColor == null)
                {
                    Console.WriteLine("GetColorfulImage Failed");
                }
                return bmpColor;
            }
            catch
            {
                return null;
            }
        }

        /// <summarray>
        /// 获取灰度图
        /// </summary> 
        /// <param name="array"></param>
        private Bitmap GetGrayImage()
        {
            PixelFormat DrawPixelFormat = PixelFormat.Format24bppRgb;
            Bitmap bmpGray;
            bmpGray = CreateBitmap(camera.Width, camera.Height, this.imageData, DrawPixelFormat);
            if (bmpGray == null)
            {
                Console.WriteLine("GetGrayImage Failed");
            }
            return bmpGray;
        }

        private void btnActConfig_Click(object sender, EventArgs e)
        {
            int actWidth = (Int32)nudActWidth.Value;
            int actDelay1 = (Int32)nudActDelay1.Value;
            int actDelay2 = (Int32)nudActDelay2.Value;
            int measPeriod = 1000 / (int.Parse(cbxMeasFreq.SelectedItem.ToString()));   //ms
            int N = (measPeriod - actDelay1 / 1000 - actDelay2 / 1000);

            byte[] paras = new byte[11];
            paras[0] = 0x55;
            paras[1] = 0xAA;
            paras[2] = 0x03;
            paras[3] = (byte)(actWidth >> 8);
            paras[4] = (byte)(actWidth % 256);
            paras[5] = (byte)(N >> 8);
            paras[6] = (byte)(N % 256);
            paras[7] = (byte)(actDelay1 >> 8);
            paras[8] = (byte)(actDelay1 % 256);
            paras[9] = (byte)(actDelay2 >> 8);
            paras[10] = (byte)(actDelay2 % 256);

            device.WriteCommand(paras);
            CmdShow(paras);
        }

    }
}
