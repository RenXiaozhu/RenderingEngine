using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Mesh:MeshBase
    {
        public Mesh(string name ,string textureFileName )
        {
            Name = name;
            TextureFileName = textureFileName;
            Position = new Vector4(0, 0, 0, 0);
            PreLoad();
        }

        public void PreLoad()
        {
            GetVertices();
            MakeTriangles();
            this.texture = new Texture(TextureFileName, 256, 256);//706, 530);
            this.rotation = new Matrix4x4();
            this.Move = new Matrix4x4();
        }

        public virtual void SetVertices(Vertex[] vertices)
        {
            this.Vertices = vertices;
        }

        public void Action_Rotate(float degreeX, float degreeY, float degreeZ)
        {
            rotation.SetRotate(degreeX, degreeY, degreeZ);
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertex vt = Vertices[i];
               vt.nowNormal = rotation.ApplY(vt.nowNormal);
               vt.nowNormal.Normalize();
                //vt.nowPos = rotation.ApplY(vt.nowPos);
            }
        }

        public void Action_Move(float x, float y, float z)
        {
            Move.SetTranslate(x, y, z);
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertex vt = Vertices[i];
                //vt.nowNormal = rotation.ApplY(vt.Normal);
                //vt.nowNormal.Normalize();
                vt.nowPos = Move.ApplY(vt.nowPos);
            }
        }

        public Matrix4x4 Action()
        {
            return Move * rotation;
        }

		public  void MakeTriangles()
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

        public void GetVertices()
        {
            //base.GetVertices();
            Vertices = new Vertex[8] {
                
                new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(-1, -1, -1, 1), new Vector4(1, 0, 0, 0), new Color4(0, 0, 0)),
       
                new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 1, -1, 1), new Vector4(0, 0, 0, 0), new Color4( 0, 255, 0)),
                //new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 0, 0, 1), new Vector4(1, 1, 0, 0), new Color4( 0, 255, 0)),
                //new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(0, 1, 0, 0), new Color4( 0, 255, 0)),

                new Vertex(new Vector4(1, 1, -1, 1), new Vector4(1, 1, -1, 1), new Vector4(0, 1, 0, 0), new Color4( 255, 255, 0)),
                //new Vertex(new Vector4(1, 1, -1, 1), new Vector4(1, 0, 0, 1), new Vector4(0, 1, 0, 0), new Color4(255, 255, 0)),
                //new Vertex(new Vector4(1, 1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(1, 1, 0, 0), new Color4(255, 255, 0)),

                new Vertex(new Vector4(1, -1, -1, 1), new Vector4(1, -1, -1, 1), new Vector4(1, 1, 0, 0), new Color4(255, 0, 0)),
                //new Vertex(new Vector4(1, -1, -1, 1), new Vector4(1, 0, 0, 1),  new Vector4(0, 0, 0, 0), new Color4(255, 0, 0)),
                //new Vertex(new Vector4(1, -1, -1, 1), new Vector4(0, 0, -1, 1), new Vector4(1, 0, 0, 0), new Color4(255, 0, 0)),

                new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, -1, 1, 1), new Vector4(1, 1, 0, 0), new Color4(0, 0, 255)),
                //new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),
                //new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(0, 0, 0, 0), new Color4(0, 0, 255)),

                new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 1, 1, 1), new Vector4(0, 1, 0, 0), new Color4(0, 255, 255)),
                //new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 0, 0, 1), new Vector4(0, 1, 0, 0), new Color4(40, 55, 255)),
                //new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(0, 1, 0, 0), new Color4(40, 55, 255)),

                new Vertex(new Vector4(1, 1, 1, 1), new Vector4( 1, 1, 1, 1), new Vector4( 0, 0, 0, 0), new Color4(255, 255, 255)),
                //new Vertex(new Vector4(1, 1, 1, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 1, 0, 0), new Color4(255, 100, 100)),
                //new Vertex(new Vector4(1, 1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(1, 1, 0, 0), new Color4(255, 100, 100)),

                new Vertex(new Vector4(1, -1, 1, 1), new Vector4(1, -1, 1, 1), new Vector4(1, 0, 0, 0), new Color4(255, 0, 255))
                //new Vertex(new Vector4(1, -1, 1, 1), new Vector4(1, 0, 0, 1), new Vector4(1, 0, 0, 0), new Color4(255, 0, 255)),
                //new Vertex(new Vector4(1, -1, 1, 1), new Vector4(0, 0, 1, 1), new Vector4(1, 0, 0, 0), new Color4(255, 0, 255)),

            };
        }

        public void PrePareUV(Face face)
        {
            Vertex vertexA;
            Vertex vertexB;
            Vertex vertexC;
            switch (face.type)
            {
                case Face.FaceType.Front:
                    {
                        Triangle t1 = face.t_1;
                        vertexA = Vertices[t1.a];
                        vertexA.UV = new Vector4(0, 0, 0, 0);

                        vertexB = Vertices[t1.b];
                        vertexB.UV = new Vector4(1, 0, 0, 0);

                        vertexC = Vertices[t1.c];
                        vertexC.UV = new Vector4(0, 1, 0, 0);



                        Triangle t2 = face.t_2;
                        vertexA = Vertices[t2.a];
                        vertexA.UV = new Vector4(1, 1, 0, 0);

                        vertexB = Vertices[t2.b];
                        vertexB.UV = new Vector4(0, 1, 0, 0);

                        vertexC = Vertices[t2.c];
                        vertexC.UV = new Vector4(1, 0, 0, 0);

                    }
                    break;
                case Face.FaceType.Left:
                    {
                        Triangle t1 = face.t_1;
                        vertexA = Vertices[t1.a];
                        vertexA.UV = new Vector4(0, 0, 0, 0);

                        vertexB = Vertices[t1.b];
                        vertexB.UV = new Vector4(1, 0, 0, 0);

                        vertexC = Vertices[t1.c];
                        vertexC.UV = new Vector4(0, 1, 0, 0);

                        Triangle t2 = face.t_2;
                        vertexA = Vertices[t2.a];
                        vertexA.UV = new Vector4(1, 1, 0, 0);

                        vertexB = Vertices[t2.b];
                        vertexB.UV = new Vector4(0, 1, 0, 0);

                        vertexC = Vertices[t2.c];
                        vertexC.UV = new Vector4(1, 0, 0, 0);
                    }
                    break;
                case Face.FaceType.Right:
                    {
                        Triangle t1 = face.t_1;
                        vertexA = Vertices[t1.a];
                        vertexA.UV = new Vector4(0, 0, 0, 0);

                        vertexB = Vertices[t1.b];
                        vertexB.UV = new Vector4(1, 0, 0, 0);

                        vertexC = Vertices[t1.c];
                        vertexC.UV = new Vector4(0, 1, 0, 0);

                        Triangle t2 = face.t_2;
                        vertexA = Vertices[t2.a];
                        vertexA.UV = new Vector4(1, 1, 0, 0);

                        vertexB = Vertices[t2.b];
                        vertexB.UV = new Vector4(0, 1, 0, 0);

                        vertexC = Vertices[t2.c];
                        vertexC.UV = new Vector4(1, 0, 0, 0);

                    }
                    break;
                case Face.FaceType.Up:
                    {
                        Triangle t1 = face.t_1;
                        vertexA = Vertices[t1.a];
                        vertexA.UV = new Vector4(0, 0, 0, 0);

                        vertexB = Vertices[t1.b];
                        vertexB.UV = new Vector4(1, 0, 0, 0);

                        vertexC = Vertices[t1.c];
                        vertexC.UV = new Vector4(0, 1, 0, 0);


                        Triangle t2 = face.t_2;
                        vertexA = Vertices[t2.a];
                        vertexA.UV = new Vector4(1, 1, 0, 0);

                        vertexB = Vertices[t2.b];
                        vertexB.UV = new Vector4(0, 1, 0, 0);

                        vertexC = Vertices[t2.c];
                        vertexC.UV = new Vector4(1, 0, 0, 0);

                    }
                    break;
                case Face.FaceType.Behind:
                    {
                        Triangle t1 = face.t_1;
                        vertexA = Vertices[t1.a];
                        vertexA.UV = new Vector4(0, 0, 0, 0);

                        vertexB = Vertices[t1.b];
                        vertexB.UV = new Vector4(1, 0, 0, 0);

                        vertexC = Vertices[t1.c];
                        vertexC.UV = new Vector4(0, 1, 0, 0);

                        Triangle t2 = face.t_2;
                        vertexA = Vertices[t2.a];
                        vertexA.UV = new Vector4(1, 1, 0, 0);

                        vertexB = Vertices[t2.b];
                        vertexB.UV = new Vector4(0, 1, 0, 0);

                        vertexC = Vertices[t2.c];
                        vertexC.UV = new Vector4(1, 0, 0, 0);

                    }
                    break;
                case Face.FaceType.Below:
                    {
                        Triangle t1 = face.t_1;
                        vertexA = Vertices[t1.a];
                        vertexA.UV = new Vector4(0, 0, 0, 0);

                        vertexB = Vertices[t1.b];
                        vertexB.UV = new Vector4(1, 0, 0, 0);

                        vertexC = Vertices[t1.c];
                        vertexC.UV = new Vector4(0, 1, 0, 0);

                        Triangle t2 = face.t_2;
                        vertexA = Vertices[t2.a];
                        vertexA.UV = new Vector4(1, 1, 0, 0);

                        vertexB = Vertices[t2.b];
                        vertexB.UV = new Vector4(0, 1, 0, 0);

                        vertexC = Vertices[t2.c];
                        vertexC.UV = new Vector4(1, 0, 0, 0);

                    }
                    break;
            }
        }
    }
}
