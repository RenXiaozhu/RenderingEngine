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
		public HodgmanClip clip;
		private Vector4 clipMin; //裁剪空间 （-1,-1，-1）
		private Vector4 clipMax;// 裁剪空间（1,1,1）

		
        public int width;
        public int height;
		//扫描线
		public Scanline scanline;
		// 场景
        private Scene scene;
		//深度缓存
		private readonly double[] depthBuffer;

        public Device(Bitmap bmp)
        {
            this.bitmap = bmp;
            this.width = bmp.Width;
            this.height = bmp.Height;
            this.scanline = new Scanline(this);
			this.depthBuffer = new double[bmp.Width * bmp.Height];
        }

        public void StartDisplay(FormWindow window)
        {
            //this.bitmapData = bitData;
            this.scene = window.scene;
            scanline.StartScan(this.scene);
            window.g.DrawImage(this.bitmap, new Point(0, 0));
        }

		public void ClearBitmap()
		{
			for (int i = 0; i < height; i += 1)
			{
				for (int j = 0; j < width; j += 1)
				{
					bitmap.SetPixel(j, i, Color.Black);
				}
			}
		}

		public void ClearBitmapData(BitmapData data)
		{
			if (this.bitmapData == null)
			{
				this.bitmapData = data;
			}
			for(int i = 0; i<depthBuffer.Length; i+=1)
			{
				depthBuffer[i] = float.MaxValue;
			}
			
			unsafe
			{
				//首地址
				byte* ptr = (byte*)(bitmapData.Scan0);
				for (int i = 0; i < bitmapData.Height; i++)
				{
					for (int j = 0; j < bitmapData.Width; j++)
					{
						*ptr = 0;
						*(ptr + 1) = 0;
						*(ptr + 3) = 0;
						ptr += 3;
					}
					ptr += bitmapData.Stride - bitmapData.Width * 3;
				}
			}
		}

        public void DrawTriangles(TriangleModel triangleModel)
        {
            Vertex[] vectex = triangleModel.Vertices;
            DrawDDALine(new Vector2(vectex[0].Position.x, vectex[0].Position.y), new Vector2(vectex[1].Position.x, vectex[1].Position.y), vectex[0].Color);
            DrawDDALine(new Vector2(vectex[1].Position.x, vectex[1].Position.y), new Vector2(vectex[2].Position.x, vectex[2].Position.y), vectex[1].Color);
            DrawDDALine(new Vector2(vectex[2].Position.x, vectex[2].Position.y), new Vector2(vectex[0].Position.x, vectex[0].Position.y), vectex[2].Color);
        }

		// 判断三角形的三个点是否在一条线上
		public bool IsOnSameLine(TriangleModel orivt)
		{
			Vector4 a = new Vector4(orivt.Vertices[0].ScreenSpacePosition);
			Vector4 b = new Vector4(orivt.Vertices[1].ScreenSpacePosition);
			Vector4 c = new Vector4(orivt.Vertices[2].ScreenSpacePosition);

			a.z = b.z = c.z = 0;
			Vector4 ab = b - a;
			Vector4 ac = c - a;
			return ab.CrossMultiply(ac).z > 0;
		}

		public Color4 Text2D(float u, float v, Texture texture)
		{
			int x = Math.Abs((int)(1f - u) * texture.GetWidth() % texture.GetWidth());
			int y = Math.Abs((int)(1f - v) * texture.GetHeight() % texture.GetHeight());
			byte r = 0;
			byte g = 0;
			byte b = 0;
			unsafe
			{
				byte* ptr = (byte*)(texture.GetBmData().Scan0);
				byte* row = ptr + (y * texture.GetBmData().Stride);
				b = row[x * 3];
				g = row[x * 3 + 1];
				r = row[x * 3 + 2];
			}
			return new Color4((byte)r, (byte)g, (byte)b);
		}
		private void DrawTriangle(TriangleModel vt, TriangleModel orivt, Scene scene)
		{
			if (!IsOnSameLine(orivt))
			{
				this.scanline.ProcessScanLine(vt, orivt, scene);
			}
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
            if (x >= 0 && y >= 0 && x <= scene.camera.ViewPlaneWidth && y <= scene.camera.ViewPlaneHeight)
            {
                this.bitmap.SetPixel(x, y, color);
            }
        }

		public void PutPixel(int x, int y, float z,Color4 finalColor)
		{
			int index = (x + y * width);
			if (index < 0 || index > depthBuffer.Length)
				return;
			if (depthBuffer[index] < z)
				return;
			depthBuffer[index] = z;
			unsafe
			{
				//this.bitmap.SetPixel(x, y, Color.FromArgb(finalColor.red, finalColor.green, finalColor.blue));
				byte* ptr = (byte*)(this.bitmapData.Scan0);
				//stride 扫描宽度
				byte* row = ptr + (y * this.bitmapData.Stride);
				row[x * 3] = finalColor.blue;
				row[x * 3 + 1] = finalColor.green;
				row[x * 3 + 2] = finalColor.red;
			}
		}

		private Vector4 Project(Vector4 coord, VETransform3D mvp)
		{
			Vector4 point = mvp.Maxtrix4x1(coord);
			point.Normalize();
			return point;
		}

		private Vector4 Normalization(Vector4 vt)
		{
			Vector4 vct = new Vector4();
			double rhw = 1.0f / vt.h;
			vct.x = (1.0f + vt.x * rhw) * width * 0.5f;
			vct.y = (1.0f - vt.y * rhw) * height * 0.5f;
			vct.z = vt.z * rhw;
			vct.h = 1.0f;
			return vct;
		}

		// 裁剪平面
		public Vector4 ClipSpace(Vector4 x, VETransform3D mvp)
		{
			Vector4 val = mvp.Maxtrix1x4(x);
			double rhw = 1.0f / val.h;
			val.x = val.x * rhw;
			val.y = val.y * rhw;
			val.z = val.z * rhw;
			val.h = 1.0f;
			return val;
		}

		// 视平面 将规范化视空间安比例还原到屏幕上
		public Vector4 ViewPort(Vector4 x)
		{
			Vector4 val = new Vector4();
			val.x = (1.0f + x.x) * width * 0.5f;
			val.y = (1.0f - x.y) * height * 0.5f;
			val.z = x.z;
			val.h = 1.0f;
			return val;
		}

		public void DrawPoint(Vector4 point, Color4 color)
		{
			if (point.x == width)
				point.x = point.x - 1;
			if (point.y == height)
				point.y = point.y - 1;
			PutPixel((int)point.x, (int)point.y, (float)point.z, color);
		}


		public void DrawLine(Vertex v1, Vertex v2, Vector4 point0, Vector4 point1, Scene scene)
		{
			int x0 = (int)point0.x;
			int y0 = (int)point0.y;
			int x1 = (int)point1.x;
			int y1 = (int)point1.y;

			double z1 = point0.z;
			double z2 = point1.z;

			int dx = x1 - x0;
			int dy = y1 - y0;

			// 步长 DDA算法 按像素逐步递增划线
			int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

			if (steps == 0) return;

			double xInc = dx / steps;
			double yInc = dy / steps;

			double x = x0;
			double y = y0;

			//顶点位置和法线方向与 光线方向的点积  
			// 顶点的实际颜色 = 光线方向 点积 法线方向 * 光的颜色* 本身颜色
			double nDotL1 = scene.light.ComputeNDotL(v1.Position, v1.Normal);

			double nDotL2 = scene.light.ComputeNDotL(v2.Position, v2.Normal);

			for (int i = 1; i <= steps; i++)
			{

				float ratio = (float)i / (float)steps;

				Color4 vertexColor = new Color4(0, 0, 0);

				Color4 lightColor = new Color4(0, 0, 0);

				if (scene.light.IsEnable)
				{
					Color4 c1 = scene.light.GetFinalLightColor((float)nDotL1);
					Color4 c2 = scene.light.GetFinalLightColor((float)nDotL2);
					lightColor = MathUtil.ColorInterp(c1, c2, ratio);
				}
				else
				{
					vertexColor = MathUtil.ColorInterp(v1.Color, v2.Color,ratio);
				}

				float z = (float)MathUtil.Interp(z1, z2, ratio);
				if (float.IsNaN(z))
				{
					return;
				}
				DrawPoint(new Vector4((int)x, (int)y, z, 0), vertexColor + lightColor);
				x += xInc;
				y += yInc;
			}
		}

		public void Render(Scene scene, BitmapData bmData)
		{
			this.scene = scene;
			this.bitmapData = bmData;
			VETransform3D mvpMatrix = CATransform.MVPMatrix;
			foreach (var triangle in scene.mesh.triangles)
			{
				Vertex vertexA = scene.mesh.vertices[triangle.a];
				Vertex vertexB = scene.mesh.vertices[triangle.b];
				Vertex vertexC = scene.mesh.vertices[triangle.c];

				List<Vertex> pIn = new List<Vertex>();
				//裁剪后的位置
				vertexA.ClipSpacePosition = ClipSpace(vertexA.Position, mvpMatrix);
				vertexB.ClipSpacePosition = ClipSpace(vertexB.Position, mvpMatrix);
				vertexC.ClipSpacePosition = ClipSpace(vertexC.Position, mvpMatrix);
				// 视区位置
				vertexA.ScreenSpacePosition = ViewPort(vertexA.ClipSpacePosition);
				vertexB.ScreenSpacePosition = ViewPort(vertexB.ClipSpacePosition);
				vertexC.ScreenSpacePosition = ViewPort(vertexC.ClipSpacePosition);

				pIn.Add(vertexA);
				pIn.Add(vertexB);
				pIn.Add(vertexC);

				//for (int i = 0; i < 6; i += 1)
				//{
				//	if (pIn.Count == 0) break;
				//	clip = new HodgmanClip(this);
				//	clip.HodgmanPolygonClip((HodgmanClip.Boundary)i, clipMin, clipMax, pIn.ToArray());
				//	pIn = clip.GetOutputList();
				//}

				List<TriangleModel> vtList = MakeTriangle(pIn);
				TriangleModel orivt = new TriangleModel(vertexA, vertexB, vertexC);
				if (scene.renderState == Scene.RenderState.WireFrame)
				{
					//画线框 需要vertex的法向量normal， 位置 pos， 颜色 color
					for (int i = 0; i < vtList.Count; i += 1)
					{
						int length = vtList[i].Vertices.Length;
						Vertex start = vtList[i].Vertices[length - 1];
						for (int j = 0; j < length; j += 1)
						{
							Vector4 viewPortA =this.ViewPort( start.ClipSpacePosition);
							Vector4 viewPortB = this.ViewPort(vtList[i].Vertices[j].ClipSpacePosition);
							DrawLine(start, vtList[i].Vertices[j], viewPortA, viewPortB, scene);
							start = vtList[i].Vertices[j];
						}
					}
				}
				else
				{
					//填充三角形
					for (int i = 0; i < vtList.Count; i += 1)
					{
						DrawTriangle(vtList[i], orivt, scene);
					}
				}
			}
		}

		private List<TriangleModel> MakeTriangle(List<Vertex> input)
		{
			List<TriangleModel> temp = new List<TriangleModel>();
			for (int i = 0; i < input.Count - 2; i += 1)
			{
				TriangleModel vt = new TriangleModel(input[0], input[i + 1], input[i + 2]);
				temp.Add(vt);
			}
			return temp;
		}
	}
}
