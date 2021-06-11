using System;
using System.Security;
using System.Text;

namespace engine.framework.graphics
{
    class BufWriter : IDisposable
    {

        #region Fields

        private Encoder _encoder;
        private Encoding _encoding;
        private int _maxChars;
        private int _position = 0;
        private int _length = 0;

        private byte[] _buffer = new byte[0x10];
        private byte[] _largeByteBuffer;
        private byte[] _storeBuffer;
        private const int StoreBufferResizeSize = 0x100;
        private const int LargeByteBufferSize = 102400;

        #endregion

        #region Properties

        public BufWriter()
        {
            this._storeBuffer = new byte[0x100];
            this._encoding = new UTF8Encoding(false, true);
            this._encoder = this._encoding.GetEncoder();
        }

        public BufWriter(byte[] buffer)
        {
            this._storeBuffer = buffer;
            this._encoding = new UTF8Encoding(false, true);
            this._encoder = this._encoding.GetEncoder();
        }

        public int Length => _length;

        public byte[] Bytes => _storeBuffer;

        public int Position 
        {
            get { return _position; }
            set { _position = value; }
        }

        #endregion

        #region Primitive Write

        public void Write(bool value)
        {
            this._buffer[0] = value ? ((byte)1) : ((byte)0);
            Write(this._buffer, 0, 1);
        }

        public void Write(byte value)
        {
            CheckBufferLength();

            _storeBuffer[_position++] = value;

            _position++;
            _length = Math.Max(_position, _length);
        }

        [SecuritySafeCritical]
        public unsafe void Write(char ch)
        {
            if (char.IsSurrogate(ch))
            {
                throw new ArgumentException("Arg_SurrogatesNotAllowedAsSingleChar");
            }
            int count = 0;
            fixed (byte* numRef = this._buffer)
            {
                count = this._encoder.GetBytes(&ch, 1, numRef, 0x10, true);
            }
            Write(this._buffer, 0, count);
        }

        public void Write(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            Write(buffer, 0, buffer.Length);
        }

        [SecuritySafeCritical]
        public unsafe void Write(double value)
        {
            ulong num = *((ulong*)&value);
            this._buffer[0] = (byte)num;
            this._buffer[1] = (byte)(num >> 8);
            this._buffer[2] = (byte)(num >> 0x10);
            this._buffer[3] = (byte)(num >> 0x18);
            this._buffer[4] = (byte)(num >> 0x20);
            this._buffer[5] = (byte)(num >> 40);
            this._buffer[6] = (byte)(num >> 0x30);
            this._buffer[7] = (byte)(num >> 0x38);
            Write(this._buffer, 0, 8);
        }

        public void Write(int value)
        {
            this._buffer[0] = (byte)value;
            this._buffer[1] = (byte)(value >> 8);
            this._buffer[2] = (byte)(value >> 0x10);
            this._buffer[3] = (byte)(value >> 0x18);
            Write(this._buffer, 0, 4);
        }

        public void Write(char[] chars)
        {
            if (chars == null)
            {
                throw new ArgumentNullException("chars");
            }
            byte[] buffer = this._encoding.GetBytes(chars, 0, chars.Length);
            Write(buffer, 0, buffer.Length);
        }

        public void Write(short value)
        {
            this._buffer[0] = (byte)value;
            this._buffer[1] = (byte)(value >> 8);
            Write(this._buffer, 0, 2);
        }

        public void Write(long value)
        {
            this._buffer[0] = (byte)value;
            this._buffer[1] = (byte)(value >> 8);
            this._buffer[2] = (byte)(value >> 0x10);
            this._buffer[3] = (byte)(value >> 0x18);
            this._buffer[4] = (byte)(value >> 0x20);
            this._buffer[5] = (byte)(value >> 40);
            this._buffer[6] = (byte)(value >> 0x30);
            this._buffer[7] = (byte)(value >> 0x38);
            Write(this._buffer, 0, 8);
        }

