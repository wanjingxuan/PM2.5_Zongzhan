using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PM2._5_Project
{
	class FullTabControl__sy:TabControl
	{
		 public FullTabControl__sy()
        {
           SetStyle ( ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
      
       protected override CreateParams CreateParams
        {
           get
            {
               CreateParams cp = base.CreateParams;
               cp.ExStyle |= 0x02000000;
               return cp;
            }
        }


        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
               
                return new Rectangle(rect.Left-10, rect.Top-10, rect.Width+15, rect.Height+15);
                
            }
        }
    }  
	}

