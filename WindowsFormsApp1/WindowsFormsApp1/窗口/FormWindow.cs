using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace RenderingEngine
{
    public partial class FormWindow : Form
    {
        private Bitmap bmp;
        private Graphics g;
        private Device device;
        private Scene scene;
        private Rectangle rt;
        private PixelFormat pixelFormat;
        private Point pressDown;
        private Point pressUp;
        private float degreeX = 0;
        private float degreeY = 0;
        private float MoveSpeed = 0.5f;
        private const float RotateSpeed = 5f;
        private float angle = 0;
        private Point preE;
        private bool isUp = false;
        public FormWindow()
        {
            InitializeComponent();
            InitSettings();
            InitScene();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseWheel += new MouseEventHandler(Form1_OnMouseWheel);
            this.MouseMove += new MouseEventHandler(Form1_OnMouseMove);
        }

        private void InitScene()
        {
            bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Format24bppRgb);
            device = new Device(bmp);
            scene = new Scene(this.ClientSize.Width, this.ClientSize.Height);
            this.rt = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            this.pixelFormat = bmp.PixelFormat;
        }

        private void InitSettings()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs pe)
        {
            BitmapData data = this.bmp.LockBits(rt, ImageLockMode.ReadWrite, this.pixelFormat);
            BitmapData bmData = this.scene.mesh.texture.LockBits();
            this.device.Clear(data);
            device.Render(scene, data);
            this.bmp.UnlockBits(data);
            this.scene.mesh.texture.UnlockBits(bmData);
            g = pe.Graphics;
            g.DrawImage(this.bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
        }

        private void Form1_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if(Math.Abs( e.X- pressDown.X) > Math.Abs( e.Y - pressDown.Y))
                {
                    if(e.X - pressDown.X < 0)
                    {
                        if(e.X > preE.X)
                        {
                            degreeY += RotateSpeed;
                        }
                        else if(e.X < preE.X)
                        {
                            degreeY -= RotateSpeed;
                        }

                    }
                    else
                    {
                        if (e.X > preE.X)
                        {
                            degreeY += RotateSpeed;
                        }
                        else if (e.X < preE.X)
                        {
                            degreeY -= RotateSpeed;
                        }
                    }
                    Action.RotateMesh(degreeX, degreeY, 0, scene);
                }
                else
                {
                    if (e.Y - pressDown.Y < 0)
                    {
                        if (e.Y > preE.Y)
                        {
                            degreeX += RotateSpeed;
                        }
                        else if(e.Y < preE.Y)
                        {
                            degreeX -= RotateSpeed;
                        }
                    }
                    else
                    {
                        if (e.Y > preE.Y)
                        {
                            degreeX += RotateSpeed;
                        }
                        else if(e.Y < preE.Y)
                        {
                            degreeX -= RotateSpeed;
                        }

                    }
                    Action.RotateMesh(degreeX, degreeY, 0, scene);
                }


                preE.X = e.X;
                preE.Y = e.Y;
                                      
                this.Invalidate();
            }
        }

        private void Form1_OnMouseWheel(object sender, MouseEventArgs e)
        {
            float oriX = this.scene.camera.Position.X;
            float oriY = this.scene.camera.Position.Y;
            float oriZ = this.scene.camera.Position.Z;

            if (e.Delta < 0)
            {
                MoveSpeed += 0.5f;
               Action.TranslateCamera( 0, 0, -0.5f,scene);
                //MoveSpeed += 0.5f;
                //Action.UpdateCameraPitch((float)MoveSpeed,scene);
                //Action.UpdateCameraYaw(MoveSpeed, scene);
            }
            else
            {
                MoveSpeed -= 0.5f;
                //Action.UpdateCameraPitch((float)MoveSpeed, scene);
                //Action.UpdateCameraYaw(MoveSpeed,scene);
                Action.TranslateCamera(0, 0,  0.5f, scene);
            }

            this.Invalidate();
        }

        private void FormWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pressUp = new Point(e.X, e.Y);
                isUp = true;
            }
        }

        private void FormWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pressDown = new Point(e.X, e.Y);
                preE = new Point(e.X, e.Y);
                isUp = false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            float oriX = this.scene.camera.Position.X;
            float oriY = this.scene.camera.Position.Y;
            float oriZ = this.scene.camera.Position.Z;
            if (keyData == Keys.W)
            {
                Action.TranslateCamera(oriX, oriY + MoveSpeed, oriZ, scene);
            }
            else if (keyData == Keys.S)
            {
                Action.TranslateCamera(oriX, oriY - MoveSpeed, oriZ, scene);
            }
            else if (keyData == Keys.A)
            {
                angle += RotateSpeed;
                Action.RotateCamera(0,angle,0,scene);
            }
            else if (keyData == Keys.D)
            {
                angle -= RotateSpeed;
                Action.RotateCamera(0,angle,0,scene);
            }
            this.Invalidate();
            return true;
        }

        // Wire Frame
        private void WireFrame(object sender, EventArgs e)
        {
            this.scene.renderState = Scene.RenderState.WireFrame;
            this.Invalidate();
        }

        // Gouraud Shading
        private void gouraudShading(object sender, EventArgs e)
        {

            this.scene.renderState = Scene.RenderState.GouraduShading;
            this.Invalidate();
        }

        // Texture Mapping
        private void textureMapping(object sender, EventArgs e)
        {

            this.scene.renderState = Scene.RenderState.TextureMapping;

            this.Invalidate();

        }

        // Lighting
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //this.textureMappingToolStripMenuItem.Visible = checkBox1.Checked;
            //this.hScrollBar1.Value = (int)(DirectionalLight.STARTKD * this.hScrollBar1.Maximum);
            //this.scene.light.IsEnable = checkBox1.Checked;
            //this.Invalidate();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //float ratio = (float)this.hScrollBar1.Value / (float)this.hScrollBar1.Maximum;
            //this.scene.light.Kd = ratio;
           
            //this.Invalidate();
        }

        private void Blank(object sender, EventArgs e)
        {
            ToolStripDropDownItem item = (ToolStripDropDownItem)sender;
            if(item.Text == "Blank")
            {
                item.Text = "cancel blank";
                this.device.isShowBackFace = false;
            }
            else
            {
                item.Text = "Blank";
                this.device.isShowBackFace = true;
            }
            this.Invalidate();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rotate(object sender, EventArgs e)
        {
      
            degreeY -= RotateSpeed;
           
            Action.RotateMesh(degreeX, degreeY, 0, scene);

            this.Invalidate();
        }

        private void rotate_right(object sender, EventArgs e)
        {
            degreeY += RotateSpeed;

            Action.RotateMesh(degreeX, degreeY, 0, scene);

            this.Invalidate();
        }

        private void tranlate(object sender, EventArgs e)
        {
            MoveSpeed += 0.5f;
            Action.TranslateCamera(0,0,0.5f,scene);
            this.Invalidate();
        }

        private void translate_right(object sender, EventArgs e)
        {
            MoveSpeed -= 0.5f;
            Action.TranslateCamera(0, 0, -0.5f, scene);
            this.Invalidate();
        }

        private void lighting(object sender, EventArgs e)
        {
            scene.light.IsEnable = true;
            this.Invalidate();
        }
    }
}
