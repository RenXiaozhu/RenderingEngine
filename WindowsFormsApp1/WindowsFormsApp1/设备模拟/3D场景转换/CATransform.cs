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

        public static Matrix4x4 MVPMatrix;
		public static Matrix4x4 rotation = new Matrix4x4();
		public static Matrix4x4 MeshRotation = new Matrix4x4();
		public static Matrix4x4 view = new Matrix4x4();
		
		public static Matrix4x4 model = new Matrix4x4();

		public static void InitMVPMatrix(Scene scene, bool isCameraRotate)
		{
            Matrix4x4 translate = new Matrix4x4();

            translate.SetRotate(0, 0, 0);

            Matrix4x4 scale = new Matrix4x4();

            scale.SetScale(1, 1, 1);

			if (isCameraRotate)
			{
				rotation *= scene.camera.rotate;

				Matrix4x4 model = scale * MeshRotation * translate;

				view = rotation * scene.camera.LookAt();

				Matrix4x4 projection = scene.camera.Perspective();

				Matrix4x4 mvp = model * view * projection;

				//MeshRotation *= scene.camera.rotate;

				MVPMatrix = mvp;
			}
			else
			{

				//MeshRotation *= scene.mesh.rotation;

				 model = scale  * MeshRotation * translate;

				Matrix4x4 view = scene.camera.LookAt();

				Matrix4x4 projection = scene.camera.Perspective();

				Matrix4x4 mvp = model * view * projection;

				MVPMatrix = mvp;
			}
		
		}

		public static void ModelTransformToWindow(Scene scene)
        {
            ////scene.mesh.RefreshVectex();

            //Mesh newmesh = scene.mesh;

            //int i = 0;

            //while (i < newmesh.TmpVertices.Length)
            //{
                //int a = i;
                //Vertex vt = newmesh.TmpVertices[i];

                //Vector4 pos = new Vector4();


    //            //转换到世界坐标系
    //            VETransform3D trans = CoordinateTransform.CoordinateToOther(new Vector4(1, 0, 0, 1), new Vector4(0, 1, 0, 1), new Vector4(0, 0, 1, 1), new Vector4(0, 0, 0, 1));

				//pos = trans.Maxtrix4x1(vt.Position);


				//// 放缩变换
				//VETransform3D scale = new VETransform3D();

				//scale.VETransform3DScale(50, 50, 50);

				////pos = scale.Maxtrix4x1(pos);

				//// 转换到相机坐标系
				//VETransform3D cameraTrans = scene.camera.TransformChange();

				//pos = cameraTrans.Maxtrix4x1(pos);

				////透视变换
				//VETransform3D cameraPerspective = scene.camera.MatrixProjection;

				//pos = cameraPerspective.Maxtrix4x1(pos);

				//pos.Vector3Nomal();

				////Vector4 newpos = new Vector4(pos.x / (1 - pos.z / scene.camera.ViewDistance), pos.y / (1 - pos.z / scene.camera.ViewDistance),0,1);

				////屏幕变换
				//VETransform3D screenTrans = scene.camera.MatrixScreen;

				//pos = screenTrans.Maxtrix4x1(pos);

				//// 平移变换
				//VETransform3D translate = new VETransform3D();
    //            translate.VETransform3DTranslation(300, 300, 0);
				//translate.d44 = 0;
				////pos = translate.Maxtrix4x1(pos);

                //vt.Position = pos;
                //Console.WriteLine(pos.x);
        //        i+=1;
        //    }
        }
    }
}
