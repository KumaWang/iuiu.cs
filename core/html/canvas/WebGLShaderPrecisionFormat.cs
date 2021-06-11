namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLShaderPrecisionFormat
    {
        private readonly int m_rangeMin;
        private readonly int m_rangeMax;
        private readonly int m_precision;

        internal WebGLShaderPrecisionFormat(int rangeMin, int rangeMax, int precision)
        {
            this.m_rangeMin = rangeMin;
            this.m_rangeMax = rangeMax;
            this.m_precision = precision;
        }

        public int rangeMin()
        {
            return this.m_rangeMin;
        }

        public int rangeMax()
        {
            return this.m_rangeMax;
        }

        public int precision()
        {
            return this.m_precision;
        }
    }

    // ReSharper restore InconsistentNaming
}
