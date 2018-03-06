using System;
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

        #region 简单scanline
        public void ProcessScanLineAd(int y, Vertex v1, Vertex v2, Vertex v3, Vertex v4, Scene scene)
        {
            var pa = v1.ScreenSpacePosition;
            var pb = v2.ScreenSpacePosition;
            var pc = v3.ScreenSpacePosition;
            var pd = v4.ScreenSpacePosition;
            var gradient1 = pa.Y != pb.Y ? (y - pa.Y) / (pb.Y - pa.Y) : 1;
            var gradient2 = pc.Y != pd.Y ? (y - pc.Y) / (pd.Y - pc.Y) : 1;

            int sx = (int)MathUtil.Interp(pa.X, pb.X, gradient1);
            int ex = (int)MathUtil.Interp(pc.X, pd.X, gradient2);

            float z1 = MathUtil.Interp(pa.Z, pb.Z, gradient1);
            float z2 = MathUtil.Interp(pc.Z, pd.Z, gradient2);

            for (var x = sx; x < ex; x++)
            {
                float gradient = (x - sx) / (float)(ex - sx);
                var z = MathUtil.Interp(z1, z2, gradient);

                if (scene.renderState == Scene.RenderState.GouraduShading)
                {
                    if (scene.light.IsEnable)
                    {
                        float nDotLA1V1 = scene.light.ComputeNDotL(v1.nowPos, v1.nowNormal);
                        float nDotLA1V2 = scene.light.ComputeNDotL(v2.nowPos, v2.nowNormal);
                        float nDotLA2V1 = scene.light.ComputeNDotL(v3.nowPos, v3.nowNormal);
                        float nDotLA2V2 = scene.light.ComputeNDotL(v4.nowPos, v4.nowNormal);
                        float nDotL1 = MathUtil.Interp(nDotLA1V1, nDotLA1V2, gradient1);
                        float nDotL2 = MathUtil.Interp(nDotLA2V1, nDotLA2V2, gradient2);
                        float nDotL = MathUtil.Interp(nDotL1, nDotL2, gradient);
                        final = scene.light.GetFinalLightColor(nDotL);
                    }
                    else
                    {
                        Color4 c1 = MathUtil.ColorInterp(v1.Color, v2.Color, gradient1);
                        Color4 c2 = MathUtil.ColorInterp(v3.Color, v4.Color, gradient2);
                        Color4 c3 = MathUtil.ColorInterp(c1, c2, gradient);
                        //vt.CalWeight(s1, s2, s3, new Vector4(x, i, 0, 0));
                        //Color4 c3 = vt.GetInterColor();
                        final = c3;
                    }
                }
                else if (scene.renderState == Scene.RenderState.TextureMapping)
                {
                    float w11 = v1.ClipSpacePosition.W;
                    float w12 = v2.ClipSpacePosition.W;
                    float w21 = v3.ClipSpacePosition.W;
                    float w22 = v4.ClipSpacePosition.W;
                    // 通过裁剪空间坐标插值算出 当前点的UV坐标；‘
                    float uu1 = MathUtil.Interp(v1.UV.X / w11, v2.UV.X / w12, gradient1);
                    float vv1 = MathUtil.Interp(v1.UV.Y / w11, v2.UV.Y / w12, gradient1);
                    float uu2 = MathUtil.Interp(v3.UV.X / w21, v4.UV.X / w22, gradient2);
                    float vv2 = MathUtil.Interp(v3.UV.Y / w21, v4.UV.Y / w22, gradient2);
                    float w1 = MathUtil.Interp(1 / w11, 1 / w12, gradient1);
                    float w2 = MathUtil.Interp(1 / w21, 1 / w22, gradient2);

                    float w = MathUtil.Interp(w1, w2, gradient);
                    float uu3 = MathUtil.Interp(uu1, uu2, gradient) / w;
                    float vv3 = MathUtil.Interp(vv1, vv2, gradient) / w;

                    final = this.device.Tex2D(uu3, vv3, scene.mesh.texture);
                    //final = this.device.Tex2D(u / w, v / w, scene.mesh.texture);
                }
                this.device.DrawPoint(new Vector4(x, y, z, 0), final);
            }
        }
        #endregion

        /* vt是裁剪后的三角形，
         * orivt是原始三角形,目的是在纹理映射的时候计算权重
         * 
         */
        public void ProcessScanLine(VertexTriangle vt, VertexTriangle oriVt, Scene scene)
        {
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
            int yMin = this.height;
            int yMax = 0;

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

                        // 边的斜率的倒数
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

            // 从ymin开始扫描填充

            oriVt.Preproccess();


            for (int i = yMin; i < yMax; i++)
            {
                // 将边表中在 y = i 的边 插入活动边表AEL中，并删除边表里的边
                EdgeTable.SearchTable(i, ET, AEL);

                if (AEL.nextEdge == null) continue;

                // 获取连续的两条边
                Edge a1 = (Edge)AEL.nextEdge.Clone();
                Edge a2 = (Edge)AEL.nextEdge.nextEdge.Clone();

                if (scene.light.IsEnable)
                {

                    //Console.WriteLine(a1.v1.Position +" " + a1.v1.Normal);
                    float nDotLA1V1 = scene.light.ComputeNDotL(a1.v1.nowPos, a1.v1.nowNormal);
                    a1.v1.lightColor = a1.v1.Color * scene.light.GetFinalLightColor(nDotLA1V1);

                    float nDotLA1V2 = scene.light.ComputeNDotL(a1.v2.nowPos, a1.v2.nowNormal);
                    a1.v2.lightColor = a1.v2.Color * scene.light.GetFinalLightColor(nDotLA1V2);

                    float nDotLA2V1 = scene.light.ComputeNDotL(a2.v1.nowPos, a2.v1.nowNormal);
                    a2.v1.lightColor = a2.v1.Color * scene.light.GetFinalLightColor(nDotLA2V1);

                    float nDotLA2V2 = scene.light.ComputeNDotL(a2.v2.nowPos, a2.v2.nowNormal);
                    a2.v2.lightColor = a2.v2.Color * scene.light.GetFinalLightColor(nDotLA2V2);

                    //nDotL1 = MathUtil.Interp(nDotLA1V1, nDotLA1V2, r1);
                    //nDotL2 = MathUtil.Interp(nDotLA2V1, nDotLA2V2, r2);
                }

                if (scene.renderState == Scene.RenderState.TextureMapping)
                {
                    
                }

                // 横向填充
                while (a1 != null && a2 != null)
                {
                    //Padding.x = AEL.nextEdge.x;
                    //Padding.GouraudColor(scene, device, i, a1, a2, oriVt);

                    //Padding.x = AEL.nextEdge.nextEdge.x-1;
                    //Padding.GouraudColor(scene, device, i, a1, a2, oriVt);

                    for (int x = (int)AEL.nextEdge.x ; x < (int)AEL.nextEdge.nextEdge.x; x++)
                    {
                        Padding.x = x;
                        Padding.GouraudColor(scene, device, i, a1, a2, oriVt);
                    }
                    if (a2.nextEdge != null && a2.nextEdge.nextEdge != null)
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

    }
}
