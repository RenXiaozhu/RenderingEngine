using System;
using System.Collections;
using System.Collections.Generic;
namespace RenderingEngine
{
    public class Padding
    {
        // 扫描列
        public static float x;

        // 扫描行
        public static float y;

        // 三角形的3个顶点颜色
        public static Color4 I1;

        public static Color4 I2;

        public static Color4 I3;

        public static Edge ab;

        public static Edge ac;

        // 左侧交点A的颜色
        /// <summary>
        /// 
        /// I_A = I_A_1*I1 + I_A_2*I2
        /// 
        /// </summary>
        public static Color4 I_A; 

        /* A点线性插值系数
         * 
         * I_A_1 = (ya - y2)/(y1 - y2)
         * 
        */
        public static float I_A_1;

        /* A点线性插值系数
         * 
         * I_A_2 = (y1 - ya)/(y1 - y2)
         * 
        */
        public static float I_A_2;

        /* 延 ab边的 A点颜色增量
         * 
         *  I_A_t = (1/(y1-y2)) * (I1-I2)
        */
        public static Color4 I_A_t;

        /*右侧交点B的颜色
         * 
         * I_B = I_B_1*I1 + I_B_2*I3
         * 
        */
        public static Color4 I_B;

        /* B点线性插值系数
         * 
         * I_B_1 = (yb - y3)/( y1 - 13 )
         * 
        */
        public static float I_B_1;

        /* B点线性插值系数
         * 
         * I_B_2 = (y1 - yb)/(y1 - 13)
         * 
        */
        public static float I_B_2;

        /* 延 aC边的 B点颜色增量
         * 
         * I_B_t = (1/(y1-y3)) * (I1-I3)
         */
        public static Color4 I_B_t;

        /* P点在扫描线上 位于 A B之间的一点
         * 
         * I_P = I_P_1* I_A + I_P_2* I_B
         * 
        */
        public static Color4 I_P;

        /* P点的线性插值系数
         * 
         * I_P_1 = (xb - x)/(xb -xa)
         * 
        */
        public static float I_P_1;

        /* P点的线性插值系数
         * 
         * I_P_1 = (x - xa)/(xb -xa)
         * 
        */
        public static float I_P_2;


        /* 延 两交点直线边的 P 的颜色增量
         * 
         * I_P_t = (1/(y1-y3)) * (I1-I3)
         */
        public static Color4 I_P_t;


        public Padding()
        {
        }


        /* 线性插值求 交点的x坐标
         * 
        */
        public static float LinearInterpolation(Vector4 v1, Vector4 v2)
        {
            float α = (y - v1.Y) / (v1.Y - v2.Y);
            return (1 - α)*v1.X + α * v2.X;
        }

        /* gouraud 着色
         *
         * Ia = (ya - y2)*I1/(y1-y2) + (y1-ya)*I2/(y1 -y2)
         *
         */
        public static void GouraudColor(Scene scene, Device device,float x, float y,float z, VertexTriangle vt, VertexTriangle orivt)
        {
            Color4 final = new Color4();
            Vertex A = vt.Vertices[0];
            Vertex B = vt.Vertices[1];
            Vertex C = vt.Vertices[2];

            //Console.WriteLine(a1.v1.Position +" " + a1.v1.Normal);
            //float nDotLA1V1 = scene.light.ComputeNDotL(A.nowPos, A.nowNormal);
            //A.lightColor = A.Color * scene.light.GetFinalLightColor(nDotLA1V1);

            //float nDotLA1V2 = scene.light.ComputeNDotL(B.nowPos, B.nowNormal);
            //B.lightColor = B.Color * scene.light.GetFinalLightColor(nDotLA1V2);

            //float nDotLA2V1 = scene.light.ComputeNDotL(C.nowPos, C.nowNormal);
            //C.lightColor = C.Color * scene.light.GetFinalLightColor(nDotLA2V1);


            float ay = A.ScreenSpacePosition.Y;
            float by = B.ScreenSpacePosition.Y;
            float cy = C.ScreenSpacePosition.Y;
            float ax = A.ScreenSpacePosition.X;
            float bx = B.ScreenSpacePosition.X;
            float cx = C.ScreenSpacePosition.X;

            bool isIn = vt.CalculateWeight(new Vector4(x, y, 0, 0));

            if(isIn)
            {
                final = vt.GetInterColor();
                device.DrawPoint(new Vector4(x, y, z, 0), final);
            }
            else
            {
                //Console.WriteLine(j+ " " + i);
            }

        }

