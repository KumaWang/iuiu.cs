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

    abstract class WebGLAttachment
    {
        internal abstract GLsizei getWidth();

        internal abstract GLsizei getHeight();

        internal abstract GLenum getFormat();

        internal abstract WebGLSharedObject getObject();

        internal abstract bool isSharedObject(WebGLSharedObject @object);

        internal abstract bool isValid();

        internal abstract bool isInitialized();

        internal abstract void setInitialized();

        internal abstract void onDetached(GraphicsContext3D context);

        internal abstract void attach(GraphicsContext3D context, GLenum attachment);

        internal abstract void unattach(GraphicsContext3D context, GLenum attachment);
    }

    // ReSharper restore InconsistentNaming
}
