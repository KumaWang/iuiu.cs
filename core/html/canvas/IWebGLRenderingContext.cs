using System;
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

    interface IWebGLRenderingContext
    {
        HTMLCanvasElement canvas();
        GLsizei drawingBufferWidth();
        GLsizei drawingBufferHeight();

        [WebGLHandlesContextLoss]
        WebGLContextAttributes getContextAttributes();

        [WebGLHandlesContextLoss]
        GLboolean isContextLost();

        DOMString[] getSupportedExtensions();
        Object getExtension(DOMString name);

        void activeTexture(GLenum texture);
        void attachShader(WebGLProgram program, WebGLShader shader);
        void bindAttribLocation(WebGLProgram program, GLuint index, DOMString name);
        void bindBuffer(GLenum target, WebGLBuffer buffer);
        void bindFramebuffer(GLenum target, WebGLFramebuffer framebuffer);
        void bindRenderbuffer(GLenum target, WebGLRenderbuffer renderbuffer);
        void bindTexture(GLenum target, WebGLTexture texture);
        void blendColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha);
        void blendEquation(GLenum mode);
        void blendEquationSeparate(GLenum modeRGB, GLenum modeAlpha);
        void blendFunc(GLenum sfactor, GLenum dfactor);
        void blendFuncSeparate(GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

        void bufferData(GLenum target, GLsizeiptr size, GLenum usage);
        void bufferData(GLenum target, ArrayBufferView data, GLenum usage);
        void bufferData(GLenum target, ArrayBuffer data, GLenum usage);
        void bufferSubData(GLenum target, GLintptr offset, ArrayBufferView data);
        void bufferSubData(GLenum target, GLintptr offset, ArrayBuffer data);

        [WebGLHandlesContextLoss]
        GLenum checkFramebufferStatus(GLenum target);

        void clear(GLbitfield mask);
        void clearColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha);
        void clearDepth(GLclampf depth);
        void clearStencil(GLint s);
        void colorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);
        void compileShader(WebGLShader shader);

        void compressedTexImage2D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, ArrayBufferView data);
        void compressedTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, ArrayBufferView data);

        void copyTexImage2D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);
        void copyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);

        WebGLBuffer createBuffer();
        WebGLFramebuffer createFramebuffer();
        WebGLProgram createProgram();
        WebGLRenderbuffer createRenderbuffer();
        WebGLShader createShader(GLenum type);
        WebGLTexture createTexture();

        void cullFace(GLenum mode);

        void deleteBuffer(WebGLBuffer buffer);
        void deleteFramebuffer(WebGLFramebuffer framebuffer);
        void deleteProgram(WebGLProgram program);
        void deleteRenderbuffer(WebGLRenderbuffer renderbuffer);
        void deleteShader(WebGLShader shader);
        void deleteTexture(WebGLTexture texture);

        void depthFunc(GLenum func);
        void depthMask(GLboolean flag);
        void depthRange(GLclampf zNear, GLclampf zFar);
        void detachShader(WebGLProgram program, WebGLShader shader);
        void disable(GLenum cap);
        void disableVertexAttribArray(GLuint index);
        void drawArrays(GLenum mode, GLint first, GLsizei count);
        void drawElements(GLenum mode, GLsizei count, GLenum type, GLintptr offset);

        void enable(GLenum cap);
        void enableVertexAttribArray(GLuint index);
        void finish();
        void flush();
        void framebufferRenderbuffer(GLenum target, GLenum attachment, GLenum renderbuffertarget, WebGLRenderbuffer renderbuffer);
        void framebufferTexture(GLenum target, GLenum attachment, GLenum textarget, WebGLTexture texture, GLint level);
        void frontFace(GLenum mode);

        void generateMipmap(GLenum target);

        WebGLActiveInfo getActiveAttrib(WebGLProgram program, GLuint index);
        WebGLActiveInfo getActiveUniform(WebGLProgram program, GLuint index);
        WebGLShader[] getAttachedShaders(WebGLProgram program);

        [WebGLHandlesContextLoss]
        GLint getAttribLocation(WebGLProgram program, DOMString name);

        dynamic getBufferParameter(GLenum target, GLenum pname);
        dynamic getParameter(GLenum pname);

        [WebGLHandlesContextLoss]
        GLenum getError();

        dynamic getFramebufferAttachmentParameter(GLenum target, GLenum attachment, GLenum pname);
        dynamic getProgramParameter(WebGLProgram program, GLenum pname);
        DOMString getProgramInfoLog(WebGLProgram program);
        dynamic getRenderbufferParameter(GLenum target, GLenum pname);
        dynamic getShaderParameter(WebGLShader shader, GLenum pname);
        WebGLShaderPrecisionFormat getShaderPrecisionFormat(GLenum shadertype, GLenum precisiontype);
        DOMString getShaderInfoLog(WebGLShader shader);

        DOMString getShaderSource(WebGLShader shader);

        dynamic getTexParameter(GLenum target, GLenum pname);

        dynamic getUniform(WebGLProgram program, WebGLUniformLocation location);

        WebGLUniformLocation getUniformLocation(WebGLProgram program, DOMString name);

        dynamic getVertexAttrib(GLuint index, GLenum pname);

        [WebGLHandlesContextLoss]
        GLsizeiptr getVertexAttribOffset(GLuint index, GLenum pname);

        void hint(GLenum target, GLenum mode);

        [WebGLHandlesContextLoss]
        GLboolean isBuffer(WebGLBuffer buffer);

        [WebGLHandlesContextLoss]
        GLboolean isEnabled(GLenum cap);

        [WebGLHandlesContextLoss]
        GLboolean isFramebuffer(WebGLFramebuffer framebuffer);

        [WebGLHandlesContextLoss]
        GLboolean isProgram(WebGLProgram program);

        [WebGLHandlesContextLoss]
        GLboolean isRenderbuffer(WebGLRenderbuffer renderbuffer);

        [WebGLHandlesContextLoss]
        GLboolean isShader(WebGLShader shader);

        [WebGLHandlesContextLoss]
        GLboolean isTexture(WebGLTexture texture);

        void lineWidth(GLfloat width);
        void linkProgram(WebGLProgram program);
        void pixelStorei(GLenum pname, GLint param);
        void polygonOffset(GLfloat factor, GLfloat units);

        void readPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, ArrayBufferView pixels);

        void renderbufferStorage(GLenum target, GLenum internalformat, GLsizei width, GLsizei height);
        void sampleCoverage(GLclampf value, GLboolean invert);
        void scissor(GLint x, GLint y, GLsizei width, GLsizei height);

        void shaderSource(WebGLShader shader, DOMString source);

        void stencilFunc(GLenum func, GLint @ref, GLuint mask);
        void stencilFuncSeparate(GLenum face, GLenum func, GLint @ref, GLuint mask);
        void stencilMask(GLuint mask);
        void stencilMaskSeparate(GLenum face, GLuint mask);
        void stencilOp(GLenum fail, GLenum zfail, GLenum zpass);
        void stencilOpSeparate(GLenum face, GLenum fail, GLenum zfail, GLenum zpass);

        void texImage2D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, ArrayBufferView pixels);
        void texImage2D(GLenum target, GLint level, GLenum internalformat, GLenum format, GLenum type, ImageData pixels);
        void texImage2D(GLenum target, GLint level, GLenum internalformat, GLenum format, GLenum type, HTMLImageElement image); // May throw DOMException
        void texImage2D(GLenum target, GLint level, GLenum internalformat, GLenum format, GLenum type, HTMLCanvasElement canvas); // May throw DOMException
        void texImage2D(GLenum target, GLint level, GLenum internalformat, GLenum format, GLenum type, HTMLVideoElement video); // May throw DOMException

        void texParameterf(GLenum target, GLenum pname, GLfloat param);
        void texParameteri(GLenum target, GLenum pname, GLint param);

        void texSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, ArrayBufferView pixels);
        void texSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLenum format, GLenum type, ImageData pixels);
        void texSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLenum format, GLenum type, HTMLImageElement image); // May throw DOMException
        void texSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLenum format, GLenum type, HTMLCanvasElement canvas); // May throw DOMException
        void texSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLenum format, GLenum type, HTMLVideoElement video); // May throw DOMException

        void uniform1f(WebGLUniformLocation location, GLfloat x);
        void uniform1fv(WebGLUniformLocation location, Float32Array v);
        void uniform1fv(WebGLUniformLocation location, params GLfloat[] v);
        void uniform1i(WebGLUniformLocation location, GLint x);
        void uniform1iv(WebGLUniformLocation location, Int32Array v);
        void uniform1iv(WebGLUniformLocation location, params GLint[] v);
        void uniform2f(WebGLUniformLocation location, GLfloat x, GLfloat y);
        void uniform2fv(WebGLUniformLocation location, Float32Array v);
        void uniform2fv(WebGLUniformLocation location, params GLfloat[] v);
        void uniform2i(WebGLUniformLocation location, GLint x, GLint y);
        void uniform2iv(WebGLUniformLocation location, Int32Array v);
        void uniform2iv(WebGLUniformLocation location, params GLint[] v);
        void uniform3f(WebGLUniformLocation location, GLfloat x, GLfloat y, GLfloat z);
        void uniform3fv(WebGLUniformLocation location, Float32Array v);
        void uniform3fv(WebGLUniformLocation location, params GLfloat[] v);
        void uniform3i(WebGLUniformLocation location, GLint x, GLint y, GLint z);
        void uniform3iv(WebGLUniformLocation location, Int32Array v);
        void uniform3iv(WebGLUniformLocation location, params GLint[] v);
        void uniform4f(WebGLUniformLocation location, GLfloat x, GLfloat y, GLfloat z, GLfloat w);
        void uniform4fv(WebGLUniformLocation location, Float32Array v);
        void uniform4fv(WebGLUniformLocation location, params GLfloat[] v);
        void uniform4i(WebGLUniformLocation location, GLint x, GLint y, GLint z, GLint w);
        void uniform4iv(WebGLUniformLocation location, Int32Array v);
        void uniform4iv(WebGLUniformLocation location, params GLint[] v);

        void uniformMatrix2fv(WebGLUniformLocation location, GLboolean transpose, Float32Array value);
        void uniformMatrix2fv(WebGLUniformLocation location, GLboolean transpose, params GLfloat[] value);
        void uniformMatrix3fv(WebGLUniformLocation location, GLboolean transpose, Float32Array value);
        void uniformMatrix3fv(WebGLUniformLocation location, GLboolean transpose, params GLfloat[] value);
        void uniformMatrix4fv(WebGLUniformLocation location, GLboolean transpose, Float32Array value);
        void uniformMatrix4fv(WebGLUniformLocation location, GLboolean transpose, params GLfloat[] value);

        void useProgram(WebGLProgram program);
        void validateProgram(WebGLProgram program);

        void vertexAttrib1f(GLuint indx, GLfloat x);
        void vertexAttrib1fv(GLuint indx, Float32Array values);
        void vertexAttrib1fv(GLuint indx, params GLfloat[] values);
        void vertexAttrib2f(GLuint indx, GLfloat x, GLfloat y);
        void vertexAttrib2fv(GLuint indx, Float32Array values);
        void vertexAttrib2fv(GLuint indx, params GLfloat[] values);
        void vertexAttrib3f(GLuint indx, GLfloat x, GLfloat y, GLfloat z);
        void vertexAttrib3fv(GLuint indx, Float32Array values);
        void vertexAttrib3fv(GLuint indx, params GLfloat[] values);
        void vertexAttrib4f(GLuint indx, GLfloat x, GLfloat y, GLfloat z, GLfloat w);
        void vertexAttrib4fv(GLuint indx, Float32Array values);
        void vertexAttrib4fv(GLuint indx, params GLfloat[] values);
        void vertexAttribPointer(GLuint indx, GLint size, GLenum type, GLboolean normalized, GLsizei stride, GLintptr offset);

        void viewport(GLint x, GLint y, GLsizei width, GLsizei height);
    }

    class WebGLHandlesContextLossAttribute : Attribute
    {
    }

    // ReSharper restore InconsistentNaming
}
