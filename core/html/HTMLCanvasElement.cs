using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class HTMLCanvasElement : JSEventDispatcher, IDisposable
    {
        private IntPtr m_intptr;
        private Func<Size> m_getSizeFunc;
        private CanvasRenderingContext m_context;

        public HTMLCanvasElement(IntPtr intptr, Func<Size> getSizeFunc)
        {
            m_intptr = intptr;
            m_getSizeFunc = getSizeFunc;
        }

        ~HTMLCanvasElement()
        {
            this.dispose(false);
        }

        public int width
        {
            get { return this.m_getSizeFunc().Width; }
        }

        public int height
        {
            get { return this.m_getSizeFunc().Height; }
        }

        public IntPtr handle()
        {
            return m_intptr;
        }

        public CanvasRenderingContext getContext(string type, CanvasContextAttributes attrs = null)
        {
            if (is2dType(type))
            {
                throw new NotImplementedException();
            }

            if (is3dType(type))
            {
                if (this.m_context != null && !this.m_context.is3d)
                {
                    return null;
                }
                if (this.m_context == null)
                {
                    this.m_context = WebGLRenderingContext.create(this, (WebGLContextAttributes)attrs);
                }
                return this.m_context;
            }

            return null;
        }

        public void Dispose()
        {
            this.dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_context.Dispose();
            }
        }

        private static bool is2dType(string type)
        {
            return type.Equals("2d");
        }

        private static bool is3dType(string type)
        {
            return type == "webgl" || type == "experimental-webgl" || type == "webkit-3d";
        }
    }

    // ReSharper restore InconsistentNaming
}
