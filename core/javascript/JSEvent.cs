using System;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class JSEvent : JSObject
    {
        private readonly JSObject m_target;
        private readonly string m_type;

        public JSEvent(JSObject target, String type)
        {
            this.m_target = target;
            this.m_type = type;
        }

        public object target
        {
            get { return this.m_target; }
        }

        public string type
        {
            get { return this.m_type; }
        }
    }

    // ReSharper restore InconsistentNaming
}
