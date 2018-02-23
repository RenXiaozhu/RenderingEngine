using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    class Edge: ICloneable
    {
        public int ymax;//保存边的上端点y值
        public float x;   // 在AEL中表示当前扫描线与边的交点的x坐标，初值（即在ET中的值） 为边的下端点的x坐标
        public float deltax;//边的斜率的倒数
        public Edge nextEdge;//指向下一条边

        public Vertex yvMax;//y较大的值
        public Vertex yvMin;//y较小的值

        public object Clone()
        {
            Edge e = new Edge();
            e.ymax = ymax;
            e.x = x;
            e.deltax = deltax;
            e.nextEdge = nextEdge;
            e.yvMax = yvMax;
            e.yvMin = yvMin;
            return e;
        }
    }
}
