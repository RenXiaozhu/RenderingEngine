using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    class Vertex
    {
        //位置
        Vector4 Position;
        //顶点三条边的方向
        Vector4 direction_1;
        Vector4 direction_2;
        Vector4 direction_3;
        public Vertex(Vector4 postion)
        {
            this.Position = postion;
        }
       

    }
}
