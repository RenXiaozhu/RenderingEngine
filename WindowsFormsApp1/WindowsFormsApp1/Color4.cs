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
        
        public Color4(byte red, byte blue, byte green)
            :this()
        {
            this.red = red;
            this.blue = blue;
            this.green = green;
            this.A = 255;
        }
    }
}
