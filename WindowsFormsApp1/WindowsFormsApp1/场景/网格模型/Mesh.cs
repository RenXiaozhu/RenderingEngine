using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Mesh
    {
        public string Name { get; set; }
        public Vertex[] Vertices { get; private set; }
        public Triangle[] triangles { get; set; }
        public Face[] faces { get; set; }
        public Texture texture;
        public Matrix4x4 rotation { get; set; }

        public Mesh(string name)
        {
            Name = name;
            Load();
        }


        public void Rotate(float degreeX, float degreeY, float degreeZ)
        {
            

            rotation.SetRotate(degreeX, degreeY, degreeZ);
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertex vt = Vertices[i];
                vt.nowNormal = rotation.ApplY(vt.Normal);
                vt.nowNormal.Normalize();
                vt.nowPos = rotation.ApplY(vt.Position);
                //vt.Position = rotation.ApplY(vt.Position);
                //vt.UV = rotation.ApplY(vt.UV);
                //Console.WriteLine(vt.ClipSpacePosition);
            }
        }

        public void Load()
        {
            MakeTriangles();
            GetVertices();
            this.texture = new Texture(@"/Users/wangao/Documents/workSpace/RenderingEngine/WindowsFormsApp1/WindowsFormsApp1/textures/texture.jpg", 256, 256);//706, 530);
            this.rotation = new Matrix4x4();
        }

        public void SetVertices(Vertex[] vertices)
        {
            this.Vertices = vertices;
            this.MakeTriangles();
        }

        public void MakeTriangles()
        {
            triangles = new Triangle[] {
                // 正面
                new Triangle(1, 0, 2),
                new Triangle(3, 2, 0),
                // 右面
                new Triangle(2, 3, 6),
                new Triangle(7, 6, 3),
                // 左面
                new Triangle(4, 0, 5),
                new Triangle(1, 5, 0),
                // 背面
                new Triangle(6, 7, 5),
                new Triangle(4, 5, 7),
                // 上面
                new Triangle(5, 1, 6),
                new Triangle(2, 6, 1),
                // 下面
                new Triangle(7, 3, 4),
                new Triangle(0, 4, 3)
                //// 正面
                //new Triangle(2, 5, 8),
                //new Triangle(2, 8, 11),
                //// 右面
                //new Triangle(4, 16, 7),
                //new Triangle(16, 19, 7),
                //// 左面
                //new Triangle(13, 1, 10),
                //new Triangle(13, 10, 22),
                //// 背面
                //new Triangle(17, 14, 23),
                //new Triangle(17, 23, 20),
                //// 上面
                //new Triangle(9, 6, 18),
                //new Triangle(9, 18, 21),
                //// 下面
                //new Triangle(12, 15, 3),
                //new Triangle(12, 3, 0)
            };

            faces = new Face[]{
                new Face(triangles[0],triangles[1],Face.FaceType.Front),
                new Face(triangles[2],triangles[3],Face.FaceType.Right),
                new Face(triangles[4],triangles[5],Face.FaceType.Left),
                new Face(triangles[6],triangles[7],Face.FaceType.Behind),
                new Face(triangles[8],triangles[9],Face.FaceType.Up),
                new Face(triangles[10],triangles[11],Face.FaceType.Below)
            };
        }

        private void GetVertices()
        {
            
            Vertices = new Vertex[8] {
                
                new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(1, 0, 0, 0), new Color4(0, 0, 0)),
                //new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(-1, 0, 0, 1), new Vector4(1, 0, 0, 0), new Color4(255, 255, 255)),
                //new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(0, 0, 0, 0), new Color4(255, 255, 255)),

                new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 0, 0, 0), new Color4( 0, 255, 0)),
                //new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 0, 0, 1), new Vector4(1, 1, 0, 0), new Color4( 0, 255, 0)),
                //new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(0, 1, 0, 0), new Color4( 0, 255, 0)),

                new Vertex(new Vector4(1, 1, -1, 1), new Vector4(0, 1, 0, 1), new Vector4(0, 1, 0, 0), new Color4( 255, 255, 0)),
                //new Vertex(new Vector4(1, 1, -1, 1), new Vector4(1, 0, 0, 1), new Vector4(0, 1, 0, 0), new Color4(255, 255, 0)),
                //new Vertex(new Vector4(1, 1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(1, 1, 0, 0), new Color4(255, 255, 0)),

                new Vertex(new Vector4(1, -1, -1, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 1, 0, 0), new Color4(255, 0, 0)),
                //new Vertex(new Vector4(1, -1, -1, 1), new Vector4(1, 0, 0, 1),  new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),
                //new Vertex(new Vector4(1, -1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(1, 0, 0, 0), new Color4(255, 0, 0)),

                new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(1, 1, 0, 0), new Color4( 0, 0, 255)),
                //new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),
                //new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),

                new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(0, 1, 0, 1), new Vector4(0, 1, 0, 0), new Color4(0, 255, 255)),
                //new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 1, 0, 0), new Color4(40, 55, 255)),
                //new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(0, 1, 0, 0), new Color4(40, 55, 255)),

                new Vertex(new Vector4(1, 1, 1, 1), new Vector4( 1, 0, 0, 1), new Vector4( 0, 0, 0, 0), new Color4(255, 255, 255)),
                //new Vertex(new Vector4(1, 1, 1, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 1, 0, 0), new Color4(255, 100, 100)),
                //new Vertex(new Vector4(1, 1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(1, 1, 0, 0), new Color4(255, 100, 100)),

                new Vertex(new Vector4(1, -1, 1, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 0, 0, 0), new Color4(255, 0, 255))
                //new Vertex(new Vector4(1, -1, 1, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 0, 0, 0), new Color4(255, 0, 255)),
                //new Vertex(new Vector4(1, -1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(1, 0, 0, 0), new Color4(255, 0, 255)),

            };
        }
    }
}
