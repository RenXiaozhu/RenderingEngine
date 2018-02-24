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

			a = (int)(p2.x - p1.x);
			b = (int)(p3.x - p1.x);
			c = (int)(p2.y - p1.y);
			d = (int)(p3.y - p1.y);
			dn1 = (b * c - a * d);
			dn2 = (a * d - b * c);

			u1 = Vertices[0].UV.x / Vertices[0].ClipSpacePosition.h;
			u2 = Vertices[1].UV.x / Vertices[1].ClipSpacePosition.h;
			u3 = Vertices[2].UV.x / Vertices[2].ClipSpacePosition.h;
			v1 = Vertices[0].UV.y / Vertices[0].ClipSpacePosition.h;
			v2 = Vertices[1].UV.y / Vertices[1].ClipSpacePosition.h;
			v3 = Vertices[2].UV.y / Vertices[2].ClipSpacePosition.h;
			w1 = 1f / Vertices[0].ClipSpacePosition.h;
			w2 = 1f / Vertices[1].ClipSpacePosition.h;
			w3 = 1f / Vertices[2].ClipSpacePosition.h;

		}

		public void CalWeight(Vector4 p)
		{
			Vector4 p1 = this.Vertices[0].ScreenSpacePosition;
			int m = (int)(p.x - p1.x);
			int n = (int)(p.y - p1.y);
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
			return new Vector4(u / w, v / w, 0, 0);
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
