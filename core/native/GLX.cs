using System;
using GLenum = System.UInt32;
using GLboolean = System.Byte;
using GLbitfield = System.UInt32;
using GLbyte = System.SByte;
using GLshort = System.UInt16;
using GLint = System.Int32;
using GLubyte = System.Byte;
using GLushort = System.UInt16;
using GLuint = System.UInt32;
using GLsizei = System.Int32;
using GLfloat = System.Single;
using GLclampf = System.Single;
using GLdouble = System.Double;
using GLclampd = System.Double;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    static class GLX
    {
        public delegate void PFNGLBINDVERTEXARRAYOESPROC(GLuint array);

        public delegate void PFNGLDELETEVERTEXARRAYSOESPROC(GLsizei n, GLuint[] arrays);

        public delegate void PFNGLGENVERTEXARRAYSOESPROC(GLsizei n, GLuint[] arrays);

        public delegate GLboolean PFNGLISVERTEXARRAYOESPROC(GLuint array);

        public delegate void PFNGLRENDERBUFFERSTORAGEMULTISAMPLEIMG(GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height);

        public delegate void PFNGLFRAMEBUFFERTextureMULTISAMPLEIMG(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLsizei samples);

        public delegate GLenum PFNGLGETGRAPHICSRESETSTATUSEXTPROC();

        public delegate void PFNGLREADNPIXELSEXTPROC(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, IntPtr data);

        public delegate void PFNGLGETNUNIFORMFVEXTPROC(GLuint program, GLint location, GLsizei bufSize, IntPtr @params);

        public delegate void PFNGLGETNUNIFORMIVEXTPROC(GLuint program, GLint location, GLsizei bufSize, IntPtr @params);

        public delegate void PFNGLVERTEXATTRIBDIVISORANGLEPROC(GLuint index, GLuint divisor);

        public delegate void PFNGLDRAWARRAYSINSTANCEDANGLEPROC(GLenum mode, GLint first, GLsizei count, GLsizei primcount);

        public delegate void PFNGLDRAWELEMENTSINSTANCEDANGLEPROC(GLenum mode, GLsizei count, GLenum type, IntPtr indices, GLsizei primcount);
    }

    // ReSharper restore InconsistentNaming
}
