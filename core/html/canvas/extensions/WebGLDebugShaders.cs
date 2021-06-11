namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLDebugShaders : WebGLExtension
    {
        internal WebGLDebugShaders(WebGLRenderingContext context) : base(context)
        {
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.WebGLDebugShadersName;
        }

        internal string getTranslatedShaderSource(WebGLShader shader)
        {
            if (this.m_context.isContextLost())
            {
                return string.Empty;
            }
            if (!this.m_context.validateWebGLObject("getTranslatedShaderSource", shader))
            {
                return string.Empty;
            }
            return this.m_context.graphicsContext3D().getExtensions().getTranslatedShaderSourceANGLE(shader.obj());
        }
    }

    // ReSharper restore InconsistentNaming
}
