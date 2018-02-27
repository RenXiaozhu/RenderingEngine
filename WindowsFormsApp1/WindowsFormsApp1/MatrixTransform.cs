using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public enum CATransform3DTYpe
    {
        CATransform3DIdentity,
        CATransform3DSYmmetryxy,
        CATransform3DSYmmetrtyz,
        CATransform3DSYmmetryzx
    }

    public enum CATransform2DType
    {
        CATransform2DIdentity,
        CATransform2DSYmmetryx,
        CATransform2DSYmmetryyz,
        CATransform2DSYmmetryzx
    }

    //坐标系
    public enum SystemCross
    {
        SYstemCross_X,
        SYstemCross_Y,
        SYstemCross_Z,
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
        public void VETransform2DIdentitY()
        {
            this.d11 = 1;
            this.d22 = 1;
            this.d33 = 1;
        }

        public void VETransform2DTranslation(double tX, double tY)
        {
            this.d11 = 1;
            this.d22 = 1;
            this.d33 = 1;
            this.d31 = tX;
            this.d32 = tY;
        }

        public void VETransform2DRotation(double angle)
        {
            this.d11 = Math.Cos(angle);
            this.d12 = -Math.Sin(angle);
            this.d21 = Math.Sin(angle);
            this.d22 = Math.Cos(angle);
            this.d33 = 1;
        }

        public void VETransform2DScale(double sX, double sY)
        {
            this.d11 = sX;
            this.d22 = sY;
            this.d33 = 1;
        }


    }
    //
    //public class VETransform3D
    //{
    //    public double d11, d12, d13, d14;
    //    public double d21, d22, d23, d24;
    //    public double d31, d32, d33, d34;
    //    public double d41, d42, d43, d44;

    //    public VETransform3D()
    //    {
    //        d44 = 1;
    //    }

    //    public void SetZero()
    //    {
    //        d11 = d12 = d13 = d14 = 0;
    //        d21 = d22 = d23 = d24 = 0;
    //        d31 = d32 = d33 = d34 = 0;
    //        d41 = d42 = d43 = d44 = 0;
    //    }
    //    //单位矩阵
    //    public void VETransform3DIdentitY()
    //    {
    //        this.d11 = 1;
    //        this.d22 = 1;
    //        this.d33 = 1;
    //        this.d44 = 1;
    //    }

    //    //对称变换 关于XY对称
    //    public void VETransform3DSYmmetrYXY()
    //    {
    //        this.d11 = 1;
    //        this.d22 = 1;
    //        this.d33 = -1;
    //        this.d44 = 1;
    //    }

    //    //对称变换 关于YZ对称
    //    public void VETransform3DSYmmetrYYZ()
    //    {
    //        this.d11 = -1;
    //        this.d22 = 1;
    //        this.d33 = 1;
    //        this.d44 = 1;
    //    }

    //    //对称变换 关于ZX对称
    //    public void VETransform3DSYmmetrYZX()
    //    {
    //        this.d11 = 1;
    //        this.d22 = -1;
    //        this.d33 = 1;
    //        this.d44 = 1;
    //    }

    //    public void VETransform3DTranslation(double tX, double tY, double tZ)
    //    {
    //        this.d11 = 1;
    //        this.d22 = 1;
    //        this.d33 = 1;
    //        this.d44 = 1;
    //        this.d14 = tX;
    //        this.d24 = tY;
    //        this.d34 = tZ;
    //    }
    //    /*
    //     * 坐标原点的旋转
    //     * 
    //     */
    //    public void VETransform3DRotation(double angleX,double angleY,double angleZ, bool isLeftSYstem)
    //    {
    //        if (isLeftSYstem)
    //        {
    //            VETransform3D transX = new VETransform3D();
    //            VETransform3D transY = new VETransform3D();
    //            VETransform3D transZ = new VETransform3D();

    //            transX.d11 = 1;
    //            transX.d23 = Math.Sin(angleX);
    //            transX.d32 = -Math.Sin(angleX);
    //            transX.d21 = Math.Sin(angleX);
    //            transX.d33 = Math.Cos(angleX);
    //            transX.d44 = 1;

    //            transY.d11 = Math.Cos(angleY);
    //            transY.d13 = -Math.Sin(angleY);
    //            transY.d22 = 1;
    //            transY.d31 = Math.Sin(angleY);
    //            transY.d33 = Math.Cos(angleY);
    //            transY.d44 = 1;

    //            transZ.d11 = Math.Cos(angleZ);
    //            transZ.d12 = Math.Sin(angleZ);
    //            transZ.d21 = -Math.Sin(angleZ);
    //            transZ.d22 = Math.Cos(angleZ);
    //            transZ.d33 = 1;
    //            transZ.d44 = 1;

    //            VETransform3D trans = transX * transY * transZ;

    //            this.d11 = trans.d11; this.d12 = trans.d12; this.d13 = trans.d13; this.d14 = trans.d14;
    //            this.d21 = trans.d21; this.d22 = trans.d22; this.d23 = trans.d23; this.d24 = trans.d24;
    //            this.d31 = trans.d31; this.d32 = trans.d32; this.d33 = trans.d33; this.d34 = trans.d34;
    //            this.d41 = trans.d41; this.d42 = trans.d42; this.d43 = trans.d43; this.d44 = trans.d44;
    //        }
    //        else
    //        {
    //            VETransform3D transX = new VETransform3D();
    //            VETransform3D transY = new VETransform3D();
    //            VETransform3D transZ = new VETransform3D();

    //            transX.d11 = 1;
    //            transX.d23 = -Math.Sin(angleX);
    //            transX.d32 = Math.Sin(angleX);
    //            transX.d21 = Math.Sin(angleX);
    //            transX.d33 = Math.Cos(angleX);
    //            transX.d44 = 1;

    //            transY.d11 = Math.Cos(angleY);
    //            transY.d13 = Math.Sin(angleY);
    //            transY.d22 = 1;
    //            transY.d31 = -Math.Sin(angleY);
    //            transY.d33 = Math.Cos(angleY);
    //            transY.d44 = 1;
 

    //            transZ.d11 = Math.Cos(angleZ);
    //            transZ.d12 = -Math.Sin(angleZ);
    //            transZ.d21 = Math.Sin(angleZ);
    //            transZ.d22 = Math.Cos(angleZ);
    //            transZ.d33 = 1;
    //            transZ.d44 = 1;

    //            VETransform3D trans = transX * transY * transZ;

    //            this.d11 = trans.d11; this.d12 = trans.d12; this.d13 = trans.d13; this.d14 = trans.d14;
    //            this.d21 = trans.d21; this.d22 = trans.d22; this.d23 = trans.d23; this.d24 = trans.d24;
    //            this.d31 = trans.d31; this.d32 = trans.d32; this.d33 = trans.d33; this.d34 = trans.d34;
    //            this.d41 = trans.d41; this.d42 = trans.d42; this.d43 = trans.d43; this.d44 = trans.d44;
    //        }
    //    }

    //    //  n 表示任意旋转轴
    //    public void VETransform3DRotation(double angle, Vector4 n)
    //    {
    //        this.d11 = n.X * n.X * (1 - Math.Cos(angle )) + Math.Cos(angle);
    //        this.d21 = n.X * n.Y * (1 - Math.Cos(angle)) + n.Z * Math.Sin(angle );
    //        this.d31 = n.X * n.Z * (1 - Math.Cos(angle)) - n.Y * Math.Sin(angle);

    //        this.d12 = n.X * n.Y * (1 - Math.Cos(angle)) - n.Z * Math.Sin(angle);
    //        this.d22 = n.Y * n.Y * (1 - Math.Cos(angle)) + Math.Cos(angle);
    //        this.d32 = n.Y * n.Z * (1 - Math.Cos(angle)) + n.X * Math.Sin(angle);

    //        this.d13 = n.X * n.Z * (1 - Math.Cos(angle)) + n.Y * Math.Sin(angle);
    //        this.d23 = n.Y * n.Z * (1 - Math.Cos(angle)) - n.X * Math.Sin(angle);
    //        this.d33 = n.Z * n.Z * (1 - Math.Cos(angle)) + Math.Cos(angle);
    //    }

    //    /*
    //     * 参照坐标原点的缩放
    //     */
    //    public void VETransform3DScale(double sX, double sY, double sZ)
    //    {
    //        this.d11 = sX;
    //        this.d22 = sY;
    //        this.d33 = sZ;
    //        this.d44 = 1;
    //    }
    //    /*
    //     * 右手坐标系
    //     * 参照任意点P(Xr,Yr,Zr)的放缩变换
    //     */ 
    //    public void VETransform3DScale(double sX, double sY, double sZ, Vector3 vt, bool isLeftSYstem)
    //    {
    //        this.d11 = sX;
    //        this.d22 = sY;
    //        this.d33 = sZ;
    //        this.d44 = 1;
    //        if (isLeftSYstem)
    //        {
    //            this.d41 = -sX * vt.x + vt.x;
    //            this.d42 = -sY * vt.y + vt.y;
    //            this.d43 = -sZ * vt.z + vt.z;
    //        }
    //        else
    //        {
    //            this.d14 = -sX * vt.x + vt.x;
    //            this.d24 = -sY * vt.y + vt.y;
    //            this.d34 = -sZ * vt.z + vt.z;
    //        }

    //    }


    //    // 以X Y Z轴的错切变换
    //    public void VEShearTransformation(double degreeX, double degreeY , double degreeZ, SystemCross cross)
    //    {
    //        this.VETransform3DIdentitY();
           
    //        switch (cross)
    //        {
    //            case SystemCross.SYstemCross_X:
    //                {
    //                    this.d21 = Math.Tan(degreeY/ 180);
    //                    this.d31 = Math.Tan(degreeZ / 180);
    //                }
    //                break;
    //            case SystemCross.SYstemCross_Y:
    //                {
    //                    this.d12 = Math.Tan(degreeX / 180);
    //                    this.d32 = Math.Tan(degreeZ / 180);
    //                }
    //                break;
    //            case SystemCross.SYstemCross_Z:
    //                {
    //                    this.d13 = Math.Tan(degreeX / 180);
    //                    this.d23 = Math.Tan(degreeY / 180);
    //                }
    //                break;
    //        }       
    //    }

    //    //2 3维矢量和 几何意义在于将 vt1 点平移到 vt2点 2维坐标可以表示维（X,Y,0）
    //    public static Vector3 VectorSum(Vector3 vt1, Vector3 vt2)
    //    {
    //        Vector3 vt = new Vector3();
    //        vt.x = vt1.x + vt2.x;
    //        vt.y = vt1.y + vt2.y;
    //        vt.z = vt1.z + vt2.z;
    //        return vt;
    //    }

    //    //2 3维矢量积 几何意义在于延伸或缩短 2维坐标可以表示维（X,Y,0）
    //    public static Vector3 VectorMultiplYNum(double num, Vector3 vt)
    //    {
    //        Vector3 vct = new Vector3();
    //        vct.x = vt.x * num;
    //        vct.y = vt.y * num;
    //        vct.z = vt.z * num;

    //        return vct;
    //    }

    //    //2 3维适量点积 几何意义表示 向量vt1在vt2上的投影长度 2维坐标可以表示维（X,Y,0）
    //    public static double VectorPointMultiplY(Vector3 vt1, Vector3 vt2)
    //    {
    //        double vt = vt1.x * vt2.x + vt1.y * vt2.y + vt1.z * vt2.z;
    //        return vt;
    //    }

    //    //2 3维矢量长度 2维坐标可以表示维（X,Y,0）
    //    public static double VectorLength(Vector3 vt)
    //    {
    //        return Math.Sqrt(vt.x * vt.x + vt.y * vt.y + vt.z * vt.z);
    //    }

    //    //2 3维矢量夹角cos值
    //    public static double CosUV(Vector3 ut, Vector3 vt)
    //    {
    //        return (ut.x * vt.x + vt.y * ut.y + ut.z * vt.z) / (Math.Abs(Math.Sqrt(ut.x * ut.x + ut.y * ut.y + ut.z * ut.z)) * Math.Abs(Math.Sqrt(vt.x * vt.x + vt.y * vt.y + vt.z * vt.z)));
    //    }

    //    // 2 3维矢量叉积 几何意义在于求得两向量所在平面的法向量 2维坐标可以表示维（X,Y,0）
    //    public static Vector3 VectorCrossMultiplY(Vector3 ut, Vector3 vt)
    //    {
    //        Vector3 vct = new Vector3();
    //        vct.x = ut.y * vt.z - vt.y * ut.z;
    //        vct.y = vt.x * ut.z - ut.x * vt.z;
    //        vct.z = ut.x * vt.y - vt.x * ut.y;
    //        return vct;
    //    }

    //    //左乘  4X1*4X4 
    //    public Vector4 Maxtrix1x4(Vector4 vector)
    //    {
    //        double X = this.d11 * vector.X + this.d21 * vector.Y + this.d31 * vector.Z + this.d41 * vector.W;
    //        double Y = this.d12 * vector.X + this.d22 * vector.Y + this.d32 * vector.Z + this.d42 * vector.W;
    //        double Z = this.d13 * vector.X + this.d23 * vector.Y + this.d33 * vector.Z + this.d43 * vector.W;
    //        double h = this.d14 * vector.X + this.d24 * vector.Y + this.d34 * vector.Z + this.d44 * vector.W;
    //        return new Vector4((float)X, (float)Y, (float)Z, (float)h);
    //    }

    //    //右乘   4X4 *  4X1
    //    public Vector4 Maxtrix4x1(Vector4 vector)
    //    {
    //        double X = this.d11 * vector.X + this.d12 * vector.Y + this.d13 * vector.Z + this.d14 * vector.W;
    //        double Z = this.d31 * vector.X + this.d32 * vector.Y + this.d33 * vector.Z + this.d34 * vector.W;
    //        double Y = this.d21 * vector.X + this.d22 * vector.Y + this.d23 * vector.Z + this.d24 * vector.W;
    //        double h = this.d41 * vector.X + this.d42 * vector.Y + this.d43 * vector.Z + this.d44 * vector.W;
    //        return new Vector4((float)X,(float) Y,(float) Z,(float) h);
    //    }

    //    //右乘   4X4 *  4X4
    //    public static VETransform3D operator*(VETransform3D m, VETransform3D n)
    //    {
    //        VETransform3D trans = new VETransform3D();
    //        trans.d11 = m.d11 * n.d11 + m.d12 * n.d21 + m.d13 * n.d31 + m.d14 * n.d41;
    //        trans.d12 = m.d11 * n.d12 + m.d12 * n.d22 + m.d13 * n.d32 + m.d14 * n.d42;
    //        trans.d13 = m.d11 * n.d13 + m.d12 * n.d23 + m.d13 * n.d33 + m.d14 * n.d43;
    //        trans.d14 = m.d11 * n.d14 + m.d12 * n.d24 + m.d13 * n.d34 + m.d14 * n.d44;

    //        trans.d21 = m.d21 * n.d11 + m.d22 * n.d21 + m.d23 * n.d31 + m.d24 * n.d41;
    //        trans.d22 = m.d21 * n.d12 + m.d22 * n.d22 + m.d23 * n.d32 + m.d24 * n.d42;
    //        trans.d23 = m.d21 * n.d13 + m.d22 * n.d23 + m.d23 * n.d33 + m.d24 * n.d43;
    //        trans.d24 = m.d21 * n.d14 + m.d22 * n.d24 + m.d23 * n.d34 + m.d24 * n.d44;

    //        trans.d31 = m.d31 * n.d11 + m.d32 * n.d21 + m.d33 * n.d31 + m.d34 * n.d41;
    //        trans.d32 = m.d31 * n.d12 + m.d32 * n.d22 + m.d33 * n.d32 + m.d34 * n.d42;
    //        trans.d33 = m.d31 * n.d13 + m.d32 * n.d23 + m.d33 * n.d33 + m.d34 * n.d43;
    //        trans.d34 = m.d31 * n.d14 + m.d32 * n.d24 + m.d33 * n.d34 + m.d34 * n.d44;

    //        trans.d41 = m.d41 * n.d11 + m.d42 * n.d21 + m.d43 * n.d31 + m.d44 * n.d41;
    //        trans.d42 = m.d41 * n.d12 + m.d42 * n.d22 + m.d43 * n.d32 + m.d44 * n.d42;
    //        trans.d43 = m.d41 * n.d13 + m.d42 * n.d23 + m.d43 * n.d33 + m.d44 * n.d43;
    //        trans.d44 = m.d41 * n.d14 + m.d42 * n.d24 + m.d43 * n.d34 + m.d44 * n.d44;

    //        return trans;
    //    }

    //    //加法   4X4 *  4X4
    //    public static VETransform3D operator +(VETransform3D m, VETransform3D n)
    //    {
    //        VETransform3D trans = new VETransform3D();
    //        trans.d11 = m.d11 + n.d11; trans.d12 = m.d12 + n.d12; trans.d13 = m.d13 + n.d31; trans.d14 = m.d14 + n.d41;
    //        trans.d21 = m.d21 + n.d12; trans.d22 = m.d22 + n.d22; trans.d23 = m.d23 + n.d32; trans.d24 = m.d24 + n.d42;
    //        trans.d31 = m.d31 + n.d13; trans.d32 = m.d32 + n.d23; trans.d33 = m.d33 + n.d33; trans.d34 = m.d34 + n.d43;
    //        trans.d41 = m.d41 + n.d14; trans.d42 = m.d42 + n.d24; trans.d43 = m.d43 + n.d34; trans.d44 = m.d44 + n.d44;
    //        return trans;
    //    }

    //    //减法    4X4 *  4X4
    //    public static VETransform3D operator -(VETransform3D m, VETransform3D n)
    //    {
    //        VETransform3D trans = new VETransform3D();
    //        trans.d11 = m.d11 - n.d11; trans.d12 = m.d12 - n.d12; trans.d13 = m.d13 - n.d31; trans.d14 = m.d14 - n.d41;
    //        trans.d21 = m.d21 - n.d12; trans.d22 = m.d22 - n.d22; trans.d23 = m.d23 - n.d32; trans.d24 = m.d24 - n.d42;
    //        trans.d31 = m.d31 - n.d13; trans.d32 = m.d32 - n.d23; trans.d33 = m.d33 - n.d33; trans.d34 = m.d34 - n.d43;
    //        trans.d41 = m.d41 - n.d14; trans.d42 = m.d42 - n.d24; trans.d43 = m.d43 - n.d34; trans.d44 = m.d44 - n.d44;
    //        return trans;
    //    }
    //}

    public class Matrix4x4
    {
        public float[,] M { get; set; }
        public Matrix4x4()
        {
            M = new float[4, 4];
            this.SetIdentity();
        }

        public static Matrix4x4 operator +(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 t = new Matrix4x4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    t.M[i, j] = a.M[i, j] + b.M[i, j];
                }
            }
            return t;
        }

        public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 t = new Matrix4x4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    t.M[i, j] = a.M[i, j] - b.M[i, j];
                }
            }
            return t;
        }

        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 t = new Matrix4x4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    t.M[j, i] = (a.M[j, 0] * b.M[0, i]) +
                        (a.M[j, 1] * b.M[1, i]) +
                        (a.M[j, 2] * b.M[2, i]) +
                        (a.M[j, 3] * b.M[3, i]);
                }
            }
            return t;
        }

        // c = a * f  
        public void Scale(float f)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.M[i, j] = this.M[i, j] * f;
                }
            }
        }

        // Y = X * m
        public Vector4 ApplY(Vector4 a)
        {
            Vector4 t = new Vector4();
            float X = a.X, Y = a.Y, Z = a.Z, W = a.W;
            t.X = X * this.M[0, 0] + Y * this.M[1, 0] + Z * this.M[2, 0] + W * this.M[3, 0];
            t.Y = X * this.M[0, 1] + Y * this.M[1, 1] + Z * this.M[2, 1] + W * this.M[3, 1];
            t.Z = X * this.M[0, 2] + Y * this.M[1, 2] + Z * this.M[2, 2] + W * this.M[3, 2];
            t.W = X * this.M[0, 3] + Y * this.M[1, 3] + Z * this.M[2, 3] + W * this.M[3, 3];
            return t;
        }

        public void SetIdentity()
        {
            this.M[0, 0] = this.M[1, 1] = this.M[2, 2] = this.M[3, 3] = 1.0f;
            this.M[0, 1] = this.M[0, 2] = this.M[0, 3] = 0.0f;
            this.M[1, 0] = this.M[1, 2] = this.M[1, 3] = 0.0f;
            this.M[2, 0] = this.M[2, 1] = this.M[2, 3] = 0.0f;
            this.M[3, 0] = this.M[3, 1] = this.M[3, 2] = 0.0f;
        }

        public void SetZero()
        {
            this.M[0, 0] = this.M[0, 1] = this.M[0, 2] = this.M[0, 3] = 0.0f;
            this.M[1, 0] = this.M[1, 1] = this.M[1, 2] = this.M[1, 3] = 0.0f;
            this.M[2, 0] = this.M[2, 1] = this.M[2, 2] = this.M[2, 3] = 0.0f;
            this.M[3, 0] = this.M[3, 1] = this.M[3, 2] = this.M[3, 3] = 0.0f;
        }

        // 平移变换
        public void SetTranslate(float X, float Y, float Z)
        {
            this.SetIdentity();
            this.M[3, 0] = X;
            this.M[3, 1] = Y;
            this.M[3, 2] = Z;
        }

        // 缩放变换
        public void SetScale(float X, float Y, float Z)
        {
            this.SetIdentity();
            this.M[0, 0] = X;
            this.M[1, 1] = Y;
            this.M[2, 2] = Z;
        }

        // 旋转矩阵
        public void SetRotate(float X, float Y, float Z, float theta)
        {
            float qsin = (float)Math.Sin(theta * 0.5f);
            float qcos = (float)Math.Cos(theta * 0.5f);
            Vector4 vec = new Vector4(X, Y, Z, 1.0f);
            float w = qcos;
            vec.Normalize();
            X = vec.X * qsin;
            Y = vec.Y * qsin;
            Z = vec.Z * qsin;
            this.M[0, 0] = 1 - 2 * Y * Y - 2 * Z * Z;
            this.M[1, 0] = 2 * X * Y - 2 * w * Z;
            this.M[2, 0] = 2 * X * Z + 2 * w * Y;
            this.M[0, 1] = 2 * X * Y + 2 * w * Z;
            this.M[1, 1] = 1 - 2 * X * X - 2 * Z * Z;
            this.M[2, 1] = 2 * Y * Z - 2 * w * X;
            this.M[0, 2] = 2 * X * Z - 2 * w * Y;
            this.M[1, 2] = 2 * Y * Z + 2 * w * X;
            this.M[2, 2] = 1 - 2 * X * X - 2 * Y * Y;
            this.M[0, 3] = this.M[1, 3] = this.M[2, 3] = 0.0f;
            this.M[3, 0] = this.M[3, 1] = this.M[3, 2] = 0.0f;
            this.M[3, 3] = 1.0f;
        }

        public void SetRotate(float degreeX, float degreeY, float degreeZ)
        {
            degreeX = degreeX * (float)Math.PI / 180;
            degreeY = degreeY * (float)Math.PI / 180;
            degreeZ = degreeZ * (float)Math.PI / 180;
            Matrix4x4 Z = new Matrix4x4();
            Z.M[0, 0] = (float)Math.Cos(degreeZ);
            Z.M[0, 1] = -(float)Math.Sin(degreeZ);
            Z.M[1, 0] = (float)Math.Sin(degreeZ);
            Z.M[1, 1] = (float)Math.Cos(degreeZ);
            Matrix4x4 X = new Matrix4x4();
            X.M[1, 1] = (float)Math.Cos(degreeX);
            X.M[1, 2] = -(float)Math.Sin(degreeX);
            X.M[2, 1] = (float)Math.Sin(degreeX);
            X.M[2, 2] = (float)Math.Cos(degreeX);
            Matrix4x4 Y = new Matrix4x4();
            Y.M[0, 0] = (float)Math.Cos(degreeY);
            Y.M[0, 2] = (float)Math.Sin(degreeY);
            Y.M[2, 0] = -(float)Math.Sin(degreeY);
            Y.M[2, 2] = (float)Math.Cos(degreeY);

            this.SetIdentity();
            Matrix4x4 final = Y * X * Z;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    this.M[i, j] = final.M[i, j];
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      