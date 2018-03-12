using System;
namespace RenderingEngine
{
    public struct Angle
    {
        public float X;
        public float Y;
        public float Z;

        public Angle(float x, float y, float z)
            :this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
