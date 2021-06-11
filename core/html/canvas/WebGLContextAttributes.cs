namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLContextAttributes : CanvasContextAttributes
    {
        private readonly Attributes m_attrs = new Attributes();

        public WebGLContextAttributes()
        {
        }

        public WebGLContextAttributes(Attributes attributes)
        {
            this.m_attrs.alpha = attributes.alpha;
            this.m_attrs.depth = attributes.depth;
            this.m_attrs.stencil = attributes.stencil;
            this.m_attrs.antialias = attributes.antialias;
            this.m_attrs.premultipliedAlpha = attributes.premultipliedAlpha;
            this.m_attrs.preserveDrawingBuffer = attributes.preserveDrawingBuffer;
            this.m_attrs.noExtensions = attributes.noExtensions;
            this.m_attrs.shareResources = attributes.shareResources;
            this.m_attrs.preferDiscreteGPU = attributes.preferDiscreteGPU;
            this.m_attrs.multithreaded = attributes.multithreaded;
            this.m_attrs.forceSoftwareRenderer = attributes.forceSoftwareRenderer;
        }

        public bool alpha()
        {
            return this.m_attrs.alpha;
        }

        public void setAlpha(bool alpha)
        {
            this.m_attrs.alpha = alpha;
        }

        public bool depth()
        {
            return this.m_attrs.depth;
        }

        public void setDepth(bool depth)
        {
            this.m_attrs.depth = depth;
        }

        public bool stencil()
        {
            return this.m_attrs.stencil;
        }

        public void setStencil(bool stencil)
        {
            this.m_attrs.stencil = stencil;
        }

        public bool antialias()
        {
            return this.m_attrs.antialias;
        }

        public void setAntialias(bool antialias)
        {
            this.m_attrs.antialias = antialias;
        }

        public bool premultipliedAlpha()
        {
            return this.m_attrs.premultipliedAlpha;
        }

        public void setPremultipliedAlpha(bool premultipliedAlpha)
        {
            this.m_attrs.premultipliedAlpha = premultipliedAlpha;
        }

        public bool preserveDrawingBuffer()
        {
            return this.m_attrs.preserveDrawingBuffer;
        }

        public void setPreserveDrawingBuffer(bool preserveDrawingBuffer)
        {
            this.m_attrs.preserveDrawingBuffer = preserveDrawingBuffer;
        }

        public Attributes attributes()
        {
            return this.m_attrs;
        }
    }

    // ReSharper restore InconsistentNaming
}
