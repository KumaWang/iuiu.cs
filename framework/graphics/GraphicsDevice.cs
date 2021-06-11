using System;
using System.Drawing;
using WebGL;

namespace engine.framework.graphics
{
    public class GraphicsDevice
    {
        private Func<Size> _getSize;
        private WebGLRenderingContext _gl;

        internal WebGLRenderingContext Context => _gl;

        public AABB Viewport { get; set; }

        public Size Size => _getSize();

        public int Width => Size.Width;

        public int Height => Size.Height;

        public GraphicsDevice(IntPtr intptr, Func<Size> getSizeFunc) 
        {
            _getSize = getSizeFunc;
            _gl = (WebGLRenderingContext)new HTMLCanvasElement(intptr, getSizeFunc).getContext("webgl", new WebGLContextAttributes(new Attributes()));
        }

        public void SwapBuffers() 
        {
            Context.swapBuffers();
        }

        public void Clear(Color c)
        {
            _gl.clearColor(c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
            _gl.clear(_gl.COLOR_BUFFER_BIT | _gl.DEPTH_BUFFER_BIT);
        }
    }
}
