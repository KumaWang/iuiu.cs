namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLDepthTexture : WebGLExtension
    {
        internal WebGLDepthTexture(WebGLRenderingContext context) : base(context)
        {
        }

        internal static bool supported(GraphicsContext3D context)
        {
            var extensions = context.getExtensions();
            return extensions.supports("GL_ANGLE_depth_texture") ||
                   extensions.supports("GL_OES_depth_texture") ||
                   extensions.supports("GL_ARB_depth_texture");
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.WebGLDepthTextureName;
        }
    }

    // ReSharper restore InconsistentNaming
}
