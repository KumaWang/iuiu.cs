using GLenum = System.UInt32;
using GLboolean = System.Boolean;
using GLbitfield = System.UInt32;
using GLbyte = System.SByte;
using GLshort = System.Int16;
using GLint = System.Int32;
using GLsizei = System.Int32;
using GLintptr = System.Int64;
using GLsizeiptr = System.Int64;
using GLubyte = System.Byte;
using GLushort = System.UInt16;
using GLuint = System.UInt32;
using GLfloat = System.Single;
using GLclampf = System.Single;
using DOMString = System.String;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLRenderbuffer : WebGLSharedObject
    {
        private GLenum m_internalFormat;
        private bool m_initialized;
        private GLsizei m_width, m_height;
        private bool m_isValid;
        private bool m_hasEverBeenBound;

        internal WebGLRenderbuffer(WebGLRenderingContext ctx) : base(ctx)
        {
            this.m_internalFormat = GraphicsContext3D.RGBA4;
            this.m_initialized = false;
            this.m_width = 0;
            this.m_height = 0;
            this.m_isValid = true;
            this.m_hasEverBeenBound = false;

            this.setObject(ctx.graphicsContext3D().createRenderbuffer());
        }

        ~WebGLRenderbuffer()
        {
            this.deleteObject(null);
        }

        internal void setInternalFormat(GLenum internalformat)
        {
            this.m_internalFormat = internalformat;
            this.m_initialized = false;
        }

        internal GLenum getInternalFormat()
        {
            return this.m_internalFormat;
        }

        internal void setSize(GLsizei width, GLsizei height)
        {
            this.m_width = width;
            this.m_height = height;
        }

        internal GLsizei getWidth()
        {
            return this.m_width;
        }

        internal GLsizei getHeight()
        {
            return this.m_height;
        }

        internal void setIsValid(bool isValid)
        {
            this.m_isValid = isValid;
        }

        internal bool isValid()
        {
            return this.m_isValid;
        }

        internal bool isInitialized()
        {
            return this.m_initialized;
        }

        internal void setInitialized()
        {
            this.m_initialized = true;
        }

        internal bool hasEverBeenBound()
        {
            return this.obj() != 0 && this.m_hasEverBeenBound;
        }

        internal void setHasEverBeenBound()
        {
            this.m_hasEverBeenBound = true;
        }

        internal override void deleteObjectImpl(GraphicsContext3D context3d, Platform3DObject @object)
        {
            context3d.deleteRenderbuffer(@object);
        }

        internal override bool isRenderbuffer()
        {
            return true;
        }
    }

    // ReSharper restore InconsistentNaming
}
