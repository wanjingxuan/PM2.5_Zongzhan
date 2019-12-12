using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;

namespace PM2._5_Project
{
	class ButtonX : Button
	{

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{


			base.OnPaint(e);
			System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            GraphicsPath gp = new GraphicsPath();
			List<PointF> arrPoints = new List<PointF>();
			arrPoints.Add(new PointF(18, 10));
			arrPoints.Add(new PointF(35, 41));
			arrPoints.Add(new PointF(50, 10));
			//arrPoints.Add(new PointF(18, 21));
			gp.AddLines(arrPoints.ToArray());
			gp.CloseFigure();

			this.CreateGraphics().DrawPath(Pens.Black, gp);
			//Region r = new Region(gp);

			//需要将button 的FlatStyle属性更改成.Flat，才能去掉边框;
			FlatAppearance.BorderSize = 0;//去掉边框

			this.Region = new Region(gp);


		}

		
	}
}
