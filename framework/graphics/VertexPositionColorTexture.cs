using System.Numerics;

namespace engine.framework.graphics
{
    public struct VertexPositionColorTexture
    {
        public Vector3 Vertex { get; }

        public Color Color { get; }

        public Vector2 UV { get; }

        public VertexPositionColorTexture(Vector3 vertex, Color color, Vector2 uv) 
        {
            Vertex = vertex;
            Color = color;
            UV = uv;
        }
    }
}
