namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLContextObject : WebGLObject
    {
        private WebGLRenderingContext m_context;

        protected WebGLContextObject(WebGLRenderingContext context) : base(context)
        {
            this.m_context = context;
        }

        ~WebGLContextObject()
        {
            if (this.m_context != null)
            {
                this.m_context.removeContextObject(this);
            }
        }

        internal WebGLRenderingContext context()
        {
            return this.m_context;
        }

        internal override bool validate(WebGLContextGroup group, WebGLRenderingContext context)
        {
            return context == this.m_context;
        }

        internal void detachContext()
        {
            this.detach();
            if (this.m_context != null)
            {
                this.deleteObject(this.m_context.graphicsContext3D());
                this.m_context.removeContextObject(this);
                this.m_context = null;
            }
        }

        internal override bool hasGroupOrContext()
        {
            return this.m_context != null;
        }

        internal override GraphicsContext3D getAGraphicsContext3D()
        {
            return this.m_context != null ? this.m_context.graphicsContext3D() : null;
        }
    }

    // ReSharper restore InconsistentNaming
}
