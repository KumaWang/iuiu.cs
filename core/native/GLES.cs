using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using GLchar = System.Byte;
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
using GLintptr = System.Int64;
using GLsizeiptr = System.Int64;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    [SuppressUnmanagedCodeSecurity]
    static class GLES
    {
        /* OpenGL ES core versions */
        public const GLshort GL_ES_VERSION_2_0 = 1;

        /* ClearBufferMask */
        public const GLshort GL_DEPTH_BUFFER_BIT = 0x00000100;
        public const GLshort GL_STENCIL_BUFFER_BIT = 0x00000400;
        public const GLshort GL_COLOR_BUFFER_BIT = 0x00004000;

        /* Boolean */
        public const GLshort GL_FALSE = 0;
        public const GLshort GL_TRUE = 1;

        /* BeginMode */
        public const GLshort GL_POINTS = 0x0000;
        public const GLshort GL_LINES = 0x0001;
        public const GLshort GL_LINE_LOOP = 0x0002;
        public const GLshort GL_LINE_STRIP = 0x0003;
        public const GLshort GL_TRIANGLES = 0x0004;
        public const GLshort GL_TRIANGLE_STRIP = 0x0005;
        public const GLshort GL_TRIANGLE_FAN = 0x0006;

        /* BlendingFactorDest */
        public const GLshort GL_ZERO = 0;
        public const GLshort GL_ONE = 1;
        public const GLshort GL_SRC_COLOR = 0x0300;
        public const GLshort GL_ONE_MINUS_SRC_COLOR = 0x0301;
        public const GLshort GL_SRC_ALPHA = 0x0302;
        public const GLshort GL_ONE_MINUS_SRC_ALPHA = 0x0303;
        public const GLshort GL_DST_ALPHA = 0x0304;
        public const GLshort GL_ONE_MINUS_DST_ALPHA = 0x0305;

        /* BlendingFactorSrc */
        public const GLshort GL_DST_COLOR = 0x0306;
        public const GLshort GL_ONE_MINUS_DST_COLOR = 0x0307;
        public const GLshort GL_SRC_ALPHA_SATURATE = 0x0308;

        /* BlendEquationSeparate */
        public const GLshort GL_FUNC_ADD = 0x8006;
        public const GLshort GL_BLEND_EQUATION = 0x8009;
        public const GLshort GL_BLEND_EQUATION_RGB = 0x8009; /* same as BLEND_EQUATION */
        public const GLshort GL_BLEND_EQUATION_ALPHA = 0x883D;

        /* BlendSubtract */
        public const GLshort GL_FUNC_SUBTRACT = 0x800A;
        public const GLshort GL_FUNC_REVERSE_SUBTRACT = 0x800B;

        /* Separate Blend Functions */
        public const GLshort GL_BLEND_DST_RGB = 0x80C8;
        public const GLshort GL_BLEND_SRC_RGB = 0x80C9;
        public const GLshort GL_BLEND_DST_ALPHA = 0x80CA;
        public const GLshort GL_BLEND_SRC_ALPHA = 0x80CB;
        public const GLshort GL_CONSTANT_COLOR = 0x8001;
        public const GLshort GL_ONE_MINUS_CONSTANT_COLOR = 0x8002;
        public const GLshort GL_CONSTANT_ALPHA = 0x8003;
        public const GLshort GL_ONE_MINUS_CONSTANT_ALPHA = 0x8004;
        public const GLshort GL_BLEND_COLOR = 0x8005;

        /* Buffer Objects */
        public const GLshort GL_ARRAY_BUFFER = 0x8892;
        public const GLshort GL_ELEMENT_ARRAY_BUFFER = 0x8893;
        public const GLshort GL_ARRAY_BUFFER_BINDING = 0x8894;
        public const GLshort GL_ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;

        public const GLshort GL_STREAM_DRAW = 0x88E0;
        public const GLshort GL_STATIC_DRAW = 0x88E4;
        public const GLshort GL_DYNAMIC_DRAW = 0x88E8;

        public const GLshort GL_BUFFER_SIZE = 0x8764;
        public const GLshort GL_BUFFER_USAGE = 0x8765;

        public const GLshort GL_CURRENT_VERTEX_ATTRIB = 0x8626;

        /* CullFaceMode */
        public const GLshort GL_FRONT = 0x0404;
        public const GLshort GL_BACK = 0x0405;
        public const GLshort GL_FRONT_AND_BACK = 0x0408;

        /* EnableCap */
        public const GLshort GL_TEXTURE_2D = 0x0DE1;
        public const GLshort GL_CULL_FACE = 0x0B44;
        public const GLshort GL_BLEND = 0x0BE2;
        public const GLshort GL_DITHER = 0x0BD0;
        public const GLshort GL_STENCIL_TEST = 0x0B90;
        public const GLshort GL_DEPTH_TEST = 0x0B71;
        public const GLshort GL_SCISSOR_TEST = 0x0C11;
        public const GLshort GL_POLYGON_OFFSET_FILL = 0x8037;
        public const GLshort GL_SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
        public const GLshort GL_SAMPLE_COVERAGE = 0x80A0;

        /* ErrorCode */
        public const GLshort GL_NO_ERROR = 0;
        public const GLshort GL_INVALID_ENUM = 0x0500;
        public const GLshort GL_INVALID_VALUE = 0x0501;
        public const GLshort GL_INVALID_OPERATION = 0x0502;
        public const GLshort GL_OUT_OF_MEMORY = 0x0505;

        /* FrontFaceDirection */
        public const GLshort GL_CW = 0x0900;
        public const GLshort GL_CCW = 0x0901;

        /* GetPName */
        public const GLshort GL_LINE_WIDTH = 0x0B21;
        public const GLshort GL_ALIASED_POINT_SIZE_RANGE = 0x846D;
        public const GLshort GL_ALIASED_LINE_WIDTH_RANGE = 0x846E;
        public const GLshort GL_CULL_FACE_MODE = 0x0B45;
        public const GLshort GL_FRONT_FACE = 0x0B46;
        public const GLshort GL_DEPTH_RANGE = 0x0B70;
        public const GLshort GL_DEPTH_WRITEMASK = 0x0B72;
        public const GLshort GL_DEPTH_CLEAR_VALUE = 0x0B73;
        public const GLshort GL_DEPTH_FUNC = 0x0B74;
        public const GLshort GL_STENCIL_CLEAR_VALUE = 0x0B91;
        public const GLshort GL_STENCIL_FUNC = 0x0B92;
        public const GLshort GL_STENCIL_FAIL = 0x0B94;
        public const GLshort GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95;
        public const GLshort GL_STENCIL_PASS_DEPTH_PASS = 0x0B96;
        public const GLshort GL_STENCIL_REF = 0x0B97;
        public const GLshort GL_STENCIL_VALUE_MASK = 0x0B93;
        public const GLshort GL_STENCIL_WRITEMASK = 0x0B98;
        public const GLshort GL_STENCIL_BACK_FUNC = 0x8800;
        public const GLshort GL_STENCIL_BACK_FAIL = 0x8801;
        public const GLshort GL_STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;
        public const GLshort GL_STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;
        public const GLshort GL_STENCIL_BACK_REF = 0x8CA3;
        public const GLshort GL_STENCIL_BACK_VALUE_MASK = 0x8CA4;
        public const GLshort GL_STENCIL_BACK_WRITEMASK = 0x8CA5;
        public const GLshort GL_VIEWPORT = 0x0BA2;
        public const GLshort GL_SCISSOR_BOX = 0x0C10;

        /*      GL_SCISSOR_TEST */
        public const GLshort GL_COLOR_CLEAR_VALUE = 0x0C22;
        public const GLshort GL_COLOR_WRITEMASK = 0x0C23;
        public const GLshort GL_UNPACK_ALIGNMENT = 0x0CF5;
        public const GLshort GL_PACK_ALIGNMENT = 0x0D05;
        public const GLshort GL_MAX_TEXTURE_SIZE = 0x0D33;
        public const GLshort GL_MAX_VIEWPORT_DIMS = 0x0D3A;
        public const GLshort GL_SUBPIXEL_BITS = 0x0D50;
        public const GLshort GL_RED_BITS = 0x0D52;
        public const GLshort GL_GREEN_BITS = 0x0D53;
        public const GLshort GL_BLUE_BITS = 0x0D54;
        public const GLshort GL_ALPHA_BITS = 0x0D55;
        public const GLshort GL_DEPTH_BITS = 0x0D56;
        public const GLshort GL_STENCIL_BITS = 0x0D57;
        public const GLshort GL_POLYGON_OFFSET_UNITS = 0x2A00;

        /*      GL_POLYGON_OFFSET_FILL */
        public const GLshort GL_POLYGON_OFFSET_FACTOR = 0x8038;
        public const GLshort GL_TEXTURE_BINDING_2D = 0x8069;
        public const GLshort GL_SAMPLE_BUFFERS = 0x80A8;
        public const GLshort GL_SAMPLES = 0x80A9;
        public const GLshort GL_SAMPLE_COVERAGE_VALUE = 0x80AA;
        public const GLshort GL_SAMPLE_COVERAGE_INVERT = 0x80AB;

        public const GLshort GL_NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;
        public const GLshort GL_COMPRESSED_TEXTURE_FORMATS = 0x86A3;

        /* HintMode */
        public const GLshort GL_DONT_CARE = 0x1100;
        public const GLshort GL_FASTEST = 0x1101;
        public const GLshort GL_NICEST = 0x1102;

        /* HintTarget */
        public const GLshort GL_GENERATE_MIPMAP_HINT = 0x8192;

        /* DataType */
        public const GLshort GL_BYTE = 0x1400;
        public const GLshort GL_UNSIGNED_BYTE = 0x1401;
        public const GLshort GL_SHORT = 0x1402;
        public const GLshort GL_UNSIGNED_SHORT = 0x1403;
        public const GLshort GL_INT = 0x1404;
        public const GLshort GL_UNSIGNED_INT = 0x1405;
        public const GLshort GL_FLOAT = 0x1406;
        public const GLshort GL_FIXED = 0x140C;

        /* PixelFormat */
        public const GLshort GL_DEPTH_COMPONENT = 0x1902;
        public const GLshort GL_ALPHA = 0x1906;
        public const GLshort GL_RGB = 0x1907;
        public const GLshort GL_RGBA = 0x1908;
        public const GLshort GL_LUMINANCE = 0x1909;
        public const GLshort GL_LUMINANCE_ALPHA = 0x190A;
        public const GLshort GL_DEPTH_STENCIL = 0x84F9;

        /* PixelType */
        public const GLshort GL_UNSIGNED_SHORT_4_4_4_4 = 0x8033;
        public const GLshort GL_UNSIGNED_SHORT_5_5_5_1 = 0x8034;
        public const GLshort GL_UNSIGNED_SHORT_5_6_5 = 0x8363;
        public const GLshort GL_UNSIGNED_INT_24_8 = 0x84FA;

        /* Shaders */
        public const GLshort GL_FRAGMENT_SHADER = 0x8B30;
        public const GLshort GL_VERTEX_SHADER = 0x8B31;
        public const GLshort GL_MAX_VERTEX_ATTRIBS = 0x8869;
        public const GLshort GL_MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB;
        public const GLshort GL_MAX_VARYING_VECTORS = 0x8DFC;
        public const GLshort GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;
        public const GLshort GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;
        public const GLshort GL_MAX_TEXTURE_IMAGE_UNITS = 0x8872;
        public const GLshort GL_MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD;
        public const GLshort GL_SHADER_TYPE = 0x8B4F;
        public const GLshort GL_DELETE_STATUS = 0x8B80;
        public const GLshort GL_LINK_STATUS = 0x8B82;
        public const GLshort GL_VALIDATE_STATUS = 0x8B83;
        public const GLshort GL_ATTACHED_SHADERS = 0x8B85;
        public const GLshort GL_ACTIVE_UNIFORMS = 0x8B86;
        public const GLshort GL_ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87;
        public const GLshort GL_ACTIVE_ATTRIBUTES = 0x8B89;
        public const GLshort GL_ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A;
        public const GLshort GL_SHADING_LANGUAGE_VERSION = 0x8B8C;
        public const GLshort GL_CURRENT_PROGRAM = 0x8B8D;

        /* StencilFunction */
        public const GLshort GL_NEVER = 0x0200;
        public const GLshort GL_LESS = 0x0201;
        public const GLshort GL_EQUAL = 0x0202;
        public const GLshort GL_LEQUAL = 0x0203;
        public const GLshort GL_GREATER = 0x0204;
        public const GLshort GL_NOTEQUAL = 0x0205;
        public const GLshort GL_GEQUAL = 0x0206;
        public const GLshort GL_ALWAYS = 0x0207;

        /* StencilOp */
        public const GLshort GL_KEEP = 0x1E00;
        public const GLshort GL_REPLACE = 0x1E01;
        public const GLshort GL_INCR = 0x1E02;
        public const GLshort GL_DECR = 0x1E03;
        public const GLshort GL_INVERT = 0x150A;
        public const GLshort GL_INCR_WRAP = 0x8507;
        public const GLshort GL_DECR_WRAP = 0x8508;

        /* StringName */
        public const GLshort GL_VENDOR = 0x1F00;
        public const GLshort GL_RENDERER = 0x1F01;
        public const GLshort GL_VERSION = 0x1F02;
        public const GLshort GL_EXTENSIONS = 0x1F03;

        /* TextureMagFilter */
        public const GLshort GL_NEAREST = 0x2600;
        public const GLshort GL_LINEAR = 0x2601;

        /* TextureMinFilter */
        public const GLshort GL_NEAREST_MIPMAP_NEAREST = 0x2700;
        public const GLshort GL_LINEAR_MIPMAP_NEAREST = 0x2701;
        public const GLshort GL_NEAREST_MIPMAP_LINEAR = 0x2702;
        public const GLshort GL_LINEAR_MIPMAP_LINEAR = 0x2703;

        /* TextureParameterName */
        public const GLshort GL_TEXTURE_MAG_FILTER = 0x2800;
        public const GLshort GL_TEXTURE_MIN_FILTER = 0x2801;
        public const GLshort GL_TEXTURE_WRAP_S = 0x2802;
        public const GLshort GL_TEXTURE_WRAP_T = 0x2803;

        /* TextureTarget */
        /*      GL_TEXTURE_2D */
        public const GLshort GL_TEXTURE = 0x1702;

        public const GLshort GL_TEXTURE_CUBE_MAP = 0x8513;
        public const GLshort GL_TEXTURE_BINDING_CUBE_MAP = 0x8514;
        public const GLshort GL_TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
        public const GLshort GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
        public const GLshort GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
        public const GLshort GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
        public const GLshort GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
        public const GLshort GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
        public const GLshort GL_MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;

        /* TextureUnit */
        public const GLshort GL_TEXTURE0 = 0x84C0;
        public const GLshort GL_TEXTURE1 = 0x84C1;
        public const GLshort GL_TEXTURE2 = 0x84C2;
        public const GLshort GL_TEXTURE3 = 0x84C3;
        public const GLshort GL_TEXTURE4 = 0x84C4;
        public const GLshort GL_TEXTURE5 = 0x84C5;
        public const GLshort GL_TEXTURE6 = 0x84C6;
        public const GLshort GL_TEXTURE7 = 0x84C7;
        public const GLshort GL_TEXTURE8 = 0x84C8;
        public const GLshort GL_TEXTURE9 = 0x84C9;
        public const GLshort GL_TEXTURE10 = 0x84CA;
        public const GLshort GL_TEXTURE11 = 0x84CB;
        public const GLshort GL_TEXTURE12 = 0x84CC;
        public const GLshort GL_TEXTURE13 = 0x84CD;
        public const GLshort GL_TEXTURE14 = 0x84CE;
        public const GLshort GL_TEXTURE15 = 0x84CF;
        public const GLshort GL_TEXTURE16 = 0x84D0;
        public const GLshort GL_TEXTURE17 = 0x84D1;
        public const GLshort GL_TEXTURE18 = 0x84D2;
        public const GLshort GL_TEXTURE19 = 0x84D3;
        public const GLshort GL_TEXTURE20 = 0x84D4;
        public const GLshort GL_TEXTURE21 = 0x84D5;
        public const GLshort GL_TEXTURE22 = 0x84D6;
        public const GLshort GL_TEXTURE23 = 0x84D7;
        public const GLshort GL_TEXTURE24 = 0x84D8;
        public const GLshort GL_TEXTURE25 = 0x84D9;
        public const GLshort GL_TEXTURE26 = 0x84DA;
        public const GLshort GL_TEXTURE27 = 0x84DB;
        public const GLshort GL_TEXTURE28 = 0x84DC;
        public const GLshort GL_TEXTURE29 = 0x84DD;
        public const GLshort GL_TEXTURE30 = 0x84DE;
        public const GLshort GL_TEXTURE31 = 0x84DF;
        public const GLshort GL_ACTIVE_TEXTURE = 0x84E0;

        /* TextureWrapMode */
        public const GLshort GL_REPEAT = 0x2901;
        public const GLshort GL_CLAMP_TO_EDGE = 0x812F;
        public const GLshort GL_MIRRORED_REPEAT = 0x8370;

        /* Uniform Types */
        public const GLshort GL_FLOAT_VEC2 = 0x8B50;
        public const GLshort GL_FLOAT_VEC3 = 0x8B51;
        public const GLshort GL_FLOAT_VEC4 = 0x8B52;
        public const GLshort GL_INT_VEC2 = 0x8B53;
        public const GLshort GL_INT_VEC3 = 0x8B54;
        public const GLshort GL_INT_VEC4 = 0x8B55;
        public const GLshort GL_BOOL = 0x8B56;
        public const GLshort GL_BOOL_VEC2 = 0x8B57;
        public const GLshort GL_BOOL_VEC3 = 0x8B58;
        public const GLshort GL_BOOL_VEC4 = 0x8B59;
        public const GLshort GL_FLOAT_MAT2 = 0x8B5A;
        public const GLshort GL_FLOAT_MAT3 = 0x8B5B;
        public const GLshort GL_FLOAT_MAT4 = 0x8B5C;
        public const GLshort GL_SAMPLER_2D = 0x8B5E;
        public const GLshort GL_SAMPLER_CUBE = 0x8B60;

        /* Vertex Arrays */
        public const GLshort GL_VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;
        public const GLshort GL_VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;
        public const GLshort GL_VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;
        public const GLshort GL_VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;
        public const GLshort GL_VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;
        public const GLshort GL_VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;
        public const GLshort GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;

        /* Read Format */
        public const GLshort GL_IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A;
        public const GLshort GL_IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B;

        /* Shader Source */
        public const GLshort GL_COMPILE_STATUS = 0x8B81;
        public const GLshort GL_INFO_LOG_LENGTH = 0x8B84;
        public const GLshort GL_SHADER_SOURCE_LENGTH = 0x8B88;
        public const GLshort GL_SHADER_COMPILER = 0x8DFA;

        /* Shader Binary */
        public const GLshort GL_SHADER_BINARY_FORMATS = 0x8DF8;
        public const GLshort GL_NUM_SHADER_BINARY_FORMATS = 0x8DF9;

        /* Shader Precision-Specified Types */
        public const GLshort GL_LOW_FLOAT = 0x8DF0;
        public const GLshort GL_MEDIUM_FLOAT = 0x8DF1;
        public const GLshort GL_HIGH_FLOAT = 0x8DF2;
        public const GLshort GL_LOW_INT = 0x8DF3;
        public const GLshort GL_MEDIUM_INT = 0x8DF4;
        public const GLshort GL_HIGH_INT = 0x8DF5;

        /* Framebuffer Object. */
        public const GLshort GL_FRAMEBUFFER = 0x8D40;
        public const GLshort GL_RENDERBUFFER = 0x8D41;

        public const GLshort GL_RGBA4 = 0x8056;
        public const GLshort GL_RGB5_A1 = 0x8057;
        public const GLshort GL_RGB565 = 0x8D62;
        public const GLshort GL_DEPTH_COMPONENT16 = 0x81A5;
        public const GLshort GL_STENCIL_INDEX = 0x1901;
        public const GLshort GL_STENCIL_INDEX8 = 0x8D48;

        public const GLshort GL_RENDERBUFFER_WIDTH = 0x8D42;
        public const GLshort GL_RENDERBUFFER_HEIGHT = 0x8D43;
        public const GLshort GL_RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;
        public const GLshort GL_RENDERBUFFER_RED_SIZE = 0x8D50;
        public const GLshort GL_RENDERBUFFER_GREEN_SIZE = 0x8D51;
        public const GLshort GL_RENDERBUFFER_BLUE_SIZE = 0x8D52;
        public const GLshort GL_RENDERBUFFER_ALPHA_SIZE = 0x8D53;
        public const GLshort GL_RENDERBUFFER_DEPTH_SIZE = 0x8D54;
        public const GLshort GL_RENDERBUFFER_STENCIL_SIZE = 0x8D55;

        public const GLshort GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;
        public const GLshort GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;
        public const GLshort GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;
        public const GLshort GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;

        public const GLshort GL_COLOR_ATTACHMENT0 = 0x8CE0;
        public const GLshort GL_DEPTH_ATTACHMENT = 0x8D00;
        public const GLshort GL_STENCIL_ATTACHMENT = 0x8D20;
        public const GLshort GL_DEPTH_STENCIL_ATTACHMENT = 0x821A;

        public const GLshort GL_NONE = 0;

        public const GLshort GL_FRAMEBUFFER_COMPLETE = 0x8CD5;
        public const GLshort GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
        public const GLshort GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
        public const GLshort GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9;
        public const GLshort GL_FRAMEBUFFER_UNSUPPORTED = 0x8CDD;

        public const GLshort GL_FRAMEBUFFER_BINDING = 0x8CA6;
        public const GLshort GL_RENDERBUFFER_BINDING = 0x8CA7;
        public const GLshort GL_MAX_RENDERBUFFER_SIZE = 0x84E8;

        public const GLshort GL_INVALID_FRAMEBUFFER_OPERATION = 0x0506;
        public const GLshort GL_TRANSLATED_SHADER_SOURCE_LENGTH_ANGLE = 0x93A0;
        public const GLshort GL_DEPTH24_STENCIL8_OES = 0x88F0;

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glActiveTexture(GLenum texture);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glAttachShader(GLuint program, GLuint shader);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBindAttribLocation(GLuint program, GLuint index, String name);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBindBuffer(GLenum target, GLuint buffer);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBindFramebuffer(GLenum target, GLuint framebuffer);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBindRenderbuffer(GLenum target, GLuint renderbuffer);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBindTexture(GLenum target, GLuint texture);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBlendColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBlendEquation(GLenum mode);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBlendEquationSeparate(GLenum modeRGB, GLenum modeAlpha);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBlendFunc(GLenum sfactor, GLenum dfactor);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBlendFuncSeparate(GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBufferData(GLenum target, GLsizeiptr size, IntPtr data, GLenum usage);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glBufferSubData(GLenum target, GLintptr offset, GLsizeiptr size, IntPtr data);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLenum glCheckFramebufferStatus(GLenum target);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glClear(GLbitfield mask);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glClearColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glClearDepthf(GLclampf depth);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glClearStencil(GLint s);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glColorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glCompileShader(GLuint shader);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glCompressedTexImage2D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, IntPtr data);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glCompressedTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, IntPtr data);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glCopyTexImage2D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glCopyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLuint glCreateProgram();

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLuint glCreateShader(GLenum type);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glCullFace(GLenum mode);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDeleteBuffers(GLsizei n, GLuint[] buffers);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDeleteFramebuffers(GLsizei n, GLuint[] framebuffers);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDeleteProgram(GLuint program);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDeleteRenderbuffers(GLsizei n, GLuint[] renderbuffers);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDeleteShader(GLuint shader);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDeleteTextures(GLsizei n, GLuint[] textures);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDepthFunc(GLenum func);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDepthMask(GLboolean flag);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDepthRangef(GLclampf zNear, GLclampf zFar);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDetachShader(GLuint program, GLuint shader);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDisable(GLenum cap);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDisableVertexAttribArray(GLuint index);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDrawArrays(GLenum mode, GLint first, GLsizei count);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glDrawElements(GLenum mode, GLsizei count, GLenum type, IntPtr indices);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glEnable(GLenum cap);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glEnableVertexAttribArray(GLuint index);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glFinish();

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glFlush();

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glFramebufferRenderbuffer(GLenum target, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glFramebufferTexture(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glFrontFace(GLenum mode);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGenBuffers(GLsizei n, GLuint[] buffers);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGenerateMipmap(GLenum target);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGenFramebuffers(GLsizei n, GLuint[] framebuffers);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGenRenderbuffers(GLsizei n, GLuint[] renderbuffers);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGenTextures(GLsizei n, GLuint[] textures);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetActiveAttrib(GLuint program, GLuint index, GLsizei bufsize, out GLsizei length, out GLint size, out GLenum type, StringBuilder name);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetActiveUniform(GLuint program, GLuint index, GLsizei bufsize, out GLsizei length, out GLint size, out GLenum type, StringBuilder name);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetAttachedShaders(GLuint program, GLsizei maxcount, out GLsizei count, GLuint[] shaders);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern int glGetAttribLocation(GLuint program, String name);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetBooleanv(GLenum pname, GLboolean[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetBufferParameteriv(GLenum target, GLenum pname, out GLint @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLenum glGetError();

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetFloatv(GLenum pname, GLfloat[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetFramebufferAttachmentParameteriv(GLenum target, GLenum attachment, GLenum pname, out GLint @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetIntegerv(GLenum pname, GLint[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetProgramiv(GLuint program, GLenum pname, out GLint @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetProgramInfoLog(GLuint program, GLsizei bufsize, out GLsizei length, StringBuilder infolog);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetRenderbufferParameteriv(GLenum target, GLenum pname, out GLint @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetShaderiv(GLuint shader, GLenum pname, out GLint @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetShaderInfoLog(GLuint shader, GLsizei bufsize, out GLsizei length, StringBuilder infolog);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetShaderPrecisionFormat(GLenum shadertype, GLenum precisiontype, GLint[] range, out GLint precision);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetShaderSource(GLuint shader, GLsizei bufsize, out GLsizei length, StringBuilder source);

        [DllImport("libGLESv2.dll", EntryPoint = "glGetString", ExactSpelling = true)]
        private static extern IntPtr _glGetString(GLenum name);

        public static string glGetString(GLenum name)
        {
            return Marshal.PtrToStringAnsi(_glGetString(name));
        }

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetTexParameterfv(GLenum target, GLenum pname, GLfloat[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetTexParameteriv(GLenum target, GLenum pname, GLint[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetUniformfv(GLuint program, GLint location, GLfloat[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetUniformiv(GLuint program, GLint location, GLint[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern int glGetUniformLocation(GLuint program, String name);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetVertexAttribfv(GLuint index, GLenum pname, GLfloat[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetVertexAttribiv(GLuint index, GLenum pname, GLint[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glGetVertexAttribPointerv(GLuint index, GLenum pname, out IntPtr pointer);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glHint(GLenum target, GLenum mode);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLboolean glIsBuffer(GLuint buffer);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLboolean glIsEnabled(GLenum cap);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLboolean glIsFramebuffer(GLuint framebuffer);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLboolean glIsProgram(GLuint program);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLboolean glIsRenderbuffer(GLuint renderbuffer);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLboolean glIsShader(GLuint shader);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern GLboolean glIsTexture(GLuint texture);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glLineWidth(GLfloat width);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glLinkProgram(GLuint program);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glPixelStorei(GLenum pname, GLint param);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glPolygonOffset(GLfloat factor, GLfloat units);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glReadPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glReleaseShaderCompiler();

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glRenderbufferStorage(GLenum target, GLenum internalformat, GLsizei width, GLsizei height);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glSampleCoverage(GLclampf value, GLboolean invert);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glScissor(GLint x, GLint y, GLsizei width, GLsizei height);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glShaderSource(GLuint shader, GLsizei count, String[] @string, GLint[] length);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glStencilFunc(GLenum func, GLint @ref, GLuint mask);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glStencilFuncSeparate(GLenum face, GLenum func, GLint @ref, GLuint mask);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glStencilMask(GLuint mask);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glStencilMaskSeparate(GLenum face, GLuint mask);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glStencilOp(GLenum fail, GLenum zfail, GLenum zpass);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glStencilOpSeparate(GLenum face, GLenum fail, GLenum zfail, GLenum zpass);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glTexImage2D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, IntPtr pixels);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glTexParameteri(GLenum target, GLenum pname, GLint param);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glTexParameterf(GLenum target, GLenum pname, GLfloat param);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform1f(GLint location, GLfloat x);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform1fv(GLint location, GLsizei count, IntPtr v);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform1i(GLint location, GLint x);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform1iv(GLint location, GLsizei count, IntPtr v);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform2f(GLint location, GLfloat x, GLfloat y);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform2fv(GLint location, GLsizei count, IntPtr v);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform2i(GLint location, GLint x, GLint y);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform2iv(GLint location, GLsizei count, IntPtr v);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform3f(GLint location, GLfloat x, GLfloat y, GLfloat z);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform3fv(GLint location, GLsizei count, IntPtr v);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform3i(GLint location, GLint x, GLint y, GLint z);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform3iv(GLint location, GLsizei count, IntPtr v);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform4f(GLint location, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform4fv(GLint location, GLsizei count, IntPtr v);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform4i(GLint location, GLint x, GLint y, GLint z, GLint w);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniform4iv(GLint location, GLsizei count, IntPtr v);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniformMatrix2fv(GLint location, GLsizei count, GLboolean transpose, IntPtr value);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniformMatrix3fv(GLint location, GLsizei count, GLboolean transpose, IntPtr value);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUniformMatrix4fv(GLint location, GLsizei count, GLboolean transpose, IntPtr value);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glUseProgram(GLuint program);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glValidateProgram(GLuint program);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glVertexAttrib1f(GLuint index, GLfloat x);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glVertexAttrib1fv(GLuint index, IntPtr values);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glVertexAttrib2f(GLuint index, GLfloat x, GLfloat y);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glVertexAttrib2fv(GLuint index, IntPtr values);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glVertexAttrib3f(GLuint index, GLfloat x, GLfloat y, GLfloat z);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glVertexAttrib3fv(GLuint index, IntPtr values);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glVertexAttrib4f(GLuint index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glVertexAttrib4fv(GLuint index, IntPtr values);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glVertexAttribPointer(GLuint index, GLint size, GLenum type, GLboolean normalized, GLsizei stride, IntPtr ptr);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        public static extern void glViewport(GLint x, GLint y, GLsizei width, GLsizei height);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glBlitFramebufferANGLE(GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glTexImage3DOES(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, IntPtr pixels);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glGetProgramBinaryOES(GLuint program, GLsizei bufSize, out GLsizei length, out GLenum binaryFormat, IntPtr binary);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glProgramBinaryOES(GLuint program, GLenum binaryFormat, IntPtr binary, GLint length);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glDrawBuffersEXT(GLsizei n, GLenum[] bufs);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern IntPtr glGetProcAddress(String procname);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glBeginQueryEXT(GLenum target, GLuint id);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glDeleteFencesNV(GLsizei n, GLuint[] fences);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glDeleteQueriesEXT(GLsizei n, GLuint[] ids);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glDrawArraysInstancedANGLE(GLenum mode, GLint first, GLsizei count, GLsizei primcount);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glDrawElementsInstancedANGLE(GLenum mode, GLsizei count, GLenum type, IntPtr indices, GLsizei primcount);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glEndQueryEXT(GLenum target);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glFinishFenceNV(GLuint fence);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glGenFencesNV(GLsizei n, GLuint[] fences);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glGenQueriesEXT(GLsizei n, GLuint[] ids);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glGetFenceivNV(GLuint fence, GLenum pname, GLint[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern GLenum glGetGraphicsResetStatusEXT();

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glGetQueryivEXT(GLenum target, GLenum pname, GLint[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glGetQueryObjectuivEXT(GLuint id, GLenum pname, GLuint[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glGetTranslatedShaderSourceANGLE(GLuint shader, GLsizei bufsize, out GLsizei length, StringBuilder source);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glGetnUniformfvEXT(GLuint program, GLint location, GLsizei bufSize, GLfloat[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glGetnUniformivEXT(GLuint program, GLint location, GLsizei bufSize, GLint[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern GLboolean glIsFenceNV(GLuint fence);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern GLboolean glIsQueryEXT(GLuint id);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glReadnPixelsEXT(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, IntPtr data);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glRenderbufferStorageMultisampleANGLE(GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glSetFenceNV(GLuint fence, GLenum condition);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern GLboolean glTestFenceNV(GLuint fence);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glTexParameterfv(GLenum target, GLenum pname, GLfloat[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glTexParameteriv(GLenum target, GLenum pname, GLint[] @params);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glTexStorage2DEXT(GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height);

        [DllImport("libGLESv2.dll", ExactSpelling = true)]
        private static extern void glVertexAttribDivisorANGLE(GLuint index, GLuint divisor);
    }

    // ReSharper restore InconsistentNaming
}
