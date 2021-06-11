using System;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLActiveInfo
    {
        private readonly Int32 m_size;
        private readonly UInt32 m_type;
        private readonly String m_name;

        internal WebGLActiveInfo(Int32 size, UInt32 type, String name)
        {
            this.m_size = size;
            this.m_type = type;
            this.m_name = name;
        }

        public int size()
        {
            return this.m_size;
        }

        public uint type()
        {
            return this.m_type;
        }

        public string name()
        {
            return this.m_name;
        }
    }

    // ReSharper restore InconsistentNaming
}
