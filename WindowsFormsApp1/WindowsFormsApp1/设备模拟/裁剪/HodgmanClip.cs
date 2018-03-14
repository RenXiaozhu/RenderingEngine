using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
	/*裁剪处理的基础
	 * 1. 图元在窗口区域的内外判别
	 * 2. 图形元素与窗口的求交
	 * 
	 * 裁剪的两种方式：
	 * 1. 对扫描转换后的点阵图形在设备坐标系中裁剪： 优点 算法简单 ，缺点效率不高 一般适用于求交难度大的图形
	 *
	 * 2. 在世界坐标系中队扫描转换前的参数进行裁剪：优点 简单图元 世界坐标系的裁剪也称为分析裁剪，大多数系统都采用分析裁剪
	 */
	class HodgmanClip
	{
        public enum Boundary { Left, Right, Bottom, Top, Behind, Front };
        private Device device;

        private List<Vertex> outputList;

        public HodgmanClip(Device device)
        {
            this.outputList = new List<Vertex>();
            this.device = device;
        }

        public List<Vertex> GetOutputList()
        {
            return this.outputList;
        }

        // 求交点, 返回的pos是clip空间下坐标
        Vertex Intersect(Vertex v1, Vertex v2, Boundary b, Vector4 wMin, Vector4 wMax)
        {
            Vertex iPt = new Vertex();
            float m1 = 0, m2 = 0, m3 = 0, m4 = 0, m5 = 0, m6 = 0;
            Vector4 p1 = v1.ClipSpacePosition;
            Vector4 p2 = v2.ClipSpacePosition;
            if (p1.X != p2.X)
			{
				m1 = (wMin.X - p1.X) / (p2.X - p1.X);
				m2 = (wMax.X - p1.X) / (p2.X - p1.X);
			}
            if (p1.Y != p2.Y)
			{
				m3 = (wMin.Y - p1.Y) / (p2.Y - p1.Y);
				m4 = (wMax.Y - p1.Y) / (p2.Y - p1.Y);
			}
            if (p1.Z != p2.Z)
			{
				m5 = (wMin.Z - p1.Z) / (p2.Z - p1.Z);
				m6 = (wMax.Z - p1.Z) / (p2.Z - p1.Z);
			}
            Vector4 clipPos = new Vector4();
            Vector4 pos = new Vector4();
            Color4 col = new Color4(255, 255, 255);
            Vector4 normal = new Vector4();
            Vector4 uv = new Vector4();

            switch (b)
            {
                case Boundary.Left:
                    clipPos.X = wMin.X;
                    clipPos.Y = p1.Y + (p2.Y - p1.Y) * m1;
                    clipPos.Z = p1.Z + (p2.Z - p1.Z) * m1;
                    clipPos.W = p1.W + (p2.W - p1.W) * m1;
                    col = MathUtil.ColorInterp(v1.Color, v2.Color, m1);
                    normal = MathUtil.Vector4Interp(v1.nowNormal, v2.nowNormal, m1);
					uv = MathUtil.Vector4Interp(v1.UV, v2.UV, m1);
					//uv.W = MathUtil.Interp(v1.UV.W, v2.UV.W, m1);
					//uv.X = uv.X *uv.W;
					//uv.Y = uv.Y *uv.W;
					break;
                case Boundary.Right:
                    clipPos.X = wMax.X;
                    clipPos.Y = p1.Y + (p2.Y - p1.Y) * m2;
                    clipPos.Z = p1.Z + (p2.Z - p1.Z) * m2;
                    clipPos.W = p1.W + (p2.W - p1.W) * m2;
                    col = MathUtil.ColorInterp(v1.Color, v2.Color, m2);
                    normal = MathUtil.Vector4Interp(v1.nowNormal, v2.nowNormal, m2);
					uv = MathUtil.Vector4Interp(v1.UV, v2.UV, m2);
					//uv.X = uv.X *uv.W;
					//uv.Y = uv.Y *uv.W;
					//uv.W = MathUtil.Interp(v1.UV.W, v2.UV.W, m2);
					break;
                case Boundary.Bottom:
                    clipPos.Y = wMin.Y;
                    clipPos.X = p1.X + (p2.X - p1.X) * m3;
                    clipPos.Z = p1.Z + (p2.Z - p1.Z) * m3;
                    clipPos.W = p1.W + (p2.W - p1.W) * m3;
                    col = MathUtil.ColorInterp(v1.Color, v2.Color, m3);
                    normal = MathUtil.Vector4Interp(v1.nowNormal, v2.nowNormal, m3);
					uv = MathUtil.Vector4Interp(v1.UV, v2.UV, m3);
					//uv.W = MathUtil.Interp(v1.UV.W, v2.UV.W, m3);
					//uv.X = uv.X *uv.W;
					//uv.Y = uv.Y *uv.W;
					break;
                case Boundary.Top:
                    clipPos.Y = wMax.Y;
                    clipPos.X = p1.X + (p2.X - p1.X) * m4;
                    clipPos.Z = p1.Z + (p2.Z - p1.Z) * m4;
                    clipPos.W = p1.W + (p2.W - p1.W) * m4;
                    col = MathUtil.ColorInterp(v1.Color, v2.Color, m4);
                    normal = MathUtil.Vector4Interp(v1.nowNormal, v2.nowNormal, m4);
					uv = MathUtil.Vector4Interp(v1.UV, v2.UV, m4);
					//uv.W = MathUtil.Interp(v1.UV.W, v2.UV.W, m4);
					//uv.X = uv.X * uv.W;
					//uv.Y = uv.Y *uv.W;
					break;
                case Boundary.Behind:
                    clipPos.Z = wMin.Z;
                    clipPos.X = p1.X + (p2.X - p1.X) * m5;
                    clipPos.Y = p1.Y + (p2.Y - p1.Y) * m5;
                    clipPos.W = p1.W + (p2.W - p1.W) * m5;
                    col = MathUtil.ColorInterp(v1.Color, v2.Color, m5);
                    normal = MathUtil.Vector4Interp(v1.nowNormal, v2.nowNormal, m5);
					uv = MathUtil.Vector4Interp(v1.UV, v2.UV, m5);
					//uv.W = MathUtil.Interp(v1.UV.W, v2.UV.W, m5);
					//uv.X = uv.X * uv.W;
					//uv.Y = uv.Y *uv.W;
					break;
                case Boundary.Front:
                    clipPos.Z = wMax.Z;
                    clipPos.X = p1.X + (p2.X - p1.X) * m6;
                    clipPos.Y = p1.Y + (p2.Y - p1.Y) * m6;
                    clipPos.W = p1.W + (p2.W - p1.W) * m6;
                    col = MathUtil.ColorInterp(v1.Color, v2.Color, m6);
                    normal = MathUtil.Vector4Interp(v1.nowNormal, v2.nowNormal, m6);
					uv = MathUtil.Vector4Interp(v1.UV, v2.UV, m6);
					//uv.W = MathUtil.Interp(v1.UV.W, v2.UV.W, m6);
					//uv.X = uv.X *uv.W;
					//uv.Y = uv.Y * clipPos.W;
					break;
            }

            iPt.Position = pos;
            iPt.ClipSpacePosition = clipPos;
            iPt.ScreenSpacePosition = this.device.ViewPort(clipPos);
            iPt.Normal = normal;
			iPt.nowNormal = normal.Normalized;
            iPt.UV = uv;
            iPt.Color = col;
            return iPt;
        }

        bool Inside(Vector4 p, Boundary b, Vector4 wMin, Vector4 wMax)
        {
            bool flag = true;
            switch (b)
            {
                case Boundary.Left:
                    if (p.X < wMin.X)
                        flag = false;
                    break;
                case Boundary.Right:
                    if (p.X > wMax.X)
                        flag = false;
                    break;
                case Boundary.Bottom:
                    if (p.Y < wMin.Y)
                        flag = false;
                    break;
                case Boundary.Top:
                    if (p.Y > wMax.Y)
                        flag = false;
                    break;
                case Boundary.Behind:
                    if (p.Z < wMin.Z)
                        flag = false;
                    break;
                case Boundary.Front:
                    if (p.Z > wMax.Z)
                        flag = false;
                    break;
            }
            if (p.W < 0)
            {
                flag = false;
            }
            return flag;
        }

        // 需要clip空间下的坐标（除以w分量）
        public void HodgmanPolygonClip(Boundary b, Vector4 wMin, Vector4 wMax, Vertex[] pIn)
        {
            Vertex s = pIn[pIn.Length - 1];
            for (int i = 0; i < pIn.Length; i++)
            {
                Vertex p = pIn[i];

                if (Inside(p.ClipSpacePosition, b, wMin, wMax))
                {
                    if (Inside(s.ClipSpacePosition, b, wMin, wMax))
                    {
                        this.outputList.Add(p);
                    }
                    else
                    {
                        this.outputList.Add(Intersect(s, p, b, wMin, wMax));
                        this.outputList.Add(pIn[i]);
                    }
                }
                else if (Inside(s.ClipSpacePosition, b, wMin, wMax))
                {
                    this.outputList.Add(Intersect(s, p, b, wMin, wMax));
                }
                s = pIn[i];
            }
        }
	}
}
