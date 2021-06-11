namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLLoseContext : WebGLExtension
    {
        internal WebGLLoseContext(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.WebGLLoseContextName;
        }

        internal void loseContext()
        {
            this.m_context.forceLostContext(LostContextMode.SyntheticLostContext);
        }

        internal void restoreContext()
        {
            this.m_context.forceRestoreContext();
        }
    }

    // ReSharper restore InconsistentNaming
}
