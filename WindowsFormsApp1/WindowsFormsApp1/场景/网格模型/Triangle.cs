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

    public class TriangleModel
    {
        public Vertex[] Vertices { get; set; }
        public double weight1;
        public double weight2;

		//三角形重心插值的二元一次方程组的系数
		private int a, b, c, d, dn1, dn2;
		private double u1, v1;
		private double u2, v2;
		private double u3, v3;
		private double w1, w2, w3;

        public TriangleModel(Vertex A, Vertex B, Vertex C)
        {
            this.Vertices = new Vertex[] { A, B, C };
        }

		public void PreCalculateWeight()
		{
			Vector4 p1 = this.Vertices[0].ScreenSpacePosition;
			Vector4 p2 = this.Vertices[1].ScreenSpacePosition;
			Vector4 p3 = this.Vertices[2].ScreenSpacePosition;

            a = (int)(p2.X - p1.X);
            b = (int)(p3.X - p1.X);
            c = (int)(p2.Y - p1.Y);
            d = (int)(p3.Y - p1.Y);
			dn1 = (b * c - a * d);
			dn2 = (a * d - b * c);

            u1 = Vertices[0].UV.X / Vertices[0].ClipSpacePosition.W;
            u2 = Vertices[1].UV.X / Vertices[1].ClipSpacePosition.W;
            u3 = Vertices[2].UV.X / Vertices[2].ClipSpacePosition.W;
            v1 = Vertices[0].UV.Y / Vertices[0].ClipSpacePosition.W;
            v2 = Vertices[1].UV.Y / Vertices[1].ClipSpacePosition.W;
            v3 = Vertices[2].UV.Y / Vertices[2].ClipSpacePosition.W;
            w1 = 1f / Vertices[0].ClipSpacePosition.W;
            w2 = 1f / Vertices[1].ClipSpacePosition.W;
            w3 = 1f / Vertices[2].ClipSpacePosition.W;

		}

		public void CalWeight(Vector4 p)
		{
			Vector4 p1 = this.Vertices[0].ScreenSpacePosition;
            int m = (int)(p.X - p1.X);
            int n = (int)(p.Y - p1.Y);
			this.weight1 = (b * n - d * m)/dn1;
			this.weight2 = (a * n - c * m) / dn2;
		}

		public double GetInterValue(double a, double b, double c)
		{
			return ((1 - weight1 - weight2) * a + weight1 * b + weight2 * c);
		}

		public Vector4 GetInterUV()
		{
			double u = GetInterValue(u1, u2, u3);
			double v = GetInterValue(v1, v2, v3);
			double w = GetInterValue(w1, w2, w3);
            return new Vector4((float)u /(float) w, (float)v /(float) w, 0, 0);
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
            Vector4 v1 = Vector4.Cross(AB,AC);
            Vector4 v2 = Vector4.Cross(AB,AP);
            // 判断这两个平面法向量的夹角: 0  两个向量垂直 (90度);  >0 : 形成的是锐角  ;  <0 :夹角大于90度
            return Vector4.Dot(v1,v2) >= 0;
        }
    }

    public struct VertexTriangle
    {
        //public Vertex VertexA { get; set; }
        //public Vertex VertexB { get; set; }
        //public Vertex VertexC { get; set; }
        public Vertex[] Vertices { get; set; }
        public float Weight1 { get; set; }
        public float Weight2 { get; set; }

        // 做三角形重心插值时的二元一次方程组系数
        // P.x = (1 - u - v) * P1.x + u * P2.x + v * P3.x
        // P.y = (1 - u - v) * P1.y + u * P2.y + v * P3.y
        private int a, b, c, d, dn1, dn2;
        private float u1, v1;
        private float u2, v2;
        private float u3, v3;
        private float w1, w2, w3;

        public VertexTriangle(Vertex a, Vertex b, Vertex c) : this()
        {
            this.Vertices = new Vertex[] { a, b, c };
        }

        public void PreCalWeight()
        {
            Vector4 p1 = this.Vertices[0].ScreenSpacePosition;
            Vector4 p2 = this.Vertices[1].ScreenSpacePosition;
            Vector4 p3 = this.Vertices[2].ScreenSpacePosition;

            a = (int)(p2.X - p1.X);
            b = (int)(p3.X - p1.X);
            c = (int)(p2.Y - p1.Y);
            d = (int)(p3.Y - p1.Y);
            dn1 = (b * c - a * d);
            dn2 = (a * d - b * c);

            u1 = Vertices[0].UV.X / Vertices[0].ClipSpacePosition.W;
            u2 = Vertices[1].UV.X / Vertices[1].ClipSpacePosition.W;
            u3 = Vertices[2].UV.X / Vertices[2].ClipSpacePosition.W;
            v1 = Vertices[0].UV.Y / Vertices[0].ClipSpacePosition.W;
            v2 = Vertices[1].UV.Y / Vertices[1].ClipSpacePosition.W;
            v3 = Vertices[2].UV.Y / Vertices[2].ClipSpacePosition.W;
            w1 = 1f / Vertices[0].ClipSpacePosition.W;
            w2 = 1f / Vertices[1].ClipSpacePosition.W;
            w3 = 1f / Vertices[2].ClipSpacePosition.W;
        }

        // 插值之前必须先算权重
        public void CalWeight(Vector4 p)
        {
            Vector4 p1 = this.Vertices[0].ScreenSpacePosition;
            int m = (int)(p.X - p1.X);
            int n = (int)(p.Y - p1.Y);
            this.Weight1 = (float)(b * n - d * m) / (float)dn1;
            this.Weight2 = (float)(a * n - c * m) / (float)dn2;
        }

        public float GetInterValue(float a, float b, float c)
        {
            return ((1 - Weight1 - Weight2) * a + Weight1 * b + Weight2 * c);
        }

        public Vector4 GetInterUV()
        {
            float u = GetInterValue(u1, u2, u3);
            float v = GetInterValue(v1, v2, v3);
            float w = GetInterValue(w1, w2, w3);
            return new Vector4(u / w, v / w, 0, 0);
        }
    }
}