        public static void GouraudColor(Scene scene,Device device, int i, Edge a1, Edge a2,VertexTriangle orivt ,VertexTriangle vt, bool isworld )
        {
            Color4 final = new Color4();
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
             * 
             * 
            */
            Vector4 screenA1V1 = a1.v1.ScreenSpacePosition;
            Vector4 screenA1V2 = a1.v2.ScreenSpacePosition;
            Vector4 screenA2V1 = a2.v1.ScreenSpacePosition;
            Vector4 screenA2V2 = a2.v2.ScreenSpacePosition;

            // 双线性插值系数

            // 第一次插值算出交点的颜色系数
            float r1 = ((float)i - screenA1V1.Y) / (float)(screenA1V2.Y - screenA1V1.Y);
            float r2 = ((float)i - screenA2V1.Y) / (float)(screenA2V2.Y - screenA2V1.Y);

            r1 = MathUtil.Clamp01(r1);
            r2 = MathUtil.Clamp01(r2);

            // 像素点深度插值
            float z1 = MathUtil.Interp(screenA1V1.Z, screenA1V2.Z, r1);
            float z2 = MathUtil.Interp(screenA2V1.Z, screenA2V2.Z, r2);
            //float z3 = 0;
            float r3 = MathUtil.Clamp01(((float)x - a1.x) / (a2.x - a1.x));
            //float r3 = (float)(x - Math.Floor(a1.x)) / (a2.x - a1.x);    
            float z = MathUtil.Interp(z1, z2, r3);
            //float z = vt.GetInterValue(z1, z2, z3);
            float nDotL1 = 0, nDotL2 = 0;

            switch (scene.renderState)
            {
                
                case Scene.RenderState.WireFrame:
                    {
                        
                       

                    }
                    break;
                case Scene.RenderState.GouraduShading:
                    {
                        
                    }
                    break;
                case Scene.RenderState.TextureMapping:
                    {
                        orivt.CalWeight(new Vector4(x, i, 0, 0));
                        Vector4 uv = orivt.GetInterUV();
                        if(isworld)
                        {
                            //final = device.Tex2D(uv.X, uv.Y, scene.worldMap.texture);
                        }
                        else
                        {
                            if (scene.light.IsEnable)
                            {
                                float nDotLA1V1 = scene.light.ComputeNDotL(a1.v1.nowPos, a1.v1.nowNormal);
                                float nDotLA1V2 = scene.light.ComputeNDotL(a1.v2.nowPos, a1.v2.nowNormal);
                                float nDotLA2V1 = scene.light.ComputeNDotL(a2.v1.nowPos, a2.v1.nowNormal);
                                float nDotLA2V2 = scene.light.ComputeNDotL(a2.v2.nowPos, a2.v2.nowNormal);

                                nDotL1 = MathUtil.Interp(nDotLA1V1, nDotLA1V2, r1);
                                nDotL2 = MathUtil.Interp(nDotLA2V1, nDotLA2V2, r2);

                                float nDotL = MathUtil.Interp(nDotL1, nDotL2, r3);
                                //Console.WriteLine(nDotL1+" "+ nDotL2+" " + nDotL);
                                final = device.Tex2D(uv.X, uv.Y, scene.mesh.texture);
                                final = final * scene.light.GetFinalLightColor(nDotL);
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

        public static void GetYmin(List<Vector4> vt)
        {
            for (int i = 0; i < vt.Count - 1; i+=1)
            {
                for (int j = i + 1; j < vt.Count - i; j+=1)
                {
                    if(vt[i].Y > vt[j].Y)
                    {
                        Vector4 tmp = vt[i];
                        vt[i] = vt[j];
                        vt[j] = tmp;
                    }
                }
            }
        }

        // 求交点 A B x坐标
        public static float[] GetCrossPoint(float[] yy)
        {
            float[] xx = new float[2];

            float y1 = yy[0];
            float y2 = yy[1];
            float y3 = yy[2];
            float x1 = yy[3];
            float x2 = yy[4];
            float x3 = yy[5];
      
            float ytmp = y2 - y1;
            float xtmp = x1 - x2;

            float yt = y2 - y;

            xx[0] = x2 + xtmp * yt / ytmp;

            ytmp = y3 - y1;
            xtmp = x3 - x1;

            yt = y3 - y;

            xx[1] = x1 + xtmp * yt / ytmp;
            return  xx;
        }
    }
}
