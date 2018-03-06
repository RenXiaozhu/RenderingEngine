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
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public Vector4 Normalized
        {
            get
            {
                Vector4 n = new Vector4(this.X, this.Y, this.Z, this.W);
                n.Normalize();
                return n;
            }
        }

        //public Vector4() { }

        public Vector4(float x, float y, float z, float w)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Vector4(Vector4 other)
            : this()
        {
            this.X = other.X;
            this.Y = other.Y;
            this.Z = other.Z;
            this.W = other.W;
        }

        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, 1);
        }

        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, 1);
        }

        public static Vector4 operator /(Vector4 a, float t)
        {
            return new Vector4(a.X / t, a.Y / t, a.Z / t, 1);
        }

        public static Vector4 operator *(Vector4 a, float t)
        {
            return new Vector4(a.X * t, a.Y * t, a.Z * t, 1); ;
        }

        public static bool operator ==(Vector4 a, Vector4 b)
        {
            if (a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W)
                return true;
            else
                return false;
        }

        public static bool operator !=(Vector4 a, Vector4 b)
        {
            if (a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W)
                return false;
            else
                return true;
        }

        public static float Dot(Vector4 a, Vector4 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Vector4 Cross(Vector4 a, Vector4 b)
        {
            float m1, m2, m3;
            m1 = a.Y * b.Z - a.Z * b.Y;
            m2 = a.Z * b.X - a.X * b.Z;
            m3 = a.X * b.Y - a.Y * b.X;
            return new Vector4(m1, m2, m3, 1f);
        }

        public float Length()
        {
            float sq = this.X * this.X + this.Y * this.Y + this.Z * this.Z;
            return (float)Math.Sqrt(sq);
        }

        // 矢量归一化
        public void Normalize()
        {
            float length = this.Length();
            if (length != 0.0f)
            {
                float inv = 1.0f / length;
                this.X *= inv;
                this.Y *= inv;
                this.Z *= inv;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("x = {0}, y = {1}, z = {2}, w = {3}", this.X, this.Y, this.Z, this.W);
            return sb.ToString();
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
        public  float CosUV(Vector4 A)
        {
            return (Vector4.Dot(this,A)) / this.Length() * A.Length();
        }

        // 两向量夹角大小
        public double Degree(Vector4 A)
        {
            return Math.Acos(Vector4.Dot(this,A) / this.Length() * A.Length());
        }

        /* 向量投影
         * 给定 向量 V   N   ，能将V 分解成 Vh (平行于N的分向量)  和 Vv (垂直于N的分向量)
         * V = Vh + Vv
         * Vh 一般成为 V 在N上的投影
        */

        // 在 N 上的平行分量
        public Vector4 PComponent(Vector4 n)
        {
            return n * (Vector4.Dot(this,n) / n.Length() * n.Length());
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
