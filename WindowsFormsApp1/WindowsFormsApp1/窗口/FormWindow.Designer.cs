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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.基本图形生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DDALine = new System.Windows.Forms.ToolStripMenuItem();
			this.中点直线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bresenhamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.中点圆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bresenhamToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.正负圆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bezier曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.b样条曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hermite曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.三角形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.wireFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gouraudShadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.textureMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.二维图形变换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.二位图形裁剪ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.图形填充ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.投影ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.消隐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.GouraudBtn = new System.Windows.Forms.Button();
			this.AmBtn_right = new System.Windows.Forms.Button();
			this.CameraRotateBtn = new System.Windows.Forms.Button();
			this.MeshRotateBtn_right = new System.Windows.Forms.Button();
			this.LightBtn = new System.Windows.Forms.Button();
			this.WireFrameBtn = new System.Windows.Forms.Button();
			this.UVBtn = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基本图形生成ToolStripMenuItem,
            this.二维图形变换ToolStripMenuItem,
            this.二位图形裁剪ToolStripMenuItem,
            this.图形填充ToolStripMenuItem,
            this.投影ToolStripMenuItem,
            this.消隐ToolStripMenuItem,
            this.Exit});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(719, 25);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// 基本图形生成ToolStripMenuItem
			// 
			this.基本图形生成ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DDALine,
            this.中点直线ToolStripMenuItem,
            this.bresenhamToolStripMenuItem,
            this.中点圆ToolStripMenuItem,
            this.bresenhamToolStripMenuItem1,
            this.正负圆ToolStripMenuItem,
            this.bezier曲线ToolStripMenuItem,
            this.b样条曲线ToolStripMenuItem,
            this.hermite曲线ToolStripMenuItem,
            this.三角形ToolStripMenuItem,
            this.wireFrameToolStripMenuItem,
            this.gouraudShadingToolStripMenuItem,
            this.textureMappingToolStripMenuItem});
			this.基本图形生成ToolStripMenuItem.Name = "基本图形生成ToolStripMenuItem";
			this.基本图形生成ToolStripMenuItem.Size = new System.Drawing.Size(50, 21);
			this.基本图形生成ToolStripMenuItem.Text = "Basic";
			// 
			// DDALine
			// 
			this.DDALine.Name = "DDALine";
			this.DDALine.Size = new System.Drawing.Size(178, 22);
			// 
			// 中点直线ToolStripMenuItem
			// 
			this.中点直线ToolStripMenuItem.Name = "中点直线ToolStripMenuItem";
			this.中点直线ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.中点直线ToolStripMenuItem.Text = "Mid Line";
			// 
			// bresenhamToolStripMenuItem
			// 
			this.bresenhamToolStripMenuItem.Name = "bresenhamToolStripMenuItem";
			this.bresenhamToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.bresenhamToolStripMenuItem.Text = "Bresenham Line";
			// 
			// 中点圆ToolStripMenuItem
			// 
			this.中点圆ToolStripMenuItem.Name = "中点圆ToolStripMenuItem";
			this.中点圆ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.中点圆ToolStripMenuItem.Text = "Mid Circle";
			// 
			// bresenhamToolStripMenuItem1
			// 
			this.bresenhamToolStripMenuItem1.Name = "bresenhamToolStripMenuItem1";
			this.bresenhamToolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
			// 
			// 正负圆ToolStripMenuItem
			// 
			this.正负圆ToolStripMenuItem.Name = "正负圆ToolStripMenuItem";
			this.正负圆ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.正负圆ToolStripMenuItem.Text = "Sign Circle";
			// 
			// bezier曲线ToolStripMenuItem
			// 
			this.bezier曲线ToolStripMenuItem.Name = "bezier曲线ToolStripMenuItem";
			this.bezier曲线ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.bezier曲线ToolStripMenuItem.Text = "Bezier Curve";
			// 
			// b样条曲线ToolStripMenuItem
			// 
			this.b样条曲线ToolStripMenuItem.Name = "b样条曲线ToolStripMenuItem";
			this.b样条曲线ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.b样条曲线ToolStripMenuItem.Text = "B-Spline Curve";
			// 
			// hermite曲线ToolStripMenuItem
			// 
			this.hermite曲线ToolStripMenuItem.Name = "hermite曲线ToolStripMenuItem";
			this.hermite曲线ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.hermite曲线ToolStripMenuItem.Text = "Hermite Curve";
			// 
			// 三角形ToolStripMenuItem
			// 
			this.三角形ToolStripMenuItem.Name = "三角形ToolStripMenuItem";
			this.三角形ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.三角形ToolStripMenuItem.Text = "Triangle";
			// 
			// wireFrameToolStripMenuItem
			// 
			this.wireFrameToolStripMenuItem.Name = "wireFrameToolStripMenuItem";
			this.wireFrameToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.wireFrameToolStripMenuItem.Text = "Wire Frame";
			this.wireFrameToolStripMenuItem.Click += new System.EventHandler(this.WireFrame);
			// 
			// gouraudShadingToolStripMenuItem
			// 
			this.gouraudShadingToolStripMenuItem.Name = "gouraudShadingToolStripMenuItem";
			this.gouraudShadingToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.gouraudShadingToolStripMenuItem.Text = "Gouraud Shading";
			this.gouraudShadingToolStripMenuItem.Click += new System.EventHandler(this.gouraudShading);
			// 
			// textureMappingToolStripMenuItem
			// 
			this.textureMappingToolStripMenuItem.Name = "textureMappingToolStripMenuItem";
			this.textureMappingToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.textureMappingToolStripMenuItem.Text = "Texture Mapping";
			this.textureMappingToolStripMenuItem.Click += new System.EventHandler(this.textureMapping);
			// 
			// 二维图形变换ToolStripMenuItem
			// 
			this.二维图形变换ToolStripMenuItem.Name = "二维图形变换ToolStripMenuItem";
			this.二维图形变换ToolStripMenuItem.Size = new System.Drawing.Size(125, 21);
			this.二维图形变换ToolStripMenuItem.Text = "Stereogram Trans";
			// 
			// 二位图形裁剪ToolStripMenuItem
			// 
			this.二位图形裁剪ToolStripMenuItem.Name = "二位图形裁剪ToolStripMenuItem";
			this.二位图形裁剪ToolStripMenuItem.Size = new System.Drawing.Size(123, 21);
			this.二位图形裁剪ToolStripMenuItem.Text = "Stereogram tailor";
			// 
			// 图形填充ToolStripMenuItem
			// 
			this.图形填充ToolStripMenuItem.Name = "图形填充ToolStripMenuItem";
			this.图形填充ToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
			this.图形填充ToolStripMenuItem.Text = "Bhatch";
			// 
			// 投影ToolStripMenuItem
			// 
			this.投影ToolStripMenuItem.Name = "投影ToolStripMenuItem";
			this.投影ToolStripMenuItem.Size = new System.Drawing.Size(78, 21);
			this.投影ToolStripMenuItem.Text = "Projection";
			// 
			// 消隐ToolStripMenuItem
			// 
			this.消隐ToolStripMenuItem.Name = "消隐ToolStripMenuItem";
			this.消隐ToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
			this.消隐ToolStripMenuItem.Text = "Blank";
			this.消隐ToolStripMenuItem.Click += new System.EventHandler(this.Blank);
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
			this.GouraudBtn.Location = new System.Drawing.Point(619, 382);
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
			this.AmBtn_right.Location = new System.Drawing.Point(619, 595);
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
			this.CameraRotateBtn.Location = new System.Drawing.Point(619, 539);
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
			this.MeshRotateBtn_right.Location = new System.Drawing.Point(619, 485);
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
			this.LightBtn.Location = new System.Drawing.Point(634, 645);
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
			this.WireFrameBtn.Location = new System.Drawing.Point(619, 331);
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
			this.UVBtn.Location = new System.Drawing.Point(619, 432);
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
			this.ClientSize = new System.Drawing.Size(719, 700);
			this.Controls.Add(this.UVBtn);
			this.Controls.Add(this.WireFrameBtn);
			this.Controls.Add(this.GouraudBtn);
			this.Controls.Add(this.AmBtn_right);
			this.Controls.Add(this.CameraRotateBtn);
			this.Controls.Add(this.MeshRotateBtn_right);
			this.Controls.Add(this.LightBtn);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "FormWindow";
			this.Text = "Compute Plateform";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ForLayout);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormWindow_MouseDown);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormWindow_MouseUp);
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_OnMouseWheel);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基本图形生成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DDALine;
        private System.Windows.Forms.ToolStripMenuItem 中点直线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bresenhamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 中点圆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bresenhamToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 正负圆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bezier曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b样条曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hermite曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 二维图形变换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 二位图形裁剪ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图形填充ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 投影ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 消隐ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.ToolStripMenuItem 三角形ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wireFrameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gouraudShadingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem textureMappingToolStripMenuItem;
        private System.Windows.Forms.Button GouraudBtn;
        private System.Windows.Forms.Button AmBtn_right;
        private System.Windows.Forms.Button CameraRotateBtn;
        private System.Windows.Forms.Button MeshRotateBtn_right;
        private System.Windows.Forms.Button LightBtn;
		private System.Windows.Forms.Button UVBtn;
		private System.Windows.Forms.Button WireFrameBtn;
	}
}

