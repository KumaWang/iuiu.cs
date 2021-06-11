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

    class WebGLUniformLocation
    {
        private readonly WebGLProgram m_program;
        private readonly GLint m_location;
        private readonly GLenum m_type;
        private readonly uint m_linkCount;

        internal WebGLUniformLocation(WebGLProgram program, GLint location, GLenum type)
        {
            this.m_program = program;
            this.m_location = location;
            this.m_type = type;
            this.m_linkCount = this.m_program.getLinkCount();
        }

        internal WebGLProgram program()
        {
            return this.m_program.getLinkCount() != this.m_linkCount ? null : this.m_program;
        }

        internal int location()
        {
            return this.m_location;
        }

        internal uint type()
        {
            return this.m_type;
        }
    }

    // ReSharper restore InconsistentNaming
}
