using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace RenderingEngine
{
    public class Triangle
    {
        public int a, b, c; //顶点索引

        public Triangle(int A,int B, int C)
        {
            this.a = A;
            this.b = B;
            this.c = C;
        }

        /*
        public bool IsPointInTriangle(Point point)
        {
            Vector4 P = new Vector4(point.X, point.Y, 0, 0);
            return SameSide(A, B, C, P) && SameSide(B, C, A, P) && SameSide(C, A, B, P) ;
        }

        public bool SameSide(Vector4 A, Vector4 B, Vector4 C, Vector4 P)
        {
            Vector4 AB = B - A;
            Vector4 AC = C - A;
            Vector4 AP = P - A;
            //求出垂直于当前平面的向量
            Vector4 v1 = AB.CrossMultiply(AC);
            Vector4 v2 = AB.CrossMultiply( AP);
            // 判断这两个平面法向量的夹角: 0  两个向量垂直 (90度);  >0 : 形成的是锐角  ;  <0 :夹角大于90度
            return v1.dot(v2) >= 0; 
        }
        */
    }

    class TriangleModel
    {
        public Vertex[] Vertices { get; set; }
        public float weight1;
        public float weight2;

        public TriangleModel(Vertex A, Vertex B, Vertex C)
        {
            this.Vertices = new Vertex[] { A, B, C };
        }

        public bool IsPointInTriangle(Point point)
        {
            Vector4 P = new Vector4(point.X, point.Y, 0, 0);
            return SameSide(Vertices[0].Position, Vertices[1].Position, Vertices[2].Position, P) && SameSide(Vertices[1].Position, Vertices[2].Position, Vertices[0].Position, P) && SameSide(Vertices[2].Position, Vertices[0].Position, Vertices[1].Position, P);
        }

        public bool SameSide(Vector4 A, Vector4 B, Vector4 C, Vector4 P)
        {
            Vector4 AB = B - A;
            Vector4 AC = C - A;
            Vector4 AP = P - A;
            //求出垂直于当前平面的向量
            Vector4 v1 = AB.CrossMultiply(AC);
            Vector4 v2 = AB.CrossMultiply(AP);
            // 判断这两个平面法向量的夹角: 0  两个向量垂直 (90度);  >0 : 形成的是锐角  ;  <0 :夹角大于90度
            return v1.dot(v2) >= 0;
        }
    }
}
