using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Vertex:ICloneable
    {    
        public Vector4 Position { get; set; } //位置
        public Vector4 ClipSpacePosition;//裁剪空间
        public Vector4 ScreenSpacePosition;//屏幕空间
        public Vector4 Normal; // 法线
        public Vector4 UV;//贴图坐标
        public Color4 Color;

		public Vertex() { }
        public Vertex(Vector4 postion , Vector4 normal, Vector4 uv, Color4 color)
        {
            this.Position = postion;
            this.Normal = normal;
            this.UV = uv;
            this.Color = color;
        }

        public object Clone()
        {
            Vertex e = new Vertex(this.Position,this.Normal,this.UV,this.Color);
            return e;
        }
    }
}
