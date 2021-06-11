namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLSharedObject : WebGLObject
    {
        private WebGLContextGroup m_contextGroup;

        protected WebGLSharedObject(WebGLRenderingContext context) : base(context)
        {
            this.m_contextGroup = context.contextGroup();
        }

        ~WebGLSharedObject()
        {
            if (this.m_contextGroup != null)
            {
                this.m_contextGroup.removeObject(this);
            }
        }

        internal WebGLContextGroup contextGroup()
        {
            return this.m_contextGroup;
        }

        internal virtual bool isBuffer()
        {
            return false;
        }

        internal virtual bool isFramebuffer()
        {
            return false;
        }

        internal virtual bool isProgram()
        {
            return false;
        }

        internal virtual bool isRenderbuffer()
        {
            return false;
        }

        internal virtual bool isShader()
        {
            return false;
        }

        internal virtual bool isTexture()
        {
            return false;
        }

        internal override bool validate(WebGLContextGroup contextGroup, WebGLRenderingContext context)
        {
            return contextGroup == this.m_contextGroup;
        }

        internal void detachContextGroup()
        {
            this.detach();
            if (this.m_contextGroup != null)
            {
                this.deleteObject(null);
                this.m_contextGroup.removeObject(this);
                this.m_contextGroup = null;
            }
        }

        internal override bool hasGroupOrContext()
        {
            return this.m_contextGroup != null;
        }

        internal override GraphicsContext3D getAGraphicsContext3D()
        {
            return this.m_contextGroup != null ? this.m_contextGroup.getAGraphicsContext3D() : null;
        }
    }

    // ReSharper restore InconsistentNaming
}
