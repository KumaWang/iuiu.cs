namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Uint8ClampedArray : GenericTypedArray<byte>
    {
        public Uint8ClampedArray(int length) : base(length)
        {
        }

        public Uint8ClampedArray(Uint8ClampedArray array) : base(array)
        {
        }

        public Uint8ClampedArray(Uint8Array array) : base(array)
        {
        }

        public Uint8ClampedArray(byte[] array) : base(array)
        {
        }

        public Uint8ClampedArray(ArrayBuffer buffer) : base(buffer)
        {
        }

        public Uint8ClampedArray(ArrayBuffer buffer, int byteOffset, int length) : base(buffer, byteOffset, length)
        {
        }

        public override dynamic this[int index]
        {
            get { return this.inrange(index) ? (dynamic)this.view.getUint8(index * this.bytesPerElement) : null; }
            set { this.view.setUint8(index * this.bytesPerElement, (byte)value); }
        }

        internal override ViewType viewType
        {
            get { return ViewType.Uint8Clamped; }
        }

        public static Uint8ClampedArray create(byte[] src, uint length)
        {
            var result = new Uint8ClampedArray((int)length);
            for (var i = 0; i < length; i++)
            {
                result[i] = src[i];
            }
            return result;
        }
    }

    // ReSharper restore InconsistentNaming
}
