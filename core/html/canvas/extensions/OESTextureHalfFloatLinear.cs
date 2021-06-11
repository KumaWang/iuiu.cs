namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class OESTextureHalfFloatLinear : WebGLExtension
    {
        internal OESTextureHalfFloatLinear(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.OESTextureHalfFloatLinearName;
        }
    }

    // ReSharper restore InconsistentNaming
}
