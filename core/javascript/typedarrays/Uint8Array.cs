namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Uint8Array : GenericTypedArray<byte>
    {
        public Uint8Array(int length) : base(length)
        {
        }

        public Uint8Array(TypedArray array) : base(array)
        {
        }

        public Uint8Array(byte[] array) : base(array)
        {
        }

        public Uint8Array(ArrayBuffer buffer) : base(buffer)
        {
        }

        public Uint8Array(ArrayBuffer buffer, int byteOffset, int length) : base(buffer, byteOffset, length)
        {
        }

        public override dynamic this[int index]
        {
            get { return this.inrange(index) ? (dynamic)this.view.getUint8(index * this.bytesPerElement) : null; }
            set { this.view.setUint8(index * this.bytesPerElement, (byte)value); }
        }

        internal override ViewType viewType
        {
            get { return ViewType.Uint8; }
        }

        public static Uint8Array create(byte[] src, uint length)
        {
            var result = new Uint8Array((int)length);
            for (var i = 0; i < length; i++)
            {
                result[i] = src[i];
            }
            return result;
        }
    }

    // ReSharper restore InconsistentNaming
}
