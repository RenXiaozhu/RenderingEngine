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
			InitSetting();
        }

        private void InitGraphics()
        {
            this.g = CreateGraphics();
        }

        private void InitScene()
        {
            this.bitmap = new Bitmap(584, 362, PixelFormat.Format24bppRgb);
            this.device = new Device(bitmap);
            this.scene = new Scene(584, 362);
            this.rect = new Rectangle(0, 0,584, 362);
            this.pixelFormat = bitmap.PixelFormat;
        }

		private void InitSetting()
		{
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
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
		}

		private void 二位图形裁剪ToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void 基本图形生成ToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void FormWindow_Paint(object sender, PaintEventArgs e)
		{
			this.device.ClearBitmap();
			//g.Clear(BackColor1);
			BitmapData data = this.bitmap.LockBits(rect, ImageLockMode.ReadWrite, this.pixelFormat);
			this.device.ClearBitmapData(data);
			device.Render(scene, data);
			this.bitmap.UnlockBits(data);
			g.DrawImage(this.bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
		}

		void Start(object source, System.Timers.ElapsedEventArgs e)
        {
			//this.Paint += new PaintEventHandler(FormWindow_Paint);
		}

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
			//this.Paint += new PaintEventHandler(FormWindow_Paint);
			//滑轮滚动
			this.MouseWheel += new MouseEventHandler(FormWindow_OnMouseWheel);
			this.MouseMove += new MouseEventHandler(FormWindow_OnMouseMove);

			System.Timers.Timer t = new System.Timers.Timer(33); //实例化timer 设置时间间隔1000毫秒
			t.Elapsed += new System.Timers.ElapsedEventHandler(Start);//到达时间的执行事件
			t.AutoReset = true; // 设置是执行一次还是一直执行
			t.Enabled = true; // 是否执行System.Timers.Timer.Elapsed事件
			t.Start();
		}

		private const float MoveSpeed = 5f;
		private const float RotateSpeed = 5f * (float)Math.PI / 180f;
		private int mouseX = 0;
		private int mouseY = 0;


		private void wireFrameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.scene.renderState = Scene.RenderState.WireFrame;
			
		}

		private void gouraudShadingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.scene.renderState = Scene.RenderState.GouraduShading;
			this.Invalidate();
		}

		private void textureMappingToolStripMenuItem_Click(object sender, EventArgs e)
		{

			this.scene.renderState = Scene.RenderState.TextureMapping;
			BitmapData bmData = this.scene.mesh.texture.LockBits();
			//this.Invalidate();
			this.scene.mesh.texture.UnlockBits(bmData);
		}

		private Point pressdown = new Point(0, 0);
		private double angleY = 0;
		private double angleX = 0;

		private void FormWindow_MouseDown(object sender, MouseEventArgs e)
		{
			pressdown = new Point(e.X, e.Y);
		}

		private void FormWindow_MouseLeave(object sender, EventArgs e)
		{

		}

		private void FormWindow_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				double oritX = this.scene.camera.self_position.x;
				double oriY = this.scene.camera.self_position.y;
				double oriZ = this.scene.camera.self_position.z;
				//if (e.X - mouseX > 0)
				//{
				double newX = oritX * Math.Cos(-RotateSpeed) - oriZ * Math.Sign(-RotateSpeed);
				double newZ = oritX * Math.Sin(RotateSpeed) + oriZ * Math.Cos(RotateSpeed);
				angleX = Math.Atan((e.Y - pressdown.Y) / 10 / Math.Abs(oriZ));
				angleY = Math.Atan((e.X - pressdown.X) / 10 / Math.Abs(oriZ));
				//this.scene.UpdateCameraPos(new Vector4(newX, oriY, newZ, 1));
				//this.scene.UpdateCameraRotation((float)degree);
				//}
				//else
				//{
				//	double newX = oritX * Math.Cos(RotateSpeed) - oriZ * Math.Sign(RotateSpeed);
				//	double newZ = oritX * Math.Sin(RotateSpeed) + oriZ * Math.Cos(RotateSpeed);
				//	this.scene.UpdateCameraPos(new Vector4(newX, oriY, newZ, 1));
				//}

				this.scene.UpdateModelRotationMatrix((float)angleX, (float)angleY, 0);
				float pitch = 0.0f;

				//if (e.Y - pressdown.Y < 0)
				//{
				//	pitch+= 5f;
				//}
				//else
				//{
				//	pitch -= 5f;
				//}
				//this.scene.UpdateCameraPitch(pitch);
				//图形重绘
				this.Invalidate();
			}
		}

		

		private void FormWindow_OnMouseMove(object sender, MouseEventArgs e)
		{

			mouseX = e.X;
			mouseY = e.Y;
			//this.Invalidate();
		}

		private void FormWindow_OnMouseWheel(object sender, MouseEventArgs e)
		{
			double oriX = this.scene.camera.self_position.x;
			double oriY = this.scene.camera.self_position.y;
			double oriZ = this.scene.camera.self_position.z;
			Vector4 dir = this.scene.camera.self_position.Normalized;
			double x = dir.dot(new Vector4(1, 0, 0, 1)) * MoveSpeed * 0.5f;
			double y = dir.dot(new Vector4(0, 1, 0, 1)) * MoveSpeed * 0.5f;
			double z = dir.dot(new Vector4(0, 0, 1, 1)) * MoveSpeed * 0.5f;
			//if (e.Delta < 0)
			//{
			//	this.scene.UpdateCameraPos(new Vector4(oriX + x, oriY + y, oriZ + z, 1));
			//}
			//else
			//{
			//	this.scene.UpdateCameraPos(new Vector4(oriX - x, oriY - y, oriZ - z,1));
			//}

			if (e.Delta < 0)
			{
				this.scene.UpdateCameraPos(new Vector4(oriX, oriY, oriZ - MoveSpeed*0.5f, 1));
			}
			else
			{
				this.scene.UpdateCameraPos(new Vector4(oriX , oriY , oriZ + MoveSpeed*0.5f, 1));
			}
			this.Invalidate();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			double oriX = this.scene.camera.self_position.x;
			double oriY = this.scene.camera.self_position.y;
			double oriZ = this.scene.camera.self_position.z;
			if (keyData == Keys.W)
			{
				this.scene.UpdateCameraPos(new Vector4(oriX, oriY + MoveSpeed, oriZ, 1));
			}
			else if (keyData == Keys.S)
			{
				this.scene.UpdateCameraPos(new Vector4(oriX, oriY - MoveSpeed, oriZ, 1));
			}
			else if (keyData == Keys.A)
			{
				angleY += RotateSpeed;
				this.scene.UpdateModelRotationMatrix((float)angleX, (float)angleY,0);
			}
			else if (keyData == Keys.D)
			{
				angleY -= RotateSpeed;
				this.scene.UpdateModelRotationMatrix((float)angleX,(float)angleY,0);
			}
			this.Invalidate();
			return true;

		}

		//lighting
		private void checkbox1_CheckedChanged(object sender, EventArgs e)
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
