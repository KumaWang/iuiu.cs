namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLCompressedTextureS3TC : WebGLExtension
    {
        internal WebGLCompressedTextureS3TC(WebGLRenderingContext context) : base(context)
        {
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_RGB_S3TC_DXT1_EXT);
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_RGBA_S3TC_DXT1_EXT);
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_RGBA_S3TC_DXT3_EXT);
            context.addCompressedTextureFormat(Extensions3D.COMPRESSED_RGBA_S3TC_DXT5_EXT);
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.WebGLCompressedTextureS3TCName;
        }

        internal static bool supported(WebGLRenderingContext context)
        {
            var extensions = context.graphicsContext3D().getExtensions();
            return extensions.supports("GL_EXT_texture_compression_s3tc") ||
                   (extensions.supports("GL_EXT_texture_compression_dxt1") &&
                    extensions.supports("GL_ANGLE_texture_compression_dxt3") &&
                    extensions.supports("GL_ANGLE_texture_compression_dxt5"));
        }
    }

    // ReSharper restore InconsistentNaming
}
