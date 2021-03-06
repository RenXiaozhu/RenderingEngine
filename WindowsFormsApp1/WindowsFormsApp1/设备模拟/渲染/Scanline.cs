﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class ScanLine
    {
        private Device device;
        private Edge[] ET;
        private Edge AEL;
        private int height;
        private Color4 final;

        public ScanLine(Device device)
        {
            this.device = device;
            this.height = device.GetHeight();
            this.ET = new Edge[this.height];
            for (int i = 0; i < this.height; i++)
            {
                this.ET[i] = new Edge();
            }
            final = new Color4(255, 255, 255);
        }

		//顶点排序
        public Vertex[] GetList(Vertex[] list)
        {
            Vertex A = list[0];
            Vertex B = list[1];
            Vertex C = list[2];

            Vertex[] vt = new Vertex[3];
            float ay = A.ScreenSpacePosition.Y;
            float by = B.ScreenSpacePosition.Y;
            float cy = C.ScreenSpacePosition.Y;

            float ax = A.ScreenSpacePosition.X;
            float bx = B.ScreenSpacePosition.X;
            float cx = C.ScreenSpacePosition.X;

			if (ay == by)
			{
				if (cy < ay)
				{
					vt[0] = C;
					if (ax < bx)
					{
						vt[1] = A;
						vt[2] = B;
					}
					else
					{
						vt[1] = B;
						vt[2] = A;
					}
				}
				else
				{
					vt[2] = C;
					if (ax < bx)
					{
						vt[0] = A;
						vt[1] = B;
					}
					else
					{
						vt[0] = B;
						vt[1] = A;
					}
				}
				return vt;
			}

			if (ay == cy)
			{
				if (by < ay)
				{
					vt[0] = B;
					if (ax < cx)
					{
						vt[1] = A;
						vt[2] = C;
					}
					else
					{
						vt[1] = C;
						vt[2] = A;
					}
				}
				else
				{
					vt[2] = B;
					if (ax < cx)
					{
						vt[0] = A;
						vt[1] = C;
					}
					else
					{
						vt[0] = C;
						vt[1] = A;
					}
				}

				return vt;
			}
			if (by == cy)
			{
				if (ay < cy)
				{
					vt[0] = A;
					if (cx < bx)
					{
						vt[1] = C;
						vt[2] = B;
					}
					else
					{
						vt[1] = B;
						vt[2] = C;
					}
				}
				else
				{
					vt[2] = A;
					if (bx < cx)
					{
						vt[0] = B;
						vt[1] = C;
					}
					else
					{
						vt[0] = C;
						vt[1] = B;
					}
				}

				return vt;
			}
			if (ay < by && ay < cy)
            {
                vt[0] = A;
                if (by < cy)
                {
                    vt[1] = B;
                    vt[2] = C;
                }
                else
                {
                    vt[1] = C;
                    vt[2] = B;
                }
            }
            if (by < cy && by < ay)
            {
                vt[0] = B;
				if (ay < cy)
				{
					vt[1] = A;
					vt[2] = C;
				}
				else
				{
					vt[1] = C;
					vt[2] = A;
				}
            }

            if (cy < by && cy < ay)
            {
                vt[0] = C;
                if (by < ay)
                {
                    vt[1] = B;
                    vt[2] = A;
                }
                else
                {
                    vt[1] = A;
                    vt[2] = B;
                }
            }

            return vt;
        }

        /* 重心坐标填充
		 * 填充效果明显，但基于精度问题，会有漏点
		 * 切效率低，不建议使用
		*/
        public void StartScanLine(VertexTriangle vt, VertexTriangle oriVt, Scene scene)
        {
            float yMin = this.height;
            float yMax = 0;
            Vertex[] vv = GetList(vt.Vertices);
            Vertex A = vv[0];
            Vertex B = vv[1];
            Vertex C = vv[2];

            float nDotLA1V1 = DirectionLight.ComputeNDotL(A.nowPos, A.nowNormal);
            A.lightColor = A.Color * DirectionLight.GetFinalLightColor(nDotLA1V1);

            float nDotLA1V2 = DirectionLight.ComputeNDotL(B.nowPos, B.nowNormal);
            B.lightColor = B.Color * DirectionLight.GetFinalLightColor(nDotLA1V2);

            float nDotLA2V1 = DirectionLight.ComputeNDotL(C.nowPos, C.nowNormal);
            C.lightColor = C.Color * DirectionLight.GetFinalLightColor(nDotLA2V1);

            float ay = A.ScreenSpacePosition.Y;
            float by = B.ScreenSpacePosition.Y;
            float cy = C.ScreenSpacePosition.Y;

            float ax = A.ScreenSpacePosition.X;
            float bx = B.ScreenSpacePosition.X;
            float cx = C.ScreenSpacePosition.X;

            float az = A.ScreenSpacePosition.Z;
            float bz = B.ScreenSpacePosition.Z;
            float cz = C.ScreenSpacePosition.Z;

            yMin = Math.Min(Math.Min(ay, by), Math.Min(by, cy));
            yMax = Math.Max(Math.Max(ay, by), Math.Max(by, cy));
            float xMin = (int)Math.Min(Math.Min(ax, bx), Math.Min(bx, cx));
            float xMax = (int)Math.Max(Math.Max(ax, bx), Math.Max(bx, cx));

            vt.Preproccess();

            float dtx = 0;
            float dty = 0;
            if (yMax - yMin > xMax - xMin)
            {
                dtx = (xMax - xMin) / (yMax - yMin);
                dty = 1;
            }
            else
            {
                dtx = 1;
                dty = (yMax - yMin) / (xMax - xMin);
            }

            for (int y = (int)yMin; y < yMax; y++)
            {
                for (int x = (int)xMin; x < xMax; x++)
                {

                    float z = vt.GetInterValue(az, bz, cz);

                    Padding.GouraudColor(scene, device, x, y, z, vt, oriVt);
                }
            }
        }
        /* 边表法填充
		 * 优点 扫描效率高，结构清晰
		 * 缺点 光栅化时三角形斜边在水平情况下差值不均匀造成颜色区块
		 * vt是裁剪后的三角形，
         * orivt是原始三角形,目的是在纹理映射的时候计算权重
         * 
         */
        public void ProcessScanLine(VertexTriangle vt, VertexTriangle oriVt, Scene scene, bool isworld)
        {
            int yMin = this.height;
            int yMax = 0;

            Vertex A = vt.Vertices[0];
            Vertex B = vt.Vertices[1];
            Vertex C = vt.Vertices[2];

            /* 扫描线原理
             * 
             * 1. 求扫描线与多边形的交点
             * 2. 对所求得的交点按 x 从小到大排序
             * 3. 将交点两两配对，并填充每一区段
             * 
             */
            // ET AEL 扫描算法见 72 页 4.2.2
            /* 1. 找到三角形 y 的最大值和最小值  
             * 并将三角形的边以顶点坐标X的值从左往右(x 从小到大)填充
             * 在边表 ET[] 中对应三角形的Ymin处
            */

            Vertex[] vertices = vt.Vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                for (int j = i + 1; j < vertices.Length; j++)
                {
                    Vector4 screen1 = vertices[i].ScreenSpacePosition;
                    Vector4 screen2 = vertices[j].ScreenSpacePosition;

                    if ((int)screen1.Y != (int)screen2.Y)
                    {
                        if (screen1.Y > yMax) yMax = (int)screen1.Y;
                        if (screen2.Y > yMax) yMax = (int)screen2.Y;
                        if (screen1.Y < yMin) yMin = (int)screen1.Y;
                        if (screen2.Y < yMin) yMin = (int)screen2.Y;
                        if (yMax > this.height) yMax = this.height;
                        if (yMin < 0) yMin = 0;

                        int x1 = (int)screen1.X;
                        int y1 = (int)screen1.Y;
                        int x2 = (int)screen2.X;
                        int y2 = (int)screen2.Y;
                        int ymin = y1 > y2 ? y2 : y1;
                        if (ymin < 0) ymin = 0;
                        int ymax = y1 > y2 ? y1 : y2;
                        if (ymax > this.height) ymax = this.height;

                        // 保存的边的上端点x坐标
                        float x = y1 > y2 ? x2 : x1;

                        //边的斜率的倒数
                        float dx = (float)(x1 - x2) * 1.0f / (float)(y1 - y2);

                        Edge e = new Edge();
                        e.yMax = ymax;

                        // 保存x的目的是为了插入时保证边的顺序是按照x从小到大插入
                        // 在AEL中表示当前扫描线与边的交点x坐标，初值(在ET中的值)为边的下端点的x坐标
                        e.x = x;
                        // 斜率是保证在插入时，如果 x相同，以斜率大小比较
                        e.deltaX = dx;
                        // 保存边的上端点
                        e.v1 = y1 > y2 ? vertices[j] : vertices[i];
                        // 保存边的下端点
                        e.v2 = y1 > y2 ? vertices[i] : vertices[j];

                        EdgeTable.InsertEdge(ref ET[ymin].nextEdge, e);

                    }
                }
            }

            /* 2. 置空活动边表 AEL
             * AEL 存储的是 ET表在 扫描线 y = i 有交点的三角形的边
             */
            AEL = new Edge();

            oriVt.PreCalWeight();

            for (int i = yMin; i < yMax; i++)
            {
                // 将边表中在 y = i 的边 插入活动边表AEL中，并删除边表里的边
                EdgeTable.SearchTable(i, ET, AEL);

                if (AEL.nextEdge == null) continue;

                // 获取连续的两条边
                Edge a1 = (Edge)AEL.nextEdge.Clone();
                Edge a2 = (Edge)AEL.nextEdge.nextEdge.Clone();

                // 从ymin开始扫描填充
                /*
                 * 记过排序后的边传入连续两条边 相交的情况只有两种钝角三角形 和锐角三角形
                 * x 从小到大
                 *            
                 *        /\    /\
                 *       /  \  /  \
                 *      /    \/    \_________
                 *     x1    x3 x4
                 * 1. x左右分开
                 * 2. x相同 以斜率排序
                 * 
                */

                /* 双线性插值算法
                 * 斜边一次差值找出交点
				 * 水平扫描差值找出当前点
                */
                Vector4 screenA1V1 = a1.v1.ScreenSpacePosition;
                Vector4 screenA1V2 = a1.v2.ScreenSpacePosition;
                Vector4 screenA2V1 = a2.v1.ScreenSpacePosition;
                Vector4 screenA2V2 = a2.v2.ScreenSpacePosition;

                // 第一次插值算出交点的颜色系数
                float r1 = ((float)i - screenA1V1.Y) / (float)(screenA1V2.Y - screenA1V1.Y);
                float r2 = ((float)i - screenA2V1.Y) / (float)(screenA2V2.Y - screenA2V1.Y);

                r1 = MathUtil.Clamp01(r1);
                r2 = MathUtil.Clamp01(r2);

                // 像素点深度插值
                float z1 = MathUtil.Interp(screenA1V1.Z, screenA1V2.Z, r1);
                float z2 = MathUtil.Interp(screenA2V1.Z, screenA2V2.Z, r2);

                float nDotLA1V1 = 0, nDotLA1V2 = 0, nDotLA2V1 = 0, nDotLA2V2 = 0, nDotL1 = 0, nDotL2 = 0;
                if(DirectionLight.IsEnable)
                {
                    nDotLA1V1 = DirectionLight.ComputeNDotL(a1.v1.nowPos, a1.v1.nowNormal);
                    nDotLA1V2 = DirectionLight.ComputeNDotL(a1.v2.nowPos, a1.v2.nowNormal);
                    nDotLA2V1 = DirectionLight.ComputeNDotL(a2.v1.nowPos, a2.v1.nowNormal);
                    nDotLA2V2 = DirectionLight.ComputeNDotL(a2.v2.nowPos, a2.v2.nowNormal);
                    // 双线性插值系数

                    nDotL1 = MathUtil.Interp(nDotLA1V1, nDotLA1V2, r1);
                    nDotL2 = MathUtil.Interp(nDotLA2V1, nDotLA2V2, r2);
                }

                // 横向填充
                while (a1 != null && a2 != null)
                {
                    for (int x = (int)AEL.nextEdge.x; x < (int)AEL.nextEdge.nextEdge.x; x++)
                    {
                        float r3 = MathUtil.Clamp01(((float)x - a1.x) / (a2.x - a1.x));
                        float z = MathUtil.Interp(z1, z2, r3);

                        switch (scene.renderState)
                        {
                            case Scene.RenderState.WireFrame:
                                {
                                }
                                break;
                            case Scene.RenderState.GouraduShading:
                                {
									//Padding.x = x;
									//Padding.GouraudColor(scene,device,x,i,z,vt,oriVt);
                                }
                                break;
                            case Scene.RenderState.TextureMapping:
                                {
                                    oriVt.CalWeight(new Vector4(x, i, 0, 0));
                                    Vector4 uv = oriVt.GetInterUV();
                                    if (isworld)
                                    {
                                        final = device.Tex2D(uv.X, uv.Y, scene.worldMap.texture);
                                    }
                                    else
                                    {
                                        if (DirectionLight.IsEnable)
                                        {
                                            float nDotL = MathUtil.Interp(nDotL1, nDotL2, r3);
                                            final = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
                                            final = final * DirectionLight.GetFinalLightColor(nDotL);
                                        }
                                        else
                                        {
                                            final = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
                                        }
                                    }
                                }
                                break;
                        }
                        device.DrawPoint(new Vector4(x, i, z, 0), final);
                    }

                    if (a2.nextEdge != null)
                    {
                        a1 = (Edge)a2.nextEdge.Clone();
                        a2 = (Edge)a1.nextEdge.Clone();
                    }
                    else
                    {
                        break;
                    }
                }

                // 删除y=yMax-1的边 ， 边的特性是上开下闭， 所以 当扫描到y = yamx后，ymax以前的边和扫描线不会再有交点
                // 所以要删除，避免盲目求交
                // 同时算出 x 的下一个坐标 并保存在 AEL的第一个边中 p.nextEdge.x += p.nextEdge.deltaX;
                EdgeTable.DeleteEdge(AEL, i);
            }

        }

		//三角形扫描
        public void ScanLine_new(VertexTriangle vt,VertexTriangle orit, Scene scene)
        {
            Vertex A = vt.Vertices[0];
            Vertex B = vt.Vertices[1];
            Vertex C = vt.Vertices[2];

            DrawNormalTriangle(vt,orit, scene);
        }

        /* 三角形扫描
		 * 搜索三角形光栅化
		 * __________  top
		 * \            /
		 *   \        /
		 *     \    /
		 *       \/
		 *          bottom
		 *       /\
		 *     /    \
		 *   /        \
		 *  ---------
		 */
        public void DrawNormalTriangle(VertexTriangle vt,VertexTriangle orit,Scene scene)
        {
            Vertex[] vv = GetList(vt.Vertices);

            Vertex v1 = vv[0];
            Vertex v2 = vv[1];
            Vertex v3 = vv[2];
			if (v1 == null)
				return;
            Vector4 p1 = v1.ScreenSpacePosition;
            Vector4 p2 = v2.ScreenSpacePosition;
            Vector4 p3 = v3.ScreenSpacePosition;

            float ax = p1.X;
            float bx = p2.X;
            float cx = p3.X;
            float ay = p1.Y;
            float by = p2.Y;
            float cy = p3.Y;

            float az = p1.Z;
            float bz = p2.Z;
            float cz = p3.Z;

            Color4 color1 = v1.Color;
            Color4 color2 = v2.Color;
            Color4 color3 = v3.Color;

            Vector4 normal1 = v1.nowNormal;
            Vector4 normal2 = v2.nowNormal;
            Vector4 normal3 = v3.nowNormal;

            if (ay == by)
            {
                DrawTopTriangle(v1, v2, v3,scene,vt,orit);
            }
            else if (by == cy)
            {
                DrawBottomTriangle(v1, v2, v3,scene,vt,orit);
            }
            else
            {
                if (ax == cx)
                {
                    Vertex newVt = new Vertex();

                    float y1 = (by - ay) / (cy - ay);

                    y1 = MathUtil.Clamp01(y1);

                    float x = MathUtil.Interp(ax, cx, y1);

                    float z = MathUtil.Interp(az, cz, y1);

                    Color4 c = MathUtil.ColorInterp(color1, color3, y1);

                    newVt.ScreenSpacePosition = new Vector4(x, by, z, 0);

                    newVt.lightColor = c;

                    newVt.Color = c;

                    newVt.nowNormal = MathUtil.Vector4Interp(normal1, normal3, y1);

					newVt.UV = MathUtil.Vector4Interp(v1.UV, v3.UV, y1);

					if ((int)bx > (int)cx)
                    {
                        DrawBottomTriangle(v1, newVt, v2,scene,vt,orit);

                        DrawTopTriangle(newVt, v2, v3,scene,vt,orit);
                    }
                    else
                    {
                        DrawBottomTriangle(v1, v2, newVt,scene,vt,orit);

                        DrawTopTriangle(v2, newVt, v3,scene,vt,orit);
                    }
                }
                else if (ax == bx)
                {
                    Vertex newVt = new Vertex();

                    float y1 = (by - ay) / (cy - ay);

                    y1 = MathUtil.Clamp01(y1);

                    float x = MathUtil.Interp(ax, cx, y1);

                    float z = MathUtil.Interp(az, cz, y1);

                    Color4 c = MathUtil.ColorInterp(color1, color3, y1);

                    newVt.ScreenSpacePosition = new Vector4(x, by, z, 0);

                    newVt.lightColor = c;

                    newVt.Color = c;

                    newVt.nowNormal = MathUtil.Vector4Interp(normal1, normal3, y1);

					newVt.UV = MathUtil.Vector4Interp(v1.UV, v3.UV, y1);

					if (cx < bx)
                    {
                        DrawBottomTriangle(v1, newVt, v2,scene,vt,orit);

                        DrawTopTriangle(newVt, v2, v3,scene,vt,orit);
                    }
                    else
                    {
                        DrawBottomTriangle(v1, v2, newVt,scene,vt,orit);

                        DrawTopTriangle(v2, newVt, v3,scene,vt,orit);
                    }
                }
                else if (bx == cx)
                {

                    Vertex newVt = new Vertex();

                    float y1 = (by - ay) / (cy - ay);

                    y1 = MathUtil.Clamp01(y1);

                    float x = MathUtil.Interp(ax, cx, y1);

                    float z = MathUtil.Interp(az, cz, y1);

                    Color4 c = MathUtil.ColorInterp(color1, color3, y1);

                    newVt.ScreenSpacePosition = new Vector4(x, by, z, 0);

                    newVt.lightColor = c;

                    newVt.Color = c;

                    newVt.nowNormal = MathUtil.Vector4Interp(normal1, normal3, y1);

					newVt.UV = MathUtil.Vector4Interp(v1.UV, v3.UV, y1);

					if (ax < bx)
                    {
                        DrawBottomTriangle(v1, newVt, v2,scene,vt,orit);

                        DrawTopTriangle(newVt, v2, v3,scene,vt,orit);
                    }
                    else
                    {
                        DrawBottomTriangle(v1, v2, newVt,scene,vt,orit);

                        DrawTopTriangle(v2, newVt, v3,scene,vt,orit);
                    }
                }
                else
                {
                    Vertex newVt = new Vertex();

                    float y1 = (float)(by - ay) / (float)(cy - ay);

                    y1 = MathUtil.Clamp01(y1);

                    float x = MathUtil.Interp(ax, cx, y1);

                    float z = MathUtil.Interp(az, cz, y1);

                    Color4 c = MathUtil.ColorInterp(color1, color3, y1);

                    newVt.ScreenSpacePosition = new Vector4(x, by, z, 0);

                    newVt.lightColor = c;

                    newVt.Color = c;

                    newVt.nowNormal = MathUtil.Vector4Interp(normal1, normal3, y1);

					newVt.UV = MathUtil.Vector4Interp(v1.UV, v3.UV, y1);

                    // 计算线条的方向  
                    float dP1P2, dP1P3;

                    // http://en.wikipedia.org/wiki/Slope  
                    // 计算斜率  
                    if (p2.Y - p1.Y > 0)
                        dP1P2 = (p2.X - p1.X) / (p2.Y - p1.Y);
                    else
                        dP1P2 = 0;

                    if (p3.Y - p1.Y > 0)
                        dP1P3 = (p3.X - p1.X) / (p3.Y - p1.Y);
                    else
                        dP1P3 = 0;

                    if (dP1P2 > dP1P3)
                    {
                        DrawBottomTriangle(v1, newVt, v2,scene,vt,orit);

                        DrawTopTriangle(newVt, v2, v3,scene,vt,orit);
                    }
                    else
                    {
                        DrawBottomTriangle(v1, v2, newVt,scene,vt,orit);

                        DrawTopTriangle(v2, newVt, v3,scene,vt,orit);
                    }
                }
            }
        }

        public void DrawTopTriangle(Vertex v1, Vertex v2, Vertex v3, Scene scene,VertexTriangle vt, VertexTriangle oriVt)
        {
            Vector4 p1 = v1.ScreenSpacePosition;
            Vector4 p2 = v2.ScreenSpacePosition;
            Vector4 p3 = v3.ScreenSpacePosition;

            Color4 color1 = v1.Color;
            Color4 color2 = v2.Color;
            Color4 color3 = v3.Color;

            float ax = p1.X;
            float bx = p2.X;
            float cx = p3.X;
            float ay = p1.Y;
            float by = p2.Y;
            float cy = p3.Y;

            float az = p1.Z;
            float bz = p2.Z;
            float cz = p3.Z;

            Vector4 normal1 = v1.nowNormal;
            Vector4 normal2 = v2.nowNormal;
            Vector4 normal3 = v3.nowNormal;

            oriVt.PreCalWeight();

            if (bx == cx)
            {
                for (int y = (int)ay; y <= (int)cy; y++)
                {
                    float y1 = (float)(y - ay) / (float)(cy - ay);
                    float y2 = (float)(y - by) / (float)(cy - by);

                    y1 = MathUtil.Clamp01(y1);
                    y2 = MathUtil.Clamp01(y2);

                    int x0 = (int)MathUtil.Interp(ax, cx, y1);
                    int x1 = (int)bx;

                    float z1 = MathUtil.Interp(az, cz, y1);
                    float z2 = bz;

					Color4 c1 = new Color4();
					Color4 c2 = new Color4();

					if (scene.renderState == Scene.RenderState.GouraduShading)
					{
						c1 = MathUtil.ColorInterp(color1, color3, y1);
						c2 = MathUtil.ColorInterp(color2, color3, y2);
					}

					Vector4 n1 = new Vector4(0, 0, 0, 0);
                    Vector4 n2 = new Vector4(0, 0, 0, 0);

                    if (DirectionLight.IsEnable)
                    {
                        n1 = MathUtil.Vector4Interp(normal1, normal3, y1);
                        n2 = MathUtil.Vector4Interp(normal2, normal3, y2);
                    }

                    for (int x = x0; x <= x1; x++)
                    {
                        float r3 = (float)(x - x0) / (float)(x1 - x0);

                        r3 = MathUtil.Clamp01(r3);

                        float z = MathUtil.Interp(z1, z2, r3);

                        Vector4 pos = new Vector4(x, y, z, 0);

                        switch (scene.renderState)
                        {
                            case Scene.RenderState.WireFrame:
                                { }
                                break;
                            case Scene.RenderState.GouraduShading:
                                {
									oriVt.CalWeight(pos);
									Color4 c3 = MathUtil.ColorInterp(c1, c2, r3);

                                    if (DirectionLight.IsEnable)
                                    {
                                        Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);
										n3 = oriVt.GetNormal();
										final = DirectionLight.GetFinalLightColor(n3, c3);
                                    }
                                    else
                                    {
                                        final = c3;
                                    }
                                }
                                break;
                            case Scene.RenderState.TextureMapping:
                                {
                                    oriVt.CalWeight(pos);

                                    Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);

                                    Vector4 uv = oriVt.GetInterUV();

                                    if (DirectionLight.IsEnable)
                                    {
                                        Color4 c = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
										n3 = oriVt.GetNormal();
										final = DirectionLight.GetFinalLightColor(n3, c);
                                    }
                                    else
                                    {
                                        final = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
                                    }
                                }
                                break;
                        }
                        device.DrawPoint(pos, final);
                    }
                }

            }
            else if (ax == cx)
            {
                for (int y = (int)ay; y <= (int)cy; y++)
                {
                    float y1 = (float)(y - ay) / (float)(cy - ay);
                    float y2 = (float)(y - by) / (float)(cy - by);
                    y1 = MathUtil.Clamp01(y1);
                    y2 = MathUtil.Clamp01(y2);

                    int x0 = (int)ax;
                    int x1 = (int)MathUtil.Interp(bx, cx, y2);

                    float z1 = MathUtil.Interp(az, cz, y1);
                    float z2 = MathUtil.Interp(bz, cz, y2);

					Color4 c1 = new Color4();
					Color4 c2 = new Color4();

					if (scene.renderState == Scene.RenderState.GouraduShading)
					{
						c1 = MathUtil.ColorInterp(color1, color3, y1);
						c2 = MathUtil.ColorInterp(color2, color3, y2);
					}

					Vector4 n1 = new Vector4(0, 0, 0, 0);
                    Vector4 n2 = new Vector4(0, 0, 0, 0);

                    if (DirectionLight.IsEnable)
                    {
                        n1 = MathUtil.Vector4Interp(normal1, normal3, y1);
                        n2 = MathUtil.Vector4Interp(normal2, normal3, y2);
                    }

                    for (int x = x0; x < x1; x++)
                    {
                        float r3 = (float)(x - x0) / (float)(x1 - x0);

                        r3 = MathUtil.Clamp01(r3);

                        float z = MathUtil.Interp(z1, z2, r3);

						Vector4 pos = new Vector4(x, y, z, 0);

						switch (scene.renderState)
						{
							case Scene.RenderState.WireFrame:
								{ }
								break;
							case Scene.RenderState.GouraduShading:
								{
									oriVt.CalWeight(pos);
									Color4 c3 = MathUtil.ColorInterp(c1, c2, r3);

									if (DirectionLight.IsEnable)
									{
										Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);
										n3 = oriVt.GetNormal();
										final = DirectionLight.GetFinalLightColor(n3, c3);
									}
									else
									{
										final = c3;
									}
								}
								break;
							case Scene.RenderState.TextureMapping:
								{
									oriVt.CalWeight(pos);

									Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);

									Vector4 uv = oriVt.GetInterUV();

									if (DirectionLight.IsEnable)
									{
										Color4 c = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
										n3 = oriVt.GetNormal();
										final = DirectionLight.GetFinalLightColor(n3, c);
									}
									else
									{
										final = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
									}
								}
								break;
						}
						device.DrawPoint(pos, final);
					}
                }
            }
            else
            {
                for (int y = (int)ay; y <= cy; y++)
                {
                    float y1 = (float)(y - ay) / (float)(cy - ay);
                    y1 = MathUtil.Clamp01(y1);

                    float y2 = (y - by) / (cy - by);
                    y2 = MathUtil.Clamp01(y2);

                    float x0 = (int)MathUtil.Interp(ax, cx, y1);
                    float x1 = (int)MathUtil.Interp(bx, cx, y2);

                    float z1 = MathUtil.Interp(az, cz, y1);
                    float z2 = MathUtil.Interp(bz, cz, y2);

                    Color4 c1 = new Color4();
                    Color4 c2 =new Color4();

					if (scene.renderState == Scene.RenderState.GouraduShading)
					{
						 c1 = MathUtil.ColorInterp(color1, color3, y1);
						 c2 = MathUtil.ColorInterp(color2, color3, y2);
					}
                    Vector4 n1 = new Vector4(0, 0, 0, 0);
                    Vector4 n2 = new Vector4(0, 0, 0, 0);

                    if (DirectionLight.IsEnable)
                    {
                        n1 = MathUtil.Vector4Interp(normal1, normal3, y1);
                        n2 = MathUtil.Vector4Interp(normal2, normal3, y2);
                    }

                    for (float x = x0; x < x1; x++)
                    {
                        float r3 = (float)(x - x0) / (float)(x1 - x0);

                        r3 = MathUtil.Clamp01(r3);

                        float z = MathUtil.Interp(z1, z2, r3);

                        //Color4 c3 = MathUtil.ColorInterp(c1, c2, r3);

                        Vector4 pos = new Vector4(x, y, z, 0);
                      
                        switch (scene.renderState)
                        {
                            case Scene.RenderState.WireFrame:
                                { }
                                break;
                            case Scene.RenderState.GouraduShading:
                                {
                                    Color4 c3 = MathUtil.ColorInterp(c1, c2, r3);
									oriVt.CalWeight(pos);
                                    if (DirectionLight.IsEnable)
                                    {
                                        Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);
										n3 = oriVt.GetNormal();
										final = DirectionLight.GetFinalLightColor(n3, c3);
                                    }
                                    else
                                    {
                                        final = c3;
                                    }
                                }
                                break;
                            case Scene.RenderState.TextureMapping:
                                {
                                    oriVt.CalWeight(pos);

                                    Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);

                                    Vector4 uv = oriVt.GetInterUV();

                                    if (DirectionLight.IsEnable)
                                    {
                                        Color4 c = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
										n3 = oriVt.GetNormal();
										final = DirectionLight.GetFinalLightColor(n3, c);
                                    }
                                    else
                                    {
                                        final = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
                                    }
                                }
                                break;
                        }
                        device.DrawPoint(pos, final);
                    }
                }
            }
        }

        public void DrawBottomTriangle(Vertex v1, Vertex v2, Vertex v3 ,Scene scene, VertexTriangle vt, VertexTriangle oriVt)
        {
            Vector4 p1 = v1.ScreenSpacePosition;
            Vector4 p2 = v2.ScreenSpacePosition;
            Vector4 p3 = v3.ScreenSpacePosition;

            Color4 color1 = v1.Color;
            Color4 color2 = v2.Color;
            Color4 color3 = v3.Color;

            float ax = p1.X;
            float bx = p2.X;
            float cx = p3.X;
            float ay = p1.Y;
            float by = p2.Y;
            float cy = p3.Y;

            float az = p1.Z;
            float bz = p2.Z;
            float cz = p3.Z;

            Vector4 normal1 = v1.nowNormal;
            Vector4 normal2 = v2.nowNormal;
            Vector4 normal3 = v3.nowNormal;
            oriVt.PreCalWeight();
            if (ax == bx)
            {
                for (int y = (int)ay; y <= (int)cy; y++)
                {
                    float y1 = (float)(y - ay) / (float)(by - ay);
                    y1 = MathUtil.Clamp01(y1);

                    float y2 = (y - ay) / (cy - ay);
                    y2 = MathUtil.Clamp01(y2);

                    int x0 = (int)ax;
                    int x1 = (int)MathUtil.Interp(ax, cx, y2);

                    float z1 = az;
                    float z2 = MathUtil.Interp(az, cz, y2);

                    Color4 c1 = new Color4();
                    Color4 c2 = new Color4();
					if (scene.renderState == Scene.RenderState.GouraduShading)
					{
						 c1 = MathUtil.ColorInterp(color1, color2, y1);
						 c2 = MathUtil.ColorInterp(color1, color3, y2);
					}
                    Vector4 n1 = new Vector4(0, 0, 0, 0);
                    Vector4 n2 = new Vector4(0, 0, 0, 0);

                    if (DirectionLight.IsEnable)
                    {
                        n1 = MathUtil.Vector4Interp(normal1, normal2, y1);
                        n2 = MathUtil.Vector4Interp(normal1, normal3, y2);
                    }

                    for (int x = x0; x < x1; x++)
                    {
                        float r3 = (float)(x - x0) / (float)(x1 - x0);

                        r3 = MathUtil.Clamp01(r3);

                        float z = MathUtil.Interp(z1, z2, r3);

                        //Color4 c3 = MathUtil.ColorInterp(c1, c2, r3);

                        Vector4 pos = new Vector4(x, y, z, 0);

                        switch (scene.renderState)
                        {
                            case Scene.RenderState.WireFrame:
                                { }
                                break;
                            case Scene.RenderState.GouraduShading:
                                {
									oriVt.CalWeight(pos);
									Color4 c3 = MathUtil.ColorInterp(c1, c2, r3);
									
                                    if (DirectionLight.IsEnable)
                                    {
                                        Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);
										n3 = oriVt.GetNormal();
                                        final = DirectionLight.GetFinalLightColor(n3, c3);
                                    }
                                    else
                                    {
                                        final = c3;
                                    }
                                }
                                break;
                            case Scene.RenderState.TextureMapping:
                                {
                                    oriVt.CalWeight(pos);

                                    Vector4 uv = oriVt.GetInterUV();

                                    if (DirectionLight.IsEnable)
                                    {
										Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);
										n3 = oriVt.GetNormal();
										Color4 c = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);

                                        final = DirectionLight.GetFinalLightColor(n3, c);
                                    }
                                    else
                                    {
                                        final = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
                                    }
                                }
                                break;
                        }
                        device.DrawPoint(pos, final);
                    }
                }

            }
            else if (ax == cx)
            {
                for (int y = (int)ay; y <= (int)cy; y++)
                {
                    float y1 = (float)(y - ay) / (float)(by - ay);
                    y1 = MathUtil.Clamp01(y1);
                    float y2 = (y - ay) / (cy - ay);
                    y2 = MathUtil.Clamp01(y2);

                    int x0 = (int)MathUtil.Interp(ax, bx, y1);
                    int x1 = (int)ax;

                    float z1 = MathUtil.Interp(az, bz, y1);
                    float z2 = MathUtil.Interp(az, cz, y2);

					Color4 c1 = new Color4();
					Color4 c2 = new Color4();
					if (scene.renderState == Scene.RenderState.GouraduShading)
					{
						c1 = MathUtil.ColorInterp(color1, color2, y1);
						c2 = MathUtil.ColorInterp(color1, color3, y2);
					}

					Vector4 n1 = new Vector4(0, 0, 0, 0);
                    Vector4 n2 = new Vector4(0, 0, 0, 0);

                    if (DirectionLight.IsEnable)
                    {
                        n1 = MathUtil.Vector4Interp(normal1, normal2, y1);
                        n2 = MathUtil.Vector4Interp(normal1, normal3, y2);
                    }

                    for (int x = x0; x < x1; x++)
                    {
                        float r3 = (float)(x - x0) / (float)(x1 - x0);

                        r3 = MathUtil.Clamp01(r3);

                        float z = MathUtil.Interp(z1, z2, r3);

                        //Color4 c3 = MathUtil.ColorInterp(c1, c2, r3);

                        Vector4 pos = new Vector4(x, y, z, 0);

                        switch (scene.renderState)
                        {
                            case Scene.RenderState.WireFrame:
                                { }
                                break;
                            case Scene.RenderState.GouraduShading:
                                {
									oriVt.CalWeight(pos);
									Color4 c3 = MathUtil.ColorInterp(c1, c2, r3);

                                    if (DirectionLight.IsEnable)
                                    {
                                        Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);
										n3 = oriVt.GetNormal();
										final = DirectionLight.GetFinalLightColor(n3, c3);
                                    }
                                    else
                                    {
                                        final = c3;
                                    }
                                }
                                break;
                            case Scene.RenderState.TextureMapping:
                                {
                                    oriVt.CalWeight(pos);

                                    Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);
									n3 = oriVt.GetNormal();
									Vector4 uv = oriVt.GetInterUV();

                                    if (DirectionLight.IsEnable)
                                    {
                                        Color4 c = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);

                                        final = DirectionLight.GetFinalLightColor(n3, c);
                                    }
                                    else
                                    {
                                        final = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
                                    }
                                }
                                break;
                        }
                        device.DrawPoint(pos, final);
                    }
                }
            }
            else
            {
                for (int y = (int)ay; y <= (int)cy; y++)
                {
                    float y1 = (float)(y - ay) / (float)(by - ay);
                    y1 = MathUtil.Clamp01(y1);

                    float y2 = (y - ay) / (cy - ay);
                    y2 = MathUtil.Clamp01(y2);

                    int x0 = (int)MathUtil.Interp(ax, bx, y1);
                    int x1 = (int)MathUtil.Interp(ax, cx, y2);

                    float z1 = MathUtil.Interp(az, bz, y1);
                    float z2 = MathUtil.Interp(bz, cz, y2);

					Color4 c1 = new Color4();
					Color4 c2 = new Color4();

					if (scene.renderState == Scene.RenderState.GouraduShading)
					{
						c1 = MathUtil.ColorInterp(color1, color2, y1);
						c2 = MathUtil.ColorInterp(color1, color3, y2);
					}
					Vector4 n1 = new Vector4(0, 0, 0, 0);
                    Vector4 n2 = new Vector4(0, 0, 0, 0);

                    if(DirectionLight.IsEnable)
                    {
                        n1 = MathUtil.Vector4Interp(normal1, normal2, y1);
                        n2 = MathUtil.Vector4Interp(normal1, normal3, y2);
                    }


                    for (int x = x0; x < x1; x++)
                    {
                        
                        float r3 = (float)(x - x0) / (float)(x1 - x0);

                        r3 = MathUtil.Clamp01(r3);

                        float z = MathUtil.Interp(z1, z2, r3);

                        Vector4 pos = new Vector4(x, y, z, 0);

                        switch (scene.renderState)
                        {
                            case Scene.RenderState.WireFrame:
                                { }
                                break;
                            case Scene.RenderState.GouraduShading:
                                {
									oriVt.CalWeight(pos);
                                    Color4 c3 = MathUtil.ColorInterp(c1, c2, r3);

                                    if (DirectionLight.IsEnable)
                                    {
                                        Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);
										n3 = oriVt.GetNormal();
										final = DirectionLight.GetFinalLightColor(n3, c3);
                                    }
                                    else
                                    {
                                        final = c3;
                                    }
                                }
                                break;
                            case Scene.RenderState.TextureMapping:
                                {
                                    oriVt.CalWeight(pos);

                                    Vector4 n3 = MathUtil.Vector4Interp(n1, n2, r3);
									n3 = oriVt.GetNormal();
									Vector4 uv = oriVt.GetInterUV();

                                    if (DirectionLight.IsEnable)
                                    {
                                        Color4 c = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);

                                        final = DirectionLight.GetFinalLightColor(n3, c);
                                    }
                                    else
                                    {
                                        final = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
                                    }
                                }
                                break;
                        }
                        device.DrawPoint(pos, final);
                    }
                }
            }
        }
    }
}
