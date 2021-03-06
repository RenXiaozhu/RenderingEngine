﻿using System;
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
        private float degreeZ = 0;

        private float meshDegreeX = 0;
        private float meshDegreeY = 0;
        private float MoveSpeed = 0.5f;
        private const float RotateSpeed = 3f;
        private Point preE;
        private bool isCameraRotate = false;
        private Vector4 MeshDirection = new Vector4(0,0,0,0);

        public FormWindow()
        {
            InitializeComponent();
            InitSettings();
            InitScene();
        }

        private void ForLayout(object sender, LayoutEventArgs handle)
        {
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
            BitmapData bData = this.scene.worldMap.texture.LockBits();
            this.device.Clear(data);
            device.Render(scene, data);
            this.bmp.UnlockBits(data);
            this.scene.mesh.texture.UnlockBits(bmData);
            this.scene.worldMap.texture.UnlockBits(bData);
            g = pe.Graphics;
            g.DrawImage(this.bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
        }

        private void Form1_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                float dt = (Math.Abs(degreeX) / 90.0f)%2;

                float dtx = Math.Abs(e.X - pressDown.X);

                if(isCameraRotate)
                {
                    Console.WriteLine("CameraRotate");
          
                        degreeX = (e.Y - preE.Y);
                        degreeY = (e.X - preE.X);
                        Action.RotateCamera(degreeX, degreeY, degreeZ, scene ,isCameraRotate);
                }
                else
                {
                    Console.WriteLine("meshRotate");
						
						degreeX = (e.Y - preE.Y)  ;
						degreeY = (e.X - preE.X) ;
                        Action.RotateMesh(degreeX, degreeY, degreeZ, scene,false);
                }
				meshDegreeX += degreeX;
				meshDegreeY += degreeY;
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
            }
            else
            {
                MoveSpeed -= 0.5f;
                Action.TranslateCamera(0, 0,  0.5f, scene);
            }

            this.Invalidate();
        }

        private void FormWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pressDown = new Point(e.X, e.Y);
                preE = new Point(e.X, e.Y);
            }
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

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void lighting(object sender, EventArgs e)
        {
            if(DirectionLight.IsEnable == false)
            {
                DirectionLight.IsEnable = true;
            }
            else
            {
                DirectionLight.IsEnable = false;
            }

            this.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            float oriX = this.scene.camera.Position.X;
            float oriY = this.scene.camera.Position.Y;
            float oriZ = this.scene.camera.Position.Z;

            if(keyData == Keys.L)
            {
                if(DirectionLight.IsEnable)
                {
                    DirectionLight.IsEnable = false;
                }
                else
                {
                    DirectionLight.IsEnable = true;
                }
            }
            if(keyData == Keys.U)
            {
                if (DirectionLight.IsAmLightEnable)
                {
                    DirectionLight.IsAmLightEnable = false;
                }
                else
                {
                    DirectionLight.IsAmLightEnable = true;
                }
            }

            if(keyData == Keys.C)
            {
                if(isCameraRotate)
                {
                    isCameraRotate = false;
                }
                else
                {
                    isCameraRotate = true;
                }
            }

            if (keyData == Keys.W)
            {
                MeshDirection.Z += MoveSpeed;
                Action.Mesh_Move(MeshDirection.X, MeshDirection.Y, MeshDirection.Z, scene);
            }
            else if (keyData == Keys.S)
            {
                MeshDirection.Z -= MoveSpeed;
                Action.Mesh_Move(MeshDirection.X, MeshDirection.Y, MeshDirection.Z, scene);
            }
            else if (keyData == Keys.A)
            {
                MeshDirection.X -= MoveSpeed;
                Action.Mesh_Move(MeshDirection.X, MeshDirection.Y, MeshDirection.Z, scene);
            }
            else if (keyData == Keys.D)
            {
                MeshDirection.X += MoveSpeed;
                Action.Mesh_Move(MeshDirection.X, MeshDirection.Y, MeshDirection.Z, scene);
            }
            else if (keyData == Keys.Up)
            {
                MeshDirection.Y += MoveSpeed;
                Action.Mesh_Move(MeshDirection.X, MeshDirection.Y, MeshDirection.Z, scene);
            }
            else if (keyData == Keys.Down)
            {
                MeshDirection.Y -= MoveSpeed;
                Action.Mesh_Move(MeshDirection.X, MeshDirection.Y, MeshDirection.Z, scene);
            }

            this.Invalidate();
            return true;
        }

		private void AmBtn_right_Click(object sender, EventArgs e)
		{
			if (DirectionLight.IsAmLightEnable)
			{
				DirectionLight.IsAmLightEnable = false;
			}
			else
			{
				DirectionLight.IsAmLightEnable = true;
			}
			this.Invalidate();
		}

		private void CameraRotateBtn_Click(object sender, EventArgs e)
		{
			isCameraRotate = true;
			this.Invalidate();
		}

		private void MeshRotateBtn_right_Click(object sender, EventArgs e)
		{
			isCameraRotate = false;
			this.Invalidate();
		}

		private void UVBtn_Click(object sender, EventArgs e)
		{
			scene.renderState = Scene.RenderState.TextureMapping;
			this.Invalidate();
		}

		private void GouraudBtn_Click(object sender, EventArgs e)
		{
			scene.renderState = Scene.RenderState.GouraduShading;
			this.Invalidate();
		}

		private void WireFrameBtn_Click(object sender, EventArgs e)
		{
			scene.renderState = Scene.RenderState.WireFrame;
			this.Invalidate();
		}
	}
}
