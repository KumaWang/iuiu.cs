using System;
using System.Runtime.InteropServices;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class DataView
    {
        [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
        private struct Union
        {
            [FieldOffset(0)]
            public Byte Byte0;

            [FieldOffset(1)]
            public Byte Byte1;

            [FieldOffset(2)]
            public Byte Byte2;

            [FieldOffset(3)]
            public Byte Byte3;

            [FieldOffset(4)]
            public Byte Byte4;

            [FieldOffset(5)]
            public Byte Byte5;

            [FieldOffset(6)]
            public Byte Byte6;

            [FieldOffset(7)]
            public Byte Byte7;

            [FieldOffset(0)]
            public Int16 Short0;

            [FieldOffset(0)]
            public UInt16 Ushort0;

            [FieldOffset(0)]
            public Int32 Int0;

            [FieldOffset(0)]
            public UInt32 Uint0;

            [FieldOffset(0)]
            public Single Single0;

            [FieldOffset(0)]
            public Double Double0;
        }

        private readonly byte[] _data;
        private readonly int _byteOffset;
        private readonly int _byteLength;

        public byte[] Data => _data;

        public DataView(ArrayBuffer buffer) : this(buffer, 0, buffer.byteLength)
        {
        }

        public DataView(ArrayBuffer buffer, int byteOffset, int byteLength)
        {
            if (byteOffset < 0 || byteOffset + byteLength > buffer.byteLength)
            {
                throw new ArgumentOutOfRangeException();
            }
            this._data = buffer.data;
            this._byteOffset = byteOffset;
            this._byteLength = byteLength;
        }

        public SByte getInt8(int byteOffset)
        {
            return (sbyte)this._data[this.calcOffset(byteOffset, 1)];
        }

        public Byte getUint8(int byteOffset)
        {
            return this._data[this.calcOffset(byteOffset, 1)];
        }

        public Int16 getInt16(int byteOffset, bool littleEndian = false)
        {
            return this.read16(byteOffset).Short0;
        }

        public UInt16 getUint16(int byteOffset, bool littleEndian = false)
        {
            return this.read16(byteOffset).Ushort0;
        }

        public Int32 getInt32(int byteOffset, bool littleEndian = false)
        {
            return this.read32(byteOffset).Int0;
        }

        public UInt32 getUint32(int byteOffset, bool littleEndian = false)
        {
            return this.read32(byteOffset).Uint0;
        }

        public Single getFloat32(int byteOffset, bool littleEndian = false)
        {
            return this.read32(byteOffset).Single0;
        }

        public Double getFloat64(int byteOffset, bool littleEndian = false)
        {
            return this.read64(byteOffset).Double0;
        }

        public void setInt8(int byteOffset, SByte value)
        {
            this._data[byteOffset + this._byteOffset] = (byte)value;
        }

        public void setUint8(int byteOffset, Byte value)
        {
            this._data[byteOffset + this._byteOffset] = value;
        }

        public void setInt16(int byteOffset, Int16 value, bool littleEndian = false)
        {
            this.write16(byteOffset, new Union {Short0 = value});
        }

        public void setUint16(int byteOffset, UInt16 value, bool littleEndian = false)
        {
            this.write16(byteOffset, new Union {Ushort0 = value});
        }

        public void setInt32(int byteOffset, Int32 value, bool littleEndian = false)
        {
            this.write32(byteOffset, new Union {Int0 = value});
        }

        public void setUint32(int byteOffset, UInt32 value, bool littleEndian = false)
        {
            this.write32(byteOffset, new Union {Uint0 = value});
        }

        public void setFloat32(int byteOffset, Single value, bool littleEndian = false)
        {
            this.write32(byteOffset, new Union {Single0 = value});
        }

        public void setFloat64(int byteOffset, Double value, bool littleEndian = false)
        {
            this.write64(byteOffset, new Union {Double0 = value});
        }

        private int calcOffset(int byteOffset, int byteLength)
        {
            byteOffset += this._byteOffset;
            if (byteOffset < this._byteOffset || byteOffset + byteLength > this._byteOffset + this._byteLength)
            {
                throw new IndexOutOfRangeException();
            }
            return byteOffset;
        }

        private Union read16(int byteOffset)
        {
            var offset = this.calcOffset(byteOffset, 2);
            return new Union
            {
                Byte0 = this._data[offset + 0],
                Byte1 = this._data[offset + 1],
            };
        }

        private Union read32(int byteOffset)
        {
            var offset = this.calcOffset(byteOffset, 4);
            return new Union
            {
                Byte0 = this._data[offset + 0],
                Byte1 = this._data[offset + 1],
                Byte2 = this._data[offset + 2],
                Byte3 = this._data[offset + 3]
            };
        }

        private Union read64(int byteOffset)
        {
            var offset = this.calcOffset(byteOffset, 8);
            return new Union
            {
                Byte0 = this._data[offset + 0],
                Byte1 = this._data[offset + 1],
                Byte2 = this._data[offset + 2],
                Byte3 = this._data[offset + 3],
                Byte4 = this._data[offset + 4],
                Byte5 = this._data[offset + 5],
                Byte6 = this._data[offset + 6],
                Byte7 = this._data[offset + 7]
            };
        }

        private void write16(int byteOffset, Union b)
        {
            var offset = this.calcOffset(byteOffset, 2);
            this._data[offset + 0] = b.Byte0;
            this._data[offset + 1] = b.Byte1;
        }

        private void write32(int byteOffset, Union b)
        {
            var offset = this.calcOffset(byteOffset, 4);
            this._data[offset + 0] = b.Byte0;
            this._data[offset + 1] = b.Byte1;
            this._data[offset + 2] = b.Byte2;
            this._data[offset + 3] = b.Byte3;
        }

        private void write64(int byteOffset, Union b)
        {
            var offset = this.calcOffset(byteOffset, 4);
            this._data[offset + 0] = b.Byte0;
            this._data[offset + 1] = b.Byte1;
            this._data[offset + 2] = b.Byte2;
            this._data[offset + 3] = b.Byte3;
            this._data[offset + 4] = b.Byte4;
            this._data[offset + 5] = b.Byte5;
            this._data[offset + 6] = b.Byte6;
            this._data[offset + 7] = b.Byte7;
        }
    }

    // ReSharper restore InconsistentNaming
}
