using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;


namespace PM2._5_Project
{
    public partial class Form1 : Form
    {

        ConnectAccess ConnectA = new ConnectAccess();
        System.Threading.Timer threadTimer;
        public Form1()
        {
            
            //初始化界面控件
            InitializeComponent();

            //初始化page切换页面
            tabControl1.SelectedIndex = 0;
            //初始化chart 内部数据
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "hh:mm:ss";
            //调整目标控件位置
            initControllocation();
            //初始化动画初始标记
            lineShape1.X1 = lineShape1.X1 - 23;
            lineShape1.X2 = lineShape1.X2 - 23;
            lineShape2.X1 = lineShape2.X1 - 62;
            lineShape2.X2 = lineShape2.X2 - 62;
            lineShape3.Y2 = lineShape3.Y2 -16;
            lineShape3.Y1 = lineShape3.Y1 - 16;
            lineShape3.X1 = lineShape3.X1 - 27;
            lineShape3.X2 = lineShape3.X2 - 27;


            lineShape6.Y2 = lineShape6.Y2 - 11;
            lineShape6.Y1 = lineShape6.Y1 - 11;
            lineShape6.X1 = lineShape6.X1-50;
            lineShape6.X2 = lineShape6.X2-50;

            lineShape4.Y2 = lineShape4.Y2 - 11;
            lineShape4.Y1 = lineShape4.Y1 - 11;
            lineShape4.X1 = lineShape4.X1-50 ;
            lineShape4.X2 = lineShape4.X2-50;


            lineShape5.Y2 = lineShape5.Y2 - 13;
            lineShape5.Y1 = lineShape5.Y1 - 13;
            lineShape5.X1 = lineShape5.X1 - 140;
            lineShape5.X2 = lineShape5.X2 - 140;


            lineShape7.Y2 = lineShape7.Y2 - 14;
            lineShape7.Y1 = lineShape7.Y1 - 14;
            lineShape7.X1 = lineShape7.X1 -95;
            lineShape7.X2 = lineShape7.X2 -95;


            GlobalParameter.Y2_line1 = lineShape1.Y1;
			GlobalParameter.Y2_line2_airdust = lineShape2.Y2;
			GlobalParameter.X1_line3_airdust = lineShape3.X1;
			GlobalParameter.X1_lien4_flowsmall = lineShape4.X1;
			GlobalParameter.X1_lien5_flowbig = lineShape5.X1;
			GlobalParameter.X1_lien6_flowsmall = lineShape6.X1;
			GlobalParameter.X1_lien7_flowbig = lineShape7.X1;
			lineShape1.BorderColor = Color.Transparent;
			lineShape2.BorderColor = Color.Transparent;
			lineShape3.BorderColor = Color.Transparent;
			
			lineShape5.BorderColor = Color.Transparent;
			lineShape7.BorderColor = Color.Transparent;
			lineShape4.BorderColor = Color.Transparent;
			lineShape6.BorderColor = Color.Transparent;
            this.chart2.BackImage = System.Environment.CurrentDirectory +"\\test1.jpg";
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
           
            chart2.ChartAreas[0].AxisX.Minimum = dt.ToOADate();
            chart2.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(dt.ToOADate()).AddMinutes(100).ToOADate();
            chart2.Series[7].Points.AddXY(dt,"100");


            //开启串口循环读取数据
            //开启循环读取环境数据
            initConfigFile();
            //获取主机电脑串口名称
            series_Port();
            //连接串口并开启读取数据计时器
            series_Open();

            //判断如果串口已经连接开启数据循环读取计时器
			//初始化datatable
            
			GlobalParameter.Dt = ConnectA.GetData("SELECT * FROM Data" + " order by time desc", "provider=microsoft.ace.oledb.12.0;Data Source="+System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Config.accdb");
            dust_radioButton3.Location = new Point(dust_radioButton3.Location.X + 150, dust_radioButton3.Location.Y+60);
            air_radioButton4.Location = new Point(air_radioButton4.Location.X + 156, air_radioButton4.Location.Y+58);
            T1_lable.Location = new Point(T1_lable.Location.X-10, T1_lable.Location.Y-45);
            T2_label.Location = new Point(T2_label.Location.X+7, T2_label.Location.Y+8);
            eanFlow_label.Location = new Point(eanFlow_label.Location.X -60, eanFlow_label.Location.Y - 8);
            airFlow_label.Location = new Point(airFlow_label.Location.X - 56, airFlow_label.Location.Y+8);
            P_label.Location = new Point(P_label.Location.X-10, P_label.Location.Y - 42);
            T1_blueLabel.Location = new Point(T1_blueLabel.Location.X-130, T1_blueLabel.Location.Y+6);
            T2_blueLabel.Location = new Point(T2_blueLabel.Location.X+120, T2_blueLabel.Location.Y+4);
            P_blueLabel.Location = new Point(P_blueLabel.Location.X+5, P_blueLabel.Location.Y-15);
            switch1.Location = new Point(switch1.Location.X - 220, switch1.Location.Y+38);
            air_setNumEdit.Location = new Point(air_setNumEdit.Location.X+23, air_setNumEdit.Location.Y+55);
            eanset_NumEdit.Location = new Point(eanset_NumEdit.Location.X + 23, eanset_NumEdit.Location.Y -50);
            setTesttime.Location = new Point(setTesttime.Location.X + 23, setTesttime.Location.Y +55);
            setStableTime.Location = new Point(setStableTime.Location.X + 23, setStableTime.Location.Y +68);
            panel15.Location = new Point(panel15.Location.X - 20, panel15.Location.Y);
            panel15.Width = panel15.Width + 10;
            // eanset_NumEdit,setTesttime,setStableTime

        }


        /// <summary>
        /// 初始化控件位置
        /// </summary>
        private void initControllocation()
        {

            dust_radioButton3.Location = new Point(panel15.Width / 100 * 8 - 20, panel15.Height / 100 * 5 - 10);
            air_radioButton4.Location = new Point(panel15.Width / 100 * 28 - 8, panel15.Height / 100 * 17 + 3);
            switch1.Location = new Point(panel15.Width / 100 * 126, panel15.Height / 100 * 51 + 5);
        }
        private void initConfigFile()
        {
           
            //读取配置文件中pm2.5的命令内容
            GlobalParameter.PmOrder.Add("02 34 41 52 45 47 20 4B 30 20 32 34 35 03");//RealPm
            GlobalParameter.PmOrder.Add("02 34 41 52 45 47 20 4B 30 20 32 34 32 03");//load
            GlobalParameter.PmOrder.Add("02 34 41 52 45 47 20 4B 30 20 32 35 37 03");//Fre
            GlobalParameter.PmOrder.Add("02 34 41 52 45 47 20 4B 30 20 35 38 03");//T
            GlobalParameter.PmOrder.Add("02 34 41 52 45 47 20 4B 30 20 36 33 03");//H
            GlobalParameter.PmOrder.Add("02 34 41 52 45 47 20 4B 30 20 36 36 03");//P
        }
  



        private void Form1_Load(object sender, EventArgs e)
        {
			// TODO: 这行代码将数据加载到表“configDataSet2.Data”中。您可以根据需要移动或删除它。
			this.dataTableAdapter1.Fill(this.configDataSet2.Data);
            



        }











        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            panel2.Location = new Point(panel3.Location.X + 10, 0);
            panel2.Width = panel3.Width / 2 - 25;
            panel2.Height = panel3.Height / 2 + 68;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {



            SolidBrush black = new SolidBrush(Color.Silver);
            SolidBrush White = new SolidBrush(Color.White);
            SolidBrush Gray = new SolidBrush(Color.Gray);
            e.Graphics.FillRectangle(Gray, 0, 0, 3, panel4.Height);//左内框
            e.Graphics.FillRectangle(black, 0, 0, 2, panel4.Height);//左内框
            e.Graphics.FillRectangle(White, 0, 0, 1, panel4.Height);//左内框

            e.Graphics.FillRectangle(Gray, 0, panel4.Height / 2, panel4.Width, 3);//左内框
            e.Graphics.FillRectangle(black, 0, panel4.Height / 2, panel4.Width, 2);//左内框
            e.Graphics.FillRectangle(White, 0, panel4.Height / 2, panel4.Width, 1);//左内框


            //分割线 左1 
            e.Graphics.FillRectangle(Gray, 50, 0, 3, panel4.Height);//左内框
            e.Graphics.FillRectangle(black, 50, 0, 2, panel4.Height);//左内框
            e.Graphics.FillRectangle(White, 50, 0, 1, panel4.Height);//左内框

            //分割线 左2 
            e.Graphics.FillRectangle(Gray, 100, 0, 3, panel4.Height);//左内框
            e.Graphics.FillRectangle(black, 100, 0, 2, panel4.Height);//左内框
            e.Graphics.FillRectangle(White, 100, 0, 1, panel4.Height);//左内框


            e.Graphics.FillRectangle(Gray, 2, 0, panel4.Width, 3);//上内框
            e.Graphics.FillRectangle(black, 1, 0, panel4.Width - 1, 2);//上内框
            e.Graphics.FillRectangle(White, 0, 0, panel4.Width - 1, 1);//上内框

            e.Graphics.FillRectangle(White, panel4.Width - 3, 0, 3, panel4.Height);//左内框
            e.Graphics.FillRectangle(black, panel4.Width - 2, 0, 2, panel4.Height);//左内框
            e.Graphics.FillRectangle(Gray, panel4.Width - 1, 0, 1, panel4.Height);//左内框

            e.Graphics.FillRectangle(White, 2, panel4.Height - 3, panel4.Width - 4, 3);//上内框
            e.Graphics.FillRectangle(black, 1, panel4.Height - 2, panel4.Width - 2, 2);//上内框
            e.Graphics.FillRectangle(Gray, 0, panel4.Height - 1, panel4.Width - 0, 1);


        }

     


        //获取串口并设置
        private void series_Port()
        {
            // 获取类型是ComboBox的控件并获取电脑上所有串口名称
            #region
            List<object> list = new List<object>();
            string[] SerialNameCollection = System.IO.Ports.SerialPort.GetPortNames();

            list.Add(comboBox1);
            list.Add(comboBox5);

            #endregion
            //清除Combox 类型控件内容

            #region
            for (int i = 0; i < list.Count; i++)
            {

                ComboBox cb = list[i] as ComboBox;
                cb.Items.Clear();
                if (SerialNameCollection.Length > 0)
                {
                    cb.Text = SerialNameCollection[0];
                    for (int j = 1; j < SerialNameCollection.Length; j++)
                    {

                        cb.Items.Add(SerialNameCollection[j]);
                    }
                }
            }

            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 1;
            #endregion




        }


      
        //读取设定端口
        private bool series_Open()
        {



            try
            {
               
                if (_Port.IsOpen && _PortPM.IsOpen)
                {

                    _Port.Close();
                    _PortPM.Close();
                    _Port.PortName = comboBox2.Text;
                    _PortPM.PortName = comboBox3.Text;
                    _Port.Open();
                    _PortPM.Open();

                }
                else {


                    _Port.Open();
                    _PortPM.Open();

                }

              


                GlobalParameter.receiceCount = 10;
                GlobalParameter.receiceCountPM = 10;


                if (GlobalParameter.receiceCount > 1 || GlobalParameter.receiceCountPM > 1)
                {

                    // 执行轮训命令
                    threadTimer = new System.Threading.Timer(new TimerCallback(TimerUp), null, Timeout.Infinite, 500);
                    threadTimer.Change(0, 1000);
                    return true;

                }
                else
                {


                    MessageBox.Show("设备未连接,请检查设备");
                }




            }
            catch (Exception ex)
            {

                _Port.Close();
                _PortPM.Close();
                Console.WriteLine(ex);
				MessageBox.Show("设备未连接,请检查设备");
                //write_Txt(ex.ToString());

            }



            return false;
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="sendOrder"></param>
        /// 

        private void send_Msg(string sendOrder)
        {
            GlobalParameter.serialIsUse = true;
            Thread.Sleep(1000);
            byte[] buff = new Byte[_Port.ReadBufferSize];
            //将字符串转换成byte
            try
            {
                if (_Port.IsOpen)
                {
                    GlobalParameter.sendNow = sendOrder;
                    _Port.Write(ConnectA.ToByte(sendOrder), 0, ConnectA.ToByte(sendOrder).Length);
                    //	write_Txt("发送"+GlobalParameter.sendNow);
                    Thread.Sleep(50);
                    if (sendOrder == GlobalParameter.readLog)
                    {
                        _Port_DataReceivedLog();

                    }
                    else {

                        _Port_DataReceived();
                    }
                  
                }
                else
                {

                    _Port.Open();



                }

                //处理数据
            }
            catch (Exception ex)
            {
                ////关闭测试时间
                //threadTimer.Dispose();
                //Thread.Sleep(500);
                //testTimer1.Enabled = false;
                //stableTime.Enabled = false;
                //GlobalParameter.TimeCount = 0;
                //ledArray1[0].Value = false;
                //ledArray1[1].Value = false;

                //MessageBox.Show("串口通讯失败");
                ////	write_Txt(ex.ToString(
                _Port.Close();
                _PortPM.Close();

              //threadTimer.Dispose();
                Thread.Sleep(1500);
                try
                {


                    if (!_PortPM.IsOpen)
                    {
                        _PortPM.Open();
                    }
                    if (!_Port.IsOpen)
                    {
                        _Port.Open();
                    }
                }
                catch { }
               
               
               
                threadTimer = new System.Threading.Timer(new TimerCallback(TimerUp), null, Timeout.Infinite, 450);
                threadTimer.Change(0, 500);

            }
            GlobalParameter.serialIsUse = false;


        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="sendOrder"></param>
        /// 

        private void send_MsgPM(string sendOrder)
        {
			GlobalParameter.GetPmData = new List<string> { };
            byte[] buff = new Byte[_Port.ReadBufferSize];
            //将字符串转换成byte
            try
            {
                if (_PortPM.IsOpen)
                {
                   
					for (int i = 0; i < GlobalParameter.PmOrder.Count; i++)
					{
						
						_PortPM.Write(ConnectA.ToByte(GlobalParameter.PmOrder[i]), 0, ConnectA.ToByte(GlobalParameter.PmOrder[i]).Length);
                        //	write_Txt("发送"+GlobalParameter.sendNow);
                        Thread.Sleep(50);
                        _PortPM_DataReceived();
					}
					if (GlobalParameter.GetPmData.Count == 6) {

						readParPM.SetData(GlobalParameter.GetPmData.ToArray());
					
					}
                }
                else
                {

					_PortPM.Open();



                }

                //处理数据
            }
            catch (Exception ex)
            {

                //	MessageBox.Show("串口通讯失败");
                //	write_Txt(ex.ToString());

            }

          
        }


        private void send_MsgPMP(string sendOrder)
        {
            GlobalParameter.GetPmData = new List<string> { };
            byte[] buff = new Byte[_Port.ReadBufferSize];
            //将字符串转换成byte
            try
            {//
                if (_PortPM.IsOpen)
                {
                    _PortPM.Write(ConnectA.ToByte("02 34 41 53 54 4F 20 4B 30 20 35 30 03"), 0, ConnectA.ToByte("02 34 41 53 54 4F 20 4B 30 20 35 30 03").Length);
                    //	write_Txt("发送"+GlobalParameter.sendNow);
                     Thread.Sleep(1000);
                    if (sendOrder == "02 34 53 53 54 4F 20 4B 30 20 2D 32 34 03") {
                        _PortPMP_DataReceived();
                    }
                  
                   
                }
                
                //处理数据
            }
            catch (Exception ex)
            {

                

            }


        }


        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="retruenOrder">返回数据</param>01 03 00 00 00 02 aa cc 
        /// <param name="sendOrder">发送数据</param> 01 03 02 00 00 00 00 a0 ff;



        //窗体加载后处理COM口读取工作
        private void Form1_Shown(object sender, EventArgs e)
        {
            //提起串口电脑中串口
            series_Port();
        }

        //调整和显示 panel8 位置和格式效果

        
        private void button3_Click(object sender, EventArgs e)
        {

            if (series_Open() && button3.Text == "开始")
            {

                button3.Text = "关闭";


                if (radioButton1.Checked)
                {


                    send_Msg(GlobalParameter.airOpen);
                    //更新参数参数状态


                }
                if (radioButton2.Checked)
                {

                    send_Msg(GlobalParameter.dustOpen);
                    //更新参数参数状态

                }

                //将设置流量转换成命令，  //发送设置流量,16进制，01 10 00 00 00 04 08 27 10 27 10 27 10 27 10 32 1C
                // string ean581F = "0000" + (int.Parse(((double.Parse(textBox1.Text) / 100 * 4 + 1) * 1000).ToString())).ToString("X");
                // ean581F = ean581F.Substring(ean581F.Length - 4, 4).Insert(2, " ");
                GlobalParameter.eanvalue = double.Parse(textBox1.Text);
                string  diluteF = "0000" + (int.Parse(((double.Parse(textBox2.Text) / 20 * 4 + 1) * 1000).ToString())).ToString("X");
                diluteF = diluteF.Substring(diluteF.Length - 4, 4).Insert(2, " ");
                //发送质量流量控制器的执行命令 发送命令格式必须要在中间添加空格
                string sendOrder = "03 10 00 00 00 02 04 " + "00 00" + " " + diluteF + " " + CRC.ToModbusCRC16("03100000000204" + "00 00" + diluteF).Insert(2, " ");


                send_Msg(sendOrder);

                if (checkOrder())
                {
                 

                    // 执行轮训命令
                    threadTimer = new System.Threading.Timer(new TimerCallback(TimerUp), null, Timeout.Infinite, 450);
                    threadTimer.Change(0, 500);



                    Thread thread0 = new Thread(new ParameterizedThreadStart(UpdateStats));
                    thread0.Start("");

                  

                }
                else
                {

                    MessageBox.Show("请尝试再次执行开始实验");


                }

            }
            else
            {
                if (button3.Text != "开始")
                {

                    testTimer1.Stop();
                    threadTimer.Dispose();
                    Thread.Sleep(500);
                    button3.Text = "开始";
                    send_Msg("03 10 00 00 00 02 04 00 00 00 00 F8 17");

                    send_Msg(GlobalParameter.dustClose);

                    ean581F.Text = readPar.Ean581F;
                    DiluteAirF.Text = readPar.DiluteAirF;
                }
                else
                {

                  //  MessageBox.Show("串口连接失败----");

                }



            }

            // 先开始检测串口通讯是否成功


            //读取设置流程




        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

                radioButton2.Checked = false;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {

                radioButton1.Checked = false;

            }
        }



        private void _PortPM_DataReceived()
        {

            string value = "";
            byte[] buff = new byte[256];
            //获取数据并记录读取数据数量 
        
            GlobalParameter.receiceCountPM = _PortPM.Read(buff, 0, 256);
			 //根据数据数量新建数组
			string str = Encoding.UTF8.GetString(buff, 0, GlobalParameter.receiceCountPM);
          
             str=str.Split(' ').Last();
            string aa = str.Remove(str.Length - 3);
           
            //将接收数据赋值到新的数组
            GlobalParameter.GetPmData.Add(aa);
            //readPar.StandardPM = GlobalParameter.GetPmData[0];
        }

        private void _PortPMP_DataReceived()
        {
            string value = "";
            try
            {

              
                byte[] buff = new byte[1024];
                //获取数据并记录读取数据数量 
                while (true)
                {


                    GlobalParameter.receiceCountPM = _PortPM.Read(buff, 0, 1024);
                    //根据数据数量新建数组
                    string str = Encoding.UTF8.GetString(buff, 0, GlobalParameter.receiceCountPM);
                    value += str;

                }

             
               
              
            }
            catch {

                if (value.Length > 0) {

                    String rex = "(?<=\r\n).*?(?=\r\n)";
                    MatchCollection regResult = Regex.Matches(value, rex);
                    foreach (Match item in regResult)
                    {
                        CRC.write_Txt(item.Value.ToArray());
                      
                    }

                }

               

            }
        }

        private bool checkOrder()
        {
            int Count = 0;
        
            while (true)
            {
                if (GlobalParameter.sendOrderSuccess)
                {

                    return true;

                }
                else
                {
                    //write_Txt("检测------" + Count.ToString());
                    send_Msg(GlobalParameter.sendNow);
                    Thread.Sleep(1000);
                    Count++;


                }
                if (Count > 3)
                {


                    return false;
                }



            }



        }

        /// <summary>
        /// 多线程发送指令，防止卡顿
        /// </summary>
        /// <param name="value"></param>
        private void TimerUp(object value)
        {
            if (!GlobalParameter.serialIsUse)
            {

                //发送读取控制状态命令,模块数据和控制状态
                send_Msg(GlobalParameter.readData);
                //读取标砖pm2.5 设备模块
                send_MsgPM(GlobalParameter.readData);
            }


        }


        private void timer1_Tick(object sender, EventArgs e)
        {
             
            //判断是否进行测试时间
			if (GlobalParameter.testTime > 0)
			{

				//更新控件参数

				//Thread thread0 = new Thread(new ParameterizedThreadStart(UpdateStats));
				//thread0.Start("");

				//执行处理数据并存储线程
				string[] temData = new string[8];
				// 整理数据并存储到

				temData[0] = readPar.RecordTime.AddSeconds(GlobalParameter.TimeCount).ToString("H:mm:ss");
				temData[1] = readPar.MixT;
				temData[2] = readPar.AirT;
				temData[3] = readPar.P;
                temData[5] = readPar.DiluteAirF;
                temData[4] = readPar.Ean581F;
				temData[6] = readPar.StandardPM;
				temData[7] = readPar.DetectionPM;
				//执行处理数据并存储线程
				//Thread thread1 = new Thread(new ParameterizedThreadStart(UpdateDataBase));
				//thread1.Start(temData);


				//刷新DataGrim

				Thread thread2 = new Thread(new ParameterizedThreadStart(UpdataDataGridView1));
				thread2.Start(temData);

				//执行绘制曲线

				Thread thread3 = new Thread(new ParameterizedThreadStart(UpdataChart));
				thread3.Start(temData);



				// 刷新datagrieview
				// 刷新chart



				

			}
			else {


				//关闭测试时间
				testTimer1.Enabled = false;

				GlobalParameter.TimeCount = 0;

				switch1.Value = false;
				ledArray1[1].Value = false;
				//关闭质量流量计
				send_Msg("03 10 00 00 00 02 04 00 00 00 00 F8 17");
				//关闭静电切换阀，切换成空气
				//send_Msg(GlobalParameter.dustClose);
			
			}
			GlobalParameter.testTime--;
				
            GlobalParameter.TimeCount++;

        }
        private void UpdateStats(object str)
        {
            if (T1_lable.InvokeRequired)
            {
                Action<string> actionDelegate = (x) =>
                {
                    T1_lable.Text = readPar.AirT;
                    T2_label.Text = readPar.MixT;
                    P_label.Text = readPar.P;
                  

                    if (double.Parse(readPar.Ean581F) / double.Parse(air_setNumEdit.Text) > 1.05)
                    {

                        eanFlow_label.BackColor = Color.LightPink;


                    }
                    else
                    {

                        eanFlow_label.BackColor = Color.White;

                    }
                    if (double.Parse(readPar.DiluteAirF) / double.Parse(eanset_NumEdit.Text) > 1.05)
                    {

                        airFlow_label.BackColor = Color.LightPink;


                    }
                    else
                    {

                        airFlow_label.BackColor = Color.White;

                    }
                    eanFlow_label.Text = readPar.Ean581F;
                    airFlow_label.Text = readPar.DiluteAirF;

                };


                this.T1_lable.BeginInvoke(actionDelegate, str);


            }
        }

        private void dataCare(object value)
        {


        }

        private void UpdateDataBase(object str)
        {
            string[] mydata = str as string[];

           // ConnectA.SaveData(mydata, "Data", "add", GlobalParameter.testFileAddress);
			//给datatable 添加数据
			DataRow dr = GlobalParameter.Dt.NewRow();
			dr["Time"] = mydata[0];
			dr["MixT"] = mydata[1];
			dr["AirT"] = mydata[2];
			dr["PB"] = mydata[3];
			dr["Ean581F"] = mydata[4];
			dr["DiluteAirF"] = mydata[5];
			dr["StandardPm"] = mydata[6];
			dr["DetectionPm"] = mydata[7];
			GlobalParameter.Dt.Rows.Add(dr);
        }

        private void UpdataDataGridView1(object str)
        {

            if (gridControl1.InvokeRequired)
            {


                Action<string []> actionDelegate = (x) =>
                {
					DataRow dr = GlobalParameter.Dt.NewRow();
					dr["Time"] = x[0];
					dr["MixT"] = x[1];
					dr["AirT"] = x[2];
					dr["PB"] = x[3];
					dr["Ean581F"] = x[4];
					dr["DiluteAirF"] = x[5];
					
					dr["OneHrPM"] = readParPM.OneHour;
					dr["LoadFilterPM"] = readParPM.Loading;
					dr["FrequencyPM"] = readParPM.Frequency;
					dr["TemperaturePM"] = readParPM.Temperature;
					dr["HumidityPM"] = readParPM.Humidity;
					dr["PressurePM"] = readParPM.Pressure;

					GlobalParameter.Dt.Rows.Add(dr);
                   // GlobalParameter.bindingSource1.DataSource = ConnectA.GetData("SELECT * FROM " + str.ToString() + " order by time desc", GlobalParameter.testFileAddress);
					gridControl1.DataSource = GlobalParameter.Dt;
                    gridView1.MoveFirst();
                };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                this.gridControl1.BeginInvoke(actionDelegate, str);



            }
            else
            {

               // GlobalParameter.bindingSource1.DataSource = ConnectA.GetData("SELECT * FROM " + str.ToString() + " order by time desc", GlobalParameter.testFileAddress);
				gridControl1.DataSource = GlobalParameter.Dt;

            }
        }
        private void UpdataChart(object str)
        {
            string[] mydata1 = str as string[];
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "H:mm:ss";
            DateTime dt = Convert.ToDateTime(mydata1[0].ToString(), dtFormat);

            if (chart2.InvokeRequired)
            {
                //Thread.Sleep(3000);
                //// 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string[]> actionDelegate = (x) =>
                {
                    if (dt.Second % 5 == 0)
                    {
                        //添加数据
                        chart2.Series[0].Points.AddXY(dt, readPar.MixT);
                        chart2.Series[1].Points.AddXY(dt, readPar.AirT);
                        chart2.Series[2].Points.AddXY(dt, readPar.P);
                        chart2.Series[3].Points.AddXY(dt, readPar.DiluteAirF);
                        chart2.Series[4].Points.AddXY(dt, readPar.Ean581F);
                        chart2.Series[5].Points.AddXY(dt, readPar.StandardPM);
                        //chart2.Series[6].Points.AddXY(dt, readPar.DetectionPM);

                        // 计算取消数据时间，移除当前时间之前30分钟的数据
                        double removeBefore = dt.AddMinutes((double)(30) * (-1)).ToOADate();
                        while (chart2.Series[0].Points[0].XValue < removeBefore)
                        {
                            chart2.Series[0].Points.RemoveAt(0);
                            chart2.Series[1].Points.RemoveAt(0);
                            chart2.Series[2].Points.RemoveAt(0);
                            chart2.Series[3].Points.RemoveAt(0);
                            chart2.Series[4].Points.RemoveAt(0);
                            chart2.Series[5].Points.RemoveAt(0);
                           // chart2.Series[6].Points.RemoveAt(0);
                        }

                        chart2.ChartAreas[0].AxisX.Minimum = chart2.Series[0].Points[0].XValue;
                        chart2.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(chart2.Series[0].Points[0].XValue).AddMinutes(32).ToOADate();


                    }



                };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                this.chart2.BeginInvoke(actionDelegate, str);



            }
        }
        /// <summary>
        /// The AddNewPoint function is called for each series in the chart when
        /// new points need to be added.  The new point will be placed at specified
        /// X axis (Date/Time) position with a Y value in a range +/- 1 from the previous
        /// data point's Y value, and not smaller than zero.
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <param name="ptSeries"></param>
        public void AddNewPoint(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            // Add new data point to its series.
            ptSeries.Points.AddXY(timeStamp.ToOADate(), new Random().Next(10, 20));

            //// remove all points from the source series older than 20 seconds.
            //double removeBefore = timeStamp.AddSeconds((double)(20) * (-1)).ToOADate();

            ////remove oldest values to maintain a constant number of data points
            //while (ptSeries.Points[0].XValue < removeBefore)
            //{
            //    ptSeries.Points.RemoveAt(0);
            //}

            //chart1.ChartAreas[0].AxisX.Minimum = ptSeries.Points[0].XValue;
            //chart1.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(ptSeries.Points[0].XValue).AddSeconds(30).ToOADate();

            //chart1.Invalidate();
        }

       











        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double a = 0;

            if (!double.TryParse(textBox1.Text, out a))
            {

                textBox1.Text = "";

            }
            else
            {

                for (int i = 0; i < a.ToString().Length; i++)
                {

                    if (a.ToString().Substring(i, 1) == ".")
                    {

                        if (a.ToString().Length - i > 2)
                        {

                            textBox1.Text = "";
                        }

                    }

                }



            }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double a = 0;

            if (!double.TryParse(textBox2.Text, out a))
            {

                textBox2.Text = "";

            }
            else
            {

                for (int i = 0; i < a.ToString().Length; i++)
                {

                    if (a.ToString().Substring(i, 1) == ".")
                    {

                        if (a.ToString().Length - i > 2)
                        {

                            textBox2.Text = "";
                        }

                    }

                }



            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int a = 0;

            if (!int.TryParse(textBox3.Text, out a))
            {

                textBox3.Text = "";

            }


        }



        private void localTime_Tick(object sender, EventArgs e)
        {
			if (switch1.Value)
			{
				if (lineShape1.BorderColor == Color.Transparent || lineShape2.BorderColor == Color.Transparent || lineShape3.BorderColor == Color.Transparent)
				{


					lineShape1.BorderColor = Color.Red;
					lineShape2.BorderColor = Color.Red;
					lineShape3.BorderColor = Color.Red;
				}




				if (lineShape2.Y2 - lineShape2.Y1 < 100)
				{

					lineShape2.Y2 += 10;
				}
				else
				{

					lineShape2.Y2 = GlobalParameter.Y2_line2_airdust;
				}

				if (lineShape3.X2 - lineShape3.X1 < 100)
				{


					lineShape3.X1 -= 10;
				}
				else
				{

					lineShape3.X1 = GlobalParameter.X1_line3_airdust;
				}

				if (lineShape1.Y2 - lineShape1.Y1 < 100)
				{

					lineShape1.Y2 += 10;
				}
				else
				{

					lineShape1.Y2 = GlobalParameter.Y2_line1;
				}

				


					if (lineShape4.BorderColor == Color.Transparent || lineShape6.BorderColor == Color.Transparent)
					{
						lineShape4.BorderColor = Color.Red;
						lineShape6.BorderColor = Color.Red;
						//lineShape5.BorderColor = Color.Transparent;
						//lineShape7.BorderColor = Color.Transparent;
					}

					if (lineShape4.X2 - lineShape4.X1 < 100)
					{


						lineShape4.X1 -= 10;
					}
					else
					{

						lineShape4.X1 = GlobalParameter.X1_lien4_flowsmall;
					}

					if (lineShape6.X2 - lineShape6.X1 < 100)
					{


						lineShape6.X1 -= 10;
					}
					else
					{

						lineShape6.X1 = GlobalParameter.X1_lien6_flowsmall;
					}

				


					if (lineShape5.BorderColor == Color.Transparent || lineShape7.BorderColor == Color.Transparent)
					{
						lineShape5.BorderColor = Color.Red;
						lineShape7.BorderColor = Color.Red;
						//lineShape4.BorderColor = Color.Transparent;
						//lineShape6.BorderColor = Color.Transparent;
					}


					if (lineShape5.X2 - lineShape5.X1 < 100)
					{


						lineShape5.X1 -= 10;
					}
					else
					{

						lineShape5.X1 = GlobalParameter.X1_lien5_flowbig;
					}

					if (lineShape7.X2 - lineShape7.X1 < 100)
					{


						lineShape7.X1 -= 10;
					}
					else
					{

						lineShape7.X1 = GlobalParameter.X1_lien7_flowbig;
					}

				

			}
			else {
				lineShape1.BorderColor = Color.Transparent;
				lineShape2.BorderColor = Color.Transparent;
				lineShape3.BorderColor = Color.Transparent;
				lineShape4.BorderColor = Color.Transparent;
				lineShape6.BorderColor = Color.Transparent;
				lineShape5.BorderColor = Color.Transparent;
				lineShape7.BorderColor = Color.Transparent;
			
			
			}
				
			

			
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            send_Msg("03 10 00 00 00 02 04 00 00 00 00 F8 17");
            this.Close();
        }

      

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }




        private void readData_Tick(object sender, EventArgs e)
        {


            try
            {


                T1_blueLabel.Text = T1_lable.Text = readPar.AirT;
                T2_blueLabel.Text = T2_label.Text = readPar.MixT;
                P_blueLabel.Text = P_label.Text = readPar.P;


                if (double.Parse(readPar.Ean581F) / double.Parse(air_setNumEdit.Text) > 1.05)
                {

                    eanFlow_label.BackColor = Color.LightPink;


                }
                else
                {

                    eanFlow_label.BackColor = Color.White;

                }
                if (double.Parse(readPar.DiluteAirF) / double.Parse(eanset_NumEdit.Text) > 1.05)
                {

                    airFlow_label.BackColor = Color.LightPink;


                }
                else
                {

                    airFlow_label.BackColor = Color.White;

                }
                eanFlow_label.Text = readPar.Ean581F;
                airFlow_label.Text = readPar.DiluteAirF;
                //Thread thread0 = new Thread(new ParameterizedThreadStart(UpdateStats));
                //thread0.Start("");
            }
            catch { }
        }

        private void _Port_DataReceived()
        {

            string value = "";
            byte[] buff = new byte[256];
            Thread.Sleep(50);

            //获取数据并记录读取数据数量  
            GlobalParameter.receiceCount = _Port.Read(buff, 0, 256);


            //根据数据数量新建数组
            byte[] temByte = new byte[GlobalParameter.receiceCount];
            //将接收数据赋值到新的数组
            Array.Copy(buff, temByte, GlobalParameter.receiceCount);
            //将赋值数组转换成16进制字符串
            string temOrder = CRC.byteToHexStr(temByte);
            //write_Txt(GlobalParameter.receiceCount+"接收"+ temOrder);

            //将接收数据重新计算校验码和获取数据中的校验码对比如果正确数据接收完整
            if (CRC.ToModbusCRC16(temOrder.Substring(0, temOrder.Length - 4)) == temOrder.Substring(temOrder.Length - 4, 4))
            {
                GlobalParameter.sendOrderSuccess = true;
                //根据返回数据获取功能码
                value = temOrder.Substring(2, 2);


                //数据进行处理
                switch (value)
                {
                    //读取所有通道数据 4117 模块


                    case "03":
                        //截取数据
                        string temdata = temOrder.Substring(6, temOrder.Length - 10);

                        //根据返回数据计算出数据个数并新建出存储数据数组
                        string[] temIntdata = new string[temdata.Length / 4];
                        //将数据分组并换算成十进制
                        for (int i = 0; i < temdata.Length / 4; i++)
                        {


                            temIntdata[i] = CRC.GetHexadecimalValue(temdata.Substring(i * 4, 4));



                        }

                    
                        

                        readPar.SetData(temIntdata);
                        break;
                    //设置7060D模块通道开关量
                    case "0F"://01 0F 00 00 00 04 01 0F 7E 92 


                        //if (temOrder.Equals("020F00000004543B") && GlobalParameter.sendNow.Equals(GlobalParameter.airOpen))
                        //{

                        //    airShape.FillColor = Color.Green;
                        //    Console.Write("do it ");
                        //    readPar.AirValueColor = Color.Green;
                        //}
                        //if (temOrder.Equals("020F00000004543B") && GlobalParameter.sendNow.Equals(GlobalParameter.dustOpen))
                        //{
                        //    dustShape.FillColor = Color.Green;
                        //    readPar.DustValueColor = Color.Green;
                        //    Console.Write("do it ");
                        //}
                        //if (temOrder.Equals("020F00000004543B") && GlobalParameter.sendNow.Equals(GlobalParameter.airClose))
                        //{
                        //    Console.Write("do it ");
                        //    dustShape.FillColor = Color.Red;
                        //    airShape.FillColor = Color.Red;
                        //}


                        break;
                    //设置7024 模块通道电流
                    case "10"://01 10 00 00 00 04 08 27 10 27 10 27 10 27 10 32 1C





                        break;


                }
            }
            else
            {

                GlobalParameter.sendOrderSuccess = false;

            }

        }

        private void _Port_DataReceivedLog()
        {

            string value = "";
            byte[] buff = new byte[512];
            Thread.Sleep(2000);

            //获取数据并记录读取数据数量  
            GlobalParameter.receiceCount = _Port.Read(buff, 0, 512);
            string aa = Encoding.UTF8.GetString(buff, 0, GlobalParameter.receiceCount);

            Console.WriteLine(aa);
        }

        private void testOver() {

            //关闭测试时间
            testTimer1.Enabled = false;
            stableTime.Enabled = false;
            GlobalParameter.TimeCount = 0;
            ledArray1[0].Value = false;
            ledArray1[1].Value = false;
            //关闭测试时间
            threadTimer.Dispose();
            Thread.Sleep(500);
            //关闭质量流量计
            send_Msg("03 10 00 00 00 02 04 00 00 00 00 F8 17");
            //关闭静电切换阀，切换成空气
            send_Msg(GlobalParameter.dustClose);
            threadTimer = new System.Threading.Timer(new TimerCallback(TimerUp), null, Timeout.Infinite, 500);
            threadTimer.Change(0, 1000);
        }
        private void cartoon_Tick(object sender, EventArgs e)
        {
            //判断是否执行完稳定时间
            if (GlobalParameter.stableTime > 0)
            {

                GlobalParameter.stableTime--;
            }
            else
            {
                //进入实验测试状态
				ledArray1[0].Value = false;
				ledArray1[1].Value = true;
                testTimer1.Enabled = true;
                stableTime.Enabled = false;
			   
            }



        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {

                chart2.Series[0].Color = Color.Lime;

            }
            else
            {


                chart2.Series[0].Color = Color.Transparent;
            }

        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {

                chart2.Series[1].Color = Color.Green;

            }
            else
            {


                chart2.Series[1].Color = Color.Transparent;
            }

        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {

                chart2.Series[2].Color = Color.Cyan;

            }
            else
            {


                chart2.Series[2].Color = Color.Transparent;
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {

                chart2.Series[3].Color = Color.Orange;

            }
            else
            {


                chart2.Series[3].Color = Color.Transparent;
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {

                chart2.Series[4].Color = Color.Olive;

            }
            else
            {


                chart2.Series[4].Color = Color.Transparent;
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {

                chart2.Series[5].Color = Color.Fuchsia;

            }
            else
            {


                chart2.Series[5].Color = Color.Transparent;
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {

                chart2.Series[6].Color = Color.Purple;

            }
            else
            {


                chart2.Series[6].Color = Color.Transparent;
            }

        }


		private void air_radioButton4_Click(object sender, EventArgs e)
		{
			RadioButton aa = (RadioButton)sender;
			if (!switch1.Value)
			{
               
                if (aa.Name == "dust_radioButton3")
				{
                   

                    send_Msg(GlobalParameter.airOpen);
					dust_radioButton3.Checked = true;
					air_radioButton4.Checked = false;
                    panel15.BackgroundImage = Image.FromFile(System.Environment.CurrentDirectory + "\\mainPic08.gif");
                    panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;


                }
				if (aa.Name == "air_radioButton4")
				{
					send_Msg(GlobalParameter.dustOpen);
					dust_radioButton3.Checked = false;
					air_radioButton4.Checked = true;
                    panel15.BackgroundImage = Image.FromFile(System.Environment.CurrentDirectory + "\\mainPic07.gif");
                    panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                  
				}
				panel15.Refresh();

                threadTimer = new System.Threading.Timer(new TimerCallback(TimerUp), null, Timeout.Infinite, 500);
                threadTimer.Change(0, 1000);
            }
			else {

				MessageBox.Show("需要先关闭实验！");
			
			}
			
		}


		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog saveDialog = new SaveFileDialog())
			{
				saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx";
				if (saveDialog.ShowDialog() != DialogResult.Cancel)
				{
					string exportFilePath = saveDialog.FileName;
					string fileExtenstion = new FileInfo(exportFilePath).Extension;

					switch (fileExtenstion)
					{
						case ".xls":
							gridControl1.ExportToXls(exportFilePath);
							break;
						case ".xlsx":
							gridControl1.ExportToXlsx(exportFilePath);
							break;
						case ".rtf":
							gridControl1.ExportToRtf(exportFilePath);
							break;
						case ".pdf":
							gridControl1.ExportToPdf(exportFilePath);
							break;
						case ".html":
							gridControl1.ExportToHtml(exportFilePath);
							break;
						case ".mht":
							gridControl1.ExportToMht(exportFilePath);
							break;
						default:
							break;
					}

					if (File.Exists(exportFilePath))
					{
						try
						{
							if (DialogResult.Yes == MessageBox.Show("文件已成功导出，是否打开文件?", "提示", MessageBoxButtons.YesNo))
							{

								System.Diagnostics.Process.Start(exportFilePath);
							}
						}
						catch
						{
							String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
							MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					else
					{
						String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
						MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

		}
        string diluteF = "";
        private void simpleButton1_Click_1(object sender, EventArgs e)
		{
            if (!_PortPM.IsOpen) {
                _PortPM.Open();
            }
            send_MsgPMP(GlobalParameter.readBack);
            // 重新开机后pm.25设备是数据指针在最后一个位置如果读取数据是没有的
            // 每条数据的结尾是/r/n;,在解析数据的时候可以利用这一点
            send_MsgPMP(GlobalParameter.readLog);

            //if (button3.Text == "开始" && switch1.Value) {

            //    send_Msg(GlobalParameter.readLog);

            //}

           
            //List<ReportData> rd = new List<ReportData>();
            //rd.Add(ReportData.inserData());
            //rd.Add(ReportData.inserData());
            //XtraReport1 xtraReport = new XtraReport1();
            //xtraReport.DataSource = rd;
         
            //using (SaveFileDialog saveDialog = new SaveFileDialog())
            //{
            //    saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx";
            //    if (saveDialog.ShowDialog() != DialogResult.Cancel)
            //    {
            //        string exportFilePath = saveDialog.FileName;
            //        string fileExtenstion = new FileInfo(exportFilePath).Extension;

            //        switch (fileExtenstion)
            //        {
            //            case ".xls":
            //                xtraReport.ExportToXls(exportFilePath);
            //                break;
            //            case ".xlsx":
            //                xtraReport.ExportToXlsx(exportFilePath);
            //                break;
                        
            //        }

            //        if (File.Exists(exportFilePath))
            //        {
            //            try
            //            {
            //                if (DialogResult.Yes == MessageBox.Show("文件已成功导出，是否打开文件?", "提示", MessageBoxButtons.YesNo))
            //                {

            //                    System.Diagnostics.Process.Start(exportFilePath);
            //                }
            //            }
            //            catch
            //            {
            //                String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
            //                MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //        else
            //        {
            //            String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
            //            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            //  ReportPrintTool printTool = new ReportPrintTool(xtraReport);
            // printTool.ShowPreview();
            // Invoke the Ribbon Print Preview form   
            // and load the report document into it.  
            // printTool.ShowRibbonPreview();

            // Invoke the Ribbon Print Preview form modally  
            // with the specified look and feel settings.  
            //printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);

            // Invoke the Print Preview form   
            // and load the report document into it.  


            // Invoke the Print Preview form modally  
            // with the specified look and feel settings.  
            // printTool.ShowPreviewDialog(UserLookAndFeel.Default);
            ;
            
            //try
            //{
            //	if (!switch1.Value)
            //	{
            //		if (threadTimer !=null) {

            //			threadTimer.Dispose();
            //			Thread.Sleep(500);
            //		}

            //		//获取主机电脑串口名称
            //		series_Port();
            //		//连接串口并开启读取数据计时器
            //		if (series_Open())
            //		{
            //			MessageBox.Show("串口连接成功");
            //			threadTimer = new System.Threading.Timer(new TimerCallback(TimerUp), null, Timeout.Infinite, 500);
            //			threadTimer.Change(0, 1000);

            //			readData.Enabled = true;
            //		}
            //		else
            //		{

            //			MessageBox.Show("需要检查连接设置");

            //		}


            //	}
            //	else
            //	{

            //		MessageBox.Show("先关闭实验");

            //	}
            //}
            //catch (Exception) {

            //	MessageBox.Show("串口连接未成功");

            //}

            //读取报告并


        }

        private void switch1_Click(object sender, EventArgs e)
        {

             
            if (button3.Text == "开始" && switch1.Value)
            {

                button3.Text = "关闭";
                switch1.Value = true;

                if (radioButton1.Checked)
                {


                    send_Msg(GlobalParameter.airOpen);
                    //更新参数参数状态


                }
                if (radioButton2.Checked)
                {

                    send_Msg(GlobalParameter.dustOpen);
                    //更新参数参数状态

                }
                string sendOrder = "";
               

                GlobalParameter.eanvalue = eanset_NumEdit.Value ;
                string diluteF = "0000" + (int.Parse((((air_setNumEdit.Value / 30 * 16 + 4) * 1000).ToString("0")).ToString())).ToString("X");
                diluteF = diluteF.Substring(diluteF.Length - 4, 4).Insert(2, " ");
                //发送质量流量控制器的执行命令 发送命令格式必须要在中间添加空格
                 sendOrder = "03 10 00 00 00 02 04 " + "00 00" + " " + diluteF + " " + CRC.ToModbusCRC16("03100000000204" + "00 00" + diluteF).Insert(2, " ");

                // 发送质量流量控制器的执行命令 发送命令格式必须要在中间添加空格
                ///多路同时开启


                //string diluteF = "0000" + (int.Parse(((air_setNumEdit.Value / 30 * 16 + 4) * 1000).ToString("0"))).ToString("X");
                //diluteF = diluteF.Substring(diluteF.Length - 4, 4).Insert(2, " ");
                //string ean581F = "0000" + (int.Parse(((eanset_NumEdit.Value / 100 * 16 + 4) * 1000).ToString("0"))).ToString("X");
                //ean581F = ean581F.Substring(ean581F.Length - 4, 4).Insert(2, " ");
                //string sendOrder = "03 10 00 00 00 02 04 " + ean581F + " " + diluteF + " " + CRC.ToModbusCRC16("03100000000204" + ean581F.Replace(" ", "") + diluteF.Replace(" ", "")).Insert(2, " ");

                send_Msg(sendOrder);

                if (checkOrder())
                {
                    ledArray1[0].Value = true;
                    ledArray1[1].Value = false;


                    stableTime.Enabled = true;
                    testTimer1.Enabled = false;

                    GlobalParameter.stableTime = double.Parse(setStableTime.Value.ToString()) * 60; 
                    GlobalParameter.testTime = double.Parse(setTesttime.Value.ToString()) * 60;
                    GlobalParameter.TimeCount = 0;
                    GlobalParameter.Dt.Clear();
                     // 执行轮训命令
                     threadTimer = new System.Threading.Timer(new TimerCallback(TimerUp), null, Timeout.Infinite, 450);
                    threadTimer.Change(0, 500);


                    Thread thread0 = new Thread(new ParameterizedThreadStart(UpdateStats));
                    thread0.Start("");


                }
                else
                {

                    MessageBox.Show("请尝试再次执行开始实验");


                }

            }
            else
            {
                if (button3.Text != "开始")
                {

                    testTimer1.Stop();
                    threadTimer.Dispose();
                    Thread.Sleep(500);
                    button3.Text = "开始";
                    switch1.Value = false;
                    send_Msg("03 10 00 00 00 02 04 00 00 00 00 F8 17");

                    send_Msg(GlobalParameter.dustClose);

                   // ean581F.Text = readPar.Ean581F;
                    DiluteAirF.Text = readPar.DiluteAirF;
                }
                else
                {

                    //  MessageBox.Show("串口连接失败----");

                }



            }
        }

        private void switch1_StateChanged(object sender, NationalInstruments.UI.ActionEventArgs e)
        {

        }
    }
    }
