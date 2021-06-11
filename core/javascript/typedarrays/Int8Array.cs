namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Int8Array : GenericTypedArray<sbyte>
    {
        public Int8Array(int length) : base(length)
        {
        }

        public Int8Array(TypedArray array) : base(array)
        {
        }

        public Int8Array(sbyte[] array) : base(array)
        {
        }

        public Int8Array(ArrayBuffer buffer) : base(buffer)
        {
        }

        public Int8Array(ArrayBuffer buffer, int byteOffset, int length) : base(buffer, byteOffset, length)
        {
        }

        public override dynamic this[int index]
        {
            get { return this.inrange(index) ? (dynamic)this.view.getInt8(index * this.bytesPerElement) : null; }
            set { this.view.setInt8(index * this.bytesPerElement, (sbyte)value); }
        }

        internal override ViewType viewType
        {
            get { return ViewType.Int8; }
        }

        public static Int8Array create(sbyte[] src, uint length)
        {
            var result = new Int8Array((int)length);
            for (var i = 0; i < length; i++)
            {
                result[i] = src[i];
            }
            return result;
        }
    }

    // ReSharper restore InconsistentNaming
}
