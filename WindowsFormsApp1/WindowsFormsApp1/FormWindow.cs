using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MyBrush;
using System.Drawing.Imaging;

namespace RenderingEngine
{
    public partial class FormWindow : Form
    {
        public Bitmap bitmap;
        public Graphics g;
        private Device device;
        public Scene scene;
        public Rectangle rect;
        public PixelFormat pixelFormat;
      
        public FormWindow()
        { 
            InitializeComponent();
            InitGraphics();
            InitScene();
        }

        private void InitGraphics()
        {
            this.g = CreateGraphics();
        }

        private void InitScene()
        {
            this.bitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Format24bppRgb);
            this.device = new Device(bitmap);
            this.scene = new Scene(this.ClientSize.Width, this.ClientSize.Height);
            this.rect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            this.pixelFormat = bitmap.PixelFormat;
        }

        private void bresenhamToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Color BackColor1 = Color.White;
        Color ForeColor1 = Color.Black;

        public int MenuID, PressNum, FirstX, FirstY, OldX, OldY;

        private void FormWindow_Layout(object sender, LayoutEventArgs e)
        {
        }

        private void 三角形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
			device.StartDisplay(this);
			/*
            System.Timers.Timer  t = new System.Timers.Timer(1000); //实例化timer 设置时间间隔1000毫秒
            t.Elapsed += new System.Timers.ElapsedEventHandler(Start);//到达时间的执行事件
            t.AutoReset = true; // 设置是执行一次还是一直执行
            t.Enabled = true; // 是否执行System.Timers.Timer.Elapsed事件
            t.Start();*/


		}

		private void 二位图形裁剪ToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		void Start(object source, System.Timers.ElapsedEventArgs e)
        {
            // BitmapData data = this.bitmap.LockBits(rect, ImageLockMode.ReadWrite, this.pixelFormat);

            //this.bitmap.UnlockBits(data);
            //device.StartDisplay(this.scene);
            // g.DrawImage(this.bitmap, new Point(0, 0));
            device.StartDisplay(this);
        }

      
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {           
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void DDALine_Click(object sender, EventArgs e)
        {
            MenuID = 1; PressNum = 0;
            Graphics g = CreateGraphics(); // 创建图形设备
            g.Clear(BackColor1); // 设置背景色
        }

    }
}
