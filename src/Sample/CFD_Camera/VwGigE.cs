using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;



//namespace Vieworks_Image_Capture.NET_Class_Library_Test // VWFX
namespace Vieworks
{
    using VWGIGE_HANDLE = System.IntPtr;
    using HINTERFACE = System.IntPtr;
    using HCAMERA = System.IntPtr;

        public enum PIXEL_RES					// Pixel Resolution
        {
            PIXEL_RES_MONO_1BIT,
            PIXEL_RES_MONO_2BIT,
            PIXEL_RES_MONO_4BIT,
            PIXEL_RES_MONO_8BIT,
            PIXEL_RES_MONO_10BIT,
            PIXEL_RES_MONO_12BIT,
            PIXEL_RES_MONO_14BIT,
            PIXEL_RES_MONO_16BIT
        };

        public enum MODE
        {
            MODE_SYNC,
            MODE_ASYNC
        };
        public enum EVENT
        {
            EVENT_CAP_START,			// Capture Start Event, Snap Unit
            EVENT_CAP_END				// Capture End Event, Snap Unit
        };
        public enum BUFFER
        {
            BUFFER_INTERNAL,
            BUFFER_EXTERNAL
        };

    /*
        [StructLayout(LayoutKind.Sequential)]
        public class CallbackFnParam			// Parameter for callback function
        {
            public IntPtr pParent;
            public IntPtr pCamera;
            public CallbackFnParam()
            {
                pParent = IntPtr.Zero;
                pCamera = IntPtr.Zero;
            }
        }
    */


    [StructLayout(LayoutKind.Sequential)]
    public class CallbackFnParam			// Parameter for callback function
    {
        public IntPtr pObjectInfo;
        public IntPtr pImageInfo;
        public CallbackFnParam()
        {
            pObjectInfo = IntPtr.Zero;
            pImageInfo = IntPtr.Zero;
        }
    }


    enum GET_CUSTOM_COMMAND
    {
        GET_CUSTOM_COMMAND_VALUE = 0xF0,	// Value
        GET_CUSTOM_COMMAND_NUM,				// Entry Num
        GET_CUSTOM_COMMAND_MIN,				// Minimum
        GET_CUSTOM_COMMAND_MAX,				// Maximum
        GET_CUSTOM_COMMAND_INC,				// Increment
        GET_CUSTOM_COMMAND_INDEX
    };

    public enum RESULT
    {
        RESULT_SUCCESS,
        RESULT_ERROR,
        RESULT_ERROR_OPENED_ALREADY,
        RESULT_ERROR_INVALID_HANDLE,
        RESULT_ERROR_TL_HANDLE,
        RESULT_ERROR_TLOPEN,
        RESULT_ERROR_IF_HANDLE,
        RESULT_ERROR_VWGIGE_INITIALIZATION,		// VWGIGE Module was not initialized
        RESULT_ERROR_INVALID_PARAMETER,				// Parameter is invalid
        RESULT_ERROR_DISCOVERY,
        RESULT_ERROR_NO_CAMERAS,				// There are no cameras
        RESULT_ERROR_CAMERA_NAME_DOES_NOT_EXIST,
        RESULT_ERROR_ABORTED_ALREADY,

        RESULT_ERROR_XML_UNKNOWN_COMMAND,
        RESULT_ERROR_XML_UNKNOWN_ARGUMENT,
        RESULT_ERROR_XML_NODE_ACCESS_FAILED,
        RESULT_ERROR_XML_INVALID_COMMAND,

        RESULT_ERROR_INVALID_WIDTH,

        RESULT_ERROR_VWINTERFACE_GETINTERFACENAME,
        RESULT_ERROR_VWINTERFACE_OPENINTERFACE,
        RESULT_ERROR_VWINTERFACE_CLOSEINTERFACE,
        RESULT_ERROR_VWINTERFACE_GETNUMDEVICES,

        RESULT_ERROR_VWCAMERA_INTERFACE,
        RESULT_ERROR_VWCAMERA_INTERFACE_HANDLE,
        RESULT_ERROR_VWCAMERA_CAMERAINDEX_OVER,
        RESULT_ERROR_VWCAMERA_GETXML,
        RESULT_ERROR_VWCAMERA_IMAGE_NOT4DIVIDE,
        RESULT_ERROR_VWCAMERA_IMAGE_NOT2DIVIDE,
        RESULT_ERROR_VWCAMERA_READ_ONLY,
        RESULT_ERROR_VWCAMERA_EVENTCONTROL_DOESNOT_INIT,

