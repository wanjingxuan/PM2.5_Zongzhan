using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PM2._5_Project
{
	class GlobalParameter
	{
		/// <summary>
		/// 数据库读取数据专用
		/// </summary>
		public static string[]  readList_access= new string[100];
		/// <summary>
		/// 串口是否被占用
		/// </summary>
		public static bool serialIsUse = false;
        public static int receiceCount = 0;
        public static int receiceCountPM = 0;
        public static string airOpen = "02 34 41 53 54 4F 20 4B 30 20 35 30 03";
		public static string airClose = "";
		public static string dustOpen = "02 0F 00 00 00 04 01 02 FF 42";
        /// <summary>
        /// 读取PM数据
        /// </summary>
        public static string readLog = "02 34 41 53 54 4F 20 4B 30 20 35 30 03";
        /// <summary>
        /// 设定读取数据位置
        /// </summary>
       public static string readBack = " 02 34 53 53 54 4F 20 4B 30 20 2D 32 34 03";
        public static string dustClose = "";
        public static double eanvalue = 0;
        public static string readData = "01 03 00 00 00 08 44 0C";
		public static bool PortIsBusy = false;
		public static string sendNow = "";
		public static string testFileAddress = "";
		public static string CurrentExperimentName = "";
		public static BindingSource bindingSource1 = new BindingSource();
		public static double TimeCount = 0;
		public static double stableTime = 0;
		public static double testTime = 0;
		public static bool sendOrderSuccess = false;
		public static byte[] buff = new byte[256];
		public static int Y2_line1 = 0;
		public static int Y2_line2_airdust= 0;
		public static int X1_line3_airdust= 0;
		public static int X1_lien4_flowsmall= 0;
		public static int X1_lien6_flowsmall = 0;
		public static int X1_lien7_flowbig = 0;
		public static int X1_lien5_flowbig = 0;

		private static List<string> getPmData;

		public static List<string> GetPmData
		{
			get { return GlobalParameter.getPmData; }
			set { GlobalParameter.getPmData = value; }
		}
		private static DataTable _dt;

		public static DataTable Dt
		{
			get { return GlobalParameter._dt; }
			set { GlobalParameter._dt = value; }
		}
		
		/// <summary>
		///  声明一个连接数据库地址
		/// </summary>
		/// 
		


		
		public static string configAddress = "provider=microsoft.ace.oledb.12.0;;Data Source=" +System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Config.accdb";

        //存储pm读取数据命令
		private static List<string> _pmOrder = new List<string> { };

		public static List< string> PmOrder
        {
            get
            {
                return _pmOrder;
            }

            set
            {
                _pmOrder = value;
            }
        }
    }
}
