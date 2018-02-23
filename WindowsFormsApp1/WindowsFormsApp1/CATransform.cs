using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    /*
     *  从模型坐标系 变换到窗口坐标系的一系列操作
    */ 
    class CATransform
    {
        public static void ModelTransformToWindow(Scene scene)
        {
            scene.mesh.RefreshVectex();

            Mesh newmesh = scene.mesh;

            int i = 0;

            while (i < newmesh.TmpVertices.Length)
            {
                int a = i;
                Vertex vt = newmesh.TmpVertices[i];

                Vector4 pos = new Vector4();
                //转换到世界坐标系
                VETransform3D trans = CoordinateTransform.CoordinateToOther(new Vector4(1, 0, 0, 1), new Vector4(0, 1, 0, 1), new Vector4(0, 0, 1, 1), new Vector4(0, 0, 0, 1));

                // 转换到相机坐标系
                VETransform3D cameraTrans = scene.camera.MatrixCamera;

                // 放缩变换
                VETransform3D scale = new VETransform3D();
                scale.VETransform3DScale(100, 100, 100);

                // 平移变换
                VETransform3D translate = new VETransform3D();
                translate.VETransform3DTranslation(200, 200, 0);

                pos = (translate * scale * cameraTrans * trans).Maxtrix4x1(vt.Position);

                vt.Position = pos;
                Console.WriteLine(pos.x);
                i+=1;
            }
        }
    }
}
