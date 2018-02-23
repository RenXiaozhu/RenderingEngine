using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Camera
    {
       public Vector4 self_position { get; set; }
       public Vector4 target_postion { get; set; }
       public Vector4 up; //y轴
       public Vector4 forward //z轴 深度方向
        {
            get
            {
                return target_postion - self_position ;
            }
            set
            {
               forward = value;
            }
        }

        public double ViewDistance;// 视距
        public double fov; // field of view 视域  视野角度
        public double aspect; // 宽高比
        public double zn;// 近平面   近裁剪距离
        public double zf; //远平面    远裁剪距离
        public Vector4 ClipPlaneLeft, ClipPlaneRight, ClipPlaneUp, ClipPlaneDown;//上下左右裁剪平面
        public double ViewPlaneWidth, ViewPlaneHeight;//透视平面的宽高
        public double ScreenWidth, ScreenHeight;
        public double ScreenCenterX, ScreenCenterY;//屏幕中心坐标

        public VETransform3D MatrixCamera;// 相机变换矩阵
        VETransform3D MatrixProjection;// 透视投影变换矩阵
        VETransform3D MatrixScreen;//屏幕变换矩阵

        public Camera(Vector4 pos, Vector4 target, Vector4 up)
        {
            this.self_position = pos;
            this.target_postion = target;
            this.up = up;
            this.MatrixCamera = ViewTransform();
            this.MatrixProjection = Perspective();
        }


        //观察矩阵
        public VETransform3D ViewTransform()
        {
            Vector4 trans = new Vector4();
            
            //坐标轴
            Vector4 xaxis, yaxis, zaxis;
            zaxis = forward;
            zaxis.Normalize();
            xaxis = up.CrossMultiply(zaxis);
            xaxis.Normalize();
            yaxis = zaxis.CrossMultiply(xaxis);
            yaxis.Normalize();

            return CoordinateTransform.CoordinateToOther(xaxis, yaxis, zaxis, self_position);
        }

        /*
        public VETransform3D FPSView()
        {

        }
        */

         //透视变换
        public VETransform3D Perspective()
        {
            VETransform3D trans = new VETransform3D();
            float far = 1.0f / (float)Math.Tan(this.fov * 0.5);
            trans.SetZero();
            trans.d11 = (float)(far / aspect);
            trans.d22 = far;
            trans.d33 = zf / (zf - zn);
            trans.d43 = -zn * zf / (zf - zn);
            trans.d34 = 1;
            return trans;
        }

    }
}
