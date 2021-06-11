using System.Collections.Generic;
using System.Linq;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLContextGroup
    {
        private readonly HashSet<WebGLRenderingContext> m_contexts = new HashSet<WebGLRenderingContext>();
        private readonly HashSet<WebGLSharedObject> m_groupObjects = new HashSet<WebGLSharedObject>();

        ~WebGLContextGroup()
        {
            this.detachAndRemoveAllObjects();
        }

        internal void addContext(WebGLRenderingContext context)
        {
            this.m_contexts.Add(context);
        }

        internal void removeContext(WebGLRenderingContext context)
        {
            if (this.m_contexts.Count == 1 && this.m_contexts.Contains(context))
            {
                this.detachAndRemoveAllObjects();
            }

            this.m_contexts.Remove(context);
        }

        internal void addObject(WebGLSharedObject @object)
        {
            this.m_groupObjects.Add(@object);
        }

        internal void removeObject(WebGLSharedObject @object)
        {
            this.m_groupObjects.Remove(@object);
        }

        internal GraphicsContext3D getAGraphicsContext3D()
        {
            foreach (var it in this.m_contexts)
            {
                return it.graphicsContext3D();
            }
            return null;
        }

        internal void loseContextGroup(LostContextMode mode)
        {
            foreach (var it in this.m_contexts)
            {
                it.loseContextImpl(mode);
            }

            this.detachAndRemoveAllObjects();
        }

        private void detachAndRemoveAllObjects()
        {
            while (this.m_groupObjects.Count > 0)
            {
                this.m_groupObjects.First().detachContextGroup();
            }
        }
    }

    // ReSharper restore InconsistentNaming
}
