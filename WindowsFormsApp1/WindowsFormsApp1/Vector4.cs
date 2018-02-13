using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Vector3
    {
        public double x;
        public double y;
        public double z;
        public Vector3() { }
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    //3维齐次坐标
    public struct Vector4
    {
        public double x;
        public double y;
        public double z;
        public double h;

        public Vector4 StandardIdentity
        {
            get
            {
                Vector4 vt4 = new Vector4(this.x, this.y, this.z, this.h);
                vt4.Normalize();
                return vt4;
            }
        }

        public Vector4(double x, double y, double z, double h)
            : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.h = h;
        }

        // 拷贝坐标
        public Vector4(Vector4 other)
            : this()
        {
            this.x = other.x;
            this.y = other.y;
            this.z = other.z;
            this.h = other.h;
        }

        

        //2 3维矢量长度 2维坐标可以表示维（x,y,0）
        public double Length()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        //矢量归一化
        public void Normalize()
        {
            double length = this.Length();
            double coefficient = 1 / length;
            this.Scale(coefficient);
        }

        public void Scale(double scale)
        {
            this.x *= scale;
            this.y *= scale;
            this.z *= scale;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("x = {0} , y = {1} , z = {2} , h = {3}", this.x, this.y, this.z, this.h);
            return str.ToString();
        }

   

        /// <summary>
        /// 运算符重载
        /// 
        /// 2 3维矢量和 几何意义在于将 vt1 点平移到 vt2点 2维坐标可以表示维（x,y,0）
        public static Vector4 operator +(Vector4 m, Vector4 n)
        {
            return new Vector4(m.x + n.x, m.y + n.y, m.z + n.z, m.h + n.h);
        }

        public static Vector4 operator -(Vector4 m, Vector4 n)
        {
            return new Vector4(m.x - n.x, m.y - n.y, m.z - n.z, m.h - n.h);
        }

        //2 3维矢量积 几何意义在于延伸或缩短 2维坐标可以表示维（x,y,0）
        public static Vector4 operator /(Vector4 m, double t)
        {
            return new Vector4(m.x / t, m.y / t, m.z / t, m.h /t);
        }
        //2 3维矢量积 几何意义在于延伸或缩短 2维坐标可以表示维（x,y,0）
        public static Vector4 operator *(Vector4 m, double t)
        {
            return new Vector4(m.x * t, m.y * t, m.z * t, m.h * t);
        }

        public static bool operator ==(Vector4 m, Vector4 n)
        {
            return m.x == n.x && m.y == n.y && m.z == n.z;
        }
        public static bool operator !=(Vector4 m, Vector4 n)
        {
            return m.x != n.x && m.y != n.y && m.z != n.z;
        }

        public void zero()
        {
            x = y = z = 0;
        }

        public static  Vector4 operator -(Vector4 n)
        {
            return new Vector4(-n.x, -n.y, -n.z, n.h);
        }

        //矢量点积运算  2 3维适量点积 几何意义表示 向量vt1在vt2上的投影长度 2维坐标可以表示维（x,y,0）
        public  double dot(Vector4 n)
        {
            return this.x * n.x + this.y * n.y + this.z * n.z + this.h * n.h;
        }
        //矢量叉积运算 
        //   ||aXb|| = ||a|| * ||b|| *sinQ 
        public  Vector4 CrossMultiply( Vector4 vt)
        {
            return new Vector4(this.y * vt.z - vt.y * this.z, vt.x * this.z - this.x * vt.z, this.x * vt.y - vt.x * this.y , 1);
        }
        //
        
        /// <summary>
        /// AB的cosQ值反应了夹角的大小和方向
        /// 即 取 A · B 的符号结果
        ///  小于0    0' 大于等于  Q  小于 90'    方向基本相同
        ///  等于0        Q<= 90'     正交
        ///  大于0     90' 大于 Q 小于等于 180'    方向基本相反
        ///  
        /// </summary>
        /// 矢量A
        /// <param name="A"></param>
        /// 矢量B
        /// <param name="B"></param>
        /// 
        /// 
        /// <returns></returns>
        public  double CosUV(Vector4 A)
        {
            return (this.dot(A)) / this.Length() * A.Length();
        }

        // 两向量夹角大小
        public double Degree(Vector4 A)
        {
            return Math.Acos((this.dot(A)) / this.Length() * A.Length());
        }

        /* 向量投影
         * 给定 向量 V   N   ，能将V 分解成 Vh (平行于N的分向量)  和 Vv (垂直于N的分向量)
         * V = Vh + Vv
         * Vh 一般成为 V 在N上的投影
        */

        // 在 N 上的平行分量
        public Vector4 PComponent(Vector4 n)
        {
            return n * (this.dot(n) / n.Length() * n.Length());
        }

        // 在N上的垂直分量

        public Vector4 VComponent(Vector4 n)
        {
            return this - this.PComponent(n);
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

        public static Vector3 VectorSum(Vector3 vt1, Vector3 vt2)
        {
            Vector3 vt = new Vector3();
            vt.x = vt1.x + vt2.x;
            vt.y = vt1.y + vt2.y;
            vt.z = vt1.z + vt2.z;
            return vt;
        }
        //2维矢量夹角cos值
        public static double CosUV(Vector2 ut, Vector2 vt)
        {
            return (ut.x * vt.x + vt.y * ut.y) / (Math.Abs(Math.Sqrt(ut.x * ut.x + ut.y * ut.y)) * Math.Abs(Math.Sqrt(vt.x * vt.x + vt.y * vt.y)));
        }

        //2维矢量和
        public static Vector2 VectorSum(Vector2 vt1, Vector2 vt2)
        {
            Vector2 vt = new Vector2();
            vt.x = vt1.x + vt2.x;
            vt.y = vt1.y + vt2.y;
            return vt;
        }

        //2维数乘
        public static Vector2 VectorMultiplyNum(int num, Vector2 vt)
        {
            Vector2 vct = new Vector2();
            vct.x = vt.x * num;
            vct.y = vt.y * num;
            return vct;
        }

        //2维矢量点积   几何意义表示 向量vt1在vt2上的投影长度
        public static double VectorPointMultiply(Vector2 vt1, Vector2 vt2)
        {
            double vt = vt1.x * vt2.x + vt1.y * vt2.y;
            return vt;
        }
    }
}
