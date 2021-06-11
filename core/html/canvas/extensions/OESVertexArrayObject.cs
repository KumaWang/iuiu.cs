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

    class OESVertexArrayObject : WebGLExtension
    {
        internal OESVertexArrayObject(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.OESVertexArrayObjectName;
        }

        internal WebGLVertexArrayObjectOES createVertexArrayOES()
        {
            if (this.m_context.isContextLost())
            {
                return null;
            }

            var o = new WebGLVertexArrayObjectOES(this.m_context, WebGLVertexArrayObjectOES.VaoType.VaoTypeUser);
            this.m_context.addContextObject(o);
            return o;
        }

        internal void deleteVertexArrayOES(WebGLVertexArrayObjectOES arrayObject)
        {
            if (arrayObject == null || this.m_context.isContextLost())
            {
                return;
            }

            if (!arrayObject.isDefaultObject() && arrayObject == this.m_context.m_boundVertexArrayObject)
            {
                this.m_context.setBoundVertexArrayObject(null);
            }

            arrayObject.deleteObject(this.m_context.graphicsContext3D());
        }

        internal GLboolean isVertexArrayOES(WebGLVertexArrayObjectOES arrayObject)
        {
            if (arrayObject == null || this.m_context.isContextLost())
            {
                return false;
            }

            if (!arrayObject.hasEverBeenBound())
            {
                return false;
            }

            var extensions = this.m_context.graphicsContext3D().getExtensions();
            return extensions.isVertexArrayOES(arrayObject.obj());
        }

        internal void bindVertexArrayOES(WebGLVertexArrayObjectOES arrayObject)
        {
            if (this.m_context.isContextLost())
            {
                return;
            }

            if (arrayObject != null && (arrayObject.isDeleted() || !arrayObject.validate(null, this.context())))
            {
                this.m_context.graphicsContext3D().synthesizeGLError(GraphicsContext3D.INVALID_OPERATION);
                return;
            }

            var extensions = this.m_context.graphicsContext3D().getExtensions();
            if (arrayObject != null && !arrayObject.isDefaultObject() && arrayObject.obj() != 0)
            {
                extensions.bindVertexArrayOES(arrayObject.obj());

                arrayObject.setHasEverBeenBound();
                this.m_context.setBoundVertexArrayObject(arrayObject);
            }
            else
            {
                extensions.bindVertexArrayOES(0);
                this.m_context.setBoundVertexArrayObject(null);
            }
        }
    }

    // ReSharper restore InconsistentNaming
}
