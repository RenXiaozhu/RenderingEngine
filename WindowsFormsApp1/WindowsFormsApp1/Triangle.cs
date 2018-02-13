using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace RenderingEngine
{
    class Triangle
    {
        Vector4 A;
        Vector4 B;
        Vector4 C;
        //左上方顶点
        Vertex vertex1;
        //右上方顶点
        Vertex vertex2;
        //右下方顶点
        Vertex vertex3;
        //左下方顶点
        Vertex vertex4;
        Point minPoint;
        public Triangle(Vector4 A,Vector4 B, Vector4 C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.minPoint = new Point(0 , 0);
        }

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
        /*
        // 包围三角形的盒子
        double getBoxWidth()
        {
            double a = this.point_a.x;
            double b = this.point_b.x;
            double c = this.point_c.x;
            double min;
            double max;
            if (a <= b)
            {
                min = a;
            }
            else
            {
                min = b;
            }
            if (min >= c)
            {
                min = c;
            }
            
            if (a >= b)
            {
                max = a;
            }
            else
            {
                max = b;
            }
            if (max <= c)
            {
                max = c;
            }

            this.minPoint.X = (int)min;
            return max - min;
        }
         
        double getBoxHeight()
        {
            double a = this.point_a.y;
            double b = this.point_b.y;
            double c = this.point_c.y;
            double min;
            double max;
            if (a <= b)
            {
                min = a;
            }
            else
            {
                min = b;
            }
            if (min >= c)
            {
                min = c;
            }

            if (a >= b)
            {
                max = a;
            }
            else
            {
                max = b;
            }
            if (max <= c)
            {
                max = c;
            }
            this.minPoint.Y = (int)min;
            return max - min;
        }
    }
    */
    }
}
