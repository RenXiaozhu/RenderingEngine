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

        public double ViewDistance { get; set; }// 视距
        public double fov { get; set; } // field of view 视域  视野角度
		public double Pitch { get; set; }
		public double Yaw { get; set; }
        public double aspect { get; set; } // 宽高比
        public double zn { get; set; }// 近平面   近裁剪距离
        public double zf { get; set; } //远平面    远裁剪距离
		public double Umax
		{
			get
			{
				return ViewPlaneWidth / 2;
			}
		}
		public double Umin
		{
			get
			{
				return -ViewPlaneWidth / 2;
			}
		}
		public double Vmax
		{
			get
			{
				return  ViewPlaneHeight / 2;
			}
		}
		public double Vmin
		{
			get
			{
				return  -ViewPlaneHeight / 2;
			}
		}
		public Vector4 ClipPlaneLeft, ClipPlaneRight, ClipPlaneUp, ClipPlaneDown;//上下左右裁剪平面
																				 //透视平面的宽高
		public double ViewPlaneWidth { get; set; }
		public double ViewPlaneHeight { get; set; }

        public double ScreenWidth, ScreenHeight;
		
		//屏幕中心坐标
		public double ScreenCenterX
		{
			get
			{
				return ScreenWidth / 2;
			}
		}
		public double ScreenCenterY
		{
			get
			{
				return ScreenHeight / 2;
			}
		}

        public VETransform3D MatrixCamera;// 相机变换矩阵

		// 透视投影变换矩阵
		public VETransform3D MatrixProjection
		{
			get {
				return Perspective();
			}
		}

		//屏幕变换矩阵
		public VETransform3D MatrixScreen
		{
			get
			{
				VETransform3D trans = new VETransform3D();
				trans.VETransform3DTranslation(Umax, Vmax, 0);
				return trans;
			}
		}

        public Camera(Vector4 pos, Vector4 target, Vector4 up)
        {
            this.self_position = pos;
            this.target_postion = target;
            this.up = up;
            this.MatrixCamera = ViewTransform();
            
        }

		//转换到相机坐标系
		public VETransform3D TransformChange()
		{
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

        //观察矩阵
        public VETransform3D ViewTransform()
        {   
            //坐标轴
            Vector4 xaxis, yaxis, zaxis;
            zaxis = forward;
            zaxis.Normalize();
            xaxis = up.CrossMultiply(zaxis);
            xaxis.Normalize();
            yaxis = zaxis.CrossMultiply(xaxis);
            yaxis.Normalize();

			VETransform3D trans = CoordinateTransform.CoordinateToOther(xaxis, yaxis, zaxis, new Vector4(0,0,0,0));
			trans.d14 = -xaxis.dot(self_position);
			trans.d24 = -yaxis.dot(self_position);
			trans.d34 = -zaxis.dot(self_position);
			return trans;
        }

      
        public VETransform3D FPSView()
        {
			double pitch = this.Pitch * Math.PI / 180;
			double yaw = this.Yaw * Math.PI / 180;
			double cosPitch = Math.Cos(pitch);
			double sinPitch = Math.Sin(pitch);
			double cosYaw = Math.Cos(yaw);
			double sinYaw = Math.Sin(yaw);

			Vector4 xaxis = new Vector4(cosYaw, 0, -sinYaw, 0);
			Vector4 yaxis = new Vector4(sinYaw * sinPitch, cosPitch, cosYaw * sinPitch, 0);
			Vector4 zaxis = new Vector4(sinYaw * cosPitch, -sinPitch, cosPitch * cosYaw, 0);

			VETransform3D view = new VETransform3D();
			view.d11 = xaxis.x;
			view.d21 = xaxis.y;
			view.d31 = xaxis.z;
			view.d41 = -xaxis.dot(self_position);

			view.d12 = yaxis.x;
			view.d22 = yaxis.y;
			view.d32 = yaxis.z;
			view.d42 = -yaxis.dot(self_position);

			view.d13 = zaxis.x;
			view.d23 = zaxis.y;
			view.d33 = zaxis.z;
			view.d43 = -zaxis.dot(self_position);
			view.d44 = 1;
			return view;
		}
        

         //透视变换
        public VETransform3D Perspective()
        {
            VETransform3D trans = new VETransform3D();
			double far = 1.0f / (float)Math.Tan(this.fov * 0.5f);

			trans.SetZero();

			trans.d11 = far / aspect ;
			trans.d22 = far;
			trans.d33 = zf / (zf - zn);
			trans.d34 = 1;
			trans.d43 = -zn * zf / (zf - zn);

			//trans.d11 = 1;
			//trans.d22 = 1;
			//trans.d33 = 0;
			//trans.d43 = -1 / zn;
			//trans.d44 = 0;
			return trans;
        }

    }
}
