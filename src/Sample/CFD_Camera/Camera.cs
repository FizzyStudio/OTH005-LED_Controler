using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Vieworks;
using VWGIGE_HANDLE = System.IntPtr;
using HINTERFACE = System.IntPtr;
using HCAMERA = System.IntPtr;
using System.IO;
using System.Drawing;
using CFD_Define;

namespace CFD_Camera
{
    public class Camera
    {
        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long perfcount);
        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long freq);


        private Size ccdSize;
        private int maxGain;
        private int minGain;

        private int curGain;
        private int frameRate;

        private int expTime;        //曝光时间
        private int acqTimeOut;

        private bool isCCDFound;
        private bool isCCDOpen;
        private bool isGrabbing;
        //private bool isContinousGrab;
        //private bool isTriggerGrab;

        int m_imagebuffernumber;
        long m_liFreq;
        long m_nMinInterFrameTime = 0;
        PIXEL_FORMAT pixel_format = PIXEL_FORMAT.PIXEL_FORMAT_MONO12;

        VWGIGE_HANDLE m_pvwGigE = IntPtr.Zero;          //VwGigE系统模块
        HCAMERA m_pCamera = IntPtr.Zero;                //CCD句柄
        CAMERA_INFO_STRUCT cameraInfoStruct;            //CCD信息

        public Vieworks.VwGigE.ImageCallbackFn pCallback;
        IntPtr m_pobjectInfo = IntPtr.Zero;
        GCHandle gchCallback;                           //GCHandle结构，提供用于从非托管内存访问托管对象的方法
        GCHandle gchobjectInfo;
        GCHandle gchGigE;

        #region 属性

        public int Width
        { get { return ccdSize.Width; } }
        public int Height
        { get { return ccdSize.Height; } }
        public int MaxGain
        { get { return maxGain; } }
        public int MinGain
        { get { return minGain; } }
        public int CurGain
        { get { return curGain; } }
        public int FrameRate
        { get { return frameRate; } }
        public int ExpTime
        { get { return expTime; } }
        public string Name
        { get { return cameraInfoStruct.name; } }
        public bool IsFound
        { get { return isCCDFound; } }
        public bool IsOpen
        { get { return isCCDOpen; } }
        public bool IsGrabbing
        { get { return isGrabbing; } }
        public string IP
        { get { return cameraInfoStruct.model; } }

        #endregion



        public Camera()
        {
            m_imagebuffernumber = 10;                       //图像缓冲区个数设置为300
            QueryPerformanceFrequency(out m_liFreq);        //获取系统频率
            m_nMinInterFrameTime = (long)(m_liFreq / 30);   //最小中断帧时间
            ccdSize = new Size(GlobalParas.Width, GlobalParas.Height);

            isCCDFound = false;
            isCCDOpen = false;
            isGrabbing = false;

            InitializeFromFile();
            GetParasFromFile(GlobalParas.cfgFile);

            RESULT result = Vieworks.VwGigE.OpenVwGigE(ref m_pvwGigE);     //打开VwGigE系统模块
            //一个新的GCHandle,它保护对象不被垃圾回收。当不再需要GCHandle时，必须通过Free将其释放
            gchGigE = GCHandle.Alloc(m_pvwGigE);
            SearchCamera();

        }


        /// <summary>
        /// 初始化固定参数
        /// </summary>
        private void InitializeFromFile()
        {
            string initfile = GlobalParas.initFile;
            expTime = XMLConfig.GetConfigData(initfile, "exposure_time", 1000);
            acqTimeOut = XMLConfig.GetConfigData(initfile, "acquire_timeout", 1000000);
            maxGain = XMLConfig.GetConfigData(initfile, "maxGain", 40);
            minGain = XMLConfig.GetConfigData(initfile, "minGain", 1);
        }

        /// <summary>
        /// 初始化可配置参数
        /// </summary>
        /// <param name="cfgfile"></param>
        public void GetParasFromFile(string cfgfile)
        {
            frameRate = XMLConfig.GetConfigData(cfgfile, "frameRate", 10);
            curGain = XMLConfig.GetConfigData(cfgfile, "curGain", 10);
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        public unsafe bool OpenCamera()
        {
            if (m_pvwGigE == null || isCCDFound == false)
            {
                MessageBox.Show("Open camera failed");
                isCCDOpen = false;
                return false;
            }

            OBJECT_INFO m_objectInfo = new OBJECT_INFO();       //新建OBJECT_INFO类的对象
            gchobjectInfo = GCHandle.Alloc(m_objectInfo);       //GCHandle的对象gchobjectInfo，防m_objectInfo被回收

            //Marshal类提供了一个方法集，这些方法用于分配非托管内存、复制非托管内存块、将托管类型转换为非托管类型
            //此外还提供了在与非托管代码交互时使用的其他杂项方法。
            //Marshal.SizeOf(m_objectInfo)--返回m_objectInfo的非托管大小
            //Marshal.AllocHGlobal(Int32)通过使用指定的字节数，从进程的非托管内存中分配内存
            m_pobjectInfo = Marshal.AllocHGlobal(Marshal.SizeOf(m_objectInfo));

            //struct -> pointer
            //将数据从托管对象(m_objectInfo)封送到非托管内存块(m_pobjectInfo[m_nCurrentDeviceIndex])
            Marshal.StructureToPtr(m_objectInfo, m_pobjectInfo, true);
            GCHandle.Alloc(m_pobjectInfo);

            //Vieworks.VwGigE.ImageCallbackFn CallbackFunc = GetImageEvent;       //声明托管变量CallbackFunc
            //Vieworks.VwGigE.ImageCallbackFn pCallback = new Vieworks.VwGigE.ImageCallbackFn(CallbackFunc);
            //pCallback = new Vieworks.VwGigE.ImageCallbackFn(GetImageEvent);
            gchCallback = GCHandle.Alloc(pCallback);

            //Marshal.GetFunctionPointerForDelegate(),将委托转换为可从非托管代码调用的函数指针
            IntPtr ptrCallback = Marshal.GetFunctionPointerForDelegate(pCallback);

            RESULT result = Vieworks.RESULT.RESULT_ERROR;
            //打开CCD句柄
            result = Vieworks.VwGigE.VwOpenCameraByIndex(m_pvwGigE, 0, ref m_pCamera, m_imagebuffernumber, 0, 0,
                                             0, m_pobjectInfo, ptrCallback, IntPtr.Zero);
            if (result != Vieworks.RESULT.RESULT_SUCCESS)
            {
                MessageBox.Show("Open camera failed");
                isCCDOpen = false;
                return false;
            }

            ((OBJECT_INFO*)m_pobjectInfo)->pVwCamera = m_pCamera;

            #region 调试代码
            int nWidth = 0;
            int nHeight = 0;
            Vieworks.VwGigE.CameraGetWidth(m_pCamera, ref nWidth);        //当前CCD像素宽
            Vieworks.VwGigE.CameraGetHeight(m_pCamera, ref nHeight);      //当前CCD像素高

            int nLineupNum = 0;             //获取支持的相机格式
            PIXEL_FORMAT pixelFormat = Vieworks.PIXEL_FORMAT.PIXEL_FORMAT_MONO8;
            List<PIXEL_FORMAT> plstPixelFormat = new List<PIXEL_FORMAT>();
            Vieworks.VwGigE.CameraGetPixelFormatLineupNum(m_pCamera, ref nLineupNum);
            for (int i = 0; i < nLineupNum; i++)
            {
                Vieworks.VwGigE.CameraGetPixelFormatLineup(m_pCamera, i, ref pixelFormat);
                plstPixelFormat.Add(pixelFormat);
            }

            int nPixelSize = 0;             //获取支持的像素位数
            List<string> plstStrPixelSize = new List<string>();
            Vieworks.VwGigE.CameraGetPixelSizeLineupNum(m_pCamera, ref nLineupNum);
            for (int i = 0; i < nLineupNum; i++)
            {
                Vieworks.VwGigE.CameraGetPixelSizeLineup(m_pCamera, i, ref nPixelSize);
                string strPixelSize = String.Format("Bpp{0}", nPixelSize);
                plstStrPixelSize.Add(strPixelSize);
            }
            #endregion

            #region 设置CCD相关
            //获取CCD像素宽和高
            Vieworks.VwGigE.CameraSetWidth(m_pCamera, ccdSize.Width);        //设置CCD像素宽
            Vieworks.VwGigE.CameraSetHeight(m_pCamera, ccdSize.Height);      //设置CCD像素高
            //ccdSize.Width = nWidth;
            //ccdSize.Height = nHeight;
            //设置CCD像素格式、像素位数、输出模式
            Vieworks.VwGigE.CameraSetPixelFormat(m_pCamera, pixel_format);    //单色8位模式
            Vieworks.VwGigE.CameraSetReadoutMode(m_pCamera, Vieworks.READOUT.READOUT_NORMAL);
            //设置CCD曝光时间模式为Timed模式，设置曝光时间
            Vieworks.VwGigE.CameraSetExposureMode(m_pCamera, EXPOSURE_MODE.EXPOSURE_MODE_TIMED);  //设置曝光时间模式为定时模式
            Vieworks.VwGigE.CameraSetExposureTime(m_pCamera, expTime);   //us
            //获取和设置捕捉等待时间
            //int pnTimeOut = 0;
            //Vieworks.VwGigE.CameraGetAcquisitionTimeOut(m_pCamera, ref pnTimeOut);
            Vieworks.VwGigE.CameraSetAcquisitionTimeOut(m_pCamera, acqTimeOut);  //触发延迟，us级
            //设置触发模式相关
            Vieworks.VwGigE.CameraSetTriggerSource(m_pCamera, TRIGGER_SOURCE.TRIGGER_SOURCE_EXT); //外部硬件触发模式
            Vieworks.VwGigE.CameraSetTriggerActivation(m_pCamera, TRIGGER_ACTIVATION.TRIGGER_ACTIVATION_RISINGEDGE);  //上升沿触发
            Vieworks.VwGigE.CameraSetTriggerMode(m_pCamera, false);     //暂不使能外部触发
            //设置CCD增益
            Vieworks.VwGigE.CameraSetCustomCommand(m_pCamera, "Gain", curGain.ToString());
            //设置缓冲区的格式和大小
            result = Vieworks.VwGigE.CameraChangeBufferFormat(m_pCamera, m_imagebuffernumber, ccdSize.Width, ccdSize.Height, PIXEL_FORMAT.PIXEL_FORMAT_MONO12);
            if (Vieworks.RESULT.RESULT_SUCCESS != result)
            {
                MessageBox.Show("Can't change the camera buffer.");
                isCCDOpen = false;
                return false;
            }
            #endregion

            isCCDOpen = true;
            return true;
        }


        /// <summary>
        /// 关闭相机
        /// </summary>
        public void CloseCamera()
        {
            if (IsGrabbing)
                StopGrab();
            Vieworks.VwGigE.CameraClose(m_pCamera);
            m_pCamera = IntPtr.Zero;
            if (gchCallback.IsAllocated)
                gchCallback.Free();
            if (gchobjectInfo.IsAllocated)
                gchobjectInfo.Free();
            isCCDOpen = false;
        }


        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gain"></param>
        public void SetGain(int gain)
        {
            curGain = gain;
            Vieworks.VwGigE.CameraSetCustomCommand(m_pCamera, "Gain", curGain.ToString());
        }

        /// <summary>
        /// 设置帧频
        /// </summary>
        /// <param name="rate"></param>
        public void SetFrameRate(int rate)
        {
            frameRate = rate;
            Vieworks.VwGigE.CameraSetCustomCommand(m_pCamera, "AcquisitionFrameRate", frameRate.ToString());
        }

        /// <summary>
        /// 搜索相机
        /// </summary>
        public void SearchCamera()
        {
            if (IntPtr.Zero == m_pvwGigE)
            {
                MessageBox.Show("m_pvwGigE has NULL pointer.");
                return;
            }
            RESULT ret = Vieworks.VwGigE.VwDiscovery(m_pvwGigE);        //更新可用的接口和设备列表
            if (Vieworks.RESULT.RESULT_SUCCESS != ret)
                MessageBox.Show("Failed to discovery.");
            int nCameraNum = 0;
            ret = Vieworks.VwGigE.VwGetNumCameras(m_pvwGigE, ref nCameraNum);     //获取当前可用的CCD数
            if (Vieworks.RESULT.RESULT_SUCCESS != ret)
                MessageBox.Show("Failed to access camera.");
            if (nCameraNum > 0)
            {
                isCCDFound = true;
                Vieworks.VwGigE.VwDiscoveryCameraInfo(m_pvwGigE, 0, ref cameraInfoStruct);  //获取CCD的信息
            }
            else
            {
                isCCDFound = false;
                MessageBox.Show("No cameras fonud.");
            }
        }


        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="usTime"></param>
        public void SetExposureTime(int usTime)
        {
            expTime = usTime;
            Vieworks.VwGigE.CameraSetExposureTime(m_pCamera, expTime);   //us
        }

        /// <summary>
        /// 停止采集[连续或外部]
        /// </summary>
        public void StopGrab()
        {
            Vieworks.RESULT ret = Vieworks.VwGigE.CameraAbort(m_pCamera);
            if (ret == Vieworks.RESULT.RESULT_SUCCESS || ret == Vieworks.RESULT.RESULT_ERROR_ABORTED_ALREADY)
            {
                System.Diagnostics.Debug.WriteLine("CameraAbort Successed.");
            }
            isGrabbing = false;
        }


        /// <summary>
        /// 连续采集模式
        /// </summary>
        /// <returns></returns>
        public bool StartContinuousGrabbing()
        {
            Vieworks.VwGigE.CameraSetTriggerMode(m_pCamera, false);
            if (Vieworks.VwGigE.CameraGrab(m_pCamera) != Vieworks.RESULT.RESULT_SUCCESS)
            {
                MessageBox.Show("Start Continuous Grabbing Failed.");
                return isGrabbing = false;
            }
            return isGrabbing = true;
        }

        /// <summary>
        /// 外部触发模式
        /// </summary>
        /// <returns></returns>
        public bool StartExternalTriggerGrabbing()
        {
            //StopGrab();
            //Vieworks.RESULT ret = Vieworks.VwGigE.CameraAbort(m_pCamera);
            //设置触发模式相关
            Vieworks.VwGigE.CameraSetTriggerSource(m_pCamera, TRIGGER_SOURCE.TRIGGER_SOURCE_EXT);
            Vieworks.VwGigE.CameraSetTriggerActivation(m_pCamera, TRIGGER_ACTIVATION.TRIGGER_ACTIVATION_RISINGEDGE);
            Vieworks.VwGigE.CameraSetTriggerMode(m_pCamera, true);
            //设置相机进入抓取模式
            if (Vieworks.VwGigE.CameraGrab(m_pCamera) != Vieworks.RESULT.RESULT_SUCCESS)
            {
                MessageBox.Show("Start External Trigger Grabbing Failed.");
                return isGrabbing = false;
            }
            return isGrabbing = true;
        }

        /// <summary>
        /// 设置参数为默认
        /// </summary>
        public void SaveToDefault()
        {
            string cfgfile = GlobalParas.cfgFile;
            if (!File.Exists(cfgfile))
            {
                MessageBox.Show("未找到配置文件，无法设为默认!~");
                return;
            }
            SaveToFile(cfgfile);
        }

        /// <summary>
        /// 保存到指定文件
        /// </summary>
        /// <param name="filename"></param>
        public void SaveToFile(string filename)
        {
            XMLConfig.WriteConfigData(filename, "curGain", curGain.ToString());
            XMLConfig.WriteConfigData(filename, "frameRate", frameRate.ToString());
        }

        /// <summary>
        /// 获取图像数据[一维数组]
        /// </summary>
        /// <param name="pImage"></param>
        /// <returns></returns>
        public Byte[] GetImageData(IntPtr pImage)
        {
            byte[] array;
            if (this.pixel_format == PIXEL_FORMAT.PIXEL_FORMAT_MONO8)
            {
                array = new byte[this.Height * this.Width];
                Marshal.Copy(pImage, array, 0, this.Height * this.Width);
            }
            else
            {
                array = new byte[Height * Width * 3];
                Vieworks.VwGigE.ConvertMono12ToBGR8(pImage, this.Height * this.Width * 2, array);
            }
            return array;
        }
    }
}
