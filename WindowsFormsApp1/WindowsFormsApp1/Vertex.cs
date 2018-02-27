using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Vertex:ICloneable
    {    
        public Vector4 Position { get; set; }
        public Vector4 ClipSpacePosition { get; set; }
        public Vector4 ScreenSpacePosition { get; set; }
        public Vector4 Normal { get; set; }
        public Vector4 UV { get; set; }
        public Color4 Color { get; set; }

        public Vertex() { }

        public Vertex(Vector4 pos, Vector4 normal, Vector4 uv, Color4 col)
        {
            this.Position = pos;
            this.Normal = normal;
            this.UV = uv;
            this.Color = col;
        }

        public object Clone()
        {
            Vertex e = new Vertex(this.Position,this.Normal,this.UV,this.Color);
            return e;
        }
    }
}
