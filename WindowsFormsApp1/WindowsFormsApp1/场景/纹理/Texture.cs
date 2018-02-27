using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
namespace RenderingEngine
{
	public class Texture
	{
		private Bitmap bitmap;
		private BitmapData bmData;
		public int width;
		public int height;

		public Texture(string filename, int width, int height)
		{
			this.width = width;
			this.height = height;
            this.bitmap = new Bitmap("/Users/wangao/Documents/workSpace/RenderingEngine/WindowsFormsApp1/WindowsFormsApp1/textures/background.jpg");
		}

		public BitmapData GetBmData()
		{
			return this.bmData;
		}

		public BitmapData LockBits()
		{
			this.bmData = this.bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
			return this.bmData;
		}

		public void UnlockBits(BitmapData bmData)
		{
			this.bitmap.UnlockBits(bmData);
		}
		public int GetWidth()
		{
			return this.width;
		}

		public int GetHeight()
		{
			return this.height;
		}
	}
}
