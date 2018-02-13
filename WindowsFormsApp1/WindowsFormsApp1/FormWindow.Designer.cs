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
            this.二维图形变换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.二位图形裁剪ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图形填充ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.投影ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.消隐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.三角形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuStrip1.Size = new System.Drawing.Size(491, 25);
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
            this.三角形ToolStripMenuItem});
            this.基本图形生成ToolStripMenuItem.Name = "基本图形生成ToolStripMenuItem";
            this.基本图形生成ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.基本图形生成ToolStripMenuItem.Text = "基本图形生成";
            // 
            // DDALine
            // 
            this.DDALine.Name = "DDALine";
            this.DDALine.Size = new System.Drawing.Size(165, 22);
            this.DDALine.Text = "DDA直线";
            this.DDALine.Click += new System.EventHandler(this.DDALine_Click);
            // 
            // 中点直线ToolStripMenuItem
            // 
            this.中点直线ToolStripMenuItem.Name = "中点直线ToolStripMenuItem";
            this.中点直线ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.中点直线ToolStripMenuItem.Text = "中点直线";
            // 
            // bresenhamToolStripMenuItem
            // 
            this.bresenhamToolStripMenuItem.Name = "bresenhamToolStripMenuItem";
            this.bresenhamToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.bresenhamToolStripMenuItem.Text = "Bresenham直线";
            // 
            // 中点圆ToolStripMenuItem
            // 
            this.中点圆ToolStripMenuItem.Name = "中点圆ToolStripMenuItem";
            this.中点圆ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.中点圆ToolStripMenuItem.Text = "中点圆";
            // 
            // bresenhamToolStripMenuItem1
            // 
            this.bresenhamToolStripMenuItem1.Name = "bresenhamToolStripMenuItem1";
            this.bresenhamToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.bresenhamToolStripMenuItem1.Text = "Bresenham圆";
            this.bresenhamToolStripMenuItem1.Click += new System.EventHandler(this.bresenhamToolStripMenuItem1_Click);
            // 
            // 正负圆ToolStripMenuItem
            // 
            this.正负圆ToolStripMenuItem.Name = "正负圆ToolStripMenuItem";
            this.正负圆ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.正负圆ToolStripMenuItem.Text = "正负圆";
            // 
            // bezier曲线ToolStripMenuItem
            // 
            this.bezier曲线ToolStripMenuItem.Name = "bezier曲线ToolStripMenuItem";
            this.bezier曲线ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.bezier曲线ToolStripMenuItem.Text = "Bezier曲线";
            // 
            // b样条曲线ToolStripMenuItem
            // 
            this.b样条曲线ToolStripMenuItem.Name = "b样条曲线ToolStripMenuItem";
            this.b样条曲线ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.b样条曲线ToolStripMenuItem.Text = "B样条曲线";
            // 
            // hermite曲线ToolStripMenuItem
            // 
            this.hermite曲线ToolStripMenuItem.Name = "hermite曲线ToolStripMenuItem";
            this.hermite曲线ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.hermite曲线ToolStripMenuItem.Text = "Hermite曲线";
            // 
            // 二维图形变换ToolStripMenuItem
            // 
            this.二维图形变换ToolStripMenuItem.Name = "二维图形变换ToolStripMenuItem";
            this.二维图形变换ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.二维图形变换ToolStripMenuItem.Text = "二维图形变换";
            // 
            // 二位图形裁剪ToolStripMenuItem
            // 
            this.二位图形裁剪ToolStripMenuItem.Name = "二位图形裁剪ToolStripMenuItem";
            this.二位图形裁剪ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.二位图形裁剪ToolStripMenuItem.Text = "二位图形裁剪";
            // 
            // 图形填充ToolStripMenuItem
            // 
            this.图形填充ToolStripMenuItem.Name = "图形填充ToolStripMenuItem";
            this.图形填充ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图形填充ToolStripMenuItem.Text = "图形填充";
            // 
            // 投影ToolStripMenuItem
            // 
            this.投影ToolStripMenuItem.Name = "投影ToolStripMenuItem";
            this.投影ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.投影ToolStripMenuItem.Text = "投影";
            // 
            // 消隐ToolStripMenuItem
            // 
            this.消隐ToolStripMenuItem.Name = "消隐ToolStripMenuItem";
            this.消隐ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.消隐ToolStripMenuItem.Text = "消隐";
            // 
            // Exit
            // 
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(44, 21);
            this.Exit.Text = "退出";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // 三角形ToolStripMenuItem
            // 
            this.三角形ToolStripMenuItem.Name = "三角形ToolStripMenuItem";
            this.三角形ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.三角形ToolStripMenuItem.Text = "三角形";
            this.三角形ToolStripMenuItem.Click += new System.EventHandler(this.三角形ToolStripMenuItem_Click);
            // 
            // FormWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 443);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormWindow";
            this.Text = " 计算机图形学联系平台";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormWindow_Layout);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
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
    }
}

