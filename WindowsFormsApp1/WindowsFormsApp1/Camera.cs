using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderingEngine
{
    class Camera
    {
        Vector4 self_position { get; set; }
        Vector4 target_postion { get; set; }
        Vector4 up; //y轴
        Vector4 forward //z轴 深度方向
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


    }
}
