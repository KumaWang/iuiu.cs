namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class OESStandardDerivatives : WebGLExtension
    {
        internal OESStandardDerivatives(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.OESStandardDerivativesName;
        }
    }

    // ReSharper restore InconsistentNaming
}
