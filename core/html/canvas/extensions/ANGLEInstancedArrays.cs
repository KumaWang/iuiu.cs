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

    class ANGLEInstancedArrays : WebGLExtension
    {
        internal ANGLEInstancedArrays(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.ANGLEInstancedArraysName;
        }

        internal static bool supported(WebGLRenderingContext context)
        {
            return false;
        }

        internal void drawArraysInstancedANGLE(GLenum mode, GLint first, GLsizei count, GLsizei primcount)
        {
            if (this.m_context.isContextLost())
            {
                return;
            }
            this.m_context.drawArraysInstanced(mode, first, count, primcount);
        }

        internal void drawElementsInstancedANGLE(GLenum mode, GLsizei count, GLenum type, long offset, GLsizei primcount)
        {
            if (this.m_context.isContextLost())
            {
                return;
            }
            this.m_context.drawElementsInstanced(mode, count, type, offset, primcount);
        }

        internal void vertexAttribDivisorANGLE(GLuint index, GLuint divisor)
        {
            if (this.m_context.isContextLost())
            {
                return;
            }
            this.m_context.vertexAttribDivisor(index, divisor);
        }
    }

    // ReSharper restore InconsistentNaming
}
