namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Float32Array : GenericTypedArray<float>
    {
        public Float32Array(int length) : base(length)
        {
        }

        public Float32Array(TypedArray array) : base(array)
        {
        }

        public Float32Array(float[] array) : base(array)
        {
        }

        public Float32Array(ArrayBuffer buffer) : base(buffer)
        {
        }

        public Float32Array(ArrayBuffer buffer, int byteOffset, int length) : base(buffer, byteOffset, length)
        {
        }

        public override dynamic this[int index]
        {
            get { return this.inrange(index) ? (dynamic)this.view.getFloat32(index * this.bytesPerElement) : null; }
            set { this.view.setFloat32(index * this.bytesPerElement, (float)value); }
        }

        internal override ViewType viewType
        {
            get { return ViewType.Float32; }
        }

        public static Float32Array create(float[] src, uint length)
        {
            var result = new Float32Array((int)length);
            for (var i = 0; i < length; i++)
            {
                result[i] = src[i];
            }
            return result;
        }
    }

    // ReSharper restore InconsistentNaming
}
