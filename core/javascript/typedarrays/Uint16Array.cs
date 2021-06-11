namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Uint16Array : GenericTypedArray<ushort>
    {
        public Uint16Array(int length) : base(length)
        {
        }

        public Uint16Array(TypedArray array) : base(array)
        {
        }

        public Uint16Array(ushort[] array) : base(array)
        {
        }

        public Uint16Array(ArrayBuffer buffer) : base(buffer)
        {
        }

        public Uint16Array(ArrayBuffer buffer, int byteOffset, int length) : base(buffer, byteOffset, length)
        {
        }

        public override dynamic this[int index]
        {
            get { return this.inrange(index) ? (dynamic)this.view.getUint16(index * this.bytesPerElement) : null; }
            set { this.view.setUint16(index * this.bytesPerElement, (ushort)value); }
        }

        internal override ViewType viewType
        {
            get { return ViewType.Uint16; }
        }

        public static Uint16Array create(ushort[] src, uint length)
        {
            var result = new Uint16Array((int)length);
            for (var i = 0; i < length; i++)
            {
                result[i] = src[i];
            }
            return result;
        }
    }

    // ReSharper restore InconsistentNaming
}
