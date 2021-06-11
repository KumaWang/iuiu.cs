using System;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Int32Array : GenericTypedArray<int>
    {
        public Int32Array(int length) : base(length)
        {
        }

        public Int32Array(TypedArray array) : base(array)
        {
        }

        public Int32Array(int[] array) : base(array)
        {
        }

        public Int32Array(ArrayBuffer buffer) : base(buffer)
        {
        }

        public Int32Array(ArrayBuffer buffer, int byteOffset, int length) : base(buffer, byteOffset, length)
        {
        }

        public override dynamic this[int index]
        {
            get { return this.inrange(index) ? (dynamic)this.view.getInt32(index * this.bytesPerElement) : null; }
            set { this.view.setInt32(index * this.bytesPerElement, (int)(Double.IsNaN((double)value) ? 0 : value)); }
        }

        internal override ViewType viewType
        {
            get { return ViewType.Int32; }
        }

        public static Int32Array create(int[] src, uint length)
        {
            var result = new Int32Array((int)length);
            for (var i = 0; i < length; i++)
            {
                result[i] = src[i];
            }
            return result;
        }
    }

    // ReSharper restore InconsistentNaming
}
