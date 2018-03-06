﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Scene
    {
		public enum RenderState
		{
			WireFrame = 0,
			GouraduShading,
			TextureMapping
		}
		public RenderState renderState;
        // 灯光
        public DirectionLight light;
        //相机
        public Camera camera;
        // 网格模型
        public Mesh mesh;


        public Scene(int width, int height)
        {
            this.renderState = RenderState.WireFrame;
            InitCamera(width, height);
            InitMesh();
			InitLight();
			CATransform.InitMVPMatrix(this);
        }

        void InitCamera(int width, int height)
        {
            this.camera = new Camera((float)Math.PI * 0.3f, (float)width / (float)height, 1.0f, 50.0f);
            this.camera.Position = new Vector4(0, 0, -5, 1);
            this.camera.Target = new Vector4(0, 0, 0, 1);
            this.camera.Up = new Vector4(0, 1, 0, 1);
            this.camera.Pitch = 0;
            this.camera.Yaw = 0;
        }

        void InitMesh()
        {
            this.mesh = new Mesh("Cube");
        }

        public void InitLight()
        {
            this.light = new DirectionLight(new Vector4( 0 , 5, 0, 1), new Color4(255, 255, 0));
            this.light.IsEnable = true;
        }

    }
}
