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
			this.menuStrip1.Size = new System.Drawing.Size(1354, 25);
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
			//this.基本图形生成ToolStripMenuItem.Click += new System.EventHandler(this.基本图形生成ToolStripMenuItem_Click);
			// 
			// DDALine
			// 
			
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
			//this.三角形ToolStripMenuItem.Click += new System.EventHandler(this.三角形ToolStripMenuItem_Click);
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
			//this.二位图形裁剪ToolStripMenuItem.Click += new System.EventHandler(this.二位图形裁剪ToolStripMenuItem_Click);
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



            int y = 150;
            int x = 600;

            this.RotateBtn = new System.Windows.Forms.Button();
            this.RotateBtn.Name = "左旋转";
            this.RotateBtn.BackColor = BackColor;
            this.RotateBtn.Text = "left rotate";
            this.RotateBtn.AutoSize = true;
            this.RotateBtn.Click += new System.EventHandler(this.rotate);
            this.RotateBtn.SetBounds(x, y + 50, 50, 20);
            Controls.Add(this.RotateBtn);

            this.RotateBtn_right = new System.Windows.Forms.Button();
            this.RotateBtn_right.Name = "右旋转";
            this.RotateBtn_right.BackColor = BackColor;
            this.RotateBtn_right.SetBounds(x, y + 90, 50, 20);
            this.RotateBtn_right.AutoSize = true;
            this.RotateBtn_right.Text = "right rotate";
            this.RotateBtn_right.Click += new System.EventHandler(this.rotate_right);
            Controls.Add(this.RotateBtn_right);


            this.TranslateBtn = new System.Windows.Forms.Button();
            this.TranslateBtn.Name = "左平移";
            this.TranslateBtn.SetBounds(x, y + 130, 50, 20);
            this.TranslateBtn.BackColor = BackColor;
            this.TranslateBtn.Text = "left transleft";
            this.TranslateBtn.AutoSize = true;
            this.TranslateBtn.Click += new System.EventHandler(this.tranlate);
            Controls.Add(this.TranslateBtn);


            this.TranslateBtn_right = new System.Windows.Forms.Button();
            this.TranslateBtn_right.SetBounds(x, y + 170, 50, 20);
            this.TranslateBtn_right.Name = "右平移";
            this.TranslateBtn_right.BackColor = BackColor;
            this.TranslateBtn_right.Text = "right translate";
            this.TranslateBtn_right.AutoSize = true;
            this.TranslateBtn_right.Click += new System.EventHandler(this.translate_right);
            Controls.Add(this.TranslateBtn_right);

            this.LightBtn = new System.Windows.Forms.Button();
            //this.LightBtn.DisplayRectangle = new System.Drawing.Rectangle(50, 50, 50, 20);
            //this.LightBtn.ClientSize = new System.Drawing.Size(50, 20);
            //this.LightBtn.Name = "灯光";
            this.LightBtn.SetBounds(x, y + 210, 50, 20);
            //this.LightBtn.Size = new System.Drawing.Size(100, 20);
            this.LightBtn.BackColor = BackColor;
            this.LightBtn.Text = "open light";
            this.LightBtn.AutoSize = true;
            this.LightBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LightBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LightBtn.Click += new System.EventHandler(this.lighting);
            Controls.Add(this.LightBtn);



			// 
			// Exit
			// 
			this.Exit.Name = "Exit";
			this.Exit.Size = new System.Drawing.Size(40, 21);
			this.Exit.Text = "Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
			// 
			// FormWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.WindowText;
			this.ClientSize = new System.Drawing.Size(721, 429);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "FormWindow";
			this.Text = "Compute Plateform";
			this.Load += new System.EventHandler(this.Form1_Load);
			//this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormWindow_Paint);
			//this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormWindow_Layout);
			//this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormWindow_MouseDown);
			//this.MouseLeave += new System.EventHandler(this.FormWindow_MouseLeave);
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
        private System.Windows.Forms.Button RotateBtn;
        private System.Windows.Forms.Button RotateBtn_right;
        private System.Windows.Forms.Button TranslateBtn;
        private System.Windows.Forms.Button TranslateBtn_right;
        private System.Windows.Forms.Button LightBtn;

	}
}