        RESULT_ERROR_DEVCREATEDATASTREAM,

        RESULT_ERROR_DATASTREAM_MTU,			// Datastream MTU error

        RESULT_ERROR_TLGETNUMINTERFACES,
        RESULT_ERROR_TLOPENINTERFACE,
        RESULT_ERROR_TLCLOSEINTERFACE,
        RESULT_ERROR_TLGETINTERFACENAME,
        RESULT_ERROR_TLGETNUMDEVICES,
        RESULT_ERROR_TLGETDEVICENAME,
        RESULT_ERROR_TLOPENDEVICE,

        RESULT_ERROR_BUFFER_TOO_SMALL,			// Buffer size is too small.
        RESULT_ERROR_MEMORY_ALLOCATION,

        RESULT_ERROR_FILE_STREAM_OPEN_FAILURE,
        RESULT_ERROR_FILE_STREAM_READ_FAILURE,
        RESULT_ERROR_FILE_STREAM_WRITE_FAILURE,
        RESULT_ERROR_FILE_STREAM_CLOSE_FAILURE,
        RESULT_ERROR_FILE_STREAM_NOT_CORRECT_FILE_LENGTH,


        RESULT_LAST                             // Don't Use it.
    };


    public enum PIXEL_FORMAT
    {
        PIXEL_FORMAT_MONO8 = 0x01080001,
        PIXEL_FORMAT_MONO8_SIGNED = 0x01080002,
        PIXEL_FORMAT_MONO10 = 0x01100003,
        PIXEL_FORMAT_MONO10_PACKED = 0x010C0004,
        PIXEL_FORMAT_MONO12 = 0x01100005,
        PIXEL_FORMAT_MONO12_PACKED = 0x010C0006,
        PIXEL_FORMAT_MONO16 = 0x01100007,
        PIXEL_FORMAT_BAYGR8 = 0x01080008,
        PIXEL_FORMAT_BAYRG8 = 0x01080009,
        PIXEL_FORMAT_BAYGB8 = 0x0108000A,
        PIXEL_FORMAT_BAYBG8 = 0x0108000B,
        PIXEL_FORMAT_BAYGR10 = 0x0110000C,
        PIXEL_FORMAT_BAYRG10 = 0x0110000D,
        PIXEL_FORMAT_BAYGB10 = 0x0110000E,
        PIXEL_FORMAT_BAYBG10 = 0x0110000F,
        PIXEL_FORMAT_BAYGR10_PACKED = 0x010C0026,
        PIXEL_FORMAT_BAYGR12_PACKED = 0x010C002A,
        PIXEL_FORMAT_BAYGR12 = 0x01100010,
        PIXEL_FORMAT_BAYRG12 = 0x01100011,
        PIXEL_FORMAT_BAYGB12 = 0x01100012,
        PIXEL_FORMAT_BAYBG12 = 0x01100013,
        PIXEL_FORMAT_BAYRG10_PACKED = 0x010C0027,
        PIXEL_FORMAT_BAYRG12_PACKED = 0x010C002B,

        PIXEL_FORMAT_RGB8_PACKED = 0x02180014,
        PIXEL_FORMAT_BGR8_PACKED = 0x02180015,
        PIXEL_FORMAT_YUV422_UYVY = 0x0210001F,
        PIXEL_FORMAT_YUV422_YUYV = 0x02100032,
        PIXEL_FORMAT_YUV422_10_PACKED = unchecked((int)0x80180001),
        PIXEL_FORMAT_YUV422_12_PACKED = unchecked((int)0x80180002),
        PIXEL_FORMAT_YUV411 = 0x020C001E,
        PIXEL_FORMAT_YUV411_10_PACKED = unchecked((int)0x80120004),
        PIXEL_FORMAT_YUV411_12_PACKED = unchecked((int)0x80120005),
        PIXEL_FORMAT_BGR10V1_PACKED = 0x0220001C,
        PIXEL_FORMAT_BGR10V2_PACKED = 0x0220001D,
        PIXEL_FORMAT_RGB12_PACKED = 0x0230001A,
        PIXEL_FORMAT_BGR12_PACKED = 0x0230001B,
        PIXEL_FORMAT_YUV444 = 0x02180020,
        PIXEL_FORMAT_PAL_INTERLACED = 0x02100001,
        PIXEL_FORMAT_NTSC_INTERLACED = 0x02100002
    };

