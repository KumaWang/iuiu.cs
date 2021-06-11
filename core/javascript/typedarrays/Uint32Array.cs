namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Uint32Array : GenericTypedArray<uint>
    {
        public Uint32Array(int length) : base(length)
        {
        }

        public Uint32Array(TypedArray array) : base(array)
        {
        }

        public Uint32Array(uint[] array) : base(array)
        {
        }

        public Uint32Array(ArrayBuffer buffer) : base(buffer)
        {
        }

        public Uint32Array(ArrayBuffer buffer, int byteOffset, int length) : base(buffer, byteOffset, length)
        {
        }

        public override dynamic this[int index]
        {
            get { return this.inrange(index) ? (dynamic)this.view.getUint32(index * this.bytesPerElement) : null; }
            set { this.view.setUint32(index * this.bytesPerElement, (uint)value); }
        }

        internal override ViewType viewType
        {
            get { return ViewType.Uint32; }
        }

        public static Uint32Array create(uint[] src, uint length)
        {
            var result = new Uint32Array((int)length);
            for (var i = 0; i < length; i++)
            {
                result[i] = src[i];
            }
            return result;
        }
    }

    // ReSharper restore InconsistentNaming
}
