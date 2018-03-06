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
                        *ptr = 128;
                        *(ptr + 1) = 128;
                        *(ptr + 2) = 128;
                        ptr += 3;
                    }
                    ptr += data.Stride - data.Width * 3;
                }
            }
        }

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
            val.W = 1.0f;
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
            int x = Math.Abs((int)((1f - u) * texture.GetWidth()) % texture.GetWidth());
            int y = Math.Abs((int)((1f - v) * texture.GetHeight()) % texture.GetHeight());

            byte r = 0;
            byte g = 0;
            byte b = 0;

            unsafe
            {
                byte* ptr = (byte *)texture.GetBmData().Scan0;
                byte* row = ptr + (y * texture.GetBmData().Stride);
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
            float nDotL1 = scene.light.ComputeNDotL(v1.Position, v1.Normal);
            float nDotL2 = scene.light.ComputeNDotL(v2.Position, v2.Normal);

            for (int i = 1; i <= steps; i++)
            {
                float ratio = (float)i / (float)steps;
                Color4 vertexColor = new Color4(0, 0, 0);
                Color4 lightColor = new Color4(0, 0, 0);

                if (scene.light.IsEnable)
                {
                    Color4 c1 = scene.light.GetFinalLightColor(nDotL1);
                    Color4 c2 = scene.light.GetFinalLightColor(nDotL2);
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

        private void DrawTriangle(VertexTriangle vt, VertexTriangle oriVt, Scene scene)
        {
            if (!ShouldBackFaceCull(oriVt))
            {
                this.scanLine.ProcessScanLine(vt, oriVt, scene);
            }
        }

        /*
        public void DrawTriangle(VertexTriangle vt, Scene scene)
        {
            Vector4 normal = vt.VertexA.Normal;
            Vector4 dir = scene.camera.GetDir();
            float dot = Vector4.Dot(dir, normal);
            if (dot > 0) return;

            List<Vertex> vList = new List<Vertex>();
            vList.Add(vt.VertexA);
            vList.Add(vt.VertexB);
            vList.Add(vt.VertexC);
            vList.Sort();

            //Vector4 p1 = this.ViewPort(vList[0].ClipSpacePosition);
            //Vector4 p2 = this.ViewPort(vList[1].ClipSpacePosition);
            //Vector4 p3 = this.ViewPort(vList[2].ClipSpacePosition);   
            Vector4 p1 = vList[0].ScreenSpacePosition;
            Vector4 p2 = vList[1].ScreenSpacePosition;
            Vector4 p3 = vList[2].ScreenSpacePosition;       

            //if (p1.Y > p2.Y)
            //{
            //    var temp = p2;
            //    p2 = p1;
            //    p1 = temp;
            //}

            //if (p2.Y > p3.Y)
            //{
            //    var temp = p2;
            //    p2 = p3;
            //    p3 = temp;
            //}

            //if (p1.Y > p2.Y)
            //{
            //    var temp = p2;
            //    p2 = p1;
            //    p1 = temp;
            //}

            float dP1P2, dP1P3, dP2P3;
            if (p2.Y - p1.Y > 0)
                dP1P2 = (p2.X - p1.X) / (p2.Y - p1.Y);
            else
                dP1P2 = 0;

            if (p3.Y - p1.Y > 0)
                dP1P3 = (p3.X - p1.X) / (p3.Y - p1.Y);
            else
                dP1P3 = 0;

            if (p3.Y - p2.Y > 0)
                dP2P3 = (p3.X - p2.X) / (p3.Y - p2.Y);
            else
                dP2P3 = 0;

            if(dP1P2 == 0)
            {
                if (p1.X > p2.X)
                {
                    var temp = vList[0];
                    vList[0] = vList[1];
                    vList[1] = temp;
                }
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    //this.scanLine.ProcessScanLineAd(y, p1, p3, p2, p3, scene);
                    this.scanLine.ProcessScanLineAd(y, vList[0], vList[2], vList[1], vList[2], scene);
                }
            }
            else
            {
                if (dP1P2 > dP1P3)
                {
                    for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                    {
                        if (y < p2.Y)
                        {
                            //this.scanLine.ProcessScanLineAd(y, p1, p3, p1, p2, scene);
                            this.scanLine.ProcessScanLineAd(y, vList[0], vList[2], vList[0], vList[1], scene);
                        }
                        else
                        {
                            //this.scanLine.ProcessScanLineAd(y, p1, p3, p2, p3, scene);
                            this.scanLine.ProcessScanLineAd(y, vList[0], vList[2], vList[1], vList[2], scene);
                        }
                    }
                }
                else
                {
                    for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                    {
                        if (y < p2.Y)
                        {
                            //this.scanLine.ProcessScanLineAd(y, p1, p2, p1, p3, scene);
                            this.scanLine.ProcessScanLineAd(y, vList[0], vList[1], vList[0], vList[2], scene);
                        }
                        else
                        {
                            //this.scanLine.ProcessScanLineAd(y, p2, p3, p1, p3, scene);
                            this.scanLine.ProcessScanLineAd(y, vList[1], vList[2], vList[0], vList[2], scene);
                        }
                    }
                }
            }
        }
        */

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
                switch (face.type)
                {
                    case Face.FaceType.Front:
                        {
                            Triangle t1 = face.t_1;
                            vertexA = scene.mesh.Vertices[t1.a];
                            vertexA.UV = new Vector4(0, 0, 0, 0);

                            vertexB = scene.mesh.Vertices[t1.b];
                            vertexB.UV = new Vector4(1, 0, 0, 0);

                            vertexC = scene.mesh.Vertices[t1.c];
                            vertexC.UV = new Vector4(0, 1, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);

                            Triangle t2 = face.t_2;
                            vertexA = scene.mesh.Vertices[t2.a];
                            vertexA.UV = new Vector4(1, 1, 0, 0);

                            vertexB = scene.mesh.Vertices[t2.b];
                            vertexB.UV = new Vector4(0, 1, 0, 0);

                            vertexC = scene.mesh.Vertices[t2.c];
                            vertexC.UV = new Vector4(1, 0, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);

                        }
                        break;
                    case Face.FaceType.Left:
                        {
                            Triangle t1 = face.t_1;
                            vertexA = scene.mesh.Vertices[t1.a];
                            vertexA.UV = new Vector4(0, 0, 0, 0);

                            vertexB = scene.mesh.Vertices[t1.b];
                            vertexB.UV = new Vector4(1, 0, 0, 0);

                            vertexC = scene.mesh.Vertices[t1.c];
                            vertexC.UV = new Vector4(0, 1, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);

                            Triangle t2 = face.t_2;
                            vertexA = scene.mesh.Vertices[t2.a];
                            vertexA.UV = new Vector4(1, 1, 0, 0);

                            vertexB = scene.mesh.Vertices[t2.b];
                            vertexB.UV = new Vector4(0, 1, 0, 0);

                            vertexC = scene.mesh.Vertices[t2.c];
                            vertexC.UV = new Vector4(1, 0, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);
                        }
                        break;
                    case Face.FaceType.Right:
                        {
                            Triangle t1 = face.t_1;
                            vertexA = scene.mesh.Vertices[t1.a];
                            vertexA.UV = new Vector4(0, 0, 0, 0);

                            vertexB = scene.mesh.Vertices[t1.b];
                            vertexB.UV = new Vector4(1, 0, 0, 0);

                            vertexC = scene.mesh.Vertices[t1.c];
                            vertexC.UV = new Vector4(0, 1, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);

                            Triangle t2 = face.t_2;
                            vertexA = scene.mesh.Vertices[t2.a];
                            vertexA.UV = new Vector4(1, 1, 0, 0);

                            vertexB = scene.mesh.Vertices[t2.b];
                            vertexB.UV = new Vector4(0, 1, 0, 0);

                            vertexC = scene.mesh.Vertices[t2.c];
                            vertexC.UV = new Vector4(1, 0, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);
                        }
                        break;
                    case Face.FaceType.Up:
                        {
                            Triangle t1 = face.t_1;
                            vertexA = scene.mesh.Vertices[t1.a];
                            vertexA.UV = new Vector4(0, 0, 0, 0);

                            vertexB = scene.mesh.Vertices[t1.b];
                            vertexB.UV = new Vector4(1, 0, 0, 0);

                            vertexC = scene.mesh.Vertices[t1.c];
                            vertexC.UV = new Vector4(0, 1, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);

                            Triangle t2 = face.t_2;
                            vertexA = scene.mesh.Vertices[t2.a];
                            vertexA.UV = new Vector4(1, 1, 0, 0);

                            vertexB = scene.mesh.Vertices[t2.b];
                            vertexB.UV = new Vector4(0, 1, 0, 0);

                            vertexC = scene.mesh.Vertices[t2.c];
                            vertexC.UV = new Vector4(1, 0, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);
                        }
                        break;
                    case Face.FaceType.Behind:
                        {
                            Triangle t1 = face.t_1;
                            vertexA = scene.mesh.Vertices[t1.a];
                            vertexA.UV = new Vector4(0, 0, 0, 0);

                            vertexB = scene.mesh.Vertices[t1.b];
                            vertexB.UV = new Vector4(1, 0, 0, 0);

                            vertexC = scene.mesh.Vertices[t1.c];
                            vertexC.UV = new Vector4(0, 1, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);

                            Triangle t2 = face.t_2;
                            vertexA = scene.mesh.Vertices[t2.a];
                            vertexA.UV = new Vector4(1, 1, 0, 0);

                            vertexB = scene.mesh.Vertices[t2.b];
                            vertexB.UV = new Vector4(0, 1, 0, 0);

                            vertexC = scene.mesh.Vertices[t2.c];
                            vertexC.UV = new Vector4(1, 0, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);
                        }
                        break;
                    case Face.FaceType.Below:
                        {
                            Triangle t1 = face.t_1;
                            vertexA = scene.mesh.Vertices[t1.a];
                            vertexA.UV = new Vector4(0, 0, 0, 0);

                            vertexB = scene.mesh.Vertices[t1.b];
                            vertexB.UV = new Vector4(1, 0, 0, 0);

                            vertexC = scene.mesh.Vertices[t1.c];
                            vertexC.UV = new Vector4(0, 1, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);

                            Triangle t2 = face.t_2;
                            vertexA = scene.mesh.Vertices[t2.a];
                            vertexA.UV = new Vector4(1, 1, 0, 0);

                            vertexB = scene.mesh.Vertices[t2.b];
                            vertexB.UV = new Vector4(0, 1, 0, 0);

                            vertexC = scene.mesh.Vertices[t2.c];
                            vertexC.UV = new Vector4(1, 0, 0, 0);

                            renderTriangle(vertexA, vertexB, vertexC, matrixMVP);
                        }
                        break;
                }

            }

        }

        public void renderTriangle(Vertex vertexA, Vertex vertexB, Vertex vertexC , Matrix4x4 matrixMVP)
        {

                List<Vertex> pIn = new List<Vertex>();

                vertexA.ClipSpacePosition = this.ClipSpace(vertexA.Position, matrixMVP);
                vertexB.ClipSpacePosition = this.ClipSpace(vertexB.Position, matrixMVP);
                vertexC.ClipSpacePosition = this.ClipSpace(vertexC.Position, matrixMVP);
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
                    //DrawLine(vertexA, vertexB, pixelA, pixelB, scene);
                    //DrawLine(vertexB, vertexC, pixelB, pixelC, scene);
                    for (int i = 0; i < vtList.Count; i++)
                    {
                        if (!ShouldBackFaceCull(oriVt))
                        {
                            int length = vtList[i].Vertices.Length;
                            Vertex start = vtList[i].Vertices[length - 1];
                            for (int j = 0; j < length; j++)
                            {
                                Vector4 viewPortA = this.ViewPort(start.ClipSpacePosition);
                                Vector4 viewPortB = this.ViewPort(vtList[i].Vertices[j].ClipSpacePosition);
                                DrawLine(start, vtList[i].Vertices[j], viewPortA, viewPortB, scene);
                                //DrawDDALine(start, vtList[i].Vertices[j]);
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
                        //Console.WriteLine(vtList.Count);
                        DrawTriangle(vtList[i], oriVt, scene);
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
        public void DrawTriangles(TriangleModel triangleModel)
        {
            Vertex[] vectex = triangleModel.Vertices;
            //DrawDDALine(new Vector2(vectex[0].Position.X, vectex[0].Position.Y), new Vector2(vectex[1].Position.X, vectex[1].Position.Y), vectex[0].Color);
            //DrawDDALine(new Vector2(vectex[1].Position.X, vectex[1].Position.Y), new Vector2(vectex[2].Position.X, vectex[2].Position.Y), vectex[1].Color);
            //DrawDDALine(new Vector2(vectex[2].Position.X, vectex[2].Position.Y), new Vector2(vectex[0].Position.X, vectex[0].Position.Y), vectex[2].Color);
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
            float nDotL1 = scene.light.ComputeNDotL(v1.Position, v1.Normal);
            float nDotL2 = scene.light.ComputeNDotL(v2.Position, v2.Normal);
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
                if (scene.light.IsEnable)
                {
                    Color4 c1 = scene.light.GetFinalLightColor(nDotL1);
                    Color4 c2 = scene.light.GetFinalLightColor(nDotL2);
                    lightColor = MathUtil.ColorInterp(c1, c2, ratio);
                }

                vertexColor = MathUtil.ColorInterp(v1.Color, v2.Color, ratio);

                float z = MathUtil.Interp(z1, z2, ratio);
                //PutPixel((int)x, (int)(y + 0.5), color);
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
        public void PutPixel(int x,int y, Color4 finalColor)
        {
            Color color = Color.FromArgb(finalColor.red, finalColor.blue, finalColor.green);

            this.bmp.SetPixel(x, y, color);
        }


	}
}
