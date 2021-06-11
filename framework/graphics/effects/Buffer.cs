using WebGL;

namespace engine.framework.graphics
{
    public class Buffer
    {
        internal WebGLBuffer WebGLBuffer { get; }

        public string Name { get; }

        public int ItemSize { get; }

        public GraphicsDevice GraphicsDevice { get; }

        internal uint Location { get; }

        public Buffer(Effect effect, string name, int itemSize, GraphicsDevice graphicsDevice) 
        {
            Name = name;
            ItemSize = itemSize;
            GraphicsDevice = graphicsDevice;

            effect.Apply();

            WebGLBuffer = GraphicsDevice.Context.createBuffer();
            Location = (uint)GraphicsDevice.Context.getAttribLocation(graphicsDevice.Context.m_currentProgram, name);
            GraphicsDevice.Context.enableVertexAttribArray(Location);
        }

        internal void BindData(ArrayBufferView data) 
        {
            GraphicsDevice.Context.bindBuffer(GraphicsDevice.Context.ARRAY_BUFFER, WebGLBuffer);
            GraphicsDevice.Context.bufferData(GraphicsDevice.Context.ARRAY_BUFFER, data, GraphicsDevice.Context.STATIC_DRAW);
        }
    }

    public class IndexBuffer
    {
        internal WebGLBuffer WebGLBuffer { get; }

        public GraphicsDevice GraphicsDevice { get; }

        public IndexBuffer(Effect effect, GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;

            effect.Apply();

            WebGLBuffer = GraphicsDevice.Context.createBuffer();
        }

        internal void BindData(ArrayBufferView data)
        {
            GraphicsDevice.Context.bindBuffer(GraphicsDevice.Context.ELEMENT_ARRAY_BUFFER, WebGLBuffer);
            GraphicsDevice.Context.bufferData(GraphicsDevice.Context.ELEMENT_ARRAY_BUFFER, data, GraphicsDevice.Context.STATIC_DRAW);
        }
    }
}
