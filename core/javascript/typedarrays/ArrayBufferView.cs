using System;
using System.Runtime.InteropServices;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    abstract class ArrayBufferView
    {
        public ArrayBuffer buffer { get; internal set; }

        public int byteOffset { get; internal set; }

        public int byteLength { get; internal set; }

        internal DataView view;

        internal abstract ViewType viewType { get; }

        internal enum ViewType
        {
            Int8,
            Uint8,
            Uint8Clamped,
            Int16,
            Uint16,
            Int32,
            Uint32,
            Float32,
            Float64,
            DataView
        }

        internal void calculateOffsetAndLength(long start, long end, uint arraySize, ref uint offset, ref uint length)
        {
            if (start < 0)
            {
                start += (int)arraySize;
            }
            if (start < 0)
            {
                start = 0;
            }
            if (end < 0)
            {
                end += (int)arraySize;
            }
            if (end < 0)
            {
                end = 0;
            }
            if ((uint)end > arraySize)
            {
                end = (int)arraySize;
            }
            if (end < start)
            {
                end = start;
            }
            offset = (uint)start;
            length = (uint)(end - start);
        }

        internal void clampOffsetAndNumElements<T>(ArrayBuffer buf, uint arrayByteOffset, ref uint offset, ref uint numElements)
        {
            var sizeOf = Marshal.SizeOf(typeof(T));
            var maxOffset = (uint)((uint.MaxValue - arrayByteOffset) / sizeOf);
            if (offset > maxOffset)
            {
                offset = (uint)buf.byteLength;
                numElements = 0;
                return;
            }
            offset = (uint)(arrayByteOffset + offset * sizeOf);
            offset = (uint)Math.Min(buf.byteLength, offset);
            var remainingElements = (uint)((buf.byteLength - offset) / sizeOf);
            numElements = Math.Min(remainingElements, numElements);
        }
    }

    // ReSharper restore InconsistentNaming
}
