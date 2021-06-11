namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class OESTextureFloat : WebGLExtension
    {
        internal OESTextureFloat(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.OESTextureFloatName;
        }
    }

    // ReSharper restore InconsistentNaming
}