    public enum TESTIMAGE
    {
	    TESTIMAGE_OFF, 
	    TESTIMAGE_BLACK, 
	    TESTIMAGE_WHITE, 
	    TESTIMAGE_GREYHORIZONTALRAMP, 
	    TESTIMAGE_GREYVERTICALRAMP, 
	    TESTIMAGE_GREYHORIZONTALRAMPMOVING, 
	    TESTIMAGE_GREYVERTICALRAMPMOVING,
	    TESTIMAGE_GREYCROSSRAMP,
	    TESTIMAGE_GREYCROSSRAMPMOVING
    };

    public enum STROBE_POLARITY
    {
	    STROBE_POLARITY_ACTIVEHIGH, 
	    STROBE_POLARITY_ACTIVELOW
    };


    public enum BLACKLEVEL_SEL
    {
	    BLACKLEVEL_SEL_TAP1, 
	    BLACKLEVEL_SEL_TAP2, 
	    BLACKLEVEL_SEL_TAP3, 
	    BLACKLEVEL_SEL_TAP4
    };

    public enum EXPOSURE_MODE
    {
	    EXPOSURE_MODE_TIMED, 
	    EXPOSURE_MODE_TRIGGERWIDTH
    };

    public enum READOUT
    {
	    READOUT_NORMAL,
	    READOUT_AOI, 
	    READOUT_BINNING, 
	    READOUT_HORIZONTALSTART, 
	    READOUT_HORIZONTALEND, 
	    READOUT_VERTICALSTART, 
	    READOUT_VERTICALEND, 
	    READOUT_BINNINGFATOR
    };

    public enum GAIN_SEL
    {
	    GAIN_ANALOG_ALL, 
	    GAIN_ANALOG_TAP1, 
	    GAIN_ANALOG_TAB2, 
	    GAIN_ANALOG_TAB3, 
	    GAIN_ANALOG_TAB4
    };

    public enum TRIGGER_SOURCE
    {
	    TRIGGER_SOURCE_SW, 
	    TRIGGER_SOURCE_EXT
    };

    public enum TRIGGER_ACTIVATION
    {
	    TRIGGER_ACTIVATION_RISINGEDGE,
	    TRIGGER_ACTIVATION_FALLINGEDGE
    };

    public enum GAIN_COLOR
    {
	    GAIN_COLOR_RED,
	    GAIN_COLOR_GREEN,
	    GAIN_COLOR_BLUE
    };

    public enum STREAM_INFO
    {
	    STREAM_INFO_NUM_OF_FRAMES_LOST,
	    STREAM_INFO_NUM_PACKETS_MISSING

    };

    public struct OBJECT_INFO
    {
        public IntPtr pUserPointer;
        public IntPtr pVwCamera;
    };


    public unsafe struct IMAGE_INFO
    {
	    public RESULT			callbackResult;
	    public int	            bufferIndex;
	    public PIXEL_FORMAT	    pixelFormat;
	    public int	            width;
	    public int	            height;
        public System.UInt64    unTimeStamp;
        public int		        ImageStatus;
	    public IntPtr			pImage;
    };
    public struct INTERFACE_INFO_STRUCT
    {
	    public bool				error;
	    public RESULT				errorCause;
	    public int		        index;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
	    public char[]				name;
    };

    public struct CAMERA_INFO_STRUCT
    {
	    public bool				error;
        public RESULT           errorResult;
        public int              index;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string           name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string vendor;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string model;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string ip;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string mac;

    };

    public enum PROPERTY_TYPE
    {
        UNKNOWN         =   0,
        ATTR_UINT       =   1,
        ATTR_FLOAT      =   2,
        ATTR_ENUM       =   3,
        ATTR_BOOLEAN    =   4,
        ATTR_STRING     =   5,
        ATTR_COMMAND    =   6,
        ATTR_CATEGORY   =   7
    }

    public enum PROPERTY_ACCESS_MODE
    {
        NOT_IMPLEMENT   =   0,
        NOT_AVAILABLE   =   1,
        WRITE_ONLY      =   2,
        READ_ONLY       =   3,
        READ_WRITE      =   4
    }

    public enum PROPERTY_VISIBILITY
    {
        BEGINNER    =   0,
        EXPERT      =   1,
        GURU        =   2,
        INVISIBLE   =   3,
        UNDEFINE    =   4
    }
    
    public struct PROPERTY
    {
        PROPERTY_TYPE           ePropType;
        PROPERTY_ACCESS_MODE eAccessMode;
        PROPERTY_VISIBILITY eVisibility;
        uint unPollingTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public char[]       caDisplay;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public char[]       caDescription;
    };

