namespace RenderingEngine
{
    partial class FormWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			
			this.Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.GouraudBtn = new System.Windows.Forms.Button();
			this.AmBtn_right = new System.Windows.Forms.Button();
			this.CameraRotateBtn = new System.Windows.Forms.Button();
			this.MeshRotateBtn_right = new System.Windows.Forms.Button();
			this.LightBtn = new System.Windows.Forms.Button();
			this.WireFrameBtn = new System.Windows.Forms.Button();
			this.UVBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			// 
			// Exit
			// 
			this.Exit.Name = "Exit";
			this.Exit.Size = new System.Drawing.Size(40, 21);
			this.Exit.Text = "Exit";
			this.Exit.Click += new System.EventHandler(this.Exit_Click);
			// 
			// GouraudBtn
			// 
			this.GouraudBtn.AutoSize = true;
			this.GouraudBtn.BackColor = this.BackColor;
			this.GouraudBtn.ForeColor = System.Drawing.SystemColors.Desktop;
			this.GouraudBtn.Location = new System.Drawing.Point(392, 143);
			this.GouraudBtn.Margin = new System.Windows.Forms.Padding(0);
			this.GouraudBtn.Name = "GouraudBtn";
			this.GouraudBtn.Size = new System.Drawing.Size(77, 25);
			this.GouraudBtn.TabIndex = 0;
			this.GouraudBtn.Text = "渲染";
			this.GouraudBtn.UseVisualStyleBackColor = false;
			this.GouraudBtn.Click += new System.EventHandler(this.GouraudBtn_Click);
			// 
			// AmBtn_right
			// 
			this.AmBtn_right.AutoSize = true;
			this.AmBtn_right.BackColor = this.BackColor;
			this.AmBtn_right.ForeColor = System.Drawing.SystemColors.Desktop;
			this.AmBtn_right.Location = new System.Drawing.Point(392, 356);
			this.AmBtn_right.Margin = new System.Windows.Forms.Padding(0);
			this.AmBtn_right.Name = "AmBtn_right";
			this.AmBtn_right.Size = new System.Drawing.Size(77, 25);
			this.AmBtn_right.TabIndex = 1;
			this.AmBtn_right.Text = "开关环境光";
			this.AmBtn_right.UseVisualStyleBackColor = false;
			this.AmBtn_right.Click += new System.EventHandler(this.AmBtn_right_Click);
			// 
			// CameraRotateBtn
			// 
			this.CameraRotateBtn.AutoSize = true;
			this.CameraRotateBtn.BackColor = this.BackColor;
			this.CameraRotateBtn.ForeColor = System.Drawing.SystemColors.Desktop;
			this.CameraRotateBtn.Location = new System.Drawing.Point(392, 300);
			this.CameraRotateBtn.Name = "CameraRotateBtn";
			this.CameraRotateBtn.Size = new System.Drawing.Size(77, 25);
			this.CameraRotateBtn.TabIndex = 2;
			this.CameraRotateBtn.Text = "相机旋转";
			this.CameraRotateBtn.UseVisualStyleBackColor = false;
			this.CameraRotateBtn.Click += new System.EventHandler(this.CameraRotateBtn_Click);
			// 
			// MeshRotateBtn_right
			// 
			this.MeshRotateBtn_right.AutoSize = true;
			this.MeshRotateBtn_right.BackColor = this.BackColor;
			this.MeshRotateBtn_right.ForeColor = System.Drawing.SystemColors.Desktop;
			this.MeshRotateBtn_right.Location = new System.Drawing.Point(392, 246);
			this.MeshRotateBtn_right.Name = "MeshRotateBtn_right";
			this.MeshRotateBtn_right.Size = new System.Drawing.Size(77, 25);
			this.MeshRotateBtn_right.TabIndex = 3;
			this.MeshRotateBtn_right.Text = "物体旋转";
			this.MeshRotateBtn_right.UseVisualStyleBackColor = false;
			this.MeshRotateBtn_right.Click += new System.EventHandler(this.MeshRotateBtn_right_Click);
			// 
			// LightBtn
			// 
			this.LightBtn.AutoSize = true;
			this.LightBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.LightBtn.BackColor = this.BackColor;
			this.LightBtn.Image = global::RenderingEngine.Properties.Resources._22_副本;
			this.LightBtn.Location = new System.Drawing.Point(407, 406);
			this.LightBtn.Margin = new System.Windows.Forms.Padding(0);
			this.LightBtn.Name = "LightBtn";
			this.LightBtn.Size = new System.Drawing.Size(46, 46);
			this.LightBtn.TabIndex = 4;
			this.LightBtn.UseVisualStyleBackColor = false;
			this.LightBtn.Click += new System.EventHandler(this.lighting);
			// 
			// WireFrameBtn
			// 
			this.WireFrameBtn.AutoSize = true;
			this.WireFrameBtn.BackColor = this.BackColor;
			this.WireFrameBtn.ForeColor = System.Drawing.SystemColors.Desktop;
			this.WireFrameBtn.Location = new System.Drawing.Point(392, 92);
			this.WireFrameBtn.Margin = new System.Windows.Forms.Padding(0);
			this.WireFrameBtn.Name = "WireFrameBtn";
			this.WireFrameBtn.Size = new System.Drawing.Size(77, 25);
			this.WireFrameBtn.TabIndex = 5;
			this.WireFrameBtn.Text = "线框";
			this.WireFrameBtn.UseVisualStyleBackColor = false;
			this.WireFrameBtn.Click += new System.EventHandler(this.WireFrameBtn_Click);
			// 
			// UVBtn
			// 
			this.UVBtn.AutoSize = true;
			this.UVBtn.BackColor = this.BackColor;
			this.UVBtn.ForeColor = System.Drawing.SystemColors.Desktop;
			this.UVBtn.Location = new System.Drawing.Point(392, 193);
			this.UVBtn.Name = "UVBtn";
			this.UVBtn.Size = new System.Drawing.Size(77, 25);
			this.UVBtn.TabIndex = 6;
			this.UVBtn.Text = "纹理";
			this.UVBtn.UseVisualStyleBackColor = false;
			this.UVBtn.Click += new System.EventHandler(this.UVBtn_Click);
			// 
			// FormWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.WindowText;
			this.ClientSize = new System.Drawing.Size(484, 461);
			this.Controls.Add(this.UVBtn);
			this.Controls.Add(this.WireFrameBtn);
			this.Controls.Add(this.GouraudBtn);
			this.Controls.Add(this.AmBtn_right);
			this.Controls.Add(this.CameraRotateBtn);
			this.Controls.Add(this.MeshRotateBtn_right);
			this.Controls.Add(this.LightBtn);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "FormWindow";
			this.Text = "Compute Plateform";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ForLayout);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormWindow_MouseDown);

			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_OnMouseWheel);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem Exit;

        private System.Windows.Forms.Button GouraudBtn;
        private System.Windows.Forms.Button AmBtn_right;
        private System.Windows.Forms.Button CameraRotateBtn;
        private System.Windows.Forms.Button MeshRotateBtn_right;
        private System.Windows.Forms.Button LightBtn;
		private System.Windows.Forms.Button UVBtn;
		private System.Windows.Forms.Button WireFrameBtn;
	}
}

