using System.Collections.Generic;
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

    class WebGLFramebuffer : WebGLContextObject
    {
        private readonly Dictionary<GLenum, WebGLAttachment> m_attachments = new Dictionary<uint, WebGLAttachment>();
        private GLboolean m_hasEverBeenBound;
        private GLenum[] m_drawBuffers;
        private GLenum[] m_filteredDrawBuffers;

        internal WebGLFramebuffer(WebGLRenderingContext ctx) : base(ctx)
        {
            this.m_hasEverBeenBound = false;
            this.setObject(ctx.graphicsContext3D().createFramebuffer());
        }

        ~WebGLFramebuffer()
        {
            this.deleteObject(null);
        }

        internal void setAttachmentForBoundFramebuffer(GLenum attachment, GLenum texTarget, WebGLTexture texture, GLint level)
        {
            removeAttachmentFromBoundFramebuffer(attachment);
            if (this.obj() == 0)
            {
                return;
            }
            if (texture != null && texture.obj() != 0)
            {
                this.m_attachments.Add(attachment, new WebGLTextureAttachment(texture, texTarget, level));
                this.drawBuffersIfNecessary(false);
                texture.onAttached();
            }
        }

        internal void setAttachmentForBoundFramebuffer(GLenum attachment, WebGLRenderbuffer renderbuffer)
        {
            removeAttachmentFromBoundFramebuffer(attachment);
            if (this.obj() == 0)
            {
                return;
            }
            if (renderbuffer != null && renderbuffer.obj() != 0)
            {
                this.m_attachments.Add(attachment, new WebGLRenderbufferAttachment(renderbuffer));
                this.drawBuffersIfNecessary(false);
                renderbuffer.onAttached();
            }
        }

        internal void removeAttachmentFromBoundFramebuffer(WebGLSharedObject attachment)
        {
            if (this.obj() == 0)
            {
                return;
            }
            if (attachment == null)
            {
                return;
            }

            var checkMore = true;
            while (checkMore)
            {
                checkMore = false;
                foreach (var it in this.m_attachments)
                {
                    var attachmentObject = it.Value;
                    if (attachmentObject.isSharedObject(attachment))
                    {
                        var attachmentType = it.Key;
                        attachmentObject.unattach(this.context().graphicsContext3D(), attachmentType);
                        removeAttachmentFromBoundFramebuffer(attachmentType);
                        checkMore = true;
                        break;
                    }
                }
            }
        }

        internal void removeAttachmentFromBoundFramebuffer(GLenum attachment)
        {
            if (this.obj() == 0)
            {
                return;
            }

            var attachmentObject = this.getAttachment(attachment);
            if (attachmentObject != null)
            {
                attachmentObject.onDetached(this.context().graphicsContext3D());
                this.m_attachments.Remove(attachment);
                this.drawBuffersIfNecessary(false);
                switch (attachment)
                {
                    case GraphicsContext3D.DEPTH_STENCIL_ATTACHMENT:
                        this.attach(GraphicsContext3D.DEPTH_ATTACHMENT, GraphicsContext3D.DEPTH_ATTACHMENT);
                        this.attach(GraphicsContext3D.STENCIL_ATTACHMENT, GraphicsContext3D.STENCIL_ATTACHMENT);
                        break;
                    case GraphicsContext3D.DEPTH_ATTACHMENT:
                        this.attach(GraphicsContext3D.DEPTH_STENCIL_ATTACHMENT, GraphicsContext3D.DEPTH_ATTACHMENT);
                        break;
                    case GraphicsContext3D.STENCIL_ATTACHMENT:
                        this.attach(GraphicsContext3D.DEPTH_STENCIL_ATTACHMENT, GraphicsContext3D.STENCIL_ATTACHMENT);
                        break;
                }
            }
        }

        internal WebGLSharedObject getAttachmentObject(GLenum attachment)
        {
            if (this.obj() == 0)
            {
                return null;
            }
            var attachmentObject = this.getAttachment(attachment);
            return attachmentObject != null ? attachmentObject.getObject() : null;
        }

        internal GLenum getColorBufferFormat()
        {
            if (this.obj() == 0)
            {
                return 0;
            }
            var attachment = this.getAttachment(GraphicsContext3D.COLOR_ATTACHMENT0);
            if (attachment == null)
            {
                return 0;
            }
            return attachment.getFormat();
        }

        internal GLsizei getColorBufferWidth()
        {
            if (this.obj() == 0)
            {
                return 0;
            }
            var attachment = this.getAttachment(GraphicsContext3D.COLOR_ATTACHMENT0);
            if (attachment == null)
            {
                return 0;
            }

            return attachment.getWidth();
        }

        internal GLsizei getColorBufferHeight()
        {
            if (this.obj() == 0)
            {
                return 0;
            }
            var attachment = this.getAttachment(GraphicsContext3D.COLOR_ATTACHMENT0);
            if (attachment == null)
            {
                return 0;
            }

            return attachment.getHeight();
        }

        internal GLboolean onAccess(GraphicsContext3D context3d, GLboolean needToInitializeAttachments, ref DOMString reason)
        {
            if (this.checkStatus(ref reason) != GraphicsContext3D.FRAMEBUFFER_COMPLETE)
            {
                return false;
            }
            if (needToInitializeAttachments)
            {
                return this.initializeAttachments(context3d, ref reason);
            }
            return true;
        }

        internal GLenum checkStatus(ref DOMString reason)
        {
            uint count = 0;
            GLsizei width = 0, height = 0;
            var haveDepth = false;
            var haveStencil = false;
            var haveDepthStencil = false;
            foreach (var it in this.m_attachments)
            {
                var attachment = it.Value;
                if (!isAttachmentComplete(attachment, it.Key, ref reason))
                {
                    return GraphicsContext3D.FRAMEBUFFER_INCOMPLETE_ATTACHMENT;
                }
                if (!attachment.isValid())
                {
                    reason = "attachment is not valid";
                    return GraphicsContext3D.FRAMEBUFFER_UNSUPPORTED;
                }
                if (attachment.getFormat() == 0)
                {
                    reason = "attachment is an unsupported format";
                    return GraphicsContext3D.FRAMEBUFFER_INCOMPLETE_ATTACHMENT;
                }
                switch (it.Key)
                {
                    case GraphicsContext3D.DEPTH_ATTACHMENT:
                        haveDepth = true;
                        break;
                    case GraphicsContext3D.STENCIL_ATTACHMENT:
                        haveStencil = true;
                        break;
                    case GraphicsContext3D.DEPTH_STENCIL_ATTACHMENT:
                        haveDepthStencil = true;
                        break;
                }
                if (count == 0)
                {
                    width = attachment.getWidth();
                    height = attachment.getHeight();
                }
                else
                {
                    if (width != attachment.getWidth() || height != attachment.getHeight())
                    {
                        reason = "attachments do not have the same dimensions";
                        return GraphicsContext3D.FRAMEBUFFER_INCOMPLETE_DIMENSIONS;
                    }
                }
                ++count;
            }
            if (count == 0)
            {
                reason = "no attachments";
                return GraphicsContext3D.FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT;
            }
            if (width == 0 || height == 0)
            {
                reason = "framebuffer has a 0 dimension";
                return GraphicsContext3D.FRAMEBUFFER_INCOMPLETE_ATTACHMENT;
            }
            // WebGL specific: no conflicting DEPTH/STENCIL/DEPTH_STENCIL attachments.
            if ((haveDepthStencil && (haveDepth || haveStencil)) || (haveDepth && haveStencil))
            {
                reason = "conflicting DEPTH/STENCIL/DEPTH_STENCIL attachments";
                return GraphicsContext3D.FRAMEBUFFER_UNSUPPORTED;
            }
            return GraphicsContext3D.FRAMEBUFFER_COMPLETE;
        }

        internal GLboolean hasEverBeenBound()
        {
            return this.obj() != 0 && this.m_hasEverBeenBound;
        }

        internal void setHasEverBeenBound()
        {
            this.m_hasEverBeenBound = true;
        }

        internal GLboolean hasStencilBuffer()
        {
            var attachment = this.getAttachment(GraphicsContext3D.STENCIL_ATTACHMENT) ?? this.getAttachment(GraphicsContext3D.DEPTH_STENCIL_ATTACHMENT);
            return attachment != null && attachment.isValid();
        }

        internal void drawBuffers(GLenum[] bufs)
        {
            this.m_drawBuffers = bufs;
            this.m_filteredDrawBuffers = new uint[this.m_drawBuffers.Length];
            for (var i = 0; i < this.m_filteredDrawBuffers.Length; ++i)
            {
                this.m_filteredDrawBuffers[i] = GraphicsContext3D.NONE;
            }
            this.drawBuffersIfNecessary(true);
        }

        internal GLenum getDrawBuffer(GLenum drawBuffer)
        {
            var index = (int)(drawBuffer - Extensions3D.DRAW_BUFFER0_EXT);
            if (index < this.m_drawBuffers.Length)
            {
                return this.m_drawBuffers[index];
            }
            if (drawBuffer == Extensions3D.DRAW_BUFFER0_EXT)
            {
                return GraphicsContext3D.COLOR_ATTACHMENT0;
            }
            return GraphicsContext3D.NONE;
        }

        internal override void deleteObjectImpl(GraphicsContext3D context3d, Platform3DObject @object)
        {
            foreach (var it in this.m_attachments)
            {
                it.Value.onDetached(context3d);
            }

            context3d.deleteFramebuffer(@object);
        }

        internal GLboolean isFramebuffer()
        {
            return true;
        }

        private WebGLAttachment getAttachment(GLenum attachment)
        {
            if (this.m_attachments.ContainsKey(attachment))
            {
                return this.m_attachments[attachment];
            }
            return null;
        }

        private GLboolean initializeAttachments(GraphicsContext3D g3d, ref DOMString reason)
        {
            GLbitfield mask = 0;

            foreach (var it in this.m_attachments)
            {
                var attachmentType = it.Key;
                var attachment = it.Value;
                if (!attachment.isInitialized())
                {
                    mask |= DataFormat.getClearBitsByAttachmentType(attachmentType);
                }
            }
            if (mask == 0)
            {
                return true;
            }

            // We only clear un-initialized renderbuffers when they are ready to be
            // read, i.e., when the framebuffer is complete.
            if (g3d.checkFramebufferStatus(GraphicsContext3D.FRAMEBUFFER) != GraphicsContext3D.FRAMEBUFFER_COMPLETE)
            {
                reason = "framebuffer not complete";
                return false;
            }

            var initColor = (mask & GraphicsContext3D.COLOR_BUFFER_BIT) != 0;
            var initDepth = (mask & GraphicsContext3D.DEPTH_BUFFER_BIT) != 0;
            var initStencil = (mask & GraphicsContext3D.STENCIL_BUFFER_BIT) != 0;

            GLfloat[] colorClearValue = {0, 0, 0, 0};
            var depthClearValue = new float[1];
            var stencilClearValue = new int[1];
            byte[] colorMask = {0, 0, 0, 0};
            var depthMask = new byte[1];
            var stencilMask = new[] {-1};
            if (initColor)
            {
                g3d.getFloatv(GraphicsContext3D.COLOR_CLEAR_VALUE, colorClearValue);
                g3d.getBooleanv(GraphicsContext3D.COLOR_WRITEMASK, colorMask);
                g3d.clearColor(0, 0, 0, 0);
                g3d.colorMask(true, true, true, true);
            }
            if (initDepth)
            {
                g3d.getFloatv(GraphicsContext3D.DEPTH_CLEAR_VALUE, depthClearValue);
                g3d.getBooleanv(GraphicsContext3D.DEPTH_WRITEMASK, depthMask);
                g3d.clearDepth(1.0f);
                g3d.depthMask(true);
            }
            if (initStencil)
            {
                g3d.getIntegerv(GraphicsContext3D.STENCIL_CLEAR_VALUE, stencilClearValue);
                g3d.getIntegerv(GraphicsContext3D.STENCIL_WRITEMASK, stencilMask);
                g3d.clearStencil(0);
                g3d.stencilMask(0xffffffff);
            }
            var isScissorEnabled = g3d.isEnabled(GraphicsContext3D.SCISSOR_TEST);
            g3d.disable(GraphicsContext3D.SCISSOR_TEST);
            var isDitherEnabled = g3d.isEnabled(GraphicsContext3D.DITHER);
            g3d.disable(GraphicsContext3D.DITHER);

            g3d.clear(mask);

            if (initColor)
            {
                g3d.clearColor(colorClearValue[0], colorClearValue[1], colorClearValue[2], colorClearValue[3]);
                g3d.colorMask(colorMask[0] != 0, colorMask[1] != 0, colorMask[2] != 0, colorMask[3] != 0);
            }
            if (initDepth)
            {
                g3d.clearDepth(depthClearValue[0]);
                g3d.depthMask(depthMask[0] != 0);
            }
            if (initStencil)
            {
                g3d.clearStencil(stencilClearValue[0]);
                g3d.stencilMask((uint)stencilMask[0]);
            }
            if (isScissorEnabled)
            {
                g3d.enable(GraphicsContext3D.SCISSOR_TEST);
            }
            else
            {
                g3d.disable(GraphicsContext3D.SCISSOR_TEST);
            }
            if (isDitherEnabled)
            {
                g3d.enable(GraphicsContext3D.DITHER);
            }
            else
            {
                g3d.disable(GraphicsContext3D.DITHER);
            }

            foreach (var it in this.m_attachments)
            {
                var attachmentType = it.Key;
                var attachment = it.Value;
                var bits = DataFormat.getClearBitsByAttachmentType(attachmentType);
                if ((bits & mask) != 0)
                {
                    attachment.setInitialized();
                }
            }

            return true;
        }

        public GLboolean isBound()
        {
            return (this.context().m_framebufferBinding == this);
        }

        private void attach(GLenum attachment, GLenum attachmentPoint)
        {
            var attachmentObject = this.getAttachment(attachment);
            if (attachmentObject != null)
            {
                attachmentObject.attach(this.context().graphicsContext3D(), attachmentPoint);
            }
        }

        private void drawBuffersIfNecessary(GLboolean force)
        {
            if (this.context().m_webglDrawBuffers == null)
            {
                return;
            }
            var reset = force;
            // This filtering works around graphics driver bugs on Mac OS X.
            for (var i = 0; i < this.m_drawBuffers.Length; ++i)
            {
                if (this.m_drawBuffers[i] != GraphicsContext3D.NONE && this.getAttachment(this.m_drawBuffers[i]) != null)
                {
                    if (this.m_filteredDrawBuffers[i] != this.m_drawBuffers[i])
                    {
                        this.m_filteredDrawBuffers[i] = this.m_drawBuffers[i];
                        reset = true;
                    }
                }
                else
                {
                    if (this.m_filteredDrawBuffers[i] != GraphicsContext3D.NONE)
                    {
                        this.m_filteredDrawBuffers[i] = GraphicsContext3D.NONE;
                        reset = true;
                    }
                }
            }
            if (reset)
            {
                this.context().graphicsContext3D().getExtensions().drawBuffersEXT(this.m_filteredDrawBuffers.Length, this.m_filteredDrawBuffers);
            }
        }

        private static GLboolean isAttachmentComplete(WebGLAttachment attachedObject, GLenum attachment, ref DOMString reason)
        {
            var format = attachedObject.getFormat();
            var need = DataFormat.getClearBitsByAttachmentType(attachment);
            var have = DataFormat.getClearBitsByFormat(format);

            if ((need & have) != need)
            {
                reason = "attachment type is not correct for attachment";
                return false;
            }
            if (attachedObject.getWidth() == 0 || attachedObject.getHeight() == 0)
            {
                reason = "attachment has a 0 dimension";
                return false;
            }
            if ((attachment == GraphicsContext3D.DEPTH_ATTACHMENT || attachment == GraphicsContext3D.STENCIL_ATTACHMENT)
                && format == GraphicsContext3D.DEPTH_STENCIL)
            {
                reason = "attachment DEPTH_STENCIL not allowed on DEPTH or STENCIL attachment";
                return false;
            }
            return true;
        }
    }

    // ReSharper restore InconsistentNaming
}
