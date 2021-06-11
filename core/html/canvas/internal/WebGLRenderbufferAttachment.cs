namespace WebGL
{
    class WebGLRenderbufferAttachment : WebGLAttachment
    {
        private readonly WebGLRenderbuffer _renderbuffer;

        internal WebGLRenderbufferAttachment(WebGLRenderbuffer renderbuffer)
        {
            this._renderbuffer = renderbuffer;
        }

        internal override int getWidth()
        {
            return this._renderbuffer.getWidth();
        }

        internal override int getHeight()
        {
            return this._renderbuffer.getHeight();
        }

        internal override uint getFormat()
        {
            return this._renderbuffer.getInternalFormat();
        }

        internal override WebGLSharedObject getObject()
        {
            return this._renderbuffer.obj() != 0 ? this._renderbuffer : null;
        }

        internal override bool isSharedObject(WebGLSharedObject obj)
        {
            return obj == this._renderbuffer;
        }

        internal override bool isValid()
        {
            return this._renderbuffer.obj() != 0;
        }

        internal override bool isInitialized()
        {
            return this._renderbuffer.obj() != 0 && this._renderbuffer.isInitialized();
        }

        internal override void setInitialized()
        {
            if (this._renderbuffer.obj() != 0)
            {
                this._renderbuffer.setInitialized();
            }
        }

        internal override void onDetached(GraphicsContext3D context)
        {
            this._renderbuffer.onDetached(context);
        }

        internal override void attach(GraphicsContext3D context, uint attachment)
        {
            var obj = this._renderbuffer.obj();
            context.framebufferRenderbuffer(GLES.GL_FRAMEBUFFER, attachment, GLES.GL_RENDERBUFFER, obj);
        }

        internal override void unattach(GraphicsContext3D context, uint attachment)
        {
            if (attachment == GLES.GL_DEPTH_STENCIL_ATTACHMENT)
            {
                context.framebufferRenderbuffer(GLES.GL_FRAMEBUFFER, GLES.GL_DEPTH_ATTACHMENT, GLES.GL_RENDERBUFFER, 0);
                context.framebufferRenderbuffer(GLES.GL_FRAMEBUFFER, GLES.GL_STENCIL_ATTACHMENT, GLES.GL_RENDERBUFFER, 0);
            }
            else
            {
                context.framebufferRenderbuffer(GLES.GL_FRAMEBUFFER, attachment, GLES.GL_RENDERBUFFER, 0);
            }
        }
    }
}
