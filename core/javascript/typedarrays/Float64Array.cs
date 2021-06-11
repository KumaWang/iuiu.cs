namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Float64Array : GenericTypedArray<double>
    {
        public Float64Array(int length) : base(length)
        {
        }

        public Float64Array(TypedArray array) : base(array)
        {
        }

        public Float64Array(double[] array) : base(array)
        {
        }

        public Float64Array(ArrayBuffer buffer) : base(buffer)
        {
        }

        public Float64Array(ArrayBuffer buffer, int byteOffset, int length) : base(buffer, byteOffset, length)
        {
        }

        public override dynamic this[int index]
        {
            get { return this.inrange(index) ? (dynamic)this.view.getFloat64(index * this.bytesPerElement) : null; }
            set { this.view.setFloat64(index * this.bytesPerElement, (double)value); }
        }

        internal override ViewType viewType
        {
            get { return ViewType.Float64; }
        }

        public static Float64Array create(double[] src, uint length)
        {
            var result = new Float64Array((int)length);
            for (var i = 0; i < length; i++)
            {
                result[i] = src[i];
            }
            return result;
        }
    }

    // ReSharper restore InconsistentNaming
}
