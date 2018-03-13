using System;

namespace RenderingEngine
{
    public class Action
    {
        public Action()
        {
            
        }

        // 相机左右前后平移
        public static void TranslateCamera(float x, float y, float z, Scene scene)
        {
            Vector4 pos = scene.camera.Position;
            pos.X += x;
            pos.Y += y;
            pos.Z += z;
            scene.camera.Position = pos;
            CATransform.InitMVPMatrix(scene,false);
        }

        public static void UpdateCameraPitch(float pitch, Scene scene)
        {
            scene.camera.Pitch = pitch;
            CATransform.InitMVPMatrix(scene,false);
        }

        public static void UpdateCameraYaw(float yaw, Scene scene)
        {
            scene.camera.Yaw = yaw;
            CATransform.InitMVPMatrix(scene,false);
        }

        public static void RotateCamera(float degreeX, float degreeY, float degreeZ,Scene scene,bool isCameraRotate)
        {
            //Matrix4x4 rotateNew = new Matrix4x4();
            //rotateNew.SetRotate(degreeX*0.02f, degreeY*0.02f, degreeZ*0.02f);
            Matrix4x4 rotate = scene.camera.rotate;
            scene.camera.rotate.SetRotate(degreeX*0.1f , degreeY *0.1f, degreeZ*0.1f );
			//scene.camera.Position = rotate.ApplY(scene.camera.Position);
			//scene.camera.Yaw = -degreeX/180;
			// scene.camera.Yaw = -degreeY/180;
			//scene.camera.Up = rotate.ApplY(scene.camera.Up);
			//scene.camera.Up.Normalize();
			CATransform.InitMVPMatrix(scene,isCameraRotate);
			scene.camera.rotate.SetRotate(0, 0, 0);
        }

        public void UpdateCameraRotation(float degree ,Scene scene)
        {
            scene.camera.Yaw += degree;
            CATransform.InitMVPMatrix(scene,false);
        }

        public static void RotateMesh(float degreeX, float degreeY, float degreeZ, Scene scene, bool isCameraRotate)
        {
            scene.mesh.Action_Rotate(degreeX*0.1f, degreeY*0.1f, degreeZ*0.1f);
            //scene.worldMap.Action_Rotate(degreeX, degreeY, degreeZ);
            CATransform.InitMVPMatrix(scene,isCameraRotate);
			scene.mesh.Action_Rotate(0, 0, 0);
		}

        public static void Mesh_Move(float dx,float dy,float dz,Scene scene)
        {
            scene.mesh.Action_Move(dx, dy, dz);
            CATransform.InitMVPMatrix(scene,false);
        }

    }
}
