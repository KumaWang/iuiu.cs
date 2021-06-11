using System;
using System.Runtime.InteropServices;
using GLenum = System.UInt32;
using GLboolean = System.Boolean;
using GLbitfield = System.UInt32;
using GLbyte = System.SByte;
using GLshort = System.Int16;
using GLint = System.Int32;
using GLsizei = System.Int32;
using GLintptr = System.Int64;
using GLsizeiptr = System.Int64;
using GLubyte = System.Byte;
using GLushort = System.UInt16;
using GLuint = System.UInt32;
using GLfloat = System.Single;
using GLclampf = System.Single;
using DOMString = System.String;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLBuffer : WebGLSharedObject
    {
        private GLenum m_target;
        private ArrayBuffer m_elementArrayBuffer;
        private GLsizeiptr m_byteLength;
        private readonly MaxIndexCacheEntry[] m_maxIndexCache = new MaxIndexCacheEntry[4];
        private uint m_nextAvailableCacheEntry;

        internal WebGLBuffer(WebGLRenderingContext ctx) : base(ctx)
        {
            this.m_target = 0;
            this.m_byteLength = 0;
            this.m_nextAvailableCacheEntry = 0;
            this.setObject(ctx.graphicsContext3D().createBuffer());
            this.clearCachedMaxIndices();
        }

        ~WebGLBuffer()
        {
            this.deleteObject(null);
        }

        internal bool associateBufferData(GLsizeiptr size)
        {
            return this.associateBufferDataImpl(IntPtr.Zero, size);
        }

        internal bool associateBufferData(ArrayBuffer array)
        {
            if (array == null)
            {
                return false;
            }
            var result = this.associateBufferDataImpl(array.@lock(), array.byteLength);
            array.unlock();
            return result;
        }

        internal bool associateBufferData(ArrayBufferView array)
        {
            if (array == null)
            {
                return false;
            }
            var result = this.associateBufferDataImpl(array.buffer.@lock() + array.byteOffset, array.byteLength);
            array.buffer.unlock();
            return result;
        }

        internal bool associateBufferSubData(GLintptr offset, ArrayBuffer array)
        {
            if (array == null)
            {
                return false;
            }
            var result = this.associateBufferSubDataImpl(offset, array.@lock(), array.byteLength);
            array.unlock();
            return result;
        }

        internal bool associateBufferSubData(GLintptr offset, ArrayBufferView array)
        {
            if (array == null)
            {
                return false;
            }
            var result = this.associateBufferSubDataImpl(offset, array.buffer.@lock() + array.byteOffset, array.byteLength);
            array.buffer.unlock();
            return result;
        }

        internal GLsizeiptr byteLength()
        {
            return this.m_byteLength;
        }

        internal ArrayBuffer elementArrayBuffer()
        {
            return this.m_elementArrayBuffer;
        }

        internal int getCachedMaxIndex(GLenum type)
        {
            for (var i = 0; i < this.m_maxIndexCache.Length; ++i)
            {
                if (this.m_maxIndexCache[i].type == type)
                {
                    return this.m_maxIndexCache[i].maxIndex;
                }
            }
            return -1;
        }

        internal void setCachedMaxIndex(GLenum type, int value)
        {
            var numEntries = this.m_maxIndexCache.Length;
            for (var i = 0; i < numEntries; ++i)
            {
                if (this.m_maxIndexCache[i].type == type)
                {
                    this.m_maxIndexCache[i].maxIndex = value;
                    return;
                }
            }
            this.m_maxIndexCache[this.m_nextAvailableCacheEntry].type = type;
            this.m_maxIndexCache[this.m_nextAvailableCacheEntry].maxIndex = value;
            this.m_nextAvailableCacheEntry = (uint)((this.m_nextAvailableCacheEntry + 1) % numEntries);
        }

        internal GLenum getTarget()
        {
            return this.m_target;
        }

        internal void setTarget(GLenum target)
        {
            if (this.m_target != 0)
            {
                return;
            }
            if (target == GraphicsContext3D.ARRAY_BUFFER || target == GraphicsContext3D.ELEMENT_ARRAY_BUFFER)
            {
                this.m_target = target;
            }
        }

        internal bool hasEverBeenBound()
        {
            return this.obj() != 0 && this.m_target != 0;
        }

        internal override void deleteObjectImpl(GraphicsContext3D context3d, Platform3DObject @object)
        {
            context3d.deleteBuffer(@object);
        }

        internal override bool isBuffer()
        {
            return true;
        }

        private void clearCachedMaxIndices()
        {
            for (var i = 0; i < this.m_maxIndexCache.Length; i++)
            {
                this.m_maxIndexCache[i].type = 0;
                this.m_maxIndexCache[i].maxIndex = 0;
            }
        }

        private bool associateBufferDataImpl(IntPtr data, GLsizeiptr byteLength)
        {
            if (byteLength < 0)
            {
                return false;
            }

            switch (this.m_target)
            {
                case GraphicsContext3D.ELEMENT_ARRAY_BUFFER:
                    this.m_byteLength = byteLength;
                    this.clearCachedMaxIndices();
                    if (byteLength != 0)
                    {
                        this.m_elementArrayBuffer = new ArrayBuffer((int)byteLength);
                        if (data != IntPtr.Zero)
                        {
                            Marshal.Copy(data, this.m_elementArrayBuffer.data, 0, (int)byteLength);
                        }
                    }
                    else
                    {
                        this.m_elementArrayBuffer = null;
                    }
                    return true;
                case GraphicsContext3D.ARRAY_BUFFER:
                    this.m_byteLength = byteLength;
                    return true;
                default:
                    return false;
            }
        }

        private bool associateBufferSubDataImpl(GLintptr offset, IntPtr data, GLsizeiptr byteLength)
        {
            if (data == IntPtr.Zero || offset < 0 || byteLength < 0)
            {
                return false;
            }

            if (byteLength != 0)
            {
                var checkedBufferOffset = offset;
                var checkedDataLength = byteLength;
                var checkedBufferMax = checkedBufferOffset + checkedDataLength;
                if (offset > this.m_byteLength || checkedBufferMax > this.m_byteLength)
                {
                    return false;
                }
            }

            switch (this.m_target)
            {
                case GraphicsContext3D.ELEMENT_ARRAY_BUFFER:
                    this.clearCachedMaxIndices();
                    if (byteLength != 0)
                    {
                        if (this.m_elementArrayBuffer == null)
                        {
                            return false;
                        }
                        Marshal.Copy(data, this.m_elementArrayBuffer.data, (int)offset, (int)byteLength);
                    }
                    return true;
                case GraphicsContext3D.ARRAY_BUFFER:
                    return true;
                default:
                    return false;
            }
        }

        private struct MaxIndexCacheEntry
        {
            internal GLenum type;
            internal int maxIndex;
        }
    }

    // ReSharper restore InconsistentNaming
}
