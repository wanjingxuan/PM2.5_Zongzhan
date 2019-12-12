using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace New_Mix
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {

        float initWidth =800;
        float initHeiht =600;
        public Form1()
        {
            InitializeComponent();
            setTag(this);//调用方法
            // InitInstance(this);
        }

        /// <summary>
        /// 自适应屏幕分辨率
        /// </summary>
        /// <param name="form">窗体对象</param>
        public  void InitInstance(DevExpress.XtraEditors.XtraForm form)
        {
            System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            int h = rect.Height; //高（像素）
            int w = rect.Width;  //宽（像素）

            this.Size = new Size(750, 600);
            foreach (Control c in form.Controls)
            {
                // 1920*1080  为当前窗体设计的尺寸
                c.Size = new Size((int)(c.Width * w / 1920), (int)(c.Height * h / 1080));
                c.Location = new Point((int)(c.Left * w / 1920), (int)(c.Top * h / 1080));
                Single size = Convert.ToSingle(c.Font.Size * h / 1080);
                c.Font = new Font(c.Font.Name, size, c.Font.Style, c.Font.Unit);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
            //timer1.Interval = 200;
            //timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //if ((new Random().Next(0, 5) + int.Parse(textBox1.Text)) > 97)
            //{
            //    timer1.Enabled = false;
            //    labelControl1.Text = "    Average    ";
            //    progressBarControl1.Properties.StartColor = Color.Transparent ;
            //    progressBarControl1.Properties.EndColor = Color.Transparent;
            //    progressBarControl1.Text ="";
            //    //  progressBarControl1.Properties.DisplayFormat.FormatString = "{0}";
            //    timer2.Interval = 500;
            //    timer2.Enabled = true;
            //}
            //else {
            //}
            try
            {
                string cc = "{0}." + new Random().Next(0, 9) + "                            100  m3/h                 ";
              //  progressBarControl1.Properties.DisplayFormat.FormatString = cc;
             //   progressBarControl1.Text = (new Random().Next(0, 5) + int.Parse(textBox1.Value.ToString().Split('.')[0])).ToString();



            }
            catch { }




        }

        private void timer2_Tick(object sender, EventArgs e)
        {

           // progressBarControl1.Properties.DisplayFormat.FormatString = (new Random().Next(0, 5) + int.Parse(textBox1.Text)).ToString() + "                               100  m3/h                 ";

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //textBox3.Text = new Random().Next(1, 200).ToString();
        }

        

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            float newx = (this.Width) / initWidth; //窗体宽度缩放比例
            float newy = (this.Height) / initHeiht;//窗体高度缩放比例
            setControls(newx, newy, this);//随窗体改变控件大小
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.Width + "||" + this.Height);
            MessageBox.Show(panel1.Location.X + "||" + panel1.Location.Y);
        }

        /// <summary>
        /// 将控件的宽，高，左边距，顶边距和字体大小暂存到tag属性中
        /// </summary>
        /// <param name="cons">递归控件中的控件</param>
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }

        //根据窗体大小调整控件大小
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                if (con.Name != "panel2")
                {


                    string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                    float a = System.Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                    con.Width = (int)a;//宽度
                    a = System.Convert.ToSingle(mytag[1]) * newy;//高度
                    con.Height = (int)(a);
                    a = System.Convert.ToSingle(mytag[2]) * newx;//左边距离
                    con.Left = (int)(a);
                    a = System.Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                    con.Top = (int)(a);
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
                else {

                    string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                    float a = System.Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                    con.Width = (int)a;//宽度
                   a = System.Convert.ToSingle(mytag[1]) * newy;//高度
                   // con.Height = (int)(a);
                   a = System.Convert.ToSingle(mytag[2]) * newx;//左边距离
                    con.Left = (int)(a);
                    a = System.Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                    con.Top = (int)(a);
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    //if (con.Controls.Count > 0)
                    //{
                    //    setControls(newx, newy, con);
                    //}


                }
            }
        }

        private void progressBarControl1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
    }


 }
