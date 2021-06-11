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

    class WebGLProgram : WebGLSharedObject
    {
        private GLint[] m_activeAttribLocations;
        private GLint m_linkStatus;
        private uint m_linkCount;
        private WebGLShader m_vertexShader;
        private WebGLShader m_fragmentShader;
        private bool m_infoValid;

        internal WebGLProgram(WebGLRenderingContext ctx) : base(ctx)
        {
            this.m_linkStatus = 0;
            this.m_linkCount = 0;
            this.m_infoValid = true;

            this.setObject(ctx.graphicsContext3D().createProgram());
        }

        ~WebGLProgram()
        {
            this.deleteObject(null);
        }

        internal uint numActiveAttribLocations()
        {
            this.cacheInfoIfNeeded();
            return (uint)this.m_activeAttribLocations.Length;
        }

        internal GLint getActiveAttribLocation(GLuint index)
        {
            this.cacheInfoIfNeeded();
            if (index >= this.numActiveAttribLocations())
            {
                return -1;
            }
            return this.m_activeAttribLocations[(int)index];
        }

        internal bool getLinkStatus()
        {
            this.cacheInfoIfNeeded();
            return this.m_linkStatus != 0;
        }

        internal void setLinkStatus(bool status)
        {
            this.cacheInfoIfNeeded();
            this.m_linkStatus = status ? 1 : 0;
        }

        internal uint getLinkCount()
        {
            return this.m_linkCount;
        }

        internal void increaseLinkCount()
        {
            ++this.m_linkCount;
            this.m_infoValid = false;
        }

        internal WebGLShader getAttachedShader(GLenum type)
        {
            switch (type)
            {
                case GraphicsContext3D.VERTEX_SHADER:
                    return this.m_vertexShader;
                case GraphicsContext3D.FRAGMENT_SHADER:
                    return this.m_fragmentShader;
                default:
                    return null;
            }
        }

        internal bool attachShader(WebGLShader shader)
        {
            if (shader == null || shader.obj() == 0)
            {
                return false;
            }
            switch (shader.getType())
            {
                case GraphicsContext3D.VERTEX_SHADER:
                    if (this.m_vertexShader != null)
                    {
                        return false;
                    }
                    this.m_vertexShader = shader;
                    return true;
                case GraphicsContext3D.FRAGMENT_SHADER:
                    if (this.m_fragmentShader != null)
                    {
                        return false;
                    }
                    this.m_fragmentShader = shader;
                    return true;
                default:
                    return false;
            }
        }

        internal bool detachShader(WebGLShader shader)
        {
            if (shader == null || shader.obj() == 0)
            {
                return false;
            }
            switch (shader.getType())
            {
                case GraphicsContext3D.VERTEX_SHADER:
                    if (this.m_vertexShader != shader)
                    {
                        return false;
                    }
                    this.m_vertexShader = null;
                    return true;
                case GraphicsContext3D.FRAGMENT_SHADER:
                    if (this.m_fragmentShader != shader)
                    {
                        return false;
                    }
                    this.m_fragmentShader = null;
                    return true;
                default:
                    return false;
            }
        }

        internal override void deleteObjectImpl(GraphicsContext3D context3d, Platform3DObject @object)
        {
            context3d.deleteProgram(@object);
            if (this.m_vertexShader != null)
            {
                this.m_vertexShader.onDetached(context3d);
                this.m_vertexShader = null;
            }
            if (this.m_fragmentShader != null)
            {
                this.m_fragmentShader.onDetached(context3d);
                this.m_fragmentShader = null;
            }
        }

        internal override bool isProgram()
        {
            return true;
        }

        private void cacheActiveAttribLocations(GraphicsContext3D context3d)
        {
            int numAttribs;
            context3d.getProgramiv(this.obj(), GraphicsContext3D.ACTIVE_ATTRIBUTES, out numAttribs);
            this.m_activeAttribLocations = new int[numAttribs];
            for (var i = 0; i < numAttribs; ++i)
            {
                var info = new ActiveInfo();
                context3d.getActiveAttribImpl(this.obj(), (uint)i, info);
                this.m_activeAttribLocations[i] = context3d.getAttribLocation(this.obj(), info.name);
            }
        }

        private void cacheInfoIfNeeded()
        {
            if (this.m_infoValid)
            {
                return;
            }

            if (this.obj() == 0)
            {
                return;
            }

            var context = this.getAGraphicsContext3D();
            if (context == null)
            {
                return;
            }
            var linkStatus = 0;
            context.getProgramiv(this.obj(), GraphicsContext3D.LINK_STATUS, out linkStatus);
            this.m_linkStatus = linkStatus;
            if (this.m_linkStatus != 0)
            {
                this.cacheActiveAttribLocations(context);
            }
            this.m_infoValid = true;
        }
    }

    // ReSharper restore InconsistentNaming
}
