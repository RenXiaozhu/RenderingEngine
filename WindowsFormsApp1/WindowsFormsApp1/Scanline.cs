using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RenderingEngine
{
    class Scanline
    {
        private Device device;
        private Edge[] ET;  // Edge Table 边的分类表
        private Edge AEL; //活化边表
        private int height;
        private int width;
        private Color4 finalColor;
        private List<Triangle> angleList;
        public Color4 backgroundColor;
        public Color4 forwardColor;

        public Scanline(Device device)
        {
            this.device = device;
            this.width = device.width;
            this.height = device.height;
            backgroundColor = new Color4(255, 255, 255);
            forwardColor = new Color4(255, 0, 0);
            finalColor = new Color4(222, 128, 50);
            this.angleList = new List<Triangle>();

            this.ET = new Edge[this.height];
            for (int i = 0; i < this.height; i++)
            {
                this.ET[i] = new Edge();
            }
        }



        void InsertEdge(ref Edge root, Edge e)
        {
            Edge newEdge = (Edge)e.Clone();

        }



        public void StartScan(Scene scene)
        {
            // 模型坐标系转换
            CATransform.ModelTransformToWindow(scene);


            for (int i = 0; i < scene.mesh.triangles.Length; i+=1)
            {
                Vertex[] tmp = scene.mesh.TmpVertices;
                Triangle triangle = scene.mesh.triangles[i];
                TriangleModel model = new TriangleModel(tmp[triangle.a], tmp[triangle.b], tmp[triangle.c]);

                //this.device.PutPixel((int)vt.Position.x, (int)vt.Position.y, vt.Color);
               

                    //this.device.DrawMidPointLine(new Vector2(vt.Position.x, vt.Position.y), new Vector2(next.Position.x, next.Position.y), vt.Color);
                    //this.device.DrawDDALine(new Vector2(vt.Position.x, vt.Position.y), new Vector2(next.Position.x, next.Position.y), vt.Color);
                    this.device.DrawTriangles(model);
            }
        }

        
        

        public void addTriangle(Triangle triangle)
        {
            this.angleList.Add(triangle);
        }
    }
}
