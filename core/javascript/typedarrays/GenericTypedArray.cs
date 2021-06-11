using System;
using System.Runtime.InteropServices;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    abstract class GenericTypedArray<T> : TypedArray where T : struct
    {
        private GenericTypedArray()
        {
            this.bytesPerElement = Marshal.SizeOf(typeof(T));
        }

        internal GenericTypedArray(int length) : this()
        {
            this.allocate(new ArrayBuffer(length * this.bytesPerElement), 0, length);
        }

        internal GenericTypedArray(TypedArray array) : this()
        {
            this.allocate(new ArrayBuffer(array.length * this.bytesPerElement), 0, array.length);
            set(array, 0);
        }

        internal GenericTypedArray(T[] array) : this()
        {
            this.allocate(new ArrayBuffer(array.Length * this.bytesPerElement), 0, array.Length);
            set(array, 0);
        }

        internal GenericTypedArray(ArrayBuffer buffer) : this()
        {
            this.allocate(buffer, 0, buffer.byteLength / this.bytesPerElement);
        }

        internal GenericTypedArray(ArrayBuffer buffer, int byteOffset, int length) : this()
        {
            this.allocate(buffer, byteOffset, length);
        }

        public abstract dynamic this[int index] { get; set; }

        public void set(TypedArray array, int offset = 0)
        {
            dynamic dynArray = array;
            for (var i = 0; i < array.length; i++)
            {
                this[offset + i] = (T)dynArray[i];
            }
        }

        public void set(Array array, int offset = 0)
        {
            this.allocate(new ArrayBuffer(array.Length * this.bytesPerElement), 0, array.Length);
            if (this.length > 0 && array.Length > 0)
            {
                if (this[0] is byte && array.GetValue(0) is byte)
                {
                    Buffer.BlockCopy(array, offset, this.buffer.data, 0, array.Length);
                }
                else
                {
                    dynamic dynArray = array;
                    for (var i = 0; i < array.Length; i++)
                    {
                        this[offset + i] = (T)dynArray[i];
                    }
                }
            }
        }

        public void set(JSArray array, int offset = 0)
        {
            this.allocate(new ArrayBuffer(array.length * this.bytesPerElement), 0, array.length);
            if (this.length > 0 && array.length > 0)
            {
                dynamic dynArray = array;
                for (var i = 0; i < array.length; i++)
                {
                    this[offset + i] = (T)dynArray[i];
                }
            }
        }

        public void set(T[] array, int offset)
        {
            for (var i = 0; i < array.Length; i++)
            {
                this[offset + i] = array[i];
            }
        }

        public GenericTypedArray<T> subarray(long begin)
        {
            return this.subarray(begin, this.length);
        }

        public GenericTypedArray<T> subarray(long begin, long end)
        {
            var offset = 0u;
            var length = 0u;
            this.calculateOffsetAndLength(begin, end, (uint)this.length, ref offset, ref length);
            this.clampOffsetAndNumElements<T>(this.buffer, (uint)this.byteOffset, ref offset, ref length);
            switch (this.viewType)
            {
                case ViewType.Int8:
                    return new Int8Array(this.buffer, (int)offset, (int)length) as GenericTypedArray<T>;
                case ViewType.Uint8:
                    return new Uint8Array(this.buffer, (int)offset, (int)length) as GenericTypedArray<T>;
                case ViewType.Uint8Clamped:
                    return new Uint8ClampedArray(this.buffer, (int)offset, (int)length) as GenericTypedArray<T>;
                case ViewType.Int16:
                    return new Int16Array(this.buffer, (int)offset, (int)length) as GenericTypedArray<T>;
                case ViewType.Uint16:
                    return new Uint16Array(this.buffer, (int)offset, (int)length) as GenericTypedArray<T>;
                case ViewType.Int32:
                    return new Int32Array(this.buffer, (int)offset, (int)length) as GenericTypedArray<T>;
                case ViewType.Uint32:
                    return new Uint32Array(this.buffer, (int)offset, (int)length) as GenericTypedArray<T>;
                case ViewType.Float32:
                    return new Float32Array(this.buffer, (int)offset, (int)length) as GenericTypedArray<T>;
                case ViewType.Float64:
                    return new Float64Array(this.buffer, (int)offset, (int)length) as GenericTypedArray<T>;
            }
            throw new InvalidOperationException();
        }

        protected bool inrange(int index)
        {
            return index >= 0 && index < this.length;
        }

        private void allocate(ArrayBuffer bufferArg, int offsetArg, int lengthArg)
        {
            this.buffer = bufferArg;
            this.byteLength = lengthArg * this.bytesPerElement;
            this.byteOffset = offsetArg;
            this.length = lengthArg;
            this.view = new DataView(bufferArg, offsetArg, lengthArg * this.bytesPerElement);
        }
    }

    // ReSharper restore InconsistentNaming
}
