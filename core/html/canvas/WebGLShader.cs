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

    class WebGLShader : WebGLSharedObject
    {
        private readonly GLenum m_type;
        private DOMString m_source;
        private bool m_isValid;

        internal WebGLShader(WebGLRenderingContext ctx, GLenum type) : base(ctx)
        {
            this.m_type = type;
            this.m_source = "";
            this.m_isValid = false;
            this.setObject(ctx.graphicsContext3D().createShader(type));
        }

        ~WebGLShader()
        {
            this.deleteObject(null);
        }

        internal GLenum getType()
        {
            return this.m_type;
        }

        internal DOMString getSource()
        {
            return this.m_source;
        }

        internal void setSource(DOMString source)
        {
            this.m_source = source;
        }

        internal bool isValid()
        {
            return this.m_isValid;
        }

        internal void setValid(bool valid)
        {
            this.m_isValid = valid;
        }

        internal override void deleteObjectImpl(GraphicsContext3D context3d, Platform3DObject @object)
        {
            context3d.deleteShader(@object);
        }

        internal override bool isShader()
        {
            return true;
        }
    }

    // ReSharper restore InconsistentNaming
}
