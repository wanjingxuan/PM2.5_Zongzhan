namespace PM2._5_Project
{
    partial class page2
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.buttonX4 = new PM2._5_Project.ButtonX();
			this.buttonX3 = new PM2._5_Project.ButtonX();
			this.buttonX2 = new PM2._5_Project.ButtonX();
			this.buttonX1 = new PM2._5_Project.ButtonX();
			this.SuspendLayout();
			// 
			// buttonX4
			// 
			this.buttonX4.Location = new System.Drawing.Point(361, 154);
			this.buttonX4.Name = "buttonX4";
			this.buttonX4.Size = new System.Drawing.Size(75, 23);
			this.buttonX4.TabIndex = 3;
			this.buttonX4.Text = "buttonX4";
			this.buttonX4.UseVisualStyleBackColor = true;
			// 
			// buttonX3
			// 
			this.buttonX3.BackColor = System.Drawing.Color.Red;
			this.buttonX3.Location = new System.Drawing.Point(264, 275);
			this.buttonX3.Name = "buttonX3";
			this.buttonX3.Size = new System.Drawing.Size(61, 75);
			this.buttonX3.TabIndex = 2;
			this.buttonX3.UseVisualStyleBackColor = false;
			this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
			// 
			// buttonX2
			// 
			this.buttonX2.Location = new System.Drawing.Point(317, 71);
			this.buttonX2.Name = "buttonX2";
			this.buttonX2.Size = new System.Drawing.Size(269, 152);
			this.buttonX2.TabIndex = 1;
			this.buttonX2.Text = "buttonX2";
			this.buttonX2.UseVisualStyleBackColor = true;
			// 
			// buttonX1
			// 
			this.buttonX1.Location = new System.Drawing.Point(468, 295);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(48, 35);
			this.buttonX1.TabIndex = 0;
			this.buttonX1.Text = "buttonX1";
			this.buttonX1.UseVisualStyleBackColor = true;
			// 
			// page2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonX4);
			this.Controls.Add(this.buttonX3);
			this.Controls.Add(this.buttonX2);
			this.Controls.Add(this.buttonX1);
			this.Name = "page2";
			this.Size = new System.Drawing.Size(730, 556);
			this.ResumeLayout(false);

        }

        #endregion

		private ButtonX buttonX1;
		private ButtonX buttonX2;
		private ButtonX buttonX3;
		private ButtonX buttonX4;
    }
}
