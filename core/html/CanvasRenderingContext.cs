using System;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    abstract class CanvasRenderingContext : IDisposable
    {
        private readonly HTMLCanvasElement m_canvas;

        protected CanvasRenderingContext(HTMLCanvasElement canvas)
        {
            this.m_canvas = canvas;
        }

        public HTMLCanvasElement canvas
        {
            get { return this.m_canvas; }
        }

        public virtual bool is2d
        {
            get { return false; }
        }

        public virtual bool is3d
        {
            get { return false; }
        }

        public virtual bool isAccelerated
        {
            get { return false; }
        }

        public abstract void Dispose();
    }

    // ReSharper restore InconsistentNaming
}
