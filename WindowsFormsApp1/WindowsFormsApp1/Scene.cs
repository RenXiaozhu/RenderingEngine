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
        }

        void InitCamera(int width, int height)
        {
            this.camera = new Camera(new Vector4(0, 0, -100, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 1, 0, 1));
            this.camera.aspect = 712/400.0f;
			this.camera.ViewDistance = 20.0f;
            this.camera.fov = Math.PI * 0.5;
            this.camera.ScreenWidth = width;
            this.camera.ScreenHeight = height;
			this.camera.ViewPlaneWidth = 712.0f;
			this.camera.ViewPlaneHeight = 400.0f;
			this.camera.zn = 50.0f;
			this.camera.zf = 300.0f;

        }

        void InitMesh()
        {
            this.mesh = new Mesh("Cube");

            Vertex[] vertices = new Vertex[8]{
                new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0) ),
                //new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0) ),
                //new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0) ),

                new Vertex(new Vector4(1, -1, -1, 1) , new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),
                //new Vertex(new Vector4(1, -1, -1, 1) , new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),
                //new Vertex(new Vector4(1, -1, -1, 1) , new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),

                new Vertex(new Vector4(1, 1, -1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),
                //new Vertex(new Vector4(1, 1, -1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),
                //new Vertex(new Vector4(1, 1, -1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),

                //new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),
                //new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),
                new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),

                new Vertex(new Vector4(1, 1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),
                //new Vertex(new Vector4(1, 1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),
                //new Vertex(new Vector4(1, 1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),

                new Vertex(new Vector4(1, -1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),
                //new Vertex(new Vector4(1, -1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),
                //new Vertex(new Vector4(1, -1, 1, 1),  new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),

                new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),
                //new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),
                //new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),

                new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),
                //new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0)),
                //new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 0, 0, 0), new Vector4(0, 0, 0, 0), new Color4(0, 255, 0))
            };

            this.mesh.vertices = vertices;
        }

		public void InitLight()
		{
			this.light = new DirectionLight(new Vector4(-5, 5, 5, 1), new Color4(255, 255, 255));
			this.light.IsEnable = false;
		}

		public void UpdateCameraPos(Vector4 pos)
		{
			this.camera.self_position = pos;
		}

		public void UpdateCameraRotation(float degree)
		{
			this.camera.Yaw += degree;
		}

		public void UpdateModelRotationMatrix( float degreeY)
		{
			mesh.rotation.VETransform3DRotation( degreeY,SystemCross.SystemCross_Y,true);
		}

		public VETransform3D MvpMatrix()
		{
			VETransform3D translate = new VETransform3D();
			translate.VETransform3DTranslation(0, 0, 0);

			VETransform3D scale = new VETransform3D();
			scale.VETransform3DScale(1, 1, 1);

			VETransform3D rotation = mesh.rotation;
			VETransform3D model = scale * rotation * translate;
			VETransform3D view = this.camera.FPSView();
			VETransform3D projection = this.camera.Perspective();
			return model * view * projection;
		}
    }
}
