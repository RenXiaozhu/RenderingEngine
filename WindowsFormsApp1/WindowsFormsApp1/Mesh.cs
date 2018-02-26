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
		public Texture texture;

		public Mesh(string name)
        {
            TmpVertices = new Vertex[24];
			//texture = new Texture(@".........", 512, 512);
			rotation = new VETransform3D();
			rotation.VETransform3DIdentity();
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
                new Triangle(2,5,8),
                new Triangle(2,8,11),
				 // 右面
                new Triangle(4,16, 7),
				new Triangle(16, 19,7),

				 // 左面
                new Triangle(13 , 1, 10),
				new Triangle(13, 10, 22),

				 // 后面
                new Triangle( 17, 14, 23),
				new Triangle( 17, 23, 20),

                // 上面
                new Triangle(9, 6, 18),
                new Triangle(9, 18, 21),
                
                // 下面
                new Triangle(12, 15, 3),
                new Triangle(12, 3, 0)

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
