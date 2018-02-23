using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;

namespace RenderingEngine
{
    class Device
    {
        public Bitmap bitmap;
        public BitmapData bitmapData;
        public int width;
        public int height;
        public Scanline scanline;
        private Scene scene;

        public Device(Bitmap bmp)
        {
            this.bitmap = bmp;
            this.width = bmp.Width;
            this.height = bmp.Height;
            this.scanline = new Scanline(this);

        }

        public void StartDisplay(FormWindow window)
        {
            //this.bitmapData = bitData;
            this.scene = window.scene;
            scanline.StartScan(this.scene);
            window.g.DrawImage(this.bitmap, new Point(0, 0));
        }

        public void DrawTriangles(TriangleModel triangleModel)
        {
            Vertex[] vectex = triangleModel.Vertices;
            DrawDDALine(new Vector2(vectex[0].Position.x, vectex[0].Position.y), new Vector2(vectex[1].Position.x, vectex[1].Position.y), vectex[0].Color);
            DrawDDALine(new Vector2(vectex[1].Position.x, vectex[1].Position.y), new Vector2(vectex[2].Position.x, vectex[2].Position.y), vectex[1].Color);
            DrawDDALine(new Vector2(vectex[2].Position.x, vectex[2].Position.y), new Vector2(vectex[0].Position.x, vectex[0].Position.y), vectex[2].Color);
        }

        public void DrawDDALine(Vector2 point1, Vector2 point2, Color4 color)
        {
            int a, b, c, d;
            double m = point2.x - point1.x;
            double n = point2.y - point1.y;
            if (m == 0)
            {
                double tmp = point2.y > point1.y ? point2.y : point1.y;
                double tmpy = point2.y < point1.y ? point2.y : point1.y;
                for (double y = tmpy; y <= tmp; y = y + 1)
                {
                    PutPixel((int)point1.x, (int)y , color);
                }
            }
            else if (n == 0)
            {
                double tmp = point2.x > point1.x ? point2.x : point1.x;
                double tmpx = point2.x< point1.x ? point2.x : point1.x;
                for (int x = (int)tmpx; x <=tmp; x = x + 1)
                {
                    PutPixel(x, (int)point1.y, color);
                }
            }
            else
            {
                double aspect = (point2.x - point1.x) / (point2.y - point1.y);
                if (point1.x > point2.x)
                {
                    a = (int)point2.x;
                    b = (int)point2.y;
                    c = (int)point1.x;
                    d = (int)point1.y;
                }
                else
                {
                    a = (int)point1.x;
                    b = (int)point1.y;
                    c = (int)point2.x;
                    d = (int)point2.y;
                }
                for (double x = a, y = b; x <= c; x += 1, y = y + aspect)
                {
                    PutPixel((int)x, (int)(y + 0.5), color);
                }
            }
           
         }

        
        // 直线段中点算法
        public void DrawMidPointLine(Vector2 point1, Vector2 point2, Color4 color)
        {
            int dx, dy, incrE, incrNE, d, x, y;
            dx =(int) (point2.x - point1.x);
            dy = (int)(point2.y - point1.y);
            d = dx - 2 * dy;
            incrE = -2 * dy;
            incrNE = 2 * (dx - dy);
            x = (int)point1.x;
            y = (int)point1.y;
            PutPixel((int)point1.x,(int)point1.y, color);

            while (x < (point2.x))
            {
                if (d > 0)
                {
                    d += incrE;
                }
                else
                {
                    d += incrNE;
                    y+=1;
                    x+=1;
                }
                PutPixel(x, y, color);
            }
        }

        // 圆弧中点算法

        void DrawMidPointCircle(int raduis, Color4 color)
        {
            int x, y,d;
            x = 0;
            y = raduis;
            d = 5 - 4 * raduis;
            PutPixel(x, y, color);
            while (y > x)
            {
                if (d <= 0)
                {
                    d += 8 * x + 12;
                }
                else
                {
                    d += 8 * (x - y) + 20;
                    y-=1;
                }
                x+=1;
                PutPixel(x, y, color);
            }
        }

        // 生成椭圆弧的中点算法
        void EllipsePoints(int x, int y, Color4 color)
        {
            PutPixel(x, y, color);
            PutPixel(-x, y, color);
            PutPixel(x, -y, color);
            PutPixel(-x, -y, color);
        }

        void MidPointEllipse(int a, int b, Color4 color)
        {
            int x, y, d, xP, yP, squarea, squareb;
            squarea = a * a;
            squareb = b * b;
            /* 计算分界点*/
            xP = (int)(0.5 + (float)squarea / Math.Sqrt((float)(squarea + squareb)));
            yP = (int)(0.5 + (float)squareb / Math.Sqrt((float)(squarea + squareb)));
           
            /*生成第一象限内的上半部分椭圆弧*/
            x = 0;
            y = b;
            d = 4 * (squareb - squareb * b) + squarea;

            EllipsePoints(x, y, color);

            while (x <= xP)
            {
                if (d <= 0)
                {
                    d += 4 * squareb * (2 * x + 3);
                }
                else
                {
                    d += 4 * squareb * (2 * x + 3) - 8 * squarea * (y - 1);
                    y-=1;
                }
                x+=1;
            }
            EllipsePoints(x, y, color);

            x = a;
            y = 0;
            d = 4 * (squarea - a * squareb) + squareb;
            EllipsePoints(x, y, color);
            while (y < yP)
            {
                if (d <= 0)
                {
                    d += 4 * squarea * (2 * y + 3);

                }
                else
                {
                    d += 4 * squarea * (2 * y + 3) - 8 * squareb * (x - 1);
                    x-=1;
                }
                y-=1;
                EllipsePoints(x, y, color);
            }

        }

        //画像素点
        public void PutPixel(int x,int y, Color4 finalColor)
        {
            Color color = Color.FromArgb(finalColor.red, finalColor.blue, finalColor.green);
            if (x >= 0 && y >= 0 && x <= width && y <= height)
            {

                this.bitmap.SetPixel(x, y, color);
            }
           
        }
    }
}
