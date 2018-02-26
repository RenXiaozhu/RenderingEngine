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

		public static VETransform3D MVPMatrix;

		public static void InitMVPMatrix(Scene scene)
		{
			VETransform3D translate = new VETransform3D();
			translate.VETransform3DTranslation(0, 0, 0);

			VETransform3D scale = new VETransform3D();
			scale.VETransform3DScale(1, 1, 1);

			VETransform3D rotation = scene.mesh.rotation;
			VETransform3D model = scale * rotation * translate;
			VETransform3D view = scene.camera.FPSView();
			VETransform3D projection = scene.camera.Perspective();
			VETransform3D mvp = model * view;
				mvp = mvp * projection;

			MVPMatrix = mvp;

		}

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

				pos = trans.Maxtrix4x1(vt.Position);


				// 放缩变换
				VETransform3D scale = new VETransform3D();

				scale.VETransform3DScale(50, 50, 50);

				//pos = scale.Maxtrix4x1(pos);

				// 转换到相机坐标系
				VETransform3D cameraTrans = scene.camera.TransformChange();

				pos = cameraTrans.Maxtrix4x1(pos);

				//透视变换
				VETransform3D cameraPerspective = scene.camera.MatrixProjection;

				pos = cameraPerspective.Maxtrix4x1(pos);

				pos.Vector3Nomal();

				//Vector4 newpos = new Vector4(pos.x / (1 - pos.z / scene.camera.ViewDistance), pos.y / (1 - pos.z / scene.camera.ViewDistance),0,1);

				//屏幕变换
				VETransform3D screenTrans = scene.camera.MatrixScreen;

				pos = screenTrans.Maxtrix4x1(pos);

				// 平移变换
				VETransform3D translate = new VETransform3D();
                translate.VETransform3DTranslation(300, 300, 0);
				translate.d44 = 0;
				//pos = translate.Maxtrix4x1(pos);

                vt.Position = pos;
                Console.WriteLine(pos.x);
                i+=1;
            }
        }
    }
}
