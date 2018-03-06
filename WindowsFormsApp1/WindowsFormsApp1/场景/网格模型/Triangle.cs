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

        public Vector4 AUV;
        public Vector4 BUV;
        public Vector4 CUV;

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

        // 做三角形重心插值时的二元一次方程组系数
        // P.x = (1 - u - v) * P1.x + u * P2.x + v * P3.x
        // P.y = (1 - u - v) * P1.y + u * P2.y + v * P3.y
        private int a, b, c, d, e, f, den, dn1, dn2;
        // 重心坐标
        private float b1, b2, b3;
        private float u1, v1;
        private float u2, v2;
        private float u3, v3;
        private float w1, w2, w3;
        private Vector4 nowpos;
        private float area;
        private float l1, l2, l3, l;

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

        public void Preproccess()
        {
            Vector4 p1 = this.Vertices[0].ScreenSpacePosition;
            Vector4 p2 = this.Vertices[1].ScreenSpacePosition;
            Vector4 p3 = this.Vertices[2].ScreenSpacePosition;

            a = (int)(p1.X - p2.X);
            b = (int)(p2.X - p3.X);
            c = (int)(p3.X - p1.X);
            d = (int)(p1.Y - p3.Y);
            e = (int)(p2.Y - p3.Y);
            f = (int)(p1.Y - p2.Y);

            Vector4 e1 = p2 - p1;
            Vector4 e2 = p3 - p1;
            Vector4 e3 = p3 - p2;

            Vector4 at = Vector4.Cross(e1, e2);

            area = at.Length() / 2;
            //公式的分母
            den = d * b + e * c;

            l1 = e1.Length();
            l2 = e2.Length();
            l3 = e3.Length();
            l = (l1 + l2 + l3) / 2;

            u1 = Vertices[0].UV.X / Vertices[0].ClipSpacePosition.W;
            u2 = Vertices[1].UV.X / Vertices[1].ClipSpacePosition.W;
            u3 = Vertices[2].UV.X / Vertices[2].ClipSpacePosition.W;
            v1 = Vertices[0].UV.Y / Vertices[0].ClipSpacePosition.W;
            v2 = Vertices[1].UV.Y / Vertices[1].ClipSpacePosition.W;
            v3 = Vertices[2].UV.Y / Vertices[2].ClipSpacePosition.W;
            //透视校正
            w1 = 1f / Vertices[0].ClipSpacePosition.W;
            w2 = 1f / Vertices[1].ClipSpacePosition.W;
            w3 = 1f / Vertices[2].ClipSpacePosition.W;


        }

        public void CalculateWeight(Vector4 p)
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

            float h = (d1 + d2 + d3) / 2;
            float t1 = (float)Math.Sqrt(h * (h - d1) * (h - d2) * (h - l3));
            float t2 = (float)Math.Sqrt(h * (h - d1) * (h - d2) * (h - l1));
            float t3 = (float)Math.Sqrt(h * (h - d1) * (h - d2) * (h - l2));

            b1 = t1 / area;
            b1 = MathUtil.Clamp01(b1);
            b2 = t2 / area;
            b2 = MathUtil.Clamp01(b2);
            b3 = t3 / area;
            b3 = MathUtil.Clamp01(b3);

            this.nowpos = p;
        }

        //// 插值之前必须先算权重
        //public void CalWeight(Vector4 p)
        //{
        //    Vector4 p1 = this.Vertices[0].ScreenSpacePosition;
        //    int m = (int)(p.X - p1.X);
        //    int n = (int)(p.Y - p1.Y);
        //    this.Weight1 = (float)(b * n - d * m) / (float)dn1;
        //    this.Weight2 = (float)(a * n - c * m) / (float)dn2;

        //}

        //求得重心坐标后将UV坐标带入
        public float GetInterValue(float a, float b, float c)
        {
            return ((1 - b2 - b3) * a + b2 * b + b3 * c);
        }

        public byte GetInterValue(byte c1, byte c2, byte c3)
        {
            byte b = (byte)((1 - b2 - b3) * c1 + c2 * b2 + c3 * b3);

            return b;
        }

        public Vector4 GetInterUV()
        {
            float u = GetInterValue(u1, u2, u3);
            float v = GetInterValue(v1, v2, v3);
            float w = GetInterValue(w1, w2, w3);
            return new Vector4(u / w, v / w, 0, 0);
        }

        public Color4 GetInterColor()
        {
            Vertex t1 = Vertices[0];
            Vertex t2 = Vertices[1];
            Vertex t3 = Vertices[2];

            byte red = GetInterValue(t1.lightColor.red, t2.lightColor.red, t3.lightColor.red);
            byte green = GetInterValue(t1.lightColor.green, t2.lightColor.green, t3.lightColor.green);
            byte blue = GetInterValue(t1.lightColor.blue, t2.lightColor.blue, t3.lightColor.blue);

            //Console.WriteLine(nowpos);
            if(red == 255 && green == 0 && blue == 0)
            {
                
            }
            return new Color4(red, green, blue);
        }
    }
}
