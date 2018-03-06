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
            CATransform.InitMVPMatrix(scene);
        }

        public static void UpdateCameraPitch(float pitch, Scene scene)
        {
            scene.camera.Pitch = pitch;
            CATransform.InitMVPMatrix(scene);
        }

        public static void UpdateCameraYaw(float yaw, Scene scene)
        {
            scene.camera.Yaw = yaw;
            CATransform.InitMVPMatrix(scene);
        }

        public static void RotateCamera(float degreeX, float degreeY, float degreeZ,Scene scene)
        {
            Matrix4x4 rotate = new Matrix4x4();
            rotate.SetRotate(degreeX, degreeY, degreeZ);
            scene.camera.Target = rotate.ApplY(scene.camera.Target);
            CATransform.InitMVPMatrix(scene);
        }

        public void UpdateCameraRotation(float degree ,Scene scene)
        {
            scene.camera.Yaw += degree;
            CATransform.InitMVPMatrix(scene);
        }

        public static void RotateMesh(float degreeX, float degreeY, float degreeZ, Scene scene)
        {
            
            scene.mesh.Rotate(degreeX, degreeY, degreeZ);
             
            CATransform.InitMVPMatrix(scene);
        }

    }
}