    public class VwGigE   // GIGE
    {
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT OpenVwGigE(ref VWGIGE_HANDLE hVwGigE);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CloseVwGigE( VWGIGE_HANDLE hVwGigE );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwGetNumInterfaces( VWGIGE_HANDLE hVwGigE, IntPtr pNumInterfaces );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwGetNumCameras( VWGIGE_HANDLE hVwGigE, ref int aPNumCamera );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwOpenCameraByIndex(VWGIGE_HANDLE hVwGigE, int nCameraIndex, ref HCAMERA phCamera, int nNumBuffer, int nWidth, int nHeight,
                                                         int nPacketSize, IntPtr pUserPointer, IntPtr pImageCallbackFn, IntPtr pDisconnectCallbackFn);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ImageCallbackFn( IntPtr pObjectInfo, ref IMAGE_INFO pImageInfo);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwOpenCameraByName(VWGIGE_HANDLE hVwGigE, IntPtr pCameraName, ref HCAMERA phCamera, int nNumBuffer, int nWidth,
                                                        int nHeight, int nPacketSize, IntPtr pUserPointer, IntPtr pImageCallbackFn, IntPtr pDisconnectCallbackFn);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwOpenInterfaceByIndex(VWGIGE_HANDLE hVwGigE, int aNIndex, ref HINTERFACE phInterface);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwOpenInterfaceByName( VWGIGE_HANDLE hVwGigE, IntPtr pInterfaceName, ref HINTERFACE phInterface );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwDiscovery( VWGIGE_HANDLE hVwGigE );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwDiscoveryInterfaceInfo(VWGIGE_HANDLE hVwGigE, int nIndex, ref INTERFACE_INFO_STRUCT pInterfaceInfoStruct);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwDiscoveryCameraInfo(VWGIGE_HANDLE hVwGigE, int nIndex, ref CAMERA_INFO_STRUCT pCameraInfoStruct);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwForceIP( VWGIGE_HANDLE hVwGigE, IntPtr pMAC, int nIP, int nSubnet, int nGateway );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwUseMTUOptimize(VWGIGE_HANDLE hVwGigE, bool bUse);

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwSetMTUTimeout(VWGIGE_HANDLE hVwGigE, uint uTimeout);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwGetMTUTimeout(VWGIGE_HANDLE hVwGigE, ref uint uTimeout);

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwSetMultiCastAddress(VWGIGE_HANDLE hVwGigE, ulong dwMultiCastAddress);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT VwGetMultiCastAddress(VWGIGE_HANDLE hVwGigE, ref ulong dwMultiCastAddress);

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT InterfaceOpenCameraByIndex( HINTERFACE hInstance, IntPtr pCallbackParent, int nDevIndex, IntPtr phCamera, int nNumBuffer,
                                                                int nWidth, int nHeight, int nPacketSize, IntPtr pImageCallbackFn, IntPtr pDisconnectCallbackFn);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT InterfaceOpenCameraByName( HINTERFACE hInterface, IntPtr pParent, IntPtr pName, IntPtr phCamera, int nNumBuffer,
                                                               int nWidth, int nHeight, int nPacketSize, IntPtr pImageCallbackFn, IntPtr pDisconnectCallbackFn);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT InterfaceGetNumCameras( HINTERFACE hInterface, IntPtr aPNumDevices );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT InterfaceGetCameraName( HINTERFACE hInterface, int aNDevIndex, IntPtr aName, IntPtr aPNameSize );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT InterfaceGetIP( HINTERFACE hInterface, IntPtr pInterfaceName, IntPtr pIP, IntPtr aIP );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT InterfaceGetSubnet(HINTERFACE hInterface, IntPtr pInterfaceName, IntPtr pSubnet, IntPtr aSubnet);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT InterfaceCloseInterface(HINTERFACE hInterface);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT InterfaceGetVwGigEHandle( HINTERFACE hInterface, IntPtr phVwGigEHandle);

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraClose(HCAMERA hCamera);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGrab(HCAMERA hCamera);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSnap(HCAMERA hCamera, int aNFrame);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraAbort(HCAMERA hCamera);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetGrabCondition(HCAMERA hCamera, ref bool bIsGrabbing);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetWidth(HCAMERA hCamera, ref int aPWidth);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetHeight(HCAMERA hCamera, ref int aPHeight);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetWidth(HCAMERA hCamera, int aNWidth);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetHeight(HCAMERA hCamera, int aNHeight);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetPixelSize(HCAMERA hCamera, int nPixelSize);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetPixelSize(HCAMERA hCamera, ref int nPixelSize);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetPixelFormat(HCAMERA hCamera, PIXEL_FORMAT pixelFormat);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetPixelFormat(HCAMERA hCamera, ref PIXEL_FORMAT pPixelFormat);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetPixelFormatLineup(HCAMERA hCamera, int nIndex, ref PIXEL_FORMAT pPixelFormat);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetPixelFormatLineupNum(HCAMERA hCamera, ref int npNum);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetPixelSizeLineup(HCAMERA hCamera, int nIndex, ref int nPixelSize);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetPixelSizeLineupNum(HCAMERA hCamera, ref int npNum);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetTestImage(HCAMERA hCamera, TESTIMAGE aTestImage);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetTestImage(HCAMERA hCamera, ref TESTIMAGE pTestImage);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetTestImageLineup(HCAMERA hCamera, int nIndex, ref TESTIMAGE pTestImage);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetTestImageLineupNum(HCAMERA hCamera, ref int npNum);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetAcquisitionTimeOut(HCAMERA hCamera, int nTimeOut);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetAcquisitionTimeOut(HCAMERA hCamera, ref int pnTimeOut);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetReadoutMode(HCAMERA hCamera, READOUT aReadout);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetHorizontalStart(HCAMERA hCamera, int uStart);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetHorizontalStart(HCAMERA hCamera, ref int uStart);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetHorizontalEnd(HCAMERA hCamera, int uEnd);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetHorizontalEnd(HCAMERA hCamera, ref int uEnd);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetVerticalStart(HCAMERA hCamera, int uStart);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetVerticalStart(HCAMERA hCamera, ref int uStart);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetVerticalEnd(HCAMERA hCamera, int uEnd);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetVerticalEnd(HCAMERA hCamera, ref int uEnd);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetDeviceModelName(HCAMERA hCamera, int uIndex, Byte[] pInfo, IntPtr pInfoSize);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetDeviceVersion(HCAMERA hCamera, int uIndex, Byte[] pInfo, IntPtr pInfoSize);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetDeviceVendorName(HCAMERA hCamera, int uIndex, Byte[] pInfo, IntPtr pInfoSize);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetDeviceManufacturerInfo(HCAMERA hCamera, Byte[] pInfo, IntPtr pInfoSize);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetDeviceID(HCAMERA hCamera, int uIndex, Byte[] pInfo, IntPtr pInfoSize);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetTriggerMode( HCAMERA hCamera, bool bSet);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetTriggerMode(HCAMERA hCamera, ref bool bSet);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetTriggerModeLineup(HCAMERA hCamera, int nIndex, ref int nTriggerMode);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetTriggerModeLineupNum(HCAMERA hCamera, ref int npNum);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetTriggerSource( HCAMERA hCamera, TRIGGER_SOURCE triggerSource);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetTriggerSource( HCAMERA hCamera, ref TRIGGER_SOURCE pTriggerSource);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetTriggerSourceLineup( HCAMERA hCamera, int nIndex, ref TRIGGER_SOURCE pTriggerSource);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetTriggerSourceLineupNum( HCAMERA hCamera, ref int npNum);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetTriggerActivation( HCAMERA hCamera, TRIGGER_ACTIVATION triggerActivation);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetExposureMode( HCAMERA hCamera, EXPOSURE_MODE aExpmode);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetExposureTime( HCAMERA hCamera, int aExptime_microsec);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetGain( HCAMERA hCamera, GAIN_SEL gainSel, int nGainValue);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetBlackLevel( HCAMERA hCamera, BLACKLEVEL_SEL blackLevelSel, int aBlacklevelVal);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetReverseX( HCAMERA hCamera, bool aBSet);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetStrobeOffset( HCAMERA hCamera, int nOffset);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetStrobePolarity( HCAMERA hCamera, STROBE_POLARITY aStrobePolarity);	
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetForceIP( HCAMERA hCamera, int dwIP, int dwSubnet, int dwGateway );	
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraChangeBufferFormat( HCAMERA hCamera, int nBufferNum, int nWidth, int nHeight, PIXEL_FORMAT pixelFormat );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetBufferInfo( HCAMERA hCamera, IntPtr nBufferNum, IntPtr nWidth, IntPtr nHeight, IntPtr pixelFormat );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetInterfaceHandle( HCAMERA hCamera, IntPtr phInterface);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGigESetCurrentIpConfigurationDHCP( HCAMERA hCamera, bool bSet );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGigESetCurrentIpConfigurationPersistentIP( HCAMERA hCamera, bool bSet );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGigEGetCurrentIpConfigurationDHCP( HCAMERA hCamera, IntPtr pbSet );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGigEGetCurrentIpConfigurationPersistentIP( HCAMERA hCamera, IntPtr pbSet );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGigEGetPersistentSubnetMask( HCAMERA hCamera, IntPtr pnSubnetMask );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGigEGetCurrentMACAddress( HCAMERA hCamera, IntPtr pNameSize, IntPtr pszMACAddress );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGigEGetColorRGBGain( HCAMERA hCamera, int nRGBType, ref double dpRGBGainValue );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGigESetColorRGBGain( HCAMERA hCamera, int nRGBType, double dRGBGainValue );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetStreamInfo( HCAMERA hCamera, STREAM_INFO streamInfo, IntPtr nInfo );

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGigEGetTemperature(HCAMERA hCamera, ref double dpTemperature);
        
