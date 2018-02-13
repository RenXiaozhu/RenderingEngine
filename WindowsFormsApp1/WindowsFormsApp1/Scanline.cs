using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RenderingEngine
{
    class Scanline
    {
        private Device device;
        private int height;
        private int width;
        private Color4 finalColor;
        private List<Triangle> angleList;
        public Scanline(Device device)
        {
            this.device = device;
            this.width = device.width;
            this.height = device.height;

            finalColor = new Color4(222, 128, 50);
            this.angleList = new List<Triangle>();
        }

        public void StartScan()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = Color.FromArgb(finalColor.red, finalColor.blue, finalColor.green);
                    if (this.angleList[0].IsPointInTriangle(new Point(x, y)))
                    {
                        this.device.bitmap.SetPixel(x, y, color);
                    }
                }
            }
        }

        public void addTriangle(Triangle triangle)
        {
            this.angleList.Add(triangle);
        }
    }
}
