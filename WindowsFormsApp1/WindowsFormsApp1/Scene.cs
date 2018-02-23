using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Scene
    {
        // 灯光
        public DirectionLight light;
        //相机
        public Camera camera;
        // 网格模型
        public Mesh mesh;

        public Scene(int width, int height)
        {
            InitCamera(width, height);
            InitMesh();
        }

        void InitCamera(int width, int height)
        {
            this.camera = new Camera(new Vector4(0, 0, -20, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 1, 0, 1));
            this.camera.aspect = 16 / 9;
            this.camera.fov = Math.PI * 0.5;
            this.camera.ScreenWidth = width;
            this.camera.ScreenHeight = height;
        }

        void InitMesh()
        {
            this.mesh = new Mesh();

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
    }
}
