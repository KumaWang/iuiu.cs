using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

    class WebGLRenderingContext : CanvasRenderingContext
    {
        // ReSharper disable RedundantDefaultFieldInitializer

        public readonly GLenum DEPTH_BUFFER_BIT = 0x00000100;
        public readonly GLenum STENCIL_BUFFER_BIT = 0x00000400;
        public readonly GLenum COLOR_BUFFER_BIT = 0x00004000;

        public readonly GLenum POINTS;
        public readonly GLenum LINES = 0x0001;
        public readonly GLenum LINE_LOOP = 0x0002;
        public readonly GLenum LINE_STRIP = 0x0003;
        public readonly GLenum TRIANGLES = 0x0004;
        public readonly GLenum TRIANGLE_STRIP = 0x0005;
        public readonly GLenum TRIANGLE_FAN = 0x0006;

        public readonly GLenum ZERO;
        public readonly GLenum ONE = 1;
        public readonly GLenum SRC_COLOR = 0x0300;
        public readonly GLenum ONE_MINUS_SRC_COLOR = 0x0301;
        public readonly GLenum SRC_ALPHA = 0x0302;
        public readonly GLenum ONE_MINUS_SRC_ALPHA = 0x0303;
        public readonly GLenum DST_ALPHA = 0x0304;
        public readonly GLenum ONE_MINUS_DST_ALPHA = 0x0305;

        public readonly GLenum DST_COLOR = 0x0306;
        public readonly GLenum ONE_MINUS_DST_COLOR = 0x0307;
        public readonly GLenum SRC_ALPHA_SATURATE = 0x0308;

        public readonly GLenum FUNC_ADD = 0x8006;
        public readonly GLenum BLEND_EQUATION = 0x8009;
        public readonly GLenum BLEND_EQUATION_RGB = 0x8009;
        public readonly GLenum BLEND_EQUATION_ALPHA = 0x883D;

        public readonly GLenum FUNC_SUBTRACT = 0x800A;
        public readonly GLenum FUNC_REVERSE_SUBTRACT = 0x800B;

        public readonly GLenum BLEND_DST_RGB = 0x80C8;
        public readonly GLenum BLEND_SRC_RGB = 0x80C9;
        public readonly GLenum BLEND_DST_ALPHA = 0x80CA;
        public readonly GLenum BLEND_SRC_ALPHA = 0x80CB;
        public readonly GLenum CONSTANT_COLOR = 0x8001;
        public readonly GLenum ONE_MINUS_CONSTANT_COLOR = 0x8002;
        public readonly GLenum CONSTANT_ALPHA = 0x8003;
        public readonly GLenum ONE_MINUS_CONSTANT_ALPHA = 0x8004;
        public readonly GLenum BLEND_COLOR = 0x8005;

        public readonly GLenum ARRAY_BUFFER = 0x8892;
        public readonly GLenum ELEMENT_ARRAY_BUFFER = 0x8893;
        public readonly GLenum ARRAY_BUFFER_BINDING = 0x8894;
        public readonly GLenum ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;

        public readonly GLenum STREAM_DRAW = 0x88E0;
        public readonly GLenum STATIC_DRAW = 0x88E4;
        public readonly GLenum DYNAMIC_DRAW = 0x88E8;

        public readonly GLenum BUFFER_SIZE = 0x8764;
        public readonly GLenum BUFFER_USAGE = 0x8765;

        public readonly GLenum CURRENT_VERTEX_ATTRIB = 0x8626;

        public readonly GLenum FRONT = 0x0404;
        public readonly GLenum BACK = 0x0405;
        public readonly GLenum FRONT_AND_BACK = 0x0408;

        public readonly GLenum CULL_FACE = 0x0B44;
        public readonly GLenum BLEND = 0x0BE2;
        public readonly GLenum DITHER = 0x0BD0;
        public readonly GLenum STENCIL_TEST = 0x0B90;
        public readonly GLenum DEPTH_TEST = 0x0B71;
        public readonly GLenum SCISSOR_TEST = 0x0C11;
        public readonly GLenum POLYGON_OFFSET_FILL = 0x8037;
        public readonly GLenum SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
        public readonly GLenum SAMPLE_COVERAGE = 0x80A0;

        public readonly GLenum NO_ERROR;
        public readonly GLenum INVALID_ENUM = 0x0500;
        public readonly GLenum INVALID_VALUE = 0x0501;
        public readonly GLenum INVALID_OPERATION = 0x0502;
        public readonly GLenum OUT_OF_MEMORY = 0x0505;

        public readonly GLenum CW = 0x0900;
        public readonly GLenum CCW = 0x0901;

        public readonly GLenum LINE_WIDTH = 0x0B21;
        public readonly GLenum ALIASED_POINT_SIZE_RANGE = 0x846D;
        public readonly GLenum ALIASED_LINE_WIDTH_RANGE = 0x846E;
        public readonly GLenum CULL_FACE_MODE = 0x0B45;
        public readonly GLenum FRONT_FACE = 0x0B46;
        public readonly GLenum DEPTH_RANGE = 0x0B70;
        public readonly GLenum DEPTH_WRITEMASK = 0x0B72;
        public readonly GLenum DEPTH_CLEAR_VALUE = 0x0B73;
        public readonly GLenum DEPTH_FUNC = 0x0B74;
        public readonly GLenum STENCIL_CLEAR_VALUE = 0x0B91;
        public readonly GLenum STENCIL_FUNC = 0x0B92;
        public readonly GLenum STENCIL_FAIL = 0x0B94;
        public readonly GLenum STENCIL_PASS_DEPTH_FAIL = 0x0B95;
        public readonly GLenum STENCIL_PASS_DEPTH_PASS = 0x0B96;
        public readonly GLenum STENCIL_REF = 0x0B97;
        public readonly GLenum STENCIL_VALUE_MASK = 0x0B93;
        public readonly GLenum STENCIL_WRITEMASK = 0x0B98;
        public readonly GLenum STENCIL_BACK_FUNC = 0x8800;
        public readonly GLenum STENCIL_BACK_FAIL = 0x8801;
        public readonly GLenum STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;
        public readonly GLenum STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;
        public readonly GLenum STENCIL_BACK_REF = 0x8CA3;
        public readonly GLenum STENCIL_BACK_VALUE_MASK = 0x8CA4;
        public readonly GLenum STENCIL_BACK_WRITEMASK = 0x8CA5;
        public readonly GLenum VIEWPORT = 0x0BA2;
        public readonly GLenum SCISSOR_BOX = 0x0C10;

        public readonly GLenum COLOR_CLEAR_VALUE = 0x0C22;
        public readonly GLenum COLOR_WRITEMASK = 0x0C23;
        public readonly GLenum UNPACK_ALIGNMENT = 0x0CF5;
        public readonly GLenum PACK_ALIGNMENT = 0x0D05;
        public readonly GLenum MAX_TEXTURE_SIZE = 0x0D33;
        public readonly GLenum MAX_VIEWPORT_DIMS = 0x0D3A;
        public readonly GLenum SUBPIXEL_BITS = 0x0D50;
        public readonly GLenum RED_BITS = 0x0D52;
        public readonly GLenum GREEN_BITS = 0x0D53;
        public readonly GLenum BLUE_BITS = 0x0D54;
        public readonly GLenum ALPHA_BITS = 0x0D55;
        public readonly GLenum DEPTH_BITS = 0x0D56;
        public readonly GLenum STENCIL_BITS = 0x0D57;
        public readonly GLenum POLYGON_OFFSET_UNITS = 0x2A00;

        public readonly GLenum POLYGON_OFFSET_FACTOR = 0x8038;
        public readonly GLenum TEXTURE_BINDING_2D = 0x8069;
        public readonly GLenum SAMPLE_BUFFERS = 0x80A8;
        public readonly GLenum SAMPLES = 0x80A9;
        public readonly GLenum SAMPLE_COVERAGE_VALUE = 0x80AA;
        public readonly GLenum SAMPLE_COVERAGE_INVERT = 0x80AB;

        public readonly GLenum COMPRESSED_TEXTURE_FORMATS = 0x86A3;

        public readonly GLenum DONT_CARE = 0x1100;
        public readonly GLenum FASTEST = 0x1101;
        public readonly GLenum NICEST = 0x1102;

        public readonly GLenum GENERATE_MIPMAP_HINT = 0x8192;

        public readonly GLenum BYTE = 0x1400;
        public readonly GLenum UNSIGNED_BYTE = 0x1401;
        public readonly GLenum SHORT = 0x1402;
        public readonly GLenum UNSIGNED_SHORT = 0x1403;
        public readonly GLenum INT = 0x1404;
        public readonly GLenum UNSIGNED_INT = 0x1405;
        public readonly GLenum FLOAT = 0x1406;

        public readonly GLenum DEPTH_COMPONENT = 0x1902;
        public readonly GLenum ALPHA = 0x1906;
        public readonly GLenum RGB = 0x1907;
        public readonly GLenum RGBA = 0x1908;
        public readonly GLenum LUMINANCE = 0x1909;
        public readonly GLenum LUMINANCE_ALPHA = 0x190A;

        public readonly GLenum UNSIGNED_SHORT_4_4_4_4 = 0x8033;
        public readonly GLenum UNSIGNED_SHORT_5_5_5_1 = 0x8034;
        public readonly GLenum UNSIGNED_SHORT_5_6_5 = 0x8363;

        public readonly GLenum FRAGMENT_SHADER = 0x8B30;
        public readonly GLenum VERTEX_SHADER = 0x8B31;
        public readonly GLenum MAX_VERTEX_ATTRIBS = 0x8869;
        public readonly GLenum MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB;
        public readonly GLenum MAX_VARYING_VECTORS = 0x8DFC;
        public readonly GLenum MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;
        public readonly GLenum MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;
        public readonly GLenum MAX_TEXTURE_IMAGE_UNITS = 0x8872;
        public readonly GLenum MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD;
        public readonly GLenum SHADER_TYPE = 0x8B4F;
        public readonly GLenum DELETE_STATUS = 0x8B80;
        public readonly GLenum LINK_STATUS = 0x8B82;
        public readonly GLenum VALIDATE_STATUS = 0x8B83;
        public readonly GLenum ATTACHED_SHADERS = 0x8B85;
        public readonly GLenum ACTIVE_UNIFORMS = 0x8B86;
        public readonly GLenum ACTIVE_ATTRIBUTES = 0x8B89;
        public readonly GLenum SHADING_LANGUAGE_VERSION = 0x8B8C;
        public readonly GLenum CURRENT_PROGRAM = 0x8B8D;

        public readonly GLenum NEVER = 0x0200;
        public readonly GLenum LESS = 0x0201;
        public readonly GLenum EQUAL = 0x0202;
        public readonly GLenum LEQUAL = 0x0203;
        public readonly GLenum GREATER = 0x0204;
        public readonly GLenum NOTEQUAL = 0x0205;
        public readonly GLenum GEQUAL = 0x0206;
        public readonly GLenum ALWAYS = 0x0207;

        public readonly GLenum KEEP = 0x1E00;
        public readonly GLenum REPLACE = 0x1E01;
        public readonly GLenum INCR = 0x1E02;
        public readonly GLenum DECR = 0x1E03;
        public readonly GLenum INVERT = 0x150A;
        public readonly GLenum INCR_WRAP = 0x8507;
        public readonly GLenum DECR_WRAP = 0x8508;

        public readonly GLenum VENDOR = 0x1F00;
        public readonly GLenum RENDERER = 0x1F01;
        public readonly GLenum VERSION = 0x1F02;

        public readonly GLenum NEAREST = 0x2600;
        public readonly GLenum LINEAR = 0x2601;

        public readonly GLenum NEAREST_MIPMAP_NEAREST = 0x2700;
        public readonly GLenum LINEAR_MIPMAP_NEAREST = 0x2701;
        public readonly GLenum NEAREST_MIPMAP_LINEAR = 0x2702;
        public readonly GLenum LINEAR_MIPMAP_LINEAR = 0x2703;

        public readonly GLenum TEXTURE_MAG_FILTER = 0x2800;
        public readonly GLenum TEXTURE_MIN_FILTER = 0x2801;
        public readonly GLenum TEXTURE_WRAP_S = 0x2802;
        public readonly GLenum TEXTURE_WRAP_T = 0x2803;

        public readonly GLenum TEXTURE_2D = 0x0DE1;
        public readonly GLenum TEXTURE = 0x1702;

        public readonly GLenum TEXTURE_CUBE_MAP = 0x8513;
        public readonly GLenum TEXTURE_BINDING_CUBE_MAP = 0x8514;
        public readonly GLenum TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
        public readonly GLenum TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
        public readonly GLenum TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
        public readonly GLenum TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
        public readonly GLenum TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
        public readonly GLenum TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
        public readonly GLenum MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;

        public readonly GLenum TEXTURE0 = 0x84C0;
        public readonly GLenum TEXTURE1 = 0x84C1;
        public readonly GLenum TEXTURE2 = 0x84C2;
        public readonly GLenum TEXTURE3 = 0x84C3;
        public readonly GLenum TEXTURE4 = 0x84C4;
        public readonly GLenum TEXTURE5 = 0x84C5;
        public readonly GLenum TEXTURE6 = 0x84C6;
        public readonly GLenum TEXTURE7 = 0x84C7;
        public readonly GLenum TEXTURE8 = 0x84C8;
        public readonly GLenum TEXTURE9 = 0x84C9;
        public readonly GLenum TEXTURE10 = 0x84CA;
        public readonly GLenum TEXTURE11 = 0x84CB;
        public readonly GLenum TEXTURE12 = 0x84CC;
        public readonly GLenum TEXTURE13 = 0x84CD;
        public readonly GLenum TEXTURE14 = 0x84CE;
        public readonly GLenum TEXTURE15 = 0x84CF;
        public readonly GLenum TEXTURE16 = 0x84D0;
        public readonly GLenum TEXTURE17 = 0x84D1;
        public readonly GLenum TEXTURE18 = 0x84D2;
        public readonly GLenum TEXTURE19 = 0x84D3;
        public readonly GLenum TEXTURE20 = 0x84D4;
        public readonly GLenum TEXTURE21 = 0x84D5;
        public readonly GLenum TEXTURE22 = 0x84D6;
        public readonly GLenum TEXTURE23 = 0x84D7;
        public readonly GLenum TEXTURE24 = 0x84D8;
        public readonly GLenum TEXTURE25 = 0x84D9;
        public readonly GLenum TEXTURE26 = 0x84DA;
        public readonly GLenum TEXTURE27 = 0x84DB;
        public readonly GLenum TEXTURE28 = 0x84DC;
        public readonly GLenum TEXTURE29 = 0x84DD;
        public readonly GLenum TEXTURE30 = 0x84DE;
        public readonly GLenum TEXTURE31 = 0x84DF;
        public readonly GLenum ACTIVE_TEXTURE = 0x84E0;

        public readonly GLenum REPEAT = 0x2901;
        public readonly GLenum CLAMP_TO_EDGE = 0x812F;
        public readonly GLenum MIRRORED_REPEAT = 0x8370;

        public readonly GLenum FLOAT_VEC2 = 0x8B50;
        public readonly GLenum FLOAT_VEC3 = 0x8B51;
        public readonly GLenum FLOAT_VEC4 = 0x8B52;
        public readonly GLenum INT_VEC2 = 0x8B53;
        public readonly GLenum INT_VEC3 = 0x8B54;
        public readonly GLenum INT_VEC4 = 0x8B55;
        public readonly GLenum BOOL = 0x8B56;
        public readonly GLenum BOOL_VEC2 = 0x8B57;
        public readonly GLenum BOOL_VEC3 = 0x8B58;
        public readonly GLenum BOOL_VEC4 = 0x8B59;
        public readonly GLenum FLOAT_MAT2 = 0x8B5A;
        public readonly GLenum FLOAT_MAT3 = 0x8B5B;
        public readonly GLenum FLOAT_MAT4 = 0x8B5C;
        public readonly GLenum SAMPLER_2D = 0x8B5E;
        public readonly GLenum SAMPLER_CUBE = 0x8B60;

        public readonly GLenum VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;
        public readonly GLenum VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;
        public readonly GLenum VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;
        public readonly GLenum VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;
        public readonly GLenum VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;
        public readonly GLenum VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;
        public readonly GLenum VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;

        public readonly GLenum IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A;
        public readonly GLenum IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B;

        public readonly GLenum COMPILE_STATUS = 0x8B81;

        public readonly GLenum LOW_FLOAT = 0x8DF0;
        public readonly GLenum MEDIUM_FLOAT = 0x8DF1;
        public readonly GLenum HIGH_FLOAT = 0x8DF2;
        public readonly GLenum LOW_INT = 0x8DF3;
        public readonly GLenum MEDIUM_INT = 0x8DF4;
        public readonly GLenum HIGH_INT = 0x8DF5;

        public readonly GLenum FRAMEBUFFER = 0x8D40;
        public readonly GLenum RENDERBUFFER = 0x8D41;

        public readonly GLenum RGBA4 = 0x8056;
        public readonly GLenum RGB5_A1 = 0x8057;
        public readonly GLenum RGB565 = 0x8D62;
        public readonly GLenum DEPTH_COMPONENT16 = 0x81A5;
        public readonly GLenum STENCIL_INDEX = 0x1901;
        public readonly GLenum STENCIL_INDEX8 = 0x8D48;
        public readonly GLenum DEPTH_STENCIL = 0x84F9;

        public readonly GLenum RENDERBUFFER_WIDTH = 0x8D42;
        public readonly GLenum RENDERBUFFER_HEIGHT = 0x8D43;
        public readonly GLenum RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;
        public readonly GLenum RENDERBUFFER_RED_SIZE = 0x8D50;
        public readonly GLenum RENDERBUFFER_GREEN_SIZE = 0x8D51;
        public readonly GLenum RENDERBUFFER_BLUE_SIZE = 0x8D52;
        public readonly GLenum RENDERBUFFER_ALPHA_SIZE = 0x8D53;
        public readonly GLenum RENDERBUFFER_DEPTH_SIZE = 0x8D54;
        public readonly GLenum RENDERBUFFER_STENCIL_SIZE = 0x8D55;

        public readonly GLenum FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;
        public readonly GLenum FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;
        public readonly GLenum FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;
        public readonly GLenum FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;

        public readonly GLenum COLOR_ATTACHMENT0 = 0x8CE0;
        public readonly GLenum DEPTH_ATTACHMENT = 0x8D00;
        public readonly GLenum STENCIL_ATTACHMENT = 0x8D20;
        public readonly GLenum DEPTH_STENCIL_ATTACHMENT = 0x821A;

        public readonly GLenum NONE;

        public readonly GLenum FRAMEBUFFER_COMPLETE = 0x8CD5;
        public readonly GLenum FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
        public readonly GLenum FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
        public readonly GLenum FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9;
        public readonly GLenum FRAMEBUFFER_UNSUPPORTED = 0x8CDD;

        public readonly GLenum FRAMEBUFFER_BINDING = 0x8CA6;
        public readonly GLenum RENDERBUFFER_BINDING = 0x8CA7;
        public readonly GLenum MAX_RENDERBUFFER_SIZE = 0x84E8;

        public readonly GLenum INVALID_FRAMEBUFFER_OPERATION = 0x0506;
        public readonly GLenum UNPACK_FLIP_Y_WEBGL = 0x9240;
        public readonly GLenum UNPACK_PREMULTIPLY_ALPHA_WEBGL = 0x9241;
        public readonly GLenum CONTEXT_LOST_WEBGL = 0x9242;
        public readonly GLenum UNPACK_COLORSPACE_CONVERSION_WEBGL = 0x9243;
        public readonly GLenum BROWSER_DEFAULT_WEBGL = 0x9244;

        internal const int maxGLErrorsAllowedToConsole = 256;

        // ReSharper restore RedundantDefaultFieldInitializer

        public event EventHandler contextLost;

        internal readonly GraphicsContext3D m_context;
        internal readonly WebGLContextGroup m_contextGroup;

        internal bool m_markedCanvasDirty;
        internal readonly HashSet<WebGLContextObject> m_contextObjects = new HashSet<WebGLContextObject>();

        internal WebGLBuffer m_boundArrayBuffer;

        internal WebGLVertexArrayObjectOES m_defaultVertexArrayObject;
        internal WebGLVertexArrayObjectOES m_boundVertexArrayObject;

        internal VertexAttribValue[] m_vertexAttribValue;
        internal uint m_maxVertexAttribs;

        internal WebGLProgram m_currentProgram;
        internal WebGLFramebuffer m_framebufferBinding;
        internal WebGLRenderbuffer m_renderbufferBinding;

        internal TextureUnitState[] m_textureUnits;
        internal uint m_activeTextureUnit;

        internal WebGLTexture m_blackTexture;
        internal WebGLTexture m_blackTextureCubeMap;

        internal readonly List<GLenum> m_compressedTextureFormats = new List<GLenum>();

        internal GLint m_maxTextureSize;
        internal GLint m_maxCubeMapTextureSize;
        internal readonly GLint[] m_maxViewportDims = new GLint[2];
        internal GLint m_maxTextureLevel;
        internal GLint m_maxCubeMapTextureLevel;

        internal GLint m_maxDrawBuffers;
        internal GLint m_maxColorAttachments;
        internal GLenum m_backDrawBuffer;
        internal bool m_drawBuffersWebGLRequirementsChecked;
        internal bool m_drawBuffersSupported;

        internal GLint m_packAlignment;
        internal GLint m_unpackAlignment;
        internal bool m_unpackFlipY;
        internal bool m_unpackPremultiplyAlpha;
        internal GLenum m_unpackColorspaceConversion;
        internal bool m_contextLost;
        internal readonly Attributes m_attributes;

        internal readonly GLfloat[] m_clearColor = new float[4];
        internal readonly GLboolean[] m_colorMask = new GLboolean[4];
        internal GLboolean m_depthMask;

        internal bool m_stencilEnabled;
        internal GLuint m_stencilMask;
        internal GLuint m_stencilMaskBack;
        internal GLint m_stencilFuncRef;
        internal GLint m_stencilFuncRefBack;
        internal GLuint m_stencilFuncMask;
        internal GLuint m_stencilFuncMaskBack;

        internal bool m_isGLES2NPOTStrict;
        internal bool m_isErrorGeneratedOnOutOfBoundsAccesses;
        internal bool m_isResourceSafe;
        internal bool m_isDepthStencilSupported;
        internal bool m_isRobustnessEXTSupported;

        internal bool m_synthesizedErrorsToConsole;
        internal int m_numGLErrorsToConsoleAllowed;

        internal readonly Validation m_validation;

        internal EXTTextureFilterAnisotropic m_extTextureFilterAnisotropic;
        internal OESTextureFloat m_oesTextureFloat;
        internal OESTextureFloatLinear m_oesTextureFloatLinear;
        internal OESTextureHalfFloat m_oesTextureHalfFloat;
        internal OESTextureHalfFloatLinear m_oesTextureHalfFloatLinear;
        internal OESStandardDerivatives m_oesStandardDerivatives;
        internal OESVertexArrayObject m_oesVertexArrayObject;
        internal OESElementIndexUint m_oesElementIndexUint;
        internal WebGLLoseContext m_webglLoseContext;
        internal WebGLDebugRendererInfo m_webglDebugRendererInfo;
        internal WebGLDebugShaders m_webglDebugShaders;
        internal WebGLCompressedTextureATC m_webglCompressedTextureATC;
        internal WebGLCompressedTexturePVRTC m_webglCompressedTexturePVRTC;
        internal WebGLCompressedTextureS3TC m_webglCompressedTextureS3TC;
        internal WebGLDepthTexture m_webglDepthTexture;
        internal WebGLDrawBuffers m_webglDrawBuffers;
        internal ANGLEInstancedArrays m_angleInstancedArrays;

        public static WebGLRenderingContext create(HTMLCanvasElement canvas, WebGLContextAttributes attrs)
        {
            var attributes = attrs != null ? attrs.attributes() : new Attributes();

            attributes.noExtensions = true;
            attributes.shareResources = false;
            attributes.preferDiscreteGPU = true;

            var context = new GraphicsContext3D(attributes, canvas.handle());

            if (!context.makeContextCurrent())
            {
                throw new ApplicationException("Could not create a WebGL context.");
            }

            var extensions = context.getExtensions();
            if (extensions.supports("GL_EXT_debug_marker"))
            {
                extensions.pushGroupMarkerEXT("WebGLRenderingContext");
            }

            var renderingContext = new WebGLRenderingContext(canvas, context, attributes);

            return renderingContext;
        }

        ~WebGLRenderingContext()
        {
            this.Dispose(false);
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.m_boundArrayBuffer = null;
            this.m_defaultVertexArrayObject = null;
            this.m_boundVertexArrayObject = null;
            this.m_currentProgram = null;
            this.m_framebufferBinding = null;
            this.m_renderbufferBinding = null;

            for (var i = 0; i < this.m_textureUnits.Length; ++i)
            {
                this.m_textureUnits[i].TextureBinding = null;
                this.m_textureUnits[i].textureCubeMapBinding = null;
            }

            this.m_blackTexture = null;
            this.m_blackTextureCubeMap = null;

            this.detachAndRemoveAllObjects();
            this.destroyGraphicsContext3D();
            this.m_contextGroup.removeContext(this);

            if (disposing)
            {
                this.m_context.Dispose();
            }
        }

        public override bool is3d
        {
            get { return true; }
        }

        public override bool isAccelerated
        {
            get { return true; }
        }

        public int drawingBufferWidth()
        {
            return this.m_context.getInternalFramebufferSize().Width;
        }

        public int drawingBufferHeight()
        {
            return this.m_context.getInternalFramebufferSize().Height;
        }

        public void activeTexture(GLenum texture)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (texture - GraphicsContext3D.TEXTURE0 >= this.m_textureUnits.Length)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "activeTexture", "texture unit out of range");
                return;
            }
            this.m_activeTextureUnit = texture - GraphicsContext3D.TEXTURE0;
            this.m_context.activeTexture(texture);
        }

        public void attachShader(WebGLProgram program, WebGLShader shader)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("attachShader", program) || !this.m_validation.validateWebGLObject("attachShader", shader))
            {
                return;
            }
            if (!program.attachShader(shader))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "attachShader", "shader attachment already has shader");
                return;
            }
            this.m_context.attachShader(objectOrZero(program), objectOrZero(shader));
            shader.onAttached();
        }

        public void bindAttribLocation(WebGLProgram program, GLuint index, DOMString name)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("bindAttribLocation", program))
            {
                return;
            }
            if (!this.m_validation.validateLocationLength("bindAttribLocation", name))
            {
                return;
            }
            if (!this.m_validation.validateString("bindAttribLocation", name))
            {
                return;
            }
            if (isPrefixReserved(name))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "bindAttribLocation", "reserved prefix");
                return;
            }
            if (index >= this.m_maxVertexAttribs)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bindAttribLocation", "index out of range");
                return;
            }
            this.m_context.bindAttribLocation(objectOrZero(program), index, name);
        }

        public void bindBuffer(GLenum target, WebGLBuffer buffer)
        {
            bool deleted;
            if (!this.checkObjectToBeBound("bindBuffer", buffer, out deleted))
            {
                return;
            }
            if (deleted)
            {
                buffer = null;
            }
            if (buffer != null && buffer.getTarget() != 0 && buffer.getTarget() != target)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "bindBuffer", "buffers can not be used with multiple targets");
                return;
            }
            if (target == GraphicsContext3D.ARRAY_BUFFER)
            {
                this.m_boundArrayBuffer = buffer;
            }
            else if (target == GraphicsContext3D.ELEMENT_ARRAY_BUFFER)
            {
                this.m_boundVertexArrayObject.setElementArrayBuffer(buffer);
            }
            else
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "bindBuffer", "invalid target");
                return;
            }

            this.m_context.bindBuffer(target, objectOrZero(buffer));
            if (buffer != null)
            {
                buffer.setTarget(target);
            }
        }

        public void bindFramebuffer(GLenum target, WebGLFramebuffer buffer)
        {
            bool deleted;
            if (!this.checkObjectToBeBound("bindFramebuffer", buffer, out deleted))
            {
                return;
            }
            if (deleted)
            {
                buffer = null;
            }
            if (target != GraphicsContext3D.FRAMEBUFFER)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "bindFramebuffer", "invalid target");
                return;
            }
            this.m_framebufferBinding = buffer;
            this.m_context.bindFramebuffer(target, objectOrZero(buffer));
            if (buffer != null)
            {
                buffer.setHasEverBeenBound();
            }
            this.applyStencilTest();
        }

        public void bindRenderbuffer(GLenum target, WebGLRenderbuffer renderBuffer)
        {
            bool deleted;
            if (!this.checkObjectToBeBound("bindRenderbuffer", renderBuffer, out deleted))
            {
                return;
            }
            if (deleted)
            {
                renderBuffer = null;
            }
            if (target != GraphicsContext3D.RENDERBUFFER)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "bindRenderbuffer", "invalid target");
                return;
            }
            this.m_renderbufferBinding = renderBuffer;
            this.m_context.bindRenderbuffer(target, objectOrZero(renderBuffer));
            if (renderBuffer != null)
            {
                renderBuffer.setHasEverBeenBound();
            }
        }

        public void bindTexture(GLenum target, WebGLTexture texture)
        {
            bool deleted;
            if (!this.checkObjectToBeBound("bindTexture", texture, out deleted))
            {
                return;
            }
            if (deleted)
            {
                texture = null;
            }
            if (texture != null && texture.getTarget() != 0 && texture.getTarget() != target)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "bindTexture", "textures can not be used with multiple targets");
                return;
            }
            int maxLevel;
            if (target == GraphicsContext3D.TEXTURE_2D)
            {
                this.m_textureUnits[(int)this.m_activeTextureUnit].TextureBinding = texture;
                maxLevel = this.m_maxTextureLevel;
            }
            else if (target == GraphicsContext3D.TEXTURE_CUBE_MAP)
            {
                this.m_textureUnits[(int)this.m_activeTextureUnit].textureCubeMapBinding = texture;
                maxLevel = this.m_maxCubeMapTextureLevel;
            }
            else
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "bindTexture", "invalid target");
                return;
            }
            this.m_context.bindTexture(target, objectOrZero(texture));
            if (texture != null)
            {
                texture.setTarget(target, maxLevel);
            }
        }

        public void blendColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.blendColor(red, green, blue, alpha);
        }

        public void blendEquation(GLenum mode)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateBlendEquation("blendEquation", mode))
            {
                return;
            }
            this.m_context.blendEquation(mode);
        }

        public void blendEquationSeparate(GLenum modeRGB, GLenum modeAlpha)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateBlendEquation("blendEquation", modeRGB) || !this.m_validation.validateBlendEquation("blendEquation", modeAlpha))
            {
                return;
            }
            this.m_context.blendEquationSeparate(modeRGB, modeAlpha);
        }

        public void blendFunc(GLenum sfactor, GLenum dfactor)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateBlendFuncFactors("blendFunc", sfactor, dfactor))
            {
                return;
            }
            this.m_context.blendFunc(sfactor, dfactor);
        }

        public void blendFuncSeparate(GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateBlendFuncFactors("blendFunc", srcRGB, dstRGB))
            {
                return;
            }
            this.m_context.blendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        public void bufferData(GLenum target, long size, GLenum usage)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            var buffer = this.m_validation.validateBufferDataParameters("bufferData", target, usage);
            if (buffer == null)
            {
                return;
            }
            if (size < 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferData", "size < 0");
                return;
            }
            if (size == 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferData", "size == 0");
                return;
            }
            if (!this.isErrorGeneratedOnOutOfBoundsAccesses())
            {
                if (!buffer.associateBufferData(size))
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferData", "invalid buffer");
                    return;
                }
            }

            this.m_context.bufferData(target, size, usage);
        }

        public void bufferData(GLenum target, ArrayBuffer data, GLenum usage)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            var buffer = this.m_validation.validateBufferDataParameters("bufferData", target, usage);
            if (buffer == null)
            {
                return;
            }
            if (data == null)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferData", "no data");
                return;
            }
            if (!this.isErrorGeneratedOnOutOfBoundsAccesses())
            {
                if (!buffer.associateBufferData(data))
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferData", "invalid buffer");
                    return;
                }
            }

            this.m_context.bufferData(target, data.byteLength, data.@lock(), usage);
            data.unlock();
        }

        public void bufferData(GLenum target, ArrayBufferView data, GLenum usage)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            var buffer = this.m_validation.validateBufferDataParameters("bufferData", target, usage);
            if (buffer == null)
            {
                return;
            }
            if (data == null)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferData", "no data");
                return;
            }
            if (!this.isErrorGeneratedOnOutOfBoundsAccesses())
            {
                if (!buffer.associateBufferData(data))
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferData", "invalid buffer");
                    return;
                }
            }

            this.m_context.bufferData(target, data.byteLength, data.buffer.@lock() + data.byteOffset, usage);
            data.buffer.unlock();
        }

        public void bufferSubData(GLenum target, long offset, ArrayBuffer data)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            var buffer = this.m_validation.validateBufferDataParameters("bufferSubData", target, GraphicsContext3D.STATIC_DRAW);
            if (buffer == null)
            {
                return;
            }
            if (offset < 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferSubData", "offset < 0");
                return;
            }
            if (data == null)
            {
                return;
            }
            if (!this.isErrorGeneratedOnOutOfBoundsAccesses())
            {
                if (!buffer.associateBufferSubData(offset, data))
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferSubData", "offset out of range");
                    return;
                }
            }

            this.m_context.bufferSubData(target, offset, data.byteLength, data.@lock());
            data.unlock();
        }

        public void bufferSubData(GLenum target, long offset, ArrayBufferView data)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            var buffer = this.m_validation.validateBufferDataParameters("bufferSubData", target, GraphicsContext3D.STATIC_DRAW);
            if (buffer == null)
            {
                return;
            }
            if (offset < 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferSubData", "offset < 0");
                return;
            }
            if (data == null)
            {
                return;
            }
            if (!this.isErrorGeneratedOnOutOfBoundsAccesses())
            {
                if (!buffer.associateBufferSubData(offset, data))
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "bufferSubData", "offset out of range");
                    return;
                }
            }

            this.m_context.bufferSubData(target, offset, data.byteLength, data.buffer.@lock() + data.byteOffset);
            data.buffer.unlock();
        }

        public GLenum checkFramebufferStatus(GLenum target)
        {
            if (this.isContextLostOrPending())
            {
                return GraphicsContext3D.FRAMEBUFFER_UNSUPPORTED;
            }
            if (target != GraphicsContext3D.FRAMEBUFFER)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "checkFramebufferStatus", "invalid target");
                return 0;
            }
            if (this.m_framebufferBinding == null || this.m_framebufferBinding.obj() == 0)
            {
                return GraphicsContext3D.FRAMEBUFFER_COMPLETE;
            }
            var reason = "framebuffer incomplete";
            var result = this.m_framebufferBinding.checkStatus(ref reason);
            if (result != GraphicsContext3D.FRAMEBUFFER_COMPLETE)
            {
                this.printGLWarningToConsole("checkFramebufferStatus", reason);
                return result;
            }
            result = this.m_context.checkFramebufferStatus(target);
            return result;
        }

        public void clear(GLbitfield mask)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if ((mask & ~(GraphicsContext3D.COLOR_BUFFER_BIT | GraphicsContext3D.DEPTH_BUFFER_BIT | GraphicsContext3D.STENCIL_BUFFER_BIT)) != 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "clear", "invalid mask");
                return;
            }
            var reason = "framebuffer incomplete";
            if (this.m_framebufferBinding != null && !this.m_framebufferBinding.onAccess(this.graphicsContext3D(), !this.isResourceSafe(), ref reason))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_FRAMEBUFFER_OPERATION, "clear", reason);
                return;
            }
            if (true)
            {
                this.m_context.clear(mask);
            }
            this.markContextChanged();
        }

        public void clearColor(GLfloat r, GLfloat g, GLfloat b, GLfloat a)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (float.IsNaN(r))
            {
                r = 0;
            }
            if (float.IsNaN(g))
            {
                g = 0;
            }
            if (float.IsNaN(b))
            {
                b = 0;
            }
            if (float.IsNaN(a))
            {
                a = 1;
            }
            this.m_clearColor[0] = r;
            this.m_clearColor[1] = g;
            this.m_clearColor[2] = b;
            this.m_clearColor[3] = a;
            this.m_context.clearColor(r, g, b, a);
        }

        public void clearDepth(GLfloat depth)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.clearDepth(depth);
        }

        public void clearStencil(GLint s)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.clearStencil(s);
        }

        public void colorMask(bool red, bool green, bool blue, bool alpha)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_colorMask[0] = red;
            this.m_colorMask[1] = green;
            this.m_colorMask[2] = blue;
            this.m_colorMask[3] = alpha;
            this.m_context.colorMask(this.m_colorMask[0], this.m_colorMask[1], this.m_colorMask[2], this.m_colorMask[3]);
        }

        public void compileShader(WebGLShader shader)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("compileShader", shader))
            {
                return;
            }
            shader.setValid(this.m_context.compileShader(objectOrZero(shader)));
        }

        public void compressedTexImage2D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, ArrayBufferView data)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (!this.m_validation.validateTexFuncLevel("compressedTexImage2D", target, level))
            {
                return;
            }

            if (!this.m_compressedTextureFormats.Contains(internalformat))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "compressedTexImage2D", "invalid internalformat");
                return;
            }
            if (border != 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "compressedTexImage2D", "border not 0");
                return;
            }
            if (!this.m_validation.validateCompressedTexDimensions("compressedTexImage2D", target, level, width, height, internalformat))
            {
                return;
            }
            if (!this.m_validation.validateCompressedTexFuncData("compressedTexImage2D", width, height, internalformat, data))
            {
                return;
            }

            var tex = this.m_validation.validateTextureBinding("compressedTexImage2D", target, true);
            if (tex == null)
            {
                return;
            }
            if (!this.isGLES2NPOTStrict())
            {
                if (level != 0 && WebGLTexture.isNPOT(width, height))
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "compressedTexImage2D", "level > 0 not power of 2");
                    return;
                }
            }
            this.graphicsContext3D().compressedTexImage2D(target, level, internalformat, width, height, border, data.byteLength, data.buffer.@lock() + data.byteOffset);
            tex.setLevelInfo(target, level, internalformat, width, height, GraphicsContext3D.UNSIGNED_BYTE);
            tex.setCompressed();
        }

        public void compressedTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, ArrayBufferView data)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (!this.m_validation.validateTexFuncLevel("compressedTexSubImage2D", target, level))
            {
                return;
            }
            if (!this.m_compressedTextureFormats.Contains(format))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "compressedTexSubImage2D", "invalid format");
                return;
            }
            if (!this.m_validation.validateCompressedTexFuncData("compressedTexSubImage2D", width, height, format, data))
            {
                return;
            }

            var tex = this.m_validation.validateTextureBinding("compressedTexSubImage2D", target, true);
            if (tex == null)
            {
                return;
            }

            if (format != tex.getInternalFormat(target, level))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "compressedTexSubImage2D", "format does not match texture format");
                return;
            }

            if (!this.m_validation.validateCompressedTexSubDimensions("compressedTexSubImage2D", target, level, xoffset, yoffset, width, height, format, tex))
            {
                return;
            }

            this.graphicsContext3D().compressedTexSubImage2D(target, level, xoffset, yoffset, width, height, format, data.byteLength, data.buffer.@lock() + data.byteOffset);
            tex.setCompressed();
        }

        public void copyTexImage2D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (!this.m_validation.validateTexFuncParameters("copyTexImage2D", TexFuncValidationFunctionType.NotTexSubImage2D, target, level, internalformat, width, height, border, internalformat, GraphicsContext3D.UNSIGNED_BYTE))
            {
                return;
            }
            if (!this.m_validation.validateSettableTexFormat("copyTexImage2D", internalformat))
            {
                return;
            }
            var tex = this.m_validation.validateTextureBinding("copyTexImage2D", target, true);
            if (tex == null)
            {
                return;
            }
            if (!isTexInternalFormatColorBufferCombinationValid(internalformat, this.getBoundFramebufferColorFormat()))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "copyTexImage2D", "framebuffer is incompatible format");
                return;
            }
            if (!this.isGLES2NPOTStrict() && level != 0 && WebGLTexture.isNPOT(width, height))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "copyTexImage2D", "level > 0 not power of 2");
                return;
            }
            var reason = "framebuffer incomplete";
            if (this.m_framebufferBinding != null && !this.m_framebufferBinding.onAccess(this.graphicsContext3D(), !this.isResourceSafe(), ref reason))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_FRAMEBUFFER_OPERATION, "copyTexImage2D", reason);
                return;
            }
            if (this.isResourceSafe())
            {
                this.m_context.copyTexImage2D(target, level, internalformat, x, y, width, height, border);
            }
            else
            {
                GLint clippedX, clippedY;
                GLsizei clippedWidth, clippedHeight;
                if (clip2D(x, y, width, height, this.getBoundFramebufferWidth(), this.getBoundFramebufferHeight(), out clippedX, out clippedY, out clippedWidth, out clippedHeight))
                {
                    this.m_context.texImage2DResourceSafe(target, level, internalformat, width, height, border,
                                                          internalformat, GraphicsContext3D.UNSIGNED_BYTE, this.m_unpackAlignment);
                    if (clippedWidth > 0 && clippedHeight > 0)
                    {
                        this.m_context.copyTexSubImage2D(target, level, clippedX - x, clippedY - y, clippedX, clippedY, clippedWidth, clippedHeight);
                    }
                }
                else
                {
                    this.m_context.copyTexImage2D(target, level, internalformat, x, y, width, height, border);
                }
            }
            // FIXME: if the framebuffer is not complete, none of the below should be executed.
            tex.setLevelInfo(target, level, internalformat, width, height, GraphicsContext3D.UNSIGNED_BYTE);
        }

        public void copyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (!this.m_validation.validateTexFuncLevel("copyTexSubImage2D", target, level))
            {
                return;
            }
            var tex = this.m_validation.validateTextureBinding("copyTexSubImage2D", target, true);
            if (tex == null)
            {
                return;
            }
            if (!this.m_validation.validateSize("copyTexSubImage2D", xoffset, yoffset) || !this.m_validation.validateSize("copyTexSubImage2D", width, height))
            {
                return;
            }
            // Before checking if it is in the range, check if overflow happens first.
            if (xoffset + width < 0 || yoffset + height < 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "copyTexSubImage2D", "bad dimensions");
                return;
            }
            if (xoffset + width > tex.getWidth(target, level) || yoffset + height > tex.getHeight(target, level))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "copyTexSubImage2D", "rectangle out of range");
                return;
            }
            var internalformat = tex.getInternalFormat(target, level);
            if (!this.m_validation.validateSettableTexFormat("copyTexSubImage2D", internalformat))
            {
                return;
            }
            if (!isTexInternalFormatColorBufferCombinationValid(internalformat, this.getBoundFramebufferColorFormat()))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "copyTexSubImage2D", "framebuffer is incompatible format");
                return;
            }
            var reason = "framebuffer incomplete";
            if (this.m_framebufferBinding != null && !this.m_framebufferBinding.onAccess(this.graphicsContext3D(), !this.isResourceSafe(), ref reason))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_FRAMEBUFFER_OPERATION, "copyTexSubImage2D", reason);
                return;
            }
            if (this.isResourceSafe())
            {
                this.m_context.copyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
            }
            else
            {
                GLint clippedX, clippedY;
                GLsizei clippedWidth, clippedHeight;
                if (clip2D(x, y, width, height, this.getBoundFramebufferWidth(), this.getBoundFramebufferHeight(), out clippedX, out clippedY, out clippedWidth, out clippedHeight))
                {
                    var format = tex.getInternalFormat(target, level);
                    var type = tex.getType(target, level);
                    byte[] zero = null;
                    if (width != 0 && height != 0)
                    {
                        int size;
                        int paddingInBytes;
                        var error = GraphicsContext3D.computeImageSizeInBytes(format, type, width, height, this.m_unpackAlignment, out size, out paddingInBytes);
                        if (error != GraphicsContext3D.NO_ERROR)
                        {
                            this.synthesizeGLError(error, "copyTexSubImage2D", "bad dimensions");
                            return;
                        }
                        zero = new byte[size];
                        if (zero.Length == 0)
                        {
                            this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "copyTexSubImage2D", "out of memory");
                            return;
                        }
                    }

                    var handle = GCHandle.Alloc(zero, GCHandleType.Pinned);
                    this.m_context.texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, handle.AddrOfPinnedObject());
                    handle.Free();

                    if (clippedWidth > 0 && clippedHeight > 0)
                    {
                        this.m_context.copyTexSubImage2D(target, level, xoffset + clippedX - x, yoffset + clippedY - y, clippedX, clippedY, clippedWidth, clippedHeight);
                    }
                }
                else
                {
                    this.m_context.copyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
                }
            }
        }

        public WebGLBuffer createBuffer()
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            var o = new WebGLBuffer(this);
            this.addSharedObject(o);
            return o;
        }

        public WebGLFramebuffer createFramebuffer()
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            var o = new WebGLFramebuffer(this);
            this.addContextObject(o);
            return o;
        }

        public WebGLProgram createProgram()
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            var o = new WebGLProgram(this);
            this.addSharedObject(o);
            return o;
        }

        public WebGLRenderbuffer createRenderbuffer()
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            var o = new WebGLRenderbuffer(this);
            this.addSharedObject(o);
            return o;
        }

        public WebGLShader createShader(GLenum type)
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            if (type != GraphicsContext3D.VERTEX_SHADER && type != GraphicsContext3D.FRAGMENT_SHADER)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "createShader", "invalid shader type");
                return null;
            }
            var o = new WebGLShader(this, type);
            this.addSharedObject(o);
            return o;
        }

        public WebGLTexture createTexture()
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            var o = new WebGLTexture(this);
            this.addSharedObject(o);
            return o;
        }

        public void cullFace(GLenum mode)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.cullFace(mode);
        }

        public void deleteBuffer(WebGLBuffer buffer)
        {
            if (!this.deleteObject(buffer))
            {
                return;
            }
            if (this.m_boundArrayBuffer == buffer)
            {
                this.m_boundArrayBuffer = null;
            }
            this.m_boundVertexArrayObject.unbindBuffer(buffer);
        }

        public void deleteFramebuffer(WebGLFramebuffer framebuffer)
        {
            if (!this.deleteObject(framebuffer))
            {
                return;
            }
            if (framebuffer == this.m_framebufferBinding)
            {
                this.m_framebufferBinding = null;
                this.m_context.bindFramebuffer(GraphicsContext3D.FRAMEBUFFER, 0);
            }
        }

        public void deleteProgram(WebGLProgram program)
        {
            this.deleteObject(program);
        }

        public void deleteRenderbuffer(WebGLRenderbuffer renderbuffer)
        {
            if (!this.deleteObject(renderbuffer))
            {
                return;
            }
            if (renderbuffer == this.m_renderbufferBinding)
            {
                this.m_renderbufferBinding = null;
            }
            if (this.m_framebufferBinding != null)
            {
                this.m_framebufferBinding.removeAttachmentFromBoundFramebuffer(renderbuffer);
            }
        }

        public void deleteShader(WebGLShader shader)
        {
            this.deleteObject(shader);
        }

        public void deleteTexture(WebGLTexture texture)
        {
            if (!this.deleteObject(texture))
            {
                return;
            }
            for (var i = 0; i < this.m_textureUnits.Length; ++i)
            {
                if (texture == this.m_textureUnits[i].TextureBinding)
                {
                    this.m_textureUnits[i].TextureBinding = null;
                }
                if (texture == this.m_textureUnits[i].textureCubeMapBinding)
                {
                    this.m_textureUnits[i].textureCubeMapBinding = null;
                }
            }
            if (this.m_framebufferBinding != null)
            {
                this.m_framebufferBinding.removeAttachmentFromBoundFramebuffer(texture);
            }
        }

        public void depthFunc(GLenum func)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.depthFunc(func);
        }

        public void depthMask(bool flag)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_depthMask = flag;
            this.m_context.depthMask(this.m_depthMask);
        }

        public void depthRange(GLfloat zNear, GLfloat zFar)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (zNear > zFar)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "depthRange", "zNear > zFar");
                return;
            }
            this.m_context.depthRange(zNear, zFar);
        }

        public void detachShader(WebGLProgram program, WebGLShader shader)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("detachShader", program) || !this.m_validation.validateWebGLObject("detachShader", shader))
            {
                return;
            }
            if (!program.detachShader(shader))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "detachShader", "shader not attached");
                return;
            }
            this.m_context.detachShader(objectOrZero(program), objectOrZero(shader));
            shader.onDetached(this.graphicsContext3D());
        }

        public void disable(GLenum cap)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateCapability("disable", cap))
            {
                return;
            }
            if (cap == GraphicsContext3D.STENCIL_TEST)
            {
                this.m_stencilEnabled = false;
                this.applyStencilTest();
                return;
            }
            this.m_context.disable(cap);
        }

        public void disableVertexAttribArray(GLuint index)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (index >= this.m_maxVertexAttribs)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "disableVertexAttribArray", "index out of range");
                return;
            }
            var state = this.m_boundVertexArrayObject.getVertexAttribState((int)index);
            state.enabled = false;
            this.m_context.disableVertexAttribArray(index);
        }

        public void drawArrays(GLenum mode, GLint first, GLsizei count)
        {
            if (!this.m_validation.validateDrawArrays("drawArrays", mode, first, count, 0))
            {
                return;
            }
            if (!this.isGLES2NPOTStrict())
            {
                this.checkTextureCompleteness("drawArrays", true);
            }
            this.m_context.drawArrays(mode, first, count);
            if (!this.isGLES2NPOTStrict())
            {
                this.checkTextureCompleteness("drawArrays", false);
            }
            this.markContextChanged();
        }

        public void drawElements(GLenum mode, GLsizei count, GLenum type, long offset)
        {
            uint numElements;
            if (!this.m_validation.validateDrawElements("drawElements", mode, count, type, offset, out numElements))
            {
                return;
            }
            if (!this.isGLES2NPOTStrict())
            {
                this.checkTextureCompleteness("drawElements", true);
            }
            this.m_context.drawElements(mode, count, type, new IntPtr(offset));
            if (!this.isGLES2NPOTStrict())
            {
                this.checkTextureCompleteness("drawElements", false);
            }
            this.markContextChanged();
        }

        public void enable(GLenum cap)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateCapability("enable", cap))
            {
                return;
            }
            if (cap == GraphicsContext3D.STENCIL_TEST)
            {
                this.m_stencilEnabled = true;
                this.applyStencilTest();
                return;
            }
            this.m_context.enable(cap);
        }

        public void enableVertexAttribArray(GLuint index)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (index >= this.m_maxVertexAttribs)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "enableVertexAttribArray", "index out of range");
                return;
            }
            var state = this.m_boundVertexArrayObject.getVertexAttribState((int)index);
            state.enabled = true;
            this.m_context.enableVertexAttribArray(index);
        }

        public void finish()
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.finish();
        }

        public void flush()
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.flush();
        }

        public void framebufferRenderbuffer(GLenum target, GLenum attachment, GLenum renderbuffertarget, WebGLRenderbuffer buffer)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateFramebufferFuncParameters("framebufferRenderbuffer", target, attachment))
            {
                return;
            }
            if (renderbuffertarget != GraphicsContext3D.RENDERBUFFER)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "framebufferRenderbuffer", "invalid target");
                return;
            }
            if (buffer != null && !buffer.validate(this.contextGroup(), this))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "framebufferRenderbuffer", "no buffer or buffer not from this context");
                return;
            }
            // Don't allow the default framebuffer to be mutated; all current
            // implementations use an FBO internally in place of the default
            // FBO.
            if (this.m_framebufferBinding == null || this.m_framebufferBinding.obj() == 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "framebufferRenderbuffer", "no framebuffer bound");
                return;
            }
            var bufferObject = objectOrZero(buffer);
            switch (attachment)
            {
                case GraphicsContext3D.DEPTH_STENCIL_ATTACHMENT:
                    this.m_context.framebufferRenderbuffer(target, GraphicsContext3D.DEPTH_ATTACHMENT, renderbuffertarget, bufferObject);
                    this.m_context.framebufferRenderbuffer(target, GraphicsContext3D.STENCIL_ATTACHMENT, renderbuffertarget, bufferObject);
                    break;
                default:
                    this.m_context.framebufferRenderbuffer(target, attachment, renderbuffertarget, bufferObject);
                    break;
            }
            this.m_framebufferBinding.setAttachmentForBoundFramebuffer(attachment, buffer);
            this.applyStencilTest();
        }

        public void framebufferTexture(GLenum target, GLenum attachment, GLenum textarget, WebGLTexture texture, GLint level)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateFramebufferFuncParameters("framebufferTexture", target, attachment))
            {
                return;
            }
            if (level != 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "framebufferTexture", "level not 0");
                return;
            }
            if (texture != null && !texture.validate(this.contextGroup(), this))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "framebufferTexture", "no texture or texture not from this context");
                return;
            }
            // Don't allow the default framebuffer to be mutated; all current
            // implementations use an FBO internally in place of the default
            // FBO.
            if (this.m_framebufferBinding == null || this.m_framebufferBinding.obj() == 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "framebufferTexture", "no framebuffer bound");
                return;
            }
            var textureObject = objectOrZero(texture);
            switch (attachment)
            {
                case GraphicsContext3D.DEPTH_STENCIL_ATTACHMENT:
                    this.m_context.framebufferTexture(target, GraphicsContext3D.DEPTH_ATTACHMENT, textarget, textureObject, level);
                    this.m_context.framebufferTexture(target, GraphicsContext3D.STENCIL_ATTACHMENT, textarget, textureObject, level);
                    break;
                case GraphicsContext3D.DEPTH_ATTACHMENT:
                    this.m_context.framebufferTexture(target, attachment, textarget, textureObject, level);
                    break;
                case GraphicsContext3D.STENCIL_ATTACHMENT:
                    this.m_context.framebufferTexture(target, attachment, textarget, textureObject, level);
                    break;
                default:
                    this.m_context.framebufferTexture(target, attachment, textarget, textureObject, level);
                    break;
            }
            this.m_framebufferBinding.setAttachmentForBoundFramebuffer(attachment, textarget, texture, level);
            this.applyStencilTest();
        }

        public void frontFace(GLenum mode)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.frontFace(mode);
        }

        public void generateMipmap(GLenum target)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            var tex = this.m_validation.validateTextureBinding("generateMipmap", target, false);
            if (tex == null)
            {
                return;
            }
            if (!tex.canGenerateMipmaps())
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "generateMipmap", "level 0 not power of 2 or not all the same size");
                return;
            }
            // FIXME: https://bugs.webkit.org/show_bug.cgi?id=123916. Compressed textures should be allowed in WebGL 2:
            if (tex.isCompressed())
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "generateMipmap", "trying to generate mipmaps from compressed texture");
                return;
            }
            if (!this.m_validation.validateSettableTexFormat("generateMipmap", tex.getInternalFormat(target, 0)))
            {
                return;
            }

            // generateMipmap won't work properly if minFilter is not NEAREST_MIPMAP_LINEAR
            // on Mac.  Remove the hack once this driver issue is fixed.
            this.m_context.generateMipmap(target);
            tex.generateMipmapLevelInfo();
        }

        public WebGLActiveInfo getActiveAttrib(WebGLProgram program, GLuint index)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("getActiveAttrib", program))
            {
                return null;
            }
            var info = new ActiveInfo();
            if (!this.m_context.getActiveAttrib(objectOrZero(program), index, info))
            {
                return null;
            }

            return new WebGLActiveInfo(info.size, info.type, info.name);
        }

        public WebGLActiveInfo getActiveUniform(WebGLProgram program, GLuint index)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("getActiveUniform", program))
            {
                return null;
            }
            var info = new ActiveInfo();
            if (!this.m_context.getActiveUniform(objectOrZero(program), index, info))
            {
                return null;
            }

            return new WebGLActiveInfo(info.size, info.type, info.name);
        }

        public WebGLShader[] getAttachedShaders(WebGLProgram program)
        {
            var shaders = new WebGLShader[2];

            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("getAttachedShaders", program))
            {
                return null;
            }

            GLenum[] shaderType = {GraphicsContext3D.VERTEX_SHADER, GraphicsContext3D.FRAGMENT_SHADER};

            for (var i = 0; i < shaderType.Length; ++i)
            {
                shaders[i] = program.getAttachedShader(shaderType[i]);
            }
            return shaders;
        }

        public GLint getAttribLocation(WebGLProgram program, DOMString name)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("getAttribLocation", program))
            {
                return -1;
            }
            if (!this.m_validation.validateLocationLength("getAttribLocation", name))
            {
                return -1;
            }
            if (!this.m_validation.validateString("getAttribLocation", name))
            {
                return -1;
            }
            if (isPrefixReserved(name))
            {
                return -1;
            }
            if (!program.getLinkStatus())
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "getAttribLocation", "program not linked");
                return -1;
            }
            return this.m_context.getAttribLocation(objectOrZero(program), name);
        }

        public dynamic getBufferParameter(GLenum target, GLenum pname)
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            if (target != GraphicsContext3D.ARRAY_BUFFER && target != GraphicsContext3D.ELEMENT_ARRAY_BUFFER)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getBufferParameter", "invalid target");
                return null;
            }

            if (pname != GraphicsContext3D.BUFFER_SIZE && pname != GraphicsContext3D.BUFFER_USAGE)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getBufferParameter", "invalid parameter name");
                return null;
            }

            int value;
            this.m_context.getBufferParameteriv(target, pname, out value);
            if (pname == GraphicsContext3D.BUFFER_SIZE)
            {
                return value;
            }
            return (uint)value;
        }

        public WebGLContextAttributes getContextAttributes()
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            // We always need to return a new WebGLContextAttributes object to
            // prevent the user from mutating any cached version.

            // Also, we need to enforce requested values of "false" for depth
            // and stencil, regardless of the properties of the underlying
            // GraphicsContext3D or DrawingBuffer.
            var attributes = new WebGLContextAttributes(this.m_context.getContextAttributes());
            if (!this.m_attributes.depth)
            {
                attributes.setDepth(false);
            }
            if (!this.m_attributes.stencil)
            {
                attributes.setStencil(false);
            }
            return attributes;
        }

        public GLenum getError()
        {
            return this.m_context.getError();
        }

        public Object getExtension(String name)
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }

            if (equalIgnoringCase(name, "WEBKIT_EXT_texture_filter_anisotropic") && this.m_context.getExtensions().supports("GL_EXT_texture_filter_anisotropic"))
            {
                if (this.m_extTextureFilterAnisotropic == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_EXT_texture_filter_anisotropic");
                    this.m_extTextureFilterAnisotropic = new EXTTextureFilterAnisotropic(this);
                }
                return this.m_extTextureFilterAnisotropic;
            }
            if (equalIgnoringCase(name, "OES_standard_derivatives") && this.m_context.getExtensions().supports("GL_OES_standard_derivatives"))
            {
                if (this.m_oesStandardDerivatives == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_OES_standard_derivatives");
                    this.m_oesStandardDerivatives = new OESStandardDerivatives(this);
                }
                return this.m_oesStandardDerivatives;
            }
            if (equalIgnoringCase(name, "OES_texture_float") && this.m_context.getExtensions().supports("GL_OES_texture_float"))
            {
                if (this.m_oesTextureFloat == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_OES_texture_float");
                    this.m_oesTextureFloat = new OESTextureFloat(this);
                }
                return this.m_oesTextureFloat;
            }
            if (equalIgnoringCase(name, "OES_texture_float_linear") && this.m_context.getExtensions().supports("GL_OES_texture_float_linear"))
            {
                if (this.m_oesTextureFloatLinear == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_OES_texture_float_linear");
                    this.m_oesTextureFloatLinear = new OESTextureFloatLinear(this);
                }
                return this.m_oesTextureFloatLinear;
            }
            if (equalIgnoringCase(name, "OES_texture_half_float") && this.m_context.getExtensions().supports("GL_OES_texture_half_float"))
            {
                if (this.m_oesTextureHalfFloat == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_OES_texture_half_float");
                    this.m_oesTextureHalfFloat = new OESTextureHalfFloat(this);
                }
                return this.m_oesTextureHalfFloat;
            }
            if (equalIgnoringCase(name, "OES_texture_half_float_linear") && this.m_context.getExtensions().supports("GL_OES_texture_half_float_linear"))
            {
                if (this.m_oesTextureHalfFloatLinear == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_OES_texture_half_float_linear");
                    this.m_oesTextureHalfFloatLinear = new OESTextureHalfFloatLinear(this);
                }
                return this.m_oesTextureHalfFloatLinear;
            }
            if (equalIgnoringCase(name, "OES_vertex_array_object") && this.m_context.getExtensions().supports("GL_OES_vertex_array_object"))
            {
                if (this.m_oesVertexArrayObject == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_OES_vertex_array_object");
                    this.m_oesVertexArrayObject = new OESVertexArrayObject(this);
                }
                return this.m_oesVertexArrayObject;
            }
            if (equalIgnoringCase(name, "OES_element_index_uint") && this.m_context.getExtensions().supports("GL_OES_element_index_uint"))
            {
                if (this.m_oesElementIndexUint == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_OES_element_index_uint");
                    this.m_oesElementIndexUint = new OESElementIndexUint(this);
                }
                return this.m_oesElementIndexUint;
            }
            if (equalIgnoringCase(name, "WEBGL_lose_context"))
            {
                return this.m_webglLoseContext ?? (this.m_webglLoseContext = new WebGLLoseContext(this));
            }
            if ((equalIgnoringCase(name, "WEBKIT_WEBGL_compressed_texture_atc")) && WebGLCompressedTextureATC.supported(this))
            {
                return this.m_webglCompressedTextureATC ?? (this.m_webglCompressedTextureATC = new WebGLCompressedTextureATC(this));
            }
            if ((equalIgnoringCase(name, "WEBKIT_WEBGL_compressed_texture_pvrtc")) && WebGLCompressedTexturePVRTC.supported(this))
            {
                return this.m_webglCompressedTexturePVRTC ?? (this.m_webglCompressedTexturePVRTC = new WebGLCompressedTexturePVRTC(this));
            }
            if (equalIgnoringCase(name, "WEBGL_compressed_texture_s3tc") && WebGLCompressedTextureS3TC.supported(this))
            {
                return this.m_webglCompressedTextureS3TC ?? (this.m_webglCompressedTextureS3TC = new WebGLCompressedTextureS3TC(this));
            }
            if (equalIgnoringCase(name, "WEBGL_depth_texture") && WebGLDepthTexture.supported(this.graphicsContext3D()))
            {
                if (this.m_webglDepthTexture == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_ANGLE_depth_texture");
                    this.m_webglDepthTexture = new WebGLDepthTexture(this);
                }
                return this.m_webglDepthTexture;
            }
            if (equalIgnoringCase(name, "WEBGL_draw_buffers") && this.supportsDrawBuffers())
            {
                if (this.m_webglDrawBuffers == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_EXT_draw_buffers");
                    this.m_webglDrawBuffers = new WebGLDrawBuffers(this);
                }
                return this.m_webglDrawBuffers;
            }
            if (equalIgnoringCase(name, "ANGLE_instanced_arrays") && ANGLEInstancedArrays.supported(this))
            {
                if (this.m_angleInstancedArrays == null)
                {
                    this.m_context.getExtensions().ensureEnabled("GL_ANGLE_instanced_arrays");
                    this.m_angleInstancedArrays = new ANGLEInstancedArrays(this);
                }
                return this.m_angleInstancedArrays;
            }
            if (allowPrivilegedExtensions())
            {
                if (equalIgnoringCase(name, "WEBGL_debug_renderer_info"))
                {
                    return this.m_webglDebugRendererInfo ?? (this.m_webglDebugRendererInfo = new WebGLDebugRendererInfo(this));
                }
                if (equalIgnoringCase(name, "WEBGL_debug_shaders") && this.m_context.getExtensions().supports("GL_ANGLE_translated_shader_source"))
                {
                    return this.m_webglDebugShaders ?? (this.m_webglDebugShaders = new WebGLDebugShaders(this));
                }
            }

            return null;
        }

        public dynamic getFramebufferAttachmentParameter(GLenum target, GLenum attachment, GLenum pname)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateFramebufferFuncParameters("getFramebufferAttachmentParameter", target, attachment))
            {
                return null;
            }

            if (this.m_framebufferBinding == null || this.m_framebufferBinding.obj() == 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "getFramebufferAttachmentParameter", "no framebuffer bound");
                return null;
            }

            var @object = this.m_framebufferBinding.getAttachmentObject(attachment);
            if (@object == null)
            {
                if (pname == GraphicsContext3D.FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE)
                {
                    return GraphicsContext3D.NONE;
                }
                // OpenGL ES 2.0 specifies INVALID_ENUM in this case, while desktop GL specifies INVALID_OPERATION.
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getFramebufferAttachmentParameter", "invalid parameter name");
                return null;
            }

            if (@object.isTexture())
            {
                switch (pname)
                {
                    case GraphicsContext3D.FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE:
                        return GraphicsContext3D.TEXTURE;
                    case GraphicsContext3D.FRAMEBUFFER_ATTACHMENT_OBJECT_NAME:
                        return @object as WebGLTexture;
                    case GraphicsContext3D.FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL:
                    case GraphicsContext3D.FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE:
                    {
                        GLint value;
                        this.m_context.getFramebufferAttachmentParameteriv(target, attachment, pname, out value);
                        return value;
                    }
                    default:
                        this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getFramebufferAttachmentParameter", "invalid parameter name for texture attachment");
                        return null;
                }
            }
            switch (pname)
            {
                case GraphicsContext3D.FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE:
                    return GraphicsContext3D.RENDERBUFFER;
                case GraphicsContext3D.FRAMEBUFFER_ATTACHMENT_OBJECT_NAME:
                    return @object as WebGLRenderbuffer;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getFramebufferAttachmentParameter", "invalid parameter name for renderbuffer attachment");
                    return null;
            }
        }

        public dynamic getParameter(GLenum pname)
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            const int intZero = 0;
            switch (pname)
            {
                case GraphicsContext3D.ACTIVE_TEXTURE:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.ALIASED_LINE_WIDTH_RANGE:
                    return this.getWebGLFloatArrayParameter(pname);
                case GraphicsContext3D.ALIASED_POINT_SIZE_RANGE:
                    return this.getWebGLFloatArrayParameter(pname);
                case GraphicsContext3D.ALPHA_BITS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.ARRAY_BUFFER_BINDING:
                    return this.m_boundArrayBuffer;
                case GraphicsContext3D.BLEND:
                    return this.getBooleanParameter(pname);
                case GraphicsContext3D.BLEND_COLOR:
                    return this.getWebGLFloatArrayParameter(pname);
                case GraphicsContext3D.BLEND_DST_ALPHA:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.BLEND_DST_RGB:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.BLEND_EQUATION_ALPHA:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.BLEND_EQUATION_RGB:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.BLEND_SRC_ALPHA:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.BLEND_SRC_RGB:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.BLUE_BITS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.COLOR_CLEAR_VALUE:
                    return this.getWebGLFloatArrayParameter(pname);
                case GraphicsContext3D.COLOR_WRITEMASK:
                    return this.getBooleanArrayParameter(pname);
                case GraphicsContext3D.COMPRESSED_TEXTURE_FORMATS:
                    return new Uint32Array(this.m_compressedTextureFormats.ToArray());
                case GraphicsContext3D.CULL_FACE:
                    return this.getBooleanParameter(pname);
                case GraphicsContext3D.CULL_FACE_MODE:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.CURRENT_PROGRAM:
                    return this.m_currentProgram;
                case GraphicsContext3D.DEPTH_BITS:
                    return this.m_framebufferBinding == null && !this.m_attributes.depth ? intZero : this.getIntParameter(pname);
                case GraphicsContext3D.DEPTH_CLEAR_VALUE:
                    return this.getFloatParameter(pname);
                case GraphicsContext3D.DEPTH_FUNC:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.DEPTH_RANGE:
                    return this.getWebGLFloatArrayParameter(pname);
                case GraphicsContext3D.DEPTH_TEST:
                    return this.getBooleanParameter(pname);
                case GraphicsContext3D.DEPTH_WRITEMASK:
                    return this.getBooleanParameter(pname);
                case GraphicsContext3D.DITHER:
                    return this.getBooleanParameter(pname);
                case GraphicsContext3D.ELEMENT_ARRAY_BUFFER_BINDING:
                    return this.m_boundVertexArrayObject.getElementArrayBuffer();
                case GraphicsContext3D.FRAMEBUFFER_BINDING:
                    return this.m_framebufferBinding;
                case GraphicsContext3D.FRONT_FACE:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.GENERATE_MIPMAP_HINT:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.GREEN_BITS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.LINE_WIDTH:
                    return this.getFloatParameter(pname);
                case GraphicsContext3D.MAX_COMBINED_TEXTURE_IMAGE_UNITS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_CUBE_MAP_TEXTURE_SIZE:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_FRAGMENT_UNIFORM_VECTORS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_RENDERBUFFER_SIZE:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_TEXTURE_IMAGE_UNITS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_TEXTURE_SIZE:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_VARYING_VECTORS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_VERTEX_ATTRIBS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_VERTEX_TEXTURE_IMAGE_UNITS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_VERTEX_UNIFORM_VECTORS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.MAX_VIEWPORT_DIMS:
                    return this.getWebGLIntArrayParameter(pname);
                case GraphicsContext3D.NUM_SHADER_BINARY_FORMATS:
                    // FIXME: should we always return 0 for this?
                    return this.getIntParameter(pname);
                case GraphicsContext3D.PACK_ALIGNMENT:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.POLYGON_OFFSET_FACTOR:
                    return this.getFloatParameter(pname);
                case GraphicsContext3D.POLYGON_OFFSET_FILL:
                    return this.getBooleanParameter(pname);
                case GraphicsContext3D.POLYGON_OFFSET_UNITS:
                    return this.getFloatParameter(pname);
                case GraphicsContext3D.RED_BITS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.RENDERBUFFER_BINDING:
                    return this.m_renderbufferBinding;
                case GraphicsContext3D.RENDERER:
                    return "WebKit WebGL";
                case GraphicsContext3D.SAMPLE_BUFFERS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.SAMPLE_COVERAGE_INVERT:
                    return this.getBooleanParameter(pname);
                case GraphicsContext3D.SAMPLE_COVERAGE_VALUE:
                    return this.getFloatParameter(pname);
                case GraphicsContext3D.SAMPLES:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.SCISSOR_BOX:
                    return this.getWebGLIntArrayParameter(pname);
                case GraphicsContext3D.SCISSOR_TEST:
                    return this.getBooleanParameter(pname);
                case GraphicsContext3D.SHADING_LANGUAGE_VERSION:
                    return "WebGL GLSL ES 1.0 (" + this.m_context.getString(GraphicsContext3D.SHADING_LANGUAGE_VERSION) + ")";
                case GraphicsContext3D.STENCIL_BACK_FAIL:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_BACK_FUNC:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_BACK_PASS_DEPTH_FAIL:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_BACK_PASS_DEPTH_PASS:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_BACK_REF:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.STENCIL_BACK_VALUE_MASK:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_BACK_WRITEMASK:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_BITS:
                    return this.m_framebufferBinding == null && !this.m_attributes.stencil ? intZero : this.getIntParameter(pname);
                case GraphicsContext3D.STENCIL_CLEAR_VALUE:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.STENCIL_FAIL:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_FUNC:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_PASS_DEPTH_FAIL:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_PASS_DEPTH_PASS:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_REF:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.STENCIL_TEST:
                    return this.getBooleanParameter(pname);
                case GraphicsContext3D.STENCIL_VALUE_MASK:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.STENCIL_WRITEMASK:
                    return this.getUnsignedIntParameter(pname);
                case GraphicsContext3D.SUBPIXEL_BITS:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.TEXTURE_BINDING_2D:
                    return this.m_textureUnits[this.m_activeTextureUnit].TextureBinding;
                case GraphicsContext3D.TEXTURE_BINDING_CUBE_MAP:
                    return this.m_textureUnits[this.m_activeTextureUnit].textureCubeMapBinding;
                case GraphicsContext3D.UNPACK_ALIGNMENT:
                    return this.getIntParameter(pname);
                case GraphicsContext3D.UNPACK_FLIP_Y_WEBGL:
                    return this.m_unpackFlipY;
                case GraphicsContext3D.UNPACK_PREMULTIPLY_ALPHA_WEBGL:
                    return this.m_unpackPremultiplyAlpha;
                case GraphicsContext3D.UNPACK_COLORSPACE_CONVERSION_WEBGL:
                    return this.m_unpackColorspaceConversion;
                case GraphicsContext3D.VENDOR:
                    return "WebKit";
                case GraphicsContext3D.VERSION:
                    return "WebGL 1.0 (" + this.m_context.getString(GraphicsContext3D.VERSION) + ")";
                case GraphicsContext3D.VIEWPORT:
                    return this.getWebGLIntArrayParameter(pname);
                case Extensions3D.FRAGMENT_SHADER_DERIVATIVE_HINT_OES: // OES_standard_derivatives
                    if (this.m_oesStandardDerivatives != null)
                    {
                        return this.getUnsignedIntParameter(Extensions3D.FRAGMENT_SHADER_DERIVATIVE_HINT_OES);
                    }
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getParameter", "invalid parameter name, OES_standard_derivatives not enabled");
                    return null;
                case WebGLDebugRendererInfo.UNMASKED_RENDERER_WEBGL:
                    if (this.m_webglDebugRendererInfo != null)
                    {
                        return this.m_context.getString(GraphicsContext3D.RENDERER);
                    }
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getParameter", "invalid parameter name, WEBGL_debug_renderer_info not enabled");
                    return null;
                case WebGLDebugRendererInfo.UNMASKED_VENDOR_WEBGL:
                    if (this.m_webglDebugRendererInfo != null)
                    {
                        return this.m_context.getString(GraphicsContext3D.VENDOR);
                    }
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getParameter", "invalid parameter name, WEBGL_debug_renderer_info not enabled");
                    return null;
                case Extensions3D.VERTEX_ARRAY_BINDING_OES: // OES_vertex_array_object
                    if (this.m_oesVertexArrayObject != null)
                    {
                        return !this.m_boundVertexArrayObject.isDefaultObject() ? this.m_boundVertexArrayObject : null;
                    }
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getParameter", "invalid parameter name, OES_vertex_array_object not enabled");
                    return null;
                case Extensions3D.MAX_TEXTURE_MAX_ANISOTROPY_EXT: // EXT_texture_filter_anisotropic
                    if (this.m_extTextureFilterAnisotropic != null)
                    {
                        return this.getUnsignedIntParameter(Extensions3D.MAX_TEXTURE_MAX_ANISOTROPY_EXT);
                    }
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getParameter", "invalid parameter name, EXT_texture_filter_anisotropic not enabled");
                    return null;
                case Extensions3D.MAX_COLOR_ATTACHMENTS_EXT: // EXT_draw_buffers BEGIN
                    if (this.m_webglDrawBuffers != null)
                    {
                        return this.getMaxColorAttachments();
                    }
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getParameter", "invalid parameter name, WEBGL_draw_buffers not enabled");
                    return null;
                case Extensions3D.MAX_DRAW_BUFFERS_EXT:
                    if (this.m_webglDrawBuffers != null)
                    {
                        return this.getMaxDrawBuffers();
                    }
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getParameter", "invalid parameter name, WEBGL_draw_buffers not enabled");
                    return null;
                default:
                    if (this.m_webglDrawBuffers != null && pname >= Extensions3D.DRAW_BUFFER0_EXT && pname < Extensions3D.DRAW_BUFFER0_EXT + this.getMaxDrawBuffers())
                    {
                        int value;
                        if (this.m_framebufferBinding != null)
                        {
                            value = (int)this.m_framebufferBinding.getDrawBuffer(pname);
                        }
                        else // emulated backbuffer
                        {
                            value = (int)this.m_backDrawBuffer;
                        }
                        return value;
                    }
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getParameter", "invalid parameter name");
                    return null;
            }
        }

        public dynamic getProgramParameter(WebGLProgram program, GLenum pname)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("getProgramParameter", program))
            {
                return null;
            }

            int value;
            switch (pname)
            {
                case GraphicsContext3D.DELETE_STATUS:
                    return program.isDeleted();
                case GraphicsContext3D.VALIDATE_STATUS:
                    this.m_context.getProgramiv(objectOrZero(program), pname, out value);
                    return value != 0;
                case GraphicsContext3D.LINK_STATUS:
                    return program.getLinkStatus();
                case GraphicsContext3D.ATTACHED_SHADERS:
                    this.m_context.getProgramiv(objectOrZero(program), pname, out value);
                    return value;
                case GraphicsContext3D.ACTIVE_ATTRIBUTES:
                case GraphicsContext3D.ACTIVE_UNIFORMS:
                    this.m_context.getNonBuiltInActiveSymbolCount(objectOrZero(program), pname, out value);
                    return value;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getProgramParameter", "invalid parameter name");
                    return null;
            }
        }

        public String getProgramInfoLog(WebGLProgram program)
        {
            if (this.isContextLostOrPending())
            {
                return String.Empty;
            }
            if (!this.m_validation.validateWebGLObject("getProgramInfoLog", program))
            {
                return String.Empty;
            }
            return ensureNotNull(this.m_context.getProgramInfoLog(objectOrZero(program)));
        }

        public dynamic getRenderbufferParameter(GLenum target, GLenum pname)
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            if (target != GraphicsContext3D.RENDERBUFFER)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getRenderbufferParameter", "invalid target");
                return null;
            }
            if (this.m_renderbufferBinding == null || this.m_renderbufferBinding.obj() == 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "getRenderbufferParameter", "no renderbuffer bound");
                return null;
            }

            int value;
            if (this.m_renderbufferBinding.getInternalFormat() == GraphicsContext3D.DEPTH_STENCIL && !this.m_renderbufferBinding.isValid())
            {
                switch (pname)
                {
                    case GraphicsContext3D.RENDERBUFFER_WIDTH:
                        value = this.m_renderbufferBinding.getWidth();
                        break;
                    case GraphicsContext3D.RENDERBUFFER_HEIGHT:
                        value = this.m_renderbufferBinding.getHeight();
                        break;
                    case GraphicsContext3D.RENDERBUFFER_RED_SIZE:
                    case GraphicsContext3D.RENDERBUFFER_GREEN_SIZE:
                    case GraphicsContext3D.RENDERBUFFER_BLUE_SIZE:
                    case GraphicsContext3D.RENDERBUFFER_ALPHA_SIZE:
                        value = 0;
                        break;
                    case GraphicsContext3D.RENDERBUFFER_DEPTH_SIZE:
                        value = 24;
                        break;
                    case GraphicsContext3D.RENDERBUFFER_STENCIL_SIZE:
                        value = 8;
                        break;
                    case GraphicsContext3D.RENDERBUFFER_INTERNAL_FORMAT:
                        return this.m_renderbufferBinding.getInternalFormat();
                    default:
                        this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getRenderbufferParameter", "invalid parameter name");
                        return null;
                }
                return value;
            }

            switch (pname)
            {
                case GraphicsContext3D.RENDERBUFFER_WIDTH:
                case GraphicsContext3D.RENDERBUFFER_HEIGHT:
                case GraphicsContext3D.RENDERBUFFER_RED_SIZE:
                case GraphicsContext3D.RENDERBUFFER_GREEN_SIZE:
                case GraphicsContext3D.RENDERBUFFER_BLUE_SIZE:
                case GraphicsContext3D.RENDERBUFFER_ALPHA_SIZE:
                case GraphicsContext3D.RENDERBUFFER_DEPTH_SIZE:
                case GraphicsContext3D.RENDERBUFFER_STENCIL_SIZE:
                    this.m_context.getRenderbufferParameteriv(target, pname, out value);
                    return value;
                case GraphicsContext3D.RENDERBUFFER_INTERNAL_FORMAT:
                    return this.m_renderbufferBinding.getInternalFormat();
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getRenderbufferParameter", "invalid parameter name");
                    return null;
            }
        }

        public dynamic getShaderParameter(WebGLShader shader, GLenum pname)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("getShaderParameter", shader))
            {
                return null;
            }
            int value;
            switch (pname)
            {
                case GraphicsContext3D.DELETE_STATUS:
                    return shader.isDeleted();
                case GraphicsContext3D.COMPILE_STATUS:
                    this.m_context.getShaderiv(objectOrZero(shader), pname, out value);
                    return value != 0;
                case GraphicsContext3D.SHADER_TYPE:
                    this.m_context.getShaderiv(objectOrZero(shader), pname, out value);
                    return (uint)value;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getShaderParameter", "invalid parameter name");
                    return null;
            }
        }

        public String getShaderInfoLog(WebGLShader shader)
        {
            if (this.isContextLostOrPending())
            {
                return String.Empty;
            }
            if (!this.m_validation.validateWebGLObject("getShaderInfoLog", shader))
            {
                return "";
            }
            return ensureNotNull(this.m_context.getShaderInfoLog(objectOrZero(shader)));
        }

        public WebGLShaderPrecisionFormat getShaderPrecisionFormat(GLenum shaderType, GLenum precisionType)
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            switch (shaderType)
            {
                case GraphicsContext3D.VERTEX_SHADER:
                case GraphicsContext3D.FRAGMENT_SHADER:
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getShaderPrecisionFormat", "invalid shader type");
                    return null;
            }
            switch (precisionType)
            {
                case GraphicsContext3D.LOW_FLOAT:
                case GraphicsContext3D.MEDIUM_FLOAT:
                case GraphicsContext3D.HIGH_FLOAT:
                case GraphicsContext3D.LOW_INT:
                case GraphicsContext3D.MEDIUM_INT:
                case GraphicsContext3D.HIGH_INT:
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getShaderPrecisionFormat", "invalid precision type");
                    return null;
            }

            var range = new int[2];
            GLint precision;
            this.m_context.getShaderPrecisionFormat(shaderType, precisionType, range, out precision);
            return new WebGLShaderPrecisionFormat(range[0], range[1], precision);
        }

        public String getShaderSource(WebGLShader shader)
        {
            if (this.isContextLostOrPending())
            {
                return String.Empty;
            }
            if (!this.m_validation.validateWebGLObject("getShaderSource", shader))
            {
                return "";
            }
            return ensureNotNull(shader.getSource());
        }

        public String[] getSupportedExtensions()
        {
            var result = new List<string>();

            if (this.m_context.getExtensions().supports("GL_OES_texture_float"))
            {
                result.Add("OES_texture_float");
            }
            if (this.m_context.getExtensions().supports("GL_OES_texture_float_linear"))
            {
                result.Add("OES_texture_float_linear");
            }
            if (this.m_context.getExtensions().supports("GL_OES_texture_half_float"))
            {
                result.Add("OES_texture_half_float");
            }
            if (this.m_context.getExtensions().supports("GL_OES_texture_half_float_linear"))
            {
                result.Add("OES_texture_half_float_linear");
            }
            if (this.m_context.getExtensions().supports("GL_OES_standard_derivatives"))
            {
                result.Add("OES_standard_derivatives");
            }
            if (this.m_context.getExtensions().supports("GL_EXT_texture_filter_anisotropic"))
            {
                result.Add("WEBKIT_EXT_texture_filter_anisotropic");
            }
            if (this.m_context.getExtensions().supports("GL_OES_vertex_array_object"))
            {
                result.Add("OES_vertex_array_object");
            }
            if (this.m_context.getExtensions().supports("GL_OES_element_index_uint"))
            {
                result.Add("OES_element_index_uint");
            }
            result.Add("WEBGL_lose_context");
            if (WebGLCompressedTextureATC.supported(this))
            {
                result.Add("WEBKIT_WEBGL_compressed_texture_atc");
            }
            if (WebGLCompressedTexturePVRTC.supported(this))
            {
                result.Add("WEBKIT_WEBGL_compressed_texture_pvrtc");
            }
            if (WebGLCompressedTextureS3TC.supported(this))
            {
                result.Add("WEBGL_compressed_texture_s3tc");
            }
            if (WebGLDepthTexture.supported(this.graphicsContext3D()))
            {
                result.Add("WEBGL_depth_texture");
            }
            if (this.supportsDrawBuffers())
            {
                result.Add("WEBGL_draw_buffers");
            }
            if (ANGLEInstancedArrays.supported(this))
            {
                result.Add("ANGLE_instanced_arrays");
            }

            if (allowPrivilegedExtensions())
            {
                if (this.m_context.getExtensions().supports("GL_ANGLE_translated_shader_source"))
                {
                    result.Add("WEBGL_debug_shaders");
                }
                result.Add("WEBGL_debug_renderer_info");
            }

            return result.ToArray();
        }

        public dynamic getTexParameter(GLenum target, GLenum pname)
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }
            var tex = this.m_validation.validateTextureBinding("getTexParameter", target, false);
            if (tex == null)
            {
                return null;
            }
            var value = new int[1];
            switch (pname)
            {
                case GraphicsContext3D.TEXTURE_MAG_FILTER:
                case GraphicsContext3D.TEXTURE_MIN_FILTER:
                case GraphicsContext3D.TEXTURE_WRAP_S:
                case GraphicsContext3D.TEXTURE_WRAP_T:
                    this.m_context.getTexParameteriv(target, pname, value);
                    return (uint)value[0];
                case Extensions3D.TEXTURE_MAX_ANISOTROPY_EXT:
                    if (this.m_extTextureFilterAnisotropic != null)
                    {
                        this.m_context.getTexParameteriv(target, pname, value);
                        return (uint)value[0];
                    }
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getTexParameter", "invalid parameter name, EXT_texture_filter_anisotropic not enabled");
                    return null;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getTexParameter", "invalid parameter name");
                    return null;
            }
        }

        public dynamic getUniform(WebGLProgram program, WebGLUniformLocation uniformLocation)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("getUniform", program))
            {
                return null;
            }
            if (uniformLocation == null || uniformLocation.program() != program)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "getUniform", "no uniformlocation or not valid for this program");
                return null;
            }
            var location = uniformLocation.location();

            GLenum baseType;
            uint length;
            switch (uniformLocation.type())
            {
                case GraphicsContext3D.BOOL:
                    baseType = GraphicsContext3D.BOOL;
                    length = 1;
                    break;
                case GraphicsContext3D.BOOL_VEC2:
                    baseType = GraphicsContext3D.BOOL;
                    length = 2;
                    break;
                case GraphicsContext3D.BOOL_VEC3:
                    baseType = GraphicsContext3D.BOOL;
                    length = 3;
                    break;
                case GraphicsContext3D.BOOL_VEC4:
                    baseType = GraphicsContext3D.BOOL;
                    length = 4;
                    break;
                case GraphicsContext3D.INT:
                    baseType = GraphicsContext3D.INT;
                    length = 1;
                    break;
                case GraphicsContext3D.INT_VEC2:
                    baseType = GraphicsContext3D.INT;
                    length = 2;
                    break;
                case GraphicsContext3D.INT_VEC3:
                    baseType = GraphicsContext3D.INT;
                    length = 3;
                    break;
                case GraphicsContext3D.INT_VEC4:
                    baseType = GraphicsContext3D.INT;
                    length = 4;
                    break;
                case GraphicsContext3D.FLOAT:
                    baseType = GraphicsContext3D.FLOAT;
                    length = 1;
                    break;
                case GraphicsContext3D.FLOAT_VEC2:
                    baseType = GraphicsContext3D.FLOAT;
                    length = 2;
                    break;
                case GraphicsContext3D.FLOAT_VEC3:
                    baseType = GraphicsContext3D.FLOAT;
                    length = 3;
                    break;
                case GraphicsContext3D.FLOAT_VEC4:
                    baseType = GraphicsContext3D.FLOAT;
                    length = 4;
                    break;
                case GraphicsContext3D.FLOAT_MAT2:
                    baseType = GraphicsContext3D.FLOAT;
                    length = 4;
                    break;
                case GraphicsContext3D.FLOAT_MAT3:
                    baseType = GraphicsContext3D.FLOAT;
                    length = 9;
                    break;
                case GraphicsContext3D.FLOAT_MAT4:
                    baseType = GraphicsContext3D.FLOAT;
                    length = 16;
                    break;
                case GraphicsContext3D.SAMPLER_2D:
                case GraphicsContext3D.SAMPLER_CUBE:
                    baseType = GraphicsContext3D.INT;
                    length = 1;
                    break;
                default:
                    // Can't handle this type
                    this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "getUniform", "unhandled type");
                    return null;
            }
            switch (baseType)
            {
                case GraphicsContext3D.FLOAT:
                {
                    var value = new GLfloat[16];
                    if (this.m_isRobustnessEXTSupported)
                    {
                        var handle = GCHandle.Alloc(value, GCHandleType.Pinned);
                        this.m_context.getExtensions().getnUniformfvEXT(objectOrZero(program), location, 16 * sizeof(GLfloat), handle.AddrOfPinnedObject());
                        handle.Free();
                    }
                    else
                    {
                        this.m_context.getUniformfv(objectOrZero(program), location, value);
                    }
                    if (length == 1)
                    {
                        return value[0];
                    }
                    return Float32Array.create(value, length);
                }
                case GraphicsContext3D.INT:
                {
                    var value = new GLint[4];
                    if (this.m_isRobustnessEXTSupported)
                    {
                        var handle = GCHandle.Alloc(value, GCHandleType.Pinned);
                        this.m_context.getExtensions().getnUniformivEXT(objectOrZero(program), location, 4 * sizeof(GLint), handle.AddrOfPinnedObject());
                        handle.Free();
                    }
                    else
                    {
                        this.m_context.getUniformiv(objectOrZero(program), location, value);
                    }
                    if (length == 1)
                    {
                        return value[0];
                    }
                    return Int32Array.create(value, length);
                }
                case GraphicsContext3D.BOOL:
                {
                    var value = new[] {0, 0, 0, 0};
                    if (this.m_isRobustnessEXTSupported)
                    {
                        var handle = GCHandle.Alloc(value, GCHandleType.Pinned);
                        this.m_context.getExtensions().getnUniformivEXT(objectOrZero(program), location, 4 * sizeof(GLint), handle.AddrOfPinnedObject());
                        handle.Free();
                    }
                    else
                    {
                        this.m_context.getUniformiv(objectOrZero(program), location, value);
                    }
                    if (length > 1)
                    {
                        var boolValue = new bool[length];
                        for (var j = 0; j < length; j++)
                        {
                            boolValue[j] = value[j] != 0;
                        }
                        return boolValue;
                    }
                    return value[0] != 0;
                }
            }

            // If we get here, something went wrong in our unfortunately complex logic above
            this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "getUniform", "unknown error");
            return null;
        }

        public WebGLUniformLocation getUniformLocation(WebGLProgram program, String name)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("getUniformLocation", program))
            {
                return null;
            }
            if (!this.m_validation.validateLocationLength("getUniformLocation", name))
            {
                return null;
            }
            if (!this.m_validation.validateString("getUniformLocation", name))
            {
                return null;
            }
            if (isPrefixReserved(name))
            {
                return null;
            }
            if (!program.getLinkStatus())
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "getUniformLocation", "program not linked");
                return null;
            }
            var uniformLocation = this.m_context.getUniformLocation(objectOrZero(program), name);
            if (uniformLocation == -1)
            {
                return null;
            }

            int activeUniforms;
            this.m_context.getNonBuiltInActiveSymbolCount(objectOrZero(program), GraphicsContext3D.ACTIVE_UNIFORMS, out activeUniforms);
            for (var i = 0; i < activeUniforms; i++)
            {
                var info = new ActiveInfo();
                if (!this.m_context.getActiveUniform(objectOrZero(program), (uint)i, info))
                {
                    return null;
                }
                // Strip "[0]" from the name if it's an array.
                if (info.name.EndsWith("[0]"))
                {
                    info.name = info.name.Substring(0, info.name.Length - 3);
                }
                // If it's an array, we need to iterate through each element, appending "[index]" to the name.
                for (var index = 0; index < info.size; ++index)
                {
                    var uniformName = info.name + "[" + index + "]";

                    if (name == uniformName || name == info.name)
                    {
                        return new WebGLUniformLocation(program, uniformLocation, info.type);
                    }
                }
            }
            return null;
        }

        public dynamic getVertexAttrib(GLuint index, GLenum pname)
        {
            if (this.isContextLostOrPending())
            {
                return null;
            }

            if (index >= this.m_maxVertexAttribs)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "getVertexAttrib", "index out of range");
                return null;
            }

            var state = this.m_boundVertexArrayObject.getVertexAttribState((int)index);

            if (this.m_angleInstancedArrays != null && pname == GraphicsContext3D.VERTEX_ATTRIB_ARRAY_DIVISOR_ANGLE)
            {
                return state.divisor;
            }

            switch (pname)
            {
                case GraphicsContext3D.VERTEX_ATTRIB_ARRAY_BUFFER_BINDING:
                    return state.bufferBinding == null || state.bufferBinding.obj() == 0 ? null : state.bufferBinding;
                case GraphicsContext3D.VERTEX_ATTRIB_ARRAY_ENABLED:
                    return state.enabled;
                case GraphicsContext3D.VERTEX_ATTRIB_ARRAY_NORMALIZED:
                    return state.normalized;
                case GraphicsContext3D.VERTEX_ATTRIB_ARRAY_SIZE:
                    return state.size;
                case GraphicsContext3D.VERTEX_ATTRIB_ARRAY_STRIDE:
                    return state.originalStride;
                case GraphicsContext3D.VERTEX_ATTRIB_ARRAY_TYPE:
                    return state.type;
                case GraphicsContext3D.CURRENT_VERTEX_ATTRIB:
                    return new Float32Array(this.m_vertexAttribValue[index].value);
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "getVertexAttrib", "invalid parameter name");
                    return null;
            }
        }

        public long getVertexAttribOffset(GLuint index, GLenum pname)
        {
            if (this.isContextLostOrPending())
            {
                return 0;
            }
            return this.m_context.getVertexAttribOffset(index, pname).ToInt64();
        }

        public void hint(GLenum target, GLenum mode)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            var isValid = false;
            switch (target)
            {
                case GraphicsContext3D.GENERATE_MIPMAP_HINT:
                    isValid = true;
                    break;
                case Extensions3D.FRAGMENT_SHADER_DERIVATIVE_HINT_OES: // OES_standard_derivatives
                    if (this.m_oesStandardDerivatives != null)
                    {
                        isValid = true;
                    }
                    break;
            }
            if (!isValid)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "hint", "invalid target");
                return;
            }
            this.m_context.hint(target, mode);
        }

        public bool isBuffer(WebGLBuffer buffer)
        {
            if (buffer == null || this.isContextLostOrPending())
            {
                return false;
            }

            if (!buffer.hasEverBeenBound())
            {
                return false;
            }

            return this.m_context.isBuffer(buffer.obj());
        }

        public bool isContextLost()
        {
            return this.m_contextLost;
        }

        public bool isEnabled(GLenum cap)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateCapability("isEnabled", cap))
            {
                return false;
            }
            if (cap == GraphicsContext3D.STENCIL_TEST)
            {
                return this.m_stencilEnabled;
            }
            return this.m_context.isEnabled(cap);
        }

        public bool isFramebuffer(WebGLFramebuffer framebuffer)
        {
            if (framebuffer == null || this.isContextLostOrPending())
            {
                return false;
            }

            if (!framebuffer.hasEverBeenBound())
            {
                return false;
            }

            return this.m_context.isFramebuffer(framebuffer.obj());
        }

        public bool isProgram(WebGLProgram program)
        {
            if (program == null || program.obj() == 0 || this.isContextLostOrPending())
            {
                return false;
            }

            return this.m_context.isProgram(program.obj());
        }

        public bool isRenderbuffer(WebGLRenderbuffer renderbuffer)
        {
            if (renderbuffer == null || this.isContextLostOrPending())
            {
                return false;
            }

            if (!renderbuffer.hasEverBeenBound())
            {
                return false;
            }

            return this.m_context.isRenderbuffer(renderbuffer.obj());
        }

        public bool isShader(WebGLShader shader)
        {
            if (shader == null || shader.obj() == 0 || this.isContextLostOrPending())
            {
                return false;
            }

            return this.m_context.isShader(shader.obj());
        }

        public bool isTexture(WebGLTexture texture)
        {
            if (texture == null || this.isContextLostOrPending())
            {
                return false;
            }

            if (!texture.hasEverBeenBound())
            {
                return false;
            }

            return this.m_context.isTexture(texture.obj());
        }

        public void lineWidth(GLfloat width)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.lineWidth(width);
        }

        public void linkProgram(WebGLProgram program)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("linkProgram", program))
            {
                return;
            }

            this.m_context.linkProgram(objectOrZero(program));
            program.increaseLinkCount();
        }

        public void pixelStorei(GLenum pname, GLint param)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            switch (pname)
            {
                case GraphicsContext3D.UNPACK_FLIP_Y_WEBGL:
                    this.m_unpackFlipY = param != 0;
                    break;
                case GraphicsContext3D.UNPACK_PREMULTIPLY_ALPHA_WEBGL:
                    this.m_unpackPremultiplyAlpha = param != 0;
                    break;
                case GraphicsContext3D.UNPACK_COLORSPACE_CONVERSION_WEBGL:
                    if (param == GraphicsContext3D.BROWSER_DEFAULT_WEBGL || param == GraphicsContext3D.NONE)
                    {
                        this.m_unpackColorspaceConversion = (GLenum)param;
                    }
                    else
                    {
                        this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "pixelStorei", "invalid parameter for UNPACK_COLORSPACE_CONVERSION_WEBGL");
                        return;
                    }
                    break;
                case GraphicsContext3D.PACK_ALIGNMENT:
                case GraphicsContext3D.UNPACK_ALIGNMENT:
                    if (param == 1 || param == 2 || param == 4 || param == 8)
                    {
                        if (pname == GraphicsContext3D.PACK_ALIGNMENT)
                        {
                            this.m_packAlignment = param;
                        }
                        else // GraphicsContext3D.UNPACK_ALIGNMENT:
                        {
                            this.m_unpackAlignment = param;
                        }
                        this.m_context.pixelStorei(pname, param);
                    }
                    else
                    {
                        this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "pixelStorei", "invalid parameter for alignment");
                        return;
                    }
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "pixelStorei", "invalid parameter name");
                    return;
            }
        }

        public void polygonOffset(GLfloat factor, GLfloat units)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.polygonOffset(factor, units);
        }

        public void readPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, ArrayBufferView pixels)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }

            if (pixels == null)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "readPixels", "no destination ArrayBufferView");
                return;
            }
            switch (format)
            {
                case GraphicsContext3D.ALPHA:
                case GraphicsContext3D.RGB:
                case GraphicsContext3D.RGBA:
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "readPixels", "invalid format");
                    return;
            }
            switch (type)
            {
                case GraphicsContext3D.UNSIGNED_BYTE:
                case GraphicsContext3D.UNSIGNED_SHORT_5_6_5:
                case GraphicsContext3D.UNSIGNED_SHORT_4_4_4_4:
                case GraphicsContext3D.UNSIGNED_SHORT_5_5_5_1:
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "readPixels", "invalid type");
                    return;
            }
            if (format != GraphicsContext3D.RGBA || type != GraphicsContext3D.UNSIGNED_BYTE)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "readPixels", "format not RGBA or type not UNSIGNED_BYTE");
                return;
            }
            // Validate array type against pixel type.
            if (!(pixels is Uint8Array))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "readPixels", "ArrayBufferView not Uint8Array");
                return;
            }
            var reason = "framebuffer incomplete";
            if (this.m_framebufferBinding != null && !this.m_framebufferBinding.onAccess(this.graphicsContext3D(), !this.isResourceSafe(), ref reason))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_FRAMEBUFFER_OPERATION, "readPixels", reason);
                return;
            }
            // Calculate array size, taking into consideration of PACK_ALIGNMENT.
            if (!this.m_isRobustnessEXTSupported)
            {
                int totalBytesRequired;
                int padding;
                var error = GraphicsContext3D.computeImageSizeInBytes(format, type, width, height, this.m_packAlignment, out totalBytesRequired, out padding);
                if (error != GraphicsContext3D.NO_ERROR)
                {
                    this.synthesizeGLError(error, "readPixels", "invalid dimensions");
                    return;
                }
                if (pixels.byteLength < totalBytesRequired)
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "readPixels", "ArrayBufferView not large enough for dimensions");
                    return;
                }
            }

            var data = pixels.buffer.@lock() + pixels.byteOffset;
            if (this.m_isRobustnessEXTSupported)
            {
                this.m_context.getExtensions().readnPixelsEXT(x, y, width, height, format, type, pixels.byteLength, data);
            }
            else
            {
                this.m_context.readPixels(x, y, width, height, format, type, data);
            }
            pixels.buffer.unlock();
        }

        public void renderbufferStorage(GLenum target, GLenum internalformat, GLsizei width, GLsizei height)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (target != GraphicsContext3D.RENDERBUFFER)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "renderbufferStorage", "invalid target");
                return;
            }
            if (this.m_renderbufferBinding == null || this.m_renderbufferBinding.obj() == 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "renderbufferStorage", "no bound renderbuffer");
                return;
            }
            if (!this.m_validation.validateSize("renderbufferStorage", width, height))
            {
                return;
            }
            switch (internalformat)
            {
                case GraphicsContext3D.DEPTH_COMPONENT16:
                case GraphicsContext3D.RGBA4:
                case GraphicsContext3D.RGB5_A1:
                case GraphicsContext3D.RGB565:
                case GraphicsContext3D.STENCIL_INDEX8:
                    this.m_context.renderbufferStorage(target, internalformat, width, height);
                    this.m_renderbufferBinding.setInternalFormat(internalformat);
                    this.m_renderbufferBinding.setIsValid(true);
                    this.m_renderbufferBinding.setSize(width, height);
                    break;
                case GraphicsContext3D.DEPTH_STENCIL:
                    if (this.isDepthStencilSupported())
                    {
                        this.m_context.renderbufferStorage(target, Extensions3D.DEPTH24_STENCIL8, width, height);
                    }
                    this.m_renderbufferBinding.setSize(width, height);
                    this.m_renderbufferBinding.setIsValid(this.isDepthStencilSupported());
                    this.m_renderbufferBinding.setInternalFormat(internalformat);
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "renderbufferStorage", "invalid internalformat");
                    return;
            }
            this.applyStencilTest();
        }

        public void sampleCoverage(GLfloat value, bool invert)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.sampleCoverage(value, invert);
        }

        public void scissor(GLint x, GLint y, GLsizei width, GLsizei height)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (!this.m_validation.validateSize("scissor", width, height))
            {
                return;
            }
            this.m_context.scissor(x, y, width, height);
        }

        public void shaderSource(WebGLShader shader, String @string)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("shaderSource", shader))
            {
                return;
            }
            var stringWithoutComments = new StripComments(@string).result();
            if (!this.m_validation.validateString("shaderSource", stringWithoutComments))
            {
                return;
            }
            shader.setSource(@string);
            this.m_context.shaderSource(objectOrZero(shader), stringWithoutComments);
        }

        public void stencilFunc(GLenum func, GLint @ref, GLuint mask)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (!this.m_validation.validateStencilFunc("stencilFunc", func))
            {
                return;
            }
            this.m_stencilFuncRef = @ref;
            this.m_stencilFuncRefBack = @ref;
            this.m_stencilFuncMask = mask;
            this.m_stencilFuncMaskBack = mask;
            this.m_context.stencilFunc(func, @ref, mask);
        }

        public void stencilFuncSeparate(GLenum face, GLenum func, GLint @ref, GLuint mask)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (!this.m_validation.validateStencilFunc("stencilFuncSeparate", func))
            {
                return;
            }
            switch (face)
            {
                case GraphicsContext3D.FRONT_AND_BACK:
                    this.m_stencilFuncRef = @ref;
                    this.m_stencilFuncRefBack = @ref;
                    this.m_stencilFuncMask = mask;
                    this.m_stencilFuncMaskBack = mask;
                    break;
                case GraphicsContext3D.FRONT:
                    this.m_stencilFuncRef = @ref;
                    this.m_stencilFuncMask = mask;
                    break;
                case GraphicsContext3D.BACK:
                    this.m_stencilFuncRefBack = @ref;
                    this.m_stencilFuncMaskBack = mask;
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "stencilFuncSeparate", "invalid face");
                    return;
            }
            this.m_context.stencilFuncSeparate(face, func, @ref, mask);
        }

        public void stencilMask(GLuint mask)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_stencilMask = mask;
            this.m_stencilMaskBack = mask;
            this.m_context.stencilMask(mask);
        }

        public void stencilMaskSeparate(GLenum face, GLuint mask)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            switch (face)
            {
                case GraphicsContext3D.FRONT_AND_BACK:
                    this.m_stencilMask = mask;
                    this.m_stencilMaskBack = mask;
                    break;
                case GraphicsContext3D.FRONT:
                    this.m_stencilMask = mask;
                    break;
                case GraphicsContext3D.BACK:
                    this.m_stencilMaskBack = mask;
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "stencilMaskSeparate", "invalid face");
                    return;
            }
            this.m_context.stencilMaskSeparate(face, mask);
        }

        public void stencilOp(GLenum fail, GLenum zfail, GLenum zpass)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.stencilOp(fail, zfail, zpass);
        }

        public void stencilOpSeparate(GLenum face, GLenum fail, GLenum zfail, GLenum zpass)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            this.m_context.stencilOpSeparate(face, fail, zfail, zpass);
        }

        public void texImage2D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, ArrayBufferView pixels)
        {
            if (this.isContextLostOrPending()
                || !this.m_validation.validateTexFuncData("texImage2D", level, width, height, format, type, pixels, NullDisposition.NullAllowed)
                || !this.m_validation.validateTexFunc("texImage2D", TexFuncValidationFunctionType.NotTexSubImage2D, TexFuncValidationSourceType.SourceArrayBufferView, target, level, internalformat, width, height, border, format, type, 0, 0))
            {
                return;
            }

            var changeUnpackAlignment = false;
            byte[] data = null;
            if (pixels != null)
            {
                data = new byte[pixels.byteLength];
                for (var i = 0; i < data.Length; i++)
                {
                    data[i] = pixels.buffer.data[pixels.byteOffset + i];
                }
                if (this.m_unpackFlipY || this.m_unpackPremultiplyAlpha)
                {
                    byte[] tempData;
                    if (!GraphicsContext3D.extractTextureData(width, height, format, type, this.m_unpackAlignment, this.m_unpackFlipY, this.m_unpackPremultiplyAlpha, data, out tempData))
                    {
                        return;
                    }
                    data = tempData;
                    changeUnpackAlignment = true;
                }
            }

            if (changeUnpackAlignment)
            {
                this.m_context.pixelStorei(GraphicsContext3D.UNPACK_ALIGNMENT, 1);
            }

            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            this.texImage2DBase(target, level, internalformat, width, height, border, format, type, handle.AddrOfPinnedObject());
            handle.Free();

            if (changeUnpackAlignment)
            {
                this.m_context.pixelStorei(GraphicsContext3D.UNPACK_ALIGNMENT, this.m_unpackAlignment);
            }
        }

        public void texImage2D(GLenum target, GLint level, GLenum internalformat, GLenum format, GLenum type, ImageData pixels)
        {
            if (this.isContextLostOrPending() || pixels == null ||
                !this.m_validation.validateTexFunc("texImage2D", TexFuncValidationFunctionType.NotTexSubImage2D, TexFuncValidationSourceType.SourceImageData, target, level, internalformat, pixels.width(), pixels.height(), 0, format, type, 0, 0))
            {
                return;
            }
            byte[] data = null;
            var needConversion = true;
            // The data from ImageData is always of format RGBA8.
            // No conversion is needed if destination format is RGBA and type is USIGNED_BYTE and no Flip or Premultiply operation is required.
            if (!this.m_unpackFlipY && !this.m_unpackPremultiplyAlpha && format == GraphicsContext3D.RGBA && type == GraphicsContext3D.UNSIGNED_BYTE)
            {
                needConversion = false;
            }
            else
            {
                if (!GraphicsContext3D.extractImageData(pixels, format, type, this.m_unpackFlipY, this.m_unpackPremultiplyAlpha, out data))
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "texImage2D", "bad image data");
                    return;
                }
            }
            if (this.m_unpackAlignment != 1)
            {
                this.m_context.pixelStorei(GraphicsContext3D.UNPACK_ALIGNMENT, 1);
            }
            if (needConversion)
            {
                var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                this.texImage2DBase(target, level, internalformat, pixels.width(), pixels.height(), 0, format, type, handle.AddrOfPinnedObject());
                handle.Free();
            }
            else
            {
                this.texImage2DBase(target, level, internalformat, pixels.width(), pixels.height(), 0, format, type, pixels.data().buffer.@lock());
                pixels.data().buffer.unlock();
            }
            if (this.m_unpackAlignment != 1)
            {
                this.m_context.pixelStorei(GraphicsContext3D.UNPACK_ALIGNMENT, this.m_unpackAlignment);
            }
        }

        public void texImage2D(GLenum target, GLint level, GLenum internalformat, GLenum format, GLenum type, HTMLImageElement element)
        {
            throw new NotImplementedException();
        }

        public void texImage2D(GLenum target, GLint level, GLenum internalformat, GLenum format, GLenum type, HTMLCanvasElement element)
        {
            throw new NotImplementedException();
        }

        public void texImage2D(uint target, int level, uint internalformat, uint format, uint type, HTMLVideoElement video)
        {
            throw new NotImplementedException();
        }

        public void texParameterf(GLenum target, GLenum pname, GLfloat param)
        {
            this.texParameter(target, pname, param, 0, true);
        }

        public void texParameteri(GLenum target, GLenum pname, GLint param)
        {
            this.texParameter(target, pname, 0, param, false);
        }

        public void texSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, ArrayBufferView pixels)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateTexFuncData("texSubImage2D", level, width, height, format, type, pixels, NullDisposition.NullNotAllowed)
                || !this.m_validation.validateTexFunc("texSubImage2D", TexFuncValidationFunctionType.TexSubImage2D, TexFuncValidationSourceType.SourceArrayBufferView, target, level, format, width, height, 0, format, type, xoffset, yoffset))
            {
                return;
            }

            var data = new byte[pixels.byteLength];
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = pixels.buffer.data[pixels.byteOffset + i];
            }

            var changeUnpackAlignment = false;
            if (this.m_unpackFlipY || this.m_unpackPremultiplyAlpha)
            {
                byte[] tempData;

                if (!GraphicsContext3D.extractTextureData(width, height, format, type,
                                                          this.m_unpackAlignment,
                                                          this.m_unpackFlipY, this.m_unpackPremultiplyAlpha,
                                                          data,
                                                          out tempData))
                {
                    return;
                }
                data = tempData;
                changeUnpackAlignment = true;
            }
            if (changeUnpackAlignment)
            {
                this.m_context.pixelStorei(GraphicsContext3D.UNPACK_ALIGNMENT, 1);
            }

            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            this.texSubImage2DBase(target, level, xoffset, yoffset, width, height, format, type, handle.AddrOfPinnedObject());
            handle.Free();

            if (changeUnpackAlignment)
            {
                this.m_context.pixelStorei(GraphicsContext3D.UNPACK_ALIGNMENT, this.m_unpackAlignment);
            }
        }

        public void texSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLenum format, GLenum type, ImageData pixels)
        {
            if (this.isContextLostOrPending() || pixels == null ||
                !this.m_validation.validateTexFunc("texSubImage2D", TexFuncValidationFunctionType.TexSubImage2D, TexFuncValidationSourceType.SourceImageData, target, level, format, pixels.width(), pixels.height(), 0, format, type, xoffset, yoffset))
            {
                return;
            }

            byte[] data = null;
            var needConversion = true;
            // The data from ImageData is always of format RGBA8.
            // No conversion is needed if destination format is RGBA and type is USIGNED_BYTE and no Flip or Premultiply operation is required.
            if (format == GraphicsContext3D.RGBA && type == GraphicsContext3D.UNSIGNED_BYTE && !this.m_unpackFlipY && !this.m_unpackPremultiplyAlpha)
            {
                needConversion = false;
            }
            else
            {
                if (!GraphicsContext3D.extractImageData(pixels, format, type, this.m_unpackFlipY, this.m_unpackPremultiplyAlpha, out data))
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "texSubImage2D", "bad image data");
                    return;
                }
            }
            if (this.m_unpackAlignment != 1)
            {
                this.m_context.pixelStorei(GraphicsContext3D.UNPACK_ALIGNMENT, 1);
            }
            var handle = GCHandle.Alloc(needConversion ? data : pixels.data().buffer.data);
            this.texSubImage2DBase(target, level, xoffset, yoffset, pixels.width(), pixels.height(), format, type, handle.AddrOfPinnedObject());
            handle.Free();
            if (this.m_unpackAlignment != 1)
            {
                this.m_context.pixelStorei(GraphicsContext3D.UNPACK_ALIGNMENT, this.m_unpackAlignment);
            }
        }

        public void texSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLenum format, GLenum type, HTMLImageElement data)
        {
            throw new NotImplementedException();
        }

        public void texSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLenum format, GLenum type, HTMLCanvasElement data)
        {
            throw new NotImplementedException();
        }

        public void texSubImage2D(uint target, int level, int xoffset, int yoffset, uint format, uint type, HTMLVideoElement video)
        {
            throw new NotImplementedException();
        }

        public void uniform1f(WebGLUniformLocation location, GLfloat x)
        {
            if (this.isContextLostOrPending() || location == null)
            {
                return;
            }

            if (location.program() != this.m_currentProgram)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "uniform1f", "location not for current program");
                return;
            }

            this.m_context.uniform1f(location.location(), x);
        }

        public void uniform1fv(WebGLUniformLocation location, Float32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform1fv", location, v, 1))
            {
                return;
            }

            this.m_context.uniform1fv(location.location(), v.length, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniform1fv(WebGLUniformLocation location, params GLfloat[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform1fv", location, handle.AddrOfPinnedObject(), v.Length, 1))
            {
                handle.Free();
                return;
            }

            this.m_context.uniform1fv(location.location(), v.Length, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniform1i(WebGLUniformLocation location, GLint x)
        {
            if (this.isContextLostOrPending() || location == null)
            {
                return;
            }

            if (location.program() != this.m_currentProgram)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "uniform1i", "location not for current program");
                return;
            }

            if ((location.type() == GraphicsContext3D.SAMPLER_2D || location.type() == GraphicsContext3D.SAMPLER_CUBE) && x >= this.m_textureUnits.Length)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "uniform1i", "invalid texture unit");
                return;
            }

            this.m_context.uniform1i(location.location(), x);
        }

        public void uniform1iv(WebGLUniformLocation location, Int32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform1iv", location, v, 1))
            {
                return;
            }

            if (location.type() == GraphicsContext3D.SAMPLER_2D || location.type() == GraphicsContext3D.SAMPLER_CUBE)
            {
                for (var i = 0; i < v.length; ++i)
                {
                    if (v[i] >= this.m_textureUnits.Length)
                    {
                        this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "uniform1iv", "invalid texture unit");
                        return;
                    }
                }
            }

            this.m_context.uniform1iv(location.location(), v.length, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniform1iv(WebGLUniformLocation location, params GLint[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform1iv", location, handle.AddrOfPinnedObject(), v.Length, 1))
            {
                handle.Free();
                return;
            }

            if (location.type() == GraphicsContext3D.SAMPLER_2D || location.type() == GraphicsContext3D.SAMPLER_CUBE)
            {
                foreach (var t in v)
                {
                    if (t >= this.m_textureUnits.Length)
                    {
                        this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "uniform1iv", "invalid texture unit");
                        return;
                    }
                }
            }

            this.m_context.uniform1iv(location.location(), v.Length, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniform2f(WebGLUniformLocation location, GLfloat x, GLfloat y)
        {
            if (this.isContextLostOrPending() || location == null)
            {
                return;
            }

            if (location.program() != this.m_currentProgram)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "uniform2f", "location not for current program");
                return;
            }

            this.m_context.uniform2f(location.location(), x, y);
        }

        public void uniform2fv(WebGLUniformLocation location, Float32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform2fv", location, v, 2))
            {
                return;
            }

            this.m_context.uniform2fv(location.location(), v.length / 2, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniform2fv(WebGLUniformLocation location, params GLfloat[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform2fv", location, handle.AddrOfPinnedObject(), v.Length, 2))
            {
                return;
            }

            this.m_context.uniform2fv(location.location(), v.Length / 2, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniform2i(WebGLUniformLocation location, GLint x, GLint y)
        {
            if (this.isContextLostOrPending() || location == null)
            {
                return;
            }

            if (location.program() != this.m_currentProgram)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "uniform2i", "location not for current program");
                return;
            }

            this.m_context.uniform2i(location.location(), x, y);
        }

        public void uniform2iv(WebGLUniformLocation location, Int32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform2iv", location, v, 2))
            {
                return;
            }

            this.m_context.uniform2iv(location.location(), v.length / 2, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniform2iv(WebGLUniformLocation location, params GLint[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform2iv", location, handle.AddrOfPinnedObject(), v.Length, 2))
            {
                return;
            }

            this.m_context.uniform2iv(location.location(), v.Length / 2, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniform3f(WebGLUniformLocation location, GLfloat x, GLfloat y, GLfloat z)
        {
            if (this.isContextLostOrPending() || location == null)
            {
                return;
            }

            if (location.program() != this.m_currentProgram)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "uniform3f", "location not for current program");
                return;
            }

            this.m_context.uniform3f(location.location(), x, y, z);
        }

        public void uniform3fv(WebGLUniformLocation location, Float32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform3fv", location, v, 3))
            {
                return;
            }

            this.m_context.uniform3fv(location.location(), v.length / 3, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniform3fv(WebGLUniformLocation location, params GLfloat[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform3fv", location, handle.AddrOfPinnedObject(), v.Length, 3))
            {
                handle.Free();
                return;
            }

            this.m_context.uniform3fv(location.location(), v.Length / 3, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniform3i(WebGLUniformLocation location, GLint x, GLint y, GLint z)
        {
            if (this.isContextLostOrPending() || location == null)
            {
                return;
            }

            if (location.program() != this.m_currentProgram)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "uniform3i", "location not for current program");
                return;
            }

            this.m_context.uniform3i(location.location(), x, y, z);
        }

        public void uniform3iv(WebGLUniformLocation location, Int32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform3iv", location, v, 3))
            {
                return;
            }

            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            this.m_context.uniform3iv(location.location(), v.length / 3, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniform3iv(WebGLUniformLocation location, params GLint[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform3iv", location, handle.AddrOfPinnedObject(), v.Length, 3))
            {
                return;
            }

            this.m_context.uniform3iv(location.location(), v.Length / 3, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniform4f(WebGLUniformLocation location, GLfloat x, GLfloat y, GLfloat z, GLfloat w)
        {
            if (this.isContextLostOrPending() || location == null)
            {
                return;
            }

            if (location.program() != this.m_currentProgram)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "uniform4f", "location not for current program");
                return;
            }

            this.m_context.uniform4f(location.location(), x, y, z, w);
        }

        public void uniform4fv(WebGLUniformLocation location, Float32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform4fv", location, v, 4))
            {
                return;
            }

            this.m_context.uniform4fv(location.location(), v.length / 4, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniform4fv(WebGLUniformLocation location, params GLfloat[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform4fv", location, handle.AddrOfPinnedObject(), v.Length, 4))
            {
                return;
            }

            this.m_context.uniform4fv(location.location(), v.Length / 4, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniform4i(WebGLUniformLocation location, GLint x, GLint y, GLint z, GLint w)
        {
            if (this.isContextLostOrPending() || location == null)
            {
                return;
            }

            if (location.program() != this.m_currentProgram)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "uniform4i", "location not for current program");
                return;
            }

            this.m_context.uniform4i(location.location(), x, y, z, w);
        }

        public void uniform4iv(WebGLUniformLocation location, Int32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform4iv", location, v, 4))
            {
                return;
            }

            this.m_context.uniform4iv(location.location(), v.length / 4, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniform4iv(WebGLUniformLocation location, params GLint[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformParameters("uniform4iv", location, handle.AddrOfPinnedObject(), v.Length, 4))
            {
                return;
            }

            this.m_context.uniform4iv(location.location(), v.Length / 4, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniformMatrix2fv(WebGLUniformLocation location, bool transpose, Float32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformMatrixParameters("uniformMatrix2fv", location, transpose, v, 4))
            {
                return;
            }
            this.m_context.uniformMatrix2fv(location.location(), v.length / 4, transpose, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniformMatrix2fv(WebGLUniformLocation location, bool transpose, params GLfloat[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformMatrixParameters("uniformMatrix2fv", location, transpose, handle.AddrOfPinnedObject(), v.Length, 4))
            {
                return;
            }
            this.m_context.uniformMatrix2fv(location.location(), v.Length / 4, transpose, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniformMatrix3fv(WebGLUniformLocation location, bool transpose, Float32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformMatrixParameters("uniformMatrix3fv", location, transpose, v, 9))
            {
                return;
            }
            this.m_context.uniformMatrix3fv(location.location(), v.length / 9, transpose, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniformMatrix3fv(WebGLUniformLocation location, bool transpose, params GLfloat[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformMatrixParameters("uniformMatrix3fv", location, transpose, handle.AddrOfPinnedObject(), v.Length, 9))
            {
                return;
            }
            this.m_context.uniformMatrix3fv(location.location(), v.Length / 9, transpose, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void uniformMatrix4fv(WebGLUniformLocation location, bool transpose, Float32Array v)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformMatrixParameters("uniformMatrix4fv", location, transpose, v, 16))
            {
                return;
            }
            this.m_context.uniformMatrix4fv(location.location(), v.length / 16, transpose, v.buffer.@lock());
            v.buffer.unlock();
        }

        public void uniformMatrix4fv(WebGLUniformLocation location, bool transpose, params GLfloat[] v)
        {
            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            if (this.isContextLostOrPending() || !this.m_validation.validateUniformMatrixParameters("uniformMatrix4fv", location, transpose, handle.AddrOfPinnedObject(), v.Length, 16))
            {
                return;
            }
            this.m_context.uniformMatrix4fv(location.location(), v.Length / 16, transpose, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void useProgram(WebGLProgram program)
        {
            bool deleted;
            if (!this.checkObjectToBeBound("useProgram", program, out deleted))
            {
                return;
            }
            if (deleted)
            {
                program = null;
            }
            if (program != null && !program.getLinkStatus())
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "useProgram", "program not valid");
                return;
            }
            if (this.m_currentProgram != program)
            {
                if (this.m_currentProgram != null)
                {
                    this.m_currentProgram.onDetached(this.graphicsContext3D());
                }
                this.m_currentProgram = program;
                this.m_context.useProgram(objectOrZero(program));
                if (program != null)
                {
                    program.onAttached();
                }
            }
        }

        public void validateProgram(WebGLProgram program)
        {
            if (this.isContextLostOrPending() || !this.m_validation.validateWebGLObject("validateProgram", program))
            {
                return;
            }
            this.m_context.validateProgram(objectOrZero(program));
        }

        public void vertexAttrib1f(GLuint index, GLfloat v0)
        {
            this.vertexAttribfImpl("vertexAttrib1f", index, 1, v0, 0.0f, 0.0f, 1.0f);
        }

        public void vertexAttrib1fv(GLuint index, Float32Array v)
        {
            this.vertexAttribfvImpl("vertexAttrib1fv", index, v, 1);
        }

        public void vertexAttrib1fv(GLuint index, params GLfloat[] v)
        {
            this.vertexAttribfvImpl("vertexAttrib1fv", index, v, v.Length, 1);
        }

        public void vertexAttrib2f(GLuint index, GLfloat v0, GLfloat v1)
        {
            this.vertexAttribfImpl("vertexAttrib2f", index, 2, v0, v1, 0.0f, 1.0f);
        }

        public void vertexAttrib2fv(GLuint index, Float32Array v)
        {
            this.vertexAttribfvImpl("vertexAttrib2fv", index, v, 2);
        }

        public void vertexAttrib2fv(GLuint index, params GLfloat[] v)
        {
            this.vertexAttribfvImpl("vertexAttrib2fv", index, v, v.Length, 2);
        }

        public void vertexAttrib3f(GLuint index, GLfloat v0, GLfloat v1, GLfloat v2)
        {
            this.vertexAttribfImpl("vertexAttrib3f", index, 3, v0, v1, v2, 1.0f);
        }

        public void vertexAttrib3fv(GLuint index, Float32Array v)
        {
            this.vertexAttribfvImpl("vertexAttrib3fv", index, v, 3);
        }

        public void vertexAttrib3fv(GLuint index, params GLfloat[] v)
        {
            this.vertexAttribfvImpl("vertexAttrib3fv", index, v, v.Length, 3);
        }

        public void vertexAttrib4f(GLuint index, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3)
        {
            this.vertexAttribfImpl("vertexAttrib4f", index, 4, v0, v1, v2, v3);
        }

        public void vertexAttrib4fv(GLuint index, Float32Array v)
        {
            this.vertexAttribfvImpl("vertexAttrib4fv", index, v, 4);
        }

        public void vertexAttrib4fv(GLuint index, params GLfloat[] v)
        {
            this.vertexAttribfvImpl("vertexAttrib4fv", index, v, v.Length, 4);
        }

        public void vertexAttribPointer(GLuint index, GLint size, GLenum type, bool normalized, GLsizei stride, long offset)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            switch (type)
            {
                case GraphicsContext3D.BYTE:
                case GraphicsContext3D.UNSIGNED_BYTE:
                case GraphicsContext3D.SHORT:
                case GraphicsContext3D.UNSIGNED_SHORT:
                case GraphicsContext3D.FLOAT:
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "vertexAttribPointer", "invalid type");
                    return;
            }
            if (index >= this.m_maxVertexAttribs)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "vertexAttribPointer", "index out of range");
                return;
            }
            if (size < 1 || size > 4 || stride < 0 || stride > 255 || offset < 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "vertexAttribPointer", "bad size, stride or offset");
                return;
            }
            if (this.m_boundArrayBuffer == null)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "vertexAttribPointer", "no bound ARRAY_BUFFER");
                return;
            }
            // Determine the number of elements the bound buffer can hold, given the offset, size, type and stride
            var typeSize = sizeInBytes(type);
            if (typeSize == 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "vertexAttribPointer", "invalid type");
                return;
            }
            if (stride % typeSize != 0 || offset % typeSize != 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "vertexAttribPointer", "stride or offset not valid for type");
                return;
            }
            var bytesPerElement = size * typeSize;

            this.m_boundVertexArrayObject.setVertexAttribState(index, bytesPerElement, size, type, normalized, stride, offset, this.m_boundArrayBuffer);
            this.m_context.vertexAttribPointer(index, size, type, normalized, stride, offset);
        }

        public void viewport(GLint x, GLint y, GLsizei width, GLsizei height)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (!this.m_validation.validateSize("viewport", width, height))
            {
                return;
            }
            this.m_context.viewport(x, y, width, height);
        }

        public void makeCurrent()
        {
            this.m_context.makeContextCurrent();
        }

        public void swapBuffers()
        {
            this.m_context.swapBuffers();
        }

        public void verticalSync(bool on)
        {
            this.m_context.verticalSync(on);
        }

        internal void forceLostContext(LostContextMode mode)
        {
            if (this.isContextLostOrPending())
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "loseContext", "context already lost");
                return;
            }

            this.m_contextGroup.loseContextGroup(mode);
        }

        internal void forceRestoreContext()
        {
            throw new NotImplementedException();
        }

        internal void loseContextImpl(LostContextMode mode)
        {
            if (this.isContextLost())
            {
                return;
            }

            this.m_contextLost = true;

            this.detachAndRemoveAllObjects();

            var display = (mode == LostContextMode.RealLostContext) ? ConsoleDisplayPreference.DisplayInConsole : ConsoleDisplayPreference.DontDisplayInConsole;
            this.synthesizeGLError(GraphicsContext3D.CONTEXT_LOST_WEBGL, "loseContext", "context lost", display);

            this.dispatchContextLostEvent(EventArgs.Empty);
        }

        internal void dispatchContextLostEvent(EventArgs e)
        {
            var handler = this.contextLost;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal GraphicsContext3D graphicsContext3D()
        {
            return this.m_context;
        }

        internal WebGLContextGroup contextGroup()
        {
            return this.m_contextGroup;
        }

        internal void removeContextObject(WebGLContextObject @object)
        {
            this.m_contextObjects.Remove(@object);
        }

        internal uint getMaxVertexAttribs()
        {
            return this.m_maxVertexAttribs;
        }

        internal void drawArraysInstanced(GLenum mode, GLint first, GLsizei count, GLsizei primcount)
        {
            if (!this.m_validation.validateDrawArrays("drawArraysInstanced", mode, first, count, primcount))
            {
                return;
            }

            if (primcount < 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "drawArraysInstanced", "primcount < 0");
                return;
            }

            if (primcount == 0)
            {
                this.markContextChanged();
                return;
            }

            if (!this.isGLES2NPOTStrict())
            {
                this.checkTextureCompleteness("drawArraysInstanced", true);
            }

            this.m_context.drawArraysInstanced(mode, first, count, primcount);
            if (!this.isGLES2NPOTStrict())
            {
                this.checkTextureCompleteness("drawArraysInstanced", false);
            }
            this.markContextChanged();
        }

        internal void drawElementsInstanced(GLenum mode, GLsizei count, GLenum type, long offset, GLsizei primcount)
        {
            uint numElements;
            if (!this.m_validation.validateDrawElements("drawElementsInstanced", mode, count, type, offset, out numElements))
            {
                return;
            }

            if (primcount < 0)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "drawElementsInstanced", "primcount < 0");
                return;
            }

            if (primcount == 0)
            {
                this.markContextChanged();
                return;
            }

            if (!this.isGLES2NPOTStrict())
            {
                this.checkTextureCompleteness("drawElementsInstanced", true);
            }
            this.m_context.drawElementsInstanced(mode, count, type, offset, primcount);
            if (!this.isGLES2NPOTStrict())
            {
                this.checkTextureCompleteness("drawElementsInstanced", false);
            }
            this.markContextChanged();
        }

        internal void vertexAttribDivisor(GLuint index, GLuint divisor)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }

            if (index >= this.m_maxVertexAttribs)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "vertexAttribDivisor", "index out of range");
                return;
            }

            this.m_boundVertexArrayObject.setVertexAttribDivisor(index, divisor);
            this.m_context.vertexAttribDivisor(index, divisor);
        }

        internal WebGLRenderingContext(HTMLCanvasElement canvas, GraphicsContext3D context, Attributes attributes) : base(canvas)
        {
            this.m_validation = new Validation(this);
            this.m_context = context;
            this.m_contextLost = false;
            this.m_attributes = attributes;
            this.m_synthesizedErrorsToConsole = true;
            this.m_numGLErrorsToConsoleAllowed = maxGLErrorsAllowedToConsole;
            this.m_contextGroup = new WebGLContextGroup();
            this.m_contextGroup.addContext(this);

            this.m_maxViewportDims[0] = this.m_maxViewportDims[1] = 0;
            this.m_context.getIntegerv(this.MAX_VIEWPORT_DIMS, this.m_maxViewportDims);

            this.setupFlags();
            this.initializeNewContext();
        }

        internal void initializeNewContext()
        {
            this.m_markedCanvasDirty = false;
            this.m_activeTextureUnit = 0;
            this.m_packAlignment = 4;
            this.m_unpackAlignment = 4;
            this.m_unpackFlipY = false;
            this.m_unpackPremultiplyAlpha = false;
            this.m_unpackColorspaceConversion = GraphicsContext3D.BROWSER_DEFAULT_WEBGL;
            this.m_boundArrayBuffer = null;
            this.m_currentProgram = null;
            this.m_framebufferBinding = null;
            this.m_renderbufferBinding = null;
            this.m_depthMask = true;
            this.m_stencilEnabled = false;
            this.m_stencilMask = 0xFFFFFFFF;
            this.m_stencilMaskBack = 0xFFFFFFFF;
            this.m_stencilFuncRef = 0;
            this.m_stencilFuncRefBack = 0;
            this.m_stencilFuncMask = 0xFFFFFFFF;
            this.m_stencilFuncMaskBack = 0xFFFFFFFF;
            this.m_numGLErrorsToConsoleAllowed = maxGLErrorsAllowedToConsole;

            this.m_clearColor[0] = this.m_clearColor[1] = this.m_clearColor[2] = this.m_clearColor[3] = 0;
            this.m_colorMask[0] = this.m_colorMask[1] = this.m_colorMask[2] = this.m_colorMask[3] = true;

            var numCombinedTextureImageUnits = new int[1];
            this.m_context.getIntegerv(GraphicsContext3D.MAX_COMBINED_TEXTURE_IMAGE_UNITS, numCombinedTextureImageUnits);
            this.m_textureUnits = new TextureUnitState[numCombinedTextureImageUnits[0]];

            var numVertexAttribs = new int[1];
            this.m_context.getIntegerv(GraphicsContext3D.MAX_VERTEX_ATTRIBS, numVertexAttribs);
            this.m_maxVertexAttribs = (uint)numVertexAttribs[0];

            var temp = new int[1];

            this.m_context.getIntegerv(GraphicsContext3D.MAX_TEXTURE_SIZE, temp);
            this.m_maxTextureSize = temp[0];
            this.m_maxTextureLevel = WebGLTexture.computeLevelCount(this.m_maxTextureSize, this.m_maxTextureSize);

            this.m_context.getIntegerv(GraphicsContext3D.MAX_CUBE_MAP_TEXTURE_SIZE, temp);
            this.m_maxCubeMapTextureSize = temp[0];
            this.m_maxCubeMapTextureLevel = WebGLTexture.computeLevelCount(this.m_maxCubeMapTextureSize, this.m_maxCubeMapTextureSize);

            this.m_context.getIntegerv(GraphicsContext3D.MAX_RENDERBUFFER_SIZE, temp);

            // These two values from EXT_draw_buffers are lazily queried.
            this.m_maxDrawBuffers = 0;
            this.m_maxColorAttachments = 0;

            this.m_backDrawBuffer = GraphicsContext3D.BACK;
            this.m_drawBuffersWebGLRequirementsChecked = false;
            this.m_drawBuffersSupported = false;

            this.m_defaultVertexArrayObject = new WebGLVertexArrayObjectOES(this, WebGLVertexArrayObjectOES.VaoType.VaoTypeDefault);
            this.addContextObject(this.m_defaultVertexArrayObject);
            this.m_boundVertexArrayObject = this.m_defaultVertexArrayObject;

            this.m_vertexAttribValue = Enumerable.Range(0, (int)this.m_maxVertexAttribs).Select(x => new VertexAttribValue()).ToArray();

            if (!this.isGLES2NPOTStrict())
            {
                this.createFallbackBlackTextures1x1();
            }

            var canvasSize = this.clampedCanvasSize();

            this.m_context.reshape(canvasSize.Width, canvasSize.Height);
            this.m_context.viewport(0, 0, canvasSize.Width, canvasSize.Height);
            this.m_context.scissor(0, 0, canvasSize.Width, canvasSize.Height);
        }

        internal void setupFlags()
        {
            this.m_synthesizedErrorsToConsole = true;

            this.m_isErrorGeneratedOnOutOfBoundsAccesses = this.m_context.getExtensions().isEnabled("GL_ANGLE_strict_attribs");
            this.m_isResourceSafe = this.m_context.getExtensions().isEnabled("GL_ANGLE_resource_safe");
            this.m_isGLES2NPOTStrict = !this.m_context.getExtensions().isEnabled("GL_OES_texture_npot");
            this.m_isDepthStencilSupported = this.m_context.getExtensions().isEnabled("GL_OES_packed_depth_stencil");
            this.m_isRobustnessEXTSupported = this.m_context.getExtensions().isEnabled("GL_EXT_robustness");
        }

        internal void addSharedObject(WebGLSharedObject @object)
        {
            this.m_contextGroup.addObject(@object);
        }

        internal void addContextObject(WebGLContextObject @object)
        {
            this.m_contextObjects.Add(@object);
        }

        internal void detachAndRemoveAllObjects()
        {
            var arr = new WebGLContextObject[this.m_contextObjects.Count];
            this.m_contextObjects.CopyTo(arr);

            foreach (var o in arr)
            {
                o.detachContext();
            }
        }

        internal void destroyGraphicsContext3D()
        {
            if (this.m_context != null)
            {
                this.m_context.clear(0);
            }
        }

        internal void markContextChanged()
        {
            if (this.m_framebufferBinding != null)
            {
                return;
            }

            if (!this.m_markedCanvasDirty)
            {
                this.m_markedCanvasDirty = true;
            }
        }

        internal bool isGLES2NPOTStrict()
        {
            return this.m_isGLES2NPOTStrict;
        }

        internal bool isErrorGeneratedOnOutOfBoundsAccesses()
        {
            return this.m_isErrorGeneratedOnOutOfBoundsAccesses;
        }

        internal bool isResourceSafe()
        {
            return this.m_isResourceSafe;
        }

        internal bool isDepthStencilSupported()
        {
            return this.m_isDepthStencilSupported;
        }

        internal static int sizeInBytes(GLenum type)
        {
            switch (type)
            {
                case GraphicsContext3D.BYTE:
                    return sizeof(GLbyte);
                case GraphicsContext3D.UNSIGNED_BYTE:
                    return sizeof(GLubyte);
                case GraphicsContext3D.SHORT:
                    return sizeof(GLshort);
                case GraphicsContext3D.UNSIGNED_SHORT:
                    return sizeof(GLushort);
                case GraphicsContext3D.INT:
                    return sizeof(GLint);
                case GraphicsContext3D.UNSIGNED_INT:
                    return sizeof(GLuint);
                case GraphicsContext3D.FLOAT:
                    return sizeof(GLfloat);
            }
            return 0;
        }

        internal void addCompressedTextureFormat(GLenum format)
        {
            if (!this.m_compressedTextureFormats.Contains(format))
            {
                this.m_compressedTextureFormats.Add(format);
            }
        }

        internal bool isContextLostOrPending()
        {
            return this.m_contextLost;
        }

        internal void setBoundVertexArrayObject(WebGLVertexArrayObjectOES arrayObject)
        {
            this.m_boundVertexArrayObject = arrayObject ?? this.m_defaultVertexArrayObject;
        }

        internal bool getBooleanParameter(GLenum pname)
        {
            var value = new byte[1];
            this.m_context.getBooleanv(pname, value);
            return value[0] != 0;
        }

        internal bool[] getBooleanArrayParameter(GLenum pname)
        {
            if (pname != GraphicsContext3D.COLOR_WRITEMASK)
            {
                // not implemented
                return new bool[2];
            }
            var value = new byte[4];
            this.m_context.getBooleanv(pname, value);
            var boolValue = new bool[4];
            for (var ii = 0; ii < 4; ++ii)
            {
                boolValue[ii] = value[ii] != 0;
            }
            return boolValue;
        }

        internal float getFloatParameter(GLenum pname)
        {
            var value = new float[1];
            this.m_context.getFloatv(pname, value);
            return value[0];
        }

        internal int getIntParameter(GLenum pname)
        {
            var value = new int[1];
            this.m_context.getIntegerv(pname, value);
            return value[0];
        }

        internal dynamic getUnsignedIntParameter(GLenum pname)
        {
            var value = new int[1];
            this.m_context.getIntegerv(pname, value);
            return (uint)value[0];
        }

        internal dynamic getWebGLFloatArrayParameter(GLenum pname)
        {
            var value = new float[4];
            this.m_context.getFloatv(pname, value);
            var length = 0;
            switch (pname)
            {
                case GraphicsContext3D.ALIASED_POINT_SIZE_RANGE:
                case GraphicsContext3D.ALIASED_LINE_WIDTH_RANGE:
                case GraphicsContext3D.DEPTH_RANGE:
                    length = 2;
                    break;
                case GraphicsContext3D.BLEND_COLOR:
                case GraphicsContext3D.COLOR_CLEAR_VALUE:
                    length = 4;
                    break;
                default:
                    // not implemented
                    break;
            }
            var arr = new Float32Array(length);
            for (var i = 0; i < length; i++)
            {
                arr[i] = value[i];
            }
            return arr;
        }

        internal dynamic getWebGLIntArrayParameter(GLenum pname)
        {
            var value = new int[4];
            this.m_context.getIntegerv(pname, value);
            var length = 0;
            switch (pname)
            {
                case GraphicsContext3D.MAX_VIEWPORT_DIMS:
                    length = 2;
                    break;
                case GraphicsContext3D.SCISSOR_BOX:
                case GraphicsContext3D.VIEWPORT:
                    length = 4;
                    break;
                default:
                    // not implemented
                    break;
            }
            var arr = new Int32Array(length);
            for (var i = 0; i < length; i++)
            {
                arr[i] = value[i];
            }
            return arr;
        }

        internal void texImage2DBase(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, IntPtr pixels)
        {
            var tex = this.m_validation.validateTextureBinding("texImage2D", target, true);
            if (pixels == IntPtr.Zero)
            {
                if (this.isResourceSafe())
                {
                    this.m_context.texImage2D(target, level, internalformat, width, height, border, format, type, IntPtr.Zero);
                }
                else
                {
                    var succeed = this.m_context.texImage2DResourceSafe(target, level, internalformat, width, height, border, format, type, this.m_unpackAlignment);
                    if (!succeed)
                    {
                        return;
                    }
                }
            }
            else
            {
                this.m_context.texImage2D(target, level, internalformat, width, height, border, format, type, pixels);
            }
            tex.setLevelInfo(target, level, internalformat, width, height, type);
        }

        internal void texSubImage2DBase(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels)
        {
            var tex = this.m_validation.validateTextureBinding("texSubImage2D", target, true);
            if (tex == null)
            {
                return;
            }
            this.m_context.texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels);
        }

        internal void checkTextureCompleteness(String functionName, bool prepareToDraw)
        {
            var resetActiveUnit = false;
            var extensions = (this.m_oesTextureFloatLinear != null ? WebGLTexture.TextureExtensionFlag.TextureExtensionFloatLinearEnabled : 0) | (this.m_oesTextureHalfFloatLinear != null ? WebGLTexture.TextureExtensionFlag.TextureExtensionHalfFloatLinearEnabled : 0);

            for (var ii = 0; ii < this.m_textureUnits.Length; ++ii)
            {
                if ((this.m_textureUnits[ii].TextureBinding != null && this.m_textureUnits[ii].TextureBinding.needToUseBlackTexture(extensions))
                    || (this.m_textureUnits[ii].textureCubeMapBinding != null && this.m_textureUnits[ii].textureCubeMapBinding.needToUseBlackTexture(extensions)))
                {
                    if (ii != this.m_activeTextureUnit)
                    {
                        this.m_context.activeTexture((uint)ii);
                        resetActiveUnit = true;
                    }
                    else if (resetActiveUnit)
                    {
                        this.m_context.activeTexture((uint)ii);
                        resetActiveUnit = false;
                    }
                    WebGLTexture tex2D;
                    WebGLTexture texCubeMap;
                    if (prepareToDraw)
                    {
                        var msg = "texture bound to texture unit " + ii
                                  + " is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete',"
                                  + " or it is a float/half-float type with linear filtering and without the relevant float/half-float linear extension enabled.";
                        this.printGLWarningToConsole(functionName, msg);
                        tex2D = this.m_blackTexture;
                        texCubeMap = this.m_blackTextureCubeMap;
                    }
                    else
                    {
                        tex2D = this.m_textureUnits[ii].TextureBinding;
                        texCubeMap = this.m_textureUnits[ii].textureCubeMapBinding;
                    }
                    if (this.m_textureUnits[ii].TextureBinding != null && this.m_textureUnits[ii].TextureBinding.needToUseBlackTexture(extensions))
                    {
                        this.m_context.bindTexture(GraphicsContext3D.TEXTURE_2D, objectOrZero(tex2D));
                    }
                    if (this.m_textureUnits[ii].textureCubeMapBinding != null && this.m_textureUnits[ii].textureCubeMapBinding.needToUseBlackTexture(extensions))
                    {
                        this.m_context.bindTexture(GraphicsContext3D.TEXTURE_CUBE_MAP, objectOrZero(texCubeMap));
                    }
                }
            }
            if (resetActiveUnit)
            {
                this.m_context.activeTexture(this.m_activeTextureUnit);
            }
        }

        internal void createFallbackBlackTextures1x1()
        {
            byte[] black = {0, 0, 0, 255};
            var handle = GCHandle.Alloc(black, GCHandleType.Pinned);
            var ptr = handle.AddrOfPinnedObject();
            this.m_blackTexture = this.createTexture();
            this.m_context.bindTexture(GraphicsContext3D.TEXTURE_2D, this.m_blackTexture.obj());
            this.m_context.texImage2D(GraphicsContext3D.TEXTURE_2D, 0, GraphicsContext3D.RGBA, 1, 1,
                                      0, GraphicsContext3D.RGBA, GraphicsContext3D.UNSIGNED_BYTE, ptr);
            this.m_context.bindTexture(GraphicsContext3D.TEXTURE_2D, 0);
            this.m_blackTextureCubeMap = this.createTexture();
            this.m_context.bindTexture(GraphicsContext3D.TEXTURE_CUBE_MAP, this.m_blackTextureCubeMap.obj());
            this.m_context.texImage2D(GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_X, 0, GraphicsContext3D.RGBA, 1, 1,
                                      0, GraphicsContext3D.RGBA, GraphicsContext3D.UNSIGNED_BYTE, ptr);
            this.m_context.texImage2D(GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_X, 0, GraphicsContext3D.RGBA, 1, 1,
                                      0, GraphicsContext3D.RGBA, GraphicsContext3D.UNSIGNED_BYTE, ptr);
            this.m_context.texImage2D(GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Y, 0, GraphicsContext3D.RGBA, 1, 1,
                                      0, GraphicsContext3D.RGBA, GraphicsContext3D.UNSIGNED_BYTE, ptr);
            this.m_context.texImage2D(GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Y, 0, GraphicsContext3D.RGBA, 1, 1,
                                      0, GraphicsContext3D.RGBA, GraphicsContext3D.UNSIGNED_BYTE, ptr);
            this.m_context.texImage2D(GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Z, 0, GraphicsContext3D.RGBA, 1, 1,
                                      0, GraphicsContext3D.RGBA, GraphicsContext3D.UNSIGNED_BYTE, ptr);
            this.m_context.texImage2D(GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Z, 0, GraphicsContext3D.RGBA, 1, 1,
                                      0, GraphicsContext3D.RGBA, GraphicsContext3D.UNSIGNED_BYTE, ptr);
            this.m_context.bindTexture(GraphicsContext3D.TEXTURE_CUBE_MAP, 0);
            handle.Free();
        }

        internal static bool isTexInternalFormatColorBufferCombinationValid(GLenum texInternalFormat, GLenum colorBufferFormat)
        {
            var need = DataFormat.getChannelBitsByFormat(texInternalFormat);
            var have = DataFormat.getChannelBitsByFormat(colorBufferFormat);
            return (need & have) == need;
        }

        internal GLenum getBoundFramebufferColorFormat()
        {
            if (this.m_framebufferBinding != null && this.m_framebufferBinding.obj() != 0)
            {
                return this.m_framebufferBinding.getColorBufferFormat();
            }
            if (this.m_attributes.alpha)
            {
                return GraphicsContext3D.RGBA;
            }
            return GraphicsContext3D.RGB;
        }

        internal int getBoundFramebufferWidth()
        {
            if (this.m_framebufferBinding != null && this.m_framebufferBinding.obj() != 0)
            {
                return this.m_framebufferBinding.getColorBufferWidth();
            }
            return this.m_context.getInternalFramebufferSize().Width;
        }

        internal int getBoundFramebufferHeight()
        {
            if (this.m_framebufferBinding != null && this.m_framebufferBinding.obj() != 0)
            {
                return this.m_framebufferBinding.getColorBufferHeight();
            }
            return this.m_context.getInternalFramebufferSize().Height;
        }

        public bool validateWebGLObject(String functionName, WebGLObject @object)
        {
            return this.m_validation.validateWebGLObject(functionName, @object);
        }

        internal void texParameter(GLenum target, GLenum pname, GLfloat paramf, GLint parami, bool isFloat)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            var tex = this.m_validation.validateTextureBinding("texParameter", target, false);
            if (tex == null)
            {
                return;
            }
            switch (pname)
            {
                case GraphicsContext3D.TEXTURE_MIN_FILTER:
                case GraphicsContext3D.TEXTURE_MAG_FILTER:
                    break;
                case GraphicsContext3D.TEXTURE_WRAP_S:
                case GraphicsContext3D.TEXTURE_WRAP_T:
                    if ((isFloat && paramf != GraphicsContext3D.CLAMP_TO_EDGE && paramf != GraphicsContext3D.MIRRORED_REPEAT && paramf != GraphicsContext3D.REPEAT)
                        || (!isFloat && parami != GraphicsContext3D.CLAMP_TO_EDGE && parami != GraphicsContext3D.MIRRORED_REPEAT && parami != GraphicsContext3D.REPEAT))
                    {
                        this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "texParameter", "invalid parameter");
                        return;
                    }
                    break;
                case Extensions3D.TEXTURE_MAX_ANISOTROPY_EXT: // EXT_texture_filter_anisotropic
                    if (this.m_extTextureFilterAnisotropic == null)
                    {
                        this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "texParameter", "invalid parameter, EXT_texture_filter_anisotropic not enabled");
                        return;
                    }
                    break;
                default:
                    this.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, "texParameter", "invalid parameter name");
                    return;
            }
            if (isFloat)
            {
                tex.setParameterf(pname, paramf);
                this.m_context.texParameterf(target, pname, paramf);
            }
            else
            {
                tex.setParameteri(pname, parami);
                this.m_context.texParameteri(target, pname, parami);
            }
        }

        internal void printGLErrorToConsole(String message)
        {
            if (this.m_numGLErrorsToConsoleAllowed == 0)
            {
                return;
            }

            --this.m_numGLErrorsToConsoleAllowed;
            printWarningToConsole(message);

            if (this.m_numGLErrorsToConsoleAllowed == 0)
            {
                printWarningToConsole("WebGL: too many errors, no more errors will be reported to the console for this context.");
            }
        }

        internal void printGLWarningToConsole(String functionName, String description)
        {
            if (this.m_synthesizedErrorsToConsole)
            {
                this.printGLErrorToConsole("WebGL: " + functionName + ": " + description);
            }
        }

        internal static void printWarningToConsole(String message)
        {
            JSConsole.warn(message);
        }

        internal void vertexAttribfImpl(String functionName, GLuint index, GLsizei expectedSize, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (index >= this.m_maxVertexAttribs)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "index out of range");
                return;
            }
            switch (expectedSize)
            {
                case 1:
                    this.m_context.vertexAttrib1f(index, v0);
                    break;
                case 2:
                    this.m_context.vertexAttrib2f(index, v0, v1);
                    break;
                case 3:
                    this.m_context.vertexAttrib3f(index, v0, v1, v2);
                    break;
                case 4:
                    this.m_context.vertexAttrib4f(index, v0, v1, v2, v3);
                    break;
            }
            var attribValue = this.m_vertexAttribValue[index];
            attribValue.value[0] = v0;
            attribValue.value[1] = v1;
            attribValue.value[2] = v2;
            attribValue.value[3] = v3;
        }

        internal void vertexAttribfvImpl(String functionName, GLuint index, Float32Array v, GLsizei expectedSize)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (v == null)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "no array");
                return;
            }
            if (v.length < expectedSize)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "invalid size");
                return;
            }
            if (index >= this.m_maxVertexAttribs)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "index out of range");
                return;
            }
            // In GL, we skip setting vertexAttrib0 values.

            var ptr = v.buffer.@lock();
            switch (expectedSize)
            {
                case 1:
                    this.m_context.vertexAttrib1fv(index, ptr);
                    break;
                case 2:
                    this.m_context.vertexAttrib2fv(index, ptr);
                    break;
                case 3:
                    this.m_context.vertexAttrib3fv(index, ptr);
                    break;
                case 4:
                    this.m_context.vertexAttrib4fv(index, ptr);
                    break;
            }
            v.buffer.unlock();
            var attribValue = this.m_vertexAttribValue[index];
            attribValue.initValue();
            for (var ii = 0; ii < expectedSize; ++ii)
            {
                attribValue.value[ii] = v[ii];
            }
        }

        internal void vertexAttribfvImpl(String functionName, GLuint index, GLfloat[] v, GLsizei size, GLsizei expectedSize)
        {
            if (this.isContextLostOrPending())
            {
                return;
            }
            if (v == null)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "no array");
                return;
            }
            if (size < expectedSize)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "invalid size");
                return;
            }
            if (index >= this.m_maxVertexAttribs)
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "index out of range");
                return;
            }
            // In GL, we skip setting vertexAttrib0 values.

            var handle = GCHandle.Alloc(v, GCHandleType.Pinned);
            var ptr = handle.AddrOfPinnedObject();
            switch (expectedSize)
            {
                case 1:
                    this.m_context.vertexAttrib1fv(index, ptr);
                    break;
                case 2:
                    this.m_context.vertexAttrib2fv(index, ptr);
                    break;
                case 3:
                    this.m_context.vertexAttrib3fv(index, ptr);
                    break;
                case 4:
                    this.m_context.vertexAttrib4fv(index, ptr);
                    break;
            }
            handle.Free();
            var attribValue = this.m_vertexAttribValue[index];
            attribValue.initValue();
            for (var ii = 0; ii < expectedSize; ++ii)
            {
                attribValue.value[ii] = v[ii];
            }
        }

        internal bool deleteObject(WebGLObject @object)
        {
            if (this.isContextLostOrPending() || @object.obj() == 0)
            {
                return false;
            }
            if (!@object.validate(this.contextGroup(), this))
            {
                this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "delete", "object does not belong to this context");
                return false;
            }
            if (@object.obj() != 0)
            {
                // We need to pass in context here because we want
                // things in this context unbound.
                @object.deleteObject(this.graphicsContext3D());
            }
            return true;
        }

        internal bool checkObjectToBeBound(String functionName, WebGLObject @object, out bool deleted)
        {
            deleted = false;
            if (this.isContextLostOrPending())
            {
                return false;
            }
            if (@object != null)
            {
                if (!@object.validate(this.contextGroup(), this))
                {
                    this.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "object not from this context");
                    return false;
                }
                deleted = @object.obj() == 0;
            }
            return true;
        }

        internal static bool allowPrivilegedExtensions()
        {
            return false;
        }

        internal void synthesizeGLError(GLenum error, String functionName, String description, ConsoleDisplayPreference display = ConsoleDisplayPreference.DisplayInConsole)
        {
            if (this.m_synthesizedErrorsToConsole && display == ConsoleDisplayPreference.DisplayInConsole)
            {
                this.printGLErrorToConsole("WebGL: " + GetErrorString(error) + ": " + functionName + ": " + description);
            }
            this.m_context.synthesizeGLError(error);
        }

        internal static String ensureNotNull(String text)
        {
            return String.IsNullOrEmpty(text) ? String.Empty : text;
        }

        internal void applyStencilTest()
        {
            bool haveStencilBuffer;
            if (this.m_framebufferBinding != null)
            {
                haveStencilBuffer = this.m_framebufferBinding.hasStencilBuffer();
            }
            else
            {
                var attributes = this.getContextAttributes();
                haveStencilBuffer = attributes.stencil();
            }
            this.enableOrDisable(GraphicsContext3D.STENCIL_TEST, this.m_stencilEnabled && haveStencilBuffer);
        }

        internal void enableOrDisable(GLenum capability, bool enable)
        {
            if (enable)
            {
                this.m_context.enable(capability);
            }
            else
            {
                this.m_context.disable(capability);
            }
        }

        internal Size clampedCanvasSize()
        {
            return new Size(clamp(this.canvas.width, 1, this.m_maxViewportDims[0]), clamp(this.canvas.height, 1, this.m_maxViewportDims[1]));
        }

        internal GLint getMaxDrawBuffers()
        {
            if (!this.supportsDrawBuffers())
            {
                return 0;
            }
            if (this.m_maxDrawBuffers == 0)
            {
                var temp = new int[1];
                this.m_context.getIntegerv(Extensions3D.MAX_DRAW_BUFFERS_EXT, temp);
                this.m_maxDrawBuffers = temp[0];
            }
            if (this.m_maxColorAttachments == 0)
            {
                var temp = new int[1];
                this.m_context.getIntegerv(Extensions3D.MAX_COLOR_ATTACHMENTS_EXT, temp);
                this.m_maxColorAttachments = temp[0];
            }
            // WEBGL_draw_buffers requires MAX_COLOR_ATTACHMENTS >= MAX_DRAW_BUFFERS.
            return Math.Min(this.m_maxDrawBuffers, this.m_maxColorAttachments);
        }

        internal GLint getMaxColorAttachments()
        {
            if (!this.supportsDrawBuffers())
            {
                return 0;
            }
            if (this.m_maxColorAttachments == 0)
            {
                var temp = new int[1];
                this.m_context.getIntegerv(Extensions3D.MAX_COLOR_ATTACHMENTS_EXT, temp);
                this.m_maxColorAttachments = temp[0];
            }
            return this.m_maxColorAttachments;
        }

        internal void setBackDrawBuffer(GLenum buf)
        {
            this.m_backDrawBuffer = buf;
        }

        internal void restoreCurrentFramebuffer()
        {
            this.bindFramebuffer(GraphicsContext3D.FRAMEBUFFER, this.m_framebufferBinding);
        }

        internal void restoreCurrentTexture()
        {
            this.bindTexture(GraphicsContext3D.TEXTURE_2D, this.m_textureUnits[this.m_activeTextureUnit].TextureBinding);
        }

        internal bool supportsDrawBuffers()
        {
            if (!this.m_drawBuffersWebGLRequirementsChecked)
            {
                this.m_drawBuffersWebGLRequirementsChecked = true;
                this.m_drawBuffersSupported = WebGLDrawBuffers.supported(this);
            }
            return this.m_drawBuffersSupported;
        }

        internal static Platform3DObject objectOrZero(WebGLObject @object)
        {
            return @object != null && @object.obj() != 0 ? @object.obj() : 0;
        }

        internal static bool isPrefixReserved(String name)
        {
            if (name.StartsWith("gl_") || name.StartsWith("webgl_") || name.StartsWith("_webgl_"))
            {
                return true;
            }
            return false;
        }

        internal static void clip1D(GLint start, GLsizei range, GLsizei sourceRange, out GLint clippedStart, out GLsizei clippedRange)
        {
            if (start < 0)
            {
                range += start;
                start = 0;
            }
            var end = start + range;
            if (end > sourceRange)
            {
                range -= end - sourceRange;
            }
            clippedStart = start;
            clippedRange = range;
        }

        internal static bool clip2D(GLint x, GLint y, GLsizei width, GLsizei height, GLsizei sourceWidth, GLsizei sourceHeight, out GLint clippedX, out GLint clippedY, out GLsizei clippedWidth, out GLsizei clippedHeight)
        {
            clip1D(x, width, sourceWidth, out clippedX, out clippedWidth);
            clip1D(y, height, sourceHeight, out clippedY, out clippedHeight);
            return (clippedX != x || clippedY != y || clippedWidth != width || clippedHeight != height);
        }

        internal static GLint clamp(GLint value, GLint min, GLint max)
        {
            if (value < min)
            {
                value = min;
            }
            if (value > max)
            {
                value = max;
            }
            return value;
        }

        internal static bool equalIgnoringCase(string str1, string str2)
        {
            return str1.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
        }

        internal static String GetErrorString(GLenum error)
        {
            switch (error)
            {
                case GraphicsContext3D.INVALID_ENUM:
                    return "INVALID_ENUM";
                case GraphicsContext3D.INVALID_VALUE:
                    return "INVALID_VALUE";
                case GraphicsContext3D.INVALID_OPERATION:
                    return "INVALID_OPERATION";
                case GraphicsContext3D.OUT_OF_MEMORY:
                    return "OUT_OF_MEMORY";
                case GraphicsContext3D.INVALID_FRAMEBUFFER_OPERATION:
                    return "INVALID_FRAMEBUFFER_OPERATION";
                case GraphicsContext3D.CONTEXT_LOST_WEBGL:
                    return "CONTEXT_LOST_WEBGL";
                default:
                    return String.Format("WebGL ERROR({0:X})", error);
            }
        }

        internal class VertexAttribValue
        {
            public readonly GLfloat[] value = new float[4];

            public VertexAttribValue()
            {
                this.initValue();
            }

            public void initValue()
            {
                this.value[0] = 0.0f;
                this.value[1] = 0.0f;
                this.value[2] = 0.0f;
                this.value[3] = 1.0f;
            }
        }

        internal struct TextureUnitState
        {
            public WebGLTexture TextureBinding;
            public WebGLTexture textureCubeMapBinding;
        }
    }

    // ReSharper restore InconsistentNaming
}
