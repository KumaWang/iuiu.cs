using System;
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

    class Validation
    {
        public readonly WebGLRenderingContext m_renderingContext;

        public Validation(WebGLRenderingContext renderingContext)
        {
            this.m_renderingContext = renderingContext;
        }

        public bool validateBlendEquation(DOMString functionName, GLenum mode)
        {
            switch (mode)
            {
                case GraphicsContext3D.FUNC_ADD:
                case GraphicsContext3D.FUNC_SUBTRACT:
                case GraphicsContext3D.FUNC_REVERSE_SUBTRACT:
                    return true;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid mode");
                    return false;
            }
        }

        public bool validateBlendFuncFactors(DOMString functionName, GLenum src, GLenum dst)
        {
            if ((src == GraphicsContext3D.CONSTANT_COLOR || src == GraphicsContext3D.ONE_MINUS_CONSTANT_COLOR) &&
                (dst == GraphicsContext3D.CONSTANT_ALPHA || dst == GraphicsContext3D.ONE_MINUS_CONSTANT_ALPHA) ||
                (dst == GraphicsContext3D.CONSTANT_COLOR || dst == GraphicsContext3D.ONE_MINUS_CONSTANT_COLOR) &&
                (src == GraphicsContext3D.CONSTANT_ALPHA || src == GraphicsContext3D.ONE_MINUS_CONSTANT_ALPHA))
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "incompatible src and dst");
                return false;
            }
            return true;
        }

        public bool validateCapability(DOMString functionName, GLenum cap)
        {
            switch (cap)
            {
                case GraphicsContext3D.BLEND:
                case GraphicsContext3D.CULL_FACE:
                case GraphicsContext3D.DEPTH_TEST:
                case GraphicsContext3D.DITHER:
                case GraphicsContext3D.POLYGON_OFFSET_FILL:
                case GraphicsContext3D.SAMPLE_ALPHA_TO_COVERAGE:
                case GraphicsContext3D.SAMPLE_COVERAGE:
                case GraphicsContext3D.SCISSOR_TEST:
                case GraphicsContext3D.STENCIL_TEST:
                    return true;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid capability");
                    return false;
            }
        }

        public WebGLBuffer validateBufferDataParameters(DOMString functionName, GLenum target, GLenum usage)
        {
            WebGLBuffer buffer;
            switch (target)
            {
                case GraphicsContext3D.ELEMENT_ARRAY_BUFFER:
                    buffer = this.m_renderingContext.m_boundVertexArrayObject.getElementArrayBuffer();
                    break;
                case GraphicsContext3D.ARRAY_BUFFER:
                    buffer = this.m_renderingContext.m_boundArrayBuffer;
                    break;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid target");
                    return null;
            }
            if (buffer == null)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "no buffer");
                return null;
            }
            switch (usage)
            {
                case GraphicsContext3D.STREAM_DRAW:
                case GraphicsContext3D.STATIC_DRAW:
                case GraphicsContext3D.DYNAMIC_DRAW:
                    return buffer;
            }
            this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid usage");
            return null;
        }

        public bool validateCompressedTexDimensions(DOMString functionName, GLenum target, GLint level, GLsizei width, GLsizei height, GLenum format)
        {
            switch (format)
            {
                case Extensions3D.COMPRESSED_RGB_S3TC_DXT1_EXT:
                case Extensions3D.COMPRESSED_RGBA_S3TC_DXT1_EXT:
                case Extensions3D.COMPRESSED_RGBA_S3TC_DXT3_EXT:
                case Extensions3D.COMPRESSED_RGBA_S3TC_DXT5_EXT:
                {
                    const int kBlockWidth = 4;
                    const int kBlockHeight = 4;
                    var maxTextureSize = target != 0 ? this.m_renderingContext.m_maxTextureSize : this.m_renderingContext.m_maxCubeMapTextureSize;
                    var maxCompressedDimension = maxTextureSize >> level;
                    var widthValid = (level != 0 && width == 1) || (level != 0 && width == 2) || (0 == (width % kBlockWidth) && width <= maxCompressedDimension);
                    var heightValid = (level != 0 && height == 1) || (level != 0 && height == 2) || (0 == (height % kBlockHeight) && height <= maxCompressedDimension);
                    if (!widthValid || !heightValid)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "width or height invalid for level");
                        return false;
                    }
                    return true;
                }
                default:
                    return false;
            }
        }

        public bool validateDrawMode(DOMString functionName, GLenum mode)
        {
            switch (mode)
            {
                case GraphicsContext3D.POINTS:
                case GraphicsContext3D.LINE_STRIP:
                case GraphicsContext3D.LINE_LOOP:
                case GraphicsContext3D.LINES:
                case GraphicsContext3D.TRIANGLE_STRIP:
                case GraphicsContext3D.TRIANGLE_FAN:
                case GraphicsContext3D.TRIANGLES:
                    return true;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid draw mode");
                    return false;
            }
        }

        public bool validateStencilFunc(DOMString functionName, GLenum func)
        {
            switch (func)
            {
                case GraphicsContext3D.NEVER:
                case GraphicsContext3D.LESS:
                case GraphicsContext3D.LEQUAL:
                case GraphicsContext3D.GREATER:
                case GraphicsContext3D.GEQUAL:
                case GraphicsContext3D.EQUAL:
                case GraphicsContext3D.NOTEQUAL:
                case GraphicsContext3D.ALWAYS:
                    return true;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid function");
                    return false;
            }
        }

        public bool validateStencilSettings(DOMString functionName)
        {
            if (this.m_renderingContext.m_stencilMask != this.m_renderingContext.m_stencilMaskBack ||
                this.m_renderingContext.m_stencilFuncRef != this.m_renderingContext.m_stencilFuncRefBack ||
                this.m_renderingContext.m_stencilFuncMask != this.m_renderingContext.m_stencilFuncMaskBack)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "front and back stencils settings do not match");
                return false;
            }
            return true;
        }

        public bool validateTexFuncLevel(DOMString functionName, GLenum target, GLint level)
        {
            if (level < 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "level < 0");
                return false;
            }
            switch (target)
            {
                case GraphicsContext3D.TEXTURE_2D:
                    if (level >= this.m_renderingContext.m_maxTextureLevel)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "level out of range");
                        return false;
                    }
                    break;
                case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_X:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_X:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Y:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Y:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Z:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Z:
                    if (level >= this.m_renderingContext.m_maxCubeMapTextureLevel)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "level out of range");
                        return false;
                    }
                    break;
            }
            // This function only checks if level is legal, so we return true and don't
            // generate INVALID_ENUM if target is illegal.
            return true;
        }

        public bool validateTexFuncFormatAndType(DOMString functionName, GLenum format, GLenum type, GLint level)
        {
            switch (format)
            {
                case GraphicsContext3D.ALPHA:
                case GraphicsContext3D.LUMINANCE:
                case GraphicsContext3D.LUMINANCE_ALPHA:
                case GraphicsContext3D.RGB:
                case GraphicsContext3D.RGBA:
                    break;
                case GraphicsContext3D.DEPTH_STENCIL:
                case GraphicsContext3D.DEPTH_COMPONENT:
                    if (this.m_renderingContext.m_webglDepthTexture != null)
                    {
                        break;
                    }
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "depth texture formats not enabled");
                    return false;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid texture format");
                    return false;
            }

            switch (type)
            {
                case GraphicsContext3D.UNSIGNED_BYTE:
                case GraphicsContext3D.UNSIGNED_SHORT_5_6_5:
                case GraphicsContext3D.UNSIGNED_SHORT_4_4_4_4:
                case GraphicsContext3D.UNSIGNED_SHORT_5_5_5_1:
                    break;
                case GraphicsContext3D.FLOAT:
                    if (this.m_renderingContext.m_oesTextureFloat != null)
                    {
                        break;
                    }
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid texture type");
                    return false;
                case GraphicsContext3D.HALF_FLOAT_OES:
                    if (this.m_renderingContext.m_oesTextureHalfFloat != null)
                    {
                        break;
                    }
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid texture type");
                    return false;
                case GraphicsContext3D.UNSIGNED_INT:
                case GraphicsContext3D.UNSIGNED_INT_24_8:
                case GraphicsContext3D.UNSIGNED_SHORT:
                    if (this.m_renderingContext.m_webglDepthTexture != null)
                    {
                        break;
                    }
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid texture type");
                    return false;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid texture type");
                    return false;
            }

            // Verify that the combination of format and type is supported.
            switch (format)
            {
                case GraphicsContext3D.ALPHA:
                case GraphicsContext3D.LUMINANCE:
                case GraphicsContext3D.LUMINANCE_ALPHA:
                    if (type != GraphicsContext3D.UNSIGNED_BYTE
                        && type != GraphicsContext3D.FLOAT
                        && type != GraphicsContext3D.HALF_FLOAT_OES)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "invalid type for format");
                        return false;
                    }
                    break;
                case GraphicsContext3D.RGB:
                    if (type != GraphicsContext3D.UNSIGNED_BYTE
                        && type != GraphicsContext3D.UNSIGNED_SHORT_5_6_5
                        && type != GraphicsContext3D.FLOAT
                        && type != GraphicsContext3D.HALF_FLOAT_OES)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "invalid type for RGB format");
                        return false;
                    }
                    break;
                case GraphicsContext3D.RGBA:
                    if (type != GraphicsContext3D.UNSIGNED_BYTE
                        && type != GraphicsContext3D.UNSIGNED_SHORT_4_4_4_4
                        && type != GraphicsContext3D.UNSIGNED_SHORT_5_5_5_1
                        && type != GraphicsContext3D.FLOAT
                        && type != GraphicsContext3D.HALF_FLOAT_OES)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "invalid type for RGBA format");
                        return false;
                    }
                    break;
                case GraphicsContext3D.DEPTH_COMPONENT:
                    if (this.m_renderingContext.m_webglDepthTexture == null)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid format. DEPTH_COMPONENT not enabled");
                        return false;
                    }
                    if (type != GraphicsContext3D.UNSIGNED_SHORT
                        && type != GraphicsContext3D.UNSIGNED_INT)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "invalid type for DEPTH_COMPONENT format");
                        return false;
                    }
                    if (level > 0)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "level must be 0 for DEPTH_COMPONENT format");
                        return false;
                    }
                    break;
                case GraphicsContext3D.DEPTH_STENCIL:
                    if (this.m_renderingContext.m_webglDepthTexture == null)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid format. DEPTH_STENCIL not enabled");
                        return false;
                    }
                    if (type != GraphicsContext3D.UNSIGNED_INT_24_8)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "invalid type for DEPTH_STENCIL format");
                        return false;
                    }
                    if (level > 0)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "level must be 0 for DEPTH_STENCIL format");
                        return false;
                    }
                    break;
                default:
                    throw new ApplicationException("should never reach here");
            }

            return true;
        }

        public bool validateSettableTexFormat(DOMString functionName, GLenum format)
        {
            if ((DataFormat.getClearBitsByFormat(format) & (GraphicsContext3D.DEPTH_BUFFER_BIT | GraphicsContext3D.STENCIL_BUFFER_BIT)) != 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "format can not be set, only rendered to");
                return false;
            }
            return true;
        }

        public bool validateTexFuncData(DOMString functionName, GLint level, GLsizei width, GLsizei height, GLenum format, GLenum type, ArrayBufferView pixels, NullDisposition disposition)
        {
            if (pixels == null)
            {
                if (disposition == NullDisposition.NullAllowed)
                {
                    return true;
                }
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "no pixels");
                return false;
            }

            if (!this.validateTexFuncFormatAndType(functionName, format, type, level))
            {
                return false;
            }
            if (!this.validateSettableTexFormat(functionName, format))
            {
                return false;
            }

            switch (type)
            {
                case GraphicsContext3D.UNSIGNED_BYTE:
                    if (!(pixels is Uint8Array))
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "type UNSIGNED_BYTE but ArrayBufferView not Uint8Array");
                        return false;
                    }
                    break;
                case GraphicsContext3D.UNSIGNED_SHORT_5_6_5:
                case GraphicsContext3D.UNSIGNED_SHORT_4_4_4_4:
                case GraphicsContext3D.UNSIGNED_SHORT_5_5_5_1:
                    if (!(pixels is Uint16Array))
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "type UNSIGNED_SHORT but ArrayBufferView not Uint16Array");
                        return false;
                    }
                    break;
                case GraphicsContext3D.FLOAT: // OES_texture_float
                    if (!(pixels is Float32Array))
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "type FLOAT but ArrayBufferView not Float32Array");
                        return false;
                    }
                    break;
                case GraphicsContext3D.HALF_FLOAT_OES: // OES_texture_half_float
                    // As per the specification, ArrayBufferView should be null when
                    // OES_texture_half_float is enabled.
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "type HALF_FLOAT_OES but ArrayBufferView is not NULL");
                    return false;
                default:
                    throw new ApplicationException("should not reach here!");
            }

            int totalBytesRequired;
            int padding;
            var error = GraphicsContext3D.computeImageSizeInBytes(format, type, width, height, this.m_renderingContext.m_unpackAlignment, out totalBytesRequired, out padding);
            if (error != GraphicsContext3D.NO_ERROR)
            {
                this.m_renderingContext.synthesizeGLError(error, functionName, "invalid texture dimensions");
                return false;
            }
            if (pixels.byteLength < totalBytesRequired)
            {
                if (this.m_renderingContext.m_unpackAlignment != 1)
                {
                    GraphicsContext3D.computeImageSizeInBytes(format, type, width, height, 1, out totalBytesRequired, out padding);
                    if (pixels.byteLength == totalBytesRequired)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "ArrayBufferView not big enough for request with UNPACK_ALIGNMENT > 1");
                        return false;
                    }
                }
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "ArrayBufferView not big enough for request");
                return false;
            }
            return true;
        }

        public bool validateDrawArrays(DOMString functionName, GLenum mode, GLint first, GLsizei count, GLsizei primcount)
        {
            if (this.m_renderingContext.isContextLostOrPending() || !this.validateDrawMode(functionName, mode))
            {
                return false;
            }

            if (!this.validateStencilSettings(functionName))
            {
                return false;
            }

            if (first < 0 || count < 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "first or count < 0");
                return false;
            }

            if (count == 0)
            {
                this.m_renderingContext.markContextChanged();
                return false;
            }

            if (primcount < 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "primcount < 0");
                return false;
            }

            if (!this.m_renderingContext.isErrorGeneratedOnOutOfBoundsAccesses())
            {
                // Ensure we have a valid rendering state
                var checkedFirst = first;
                var checkedCount = count;
                var checkedSum = checkedFirst + checkedCount;
                var checkedPrimCount = primcount;
                if (!this.validateVertexAttributes((uint)checkedSum, (uint)checkedPrimCount))
                {
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "attempt to access out of bounds arrays");
                    return false;
                }
            }
            else
            {
                if (!this.validateVertexAttributes(0))
                {
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "attribs not setup correctly");
                    return false;
                }
            }

            var reason = "framebuffer incomplete";
            if (this.m_renderingContext.m_framebufferBinding != null &&
                !this.m_renderingContext.m_framebufferBinding.onAccess(this.m_renderingContext.graphicsContext3D(), !this.m_renderingContext.isResourceSafe(), ref reason))
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_FRAMEBUFFER_OPERATION, functionName, reason);
                return false;
            }

            return true;
        }

        public bool validateCharacter(char c)
        {
            // Printing characters are valid except " $ ` @ \ ' DEL.
            if (c >= 32 && c <= 126 && c != '"' && c != '$' && c != '`' && c != '@' && c != '\\' && c != '\'')
            {
                return true;
            }
            // Horizontal tab, line feed, vertical tab, form feed, carriage return
            // are also valid.
            if (c >= 9 && c <= 13)
            {
                return true;
            }
            return false;
        }

        public bool validateCompressedTexSubDimensions(DOMString functionName, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, WebGLTexture tex)
        {
            if (xoffset < 0 || yoffset < 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "xoffset or yoffset < 0");
                return false;
            }

            switch (format)
            {
                case Extensions3D.COMPRESSED_RGB_S3TC_DXT1_EXT:
                case Extensions3D.COMPRESSED_RGBA_S3TC_DXT1_EXT:
                case Extensions3D.COMPRESSED_RGBA_S3TC_DXT3_EXT:
                case Extensions3D.COMPRESSED_RGBA_S3TC_DXT5_EXT:
                {
                    const int kBlockWidth = 4;
                    const int kBlockHeight = 4;
                    if ((xoffset % kBlockWidth) != 0 || (yoffset % kBlockHeight) != 0)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "xoffset or yoffset not multiple of 4");
                        return false;
                    }
                    if (width - xoffset > tex.getWidth(target, level) || height - yoffset > tex.getHeight(target, level))
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "dimensions out of range");
                        return false;
                    }
                    return this.validateCompressedTexDimensions(functionName, target, level, width, height, format);
                }
                default:
                    return false;
            }
        }

        public bool validateCompressedTexFuncData(DOMString functionName, GLsizei width, GLsizei height, GLenum format, ArrayBufferView pixels)
        {
            if (pixels == null)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "no pixels");
                return false;
            }
            if (width < 0 || height < 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "width or height < 0");
                return false;
            }

            uint bytesRequired;

            switch (format)
            {
                case Extensions3D.COMPRESSED_RGB_S3TC_DXT1_EXT:
                case Extensions3D.COMPRESSED_RGBA_S3TC_DXT1_EXT:
                case Extensions3D.COMPRESSED_ATC_RGB_AMD:
                {
                    const int kBlockSize = 8;
                    const int kBlockWidth = 4;
                    const int kBlockHeight = 4;
                    var numBlocksAcross = (width + kBlockWidth - 1) / kBlockWidth;
                    var numBlocksDown = (height + kBlockHeight - 1) / kBlockHeight;
                    bytesRequired = (uint)(numBlocksAcross * numBlocksDown * kBlockSize);
                }
                    break;
                case Extensions3D.COMPRESSED_RGBA_S3TC_DXT3_EXT:
                case Extensions3D.COMPRESSED_RGBA_S3TC_DXT5_EXT:
                case Extensions3D.COMPRESSED_ATC_RGBA_EXPLICIT_ALPHA_AMD:
                case Extensions3D.COMPRESSED_ATC_RGBA_INTERPOLATED_ALPHA_AMD:
                {
                    const int kBlockSize = 16;
                    const int kBlockWidth = 4;
                    const int kBlockHeight = 4;
                    var numBlocksAcross = (width + kBlockWidth - 1) / kBlockWidth;
                    var numBlocksDown = (height + kBlockHeight - 1) / kBlockHeight;
                    bytesRequired = (uint)(numBlocksAcross * numBlocksDown * kBlockSize);
                }
                    break;
                case Extensions3D.COMPRESSED_RGB_PVRTC_4BPPV1_IMG:
                case Extensions3D.COMPRESSED_RGBA_PVRTC_4BPPV1_IMG:
                {
                    const int kBlockSize = 8;
                    const int kBlockWidth = 8;
                    const int kBlockHeight = 8;
                    bytesRequired = (uint)((Math.Max(width, (double)kBlockWidth) * Math.Max(height, (double)kBlockHeight) * 4 + 7) / kBlockSize);
                }
                    break;
                case Extensions3D.COMPRESSED_RGB_PVRTC_2BPPV1_IMG:
                case Extensions3D.COMPRESSED_RGBA_PVRTC_2BPPV1_IMG:
                {
                    const int kBlockSize = 8;
                    const int kBlockWidth = 16;
                    const int kBlockHeight = 8;
                    bytesRequired = (uint)((Math.Max(width, (double)kBlockWidth) * Math.Max(height, (double)kBlockHeight) * 2 + 7) / kBlockSize);
                }
                    break;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid format");
                    return false;
            }

            if (pixels.byteLength != bytesRequired)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "length of ArrayBufferView is not correct for dimensions");
                return false;
            }

            return true;
        }

        public bool validateElementArraySize(GLsizei count, GLenum type, GLintptr offset)
        {
            var elementArrayBuffer = this.m_renderingContext.m_boundVertexArrayObject.getElementArrayBuffer();

            if (elementArrayBuffer == null)
            {
                return false;
            }

            if (offset < 0)
            {
                return false;
            }

            if (type == GraphicsContext3D.UNSIGNED_INT)
            {
                // For an unsigned int array, offset must be divisible by 4 for alignment reasons.
                if (offset % 4 != 0)
                {
                    return false;
                }

                // Make uoffset an element offset.
                offset /= 4;

                var n = elementArrayBuffer.byteLength() / 4;
                if (offset > n || count > n - offset)
                {
                    return false;
                }
            }
            else if (type == GraphicsContext3D.UNSIGNED_SHORT)
            {
                // For an unsigned short array, offset must be divisible by 2 for alignment reasons.
                if (offset % 2 != 0)
                {
                    return false;
                }

                // Make uoffset an element offset.
                offset /= 2;

                var n = elementArrayBuffer.byteLength() / 2;
                if (offset > n || count > n - offset)
                {
                    return false;
                }
            }
            else if (type == GraphicsContext3D.UNSIGNED_BYTE)
            {
                var n = elementArrayBuffer.byteLength();
                if (offset > n || count > n - offset)
                {
                    return false;
                }
            }
            return true;
        }

        public bool validateIndexArrayPrecise(GLsizei count, GLenum type, GLintptr offset, out uint numElementsRequired)
        {
            uint lastIndex = 0;

            var elementArrayBuffer = this.m_renderingContext.m_boundVertexArrayObject.getElementArrayBuffer();

            if (elementArrayBuffer == null)
            {
                numElementsRequired = 0u;
                return false;
            }

            if (count == 0)
            {
                numElementsRequired = 0;
                return true;
            }

            if (elementArrayBuffer.elementArrayBuffer() == null)
            {
                numElementsRequired = 0u;
                return false;
            }

            var uoffset = offset;
            var n = count;

            if (type == GraphicsContext3D.UNSIGNED_INT)
            {
                // keep uoffset byte offset
                var p = new Uint32Array(elementArrayBuffer.elementArrayBuffer(), (int)uoffset, n);
                var pi = 0;
                while (n-- > 0)
                {
                    if (p[pi] > lastIndex)
                    {
                        lastIndex = p[pi];
                    }
                    ++pi;
                }
            }
            else if (type == GraphicsContext3D.UNSIGNED_SHORT)
            {
                // keep uoffset byte offset
                var p = new Uint16Array(elementArrayBuffer.elementArrayBuffer(), (int)uoffset, n);
                var pi = 0;
                while (n-- > 0)
                {
                    if (p[pi] > lastIndex)
                    {
                        lastIndex = p[pi];
                    }
                    ++pi;
                }
            }
            else if (type == GraphicsContext3D.UNSIGNED_BYTE)
            {
                var p = new Uint8Array(elementArrayBuffer.elementArrayBuffer(), (int)uoffset, n);
                var pi = 0;
                while (n-- > 0)
                {
                    if (p[pi] > lastIndex)
                    {
                        lastIndex = p[pi];
                    }
                    ++pi;
                }
            }

            // Then set the last index in the index array and make sure it is valid.
            numElementsRequired = lastIndex + 1;
            return numElementsRequired > 0;
        }

        public bool validateDrawElements(DOMString functionName, GLenum mode, GLsizei count, GLenum type, long offset, out uint numElements)
        {
            numElements = 0u;

            if (this.m_renderingContext.isContextLostOrPending() || !this.validateDrawMode(functionName, mode))
            {
                return false;
            }

            if (!this.validateStencilSettings(functionName))
            {
                return false;
            }

            switch (type)
            {
                case GraphicsContext3D.UNSIGNED_BYTE:
                case GraphicsContext3D.UNSIGNED_SHORT:
                    break;
                case GraphicsContext3D.UNSIGNED_INT:
                    if (this.m_renderingContext.m_oesElementIndexUint != null)
                    {
                        break;
                    }
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid type");
                    return false;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid type");
                    return false;
            }

            if (count < 0 || offset < 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "count or offset < 0");
                return false;
            }

            if (count == 0)
            {
                this.m_renderingContext.markContextChanged();
                return false;
            }

            if (this.m_renderingContext.m_boundVertexArrayObject.getElementArrayBuffer() == null)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "no ELEMENT_ARRAY_BUFFER bound");
                return false;
            }

            if (!this.m_renderingContext.isErrorGeneratedOnOutOfBoundsAccesses())
            {
                // Ensure we have a valid rendering state
                if (!this.validateElementArraySize(count, type, (int)offset))
                {
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "request out of bounds for current ELEMENT_ARRAY_BUFFER");
                    return false;
                }
                if (count == 0)
                {
                    return false;
                }
                if (!this.validateIndexArrayConservative(type, out numElements) || !this.validateVertexAttributes(numElements))
                {
                    if (!this.validateIndexArrayPrecise(count, type, (int)offset, out numElements) || !this.validateVertexAttributes(numElements))
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "attempt to access out of bounds arrays");
                        return false;
                    }
                }
            }
            else
            {
                if (!this.validateVertexAttributes(0))
                {
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "attribs not setup correctly");
                    return false;
                }
            }

            var reason = "framebuffer incomplete";
            if (this.m_renderingContext.m_framebufferBinding != null && !this.m_renderingContext.m_framebufferBinding.onAccess(this.m_renderingContext.graphicsContext3D(), !this.m_renderingContext.isResourceSafe(), ref reason))
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_FRAMEBUFFER_OPERATION, functionName, reason);
                return false;
            }

            return true;
        }

        public bool validateVertexAttributes(uint elementCount, uint primitiveCount = 0u)
        {
            if (this.m_renderingContext.m_currentProgram == null)
            {
                return false;
            }

            // Look in each enabled vertex attrib and check if they've been bound to a buffer.
            for (var i = 0; i < this.m_renderingContext.m_maxVertexAttribs; ++i)
            {
                if (!this.m_renderingContext.m_boundVertexArrayObject.getVertexAttribState(i).validateBinding())
                {
                    return false;
                }
            }

            if (elementCount <= 0)
            {
                return true;
            }

            // Look in each consumed vertex attrib (by the current program).
            var sawNonInstancedAttrib = false;
            var sawEnabledAttrib = false;
            var numActiveAttribLocations = (int)this.m_renderingContext.m_currentProgram.numActiveAttribLocations();
            for (var i = 0; i < numActiveAttribLocations; ++i)
            {
                var loc = this.m_renderingContext.m_currentProgram.getActiveAttribLocation((uint)i);
                if (loc >= 0 && loc < this.m_renderingContext.m_maxVertexAttribs)
                {
                    var state = this.m_renderingContext.m_boundVertexArrayObject.getVertexAttribState(loc);
                    if (state.enabled)
                    {
                        sawEnabledAttrib = true;
                        // Avoid off-by-one errors in numElements computation.
                        // For the last element, we will only touch the data for the
                        // element and nothing beyond it.
                        var bytesRemaining = (int)(state.bufferBinding.byteLength() - state.offset);
                        uint numElements = 0;
                        if (bytesRemaining >= state.bytesPerElement)
                        {
                            numElements = (uint)(1 + (bytesRemaining - state.bytesPerElement) / (float)state.stride);
                        }
                        if (state.divisor == 0)
                        {
                            sawNonInstancedAttrib = true;
                        }
                        if ((state.divisor == 0 && elementCount > numElements) || (state.divisor != 0 && primitiveCount > numElements))
                        {
                            return false;
                        }
                    }
                }
            }

            if (!sawNonInstancedAttrib && sawEnabledAttrib)
            {
                return false;
            }

            return true;
        }

        public bool validateIndexArrayConservative(GLenum type, out uint numElementsRequired)
        {
            var elementArrayBuffer = this.m_renderingContext.m_boundVertexArrayObject.getElementArrayBuffer();
            numElementsRequired = 0u;

            if (elementArrayBuffer == null)
            {
                return false;
            }

            var numElements = elementArrayBuffer.byteLength();
            // The case count==0 is already dealt with in drawElements before validateIndexArrayConservative.
            if (numElements == 0)
            {
                return false;
            }
            var buffer = elementArrayBuffer.elementArrayBuffer();

            var maxIndex = elementArrayBuffer.getCachedMaxIndex(type);
            if (maxIndex < 0)
            {
                // Compute the maximum index in the entire buffer for the given type of index.
                switch (type)
                {
                    case GraphicsContext3D.UNSIGNED_BYTE:
                    {
                        var p = new Uint8Array(buffer);
                        for (var i = 0; i < numElements; i++)
                        {
                            maxIndex = Math.Max(maxIndex, p[i]);
                        }
                        break;
                    }
                    case GraphicsContext3D.UNSIGNED_SHORT:
                    {
                        numElements /= sizeof(GLushort);
                        var p = new Uint16Array(buffer);
                        for (var i = 0; i < numElements; i++)
                        {
                            maxIndex = Math.Max(maxIndex, p[i]);
                        }
                        break;
                    }
                    case GraphicsContext3D.UNSIGNED_INT:
                    {
                        if (this.m_renderingContext.m_oesElementIndexUint == null)
                        {
                            return false;
                        }
                        numElements /= sizeof(GLuint);
                        var p = new Uint32Array(buffer);
                        for (var i = 0; i < numElements; i++)
                        {
                            maxIndex = Math.Max(maxIndex, p[i]);
                        }
                        break;
                    }
                    default:
                        return false;
                }
                elementArrayBuffer.setCachedMaxIndex(type, maxIndex);
            }

            if (maxIndex >= 0)
            {
                // The number of required elements is one more than the maximum
                // index that will be accessed.
                numElementsRequired = (uint)(maxIndex + 1);
                return true;
            }

            return false;
        }

        public bool validateFramebufferFuncParameters(DOMString functionName, GLenum target, GLenum attachment)
        {
            if (target != GraphicsContext3D.FRAMEBUFFER)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid target");
                return false;
            }
            switch (attachment)
            {
                case GraphicsContext3D.COLOR_ATTACHMENT0:
                case GraphicsContext3D.DEPTH_ATTACHMENT:
                case GraphicsContext3D.STENCIL_ATTACHMENT:
                case GraphicsContext3D.DEPTH_STENCIL_ATTACHMENT:
                    break;
                default:
                    if (this.m_renderingContext.m_webglDrawBuffers != null
                        && attachment > GraphicsContext3D.COLOR_ATTACHMENT0
                        && attachment < GraphicsContext3D.COLOR_ATTACHMENT0 + this.m_renderingContext.getMaxColorAttachments())
                    {
                        break;
                    }
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid attachment");
                    return false;
            }
            return true;
        }

        public bool validateUniformParameters(DOMString functionName, WebGLUniformLocation location, Float32Array v, GLsizei requiredMinSize)
        {
            if (v == null)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "no array");
                return false;
            }
            var result = this.validateUniformMatrixParameters(functionName, location, false, v.buffer.@lock(), v.length, requiredMinSize);
            v.buffer.unlock();
            return result;
        }

        public bool validateUniformParameters(DOMString functionName, WebGLUniformLocation location, Int32Array v, GLsizei requiredMinSize)
        {
            if (v == null)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "no array");
                return false;
            }
            var result = this.validateUniformMatrixParameters(functionName, location, false, v.buffer.@lock(), v.length, requiredMinSize);
            v.buffer.unlock();
            return result;
        }

        public bool validateUniformParameters(DOMString functionName, WebGLUniformLocation location, IntPtr v, GLsizei size, GLsizei requiredMinSize)
        {
            return this.validateUniformMatrixParameters(functionName, location, false, v, size, requiredMinSize);
        }

        public bool validateUniformMatrixParameters(String functionName, WebGLUniformLocation location, GLboolean transpose, Float32Array v, GLsizei requiredMinSize)
        {
            if (v == null)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "no array");
                return false;
            }
            var result = this.validateUniformMatrixParameters(functionName, location, transpose, v.buffer.@lock(), v.length, requiredMinSize);
            v.buffer.unlock();
            return result;
        }

        public bool validateUniformMatrixParameters(String functionName, WebGLUniformLocation location, GLboolean transpose, IntPtr v, GLsizei size, GLsizei requiredMinSize)
        {
            if (location == null)
            {
                return false;
            }
            if (location.program() != this.m_renderingContext.m_currentProgram)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "location is not from current program");
                return false;
            }
            if (v == IntPtr.Zero)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "no array");
                return false;
            }
            if (transpose)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "transpose not FALSE");
                return false;
            }
            if (size < requiredMinSize || (size % requiredMinSize) != 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "invalid size");
                return false;
            }
            return true;
        }

        public bool validateLocationLength(String functionName, String @string)
        {
            const uint maxWebGLLocationLength = 256;
            if (@string.Length > maxWebGLLocationLength)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "location length > 256");
                return false;
            }
            return true;
        }

        public bool validateSize(String functionName, GLint x, GLint y)
        {
            if (x < 0 || y < 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "size < 0");
                return false;
            }
            return true;
        }

        public bool validateString(String functionName, IEnumerable<char> @string)
        {
            foreach (var t in @string)
            {
                if (!this.validateCharacter(t))
                {
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "string not ASCII");
                    return false;
                }
            }
            return true;
        }

        public WebGLTexture validateTextureBinding(String functionName, GLenum target, bool useSixEnumsForCubeMap)
        {
            WebGLTexture texture;
            switch (target)
            {
                case GraphicsContext3D.TEXTURE_2D:
                    texture = this.m_renderingContext.m_textureUnits[this.m_renderingContext.m_activeTextureUnit].TextureBinding;
                    break;
                case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_X:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_X:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Y:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Y:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Z:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Z:
                    if (!useSixEnumsForCubeMap)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid texture target");
                        return null;
                    }
                    texture = this.m_renderingContext.m_textureUnits[this.m_renderingContext.m_activeTextureUnit].textureCubeMapBinding;
                    break;
                case GraphicsContext3D.TEXTURE_CUBE_MAP:
                    if (useSixEnumsForCubeMap)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid texture target");
                        return null;
                    }
                    texture = this.m_renderingContext.m_textureUnits[this.m_renderingContext.m_activeTextureUnit].textureCubeMapBinding;
                    break;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid texture target");
                    return null;
            }
            if (texture == null)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "no texture");
            }
            return texture;
        }

        public bool validateTexFunc(String functionName, TexFuncValidationFunctionType functionType, TexFuncValidationSourceType sourceType,
                                    GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border,
                                    GLenum format, GLenum type, GLint xoffset, GLint yoffset)
        {
            if (!this.validateTexFuncParameters(functionName, functionType, target, level, internalformat, width, height, border, format, type))
            {
                return false;
            }

            var texture = this.validateTextureBinding(functionName, target, true);
            if (texture == null)
            {
                return false;
            }

            if (functionType == TexFuncValidationFunctionType.NotTexSubImage2D)
            {
                if (level != 0 && WebGLTexture.isNPOT(width, height))
                {
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "level > 0 not power of 2");
                    return false;
                }
                // For SourceArrayBufferView, function validateTexFuncData() would handle whether to validate the SettableTexFormat
                // by checking if the ArrayBufferView is null or not.
                if (sourceType != TexFuncValidationSourceType.SourceArrayBufferView)
                {
                    if (!this.validateSettableTexFormat(functionName, format))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (!this.validateSettableTexFormat(functionName, format))
                {
                    return false;
                }
                if (!this.validateSize(functionName, xoffset, yoffset))
                {
                    return false;
                }
                // Before checking if it is in the range, check if overflow happens first.
                if (xoffset + width < 0 || yoffset + height < 0)
                {
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "bad dimensions");
                    return false;
                }
                if (xoffset + width > texture.getWidth(target, level) || yoffset + height > texture.getHeight(target, level))
                {
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "dimensions out of range");
                    return false;
                }
                if (texture.getInternalFormat(target, level) != format || texture.getType(target, level) != type)
                {
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "type and format do not match texture");
                    return false;
                }
            }

            return true;
        }

        public bool validateTexFuncParameters(String functionName, TexFuncValidationFunctionType functionType, GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type)
        {
            // We absolutely have to validate the format and type combination.
            // The texImage2D entry points taking HTMLImage, etc. will produce
            // temporary data based on this combination, so it must be legal.
            if (!this.validateTexFuncFormatAndType(functionName, format, type, level) || !this.validateTexFuncLevel(functionName, target, level))
            {
                return false;
            }

            if (width < 0 || height < 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "width or height < 0");
                return false;
            }

            var maxTextureSizeForLevel = (int)Math.Pow(2.0, this.m_renderingContext.m_maxTextureLevel - 1 - level);
            switch (target)
            {
                case GraphicsContext3D.TEXTURE_2D:
                    if (width > maxTextureSizeForLevel || height > maxTextureSizeForLevel)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "width or height out of range");
                        return false;
                    }
                    break;
                case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_X:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_X:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Y:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Y:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Z:
                case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Z:
                    if (functionType != TexFuncValidationFunctionType.TexSubImage2D && width != height)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "width != height for cube map");
                        return false;
                    }
                    // No need to check height here. For texImage width == height.
                    // For texSubImage that will be checked when checking yoffset + height is in range.
                    if (width > maxTextureSizeForLevel)
                    {
                        this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "width or height out of range for cube map");
                        return false;
                    }
                    break;
                default:
                    this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_ENUM, functionName, "invalid target");
                    return false;
            }

            if (format != internalformat)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "format != internalformat");
                return false;
            }

            if (border != 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "border != 0");
                return false;
            }

            return true;
        }

        internal bool validateWebGLObject(String functionName, WebGLObject @object)
        {
            if (@object == null || @object.obj() == 0)
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_VALUE, functionName, "no object or object deleted");
                return false;
            }
            if (!@object.validate(this.m_renderingContext.contextGroup(), this.m_renderingContext))
            {
                this.m_renderingContext.synthesizeGLError(GraphicsContext3D.INVALID_OPERATION, functionName, "object does not belong to this context");
                return false;
            }
            return true;
        }
    }

    // ReSharper restore InconsistentNaming
}
