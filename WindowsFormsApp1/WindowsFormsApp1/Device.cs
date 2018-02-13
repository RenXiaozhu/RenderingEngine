using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
namespace RenderingEngine
{
    class Device
    {
        public Bitmap bitmap;
        public BitmapData bitmapData;
        public int width;
        public int height;
        public Scanline scanline;
        public Scene scene;
        
        public Device(Bitmap bmp)
        {
            this.bitmap = bmp;
            this.width = bmp.Width;
            this.height = bmp.Height;
            this.scanline = new Scanline(this);

        }

        public void StartDisplay(FormWindow window)
        {
            

            while (true)
            {
                scanline.StartScan();
                window.g.DrawImage(bitmap, new Point(0, 0));
                System.Threading.Thread.Sleep(100);
            }

        }
    }
}
