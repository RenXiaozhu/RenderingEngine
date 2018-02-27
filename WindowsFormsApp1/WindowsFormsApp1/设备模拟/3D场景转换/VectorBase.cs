using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections.Generic;
namespace RenderingEngine
{
    public class Vector2
    {
        public double x;
        public double y;
        public Vector2() { }
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    //2维齐次坐标
    public class VectorH2
    {
        public double x;
        public double y;
        public double h = 1;
        public VectorH2() { }
        public VectorH2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

   

   

   

    public class VectorBase 
    {
        /*
        public static List<Vector2>  CATransform2D(CATransform2DType type, double tx, double ty, List<Vector2> vct)
        {
            List<Vector2> transList = new List<Vector2>();

            foreach (Vector2 vt in vct)
            {

            }
        }*/

        public static void DrawDDALine_2D(Vector2 point1, Vector2 point2)
        {

            double m = (point2.x - point1.x) / (point2.y - point1.y);

            for (int x = (int)point1.x, y = (int)point1.y ; x <= point2.x; x++, y+=(int)m)
            {
                DrawPoint(new Point(x, y));
            }
        }
        
        
        public static void DrawPoint(Point point)
        {
            Graphics g = Graphics.FromHdc(GetForegroundWindow());

            Pen pen = new Pen(Color.Red);

            Rectangle rect = new Rectangle(point.X, point.Y, 1, 1);

            g.DrawRectangle(pen, rect);

            g.ReleaseHdc();
        }

       

        

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
    }

    
}
