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

				MeshRotation *= scene.mesh.rotation;

				 model = scale  * MeshRotation * translate;

				Matrix4x4 view = scene.camera.LookAt();

				Matrix4x4 projection = scene.camera.Perspective();

				Matrix4x4 mvp = model * view * projection;

				MVPMatrix = mvp;
			}
		
		}

		public static void ModelTransformToWindow(Scene scene)
        {
           
        }
    }
}
