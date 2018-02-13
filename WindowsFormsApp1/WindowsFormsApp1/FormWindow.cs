using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Scene scene;
        private Rectangle rect;
        private PixelFormat pixelFormat;
      
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
            Triangle angle = new Triangle(new Vector4(50, 50, 0, 0), new Vector4(300, 150, 0, 0), new Vector4( 100, 300, 0, 0));
            this.device.scanline.addTriangle(angle);
            this.device.StartDisplay(this);
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
