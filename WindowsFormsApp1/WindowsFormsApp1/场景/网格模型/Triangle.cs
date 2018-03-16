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

        public Face.FaceType type;

        public int index;


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

    public struct VertexTriangle
    {
        public Face.FaceType type;
        public int index;
        //public Vertex VertexA { get; set; }
        //public Vertex VertexB { get; set; }
        //public Vertex VertexC { get; set; }
        public Vertex[] Vertices { get; set; }
        public float Weight1 { get; set; }
        public float Weight2 { get; set; }
        Color4 prePointColor { get; set; }
        // 做三角形重心插值时的二元一次方程组系数
        // P.x = (1 - u - v) * P1.x + u * P2.x + v * P3.x
        // P.y = (1 - u - v) * P1.y + u * P2.y + v * P3.y
        private int a, b, c, d, e, f,  dn1, dn2;
        private float den;
       
        // 重心坐标
        private float b1, b2, b3;
        private float u1, v1;
        private float u2, v2;
        private float u3, v3;
        private float w1, w2, w3;
        private Vector4 nowpos;
        private float area;

        private float p;// 半周长
        private float l1, l2, l3;// 边长

        public VertexTriangle(Vertex a, Vertex b, Vertex c) : this()
        {
            this.Vertices = new Vertex[] { a, b, c };
        }

        /*
         * p[u,v] = a*p1 + b*p2 + c*p3
         * a + b + c = 1; 其意义是三角形的三个顶点对该点的 贡献 或 权值
         * 
         * 1. 三角形的三个顶点的重心坐标都是单位向量 
         *      v2
         *      /\
         *     /  \
         *    /____\
         *   v1     v3
         *   v1 (1,0,0)
         *   v2 (0,1,0)
         *   v3 (0,0,1)
         *  2.  在某顶点的相对边上的所有点的重心坐标分量为0
            3.  不只是三角形范围内的点，改平面上的所有点都能用重心坐标描述
                三角形内的点的坐标范围在 
                [0,1] 之间变化
                三角形外的点至少有一个负坐标，重心坐标用和原三角形大小相同的块嵌满整个平面

             重心坐标空间的本质不同于笛卡尔坐标系
             因为重心坐标空间是2D的，但是用了3个坐标，又因为坐标和等于 1 ，所以重心坐标只有两个自由度，有一个分量是冗余的
             也就是说重心坐标空间仅用两个数就能完全的描述一个点，用这两个数可以计算出第三个

             由 Px = b1 * x1 + b2 * x2 + b3 * x3;
                py = b1 * y1 + b2 * y2 + b3 * y3;
                b1+b2+b3 = 1;
             解得
             
            b1 = ((Py - y3)*(x2 - x3) + (y2 - y3)*(x3 - Py))/((y1 - y3)*(x2 - x3) + (y2 - y3)*(x3 - x1)) 
            
            b2 = ((Py - y1)*(x3 - x1) + (y3 - y1)*(x1 - Px))/((y1- y3)*(x2 - x3) + (y2 - y3)*(x3 - x1))

            b3 = ((Py - y2)*(x1 - x2) + (y1 - y2)*(x2 - Px))/((y1 - y3)*(x2 - x3) + (y2 - y3)*(x3 - x1))

            b1 = 1 - b2 - b3;
            
            Px = (1 - b2 - b3)* x1 + b2 * x2 + b3 * x3 
            Py = (1 - b2 - b3)* y1 + b2 * y2 + b3 * y3 

            // 分步计算
            a = x1 - x2; 
            b = x3 - x1;
            c = y2 - y1;
            d = y3 - y1;
            
        * p1点UV坐标[u1,v1] 
        * p2点UV坐标[u2,v2]
        * p3点UV坐标[u3,v3]
        */

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

        public void Preproccess()
        {
            Vector4 p1 = this.Vertices[0].ScreenSpacePosition;
            Vector4 p2 = this.Vertices[1].ScreenSpacePosition;
            Vector4 p3 = this.Vertices[2].ScreenSpacePosition;

            a = (int)(p2.X - p1.X);
            b = (int)(p3.X - p1.X);
            c = (int)(p2.Y - p1.Y);
            d = (int)(p3.Y - p1.Y);
            e = (int)(p2.Y - p3.Y);
            f = (int)(p1.Y - p2.Y);

            Vector4 e1 = p2 - p1;
            Vector4 e2 = p3 - p1;
            Vector4 e3 = p3 - p2;

            float n1 = e1.Length();
            float n2 = e2.Length();
            float n3 = e3.Length();

            float h = (n1 + n2 + n3) / 2;


            area = (float)Math.Sqrt(h * (h - n1) * (h - n2) * (h - n3));
            ////公式的分母
            den = d * b + e * c;

            u1 = Vertices[0].UV.X / Vertices[0].ClipSpacePosition.W;
            u2 = Vertices[1].UV.X / Vertices[1].ClipSpacePosition.W;
            u3 = Vertices[2].UV.X / Vertices[2].ClipSpacePosition.W;
            v1 = Vertices[0].UV.Y / Vertices[0].ClipSpacePosition.W;
            v2 = Vertices[1].UV.Y / Vertices[1].ClipSpacePosition.W;
            v3 = Vertices[2].UV.Y / Vertices[2].ClipSpacePosition.W;
            //透视校正
            w1 = 1.0f / Vertices[0].ClipSpacePosition.W;
            w2 = 1.0f / Vertices[1].ClipSpacePosition.W;
            w3 = 1.0f / Vertices[2].ClipSpacePosition.W;

            //l1 = (p2 - p3).Length();
            //l2 = (p3 - p1).Length();
            //l3 = (p2 - p1).Length();

            //this.p2y_p3y = p2.Y - p3.Y;
            //this.p3y_p1y = p3.Y - p1.Y;
            //this.p3x_p2x = p3.X - p2.X;
            //this.p1x_p3x = p1.X - p3.X;

        }

        public bool CalculateWeight(Vector4 p)
        {
            Vector4 p1 = Vertices[0].ScreenSpacePosition;
            Vector4 p2 = Vertices[1].ScreenSpacePosition;
            Vector4 p3 = Vertices[2].ScreenSpacePosition;

            float dy1 = p.Y - p1.Y;
            float dy2 = p.Y - p2.Y;
            float dx1 = p1.X - p.X;
            float dx2 = p2.X - p.X;

            //b2 = (dy1 * c - d * dx1) / den;
            //b3 = (dy2 * a - f * dx2) / den;

            Vector4 tp1 = p2 - p;
            Vector4 tp2 = p3 - p;
            Vector4 tp3 = p1 - p;

            float d1 = tp1.Length();
            float d2 = tp2.Length();
            float d3 = tp3.Length();

            float h1 = (l1 + d1 + d2) / 2;
            float h2 = (l2 + d2 + d3) / 2;
            float h3 = (l3 + d1 + d3) / 2;

            float t1 = (float)Math.Sqrt(h1 * (h1 - l1) * (h1 - d1) * (h1 - d2));
            float t2 = (float)Math.Sqrt(h2 * (h2 - l2) * (h2 - d2) * (h2 - d3));
            float t3 = (float)Math.Sqrt(h3 * (h3 - l3) * (h3 - d3) * (h3 - d1));

            //float t1 = Vector4.Cross(tp1, tp2).Length() / 2;
            //float t2 = Vector4.Cross(tp3, tp2).Length() / 2;
            //float t3 = Vector4.Cross(tp3, tp1).Length() / 2;

            //float a = t1 + t2 + t3;
            //if(p.Y == p1.Y || p.Y == p2.Y || p.Y == p3.Y)
            //{
            //    return false; 
            //}

            //Console.WriteLine(a);
            //if(a > area + 100)
            //{
            //    return false;
            //}
            //else
            //{
                b1 = t1 / area;
                //b1 = MathUtil.Clamp01(b1);
                b2 = t2 / area;
                //b2 = MathUtil.Clamp01(b2);
                b3 = t3 / area;
                //b3 = MathUtil.Clamp01(b3);
          
               
                this.nowpos = p;
                return true;
            //}
          
        }

        //// 插值之前必须先算权重
        public void CalWeight(Vector4 p)
        {
            Vector4 p1 = this.Vertices[0].ScreenSpacePosition;
            float m = (p.X - p1.X);
            float n = (p.Y - p1.Y);
            this.b2 = (float)(b * n - d * m) / (float)dn1;
            this.b3 = (float)(a * n - c * m) / (float)dn2;
			this.b2 = MathUtil.Clamp01(b2);
			this.b3 = MathUtil.Clamp01(b3);
        }

        //求得重心坐标后将UV坐标带入
        public float GetInterValue(float a, float b, float c)
        {
            return ((1.0f - b2 - b3) * a + b2 * b + b3 * c);
        }

        public byte GetInterValue(byte c1, byte c2, byte c3)
        {
            
            byte b = (byte)((1.0f-b2-b3) * c1 + c2 * b2 + c3 * b3);

            return b;
        }

        public Vector4 GetInterUV()
        {
            float u = GetInterValue(u1, u2, u3);
            float v = GetInterValue(v1, v2, v3);
            float w = GetInterValue(w1, w2, w3);
            float tmp = 1.0f / w;
            return new Vector4(u * tmp, v * tmp, 0, 0);
        }

		public Vector4 GetNormal()
		{
			Vector4 t1 = Vertices[0].nowNormal;
			Vector4 t2 = Vertices[1].nowNormal;
			Vector4 t3 = Vertices[2].nowNormal;

			float x = GetInterValue(t1.X, t2.X, t3.X);
			float y = GetInterValue(t1.Y, t2.Y, t3.Y);
			float z = GetInterValue(t1.Z, t2.Z, t3.Z);
			//float w = GetInterValue(t1.W, t2.W, t3.W);
			Vector4 v = new Vector4(x, y, z,1).Normalized;
			return v;
		}
		public Color4 GetInterColor()
        {
            //Vector4 p1 = Vertices[0].ScreenSpacePosition;
            //Vector4 p2 = Vertices[1].ScreenSpacePosition;
            //Vector4 p3 = Vertices[2].ScreenSpacePosition;

            Vertex t1 = Vertices[0];
            Vertex t2 = Vertices[1];
            Vertex t3 = Vertices[2];
            //double r1 = p2y_p3y * (p.X - p3.X) + p3x_p2x * (p.Y - p3.Y) * den;
            //double r2 = p3y_p1y * (p.X - p3.X) + p1x_p3x * (p.Y - p3.Y) * den;
            //double r3 = 1.0f - r1 - r2;

      
            byte red = GetInterValue(t1.Color.red, t2.Color.red, t3.Color.red);
            byte green = GetInterValue(t1.Color.green, t2.Color.green, t3.Color.green);
            byte blue = GetInterValue(t1.Color.blue, t2.Color.blue, t3.Color.blue);

            Color4 c = new Color4(red, green, blue);
            //Console.WriteLine(nowpos + " " + red + " " + green + " " + blue);

            return c;
        }
    }
}
