namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLDebugRendererInfo : WebGLExtension
    {
        internal const int UNMASKED_VENDOR_WEBGL = 0x9245;
        internal const int UNMASKED_RENDERER_WEBGL = 0x9246;

        internal WebGLDebugRendererInfo(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.WebGLDebugRendererInfoName;
        }
    }

    // ReSharper restore InconsistentNaming
}
