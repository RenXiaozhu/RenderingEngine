using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class DirectionLight
    {
        /* 环境光
		 * 弥漫于整个空间的光线称为环境光，均匀分布， 强度为 Ia,
		 * 
		 * 环境光反射系数 Ka
		 * 环境光的照射下，物体的反射系数称为  环境光反射系数 Ka
		 * 
		 * 物体表面亮度 Ie = Ka * Ia（光照明方程）
		 * 
		 * 
        */
        // 环境光强度
        public static float Ia = 0.3f;

        // 环境光反射系数
        public static float Ka = 0.2f;

		public static Color4 AmbientColor = new Color4(128, 128, 128);

        /* 漫反射 Diffuse Reflection
         * 
         * 漫反射系数 Kd->[0,1]
         * 
         * 点光源强度 Ip
         * 
         * 给定表面的 点P 法线方向N P点到光源的矢量为L，Q为N与L的夹角
         * 
         * P点的漫反射光亮度为 Id ，由郎伯余弦定律得到 
         * 
         * Id = Ip *  Kd * cosQ  Q->[0, PI/2]
         * 
         * Id强度只于入射角度有关
         * 
         * 当 Q > PI/2 时， 光线被物体本身遮挡
         * 
         * 若 N 与 L 都已规范化为单位矢量， cosQ = L dot N
         * 
         * Id = Ip * Kd * (L dot N)
         * 
         *
         * 
        */
        // 光源强度
        public static float Ip = 0.5f;

        public static float KD = 0.4f;

		public const double StartKD = 1.0f;

		public const double MaxKD = 1.0f;

		//光源位置
        public static Vector4 LightPos { get; set; }

		//光源颜色
        public static Color4 LightColor { get; set; }

        /* 环境光 + 漫反射
         * 
         * I = Ie + Id = Ia * Ka + Ip * Kd *(L dot N)
         * 
        */

		// 
        private static double kd;

        public static double Kd  ;

        public static bool IsEnable { get; set; }
        public static bool IsAmLightEnable { get; set; }

		//初始化光源
        public DirectionLight(Vector4 pos, Color4 color)
        {
			LightPos = pos.Normalized;
		    LightColor = color;
            kd = MaxKD * StartKD;
        }

		// 光线的方向点积法线方向
        public static float ComputeNDotL(Vector4 pos, Vector4 normal)
		{
			/* 点光源
			 * var lightDirection = LightPos - pos;
			 * lightDirection.Normalize();
			 */
			float t = Vector4.Dot(normal,LightPos);
            return MathUtil.Clamp01(t);
		}

		// 漫反射光照颜色
        public static Color4 GetDiffuseColor(float nDotl)
		{
			return LightColor * nDotl;
		}

		// 加上环境光
        public static Color4 GetFinalLightColor(float NDotl)
		{
			Color4 diffuse = GetDiffuseColor(NDotl);
            Color4 final = diffuse;
           
            if(IsAmLightEnable)
            {
				final = final+ DirectionLight.AmbientColor;
            }
			return final;
		}

        public static  Color4 GetFinalLightColor( Vector4 normal, Color4 oriColor)
        {
            float dt;
            Color4 light;
            if(IsEnable)
            {
                dt = ComputeNDotL(normal, normal);
                light = GetFinalLightColor(dt);
                Color4 final = oriColor * light;
                return final;
            }
            else
            {
                return oriColor;
            }
        }
    }
}
