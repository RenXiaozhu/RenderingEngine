using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
	class HodgmanClip
	{
		public enum Boundary
		{
			Left,
			Right,
			Bottom,
			Top,
			Behind,
			Front
		};

		private Device device;

		//裁剪后的顶点数组
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

		//求交点， 返回的pos视clip空间下坐标
		Vertex Intersect(Vertex v1, Vertex v2, Boundary b, Vector4 wMin, Vector4 wMax)
		{
			Vertex iPt = new Vertex();
			double m1 = 0, m2 = 0, m3 = 0, m4 = 0, m5 = 0, m6 = 0;
			Vector4 p1 = v1.ClipSpacePosition;
			Vector4 p2 = v2.ClipSpacePosition;
			if (p1.x != p2.x)
			{
				m1 = (wMin.x - p1.x) / (p2.x - p1.x);
				m2 = (wMax.x - p1.x) / (p2.x - p1.x);
			}
			if (p1.y != p2.y)
			{
				m3 = (wMin.y - p1.y) / (p2.y - p1.y);
				m4 = (wMax.y - p1.y) / (p2.y - p1.y);
			}
			if (p1.z != p2.z)
			{
				m5 = (wMin.z - p1.z) / (p2.z - p1.z);
				m6 = (wMax.z - p1.z) / (p2.z - p1.z);
			}
			//裁剪位置
			Vector4 clipPos = new Vector4();

			Vector4 pos = new Vector4();
			//颜色
			Color4 col = new Color4(255, 255, 255);
			//法线
			Vector4 normal = new Vector4();
			//uv平面
			Vector4 uv = new Vector4();

			double m = 0;
			switch (b)
			{
				case Boundary.Left:
					{
						m = m1;
					}
					break;
				case Boundary.Right:
					{
						m = m2;
					}
					break;
				case Boundary.Bottom:
					{
						m = m3;
					}
					break;
				case Boundary.Top:
					{
						m = m4;
					}
					break;
				case Boundary.Behind:
					{
						m = m5;
					}
					break;
				case Boundary.Front:
					{
						m = m6;
					}
					break;
			}

			clipPos.x = wMin.x;
			clipPos.y = p1.y + (p2.y - p1.y) * m;
			clipPos.z = p1.z + (p2.z - p1.z) * m;
			clipPos.h = p1.h + (p2.h - p1.h) * m;
			col = MathUtil.ColorInterp(v1.Color, v2.Color, m);
			normal = MathUtil.Vector4Interp(v1.Normal, v2.Normal, m);

			iPt.Position = pos;
			iPt.ClipSpacePosition = clipPos;
			iPt.ScreenSpacePosition = this.device.ViewPort(clipPos);
			iPt.Normal = normal;
			iPt.UV = uv;
			iPt.Color = col;
			return iPt;
		}

		// 判断点是否包含在屏幕空间中
		bool Inside(Vector4 p, Boundary b, Vector4 wMin, Vector4 wMax)
		{
			bool flag = true;
			switch (b)
			{
				case Boundary.Left:
					{
						if (p.x < wMin.x)
							flag = false;
					}
					break;
				case Boundary.Right:
					{
						if (p.x > wMax.x)
							flag = false;
							
					}
					break;
				case Boundary.Bottom:
					{
						if (p.y < wMin.y)
							flag = false;
					}
					break;
				case Boundary.Top:
					{
						if (p.y > wMax.y)
							flag = false;
					}
					break;
				case Boundary.Behind:
					{
						if (p.z < wMin.z)
							flag = false;
					}
					break;
				case Boundary.Front:
					{
						if (p.z > wMax.z)
							flag = false;
					}
					break;		
			}
			if (p.h < 0)
			{
				flag = false;
			}
			return flag;
		}

		public void HodgmanPolygonClip(Boundary b, Vector4 wMin, Vector4 wMax, Vertex[] pIn)
		{
			Vertex s = pIn[pIn.Length - 1];
			for (int i = 0; i < pIn.Length; i += 1)
			{
				Vertex p = pIn[i];

				if (Inside(p.ClipSpacePosition, b, wMin, wMax))
				{
					if (Inside(s.ClipSpacePosition, b, wMax, wMin))
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
					this.outputList.Add(Intersect( s , p, b, wMin, wMax));
				}
				s = pIn[i];
			}
		}
	}
}
