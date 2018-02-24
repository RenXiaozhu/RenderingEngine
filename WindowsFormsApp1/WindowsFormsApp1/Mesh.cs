using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Mesh
    {
        public string Name;
        public Vertex[] vertices;
        public Vertex[] TmpVertices;
        public Triangle[] triangles;
        public VETransform3D rotation;

        public Mesh()
        {
            TmpVertices = new Vertex[8];
            CreatTriangles();
        }

        void CreatTriangles()
        {
            /*
             * 0 : -1 -1 -1  前 左下角
             * 1 : 1 -1 -1   前右下角
             * 2 : 1  1 -1   前右上角
             * 3: -1 1 -1    前左上角
             * 4: 1 1 1     后右上角
             * 5: 1 -1 1    后右下角
             * 6 : -1 -1 1  后左下角
             * 7: -1 1 1   后左上角
             */
            triangles = new Triangle[] {
                //正面
                new Triangle(0,1,3),
                new Triangle(3,2,1),
/*
                // 上面
                new Triangle(3, 7, 2),
                new Triangle(2, 4, 7),
                
                // 下面
                new Triangle(0, 6, 1),
                new Triangle(1, 5, 6),

                // 左面
                new Triangle( 0 , 3, 5),
                new Triangle(3, 7, 5),

                // 右面
                new Triangle(1,2, 5),
                new Triangle(2, 4,5),

                // 后面
                new Triangle(6, 7, 5),
                new Triangle(4, 5, 7)
                */
            };
        }

        public void RefreshVectex()
        {
            for (int i = 0; i < vertices.Length; i+=1)
            {
                TmpVertices[i] = (Vertex)vertices[i].Clone();
            }
        }
    }

}
