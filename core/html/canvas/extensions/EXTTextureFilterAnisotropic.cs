namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class EXTTextureFilterAnisotropic : WebGLExtension
    {
        internal EXTTextureFilterAnisotropic(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.EXTTextureFilterAnisotropicName;
        }
    }

    // ReSharper restore InconsistentNaming
}
