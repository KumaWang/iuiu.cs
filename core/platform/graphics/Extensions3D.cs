using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GLenum = System.UInt32;
using GLboolean = System.Boolean;
using GLint = System.Int32;
using GLsizei = System.Int32;
using GLuint = System.UInt32;
using DOMString = System.String;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Extensions3D
    {
        public const uint BGRA_EXT = 0x80E1;

        public const uint GUILTY_CONTEXT_RESET_ARB = 0x8253;
        public const uint INNOCENT_CONTEXT_RESET_ARB = 0x8254;
        public const uint UNKNOWN_CONTEXT_RESET_ARB = 0x8255;

        public const uint DEPTH24_STENCIL8 = 0x88F0;

        public const uint READ_FRAMEBUFFER = 0x8CA8;
        public const uint DRAW_FRAMEBUFFER = 0x8CA9;
        public const uint DRAW_FRAMEBUFFER_BINDING = 0x8CA6;
        public const uint READ_FRAMEBUFFER_BINDING = 0x8CAA;

        public const uint RENDERBUFFER_SAMPLES = 0x8CAB;
        public const uint FRAMEBUFFER_INCOMPLETE_MULTISAMPLE = 0x8D56;
        public const uint MAX_SAMPLES = 0x8D57;

        public const uint RENDERBUFFER_SAMPLES_IMG = 0x9133;
        public const uint FRAMEBUFFER_INCOMPLETE_MULTISAMPLE_IMG = 0x9134;
        public const uint MAX_SAMPLES_IMG = 0x9135;
        public const uint TEXTURE_SAMPLES_IMG = 0x9136;

        public const uint FRAGMENT_SHADER_DERIVATIVE_HINT_OES = 0x8B8B;

        public const uint RGB8_OES = 0x8051;
        public const uint RGBA8_OES = 0x8058;

        public const uint VERTEX_ARRAY_BINDING_OES = 0x85B5;

        public const uint TRANSLATED_SHADER_SOURCE_LENGTH_ANGLE = 0x93A0;

        public const uint TEXTURE_RECTANGLE_ARB = 0x84F5;
        public const uint TEXTURE_BINDING_RECTANGLE_ARB = 0x84F6;

        public const uint COMPRESSED_RGB_S3TC_DXT1_EXT = 0x83F0;
        public const uint COMPRESSED_RGBA_S3TC_DXT1_EXT = 0x83F1;
        public const uint COMPRESSED_RGBA_S3TC_DXT3_EXT = 0x83F2;
        public const uint COMPRESSED_RGBA_S3TC_DXT5_EXT = 0x83F3;

        public const uint ETC1_RGB8_OES = 0x8D64;

        public const uint COMPRESSED_RGB_PVRTC_4BPPV1_IMG = 0x8C00;
        public const uint COMPRESSED_RGB_PVRTC_2BPPV1_IMG = 0x8C01;
        public const uint COMPRESSED_RGBA_PVRTC_4BPPV1_IMG = 0x8C02;
        public const uint COMPRESSED_RGBA_PVRTC_2BPPV1_IMG = 0x8C03;

        public const uint COMPRESSED_ATC_RGB_AMD = 0x8C92;
        public const uint COMPRESSED_ATC_RGBA_EXPLICIT_ALPHA_AMD = 0x8C93;
        public const uint COMPRESSED_ATC_RGBA_INTERPOLATED_ALPHA_AMD = 0x87EE;

        public const uint TEXTURE_MAX_ANISOTROPY_EXT = 0x84FE;
        public const uint MAX_TEXTURE_MAX_ANISOTROPY_EXT = 0x84FF;

        public const uint MAX_DRAW_BUFFERS_EXT = 0x8824;
        public const uint DRAW_BUFFER0_EXT = 0x8825;
        public const uint DRAW_BUFFER1_EXT = 0x8826;
        public const uint DRAW_BUFFER2_EXT = 0x8827;
        public const uint DRAW_BUFFER3_EXT = 0x8828;
        public const uint DRAW_BUFFER4_EXT = 0x8829;
        public const uint DRAW_BUFFER5_EXT = 0x882A;
        public const uint DRAW_BUFFER6_EXT = 0x882B;
        public const uint DRAW_BUFFER7_EXT = 0x882C;
        public const uint DRAW_BUFFER8_EXT = 0x882D;
        public const uint DRAW_BUFFER9_EXT = 0x882E;
        public const uint DRAW_BUFFER10_EXT = 0x882F;
        public const uint DRAW_BUFFER11_EXT = 0x8830;
        public const uint DRAW_BUFFER12_EXT = 0x8831;
        public const uint DRAW_BUFFER13_EXT = 0x8832;
        public const uint DRAW_BUFFER14_EXT = 0x8833;
        public const uint DRAW_BUFFER15_EXT = 0x8834;
        public const uint MAX_COLOR_ATTACHMENTS_EXT = 0x8CDF;
        public const uint COLOR_ATTACHMENT0_EXT = 0x8CE0;
        public const uint COLOR_ATTACHMENT1_EXT = 0x8CE1;
        public const uint COLOR_ATTACHMENT2_EXT = 0x8CE2;
        public const uint COLOR_ATTACHMENT3_EXT = 0x8CE3;
        public const uint COLOR_ATTACHMENT4_EXT = 0x8CE4;
        public const uint COLOR_ATTACHMENT5_EXT = 0x8CE5;
        public const uint COLOR_ATTACHMENT6_EXT = 0x8CE6;
        public const uint COLOR_ATTACHMENT7_EXT = 0x8CE7;
        public const uint COLOR_ATTACHMENT8_EXT = 0x8CE8;
        public const uint COLOR_ATTACHMENT9_EXT = 0x8CE9;
        public const uint COLOR_ATTACHMENT10_EXT = 0x8CEA;
        public const uint COLOR_ATTACHMENT11_EXT = 0x8CEB;
        public const uint COLOR_ATTACHMENT12_EXT = 0x8CEC;
        public const uint COLOR_ATTACHMENT13_EXT = 0x8CED;
        public const uint COLOR_ATTACHMENT14_EXT = 0x8CEE;
        public const uint COLOR_ATTACHMENT15_EXT = 0x8CEF;

        private bool m_initializedAvailableExtensions;
        private HashSet<DOMString> m_availableExtensions;

        private readonly GraphicsContext3D m_context;

        private readonly bool m_isNVIDIA;
        private readonly bool m_isAMD;
        private readonly bool m_isIntel;
        private readonly bool m_isImagination;
        private readonly bool m_maySupportMultisampling;
        private readonly bool m_requiresBuiltInFunctionEmulation;
        private readonly DOMString m_vendor;

        private GLenum m_contextResetStatus;

        private bool m_supportsOESvertexArrayObject;
        private bool m_supportsIMGMultisampledRenderToTexture;
        private bool m_supportsANGLEinstancedArrays;

        private GLX.PFNGLFRAMEBUFFERTextureMULTISAMPLEIMG m_glFramebufferTextureMultisampleIMG;
        private GLX.PFNGLRENDERBUFFERSTORAGEMULTISAMPLEIMG m_glRenderbufferStorageMultisampleIMG;
        private GLX.PFNGLBINDVERTEXARRAYOESPROC m_glBindVertexArrayOES;
        private GLX.PFNGLDELETEVERTEXARRAYSOESPROC m_glDeleteVertexArraysOES;
        private GLX.PFNGLGENVERTEXARRAYSOESPROC m_glGenVertexArraysOES;
        private GLX.PFNGLISVERTEXARRAYOESPROC m_glIsVertexArrayOES;
        private GLX.PFNGLGETGRAPHICSRESETSTATUSEXTPROC m_glGetGraphicsResetStatusEXT;
        private GLX.PFNGLREADNPIXELSEXTPROC m_glReadnPixelsEXT;
        private GLX.PFNGLGETNUNIFORMFVEXTPROC m_glGetnUniformfvEXT;
        private GLX.PFNGLGETNUNIFORMIVEXTPROC m_glGetnUniformivEXT;
        private GLX.PFNGLVERTEXATTRIBDIVISORANGLEPROC m_glVertexAttribDivisorANGLE;
        private GLX.PFNGLDRAWARRAYSINSTANCEDANGLEPROC m_glDrawArraysInstancedANGLE;
        private GLX.PFNGLDRAWELEMENTSINSTANCEDANGLEPROC m_glDrawElementsInstancedANGLE;

        private ContextLostCallback m_contextLostCallback;

        public Extensions3D(GraphicsContext3D context)
        {
            this.m_contextResetStatus = GLES.GL_NO_ERROR;
            this.m_supportsOESvertexArrayObject = false;
            this.m_supportsIMGMultisampledRenderToTexture = false;
            this.m_supportsANGLEinstancedArrays = false;
            this.m_glFramebufferTextureMultisampleIMG = null;
            this.m_glRenderbufferStorageMultisampleIMG = null;
            this.m_glBindVertexArrayOES = null;
            this.m_glDeleteVertexArraysOES = null;
            this.m_glGenVertexArraysOES = null;
            this.m_glIsVertexArrayOES = null;
            this.m_glGetGraphicsResetStatusEXT = null;
            this.m_glReadnPixelsEXT = null;
            this.m_glGetnUniformfvEXT = null;
            this.m_glGetnUniformivEXT = null;
            this.m_glVertexAttribDivisorANGLE = null;
            this.m_glDrawArraysInstancedANGLE = null;
            this.m_glDrawElementsInstancedANGLE = null;

            this.m_initializedAvailableExtensions = false;
            this.m_context = context;
            this.m_isNVIDIA = false;
            this.m_isAMD = false;
            this.m_isIntel = false;
            this.m_isImagination = false;
            this.m_maySupportMultisampling = true;
            this.m_requiresBuiltInFunctionEmulation = false;
            this.m_vendor = GLES.glGetString(GLES.GL_VENDOR);

            var vendorComponents = new List<String>(this.m_vendor.ToLower().Split(' '));
            if (vendorComponents.Contains("nvidia"))
            {
                this.m_isNVIDIA = true;
            }
            if (vendorComponents.Contains("ati") || vendorComponents.Contains("amd"))
            {
                this.m_isAMD = true;
            }
            if (vendorComponents.Contains("intel"))
            {
                this.m_isIntel = true;
            }
            if (vendorComponents.Contains("imagination"))
            {
                this.m_isImagination = true;
            }
        }

        public bool supports(DOMString name)
        {
            if (!this.m_initializedAvailableExtensions)
            {
                this.initializeAvailableExtensions();
            }

            return this.supportsExtension(name);
        }

        public void ensureEnabled(DOMString name)
        {
        }

        public bool isEnabled(DOMString name)
        {
            return this.supports(name);
        }

        public int getGraphicsResetStatusARB()
        {
            if (this.m_contextResetStatus != GLES.GL_NO_ERROR)
            {
                return (int)this.m_contextResetStatus;
            }
            if (this.m_glGetGraphicsResetStatusEXT != null)
            {
                this.m_context.makeContextCurrent();
                var reasonForReset = (int)this.m_glGetGraphicsResetStatusEXT();
                if (reasonForReset != GLES.GL_NO_ERROR)
                {
                    if (this.m_contextLostCallback != null)
                    {
                        this.m_contextLostCallback.onContextLost();
                    }
                    this.m_contextResetStatus = (uint)reasonForReset;
                }
                return reasonForReset;
            }

            this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
            return 0;
        }

        public void blitFramebuffer(long srcX0, long srcY0, long srcX1, long srcY1, long dstX0, long dstY0, long dstX1, long dstY1, uint mask, uint filter)
        {
            notImplemented("blitFramebuffer");
        }

        public void framebufferTextureMultisampleIMG(uint target, uint attachment, uint textarget, uint texture, int level, uint samples)
        {
            if (this.m_glFramebufferTextureMultisampleIMG != null)
            {
                this.m_glFramebufferTextureMultisampleIMG(target, attachment, textarget, texture, level, (int)samples);
            }
            else
            {
                this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
            }
        }

        public void renderbufferStorageMultisampleIMG(uint target, uint samples, uint internalformat, uint width, uint height)
        {
            if (this.m_glRenderbufferStorageMultisampleIMG != null)
            {
                this.m_glRenderbufferStorageMultisampleIMG(target, (int)samples, internalformat, (int)width, (int)height);
            }
            else
            {
                this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
            }
        }

        public void renderbufferStorageMultisample(uint target, uint samples, uint internalformat, uint width, uint height)
        {
            if (this.m_glRenderbufferStorageMultisampleIMG != null)
            {
                this.renderbufferStorageMultisampleIMG(target, samples, internalformat, width, height);
            }
            else
            {
                notImplemented("renderbufferStorageMultisample");
            }
        }

        public Platform3DObject createVertexArrayOES()
        {
            this.m_context.makeContextCurrent();
            if (this.m_glGenVertexArraysOES != null)
            {
                var array = new uint[1];
                this.m_glGenVertexArraysOES(1, array);
                return array[0];
            }

            this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
            return 0;
        }

        public void deleteVertexArrayOES(Platform3DObject array)
        {
            if (array == 0)
            {
                return;
            }

            this.m_context.makeContextCurrent();
            if (this.m_glDeleteVertexArraysOES != null)
            {
                this.m_glDeleteVertexArraysOES(1, new uint[] {array});
            }
            else
            {
                this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
            }
        }

        public GLboolean isVertexArrayOES(Platform3DObject array)
        {
            if (array == 0)
            {
                return false;
            }

            this.m_context.makeContextCurrent();
            if (this.m_glIsVertexArrayOES != null)
            {
                return Convert.ToBoolean(this.m_glIsVertexArrayOES(array));
            }

            this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
            return false;
        }

        public void bindVertexArrayOES(Platform3DObject array)
        {
            if (array == 0)
            {
                return;
            }

            this.m_context.makeContextCurrent();
            if (this.m_glBindVertexArrayOES != null)
            {
                this.m_glBindVertexArrayOES(array);
            }
            else
            {
                this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
            }
        }

        public DOMString getTranslatedShaderSourceANGLE(Platform3DObject shader)
        {
            throw new NotImplementedException();
        }

        public void setEXTContextLostCallback(ContextLostCallback callback)
        {
            this.m_contextLostCallback = callback;
        }

        public void readnPixelsEXT(int x, int y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, IntPtr data)
        {
            if (this.m_glReadnPixelsEXT != null)
            {
                this.m_context.makeContextCurrent();

                GLES.glFlush();

                this.m_glReadnPixelsEXT(x, y, width, height, format, type, bufSize, data);
                return;
            }

            this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
        }

        public void getnUniformfvEXT(GLuint program, int location, GLsizei bufSize, IntPtr @params)
        {
            if (this.m_glGetnUniformfvEXT != null)
            {
                this.m_context.makeContextCurrent();
                this.m_glGetnUniformfvEXT(program, location, bufSize, @params);
                return;
            }

            this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
        }

        public void getnUniformivEXT(GLuint program, int location, GLsizei bufSize, IntPtr @params)
        {
            if (this.m_glGetnUniformivEXT != null)
            {
                this.m_context.makeContextCurrent();
                this.m_glGetnUniformivEXT(program, location, bufSize, @params);
                return;
            }

            this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
        }

        public void insertEventMarkerEXT(String s)
        {
            notImplemented("insertEventMarkerEXT");
        }

        public void pushGroupMarkerEXT(String s)
        {
            notImplemented("pushGroupMarkerEXT");
        }

        public void popGroupMarkerEXT()
        {
            notImplemented("popGroupMarkerEXT");
        }

        public void drawBuffersEXT(GLsizei n, GLenum[] bufs)
        {
            notImplemented("drawBuffersEXT");
        }

        public void drawArraysInstanced(GLenum mode, GLint first, GLsizei count, GLsizei primcount)
        {
            if (this.m_glDrawArraysInstancedANGLE == null)
            {
                this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
                return;
            }

            this.m_context.makeContextCurrent();
            this.m_glDrawArraysInstancedANGLE(mode, first, count, primcount);
        }

        public void drawElementsInstanced(GLenum mode, GLsizei count, GLenum type, long offset, GLsizei primcount)
        {
            if (this.m_glDrawElementsInstancedANGLE == null)
            {
                this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
                return;
            }

            this.m_context.makeContextCurrent();
            this.m_glDrawElementsInstancedANGLE(mode, count, type, new IntPtr(offset), primcount);
        }

        public void vertexAttribDivisor(GLuint index, GLuint divisor)
        {
            if (this.m_glVertexAttribDivisorANGLE == null)
            {
                this.m_context.synthesizeGLError(GLES.GL_INVALID_OPERATION);
                return;
            }

            this.m_context.makeContextCurrent();
            this.m_glVertexAttribDivisorANGLE(index, divisor);
        }

        public bool isNVIDIA()
        {
            return this.m_isNVIDIA;
        }

        public bool isAMD()
        {
            return this.m_isAMD;
        }

        public bool isIntel()
        {
            return this.m_isIntel;
        }

        public bool isImagination()
        {
            return this.m_isImagination;
        }

        public String vendor()
        {
            return this.m_vendor;
        }

        public bool maySupportMultisampling()
        {
            return this.m_maySupportMultisampling;
        }

        public bool requiresBuiltInFunctionEmulation()
        {
            return this.m_requiresBuiltInFunctionEmulation;
        }

        private bool supportsExtension(String name)
        {
            if (this.m_availableExtensions.Contains(name))
            {
                if (!this.m_supportsOESvertexArrayObject && name == "GL_OES_vertex_array_object")
                {
                    this.m_glBindVertexArrayOES = ReinterpretCast<GLX.PFNGLBINDVERTEXARRAYOESPROC>(EGL.eglGetProcAddress("glBindVertexArrayOES"));
                    this.m_glGenVertexArraysOES = ReinterpretCast<GLX.PFNGLGENVERTEXARRAYSOESPROC>(EGL.eglGetProcAddress("glGenVertexArraysOES"));
                    this.m_glDeleteVertexArraysOES = ReinterpretCast<GLX.PFNGLDELETEVERTEXARRAYSOESPROC>(EGL.eglGetProcAddress("glDeleteVertexArraysOES"));
                    this.m_glIsVertexArrayOES = ReinterpretCast<GLX.PFNGLISVERTEXARRAYOESPROC>(EGL.eglGetProcAddress("glIsVertexArrayOES"));
                    this.m_supportsOESvertexArrayObject = true;
                }
                else if (!this.m_supportsIMGMultisampledRenderToTexture && name == "GL_IMG_multisampled_render_to_texture")
                {
                    this.m_glFramebufferTextureMultisampleIMG = ReinterpretCast<GLX.PFNGLFRAMEBUFFERTextureMULTISAMPLEIMG>(EGL.eglGetProcAddress("glFramebufferTextureMultisampleIMG"));
                    this.m_glRenderbufferStorageMultisampleIMG = ReinterpretCast<GLX.PFNGLRENDERBUFFERSTORAGEMULTISAMPLEIMG>(EGL.eglGetProcAddress("glRenderbufferStorageMultisampleIMG"));
                    this.m_supportsIMGMultisampledRenderToTexture = true;
                }
                else if (this.m_glGetGraphicsResetStatusEXT == null && name == "GL_EXT_robustness")
                {
                    this.m_glGetGraphicsResetStatusEXT = ReinterpretCast<GLX.PFNGLGETGRAPHICSRESETSTATUSEXTPROC>(EGL.eglGetProcAddress("glGetGraphicsResetStatusEXT"));
                    this.m_glReadnPixelsEXT = ReinterpretCast<GLX.PFNGLREADNPIXELSEXTPROC>(EGL.eglGetProcAddress("glReadnPixelsEXT"));
                    this.m_glGetnUniformfvEXT = ReinterpretCast<GLX.PFNGLGETNUNIFORMFVEXTPROC>(EGL.eglGetProcAddress("glGetnUniformfvEXT"));
                    this.m_glGetnUniformivEXT = ReinterpretCast<GLX.PFNGLGETNUNIFORMIVEXTPROC>(EGL.eglGetProcAddress("glGetnUniformivEXT"));
                }
                else if (!this.m_supportsANGLEinstancedArrays && name == "GL_ANGLE_instanced_arrays")
                {
                    this.m_glVertexAttribDivisorANGLE = ReinterpretCast<GLX.PFNGLVERTEXATTRIBDIVISORANGLEPROC>(EGL.eglGetProcAddress("glVertexAttribDivisorANGLE"));
                    this.m_glDrawArraysInstancedANGLE = ReinterpretCast<GLX.PFNGLDRAWARRAYSINSTANCEDANGLEPROC>(EGL.eglGetProcAddress("glDrawArraysInstancedANGLE"));
                    this.m_glDrawElementsInstancedANGLE = ReinterpretCast<GLX.PFNGLDRAWELEMENTSINSTANCEDANGLEPROC>(EGL.eglGetProcAddress("glDrawElementsInstancedANGLE"));
                    this.m_supportsANGLEinstancedArrays = true;
                }
                else if (name == "GL_EXT_draw_buffers")
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        private static String getExtensions()
        {
            return GLES.glGetString(GLES.GL_EXTENSIONS);
        }

        private void initializeAvailableExtensions()
        {
            var extensionsString = getExtensions();
            var availableExtensions = new List<String>(extensionsString.Split(' '));
            this.m_availableExtensions = new HashSet<string>();
            foreach (var t in availableExtensions)
            {
                this.m_availableExtensions.Add(t);
            }
            this.m_initializedAvailableExtensions = true;
        }

        private static T ReinterpretCast<T>(IntPtr ptr) where T : class
        {
            return Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)) as T;
        }

        private static void notImplemented(string functionName)
        {
            JSConsole.log(functionName);
        }
    }

    // ReSharper restore InconsistentNaming
}
