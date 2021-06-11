namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Int16Array : GenericTypedArray<short>
    {
        public Int16Array(int length) : base(length)
        {
        }

        public Int16Array(TypedArray array) : base(array)
        {
        }

        public Int16Array(short[] array) : base(array)
        {
        }

        public Int16Array(ArrayBuffer buffer) : base(buffer)
        {
        }

        public Int16Array(ArrayBuffer buffer, int byteOffset, int length) : base(buffer, byteOffset, length)
        {
        }

        public override dynamic this[int index]
        {
            get { return this.inrange(index) ? (dynamic)this.view.getInt16(index * this.bytesPerElement) : null; }
            set { this.view.setInt16(index * this.bytesPerElement, (short)value); }
        }

        internal override ViewType viewType
        {
            get { return ViewType.Int16; }
        }

        public static Int16Array create(short[] src, uint length)
        {
            var result = new Int16Array((int)length);
            for (var i = 0; i < length; i++)
            {
                result[i] = src[i];
            }
            return result;
        }
    }

    // ReSharper restore InconsistentNaming
}
