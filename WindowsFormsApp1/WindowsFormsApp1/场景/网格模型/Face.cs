using System;
namespace RenderingEngine
{
    public class Face
    {
        public enum FaceType
        {   
            Front,
            Left,
            Right,
            Behind,
            Up,
            Below
        }
        public Vector4 UV1 = new Vector4(0,0,0,0);
        public Vector4 UV2 = new Vector4(0,1,0,0);
        public Vector4 UV3 = new Vector4(1,1,0,0);
        public Vector4 UV4 = new Vector4(1,0,0,0);

        public FaceType type;

        public VertexTriangle t1;

        public Triangle t_1;

        public VertexTriangle t2;

        public Triangle t_2;

        public Face(Triangle t_1, Triangle t_2,FaceType type)
        {
            //this.t1 = t1;
            //this.t2 = t2;
            this.t_1 = t_1;
            this.t_2 = t_2;
            this.type = type;
        }

        public void InitUV()
        {

            Vertex v1 = t1.Vertices[0];
            Vertex v2 = t1.Vertices[1];
            Vertex v3 = t1.Vertices[2];

            Vertex vt1 = t2.Vertices[0];
            Vertex vt2 = t2.Vertices[1];
            Vertex vt3 = t2.Vertices[2];

            Vector4 e1 = v2.Position - v1.Position;
            Vector4 e2 = v3.Position - v2.Position;
            Vector4 e3 = v1.Position - v3.Position;

            Vector4 e4 = vt2.Position - vt1.Position;
            Vector4 e5 = vt3.Position - vt2.Position;
            Vector4 e6 = vt1.Position - vt3.Position;

            float e1l = e1.Length();
            float e2l = e2.Length();
            float e3l = e3.Length();
            float e4l = e4.Length();
            float e5l = e5.Length();
            float e6l = e6.Length();

            float maxlength = Math.Max(Math.Max(e1l, e3l), Math.Max(e2l, e1l));
            if(maxlength == e1l)
            {
                
            }

        }


    }
}
