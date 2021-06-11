using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using GLenum = System.UInt32;
using GLboolean = System.Boolean;
using GLint = System.Int32;
using GLsizei = System.Int32;
using GLintptr = System.Int64;
using GLsizeiptr = System.Int64;
using GLuint = System.UInt32;
using GLclampf = System.Single;
using DOMString = System.String;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class GraphicsContext3D : IDisposable
    {
        private static GraphicsContext3D currentContext;

        public const int DEPTH_BUFFER_BIT = 0x00000100;
        public const int STENCIL_BUFFER_BIT = 0x00000400;
        public const int COLOR_BUFFER_BIT = 0x00004000;
        public const int POINTS = 0x0000;
        public const int LINES = 0x0001;
        public const int LINE_LOOP = 0x0002;
        public const int LINE_STRIP = 0x0003;
        public const int TRIANGLES = 0x0004;
        public const int TRIANGLE_STRIP = 0x0005;
        public const int TRIANGLE_FAN = 0x0006;
        public const int ZERO = 0;
        public const int ONE = 1;
        public const int SRC_COLOR = 0x0300;
        public const int ONE_MINUS_SRC_COLOR = 0x0301;
        public const int SRC_ALPHA = 0x0302;
        public const int ONE_MINUS_SRC_ALPHA = 0x0303;
        public const int DST_ALPHA = 0x0304;
        public const int ONE_MINUS_DST_ALPHA = 0x0305;
        public const int DST_COLOR = 0x0306;
        public const int ONE_MINUS_DST_COLOR = 0x0307;
        public const int SRC_ALPHA_SATURATE = 0x0308;
        public const int FUNC_ADD = 0x8006;
        public const int BLEND_EQUATION = 0x8009;
        public const int BLEND_EQUATION_RGB = 0x8009;
        public const int BLEND_EQUATION_ALPHA = 0x883D;
        public const int FUNC_SUBTRACT = 0x800A;
        public const int FUNC_REVERSE_SUBTRACT = 0x800B;
        public const int BLEND_DST_RGB = 0x80C8;
        public const int BLEND_SRC_RGB = 0x80C9;
        public const int BLEND_DST_ALPHA = 0x80CA;
        public const int BLEND_SRC_ALPHA = 0x80CB;
        public const int CONSTANT_COLOR = 0x8001;
        public const int ONE_MINUS_CONSTANT_COLOR = 0x8002;
        public const int CONSTANT_ALPHA = 0x8003;
        public const int ONE_MINUS_CONSTANT_ALPHA = 0x8004;
        public const int BLEND_COLOR = 0x8005;
        public const int ARRAY_BUFFER = 0x8892;
        public const int ELEMENT_ARRAY_BUFFER = 0x8893;
        public const int ARRAY_BUFFER_BINDING = 0x8894;
        public const int ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;
        public const int STREAM_DRAW = 0x88E0;
        public const int STATIC_DRAW = 0x88E4;
        public const int DYNAMIC_DRAW = 0x88E8;
        public const int BUFFER_SIZE = 0x8764;
        public const int BUFFER_USAGE = 0x8765;
        public const int CURRENT_VERTEX_ATTRIB = 0x8626;
        public const int FRONT = 0x0404;
        public const int BACK = 0x0405;
        public const int FRONT_AND_BACK = 0x0408;
        public const int TEXTURE_2D = 0x0DE1;
        public const int CULL_FACE = 0x0B44;
        public const int BLEND = 0x0BE2;
        public const int DITHER = 0x0BD0;
        public const int STENCIL_TEST = 0x0B90;
        public const int DEPTH_TEST = 0x0B71;
        public const int SCISSOR_TEST = 0x0C11;
        public const int POLYGON_OFFSET_FILL = 0x8037;
        public const int SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
        public const int SAMPLE_COVERAGE = 0x80A0;
        public const int NO_ERROR = 0;
        public const int INVALID_ENUM = 0x0500;
        public const int INVALID_VALUE = 0x0501;
        public const int INVALID_OPERATION = 0x0502;
        public const int OUT_OF_MEMORY = 0x0505;
        public const int CW = 0x0900;
        public const int CCW = 0x0901;
        public const int LINE_WIDTH = 0x0B21;
        public const int ALIASED_POINT_SIZE_RANGE = 0x846D;
        public const int ALIASED_LINE_WIDTH_RANGE = 0x846E;
        public const int CULL_FACE_MODE = 0x0B45;
        public const int FRONT_FACE = 0x0B46;
        public const int DEPTH_RANGE = 0x0B70;
        public const int DEPTH_WRITEMASK = 0x0B72;
        public const int DEPTH_CLEAR_VALUE = 0x0B73;
        public const int DEPTH_FUNC = 0x0B74;
        public const int STENCIL_CLEAR_VALUE = 0x0B91;
        public const int STENCIL_FUNC = 0x0B92;
        public const int STENCIL_FAIL = 0x0B94;
        public const int STENCIL_PASS_DEPTH_FAIL = 0x0B95;
        public const int STENCIL_PASS_DEPTH_PASS = 0x0B96;
        public const int STENCIL_REF = 0x0B97;
        public const int STENCIL_VALUE_MASK = 0x0B93;
        public const int STENCIL_WRITEMASK = 0x0B98;
        public const int STENCIL_BACK_FUNC = 0x8800;
        public const int STENCIL_BACK_FAIL = 0x8801;
        public const int STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;
        public const int STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;
        public const int STENCIL_BACK_REF = 0x8CA3;
        public const int STENCIL_BACK_VALUE_MASK = 0x8CA4;
        public const int STENCIL_BACK_WRITEMASK = 0x8CA5;
        public const int VIEWPORT = 0x0BA2;
        public const int SCISSOR_BOX = 0x0C10;
        public const int COLOR_CLEAR_VALUE = 0x0C22;
        public const int COLOR_WRITEMASK = 0x0C23;
        public const int UNPACK_ALIGNMENT = 0x0CF5;
        public const int PACK_ALIGNMENT = 0x0D05;
        public const int MAX_TEXTURE_SIZE = 0x0D33;
        public const int MAX_VIEWPORT_DIMS = 0x0D3A;
        public const int SUBPIXEL_BITS = 0x0D50;
        public const int RED_BITS = 0x0D52;
        public const int GREEN_BITS = 0x0D53;
        public const int BLUE_BITS = 0x0D54;
        public const int ALPHA_BITS = 0x0D55;
        public const int DEPTH_BITS = 0x0D56;
        public const int STENCIL_BITS = 0x0D57;
        public const int POLYGON_OFFSET_UNITS = 0x2A00;
        public const int POLYGON_OFFSET_FACTOR = 0x8038;
        public const int TEXTURE_BINDING_2D = 0x8069;
        public const int SAMPLE_BUFFERS = 0x80A8;
        public const int SAMPLES = 0x80A9;
        public const int SAMPLE_COVERAGE_VALUE = 0x80AA;
        public const int SAMPLE_COVERAGE_INVERT = 0x80AB;
        public const int NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;
        public const int COMPRESSED_TEXTURE_FORMATS = 0x86A3;
        public const int DONT_CARE = 0x1100;
        public const int FASTEST = 0x1101;
        public const int NICEST = 0x1102;
        public const int GENERATE_MIPMAP_HINT = 0x8192;
        public const int BYTE = 0x1400;
        public const int UNSIGNED_BYTE = 0x1401;
        public const int SHORT = 0x1402;
        public const int UNSIGNED_SHORT = 0x1403;
        public const int INT = 0x1404;
        public const int UNSIGNED_INT = 0x1405;
        public const int FLOAT = 0x1406;
        public const int HALF_FLOAT_OES = 0x8D61;
        public const int FIXED = 0x140C;
        public const int DEPTH_COMPONENT = 0x1902;
        public const int ALPHA = 0x1906;
        public const int RGB = 0x1907;
        public const int RGBA = 0x1908;
        public const int BGRA = 0x80E1;
        public const int LUMINANCE = 0x1909;
        public const int LUMINANCE_ALPHA = 0x190A;
        public const int UNSIGNED_SHORT_4_4_4_4 = 0x8033;
        public const int UNSIGNED_SHORT_5_5_5_1 = 0x8034;
        public const int UNSIGNED_SHORT_5_6_5 = 0x8363;
        public const int FRAGMENT_SHADER = 0x8B30;
        public const int VERTEX_SHADER = 0x8B31;
        public const int MAX_VERTEX_ATTRIBS = 0x8869;
        public const int MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB;
        public const int MAX_VARYING_VECTORS = 0x8DFC;
        public const int MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;
        public const int MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;
        public const int MAX_TEXTURE_IMAGE_UNITS = 0x8872;
        public const int MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD;
        public const int SHADER_TYPE = 0x8B4F;
        public const int DELETE_STATUS = 0x8B80;
        public const int LINK_STATUS = 0x8B82;
        public const int VALIDATE_STATUS = 0x8B83;
        public const int ATTACHED_SHADERS = 0x8B85;
        public const int ACTIVE_UNIFORMS = 0x8B86;
        public const int ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87;
        public const int ACTIVE_ATTRIBUTES = 0x8B89;
        public const int ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A;
        public const int SHADING_LANGUAGE_VERSION = 0x8B8C;
        public const int CURRENT_PROGRAM = 0x8B8D;
        public const int NEVER = 0x0200;
        public const int LESS = 0x0201;
        public const int EQUAL = 0x0202;
        public const int LEQUAL = 0x0203;
        public const int GREATER = 0x0204;
        public const int NOTEQUAL = 0x0205;
        public const int GEQUAL = 0x0206;
        public const int ALWAYS = 0x0207;
        public const int KEEP = 0x1E00;
        public const int REPLACE = 0x1E01;
        public const int INCR = 0x1E02;
        public const int DECR = 0x1E03;
        public const int INVERT = 0x150A;
        public const int INCR_WRAP = 0x8507;
        public const int DECR_WRAP = 0x8508;
        public const int VENDOR = 0x1F00;
        public const int RENDERER = 0x1F01;
        public const int VERSION = 0x1F02;
        public const int EXTENSIONS = 0x1F03;
        public const int NEAREST = 0x2600;
        public const int LINEAR = 0x2601;
        public const int NEAREST_MIPMAP_NEAREST = 0x2700;
        public const int LINEAR_MIPMAP_NEAREST = 0x2701;
        public const int NEAREST_MIPMAP_LINEAR = 0x2702;
        public const int LINEAR_MIPMAP_LINEAR = 0x2703;
        public const int TEXTURE_MAG_FILTER = 0x2800;
        public const int TEXTURE_MIN_FILTER = 0x2801;
        public const int TEXTURE_WRAP_S = 0x2802;
        public const int TEXTURE_WRAP_T = 0x2803;
        public const int TEXTURE = 0x1702;
        public const int TEXTURE_CUBE_MAP = 0x8513;
        public const int TEXTURE_BINDING_CUBE_MAP = 0x8514;
        public const int TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
        public const int TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
        public const int TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
        public const int TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
        public const int TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
        public const int TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
        public const int MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;
        public const int TEXTURE0 = 0x84C0;
        public const int TEXTURE1 = 0x84C1;
        public const int TEXTURE2 = 0x84C2;
        public const int TEXTURE3 = 0x84C3;
        public const int TEXTURE4 = 0x84C4;
        public const int TEXTURE5 = 0x84C5;
        public const int TEXTURE6 = 0x84C6;
        public const int TEXTURE7 = 0x84C7;
        public const int TEXTURE8 = 0x84C8;
        public const int TEXTURE9 = 0x84C9;
        public const int TEXTURE10 = 0x84CA;
        public const int TEXTURE11 = 0x84CB;
        public const int TEXTURE12 = 0x84CC;
        public const int TEXTURE13 = 0x84CD;
        public const int TEXTURE14 = 0x84CE;
        public const int TEXTURE15 = 0x84CF;
        public const int TEXTURE16 = 0x84D0;
        public const int TEXTURE17 = 0x84D1;
        public const int TEXTURE18 = 0x84D2;
        public const int TEXTURE19 = 0x84D3;
        public const int TEXTURE20 = 0x84D4;
        public const int TEXTURE21 = 0x84D5;
        public const int TEXTURE22 = 0x84D6;
        public const int TEXTURE23 = 0x84D7;
        public const int TEXTURE24 = 0x84D8;
        public const int TEXTURE25 = 0x84D9;
        public const int TEXTURE26 = 0x84DA;
        public const int TEXTURE27 = 0x84DB;
        public const int TEXTURE28 = 0x84DC;
        public const int TEXTURE29 = 0x84DD;
        public const int TEXTURE30 = 0x84DE;
        public const int TEXTURE31 = 0x84DF;
        public const int ACTIVE_TEXTURE = 0x84E0;
        public const int REPEAT = 0x2901;
        public const int CLAMP_TO_EDGE = 0x812F;
        public const int MIRRORED_REPEAT = 0x8370;
        public const int FLOAT_VEC2 = 0x8B50;
        public const int FLOAT_VEC3 = 0x8B51;
        public const int FLOAT_VEC4 = 0x8B52;
        public const int INT_VEC2 = 0x8B53;
        public const int INT_VEC3 = 0x8B54;
        public const int INT_VEC4 = 0x8B55;
        public const int BOOL = 0x8B56;
        public const int BOOL_VEC2 = 0x8B57;
        public const int BOOL_VEC3 = 0x8B58;
        public const int BOOL_VEC4 = 0x8B59;
        public const int FLOAT_MAT2 = 0x8B5A;
        public const int FLOAT_MAT3 = 0x8B5B;
        public const int FLOAT_MAT4 = 0x8B5C;
        public const int SAMPLER_2D = 0x8B5E;
        public const int SAMPLER_CUBE = 0x8B60;
        public const int VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;
        public const int VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;
        public const int VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;
        public const int VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;
        public const int VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;
        public const int VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;
        public const int VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;
        public const int COMPILE_STATUS = 0x8B81;
        public const int INFO_LOG_LENGTH = 0x8B84;
        public const int SHADER_SOURCE_LENGTH = 0x8B88;
        public const int SHADER_COMPILER = 0x8DFA;
        public const int SHADER_BINARY_FORMATS = 0x8DF8;
        public const int NUM_SHADER_BINARY_FORMATS = 0x8DF9;
        public const int LOW_FLOAT = 0x8DF0;
        public const int MEDIUM_FLOAT = 0x8DF1;
        public const int HIGH_FLOAT = 0x8DF2;
        public const int LOW_INT = 0x8DF3;
        public const int MEDIUM_INT = 0x8DF4;
        public const int HIGH_INT = 0x8DF5;
        public const int FRAMEBUFFER = 0x8D40;
        public const int RENDERBUFFER = 0x8D41;
        public const int RGBA4 = 0x8056;
        public const int RGB5_A1 = 0x8057;
        public const int RGB565 = 0x8D62;
        public const int DEPTH_COMPONENT16 = 0x81A5;
        public const int STENCIL_INDEX = 0x1901;
        public const int STENCIL_INDEX8 = 0x8D48;
        public const int DEPTH_STENCIL = 0x84F9;
        public const int UNSIGNED_INT_24_8 = 0x84FA;
        public const int DEPTH24_STENCIL8 = 0x88F0;
        public const int RENDERBUFFER_WIDTH = 0x8D42;
        public const int RENDERBUFFER_HEIGHT = 0x8D43;
        public const int RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;
        public const int RENDERBUFFER_RED_SIZE = 0x8D50;
        public const int RENDERBUFFER_GREEN_SIZE = 0x8D51;
        public const int RENDERBUFFER_BLUE_SIZE = 0x8D52;
        public const int RENDERBUFFER_ALPHA_SIZE = 0x8D53;
        public const int RENDERBUFFER_DEPTH_SIZE = 0x8D54;
        public const int RENDERBUFFER_STENCIL_SIZE = 0x8D55;
        public const int FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;
        public const int FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;
        public const int FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;
        public const int FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;
        public const int COLOR_ATTACHMENT0 = 0x8CE0;
        public const int DEPTH_ATTACHMENT = 0x8D00;
        public const int STENCIL_ATTACHMENT = 0x8D20;
        public const int DEPTH_STENCIL_ATTACHMENT = 0x821A;
        public const int NONE = 0;
        public const int FRAMEBUFFER_COMPLETE = 0x8CD5;
        public const int FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
        public const int FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
        public const int FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9;
        public const int FRAMEBUFFER_UNSUPPORTED = 0x8CDD;
        public const int FRAMEBUFFER_BINDING = 0x8CA6;
        public const int RENDERBUFFER_BINDING = 0x8CA7;
        public const int MAX_RENDERBUFFER_SIZE = 0x84E8;
        public const int INVALID_FRAMEBUFFER_OPERATION = 0x0506;
        public const int UNPACK_FLIP_Y_WEBGL = 0x9240;
        public const int UNPACK_PREMULTIPLY_ALPHA_WEBGL = 0x9241;
        public const int CONTEXT_LOST_WEBGL = 0x9242;
        public const int UNPACK_COLORSPACE_CONVERSION_WEBGL = 0x9243;
        public const int BROWSER_DEFAULT_WEBGL = 0x9244;
        public const int VERTEX_ATTRIB_ARRAY_DIVISOR_ANGLE = 0x88FE;

        internal class GraphicsContext3DState
        {
            public GLuint boundFBO;
            public GLenum activeTexture;

            public GraphicsContext3DState()
            {
                this.boundFBO = 0;
                this.activeTexture = TEXTURE0;
            }
        }

        public class ActiveShaderSymbolCounts
        {
            public readonly List<GLint> filteredToActualAttributeIndexMap = new List<int>();
            public readonly List<GLint> filteredToActualUniformIndexMap = new List<int>();

            public GLint countForType(GLenum activeType)
            {
                return activeType == ACTIVE_ATTRIBUTES ? this.filteredToActualAttributeIndexMap.Count : this.filteredToActualUniformIndexMap.Count;
            }
        }

        private int m_currentWidth;
        private int m_currentHeight;

        private Extensions3D m_extensions;

        private readonly Attributes m_attrs = new Attributes();

        private readonly IntPtr m_hdc;
        private readonly IntPtr m_hostWindow;
        private readonly IntPtr m_context;
        private readonly IntPtr m_display;
        private readonly IntPtr m_surface;

        private readonly List<uint> m_syntheticErrors = new List<uint>();
        private readonly GraphicsContext3DState m_state = new GraphicsContext3DState();

        private readonly Dictionary<Platform3DObject, ActiveShaderSymbolCounts> m_shaderProgramSymbolCountMap = new Dictionary<Platform3DObject, ActiveShaderSymbolCounts>();
        private readonly Dictionary<Platform3DObject, String> m_shaderSourceMap = new Dictionary<Platform3DObject, String>();
        private bool m_disposed;

        public GraphicsContext3D(Attributes attributes, IntPtr hostWindow)
        {
            this.m_hostWindow = hostWindow;
            this.m_attrs = attributes;
            this.m_hdc = User32.GetDC(hostWindow);
            this.m_display = EGL.eglGetDisplay(this.m_hdc);

            int major, minor;
            if (EGL.eglInitialize(this.m_display, out major, out minor) != EGL.EGL_TRUE)
            {
                throw new ApplicationException(String.Format("{0} [Error: {1}]", "Failed to initialize display", EGL.eglGetError()));
            }

            int numConfigs;
            if (EGL.eglGetConfigs(this.m_display, null, 0, out numConfigs) != EGL.EGL_TRUE)
            {
                throw new ApplicationException(String.Format("{0} [Error: {1}]", "Failed to obtain display configurations", EGL.eglGetError()));
            }

            var configs = new IntPtr[numConfigs];
            var attribList = new[]
            {
                EGL.EGL_RED_SIZE, 8,
                EGL.EGL_GREEN_SIZE, 8,
                EGL.EGL_BLUE_SIZE, 8,
                EGL.EGL_ALPHA_SIZE, 8,
                EGL.EGL_DEPTH_SIZE, 24,
                EGL.EGL_STENCIL_SIZE, 8,
                EGL.EGL_SAMPLE_BUFFERS, 0,
                EGL.EGL_NONE, EGL.EGL_NONE
            };
            if (EGL.eglChooseConfig(this.m_display, attribList, configs, configs.Length, out numConfigs) != EGL.EGL_TRUE)
            {
                throw new ApplicationException(String.Format("{0} [Error: {1}]", "Failed to configure display", EGL.eglGetError()));
            }

            var config = configs[0];
            this.m_surface = EGL.eglCreateWindowSurface(this.m_display, config, hostWindow, null);

            int[] contextAttribs = {EGL.EGL_CONTEXT_CLIENT_VERSION, 2, EGL.EGL_NONE, EGL.EGL_NONE};
            this.m_context = EGL.eglCreateContext(this.m_display, config, IntPtr.Zero, contextAttribs);

            if (EGL.eglMakeCurrent(this.m_display, this.m_surface, this.m_surface, this.m_context) == EGL.EGL_FALSE)
            {
                throw new ApplicationException(String.Format("{0} [Error: {1}]", "Failed to attach rendering context to surfaces", EGL.eglGetError()));
            }
        }

        public void activeTexture(GLenum texture)
        {
            this.m_state.activeTexture = texture;
            this.makeContextCurrent();
            GLES.glActiveTexture(texture);
        }

        public void attachShader(Platform3DObject program, Platform3DObject shader)
        {
            this.m_shaderProgramSymbolCountMap.Remove(program);
            this.makeContextCurrent();
            GLES.glAttachShader(program, shader);
        }

        public void bindAttribLocation(Platform3DObject program, GLenum index, String name)
        {
            this.makeContextCurrent();
            GLES.glBindAttribLocation(program, index, name);
        }

        public void bindBuffer(UInt32 target, Platform3DObject buffer)
        {
            this.makeContextCurrent();
            GLES.glBindBuffer(target, buffer);
        }

        public void bindFramebuffer(UInt32 target, Platform3DObject buffer)
        {
            if (buffer != this.m_state.boundFBO)
            {
                this.makeContextCurrent();
                GLES.glBindFramebuffer(target, buffer);
                this.m_state.boundFBO = buffer;
            }
        }

        public void bindRenderbuffer(UInt32 target, Platform3DObject renderbuffer)
        {
            this.makeContextCurrent();
            GLES.glBindRenderbuffer(target, renderbuffer);
        }

        public void bindTexture(UInt32 target, Platform3DObject texture)
        {
            this.makeContextCurrent();
            GLES.glBindTexture(target, texture);
        }

        public void blendColor(Single red, Single green, Single blue, Single alpha)
        {
            this.makeContextCurrent();
            GLES.glBlendColor(red, green, blue, alpha);
        }

        public void blendEquation(UInt32 mode)
        {
            this.makeContextCurrent();
            GLES.glBlendEquation(mode);
        }

        public void blendEquationSeparate(UInt32 modeRGB, UInt32 modeAlpha)
        {
            this.makeContextCurrent();
            GLES.glBlendEquationSeparate(modeRGB, modeAlpha);
        }

        public void blendFunc(UInt32 sfactor, UInt32 dfactor)
        {
            this.makeContextCurrent();
            GLES.glBlendFunc(sfactor, dfactor);
        }

        public void blendFuncSeparate(UInt32 srcRGB, UInt32 dstRGB, UInt32 srcAlpha, UInt32 dstAlpha)
        {
            this.makeContextCurrent();
            GLES.glBlendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        public void bufferData(GLenum target, GLsizeiptr size, GLenum usage)
        {
            this.makeContextCurrent();
            GLES.glBufferData(target, size, IntPtr.Zero, usage);
        }

        public void bufferData(UInt32 target, GLsizeiptr size, IntPtr data, UInt32 usage)
        {
            this.makeContextCurrent();
            GLES.glBufferData(target, size, data, usage);
        }

        public void bufferSubData(UInt32 target, Int64 offset, GLsizeiptr size, IntPtr data)
        {
            this.makeContextCurrent();
            GLES.glBufferSubData(target, offset, size, data);
        }

        public UInt32 checkFramebufferStatus(UInt32 target)
        {
            this.makeContextCurrent();
            return GLES.glCheckFramebufferStatus(target);
        }

        public void clear(UInt32 mask)
        {
            this.makeContextCurrent();
            GLES.glClear(mask);
        }

        public void clearColor(Single r, Single g, Single b, Single a)
        {
            this.makeContextCurrent();
            GLES.glClearColor(r, g, b, a);
        }

        public void clearDepth(Single depth)
        {
            this.makeContextCurrent();
            GLES.glClearDepthf(depth);
        }

        public void clearStencil(Int32 s)
        {
            this.makeContextCurrent();
            GLES.glClearStencil(s);
        }

        public void colorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha)
        {
            this.makeContextCurrent();
            GLES.glColorMask(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue), Convert.ToByte(alpha));
        }

        public bool compileShader(Platform3DObject shader)
        {
            this.makeContextCurrent();
            GLES.glShaderSource(shader, 1, new[] {this.m_shaderSourceMap[shader]}, null);
            GLES.glCompileShader(shader);

            int glCompileSuccess;
            GLES.glGetShaderiv(shader, COMPILE_STATUS, out glCompileSuccess);
            return glCompileSuccess == GLES.GL_TRUE;
        }

        public void copyTexImage2D(UInt32 target, Int32 level, UInt32 internalformat, Int32 x, Int32 y, Int32 width, Int32 height, Int32 border)
        {
            this.makeContextCurrent();
            GLES.glCopyTexImage2D(target, level, internalformat, x, y, width, height, border);
        }

        public void copyTexSubImage2D(UInt32 target, Int32 level, Int32 xoffset, Int32 yoffset, Int32 x, Int32 y, Int32 width, Int32 height)
        {
            this.makeContextCurrent();
            GLES.glCopyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
        }

        public void compressedTexImage2D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, IntPtr data)
        {
            this.makeContextCurrent();
            GLES.glCompressedTexImage2D(target, level, internalformat, width, height, border, imageSize, data);
        }

        public void compressedTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, IntPtr data)
        {
            this.makeContextCurrent();
            GLES.glCompressedTexSubImage2D(target, level, xoffset, yoffset, width, height, format, imageSize, data);
        }

        public void cullFace(UInt32 mode)
        {
            this.makeContextCurrent();
            GLES.glCullFace(mode);
        }

        public void deleteBuffers(int n, uint[] buffers)
        {
            this.makeContextCurrent();
            GLES.glDeleteBuffers(n, buffers);
        }

        public void deleteFramebuffers(int n, uint[] framebuffers)
        {
            this.makeContextCurrent();
            GLES.glDeleteFramebuffers(n, framebuffers);
        }

        public void deleteRenderbuffers(int n, uint[] renderbuffers)
        {
            this.makeContextCurrent();
            GLES.glDeleteRenderbuffers(n, renderbuffers);
        }

        public void deleteShader(Platform3DObject shader)
        {
            this.makeContextCurrent();
            GLES.glDeleteShader(shader);
        }

        public void deleteTextures(int n, uint[] textures)
        {
            this.makeContextCurrent();
            GLES.glDeleteTextures(n, textures);
        }

        public void depthFunc(UInt32 func)
        {
            this.makeContextCurrent();
            GLES.glDepthFunc(func);
        }

        public void depthMask(GLboolean flag)
        {
            this.makeContextCurrent();
            GLES.glDepthMask(Convert.ToByte(flag));
        }

        public void depthRange(Single zNear, Single zFar)
        {
            this.makeContextCurrent();
            GLES.glDepthRangef(zNear, zFar);
        }

        public void detachShader(Platform3DObject program, Platform3DObject shader)
        {
            this.makeContextCurrent();
            this.m_shaderProgramSymbolCountMap.Remove(program);
            GLES.glDetachShader(program, shader);
        }

        public void disable(UInt32 cap)
        {
            this.makeContextCurrent();
            GLES.glDisable(cap);
        }

        public void disableVertexAttribArray(UInt32 index)
        {
            this.makeContextCurrent();
            GLES.glDisableVertexAttribArray(index);
        }

        public void drawArrays(UInt32 mode, Int32 first, Int32 count)
        {
            this.makeContextCurrent();
            GLES.glDrawArrays(mode, first, count);
        }

        public void drawElements(UInt32 mode, Int32 count, UInt32 type, IntPtr offset)
        {
            this.makeContextCurrent();
            GLES.glDrawElements(mode, count, type, offset);
        }

        public void drawArraysInstanced(GLenum mode, GLint first, GLsizei count, GLsizei primcount)
        {
            this.getExtensions().drawArraysInstanced(mode, first, count, primcount);
        }

        public void drawElementsInstanced(GLenum mode, GLsizei count, GLenum type, GLintptr offset, GLsizei primcount)
        {
            this.getExtensions().drawElementsInstanced(mode, count, type, offset, primcount);
        }

        public void enable(UInt32 cap)
        {
            this.makeContextCurrent();
            GLES.glEnable(cap);
        }

        public void enableVertexAttribArray(UInt32 index)
        {
            this.makeContextCurrent();
            GLES.glEnableVertexAttribArray(index);
        }

        public void finish()
        {
            this.makeContextCurrent();
            GLES.glFinish();
        }

        public void flush()
        {
            this.makeContextCurrent();
            GLES.glFlush();
        }

        public void framebufferRenderbuffer(UInt32 target, UInt32 attachment, UInt32 renderbuffertarget, Platform3DObject buffer)
        {
            this.makeContextCurrent();
            GLES.glFramebufferRenderbuffer(target, attachment, renderbuffertarget, buffer);
        }

        public void framebufferTexture(UInt32 target, UInt32 attachment, UInt32 textarget, Platform3DObject texture, Int32 level)
        {
            this.makeContextCurrent();
            GLES.glFramebufferTexture(target, attachment, textarget, texture, level);
        }

        public void frontFace(UInt32 mode)
        {
            this.makeContextCurrent();
            GLES.glFrontFace(mode);
        }

        public void genBuffers(int n, uint[] buffers)
        {
            this.makeContextCurrent();
            GLES.glGenBuffers(n, buffers);
        }

        public void generateMipmap(UInt32 target)
        {
            this.makeContextCurrent();
            GLES.glGenerateMipmap(target);
        }

        public void genFramebuffers(int n, uint[] framebuffers)
        {
            this.makeContextCurrent();
            GLES.glGenFramebuffers(n, framebuffers);
        }

        public void genRenderbuffers(int n, uint[] renderbuffers)
        {
            this.makeContextCurrent();
            GLES.glGenRenderbuffers(n, renderbuffers);
        }

        public void genTextures(int n, uint[] textures)
        {
            this.makeContextCurrent();
            GLES.glGenTextures(n, textures);
        }

        public bool getActiveAttrib(Platform3DObject program, GLuint index, ActiveInfo info)
        {
            var result = this.m_shaderProgramSymbolCountMap.ContainsKey(program) ? this.m_shaderProgramSymbolCountMap[program] : null;
            if (result == null)
            {
                GLint symbolCount;
                this.getNonBuiltInActiveSymbolCount(program, ACTIVE_ATTRIBUTES, out symbolCount);
                result = this.m_shaderProgramSymbolCountMap[program];
            }

            var symbolCounts = result;
            var rawIndex = (uint)((index < symbolCounts.filteredToActualAttributeIndexMap.Count) ? symbolCounts.filteredToActualAttributeIndexMap[(int)index] : -1);

            return this.getActiveAttribImpl(program, rawIndex, info);
        }

        public bool getActiveAttribImpl(Platform3DObject program, GLuint index, ActiveInfo info)
        {
            if (program == 0)
            {
                this.synthesizeGLError(INVALID_VALUE);
                return false;
            }
            this.makeContextCurrent();
            int maxAttributeSize;
            GLES.glGetProgramiv(program, GLES.GL_ACTIVE_ATTRIBUTE_MAX_LENGTH, out maxAttributeSize);
            var name = new StringBuilder(maxAttributeSize);
            int nameLength;
            int size;
            GLenum type;
            GLES.glGetActiveAttrib(program, index, maxAttributeSize, out nameLength, out size, out type, name);
            if (nameLength == 0)
            {
                return false;
            }

            info.name = name.ToString();
            info.type = type;
            info.size = size;
            return true;
        }

        public bool getActiveUniform(Platform3DObject program, GLuint index, ActiveInfo info)
        {
            var result = this.m_shaderProgramSymbolCountMap.ContainsKey(program) ? this.m_shaderProgramSymbolCountMap[program] : null;
            if (result == null)
            {
                int symbolCount;
                this.getNonBuiltInActiveSymbolCount(program, ACTIVE_UNIFORMS, out symbolCount);
                result = this.m_shaderProgramSymbolCountMap[program];
            }

            var symbolCounts = result;
            var rawIndex = (uint)((index < symbolCounts.filteredToActualUniformIndexMap.Count) ? symbolCounts.filteredToActualUniformIndexMap[(int)index] : -1);

            return this.getActiveUniformImpl(program, rawIndex, info);
        }

        public bool getActiveUniformImpl(Platform3DObject program, GLuint index, ActiveInfo info)
        {
            if (program == 0)
            {
                this.synthesizeGLError(INVALID_VALUE);
                return false;
            }

            this.makeContextCurrent();
            int maxUniformSize;
            GLES.glGetProgramiv(program, GLES.GL_ACTIVE_UNIFORM_MAX_LENGTH, out maxUniformSize);

            var name = new StringBuilder(maxUniformSize);
            int nameLength;
            int size;
            GLenum type;
            GLES.glGetActiveUniform(program, index, maxUniformSize, out nameLength, out size, out type, name);
            if (nameLength == 0)
            {
                return false;
            }

            info.name = name.ToString();
            info.type = type;
            info.size = size;
            return true;
        }

        public void getAttachedShaders(Platform3DObject program, Int32 maxCount, out Int32 count, out Platform3DObject[] shaders)
        {
            if (program == 0)
            {
                this.synthesizeGLError(INVALID_VALUE);
                count = 0;
                shaders = new Platform3DObject[count];
                return;
            }
            this.makeContextCurrent();
            var temp = new uint[maxCount];
            GLES.glGetAttachedShaders(program, maxCount, out count, temp);
            shaders = new Platform3DObject[count];
            Array.Copy(temp, shaders, count);
        }

        public int getAttribLocation(Platform3DObject program, String name)
        {
            this.makeContextCurrent();
            return program != 0 ? GLES.glGetAttribLocation(program, name) : -1;
        }

        public void getBooleanv(UInt32 pname, byte[] @params)
        {
            this.makeContextCurrent();
            GLES.glGetBooleanv(pname, @params);
        }

        public void getIntegerv(UInt32 pname, Int32[] @params)
        {
            this.makeContextCurrent();
            GLES.glGetIntegerv(pname, @params);
        }

        public void getFloatv(UInt32 pname, Single[] @params)
        {
            this.makeContextCurrent();
            GLES.glGetFloatv(pname, @params);
        }

        public void getBufferParameteriv(UInt32 target, UInt32 pname, out Int32 value)
        {
            this.makeContextCurrent();
            GLES.glGetBufferParameteriv(target, pname, out value);
        }

        public UInt32 getError()
        {
            if (this.m_syntheticErrors.Count > 0)
            {
                var err = this.m_syntheticErrors[0];
                this.m_syntheticErrors.RemoveAt(0);
                return err;
            }
            this.makeContextCurrent();
            return GLES.glGetError();
        }

        public void getFramebufferAttachmentParameteriv(UInt32 target, UInt32 attachment, UInt32 pname, out Int32 value)
        {
            this.makeContextCurrent();
            if (attachment == DEPTH_STENCIL_ATTACHMENT)
            {
                attachment = DEPTH_ATTACHMENT;
            }
            GLES.glGetFramebufferAttachmentParameteriv(target, attachment, pname, out value);
        }

        public void getProgramiv(Platform3DObject program, UInt32 pname, out Int32 value)
        {
            this.makeContextCurrent();
            GLES.glGetProgramiv(program, pname, out value);
        }

        public String getProgramInfoLog(Platform3DObject program)
        {
            this.makeContextCurrent();
            int length;
            GLES.glGetProgramiv(program, GLES.GL_INFO_LOG_LENGTH, out length);
            if (length != 0)
            {
                int size;
                var info = new StringBuilder(length);
                GLES.glGetProgramInfoLog(program, length, out size, info);
                return info.ToString();
            }
            return String.Empty;
        }

        public void getRenderbufferParameteriv(UInt32 target, UInt32 pname, out Int32 value)
        {
            this.makeContextCurrent();
            GLES.glGetRenderbufferParameteriv(target, pname, out value);
        }

        public void getShaderiv(Platform3DObject shader, UInt32 pname, out Int32 value)
        {
            this.makeContextCurrent();
            switch (pname)
            {
                case GLES.GL_DELETE_STATUS:
                case GLES.GL_SHADER_TYPE:
                    GLES.glGetShaderiv(shader, pname, out value);
                    break;
                case GLES.GL_COMPILE_STATUS:
                    GLES.glGetShaderiv(shader, pname, out value);
                    break;
                case GLES.GL_INFO_LOG_LENGTH:
                    value = this.getShaderInfoLog(shader).Length;
                    break;
                case GLES.GL_SHADER_SOURCE_LENGTH:
                    value = this.getShaderSource(shader).Length;
                    break;
                default:
                    value = 0;
                    this.synthesizeGLError(GLES.GL_INVALID_ENUM);
                    break;
            }
        }

        public void getShaderInfoLog(uint shader, int bufsize, out int length, StringBuilder infolog)
        {
            this.makeContextCurrent();
            GLES.glGetShaderInfoLog(shader, bufsize, out length, infolog);
        }

        public void getShaderPrecisionFormat(UInt32 shaderType, UInt32 precisionType, Int32[] range, out Int32 precision)
        {
            this.makeContextCurrent();
            GLES.glGetShaderPrecisionFormat(shaderType, precisionType, range, out precision);
        }

        public void getShaderSource(uint shader, int bufsize, out int length, StringBuilder source)
        {
            this.makeContextCurrent();
            GLES.glGetShaderSource(shader, bufsize, out length, source);
        }

        public String getString(UInt32 name)
        {
            this.makeContextCurrent();
            return GLES.glGetString(name);
        }

        public void getTexParameterfv(UInt32 target, UInt32 pname, Single[] value)
        {
            this.makeContextCurrent();
            GLES.glGetTexParameterfv(target, pname, value);
        }

        public void getTexParameteriv(UInt32 target, UInt32 pname, Int32[] value)
        {
            this.makeContextCurrent();
            GLES.glGetTexParameteriv(target, pname, value);
        }

        public void getUniformfv(Platform3DObject program, Int32 location, Single[] value)
        {
            this.makeContextCurrent();
            GLES.glGetUniformfv(program, location, value);
        }

        public void getUniformiv(Platform3DObject program, Int32 location, Int32[] value)
        {
            this.makeContextCurrent();
            GLES.glGetUniformiv(program, location, value);
        }

        public Int32 getUniformLocation(Platform3DObject program, String name)
        {
            this.makeContextCurrent();
            return GLES.glGetUniformLocation(program, name);
        }

        public void getVertexAttribfv(UInt32 index, UInt32 pname, Single[] value)
        {
            this.makeContextCurrent();
            GLES.glGetVertexAttribfv(index, pname, value);
        }

        public void getVertexAttribiv(UInt32 index, UInt32 pname, Int32[] value)
        {
            this.makeContextCurrent();
            GLES.glGetVertexAttribiv(index, pname, value);
        }

        public IntPtr getVertexAttribOffset(UInt32 index, UInt32 pname)
        {
            this.makeContextCurrent();
            IntPtr pointer;
            GLES.glGetVertexAttribPointerv(index, pname, out pointer);
            return pointer;
        }

        public void hint(UInt32 target, UInt32 mode)
        {
            this.makeContextCurrent();
            GLES.glHint(target, mode);
        }

        public bool isBuffer(Platform3DObject buffer)
        {
            this.makeContextCurrent();
            return buffer != 0 && GLES.glIsBuffer(buffer) == GLES.GL_TRUE;
        }

        public bool isEnabled(UInt32 cap)
        {
            this.makeContextCurrent();
            return GLES.glIsEnabled(cap) == GLES.GL_TRUE;
        }

        public bool isFramebuffer(Platform3DObject framebuffer)
        {
            this.makeContextCurrent();
            return framebuffer != 0 && GLES.glIsFramebuffer(framebuffer) == GLES.GL_TRUE;
        }

        public bool isProgram(Platform3DObject program)
        {
            this.makeContextCurrent();
            return program != 0 && GLES.glIsProgram(program) == GLES.GL_TRUE;
        }

        public bool isRenderbuffer(Platform3DObject renderbuffer)
        {
            this.makeContextCurrent();
            return renderbuffer != 0 && GLES.glIsRenderbuffer(renderbuffer) == GLES.GL_TRUE;
        }

        public bool isShader(Platform3DObject shader)
        {
            this.makeContextCurrent();
            return shader != 0 && GLES.glIsShader(shader) == GLES.GL_TRUE;
        }

        public bool isTexture(Platform3DObject texture)
        {
            this.makeContextCurrent();
            return texture != 0 && GLES.glIsTexture(texture) == GLES.GL_TRUE;
        }

        public void lineWidth(Single width)
        {
            this.makeContextCurrent();
            GLES.glLineWidth(width);
        }

        public void linkProgram(Platform3DObject program)
        {
            this.makeContextCurrent();
            GLES.glLinkProgram(program);
        }

        public void pixelStorei(UInt32 pname, Int32 param)
        {
            this.makeContextCurrent();
            GLES.glPixelStorei(pname, param);
        }

        public void polygonOffset(Single factor, Single units)
        {
            this.makeContextCurrent();
            GLES.glPolygonOffset(factor, units);
        }

        public void readPixels(Int32 x, Int32 y, Int32 width, Int32 height, UInt32 format, UInt32 type, IntPtr data)
        {
            this.makeContextCurrent();
            GLES.glFlush();
            GLES.glReadPixels(x, y, width, height, format, type, data);
        }

        public void releaseShaderCompiler()
        {
            this.makeContextCurrent();
            GLES.glReleaseShaderCompiler();
        }

        public void renderbufferStorage(UInt32 target, UInt32 internalformat, Int32 width, Int32 height)
        {
            this.makeContextCurrent();
            GLES.glRenderbufferStorage(target, internalformat, width, height);
        }

        public void sampleCoverage(GLclampf value, GLboolean invert)
        {
            this.makeContextCurrent();
            GLES.glSampleCoverage(value, Convert.ToByte(invert));
        }

        public void scissor(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            this.makeContextCurrent();
            GLES.glScissor(x, y, width, height);
        }

        public void shaderSource(Platform3DObject shader, String @String)
        {
            this.makeContextCurrent();
            this.m_shaderSourceMap[shader] = @String;
        }

        public void stencilFunc(UInt32 func, Int32 @ref, UInt32 mask)
        {
            this.makeContextCurrent();
            GLES.glStencilFunc(func, @ref, mask);
        }

        public void stencilFuncSeparate(UInt32 face, UInt32 func, Int32 @ref, UInt32 mask)
        {
            this.makeContextCurrent();
            GLES.glStencilFuncSeparate(face, func, @ref, mask);
        }

        public void stencilMask(UInt32 mask)
        {
            this.makeContextCurrent();
            GLES.glStencilMask(mask);
        }

        public void stencilMaskSeparate(UInt32 face, UInt32 mask)
        {
            this.makeContextCurrent();
            GLES.glStencilMaskSeparate(face, mask);
        }

        public void stencilOp(UInt32 fail, UInt32 zfail, UInt32 zpass)
        {
            this.makeContextCurrent();
            GLES.glStencilOp(fail, zfail, zpass);
        }

        public void stencilOpSeparate(UInt32 face, UInt32 fail, UInt32 zfail, UInt32 zpass)
        {
            this.makeContextCurrent();
            GLES.glStencilOpSeparate(face, fail, zfail, zpass);
        }

        public bool texImage2D(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 border, UInt32 format, UInt32 type, IntPtr pixels)
        {
            if (width != 0 && height != 0 && pixels == IntPtr.Zero)
            {
                this.synthesizeGLError(INVALID_VALUE);
                return false;
            }
            this.makeContextCurrent();
            GLES.glTexImage2D(target, level, (int)internalformat, width, height, border, format, type, internalformat == DEPTH_COMPONENT ? IntPtr.Zero : pixels);
            return true;
        }

        public void texParameterf(UInt32 target, UInt32 pname, Single value)
        {
            this.makeContextCurrent();
            GLES.glTexParameterf(target, pname, value);
        }

        public void texParameteri(UInt32 target, UInt32 pname, Int32 value)
        {
            this.makeContextCurrent();
            GLES.glTexParameteri(target, pname, value);
        }

        public void texSubImage2D(UInt32 target, Int32 level, Int32 xoff, Int32 yoff, Int32 width, Int32 height, UInt32 format, UInt32 type, IntPtr pixels)
        {
            this.makeContextCurrent();
            GLES.glTexSubImage2D(target, level, xoff, yoff, width, height, format, type, pixels);
        }

        public void uniform1f(Int32 location, Single x)
        {
            this.makeContextCurrent();
            GLES.glUniform1f(location, x);
        }

        public void uniform1fv(Int32 location, Int32 count, IntPtr v)
        {
            this.makeContextCurrent();
            GLES.glUniform1fv(location, count, v);
        }

        public void uniform1i(Int32 location, Int32 x)
        {
            this.makeContextCurrent();
            GLES.glUniform1i(location, x);
        }

        public void uniform1iv(Int32 location, Int32 count, IntPtr v)
        {
            this.makeContextCurrent();
            GLES.glUniform1iv(location, count, v);
        }

        public void uniform2f(Int32 location, Single x, Single y)
        {
            this.makeContextCurrent();
            GLES.glUniform2f(location, x, y);
        }

        public void uniform2fv(Int32 location, Int32 count, IntPtr v)
        {
            this.makeContextCurrent();
            GLES.glUniform2fv(location, count, v);
        }

        public void uniform2i(Int32 location, Int32 x, Int32 y)
        {
            this.makeContextCurrent();
            GLES.glUniform2i(location, x, y);
        }

        public void uniform2iv(Int32 location, Int32 count, IntPtr v)
        {
            this.makeContextCurrent();
            GLES.glUniform2iv(location, count, v);
        }

        public void uniform3f(Int32 location, Single x, Single y, Single z)
        {
            this.makeContextCurrent();
            GLES.glUniform3f(location, x, y, z);
        }

        public void uniform3fv(Int32 location, Int32 count, IntPtr v)
        {
            this.makeContextCurrent();
            GLES.glUniform3fv(location, count, v);
        }

        public void uniform3i(Int32 location, Int32 x, Int32 y, Int32 z)
        {
            this.makeContextCurrent();
            GLES.glUniform3i(location, x, y, z);
        }

        public void uniform3iv(Int32 location, Int32 count, IntPtr v)
        {
            this.makeContextCurrent();
            GLES.glUniform3iv(location, count, v);
        }

        public void uniform4f(Int32 location, Single x, Single y, Single z, Single w)
        {
            this.makeContextCurrent();
            GLES.glUniform4f(location, x, y, z, w);
        }

        public void uniform4fv(Int32 location, Int32 count, IntPtr v)
        {
            this.makeContextCurrent();
            GLES.glUniform4fv(location, count, v);
        }

        public void uniform4i(Int32 location, Int32 x, Int32 y, Int32 z, Int32 w)
        {
            this.makeContextCurrent();
            GLES.glUniform4i(location, x, y, z, w);
        }

        public void uniform4iv(Int32 location, Int32 count, IntPtr v)
        {
            this.makeContextCurrent();
            GLES.glUniform4iv(location, count, v);
        }

        public void uniformMatrix2fv(GLint location, GLsizei size, GLboolean transpose, IntPtr value)
        {
            this.makeContextCurrent();
            GLES.glUniformMatrix2fv(location, size, Convert.ToByte(transpose), value);
        }

        public void uniformMatrix3fv(GLint location, GLsizei size, GLboolean transpose, IntPtr value)
        {
            this.makeContextCurrent();
            GLES.glUniformMatrix3fv(location, size, Convert.ToByte(transpose), value);
        }

        public void uniformMatrix4fv(GLint location, GLsizei size, GLboolean transpose, IntPtr value)
        {
            this.makeContextCurrent();
            GLES.glUniformMatrix4fv(location, size, Convert.ToByte(transpose), value);
        }

        public void useProgram(Platform3DObject program)
        {
            this.makeContextCurrent();
            GLES.glUseProgram(program);
        }

        public void validateProgram(Platform3DObject program)
        {
            this.makeContextCurrent();
            GLES.glValidateProgram(program);
        }

        public void vertexAttrib1f(UInt32 index, Single x)
        {
            this.makeContextCurrent();
            GLES.glVertexAttrib1f(index, x);
        }

        public void vertexAttrib1fv(UInt32 index, IntPtr values)
        {
            this.makeContextCurrent();
            GLES.glVertexAttrib1fv(index, values);
        }

        public void vertexAttrib2f(UInt32 index, Single x, Single y)
        {
            this.makeContextCurrent();
            GLES.glVertexAttrib2f(index, x, y);
        }

        public void vertexAttrib2fv(UInt32 index, IntPtr values)
        {
            this.makeContextCurrent();
            GLES.glVertexAttrib2fv(index, values);
        }

        public void vertexAttrib3f(UInt32 index, Single x, Single y, Single z)
        {
            this.makeContextCurrent();
            GLES.glVertexAttrib3f(index, x, y, z);
        }

        public void vertexAttrib3fv(UInt32 index, IntPtr values)
        {
            this.makeContextCurrent();
            GLES.glVertexAttrib3fv(index, values);
        }

        public void vertexAttrib4f(UInt32 index, Single x, Single y, Single z, Single w)
        {
            this.makeContextCurrent();
            GLES.glVertexAttrib4f(index, x, y, z, w);
        }

        public void vertexAttrib4fv(UInt32 index, IntPtr values)
        {
            this.makeContextCurrent();
            GLES.glVertexAttrib4fv(index, values);
        }

        public void vertexAttribPointer(GLuint index, GLint size, GLenum type, GLboolean normalized, GLsizei stride, GLintptr offset)
        {
            this.makeContextCurrent();
            GLES.glVertexAttribPointer(index, size, type, Convert.ToByte(normalized), stride, new IntPtr(offset));
        }

        public void viewport(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            this.makeContextCurrent();
            GLES.glViewport(x, y, width, height);
        }

        public void swapBuffers()
        {
            EGL.eglSwapBuffers(this.m_display, this.m_surface);
        }

        public void verticalSync(bool on)
        {
            EGL.eglSwapInterval(this.m_display, Convert.ToInt32(on));
        }

        public bool makeContextCurrent()
        {
            if (currentContext != this)
            {
                currentContext = this;
                return EGL.eglMakeCurrent(this.m_display, this.m_surface, this.m_surface, this.m_context) == EGL.EGL_TRUE;
            }
            return true;
        }

        public Attributes getContextAttributes()
        {
            return this.m_attrs;
        }

        public String getShaderSource(Platform3DObject shader)
        {
            return this.m_shaderSourceMap[shader] ?? String.Empty;
        }

        public String getShaderInfoLog(Platform3DObject shader)
        {
            this.makeContextCurrent();
            int length;
            GLES.glGetShaderiv(shader, GLES.GL_INFO_LOG_LENGTH, out length);
            if (length != 0)
            {
                int size;
                var info = new StringBuilder(length);
                GLES.glGetShaderInfoLog(shader, length, out size, info);
                return info.ToString();
            }
            return String.Empty;
        }

        public bool texImage2DResourceSafe(UInt32 target, Int32 level, UInt32 internalformat, Int32 width, Int32 height, Int32 border, UInt32 format, UInt32 type, Int32 unpackAlignment = 4)
        {
            var zero = new byte[0];
            if (width > 0 && height > 0)
            {
                int size;
                int ignore;
                var error = computeImageSizeInBytes(format, type, width, height, unpackAlignment, out size, out ignore);
                if (error != NO_ERROR)
                {
                    this.synthesizeGLError(error);
                    return false;
                }
                zero = new byte[size];
            }
            var handle = GCHandle.Alloc(zero, GCHandleType.Pinned);
            var result = this.texImage2D(target, level, internalformat, width, height, border, format, type, handle.AddrOfPinnedObject());
            handle.Free();
            return result;
        }

        public void reshape(int width, int height)
        {
            if (width != this.m_currentWidth || height != this.m_currentHeight)
            {
                var ints = new int[4];
                var floats = new float[4];
                var bools = new byte[4];

                this.m_currentWidth = width;
                this.m_currentHeight = height;

                this.makeContextCurrent();
                this.validateAttributes();

                var clearColor = new[] {0f, 0f, 0f, 0f};
                var clearDepth = 0f;
                var clearStencil = 0;
                var colorMask = new byte[] {1, 1, 1, 1};
                var depthMask = true;
                var stencilMask = 0xffffffff;
                var clearMask = GLES.GL_COLOR_BUFFER_BIT;
                GLES.glGetFloatv(GLES.GL_COLOR_CLEAR_VALUE, clearColor);
                GLES.glClearColor(0, 0, 0, 0);
                GLES.glGetBooleanv(GLES.GL_COLOR_WRITEMASK, colorMask);
                GLES.glColorMask(1, 1, 1, 1);

                if (this.m_attrs.depth)
                {
                    GLES.glGetFloatv(GLES.GL_DEPTH_CLEAR_VALUE, floats);
                    clearDepth = floats[0];
                    this.clearDepth(1);
                    GLES.glGetBooleanv(GLES.GL_DEPTH_WRITEMASK, bools);
                    depthMask = bools[0] == GLES.GL_TRUE;
                    GLES.glDepthMask(1);
                    clearMask |= GLES.GL_DEPTH_BUFFER_BIT;
                }

                if (this.m_attrs.stencil)
                {
                    GLES.glGetIntegerv(GLES.GL_STENCIL_CLEAR_VALUE, ints);
                    clearStencil = ints[0];
                    GLES.glClearStencil(0);
                    GLES.glGetIntegerv(GLES.GL_STENCIL_WRITEMASK, ints);
                    stencilMask = (uint)ints[0];
                    GLES.glStencilMaskSeparate(GLES.GL_FRONT, 0xffffffff);
                    clearMask |= GLES.GL_STENCIL_BUFFER_BIT;
                }

                var isScissorEnabled = GLES.glIsEnabled(GLES.GL_SCISSOR_TEST) == GLES.GL_TRUE;

                GLES.glDisable(GLES.GL_SCISSOR_TEST);

                var isDitherEnabled = GLES.glIsEnabled(GLES.GL_DITHER) == GLES.GL_TRUE;
                GLES.glDisable(GLES.GL_DITHER);

                GLES.glClear(clearMask);

                GLES.glClearColor(clearColor[0], clearColor[1], clearColor[2], clearColor[3]);
                GLES.glColorMask(colorMask[0], colorMask[1], colorMask[2], colorMask[3]);

                if (this.m_attrs.depth)
                {
                    this.clearDepth(clearDepth);
                    GLES.glDepthMask(Convert.ToByte(depthMask));
                }

                if (this.m_attrs.stencil)
                {
                    GLES.glClearStencil(clearStencil);
                    GLES.glStencilMaskSeparate(GLES.GL_FRONT, stencilMask);
                }

                if (isScissorEnabled)
                {
                    GLES.glEnable(GLES.GL_SCISSOR_TEST);
                }
                else
                {
                    GLES.glDisable(GLES.GL_SCISSOR_TEST);
                }

                if (isDitherEnabled)
                {
                    GLES.glEnable(GLES.GL_DITHER);
                }
                else
                {
                    GLES.glDisable(GLES.GL_DITHER);
                }

                GLES.glFlush();
            }
        }

        public Platform3DObject createBuffer()
        {
            this.makeContextCurrent();
            var o = new uint[1];
            GLES.glGenBuffers(1, o);
            return o[0];
        }

        public Platform3DObject createFramebuffer()
        {
            this.makeContextCurrent();
            var o = new uint[1];
            GLES.glGenFramebuffers(1, o);
            return o[0];
        }

        public Platform3DObject createProgram()
        {
            this.makeContextCurrent();
            return GLES.glCreateProgram();
        }

        public Platform3DObject createRenderbuffer()
        {
            this.makeContextCurrent();
            var o = new uint[1];
            GLES.glGenRenderbuffers(1, o);
            return o[0];
        }

        public Platform3DObject createShader(UInt32 type)
        {
            this.makeContextCurrent();
            return GLES.glCreateShader(type);
        }

        public Platform3DObject createTexture()
        {
            this.makeContextCurrent();
            var o = new uint[1];
            GLES.glGenTextures(1, o);
            return o[0];
        }

        public void deleteBuffer(Platform3DObject buffer)
        {
            this.deleteBuffers(1, new uint[] {buffer});
        }

        public void deleteFramebuffer(Platform3DObject framebuffer)
        {
            if (framebuffer == this.m_state.boundFBO)
            {
                this.bindFramebuffer(FRAMEBUFFER, 0);
            }
            this.deleteFramebuffers(1, new uint[] {framebuffer});
        }

        public void deleteProgram(Platform3DObject program)
        {
            this.makeContextCurrent();
            GLES.glDeleteProgram(program);
        }

        public void deleteRenderbuffer(Platform3DObject renderbuffer)
        {
            this.deleteRenderbuffers(1, new uint[] {renderbuffer});
        }

        public void deleteTexture(Platform3DObject texture)
        {
            this.deleteTextures(1, new uint[] {texture});
        }

        public Extensions3D getExtensions()
        {
            return this.m_extensions ?? (this.m_extensions = new Extensions3D(this));
        }

        public Size getInternalFramebufferSize()
        {
            return new Size(this.m_currentWidth, this.m_currentHeight);
        }

        public static UInt32 computeImageSizeInBytes(UInt32 format, UInt32 type, Int32 width, Int32 height, Int32 alignment, out int imageSizeInBytes, out int paddingInBytes)
        {
            imageSizeInBytes = 0;
            paddingInBytes = 0;

            if (width < 0 || height < 0)
            {
                return INVALID_VALUE;
            }
            int bytesPerComponent;
            int componentsPerPixel;
            if (!DataFormat.computeFormatAndTypeParameters(format, type, out bytesPerComponent, out componentsPerPixel))
            {
                return INVALID_ENUM;
            }
            if (width == 0 || height == 0)
            {
                imageSizeInBytes = 0;
                paddingInBytes = 0;
                return NO_ERROR;
            }
            long checkedValue = bytesPerComponent * componentsPerPixel;
            checkedValue *= width;
            if (checkedValue > uint.MaxValue)
            {
                return INVALID_VALUE;
            }
            var validRowSize = (uint)checkedValue;
            uint padding = 0;
            var residual = (uint)(validRowSize % alignment);
            if (residual != 0)
            {
                padding = (uint)(alignment - residual);
                checkedValue += padding;
            }
            // Last row needs no padding.
            checkedValue *= (height - 1);
            checkedValue += validRowSize;
            if (checkedValue > uint.MaxValue)
            {
                return INVALID_VALUE;
            }
            imageSizeInBytes = (int)checkedValue;
            if (paddingInBytes != 0)
            {
                paddingInBytes = (int)padding;
            }
            return NO_ERROR;
        }

        public static bool extractImageData(ImageData imageData, UInt32 format, UInt32 type, bool flipY, bool premultiplyAlpha, out byte[] data)
        {
            data = null;
            if (imageData == null)
            {
                return false;
            }
            var width = imageData.width();
            var height = imageData.height();

            int paddingInBytes;
            int packedSize;
            if (computeImageSizeInBytes(format, type, width, height, 1, out packedSize, out paddingInBytes) != GLES.GL_NO_ERROR)
            {
                return false;
            }
            data = new byte[packedSize];

            if (!packPixels(imageData.data().buffer.data, DataFormat.RGBA8, width, height, 0, (int)format, (int)type, data))
            {
                return false;
            }
            if (flipY)
            {
                int componentsPerPixel;
                int bytesPerComponent;
                if (!DataFormat.computeFormatAndTypeParameters(format, type, out componentsPerPixel, out bytesPerComponent))
                {
                    return false;
                }
                flipVertically(data, width, height, componentsPerPixel * bytesPerComponent, 1);
            }
            return true;
        }

        public static bool extractTextureData(int width, int height, UInt32 format, UInt32 type, int unpackAlignment, bool flipY, bool premultiplyAlpha, byte[] pixels, out byte[] data)
        {
            // Assumes format, type, etc. have already been validated.
            var sourceDataFormat = DataFormat.RGBA8;
            switch (type)
            {
                case GLES.GL_UNSIGNED_BYTE:
                    switch (format)
                    {
                        case GLES.GL_RGBA:
                            sourceDataFormat = DataFormat.RGBA8;
                            break;
                        case GLES.GL_RGB:
                            sourceDataFormat = DataFormat.RGB8;
                            break;
                        case GLES.GL_ALPHA:
                            sourceDataFormat = DataFormat.A8;
                            break;
                        case GLES.GL_LUMINANCE:
                            sourceDataFormat = DataFormat.R8;
                            break;
                        case GLES.GL_LUMINANCE_ALPHA:
                            sourceDataFormat = DataFormat.RA8;
                            break;
                        default:
                            // ASSERT_NOT_REACHED();
                            break;
                    }
                    break;
                case GLES.GL_FLOAT: // OES_texture_float
                    switch (format)
                    {
                        case GLES.GL_RGBA:
                            sourceDataFormat = DataFormat.RGBA32F;
                            break;
                        case GLES.GL_RGB:
                            sourceDataFormat = DataFormat.RGB32F;
                            break;
                        case GLES.GL_ALPHA:
                            sourceDataFormat = DataFormat.A32F;
                            break;
                        case GLES.GL_LUMINANCE:
                            sourceDataFormat = DataFormat.R32F;
                            break;
                        case GLES.GL_LUMINANCE_ALPHA:
                            sourceDataFormat = DataFormat.RA32F;
                            break;
                        default:
                            // ASSERT_NOT_REACHED();
                            break;
                    }
                    break;
                case GLES.GL_UNSIGNED_SHORT_5_5_5_1:
                    sourceDataFormat = DataFormat.RGBA5551;
                    break;
                case GLES.GL_UNSIGNED_SHORT_4_4_4_4:
                    sourceDataFormat = DataFormat.RGBA4444;
                    break;
                case GLES.GL_UNSIGNED_SHORT_5_6_5:
                    sourceDataFormat = DataFormat.RGB565;
                    break;
                default:
                    // ASSERT_NOT_REACHED();
                    break;
            }

            // Resize the output buffer.
            int componentsPerPixel;
            int bytesPerComponent;
            if (!DataFormat.computeFormatAndTypeParameters(format, type, out componentsPerPixel, out bytesPerComponent))
            {
                data = null;
                return false;
            }
            var bytesPerPixel = componentsPerPixel * bytesPerComponent;
            data = new byte[width * height * bytesPerPixel];

            if (!packPixels(pixels, sourceDataFormat, width, height, unpackAlignment, (int)format, (int)type, data))
            {
                return false;
            }
            if (flipY)
            {
                flipVertically(data, width, height, bytesPerPixel, 1);
            }
            return true;
        }

        public static void flipVertically(byte[] imageData, int width, int height, int bytesPerPixel, int unpackAlignment)
        {
            if (width == 0 || height == 0)
            {
                return;
            }
            var validRowBytes = width * bytesPerPixel;
            var totalRowBytes = validRowBytes;
            var remainder = validRowBytes % unpackAlignment;
            if (remainder != 0)
            {
                totalRowBytes += (unpackAlignment - remainder);
            }
            var tempRow = new byte[validRowBytes];
            for (var i = 0; i < height / 2; i++)
            {
                var lowRow = (totalRowBytes * i);
                var highRow = (totalRowBytes * (height - i - 1));
                Buffer.BlockCopy(imageData, lowRow, tempRow, 0, validRowBytes);
                Buffer.BlockCopy(imageData, highRow, imageData, lowRow, validRowBytes);
                Buffer.BlockCopy(tempRow, 0, imageData, highRow, validRowBytes);
            }
        }

        public void synthesizeGLError(GLenum error)
        {
            if (!this.m_syntheticErrors.Contains(error))
            {
                this.m_syntheticErrors.Add(error);
            }
        }

        public bool isGLES2Compliant
        {
            get { return true; }
        }

        public void vertexAttribDivisor(uint index, uint divisor)
        {
            this.getExtensions().vertexAttribDivisor(index, divisor);
        }

        public void getNonBuiltInActiveSymbolCount(Platform3DObject program, GLenum pname, out GLint value)
        {
            this.makeContextCurrent();

            ActiveShaderSymbolCounts result;
            this.m_shaderProgramSymbolCountMap.TryGetValue(program, out result);
            if (result != null)
            {
                value = result.countForType(pname);
                return;
            }

            this.m_shaderProgramSymbolCountMap[program] = new ActiveShaderSymbolCounts();
            var symbolCounts = this.m_shaderProgramSymbolCountMap[program];
            var info = new ActiveInfo();

            int attributeCount;
            GLES.glGetProgramiv(program, ACTIVE_ATTRIBUTES, out attributeCount);
            for (var i = 0; i < attributeCount; ++i)
            {
                this.getActiveAttribImpl(program, (uint)i, info);
                if (info.name.StartsWith("gl_"))
                {
                    continue;
                }

                symbolCounts.filteredToActualAttributeIndexMap.Add(i);
            }

            int uniformCount;
            GLES.glGetProgramiv(program, ACTIVE_UNIFORMS, out uniformCount);
            for (var i = 0; i < uniformCount; ++i)
            {
                this.getActiveUniformImpl(program, (uint)i, info);
                if (info.name.StartsWith("gl_"))
                {
                    continue;
                }

                symbolCounts.filteredToActualUniformIndexMap.Add(i);
            }

            value = symbolCounts.countForType(pname);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.m_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any other managed objects here. 
            }

            this.makeContextCurrent();

            EGL.eglDestroyContext(this.m_display, this.m_context);
            EGL.eglDestroySurface(this.m_display, this.m_surface);
            User32.ReleaseDC(this.m_hostWindow, this.m_hdc);
            this.m_disposed = true;
        }

        ~GraphicsContext3D()
        {
            this.Dispose(false);
        }

        private void validateAttributes()
        {
            this.validateDepthStencil("GL_OES_packed_depth_stencil");

            if (this.m_attrs.antialias)
            {
                var extensions = this.getExtensions();
                if (!extensions.supports("GL_IMG_multisampled_render_to_texture"))
                {
                    this.m_attrs.antialias = false;
                }
            }
        }

        private void validateDepthStencil(String packedDepthStencilExtension)
        {
            var extensions = this.getExtensions();
            if (this.m_attrs.stencil)
            {
                if (extensions.supports(packedDepthStencilExtension))
                {
                    extensions.ensureEnabled(packedDepthStencilExtension);
                    this.m_attrs.depth = true;
                }
                else
                {
                    this.m_attrs.stencil = false;
                }
            }
            if (this.m_attrs.antialias)
            {
                this.m_attrs.antialias = false;
            }
        }

        private static bool packPixels(byte[] sourceData, int sourceDataFormat, int width, int height, int sourceUnpackAlignment, int destinationFormat, int destinationType, byte[] destinationData)
        {
            var validSrc = (int)(width * DataFormat.texelBytesForFormat(sourceDataFormat));
            var remainder = sourceUnpackAlignment != 0 ? (validSrc % sourceUnpackAlignment) : 0;
            var srcStride = remainder != 0 ? (validSrc + sourceUnpackAlignment - remainder) : validSrc;

            var dstDataFormat = DataFormat.getDataFormat((uint)destinationFormat, (uint)destinationType);
            var dstStride = (int)(width * DataFormat.texelBytesForFormat(dstDataFormat));

            var ptr = 0;
            var dst = 0;
            var rowSize = (dstStride > 0) ? dstStride : -dstStride;
            var rows = height;
            while (rows-- > 0)
            {
                Buffer.BlockCopy(sourceData, ptr, destinationData, dst, rowSize);
                ptr += srcStride;
                dst += dstStride;
            }
            return true;
        }
    }

    // ReSharper restore InconsistentNaming
}
