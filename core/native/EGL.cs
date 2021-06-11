using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    [SuppressUnmanagedCodeSecurity]
    static class EGL
    {
        /* EGL Versioning */
        public const UInt16 EGL_VERSION_1_0 = 1;
        public const UInt16 EGL_VERSION_1_1 = 1;
        public const UInt16 EGL_VERSION_1_2 = 1;
        public const UInt16 EGL_VERSION_1_3 = 1;
        public const UInt16 EGL_VERSION_1_4 = 1;

        /* EGL Enumerants. Bitmasks and other exceptional cases aside, most
		 * enums are assigned unique values starting at 0x3000. */

        /* EGL aliases */
        public const UInt16 EGL_FALSE = 0;
        public const UInt16 EGL_TRUE = 1;

        /* Out-of-band handle values */
        public const UInt16 EGL_DEFAULT_DISPLAY = 0;
        public const UInt16 EGL_NO_CONTEXT = 0;
        public const UInt16 EGL_NO_DISPLAY = 0;
        public const UInt16 EGL_NO_SURFACE = 0;

        /* Out-of-band attribute value */
        public const UInt16 EGL_DONT_CARE = 0xFFFF;

        /* Errors / GetError return values */
        public const UInt16 EGL_SUCCESS = 0x3000;
        public const UInt16 EGL_NOT_INITIALIZED = 0x3001;
        public const UInt16 EGL_BAD_ACCESS = 0x3002;
        public const UInt16 EGL_BAD_ALLOC = 0x3003;
        public const UInt16 EGL_BAD_ATTRIBUTE = 0x3004;
        public const UInt16 EGL_BAD_CONFIG = 0x3005;
        public const UInt16 EGL_BAD_CONTEXT = 0x3006;
        public const UInt16 EGL_BAD_CURRENT_SURFACE = 0x3007;
        public const UInt16 EGL_BAD_DISPLAY = 0x3008;
        public const UInt16 EGL_BAD_MATCH = 0x3009;
        public const UInt16 EGL_BAD_NATIVE_PIXMAP = 0x300A;
        public const UInt16 EGL_BAD_NATIVE_WINDOW = 0x300B;
        public const UInt16 EGL_BAD_PARAMETER = 0x300C;
        public const UInt16 EGL_BAD_SURFACE = 0x300D;
        public const UInt16 EGL_CONTEXT_LOST = 0x300E; /* EGL 1.1 - IMG_power_management */

        /* Reserved 0x300F-0x301F for additional errors */

        /* Config attributes */
        public const UInt16 EGL_BUFFER_SIZE = 0x3020;
        public const UInt16 EGL_ALPHA_SIZE = 0x3021;
        public const UInt16 EGL_BLUE_SIZE = 0x3022;
        public const UInt16 EGL_GREEN_SIZE = 0x3023;
        public const UInt16 EGL_RED_SIZE = 0x3024;
        public const UInt16 EGL_DEPTH_SIZE = 0x3025;
        public const UInt16 EGL_STENCIL_SIZE = 0x3026;
        public const UInt16 EGL_CONFIG_CAVEAT = 0x3027;
        public const UInt16 EGL_CONFIG_ID = 0x3028;
        public const UInt16 EGL_LEVEL = 0x3029;
        public const UInt16 EGL_MAX_PBUFFER_HEIGHT = 0x302A;
        public const UInt16 EGL_MAX_PBUFFER_PIXELS = 0x302B;
        public const UInt16 EGL_MAX_PBUFFER_WIDTH = 0x302C;
        public const UInt16 EGL_NATIVE_RENDERABLE = 0x302D;
        public const UInt16 EGL_NATIVE_VISUAL_ID = 0x302E;
        public const UInt16 EGL_NATIVE_VISUAL_TYPE = 0x302F;
        public const UInt16 EGL_SAMPLES = 0x3031;
        public const UInt16 EGL_SAMPLE_BUFFERS = 0x3032;
        public const UInt16 EGL_SURFACE_TYPE = 0x3033;
        public const UInt16 EGL_TRANSPARENT_TYPE = 0x3034;
        public const UInt16 EGL_TRANSPARENT_BLUE_VALUE = 0x3035;
        public const UInt16 EGL_TRANSPARENT_GREEN_VALUE = 0x3036;
        public const UInt16 EGL_TRANSPARENT_RED_VALUE = 0x3037;
        public const UInt16 EGL_NONE = 0x3038; /* Attrib list terminator */
        public const UInt16 EGL_BIND_TO_TEXTURE_RGB = 0x3039;
        public const UInt16 EGL_BIND_TO_TEXTURE_RGBA = 0x303A;
        public const UInt16 EGL_MIN_SWAP_INTERVAL = 0x303B;
        public const UInt16 EGL_MAX_SWAP_INTERVAL = 0x303C;
        public const UInt16 EGL_LUMINANCE_SIZE = 0x303D;
        public const UInt16 EGL_ALPHA_MASK_SIZE = 0x303E;
        public const UInt16 EGL_COLOR_BUFFER_TYPE = 0x303F;
        public const UInt16 EGL_RENDERABLE_TYPE = 0x3040;
        public const UInt16 EGL_MATCH_NATIVE_PIXMAP = 0x3041; /* Pseudo-attribute (not queryable) */
        public const UInt16 EGL_CONFORMANT = 0x3042;

        /* Reserved 0x3041-0x304F for additional config attributes */

        /* Config attribute values */
        public const UInt16 EGL_SLOW_CONFIG = 0x3050; /* EGL_CONFIG_CAVEAT value */
        public const UInt16 EGL_NON_CONFORMANT_CONFIG = 0x3051; /* EGL_CONFIG_CAVEAT value */
        public const UInt16 EGL_TRANSPARENT_RGB = 0x3052; /* EGL_TRANSPARENT_TYPE value */
        public const UInt16 EGL_RGB_BUFFER = 0x308E; /* EGL_COLOR_BUFFER_TYPE value */
        public const UInt16 EGL_LUMINANCE_BUFFER = 0x308F; /* EGL_COLOR_BUFFER_TYPE value */

        /* More config attribute values, for EGL_TEXTURE_FORMAT */
        public const UInt16 EGL_NO_TEXTURE = 0x305C;
        public const UInt16 EGL_TEXTURE_RGB = 0x305D;
        public const UInt16 EGL_TEXTURE_RGBA = 0x305E;
        public const UInt16 EGL_TEXTURE_2D = 0x305F;

        /* Config attribute mask bits */
        public const UInt16 EGL_PBUFFER_BIT = 0x0001; /* EGL_SURFACE_TYPE mask bits */
        public const UInt16 EGL_PIXMAP_BIT = 0x0002; /* EGL_SURFACE_TYPE mask bits */
        public const UInt16 EGL_WINDOW_BIT = 0x0004; /* EGL_SURFACE_TYPE mask bits */
        public const UInt16 EGL_VG_COLORSPACE_LINEAR_BIT = 0x0020; /* EGL_SURFACE_TYPE mask bits */
        public const UInt16 EGL_VG_ALPHA_FORMAT_PRE_BIT = 0x0040; /* EGL_SURFACE_TYPE mask bits */
        public const UInt16 EGL_MULTISAMPLE_RESOLVE_BOX_BIT = 0x0200; /* EGL_SURFACE_TYPE mask bits */
        public const UInt16 EGL_SWAP_BEHAVIOR_PRESERVED_BIT = 0x0400; /* EGL_SURFACE_TYPE mask bits */

        public const UInt16 EGL_OPENGL_ES_BIT = 0x0001; /* EGL_RENDERABLE_TYPE mask bits */
        public const UInt16 EGL_OPENVG_BIT = 0x0002; /* EGL_RENDERABLE_TYPE mask bits */
        public const UInt16 EGL_OPENGL_ES2_BIT = 0x0004; /* EGL_RENDERABLE_TYPE mask bits */
        public const UInt16 EGL_OPENGL_BIT = 0x0008; /* EGL_RENDERABLE_TYPE mask bits */

        /* QueryString targets */
        public const UInt16 EGL_VENDOR = 0x3053;
        public const UInt16 EGL_VERSION = 0x3054;
        public const UInt16 EGL_EXTENSIONS = 0x3055;
        public const UInt16 EGL_CLIENT_APIS = 0x308D;

        /* QuerySurface / SurfaceAttrib / CreatePbufferSurface targets */
        public const UInt16 EGL_HEIGHT = 0x3056;
        public const UInt16 EGL_WIDTH = 0x3057;
        public const UInt16 EGL_LARGEST_PBUFFER = 0x3058;
        public const UInt16 EGL_TEXTURE_FORMAT = 0x3080;
        public const UInt16 EGL_TEXTURE_TARGET = 0x3081;
        public const UInt16 EGL_MIPMAP_TEXTURE = 0x3082;
        public const UInt16 EGL_MIPMAP_LEVEL = 0x3083;
        public const UInt16 EGL_RENDER_BUFFER = 0x3086;
        public const UInt16 EGL_VG_COLORSPACE = 0x3087;
        public const UInt16 EGL_VG_ALPHA_FORMAT = 0x3088;
        public const UInt16 EGL_HORIZONTAL_RESOLUTION = 0x3090;
        public const UInt16 EGL_VERTICAL_RESOLUTION = 0x3091;
        public const UInt16 EGL_PIXEL_ASPECT_RATIO = 0x3092;
        public const UInt16 EGL_SWAP_BEHAVIOR = 0x3093;
        public const UInt16 EGL_MULTISAMPLE_RESOLVE = 0x3099;

        /* EGL_RENDER_BUFFER values / BindTexImage / ReleaseTexImage buffer targets */
        public const UInt16 EGL_BACK_BUFFER = 0x3084;
        public const UInt16 EGL_SINGLE_BUFFER = 0x3085;

        /* OpenVG color spaces */
        public const UInt16 EGL_VG_COLORSPACE_sRGB = 0x3089; /* EGL_VG_COLORSPACE value */
        public const UInt16 EGL_VG_COLORSPACE_LINEAR = 0x308A; /* EGL_VG_COLORSPACE value */

        /* OpenVG alpha formats */
        public const UInt16 EGL_VG_ALPHA_FORMAT_NONPRE = 0x308B; /* EGL_ALPHA_FORMAT value */
        public const UInt16 EGL_VG_ALPHA_FORMAT_PRE = 0x308C; /* EGL_ALPHA_FORMAT value */

        /* Constant scale factor by which fractional display resolutions &
		 * aspect ratio are scaled when queried as integer values.
		 */
        public const UInt16 EGL_DISPLAY_SCALING = 10000;

        /* Unknown display resolution/aspect ratio */
        public const UInt16 EGL_UNKNOWN = 0xFFFF;

        /* Back buffer swap behaviors */
        public const UInt16 EGL_BUFFER_PRESERVED = 0x3094; /* EGL_SWAP_BEHAVIOR value */
        public const UInt16 EGL_BUFFER_DESTROYED = 0x3095; /* EGL_SWAP_BEHAVIOR value */

        /* CreatePbufferFromClientBuffer buffer types */
        public const UInt16 EGL_OPENVG_IMAGE = 0x3096;

        /* QueryContext targets */
        public const UInt16 EGL_CONTEXT_CLIENT_TYPE = 0x3097;

        /* CreateContext attributes */
        public const UInt16 EGL_CONTEXT_CLIENT_VERSION = 0x3098;

        /* Multisample resolution behaviors */
        public const UInt16 EGL_MULTISAMPLE_RESOLVE_DEFAULT = 0x309A; /* EGL_MULTISAMPLE_RESOLVE value */
        public const UInt16 EGL_MULTISAMPLE_RESOLVE_BOX = 0x309B; /* EGL_MULTISAMPLE_RESOLVE value */

        /* BindAPI/QueryAPI targets */
        public const UInt16 EGL_OPENGL_ES_API = 0x30A0;
        public const UInt16 EGL_OPENVG_API = 0x30A1;
        public const UInt16 EGL_OPENGL_API = 0x30A2;

        /* GetCurrentSurface targets */
        public const UInt16 EGL_DRAW = 0x3059;
        public const UInt16 EGL_READ = 0x305A;

        /* WaitNative engines */
        public const UInt16 EGL_CORE_NATIVE_ENGINE = 0x305B;

        /* EGL 1.2 tokens renamed for consistency in EGL 1.3 */
        public const UInt16 EGL_COLORSPACE = EGL_VG_COLORSPACE;
        public const UInt16 EGL_ALPHA_FORMAT = EGL_VG_ALPHA_FORMAT;
        public const UInt16 EGL_COLORSPACE_sRGB = EGL_VG_COLORSPACE_sRGB;
        public const UInt16 EGL_COLORSPACE_LINEAR = EGL_VG_COLORSPACE_LINEAR;
        public const UInt16 EGL_ALPHA_FORMAT_NONPRE = EGL_VG_ALPHA_FORMAT_NONPRE;
        public const UInt16 EGL_ALPHA_FORMAT_PRE = EGL_VG_ALPHA_FORMAT_PRE;

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern Int32 eglGetError();

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern IntPtr eglGetDisplay(IntPtr display_id);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern UInt32 eglInitialize(IntPtr dpy, out Int32 major, out Int32 minor);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern UInt32 eglTerminate(IntPtr dpy);

        [DllImport("libEGL.dll", EntryPoint = "eglQueryString", ExactSpelling = true)]
        private static extern IntPtr _eglQueryString(IntPtr dpy, Int32 name);

        public static String eglQueryString(IntPtr dpy, Int32 name)
        {
            return Marshal.PtrToStringAnsi(_eglQueryString(dpy, name));
        }

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern UInt32 eglGetConfigs(IntPtr dpy, IntPtr[] configs, Int32 config_size, out Int32 num_config);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern UInt32 eglChooseConfig(IntPtr dpy, Int32[] attrib_list, IntPtr[] configs, Int32 config_size, out Int32 num_config);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern IntPtr eglCreateWindowSurface(IntPtr dpy, IntPtr config, IntPtr win, Int32[] attrib_list);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern UInt32 eglDestroySurface(IntPtr dpy, IntPtr surface);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern IntPtr eglCreateContext(IntPtr dpy, IntPtr config, IntPtr share_context, Int32[] attrib_list);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern UInt32 eglDestroyContext(IntPtr dpy, IntPtr ctx);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern UInt32 eglMakeCurrent(IntPtr dpy, IntPtr draw, IntPtr read, IntPtr ctx);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern UInt32 eglSwapBuffers(IntPtr dpy, IntPtr surface);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern UInt32 eglSwapInterval(IntPtr dpy, Int32 interval);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        public static extern IntPtr eglGetProcAddress(String procname);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglGetConfigAttrib(IntPtr dpy, IntPtr config, Int32 attribute, out Int32 value);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern IntPtr eglCreatePbufferSurface(IntPtr dpy, IntPtr config, Int32[] attrib_list);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern IntPtr eglCreatePixmapSurface(IntPtr dpy, IntPtr config, IntPtr pixmap, Int32[] attrib_list);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglQuerySurface(IntPtr dpy, IntPtr surface, Int32 attribute, out Int32 value);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglBindAPI(UInt32 api);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglQueryAPI();

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglWaitClient();

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglReleaseThread();

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern IntPtr eglCreatePbufferFromClientBuffer(IntPtr dpy, UInt32 buftype, IntPtr buffer, IntPtr config, Int32[] attrib_list);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglSurfaceAttrib(IntPtr dpy, IntPtr surface, Int32 attribute, Int32 value);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglBindTexImage(IntPtr dpy, IntPtr surface, Int32 buffer);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglReleaseTexImage(IntPtr dpy, IntPtr surface, Int32 buffer);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern IntPtr eglGetCurrentContext();

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern IntPtr eglGetCurrentSurface(Int32 readdraw);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern IntPtr eglGetCurrentDisplay();

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglQueryContext(IntPtr dpy, IntPtr ctx, Int32 attribute, out Int32 value);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglWaitGL();

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglWaitNative(Int32 engine);

        [DllImport("libEGL.dll", ExactSpelling = true)]
        private static extern UInt32 eglCopyBuffers(IntPtr dpy, IntPtr surface, IntPtr target);
    }

    // ReSharper restore InconsistentNaming
}
