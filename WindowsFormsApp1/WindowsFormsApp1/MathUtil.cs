using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
	class MathUtil
	{
		public static double Clamp01(double t)
		{
			if (t.CompareTo(0) < 0)
				return 0;
			else if (t.CompareTo(1) > 0)
				return 1;
			return t;
		}

		// 计算线性插值 t在[0,1]之间
		public static double Interp(double x1, double x2 , double t)
		{
			return x1 + (x2 - x1) * t;
		}

		public static byte Interp(byte x1, byte x2, double t)
		{
			return (byte)(x1 + (x2 - x1) * t);
		}

		//矢量插值 t [0,1]
		public static Vector4 Vector4Interp(Vector4 x1, Vector4 x2, double t)
		{
			Vector4 val = new Vector4();
			val.x = Interp(x1.x, x2.x, t);
			val.y = Interp(x1.y, x2.y, t);
			val.z = Interp(x1.z, x2.z,t);
			val.h = 1.0f;
			return val;
		}

		public static Color4 ColorInterp(Color4 c1, Color4 c2, double t)
		{
			byte r = Interp(c1.red, c2.red, t);
			byte g = Interp(c1.green, c2.green, t);
			byte b = Interp(c1.blue, c2.blue, t);
			return new Color4(r, g, b);
		}

	}
}