        public void Write(sbyte value)
        {
            Write((byte)value);
        }

        [SecuritySafeCritical]
        public unsafe void Write(float value)
        {
            uint num = *((uint*)&value);
            this._buffer[0] = (byte)num;
            this._buffer[1] = (byte)(num >> 8);
            this._buffer[2] = (byte)(num >> 0x10);
            this._buffer[3] = (byte)(num >> 0x18);
            Write(this._buffer, 0, 4);
        }

        [SecuritySafeCritical]
        public unsafe void Write(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            int byteCount = this._encoding.GetByteCount(value);
            this.Write7BitEncodedInt(byteCount);
            if (this._largeByteBuffer == null)
            {
                this._largeByteBuffer = new byte[LargeByteBufferSize];
                this._maxChars = LargeByteBufferSize / this._encoding.GetMaxByteCount(1);
            }
            if (byteCount <= LargeByteBufferSize)
            {
                this._encoding.GetBytes(value, 0, value.Length, this._largeByteBuffer, 0);
                Write(this._largeByteBuffer, 0, byteCount);
            }
            else
            {
                int num4;
                int num2 = 0;
                for (int i = value.Length; i > 0; i -= num4)
                {
                    num4 = (i > this._maxChars) ? this._maxChars : i;
                    fixed (char* str = value.ToCharArray())
                    {
                        int num5;
                        char* chPtr = str;
                        fixed (byte* numRef = this._largeByteBuffer)
                        {
                            num5 = this._encoder.GetBytes(chPtr + num2, num4, numRef, LargeByteBufferSize, num4 == i);
                            //delete str = null;
                        }
                        Write(this._largeByteBuffer, 0, num5);
                        num2 += num4;
                    }
                }
            }
        }

        public void Write(ushort value)
        {
            this._buffer[0] = (byte)value;
            this._buffer[1] = (byte)(value >> 8);

            Write(this._buffer, 0, 2);
        }

        public void Write(uint value)
        {
            this._buffer[0] = (byte)value;
            this._buffer[1] = (byte)(value >> 8);
            this._buffer[2] = (byte)(value >> 0x10);
            this._buffer[3] = (byte)(value >> 0x18);

            Write(this._buffer, 0, 4);
        }

        public void Write(ulong value)
        {
            this._buffer[0] = (byte)value;
            this._buffer[1] = (byte)(value >> 8);
            this._buffer[2] = (byte)(value >> 0x10);
            this._buffer[3] = (byte)(value >> 0x18);
            this._buffer[4] = (byte)(value >> 0x20);
            this._buffer[5] = (byte)(value >> 40);
            this._buffer[6] = (byte)(value >> 0x30);
            this._buffer[7] = (byte)(value >> 0x38);

            Write(this._buffer, 0, 8);
        }

        public void Write(byte[] buffer, int index, int count)
        {
            CheckBufferLength(count);

            System.Buffer.BlockCopy(buffer, index, _storeBuffer, _position, count);

            _position += count;
            _length = Math.Max(_position, _length);
        }

        public void CheckBufferLength(int count = 1)
        {
            int farLength = _position + count;

            if (_buffer.Length <= farLength)
            {
                int multResize = farLength / StoreBufferResizeSize + 1;
                Array.Resize<byte>(ref _storeBuffer, multResize * StoreBufferResizeSize);
            }
        }

        public void Write(char[] chars, int index, int count)
        {
            byte[] buffer = this._encoding.GetBytes(chars, index, count);
            Write(buffer, 0, buffer.Length);
        }

        public void Write7BitEncodedInt(int value)
        {
            uint num = (uint)value;
            while (num >= 0x80)
            {
                this.Write((byte)(num | 0x80));
                num = num >> 7;
            }
            this.Write((byte)num);
        }

        public void Dispose()
        {
            _buffer = null;
            _largeByteBuffer = null;
            _storeBuffer = null;
        }

        #endregion

    }
}