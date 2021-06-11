namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLCompressedTextureATC : WebGLExtension
    {
        internal WebGLCompressedTextureATC(WebGLRenderingContext context) : base(context)
        {
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_ATC_RGB_AMD);
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_ATC_RGBA_EXPLICIT_ALPHA_AMD);
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_ATC_RGBA_INTERPOLATED_ALPHA_AMD);
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.WebGLCompressedTextureATCName;
        }

        internal static bool supported(WebGLRenderingContext context)
        {
            var extensions = context.graphicsContext3D().getExtensions();
            return extensions.supports("GL_AMD_compressed_ATC_texture");
        }
    }

    // ReSharper restore InconsistentNaming
}
