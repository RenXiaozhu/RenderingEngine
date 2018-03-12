using System;

namespace RenderingEngine
{
    public abstract class MeshBase
    {
        public string Name { get; set; }
        public Vertex[] Vertices { get; set; }
        public Triangle[] triangles { get; set; }
        public Face[] faces { get; set; }
        public Texture texture { get; set; }
        public string TextureFileName { get; set; }

        public Matrix4x4 rotation { get; set; }
        public Matrix4x4 Move { get; set; }
        public Vector4 Position { get; set; }

    }
}
