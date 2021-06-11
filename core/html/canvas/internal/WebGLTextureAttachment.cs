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

    class WebGLTextureAttachment : WebGLAttachment
    {
        private readonly WebGLTexture m_texture;
        private readonly GLenum m_target;
        private readonly GLint m_level;

        internal WebGLTextureAttachment(WebGLTexture texture, GLenum target, GLint level)
        {
            this.m_texture = texture;
            this.m_target = target;
            this.m_level = level;
        }

        internal override GLsizei getWidth()
        {
            return this.m_texture.getWidth(this.m_target, this.m_level);
        }

        internal override GLsizei getHeight()
        {
            return this.m_texture.getHeight(this.m_target, this.m_level);
        }

        internal override GLenum getFormat()
        {
            return this.m_texture.getInternalFormat(this.m_target, this.m_level);
        }

        internal override WebGLSharedObject getObject()
        {
            return this.m_texture.obj() != 0 ? this.m_texture : null;
        }

        internal override bool isSharedObject(WebGLSharedObject @object)
        {
            return @object == this.m_texture;
        }

        internal override bool isValid()
        {
            return this.m_texture.obj() != 0;
        }

        internal override bool isInitialized()
        {
            // Textures are assumed to be initialized.
            return true;
        }

        internal override void setInitialized()
        {
            // Textures are assumed to be initialized.
        }

        internal override void onDetached(GraphicsContext3D context)
        {
            this.m_texture.onDetached(context);
        }

        internal override void attach(GraphicsContext3D context, GLenum attachment)
        {
            var @object = objectOrZero(this.m_texture);
            context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, attachment, this.m_target, @object, this.m_level);
        }

        internal override void unattach(GraphicsContext3D context, GLenum attachment)
        {
            if (attachment == GraphicsContext3D.DEPTH_STENCIL_ATTACHMENT)
            {
                context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, GraphicsContext3D.DEPTH_ATTACHMENT, this.m_target, 0, this.m_level);
                context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, GraphicsContext3D.STENCIL_ATTACHMENT, this.m_target, 0, this.m_level);
            }
            else
            {
                context.framebufferTexture(GraphicsContext3D.FRAMEBUFFER, attachment, this.m_target, 0, this.m_level);
            }
        }

        private static Platform3DObject objectOrZero(WebGLObject @object)
        {
            return @object != null ? @object.obj() : 0;
        }
    }

    // ReSharper restore InconsistentNaming
}