        [DllImport("VwGigE.NET.V6.DLL")]
        //public static extern RESULT CameraSetCustomCommand( HCAMERA hCamera, Byte[] pCommand, Byte[] pArg );
        public static extern RESULT CameraSetCustomCommand(HCAMERA hCamera, string pCommand, string pArg);
        [DllImport("VwGigE.NET.V6.DLL")]
        //public static extern RESULT CameraGetCustomCommand( HCAMERA hCamera, Byte[] pCommand, Byte[] pArg, IntPtr pArgSize, int nCmdType );
        public static extern RESULT CameraGetCustomCommand(HCAMERA hCamera, string pCommand, string pArg, IntPtr pArgSize, int nCmdType);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraSetUARTCustomCommand( HCAMERA hCamera, Byte[] pCommand );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetUARTCustomCommand(HCAMERA hCamera, Byte[] pCommand, Byte[] pArg, ref int nArgSize);

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetPropertyCount( HCAMERA hCamera, IntPtr pCount );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetPropertyInfo( HCAMERA hCamera, Byte[] pCommand, ref PROPERTY ptPropInfo );
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern RESULT CameraGetPropertyInfoUsingIndex( HCAMERA hCamera, int nIndex, ref PROPERTY ptPropInfo );

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void Convert8a_4b_4a_8bTo16_16_MMX_I(IntPtr Src, Byte[] Dest, int LenSrcBytes);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertYUV422toBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertYUV422toBGR8Interlaced(IntPtr pbSrc, int cbSrc, Byte[] pbDst, bool bOdd, int width, bool blend, bool _signed);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertYUV422PackedtoBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertYUV411toBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertYUV411PackedtoBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertRGB12PackedtoBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertRGB8toBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertBGR10V2PackedtoBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertYUV422_UYVYtoBGR8(IntPtr pbSrc, int nWidth, int nHeight, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertYUV422_YUYVtoBGR8(IntPtr pbSrc, int nWidth, int nHeight, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertYUV444toBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertMono16PackedToBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertMonoPackedToBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertBAYGB8ToBGR8(IntPtr pbSrc, Byte[] pbDst, int width, int height);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertBAYRG8ToBGR8(IntPtr pbSrc, Byte[] pbDst, int width, int height);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertBAYGR8ToBGR8(IntPtr pbSrc, Byte[] pbDst, int width, int height);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertBAYGB10ToBGR8(IntPtr pbSrc, Byte[] pbDst, int width, int height);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertBAYRG10ToBGR8(IntPtr pbSrc, Byte[] pbDst, int width, int height);

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertMono10ToBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertMono12ToBGR8(IntPtr pbSrc, int cbSrc, Byte[] pbDst);

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertMono10PackedToMono16bit(IntPtr pbSrc, int width, int height, Byte[] pbDst);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertMono12PackedToMono16bit(IntPtr pbSrc, int width, int height, Byte[] pbDst);

        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertBAYGR10ToBGR8(IntPtr pbSrc, Byte[] pbDst, int width, int height);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertBAYGR12ToBGR8(IntPtr pbSrc, Byte[] pbDst, int width, int height);
        [DllImport("VwGigE.NET.V6.DLL")]
        public static extern void ConvertBAYRG12ToBGR8(IntPtr pbSrc, Byte[] pbDst, int width, int height);

    }
}