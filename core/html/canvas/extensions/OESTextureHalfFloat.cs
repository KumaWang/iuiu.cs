namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class OESTextureHalfFloat : WebGLExtension
    {
        internal OESTextureHalfFloat(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.OESTextureHalfFloatName;
        }
    }

    // ReSharper restore InconsistentNaming
}
