using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CFD_Define
{
    /// <summary>
    /// 全局参数
    /// </summary>
    public static class GlobalParas
    {
        public static string cfgFile = Path.Combine(Application.StartupPath, "default.cfg");
        public static string initFile = Path.Combine(Application.StartupPath, "init.cfg");

        public static int Width = 640;
        public static int Height = 480;
    }
}
