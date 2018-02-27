using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    public class DirectionLight
    {
		//环境光
		public static Color4 AmbientColor = new Color4(128, 128, 128);

		public const double StartKD = 0.5f;

		public const double MaxKD = 3.0f;

		//光源位置
		public Vector4 LightPos { get; set; }

		//光源颜色
		public Color4 LoghtColor { get; set; }

		// 
		private double kd;
		public double Kd
		{
			get
			{
				return this.kd;
			}

			set
			{
				this.kd = value * MaxKD;
			}
		}

		public bool IsEnable { get; set; }

		//初始化光源
        public DirectionLight(Vector4 pos, Color4 color)
        {
			this.LightPos = pos;
			this.LoghtColor = color;
			this.kd = MaxKD * StartKD;
        }

		// 光线的方向点积法线方向
        public float ComputeNDotL(Vector4 pos, Vector4 normal)
		{
			var lightDirection = this.LightPos - pos;
			normal.Normalize();
			lightDirection.Normalize();
            return Math.Max(0,Vector4.Dot(normal,lightDirection));
		}

		// 漫反射光照颜色
		public Color4 GetDiffuseColor(float nDotl)
		{
			return this.LoghtColor * (nDotl * kd);
		}

		// 加上环境光
		public Color4 GetFinalLightColor(float NDotl)
		{
			Color4 diffuse = GetDiffuseColor(NDotl);
			Color4 final = diffuse + DirectionLight.AmbientColor;
			return final;
		}

    }
}
