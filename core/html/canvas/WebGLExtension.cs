namespace WebGL
{
    // ReSharper disable InconsistentNaming

    abstract class WebGLExtension
    {
        public uint TEXTURE_MAX_ANISOTROPY_EXT = Extensions3D.TEXTURE_MAX_ANISOTROPY_EXT;
        public uint MAX_TEXTURE_MAX_ANISOTROPY_EXT = Extensions3D.MAX_TEXTURE_MAX_ANISOTROPY_EXT;
        public uint COMPRESSED_RGB_S3TC_DXT1_EXT = Extensions3D.COMPRESSED_RGB_S3TC_DXT1_EXT;
        public uint COMPRESSED_RGBA_S3TC_DXT1_EXT = Extensions3D.COMPRESSED_RGBA_S3TC_DXT1_EXT;
        public uint COMPRESSED_RGBA_S3TC_DXT3_EXT = Extensions3D.COMPRESSED_RGBA_S3TC_DXT3_EXT;
        public uint COMPRESSED_RGBA_S3TC_DXT5_EXT = Extensions3D.COMPRESSED_RGBA_S3TC_DXT5_EXT;

        public enum ExtensionName
        {
            WebGLLoseContextName,
            EXTTextureFilterAnisotropicName,
            OESTextureFloatName,
            OESTextureFloatLinearName,
            OESTextureHalfFloatName,
            OESTextureHalfFloatLinearName,
            OESStandardDerivativesName,
            OESVertexArrayObjectName,
            WebGLDebugRendererInfoName,
            WebGLDebugShadersName,
            WebGLCompressedTextureS3TCName,
            WebGLDepthTextureName,
            WebGLDrawBuffersName,
            OESElementIndexUintName,
            WebGLCompressedTextureATCName,
            WebGLCompressedTexturePVRTCName,
            ANGLEInstancedArraysName,
        }

        protected WebGLRenderingContext m_context;

        public WebGLRenderingContext context()
        {
            return this.m_context;
        }

        protected WebGLExtension(WebGLRenderingContext context)
        {
            this.m_context = context;
        }

        internal abstract ExtensionName getName();
    }

    // ReSharper restore InconsistentNaming
}
