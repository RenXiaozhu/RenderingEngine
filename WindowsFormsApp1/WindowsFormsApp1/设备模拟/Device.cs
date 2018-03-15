using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;

namespace RenderingEngine
{
    public class Device
    {
        private Bitmap bmp;
        private BitmapData bmData;
        private int height;
        private int width;
        private ScanLine scanLine;
        private HodgmanClip clip;
        private Vector4 clipMin;   // 裁剪空间(-1, -1, -1)
        private Vector4 clipMax;   // 裁剪空间(1, 1, 1)
        private Scene scene;

        // z-buffer
        private readonly float[] depthBuffer;

        public bool isShowBackFace { get; set; }

        public Device(Bitmap bmp)
        {
            this.bmp = bmp;
            this.height = bmp.Height;
            this.width = bmp.Width;
            this.scanLine = new ScanLine(this);
            this.clipMin = new Vector4(-1, -1, -1, 1);
            this.clipMax = new Vector4(1, 1, 1, 1);
            this.depthBuffer = new float[bmp.Width * bmp.Height];
        }

        public int GetHeight()
        {
            return this.height;
        }

        public int GetWidth()
        {
            return this.width;
        }

        public void Clear(BitmapData data)
        {
            // clear depth buffer
            for (int index = 0; index < depthBuffer.Length; index++)
            {
                depthBuffer[index] = float.MaxValue;
            }
            unsafe
            {
                byte* ptr = (byte*)(data.Scan0);
                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        *ptr = 0;
                        *(ptr + 1) = 0;
                        *(ptr + 2) = 0;
                        ptr += 3;
                    }
                    ptr += data.Stride - data.Width * 3;
                }
            }
        }

       

        private Vector4 Project(Vector4 coord, Matrix4x4 mvp)
        {
            Vector4 point = mvp.ApplY(coord);
            Vector4 viewPort = Homogenize(point);
            return viewPort;
        }

        // 归一化，得到屏幕坐标
        private Vector4 Homogenize(Vector4 x)
        {
            Vector4 val = new Vector4();
            float rhw = 1.0f / x.W;
            val.X = (1.0f + x.X * rhw) * GetWidth() * 0.5f;
            val.Y = (1.0f - x.Y * rhw) * GetHeight() * 0.5f;
            val.Z = x.Z * rhw;
            val.W = 1.0f;
            return val;
        }

        // 将坐标规格化到裁剪空间
        public Vector4 ClipSpace(Vector4 x, Matrix4x4 mvp)
        {
            Vector4 val = mvp.ApplY(x);
            float rhw = 1.0f / val.W;
            val.X = val.X * rhw;
            val.Y = val.Y * rhw;
            val.Z = val.Z * rhw;
            val.W = val.W; // val.W = 1;
            return val;
        }

        /* 视平面 将规范化视空间安比例还原到屏幕上
         * 因为裁剪空间坐标是在[-1,1]之间
         * 
         * 所以要想将坐标转换到视区[0,1]之间
         * 需要对空间坐标平移 x + 1, y + 1
         * 因为设备坐标系是倒置的，
         * 
         * 0 ------>x
         *   |
         *   |
         * y v
         * 所以 x' = 1 + x ; y' = 1 - y
         *  
         */
        public Vector4 ViewPort(Vector4 x)
        {
            
            Vector4 val = new Vector4();
            val.X = (1.0f + x.X) * GetWidth() * 0.5f;
            val.Y = (1.0f - x.Y) * GetHeight() * 0.5f;
            val.Z = x.Z;
            val.W =x.W;
            return val;
        }

        public void DrawPoint(Vector4 point, Color4 c)
        {
            if (point.X >= 0 && point.Y >= 0 && point.X <= GetWidth() && point.Y <= GetHeight())
            {
                if (point.X == GetWidth()) point.X = point.X - 1;
                if (point.Y == GetHeight()) point.Y = point.Y - 1;
                PutPixel((int)point.X, (int)point.Y, point.Z, c);
            }
        }

        public Color4 Tex2D(float u, float v, Texture texture)
        {
            // 如果不将uv坐标进行 1 - u ；1 - v；图像将是倒置的
            int Width_ = texture.GetWidth();
            int Height_ = texture.GetHeight();
            BitmapData data = texture.GetBmData();
            int x = Math.Abs((int)((1f - u) * Width_) % Width_);
            int y = Math.Abs((int)((1f - v) * Height_) % Height_);

            byte r = 0;
            byte g = 0;
            byte b = 0;

            unsafe
            {
                byte* ptr = (byte *)data.Scan0;
                byte* row = ptr + (y * data.Stride);
                b = row[x * 3];
                g = row[x * 3 + 1];
                r = row[x * 3 + 2];
            }

            return new Color4((byte)r, (byte)g, (byte)b);
        }

        // DDA 画线算法
        private void DrawLine(Vertex v1, Vertex v2, Vector4 point0, Vector4 point1, Scene scene)
        {
            int x0 = (int)point0.X;
            int y0 = (int)point0.Y;
            int x1 = (int)point1.X;
            int y1 = (int)point1.Y;
            float z1 = point0.Z;
            float z2 = point1.Z;

            int dx = x1 - x0;
            int dy = y1 - y0;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            if (steps == 0) return;
            float xInc = (float)dx / (float)steps;
            float yInc = (float)dy / (float)steps;

            float x = x0;
            float y = y0;
            //顶点位置和法线方向与 光线方向的点积  
            // 顶点的实际颜色 = 光线方向 点积 法线方向 * 光的颜色* 本身颜色
            float nDotL1 = DirectionLight.ComputeNDotL(v1.Position, v1.Normal);
            float nDotL2 = DirectionLight.ComputeNDotL(v2.Position, v2.Normal);

            for (int i = 1; i <= steps; i++)
            {
                float ratio = (float)i / (float)steps;
                Color4 vertexColor = new Color4(0, 0, 0);
                Color4 lightColor = new Color4(0, 0, 0);

                if (DirectionLight.IsEnable)
                {
                    Color4 c1 = DirectionLight.GetFinalLightColor(nDotL1);
                    Color4 c2 = DirectionLight.GetFinalLightColor(nDotL2);
                    lightColor = MathUtil.ColorInterp(c1, c2, ratio);
                }

                 vertexColor = MathUtil.ColorInterp(v1.Color, v2.Color, ratio);

                float z = MathUtil.Interp(z1, z2, ratio);
                if (float.IsNaN(z))
                {
                    //Console.WriteLine("IsNaN");
                    return;
                }
                DrawPoint(new Vector4((int)x, (int)y, z, 0), vertexColor + lightColor);
                x += xInc;
                y += yInc;
            }
        }

        //消隐
        private bool ShouldBackFaceCull(VertexTriangle oriVt)
        {
            Vector4 a = new Vector4(oriVt.Vertices[0].ScreenSpacePosition);
            Vector4 b = new Vector4(oriVt.Vertices[1].ScreenSpacePosition);
            Vector4 c = new Vector4(oriVt.Vertices[2].ScreenSpacePosition);
            a.Z = b.Z = c.Z = 0;
            Vector4 ab = b - a;
            Vector4 ac = c - a;
            return Vector4.Cross(ab, ac).Z > 0;
        }

        private void DrawTriangle(VertexTriangle vt, VertexTriangle oriVt, Scene scene,bool isWorld)
        {
            if (!ShouldBackFaceCull(vt))
            {
				//边表
                if(scene.renderState == Scene.RenderState.WireFrame)
                {
                    this.scanLine.ProcessScanLine(vt, oriVt, scene,isWorld);
                }
                else
                {//三角形扫描
                    this.scanLine.ScanLine_new(vt,oriVt, scene);
                }
				
				//重心法填充
                //this.scanLine.StartScanLine(vt,oriVt,scene);
                //this.scanLine.StartScanTriangle(vt,oriVt,scene);
            }
        }

        // 渲染，绘制点 线 面 颜色
        public void Render(Scene scene, BitmapData bmData)
        {
            this.scene = scene;

            this.bmData = bmData;

            //从世界->相机->裁剪空间->屏幕空间->视区
            Matrix4x4 matrixMVP = CATransform.MVPMatrix;

            foreach (var face in scene.mesh.faces)
            {
                Vertex vertexA;
                Vertex vertexB;
                Vertex vertexC;

                scene.mesh.PrePareUV(face);

                Triangle t1 = face.t_1;

                vertexA = scene.mesh.Vertices[t1.a];

                vertexB = scene.mesh.Vertices[t1.b];

                vertexC = scene.mesh.Vertices[t1.c];

               // renderTriangle(vertexA, vertexB, vertexC, matrixMVP,false);

                Triangle t2 = face.t_2;

                vertexA = scene.mesh.Vertices[t2.a];

                vertexB = scene.mesh.Vertices[t2.b];

                vertexC = scene.mesh.Vertices[t2.c];

              //  renderTriangle(vertexA, vertexB, vertexC, matrixMVP,false);
            }

            foreach (var face in scene.worldMap.faces)
            {
                Vertex vertexA;
                Vertex vertexB;
                Vertex vertexC;

                Triangle t1 = face.t_1;

                scene.worldMap.PrePareUV(face);

                vertexA = scene.worldMap.Vertices[t1.a];

                vertexB = scene.worldMap.Vertices[t1.b];

                vertexC = scene.worldMap.Vertices[t1.c];

                renderTriangle(vertexA, vertexB, vertexC, matrixMVP,true);

                Triangle t2 = face.t_2;

                vertexA = scene.worldMap.Vertices[t2.a];

                vertexB = scene.worldMap.Vertices[t2.b];

                vertexC = scene.worldMap.Vertices[t2.c];

                renderTriangle(vertexA, vertexB, vertexC, matrixMVP,true);
            }

        }

        public void renderTriangle(Vertex vertexA, Vertex vertexB, Vertex vertexC , Matrix4x4 matrixMVP , bool isWorld)
        {

                List<Vertex> pIn = new List<Vertex>();

                vertexA.ClipSpacePosition = this.ClipSpace(vertexA.nowPos, matrixMVP);
                vertexB.ClipSpacePosition = this.ClipSpace(vertexB.nowPos, matrixMVP);
                vertexC.ClipSpacePosition = this.ClipSpace(vertexC.nowPos, matrixMVP);
                vertexA.ScreenSpacePosition = this.ViewPort(vertexA.ClipSpacePosition);
                vertexB.ScreenSpacePosition = this.ViewPort(vertexB.ClipSpacePosition);
                vertexC.ScreenSpacePosition = this.ViewPort(vertexC.ClipSpacePosition);

                pIn.Add(vertexA);
                pIn.Add(vertexB);
                pIn.Add(vertexC);

                for (int i = 0; i < 6; i++)
                {
                    if (pIn.Count == 0) break;
                    clip = new HodgmanClip(this);
                    clip.HodgmanPolygonClip((HodgmanClip.Boundary)i, clipMin, clipMax, pIn.ToArray());
                    pIn = clip.GetOutputList();

                }
                List<VertexTriangle> vtList = this.MakeTriangle(pIn);
                VertexTriangle oriVt = new VertexTriangle(vertexA, vertexB, vertexC);

                if (scene.renderState == Scene.RenderState.WireFrame)
                {
                    // 画线框, 需要vertex的normal,pos,color
                    for (int i = 0; i < vtList.Count; i++)
                    {
                        if (!ShouldBackFaceCull(vtList[i]))
                        {
                            int length = vtList[i].Vertices.Length;
                            Vertex start = vtList[i].Vertices[length - 1];
                            for (int j = 0; j < length; j++)
                            {
                                Vector4 viewPortA = this.ViewPort(start.ClipSpacePosition);
                                Vector4 viewPortB = this.ViewPort(vtList[i].Vertices[j].ClipSpacePosition);
                               // DrawLine(start, vtList[i].Vertices[j], viewPortA, viewPortB, scene);
                                DrawDDALine(start, vtList[i].Vertices[j]);
                                start = vtList[i].Vertices[j];
                            }
                        }
                    }
                }
                else
                {
                    // 填充三角形                   bv gf      
                    for (int i = 0; i < vtList.Count; i++)
                    {
						DrawTriangle(vtList[i], oriVt, scene ,isWorld);
                    }
                }
        }

        //生成三角形数组
        private List<VertexTriangle> MakeTriangle(List<Vertex> input)
        {
            List<VertexTriangle> temp = new List<VertexTriangle>();
            for (int i = 0; i < input.Count - 2; i++)
            {
                VertexTriangle vt = new VertexTriangle(input[0], input[i + 1], input[i + 2]);
                temp.Add(vt);
            }
            return temp;
        }


        public void DrawDDALine(Vertex v1, Vertex v2)
        {
            //int a, b, c, d;
            double m = v2.ScreenSpacePosition.X - v1.ScreenSpacePosition.X;
            double n = v2.ScreenSpacePosition.Y - v1.ScreenSpacePosition.Y;

            Vector4 point1 = v1.ScreenSpacePosition;
            Vector4 point2 = v2.ScreenSpacePosition;
            //顶点位置和法线方向与 光线方向的点积  
            // 顶点的实际颜色 = 光线方向 点积 法线方向 * 光的颜色* 本身颜色
            float nDotL1 = DirectionLight.ComputeNDotL(v1.Position, v1.Normal);
            float nDotL2 = DirectionLight.ComputeNDotL(v2.Position, v2.Normal);
            float z1 = point1.Z;
            float z2 = point2.Z;

            double px = point1.X;
            double py = point1.Y;
            double steps = Math.Max(Math.Abs(m), Math.Abs(n));
            double xt = m / steps;
            double yt = n / steps;
            for (int i = 0; i < steps; i++)
            {
                Color4 vertexColor = new Color4(0, 0, 0);
                Color4 lightColor = new Color4(0, 0, 0);
                float ratio = (float)(i / steps);

                if (DirectionLight.IsEnable)
                {
                    Color4 c1 = DirectionLight.GetFinalLightColor(nDotL1);
                    Color4 c2 = DirectionLight.GetFinalLightColor(nDotL2);
                    lightColor = MathUtil.ColorInterp(c1, c2, ratio);
                }

                vertexColor = MathUtil.ColorInterp(v1.Color, v2.Color, ratio);

                float z = MathUtil.Interp(z1, z2, ratio);
                DrawPoint(new Vector4((int)px, (int)(py + 0.5), z, 0), vertexColor + lightColor);
                px += xt;
                py += yt;
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
		//直接操作的是像素图
        public void PutPixel(int x,int y, Color4 finalColor)
        {
            Color color = Color.FromArgb(finalColor.red, finalColor.blue, finalColor.green);
            this.bmp.SetPixel(x, y, color);
        }

		//画像素点
		//直接操作的是位图
		public void PutPixel(int x, int y, float z, Color4 color)
		{
			// 利用深度消除不需要显示的像素
			// 当新像素的深度大于原像素深度时，显示原有像素，新像素被抛弃
			int index = (x + y * GetWidth());
			if (depthBuffer[index] < z) return;
			depthBuffer[index] = z;
			unsafe
			{
				byte* ptr = (byte*)(this.bmData.Scan0);
				byte* row = ptr + (y * this.bmData.Stride);
				row[x * 3] = color.blue;
				row[x * 3 + 1] = color.green;
				row[x * 3 + 2] = color.red;
			}
		}
	}
}
