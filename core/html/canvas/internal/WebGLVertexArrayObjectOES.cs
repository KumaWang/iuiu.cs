using System.Linq;
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

    class WebGLVertexArrayObjectOES : WebGLContextObject
    {
        private readonly VaoType m_type;
        private bool m_hasEverBeenBound;
        private WebGLBuffer m_boundElementArrayBuffer;
        private readonly VertexAttribState[] m_vertexAttribState;

        internal WebGLVertexArrayObjectOES(WebGLRenderingContext ctx, VaoType type) : base(ctx)
        {
            this.m_type = type;
            this.m_hasEverBeenBound = false;
            this.m_boundElementArrayBuffer = null;

            this.m_vertexAttribState = Enumerable.Range(0, (int)ctx.getMaxVertexAttribs()).Select(x => new VertexAttribState()).ToArray();

            var extensions = this.context().graphicsContext3D().getExtensions();
            switch (this.m_type)
            {
                case VaoType.VaoTypeDefault:
                    break;
                default:
                    this.setObject(extensions.createVertexArrayOES());
                    break;
            }
        }

        ~WebGLVertexArrayObjectOES()
        {
            this.deleteObject(null);
        }

        internal bool isDefaultObject()
        {
            return this.m_type == VaoType.VaoTypeDefault;
        }

        internal bool hasEverBeenBound()
        {
            return this.obj() != 0 && this.m_hasEverBeenBound;
        }

        internal void setHasEverBeenBound()
        {
            this.m_hasEverBeenBound = true;
        }

        internal WebGLBuffer getElementArrayBuffer()
        {
            return this.m_boundElementArrayBuffer;
        }

        internal void setElementArrayBuffer(WebGLBuffer buffer)
        {
            if (buffer != null)
            {
                buffer.onAttached();
            }
            if (this.m_boundElementArrayBuffer != null)
            {
                this.m_boundElementArrayBuffer.onDetached(this.context().graphicsContext3D());
            }
            this.m_boundElementArrayBuffer = buffer;
        }

        internal VertexAttribState getVertexAttribState(int index)
        {
            return this.m_vertexAttribState[index];
        }

        internal void setVertexAttribState(GLuint index, GLsizei bytesPerElement, GLint size, GLenum type, GLboolean normalized, GLsizei stride, GLintptr offset, WebGLBuffer buffer)
        {
            var validatedStride = stride != 0 ? stride : bytesPerElement;

            var state = this.m_vertexAttribState[index];

            if (buffer != null)
            {
                buffer.onAttached();
            }
            if (state.bufferBinding != null)
            {
                state.bufferBinding.onDetached(this.context().graphicsContext3D());
            }

            state.bufferBinding = buffer;
            state.bytesPerElement = bytesPerElement;
            state.size = size;
            state.type = type;
            state.normalized = normalized;
            state.stride = validatedStride;
            state.originalStride = stride;
            state.offset = offset;
        }

        internal void unbindBuffer(WebGLBuffer buffer)
        {
            if (this.m_boundElementArrayBuffer == buffer)
            {
                this.m_boundElementArrayBuffer.onDetached(this.context().graphicsContext3D());
                this.m_boundElementArrayBuffer = null;
            }

            for (var i = 0; i < this.m_vertexAttribState.Length; ++i)
            {
                var state = this.m_vertexAttribState[i];
                if (state.bufferBinding == buffer)
                {
                    buffer.onDetached(this.context().graphicsContext3D());
                    state.bufferBinding = null;
                }
            }
        }

        internal void setVertexAttribDivisor(GLuint index, GLuint divisor)
        {
            var state = this.m_vertexAttribState[index];
            state.divisor = divisor;
        }

        internal override void deleteObjectImpl(GraphicsContext3D context3d, Platform3DObject @object)
        {
            var extensions = context3d.getExtensions();
            switch (this.m_type)
            {
                case VaoType.VaoTypeDefault:
                    break;
                default:
                    extensions.deleteVertexArrayOES(@object);
                    break;
            }

            if (this.m_boundElementArrayBuffer != null)
            {
                this.m_boundElementArrayBuffer.onDetached(context3d);
            }

            foreach (var state in this.m_vertexAttribState)
            {
                if (state.bufferBinding != null)
                {
                    state.bufferBinding.onDetached(context3d);
                }
            }
        }

        internal enum VaoType
        {
            VaoTypeDefault,
            VaoTypeUser,
        }

        internal class VertexAttribState
        {
            internal bool enabled;
            internal WebGLBuffer bufferBinding;
            internal GLsizei bytesPerElement;
            internal GLint size;
            internal GLenum type;
            internal bool normalized;
            internal GLsizei stride;
            internal GLsizei originalStride;
            internal GLintptr offset;
            internal GLuint divisor;

            internal VertexAttribState()
            {
                this.enabled = false;
                this.bytesPerElement = 0;
                this.size = 4;
                this.type = GraphicsContext3D.FLOAT;
                this.normalized = false;
                this.stride = 16;
                this.originalStride = 0;
                this.offset = 0;
                this.divisor = 0;
            }

            internal bool isBound()
            {
                return this.bufferBinding != null && this.bufferBinding.obj() != 0;
            }

            internal bool validateBinding()
            {
                return !this.enabled || this.isBound();
            }
        }
    }

    // ReSharper restore InconsistentNaming
}
