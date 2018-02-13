using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RenderingEngine
{
    class CoordinateTransform
    {



        // 从窗口坐标系转换到视区坐标系
        /// <summary>
        /// 
        /// </summary>
        ///  窗口
        /// <param name="window"></param>
        /// 
        ///视区
        /// <param name="viewport"></param>
        /// 
        /// 窗口和视区坐标系之间的夹角
        /// <param name="degree"></param>
        /// 
        /// 
        /// <returns></returns>
        public VETransform2D GetViewPortFromWindow(Rectangle window, Rectangle viewport, double degree)
        {
            VETransform2D trans = new VETransform2D();

            trans.d11 = (viewport.Width / window.Width) * Math.Cos(degree/180);
            trans.d12 = (viewport.Width / window.Width) * Math.Sin(degree / 180);

            trans.d21 = (viewport.Height / window.Height) * Math.Sin(degree / 180);
            trans.d22 = (viewport.Height / window.Height) * Math.Cos(degree/180);
            
            trans.d13 = - (viewport.Width / window.Width)*(window.X*Math.Cos(degree/180) + window.Height*Math.Sin(degree/180)) + viewport.X;
            trans.d23 = -(viewport.Height / window.Height)* (window.X * Math.Sin(degree / 180) - window.Height * Math.Cos(degree / 180)) + viewport.Y;

            trans.d33 = 1;
            return trans;
        }

        // Oxyz -> O`uvn 空间坐标系变换
        /// <summary>
        /// 
        /// </summary>
        /// u 坐标轴
        /// <param name="u"></param>
        /// v 坐标轴
        /// <param name="v"></param>
        /// n 坐标轴
        /// <param name="n"></param>
        /// 新坐标原点
        /// <param name="O"></param>
        /// <returns></returns>
        public VETransform3D CoordinateToOther(Vector4 u, Vector4 v, Vector4 n, Vector4 O)
        {
            VETransform3D trans = new VETransform3D();
            trans.d11 = u.x;
            trans.d12 = u.y;
            trans.d13 = u.z;

            trans.d21 = v.x;
            trans.d22 = v.y;
            trans.d23 = v.z;

            trans.d31 = n.x;
            trans.d32 = n.y;
            trans.d33 = n.z;

            VETransform3D R = new VETransform3D();
            R.VETransform3DTranslation(-O.x, -O.y, -O.z);
            return R * trans;
        }

    }
}
