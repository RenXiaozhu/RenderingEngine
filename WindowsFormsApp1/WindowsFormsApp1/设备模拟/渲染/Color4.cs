using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public struct Color4
    {
        public byte red;
        public byte blue;
        public byte green;
        public byte A;
        
        public Color4(byte red, byte green, byte blue)
            :this()
        {
            this.red = red;
            this.blue = blue;
            this.green = green;
            this.A = 255;
        }

		public static Color4 operator *(Color4 c1, Color4 c2)
		{
			float r = (c1.red / 255f) * (c2.red / 255f);
			float g = (c1.green / 255f) * (c2.green / 255f);
			float b = (c1.blue / 255f) * (c2.blue / 255f);
			return new Color4((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
		}

		public static Color4 operator *(Color4 c1, double t)
		{
			byte r = (byte)Math.Min((c1.red * t), 255);
			byte g = (byte)Math.Min((c1.green * t), 255);
			byte b = (byte)Math.Min((c1.blue * t), 255);
			return new Color4(r, g, b);
		}

		public static Color4 operator +(Color4 c1, Color4 c2)
		{
			byte r = (byte)Math.Min((c1.red + c2.red), 255);
			byte g = (byte)Math.Min((c1.green + c2.green), 255);
			byte b = (byte)Math.Min((c1.blue + c2.blue), 255);
			return new Color4(r, g, b);
		}

        public static Color4 operator -(Color4 c1, Color4 c2)
        {
            byte r = (byte)Math.Min((c1.red - c2.red), 255);
            byte g = (byte)Math.Min((c1.green - c2.green), 255);
            byte b = (byte)Math.Min((c1.blue - c2.blue), 255);
            return new Color4(r, g, b);
        }

        public static bool operator ==(Color4 c1, Color4 c2)
        {
            if (c1.red == c2.red && c1.green == c2.green && c1.blue == c2.blue)
                return true;
            else
                return false;
        }

        public static bool operator !=(Color4 c1, Color4 c2)
        {
            if (c1.red == c2.red && c1.green == c2.green && c1.blue == c2.blue)
                return false;
            else
                return true;
        }




	}
}
