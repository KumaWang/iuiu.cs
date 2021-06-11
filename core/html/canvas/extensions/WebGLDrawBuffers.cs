using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class WebGLDrawBuffers : WebGLExtension
    {
        internal WebGLDrawBuffers(WebGLRenderingContext context) : base(context)
        {
        }

        internal static bool supported(WebGLRenderingContext context)
        {
            var extensions = context.graphicsContext3D().getExtensions();
            return (extensions.supports("GL_EXT_draw_buffers") && satisfiesWebGLRequirements(context));
        }

        internal override ExtensionName getName()
        {
            return ExtensionName.WebGLDrawBuffersName;
        }

        internal void drawBuffersWEBGL(uint[] buffers)
        {
            if (this.m_context.isContextLost())
            {
                return;
            }
            var n = buffers.Length;
            var bufs = buffers;
            if (this.m_context.m_framebufferBinding == null)
            {
                if (n != 1)
                {
                    this.m_context.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "drawBuffersWEBGL", "more than one buffer");
                    return;
                }
                if (bufs[0] != GraphicsContext3D.BACK && bufs[0] != GraphicsContext3D.NONE)
                {
                    this.m_context.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "drawBuffersWEBGL", "BACK or NONE");
                    return;
                }
                // Because the backbuffer is simulated on all current WebKit ports, we need to change BACK to COLOR_ATTACHMENT0.
                var value = new uint[(bufs[0] == GraphicsContext3D.BACK) ? GraphicsContext3D.COLOR_ATTACHMENT0 : GraphicsContext3D.NONE];
                this.m_context.graphicsContext3D().getExtensions().drawBuffersEXT(1, value);
                this.m_context.setBackDrawBuffer(bufs[0]);
            }
            else
            {
                if (n > this.m_context.getMaxDrawBuffers())
                {
                    this.m_context.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, "drawBuffersWEBGL", "more than max draw buffers");
                    return;
                }
                for (var i = 0; i < n; ++i)
                {
                    if (bufs[i] != GraphicsContext3D.NONE && bufs[i] != Extensions3D.COLOR_ATTACHMENT0_EXT + i)
                    {
                        this.m_context.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, "drawBuffersWEBGL", "COLOR_ATTACHMENTi_EXT or NONE");
                        return;
                    }
                }
                this.m_context.m_framebufferBinding.drawBuffers(buffers);
            }
        }

        private static bool satisfiesWebGLRequirements(WebGLRenderingContext webglContext)
        {
            var context = webglContext.graphicsContext3D();

            var maxDrawBuffers = new int[1];
            var maxColorAttachments = new int[1];
            context.getIntegerv(Extensions3D.MAX_DRAW_BUFFERS_EXT, maxDrawBuffers);
            context.getIntegerv(Extensions3D.MAX_COLOR_ATTACHMENTS_EXT, maxColorAttachments);
            if (maxDrawBuffers[0] < 4 || maxColorAttachments[0] < 4)
            {
                return false;
            }

            var fbo = context.createFramebuffer();
            context.bindFramebuffer(GraphicsContext3D.FRAMEBUFFER, fbo);

            byte[] buffer = {0, 0, 0, 0};
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            var supportsDepth = (context.getExtensions().supports("GL_CHROMIUM_depth_texture")
                                 || context.getExtensions().supports("GL_OES_depth_texture")
                                 || context.getExtensions().supports("GL_ARB_depth_texture"));
            var supportsDepthStencil = (context.getExtensions().supports("GL_EXT_packed_depth_stencil")
                                        || context.getExtensions().supports("GL_OES_packed_depth_stencil"));
            Platform3DObject depthStencil = 0;
            if (supportsDepthStencil)
            {
                depthStencil = context.createTexture();
                context.bindTexture(GraphicsContext3D.TEXTURE_2D, depthStencil);
                context.texImage2D(GraphicsContext3D.TEXTURE_2D, 0, GraphicsContext3D.DEPTH_STENCIL, 1, 1, 0, GraphicsContext3D.DEPTH_STENCIL, GraphicsContext3D.UNSIGNED_INT_24_8, handle.AddrOfPinnedObject());
            }
            Platform3DObject depth = 0;
            if (supportsDepth)
            {
                depth = context.createTexture();
                context.bindTexture(GraphicsContext3D.TEXTURE_2D, depth);
                context.texImage2D(GraphicsContext3D.TEXTURE_2D, 0, GraphicsContext3D.DEPTH_COMPONENT, 1, 1, 0, GraphicsContext3D.DEPTH_COMPONENT, GraphicsContext3D.UNSIGNED_INT, handle.AddrOfPinnedObject());
            }

            var colors = new List<Platform3DObject>();
            var ok = true;
            var maxAllowedBuffers = Math.Min(maxDrawBuffers[0], maxColorAttachments[0]);
            for (var i = 0; i < maxAllowedBuffers; ++i)
            {
                var color = context.createTexture();
                colors.Add(color);
                context.bindTexture(GraphicsContext3D.TEXTURE_2D, color);
                context.texImage2D(GraphicsContext3D.TEXTURE_2D, 0, GraphicsContext3D.RGBA, 1, 1, 0, GraphicsContext3D.RGBA, GraphicsContext3D.UNSIGNED_BYTE, handle.AddrOfPinnedObject());
                context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, (uint)(GraphicsContext3D.COLOR_ATTACHMENT0 + i), GraphicsContext3D.TEXTURE_2D, color, 0);
                if (context.checkFramebufferStatus(GraphicsContext3D.FRAMEBUFFER) != GraphicsContext3D.FRAMEBUFFER_COMPLETE)
                {
                    ok = false;
                    break;
                }
                if (supportsDepth)
                {
                    context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, GraphicsContext3D.DEPTH_ATTACHMENT, GraphicsContext3D.TEXTURE_2D, depth, 0);
                    if (context.checkFramebufferStatus(GraphicsContext3D.FRAMEBUFFER) != GraphicsContext3D.FRAMEBUFFER_COMPLETE)
                    {
                        ok = false;
                        break;
                    }
                    context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, GraphicsContext3D.DEPTH_ATTACHMENT, GraphicsContext3D.TEXTURE_2D, 0, 0);
                }
                if (supportsDepthStencil)
                {
                    context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, GraphicsContext3D.DEPTH_ATTACHMENT, GraphicsContext3D.TEXTURE_2D, depthStencil, 0);
                    context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, GraphicsContext3D.STENCIL_ATTACHMENT, GraphicsContext3D.TEXTURE_2D, depthStencil, 0);
                    if (context.checkFramebufferStatus(GraphicsContext3D.FRAMEBUFFER) != GraphicsContext3D.FRAMEBUFFER_COMPLETE)
                    {
                        ok = false;
                        break;
                    }
                    context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, GraphicsContext3D.DEPTH_ATTACHMENT, GraphicsContext3D.TEXTURE_2D, 0, 0);
                    context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, GraphicsContext3D.STENCIL_ATTACHMENT, GraphicsContext3D.TEXTURE_2D, 0, 0);
                }
            }

            webglContext.restoreCurrentFramebuffer();
            context.deleteFramebuffer(fbo);
            webglContext.restoreCurrentTexture();
            if (supportsDepth)
            {
                context.deleteTexture(depth);
            }
            if (supportsDepthStencil)
            {
                context.deleteTexture(depthStencil);
            }
            foreach (var t in colors)
            {
                context.deleteTexture(t);
            }
            handle.Free();
            return ok;
        }
    }

    // ReSharper restore InconsistentNaming
}
