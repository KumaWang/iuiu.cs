using System;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLObject : JSObject, IDisposable
    {
        private Platform3DObject m_object;
        private uint m_attachmentCount;
        private bool m_deleted;

        protected WebGLObject(WebGLRenderingContext context)
        {
            this.m_object = 0;
            this.m_attachmentCount = 0;
            this.m_deleted = false;
        }

        ~WebGLObject()
        {
            this.dispose(false);
        }

        public void Dispose()
        {
            this.dispose(true);
            GC.SuppressFinalize(this);
        }

        internal Platform3DObject obj()
        {
            return this.m_object;
        }

        internal void deleteObject(GraphicsContext3D context3d)
        {
            this.m_deleted = true;
            if (this.m_object == 0)
            {
                return;
            }

            if (!this.hasGroupOrContext())
            {
                return;
            }

            if (this.m_attachmentCount == 0)
            {
                if (context3d == null)
                {
                    context3d = this.getAGraphicsContext3D();
                }

                if (context3d != null)
                {
                    this.deleteObjectImpl(context3d, this.m_object);
                }

                this.m_object = 0;
            }
        }

        internal void onAttached()
        {
            ++this.m_attachmentCount;
        }

        internal void onDetached(GraphicsContext3D context3d)
        {
            if (this.m_attachmentCount != 0)
            {
                --this.m_attachmentCount;
            }
            if (this.m_deleted)
            {
                this.deleteObject(context3d);
            }
        }

        internal bool isDeleted()
        {
            return this.m_deleted;
        }

        internal virtual bool validate(WebGLContextGroup group, WebGLRenderingContext context)
        {
            return false;
        }

        internal void setObject(Platform3DObject @object)
        {
            this.m_object = @object;
        }

        internal virtual void deleteObjectImpl(GraphicsContext3D context, Platform3DObject @object)
        {
        }

        internal virtual bool hasGroupOrContext()
        {
            return false;
        }

        internal virtual void detach()
        {
            this.m_attachmentCount = 0;
        }

        internal virtual GraphicsContext3D getAGraphicsContext3D()
        {
            return null;
        }

        internal virtual void dispose(bool disposing)
        {
            this.deleteObject(null);
        }
    }

    // ReSharper restore InconsistentNaming
}
