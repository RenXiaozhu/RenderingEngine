using System;
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

			// pos normal uv color
            Vertex[] vertices = new Vertex[24] {
                new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(0, -1, 0, 1), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),
                new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(-1, 0, 0, 1), new Vector4(1, 0, 0, 0), new Color4(255, 0, 0)),
                new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),

                new Vertex(new Vector4(1, -1, -1, 1), new Vector4(0, -1, 0, 1), new Vector4(1, 0, 0, 0), new Color4(0, 255, 0)),
                new Vertex(new Vector4(1, -1, -1, 1), new Vector4(1, 0, 0, 1),  new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),
                new Vertex(new Vector4(1, -1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(1, 0, 0, 0), new Color4(0, 255, 0)),

                new Vertex(new Vector4(1, 1, -1, 1), new Vector4(0, 1, 0, 1), new Vector4(1, 0, 0, 0), new Color4(0, 0, 255)),
                new Vertex(new Vector4(1, 1, -1, 1), new Vector4(1, 0, 0, 1), new Vector4(0, 1, 0, 0), new Color4(0, 0, 255)),
                new Vertex(new Vector4(1, 1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(1, 1, 0, 0), new Color4(0, 0, 255)),

                new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(0, 1, 0, 1), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),
                new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 0, 0, 1), new Vector4(1, 1, 0, 0), new Color4(255, 0, 0)),
                new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(0, 1, 0, 0), new Color4(255, 0, 0)),

                new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(0, -1, 0, 1), new Vector4(0, 1, 0, 0), new Color4(0, 255, 0)),
                new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),
                new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),

                new Vertex(new Vector4(1, -1, 1, 1), new Vector4(0, -1, 0, 1), new Vector4(1, 1, 0, 0), new Color4(0, 0, 255)),
                new Vertex(new Vector4(1, -1, 1, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 0, 0, 0), new Color4(0, 0, 255)),
                new Vertex(new Vector4(1, -1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(1, 0, 0, 0), new Color4(0, 0, 255)),

                new Vertex(new Vector4(1, 1, 1, 1), new Vector4(0, 1, 0, 1), new Vector4(1, 1, 0, 0), new Color4(0, 255, 0)),
                new Vertex(new Vector4(1, 1, 1, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 1, 0, 0), new Color4(0, 255, 0)),
                new Vertex(new Vector4(1, 1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(1, 1, 0, 0), new Color4(0, 255, 0)),

                new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(0, 1, 0, 1), new Vector4(0, 1, 0, 0), new Color4(255, 255, 0)),
                new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 1, 0, 0), new Color4(255, 255, 0)),
                new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(0, 1, 0, 0), new Color4(255, 255, 0)),
            };

            this.mesh.SetVertices(vertices);
        }

		public void InitLight()
		{
			this.light = new DirectionLight(new Vector4(-5, 5, 5, 1), new Color4(255, 255, 255));
			this.light.IsEnable = false;
		}

		public void UpdateCameraPitch(float pitch)
		{
			this.camera.Pitch = pitch;
			CATransform.InitMVPMatrix(this);
		}

		public void UpdateCameraYaw(float yaw)
		{
			this.camera.Yaw = yaw;
			CATransform.InitMVPMatrix(this);
		}

		public void UpdateCameraPos(Vector4 pos)
		{
            this.camera.Position = pos;
			CATransform.InitMVPMatrix(this);
		}

		public void UpdateCameraRotation(float degree)
		{
			this.camera.Yaw += degree;
			CATransform.InitMVPMatrix(this);
		}

		public void UpdateModelRotationMatrix(float degreeX, float degreeY, float degreeZ)
		{
			//mesh.rotation.VETransform3DRotation(degreeX,new Vector4(1,0,0,1));
			//mesh.rotation.VETransform3DRotation(degreeY,new Vector4(1,1,1,1));
			//mesh.rotation.VETransform3DRotation(degreeZ, new Vector4(0,0,1,1));
            mesh.rotation.SetRotate(degreeX, degreeY, degreeZ);
			CATransform.InitMVPMatrix(this);
		}

    }
}
