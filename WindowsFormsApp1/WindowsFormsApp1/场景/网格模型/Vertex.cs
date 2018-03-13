using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class Vertex:ICloneable
    {   
        // 3维空间坐标
        public Vector4 Position { get; set; }

        public Vector4 nowPos { get; set; }
        /* 裁剪空间坐标 规格化坐标空间
         * [-1, 1]
         * 作用：将图像显示与图像宽高比分离
         */
        public Vector4 ClipSpacePosition { get; set; }
        // 视空间坐标
        public Vector4 ScreenSpacePosition { get; set; }
        /* 表面法向量
         * 表面法向量作用
         * 1. 计算光照
         * 2. 进行背面剔除
         * 3. 模拟粒子在表面‘弹跳’的效果
         * 4. 通过只考虑正面而加速碰撞检测
         */
        public Vector4 Normal { get; set; }

        public Vector4 nowNormal { get; set; }
        /*
         * 纹理映射坐标
         * 作用：插值算出三角形中的像素各点的UV坐标，通过UV坐标索引 纹理位图中的像素
         * 
        */
        public Vector4 UV { get; set; }

        public Vector4 UV2 { get; set; }

        public Vector4 UV3 { get; set; }

        public Vector4 UV4 { get; set; }

        public Vector4 UV5 { get; set; }

        public Vector4 UV6 { get; set; }

        public Vector4[] UVList;
        // 顶点颜色
        public Color4 Color { get; set; }
        //
        public Color4 lightColor;

        public Vertex() { }

 

        public Vertex(Vector4 pos, Vector4 normal, Vector4 uv, Color4 col)
        {
            this.Position = pos;
            this.nowPos = pos;
            this.nowPos = pos;
            this.Normal = normal;
            this.nowNormal = normal.Normalized;
            //this.nowNormal.Normalize();
            this.UV = uv;
            this.Color = col;
           // this.lightColor = col;
        }

        public object Clone()
        {
            Vertex e = new Vertex(this.Position,this.Normal,this.UV,this.Color);
            return e;
        }
    }
}
