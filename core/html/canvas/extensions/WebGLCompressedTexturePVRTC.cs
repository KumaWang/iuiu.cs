namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLCompressedTexturePVRTC : WebGLExtension
    {
        internal WebGLCompressedTexturePVRTC(WebGLRenderingContext context) : base(context)
        {
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_RGB_PVRTC_4BPPV1_IMG);
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_RGB_PVRTC_2BPPV1_IMG);
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_RGBA_PVRTC_4BPPV1_IMG);
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_RGBA_PVRTC_2BPPV1_IMG);
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.WebGLCompressedTexturePVRTCName;
        }

        internal static bool supported(WebGLRenderingContext context)
        {
            var extensions = context.graphicsContext3D().getExtensions();
            return extensions.supports("GL_IMG_texture_compression_pvrtc");
        }
    }

    // ReSharper restore InconsistentNaming
}
