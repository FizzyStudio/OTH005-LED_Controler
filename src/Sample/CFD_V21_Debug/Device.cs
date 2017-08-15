using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace CFD_V21_Debug
{
    class Device
    {
        private string portName;
        private int baudRate;
        private SerialPort serialPort;
        private byte[] rBytes;

        public string PortName
        { get { return portName; } set { portName = value; } }
        public int BaudRate
        { get { return baudRate; } set { baudRate = value; } }
        public bool IsOpen
        { get { return serialPort == null ? false : serialPort.IsOpen; } }
        public Byte[] ReceivedBytes
        { get { return rBytes; } }


        public Device()
        {
            portName = "COM1";
            baudRate = 115200;
            //Connect();
        }

        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            try
            {
                if (serialPort != null)
                {
                    serialPort.Close();
                    serialPort.Dispose();
                }
                serialPort = new SerialPort(portName, baudRate);
                serialPort.StopBits = StopBits.One;         //1位停止位
                serialPort.Parity = Parity.None;            //无奇偶校验
                //serialPort.RtsEnable = true;              //设置RTS
                serialPort.ReceivedBytesThreshold = 1;    //接收缓冲区大小    
                serialPort.DataReceived += serialPort_DataReceived;
                serialPort.Open();
            }
            catch
            {
                MessageBox.Show("设备端口连接失败，请重新尝试！~");
            }
        }

        /// <summary>
        /// 断开
        /// </summary>
        public void DisConnect()
        {
            try
            {
                if (serialPort != null)
                {
                    serialPort.Close();
                    serialPort.Dispose();
                }
            }
            catch
            {
                MessageBox.Show("设备端口断开失败，请重新尝试！~");
            }
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmd"></param>
        public void WriteCommand(byte[] cmd)
        {
            serialPort.Write(cmd, 0, cmd.Length);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            rBytes = new byte[serialPort.BytesToRead];
            serialPort.Read(rBytes, 0, rBytes.Length);
        }

    }
}
