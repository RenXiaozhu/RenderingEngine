using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public enum CATransform3DType
    {
        CATransform3DIdentity,
        CATransform3DSymmetryXY,
        CATransform3DSymmetryYZ,
        CATransform3DSymmetryZX
    }

    public enum CATransform2DType
    {
        CATransform2DIdentity,
        CATransform2DSymmetryXY,
        CATransform2DSymmetryYZ,
        CATransform2DSymmetryZX
    }

    //坐标系
    public enum SystemCross
    {
        SystemCross_X,
        SystemCross_Y,
        SystemCross_Z,
    }
    class MatrixTransform
    {

    }

    public class VETransform2D
    {
        public double d11, d12, d13;
        public double d21, d22, d23;
        public double d31, d32, d33;


        //单位矩阵
        public void VETransform2DIdentity()
        {
            this.d11 = 1;
            this.d22 = 1;
            this.d33 = 1;
        }

        public void VETransform2DTranslation(double tx, double ty)
        {
            this.d11 = 1;
            this.d22 = 1;
            this.d33 = 1;
            this.d31 = tx;
            this.d32 = ty;
        }

        public void VETransform2DRotation(double angle)
        {
            this.d11 = Math.Cos(angle);
            this.d12 = -Math.Sin(angle);
            this.d21 = Math.Sin(angle);
            this.d22 = Math.Cos(angle);
            this.d33 = 1;
        }

        public void VETransform2DScale(double sx, double sy)
        {
            this.d11 = sx;
            this.d22 = sy;
            this.d33 = 1;
        }


    }

    public class VETransform3D
    {
        public double d11, d12, d13, d14;
        public double d21, d22, d23, d24;
        public double d31, d32, d33, d34;
        public double d41, d42, d43, d44;

        public VETransform3D()
        {
            d44 = 1;
        }

        public void SetZero()
        {
            d11 = d12 = d13 = d14 = 0;
            d21 = d22 = d23 = d24 = 0;
            d31 = d32 = d33 = d34 = 0;
            d41 = d42 = d43 = d44 = 0;
        }
        //单位矩阵
        public void VETransform3DIdentity()
        {
            this.d11 = 1;
            this.d22 = 1;
            this.d33 = 1;
            this.d44 = 1;
        }

        //对称变换 关于XY对称
        public void VETransform3DSymmetryXY()
        {
            this.d11 = 1;
            this.d22 = 1;
            this.d33 = -1;
            this.d44 = 1;
        }

        //对称变换 关于YZ对称
        public void VETransform3DSymmetryYZ()
        {
            this.d11 = -1;
            this.d22 = 1;
            this.d33 = 1;
            this.d44 = 1;
        }

        //对称变换 关于ZX对称
        public void VETransform3DSymmetryZX()
        {
            this.d11 = 1;
            this.d22 = -1;
            this.d33 = 1;
            this.d44 = 1;
        }

        public void VETransform3DTranslation(double tx, double ty, double tz)
        {
            this.d11 = 1;
            this.d22 = 1;
            this.d33 = 1;
            this.d44 = 1;
            this.d14 = tx;
            this.d24 = ty;
            this.d34 = tz;
        }
        /*
         * 坐标原点的旋转
         * 
         */
        public void VETransform3DRotation(double angle, SystemCross cross, bool isLeftSystem)
        {
            if (isLeftSystem)
            {
                switch (cross)
                {
                    case SystemCross.SystemCross_X:
                        this.d11 = 1;
                        this.d23 = Math.Sin(angle);
                        this.d32 = -Math.Sin(angle);
                        this.d21 = Math.Sin(angle);
                        this.d33 = Math.Cos(angle);
                        this.d44 = 1;
                        break;
                    case SystemCross.SystemCross_Y:
                        this.d11 = Math.Cos(angle);
                        this.d13 = -Math.Sin(angle);
                        this.d22 = 1;
                        this.d31 = Math.Sin(angle);
                        this.d33 = Math.Cos(angle);
                        this.d44 = 1;
                        break;
                    case SystemCross.SystemCross_Z:
                        this.d11 = Math.Cos(angle);
                        this.d12 = Math.Sin(angle);
                        this.d21 = -Math.Sin(angle);
                        this.d22 = Math.Cos(angle);
                        this.d33 = 1;
                        this.d44 = 1;
                        break;
                }
            }
            else
            {
                switch (cross)
                {
                    case SystemCross.SystemCross_X:
                        this.d11 = 1;
                        this.d23 = -Math.Sin(angle);
                        this.d32 = Math.Sin(angle);
                        this.d21 = Math.Sin(angle);
                        this.d33 = Math.Cos(angle);
                        this.d44 = 1;
                        break;
                    case SystemCross.SystemCross_Y:
                        this.d11 = Math.Cos(angle);
                        this.d13 = Math.Sin(angle);
                        this.d22 = 1;
                        this.d31 = -Math.Sin(angle);
                        this.d33 = Math.Cos(angle);
                        this.d44 = 1;
                        break;
                    case SystemCross.SystemCross_Z:
                        this.d11 = Math.Cos(angle);
                        this.d12 = -Math.Sin(angle);
                        this.d21 = Math.Sin(angle);
                        this.d22 = Math.Cos(angle);
                        this.d33 = 1;
                        this.d44 = 1;
                        break;
                }
            }
        }

        //  n 表示任意旋转轴
        public void VETransform3DRotation(double angle, Vector4 n)
        {
            this.d11 = n.x * n.x * (1 - Math.Cos(angle )) + Math.Cos(angle);
            this.d21 = n.x * n.y * (1 - Math.Cos(angle)) + n.z * Math.Sin(angle );
            this.d31 = n.x * n.z * (1 - Math.Cos(angle)) - n.y * Math.Sin(angle);

            this.d12 = n.x * n.y * (1 - Math.Cos(angle)) - n.z * Math.Sin(angle);
            this.d22 = n.y * n.y * (1 - Math.Cos(angle)) + Math.Cos(angle);
            this.d32 = n.y * n.z * (1 - Math.Cos(angle)) + n.x * Math.Sin(angle);

            this.d13 = n.x * n.z * (1 - Math.Cos(angle)) + n.y * Math.Sin(angle);
            this.d23 = n.y * n.z * (1 - Math.Cos(angle)) - n.x * Math.Sin(angle);
            this.d33 = n.z * n.z * (1 - Math.Cos(angle)) + Math.Cos(angle);
        }

        /*
         * 参照坐标原点的缩放
         */
        public void VETransform3DScale(double sx, double sy, double sz)
        {
            this.d11 = sx;
            this.d22 = sy;
            this.d33 = sz;
            this.d44 = 1;
        }
        /*
         * 右手坐标系
         * 参照任意点P(Xr,Yr,Zr)的放缩变换
         */ 
        public void VETransform3DScale(double sx, double sy, double sz, Vector3 vt, bool isLeftSystem)
        {
            this.d11 = sx;
            this.d22 = sy;
            this.d33 = sz;
            this.d44 = 1;
            if (isLeftSystem)
            {
                this.d41 = -sx * vt.x + vt.x;
                this.d42 = -sy * vt.y + vt.y;
                this.d43 = -sz * vt.z + vt.z;
            }
            else
            {
                this.d14 = -sx * vt.x + vt.x;
                this.d24 = -sy * vt.y + vt.y;
                this.d34 = -sz * vt.z + vt.z;
            }

        }


        // 以X Y Z轴的错切变换
        public void VEShearTransformation(double degreeX, double degreeY , double degreeZ, SystemCross cross)
        {
            this.VETransform3DIdentity();
           
            switch (cross)
            {
                case SystemCross.SystemCross_X:
                    {
                        this.d21 = Math.Tan(degreeY/ 180);
                        this.d31 = Math.Tan(degreeZ / 180);
                    }
                    break;
                case SystemCross.SystemCross_Y:
                    {
                        this.d12 = Math.Tan(degreeX / 180);
                        this.d32 = Math.Tan(degreeZ / 180);
                    }
                    break;
                case SystemCross.SystemCross_Z:
                    {
                        this.d13 = Math.Tan(degreeX / 180);
                        this.d23 = Math.Tan(degreeY / 180);
                    }
                    break;
            }       
        }

        //2 3维矢量和 几何意义在于将 vt1 点平移到 vt2点 2维坐标可以表示维（x,y,0）
        public static Vector3 VectorSum(Vector3 vt1, Vector3 vt2)
        {
            Vector3 vt = new Vector3();
            vt.x = vt1.x + vt2.x;
            vt.y = vt1.y + vt2.y;
            vt.z = vt1.z + vt2.z;
            return vt;
        }

        //2 3维矢量积 几何意义在于延伸或缩短 2维坐标可以表示维（x,y,0）
        public static Vector3 VectorMultiplyNum(double num, Vector3 vt)
        {
            Vector3 vct = new Vector3();
            vct.x = vt.x * num;
            vct.y = vt.y * num;
            vct.z = vt.z * num;

            return vct;
        }

        //2 3维适量点积 几何意义表示 向量vt1在vt2上的投影长度 2维坐标可以表示维（x,y,0）
        public static double VectorPointMultiply(Vector3 vt1, Vector3 vt2)
        {
            double vt = vt1.x * vt2.x + vt1.y * vt2.y + vt1.z * vt2.z;
            return vt;
        }

        //2 3维矢量长度 2维坐标可以表示维（x,y,0）
        public static double VectorLength(Vector3 vt)
        {
            return Math.Sqrt(vt.x * vt.x + vt.y * vt.y + vt.z * vt.z);
        }

        //2 3维矢量夹角cos值
        public static double CosUV(Vector3 ut, Vector3 vt)
        {
            return (ut.x * vt.x + vt.y * ut.y + ut.z * vt.z) / (Math.Abs(Math.Sqrt(ut.x * ut.x + ut.y * ut.y + ut.z * ut.z)) * Math.Abs(Math.Sqrt(vt.x * vt.x + vt.y * vt.y + vt.z * vt.z)));
        }

        // 2 3维矢量叉积 几何意义在于求得两向量所在平面的法向量 2维坐标可以表示维（x,y,0）
        public static Vector3 VectorCrossMultiply(Vector3 ut, Vector3 vt)
        {
            Vector3 vct = new Vector3();
            vct.x = ut.y * vt.z - vt.y * ut.z;
            vct.y = vt.x * ut.z - ut.x * vt.z;
            vct.z = ut.x * vt.y - vt.x * ut.y;
            return vct;
        }

        //左乘  4x1*4x4 
        public Vector4 Maxtrix1x4(Vector4 vector)
        {
            double x = this.d11 * vector.x + this.d21 * vector.y + this.d31 * vector.z + this.d41 * vector.h;
            double y = this.d12 * vector.x + this.d22 * vector.y + this.d32 * vector.z + this.d42 * vector.h;
            double z = this.d13 * vector.x + this.d23 * vector.y + this.d33 * vector.z + this.d43 * vector.h;
            double h = this.d14 * vector.x + this.d24 * vector.y + this.d34 * vector.z + this.d44 * vector.h;
            return new Vector4(x, y, z, h);
        }

		//右乘   4x4 *  4x1
		public Vector4 Maxtrix4x1(Vector4 vector)
		{
			double x = this.d11 * vector.x + this.d12 * vector.y + this.d13 * vector.z + this.d14 * vector.h;
			double z = this.d31 * vector.x + this.d32 * vector.y + this.d33 * vector.z + this.d34 * vector.h;
			double y = this.d21 * vector.x + this.d22 * vector.y + this.d23 * vector.z + this.d24 * vector.h;
			double h = this.d41 * vector.x + this.d42 * vector.y + this.d43 * vector.z + this.d44 * vector.h;
			return new Vector4(x, y, z, h);
		}

		//右乘   4x4 *  4x4
		public static VETransform3D operator*(VETransform3D m, VETransform3D n)
        {
            VETransform3D trans = new VETransform3D();
            trans.d11 = m.d11 * n.d11 + m.d12 * n.d21 + m.d13 * n.d31 + m.d14 * n.d41;
            trans.d12 = m.d11 * n.d12 + m.d12 * n.d22 + m.d13 * n.d32 + m.d14 * n.d42;
            trans.d13 = m.d11 * n.d13 + m.d12 * n.d23 + m.d13 * n.d33 + m.d14 * n.d43;
            trans.d14 = m.d11 * n.d14 + m.d12 * n.d24 + m.d13 * n.d34 + m.d14 * n.d44;

            trans.d21 = m.d21 * n.d11 + m.d22 * n.d21 + m.d23 * n.d31 + m.d24 * n.d41;
            trans.d22 = m.d21 * n.d12 + m.d22 * n.d22 + m.d23 * n.d32 + m.d24 * n.d42;
            trans.d23 = m.d21 * n.d13 + m.d22 * n.d23 + m.d23 * n.d33 + m.d24 * n.d43;
            trans.d24 = m.d21 * n.d14 + m.d22 * n.d24 + m.d23 * n.d34 + m.d24 * n.d44;

            trans.d31 = m.d31 * n.d11 + m.d32 * n.d21 + m.d33 * n.d31 + m.d34 * n.d41;
            trans.d32 = m.d31 * n.d12 + m.d32 * n.d22 + m.d33 * n.d32 + m.d34 * n.d42;
            trans.d33 = m.d31 * n.d13 + m.d32 * n.d23 + m.d33 * n.d33 + m.d34 * n.d43;
            trans.d34 = m.d31 * n.d14 + m.d32 * n.d24 + m.d33 * n.d34 + m.d34 * n.d44;

            trans.d41 = m.d41 * n.d11 + m.d42 * n.d21 + m.d43 * n.d31 + m.d44 * n.d41;
            trans.d42 = m.d41 * n.d12 + m.d42 * n.d22 + m.d43 * n.d32 + m.d44 * n.d42;
            trans.d43 = m.d41 * n.d13 + m.d42 * n.d23 + m.d43 * n.d33 + m.d44 * n.d43;
            trans.d44 = m.d41 * n.d14 + m.d42 * n.d24 + m.d43 * n.d34 + m.d44 * n.d44;

            return trans;
        }

        //加法   4x4 *  4x4
        public static VETransform3D operator +(VETransform3D m, VETransform3D n)
        {
            VETransform3D trans = new VETransform3D();
            trans.d11 = m.d11 + n.d11; trans.d12 = m.d12 + n.d12; trans.d13 = m.d13 + n.d31; trans.d14 = m.d14 + n.d41;
            trans.d21 = m.d21 + n.d12; trans.d22 = m.d22 + n.d22; trans.d23 = m.d23 + n.d32; trans.d24 = m.d24 + n.d42;
            trans.d31 = m.d31 + n.d13; trans.d32 = m.d32 + n.d23; trans.d33 = m.d33 + n.d33; trans.d34 = m.d34 + n.d43;
            trans.d41 = m.d41 + n.d14; trans.d42 = m.d42 + n.d24; trans.d43 = m.d43 + n.d34; trans.d44 = m.d44 + n.d44;
            return trans;
        }

        //减法    4x4 *  4x4
        public static VETransform3D operator -(VETransform3D m, VETransform3D n)
        {
            VETransform3D trans = new VETransform3D();
            trans.d11 = m.d11 - n.d11; trans.d12 = m.d12 - n.d12; trans.d13 = m.d13 - n.d31; trans.d14 = m.d14 - n.d41;
            trans.d21 = m.d21 - n.d12; trans.d22 = m.d22 - n.d22; trans.d23 = m.d23 - n.d32; trans.d24 = m.d24 - n.d42;
            trans.d31 = m.d31 - n.d13; trans.d32 = m.d32 - n.d23; trans.d33 = m.d33 - n.d33; trans.d34 = m.d34 - n.d43;
            trans.d41 = m.d41 - n.d14; trans.d42 = m.d42 - n.d24; trans.d43 = m.d43 - n.d34; trans.d44 = m.d44 - n.d44;
            return trans;
        }



    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      