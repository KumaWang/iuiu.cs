namespace WebGL
{
    // ReSharper disable InconsistentNaming

    struct Platform3DObject
    {
        private readonly uint _value;

        private Platform3DObject(uint value)
        {
            this._value = value;
        }

        public static implicit operator uint(Platform3DObject me)
        {
            return me._value;
        }

        public static implicit operator Platform3DObject(uint value)
        {
            return new Platform3DObject(value);
        }
    }

    // ReSharper restore InconsistentNaming
}
