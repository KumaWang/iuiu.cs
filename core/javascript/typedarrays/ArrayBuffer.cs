using System;
using System.Runtime.InteropServices;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class ArrayBuffer
    {
        internal readonly Byte[] data;
        internal GCHandle handle;

        public ArrayBuffer(int length)
        {
            this.data = new Byte[length];
        }

        public int byteLength
        {
            get { return this.data.Length; }
        }

        ~ArrayBuffer()
        {
            this.unlock();
        }

        internal bool isLocked
        {
            get { return this.handle.IsAllocated; }
        }

        internal IntPtr @lock()
        {
            if (!this.isLocked)
            {
                this.handle = GCHandle.Alloc(this.data, GCHandleType.Pinned);
            }
            return this.handle.AddrOfPinnedObject();
        }

        internal void unlock()
        {
            if (this.isLocked)
            {
                this.handle.Free();
            }
        }
    }

    // ReSharper restore InconsistentNaming
}
