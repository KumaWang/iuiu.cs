using System;
using GLenum = System.UInt32;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    static class DataFormat
    {
        public const int RGBA8 = 0;
        public const int RGBA16Little = 1;
        public const int RGBA16Big = 2;
        public const int RGBA16F = 3;
        public const int RGBA32F = 4;
        public const int RGB8 = 5;
        public const int RGB16Little = 6;
        public const int RGB16Big = 7;
        public const int RGB16F = 8;
        public const int RGB32F = 9;
        public const int BGR8 = 10;
        public const int BGRA8 = 11;
        public const int BGRA16Little = 12;
        public const int BGRA16Big = 13;
        public const int ARGB8 = 14;
        public const int ARGB16Little = 15;
        public const int ARGB16Big = 16;
        public const int ABGR8 = 17;
        public const int RGBA5551 = 18;
        public const int RGBA4444 = 19;
        public const int RGB565 = 20;
        public const int R8 = 21;
        public const int R16Little = 22;
        public const int R16Big = 23;
        public const int R16F = 24;
        public const int R32F = 25;
        public const int RA8 = 26;
        public const int RA16Little = 27;
        public const int RA16Big = 28;
        public const int RA16F = 29;
        public const int RA32F = 30;
        public const int AR8 = 31;
        public const int AR16Little = 32;
        public const int AR16Big = 33;
        public const int A8 = 34;
        public const int A16Little = 35;
        public const int A16Big = 36;
        public const int A16F = 37;
        public const int A32F = 38;
        public const int NumFormats = 39;

        public static bool computeFormatAndTypeParameters(GLenum format, GLenum type, out int componentsPerPixel, out int bytesPerComponent)
        {
            switch (format)
            {
                case GLES.GL_ALPHA:
                case GLES.GL_LUMINANCE:
                case GLES.GL_DEPTH_COMPONENT:
                case GLES.GL_DEPTH_STENCIL:
                    componentsPerPixel = 1;
                    break;
                case GLES.GL_LUMINANCE_ALPHA:
                    componentsPerPixel = 2;
                    break;
                case GLES.GL_RGB:
                    componentsPerPixel = 3;
                    break;
                case GLES.GL_RGBA:
                case Extensions3D.BGRA_EXT: // GL_EXT_texture_format_BGRA8888
                    componentsPerPixel = 4;
                    break;
                default:
                    componentsPerPixel = 0;
                    bytesPerComponent = 0;
                    return false;
            }
            switch (type)
            {
                case GLES.GL_UNSIGNED_BYTE:
                    bytesPerComponent = sizeof(Byte);
                    break;
                case GLES.GL_UNSIGNED_SHORT:
                    bytesPerComponent = sizeof(UInt16);
                    break;
                case GLES.GL_UNSIGNED_SHORT_5_6_5:
                case GLES.GL_UNSIGNED_SHORT_4_4_4_4:
                case GLES.GL_UNSIGNED_SHORT_5_5_5_1:
                    componentsPerPixel = 1;
                    bytesPerComponent = sizeof(UInt16);
                    break;
                case GLES.GL_UNSIGNED_INT_24_8:
                case GLES.GL_UNSIGNED_INT:
                    bytesPerComponent = sizeof(UInt32);
                    break;
                case GLES.GL_FLOAT: // OES_texture_float
                    bytesPerComponent = sizeof(Single);
                    break;
                default:
                    componentsPerPixel = 0;
                    bytesPerComponent = 0;

                    return false;
            }
            return true;
        }

        public static uint getClearBitsByAttachmentType(GLenum attachment)
        {
            switch (attachment)
            {
                case GLES.GL_COLOR_ATTACHMENT0:
                    return GLES.GL_COLOR_BUFFER_BIT;
                case GLES.GL_DEPTH_ATTACHMENT:
                    return GLES.GL_DEPTH_BUFFER_BIT;
                case GLES.GL_STENCIL_ATTACHMENT:
                    return GLES.GL_STENCIL_BUFFER_BIT;
                case GLES.GL_DEPTH_STENCIL_ATTACHMENT:
                    return GLES.GL_DEPTH_BUFFER_BIT | GLES.GL_STENCIL_BUFFER_BIT;
                default:
                    return 0;
            }
        }

        public static uint getClearBitsByFormat(GLenum format)
        {
            switch (format)
            {
                case GLES.GL_ALPHA:
                case GLES.GL_LUMINANCE:
                case GLES.GL_LUMINANCE_ALPHA:
                case GLES.GL_RGB:
                case GLES.GL_RGB565:
                case GLES.GL_RGBA:
                case GLES.GL_RGBA4:
                case GLES.GL_RGB5_A1:
                    return GLES.GL_COLOR_BUFFER_BIT;
                case GLES.GL_DEPTH_COMPONENT16:
                case GLES.GL_DEPTH_COMPONENT:
                    return GLES.GL_DEPTH_BUFFER_BIT;
                case GLES.GL_STENCIL_INDEX8:
                    return GLES.GL_STENCIL_BUFFER_BIT;
                case GLES.GL_DEPTH_STENCIL:
                    return GLES.GL_DEPTH_BUFFER_BIT | GLES.GL_STENCIL_BUFFER_BIT;
                default:
                    return 0;
            }
        }

        public static ChannelBits getChannelBitsByFormat(GLenum format)
        {
            switch (format)
            {
                case GLES.GL_ALPHA:
                    return ChannelBits.ChannelAlpha;
                case GLES.GL_LUMINANCE:
                    return ChannelBits.ChannelRGB;
                case GLES.GL_LUMINANCE_ALPHA:
                    return ChannelBits.ChannelRGBA;
                case GLES.GL_RGB:
                case GLES.GL_RGB565:
                    return ChannelBits.ChannelRGB;
                case GLES.GL_RGBA:
                case GLES.GL_RGBA4:
                case GLES.GL_RGB5_A1:
                    return ChannelBits.ChannelRGBA;
                case GLES.GL_DEPTH_COMPONENT16:
                case GLES.GL_DEPTH_COMPONENT:
                    return ChannelBits.ChannelDepth;
                case GLES.GL_STENCIL_INDEX8:
                    return ChannelBits.ChannelStencil;
                case GLES.GL_DEPTH_STENCIL:
                    return ChannelBits.ChannelDepth | ChannelBits.ChannelStencil;
                default:
                    return 0;
            }
        }

        public static int getDataFormat(GLenum destinationFormat, GLenum destinationType)
        {
            var dstFormat = RGBA8;
            switch (destinationType)
            {
                case GraphicsContext3D.UNSIGNED_BYTE:
                    switch (destinationFormat)
                    {
                        case GraphicsContext3D.RGB:
                            dstFormat = RGB8;
                            break;
                        case GraphicsContext3D.RGBA:
                            dstFormat = RGBA8;
                            break;
                        case GraphicsContext3D.ALPHA:
                            dstFormat = A8;
                            break;
                        case GraphicsContext3D.LUMINANCE:
                            dstFormat = R8;
                            break;
                        case GraphicsContext3D.LUMINANCE_ALPHA:
                            dstFormat = RA8;
                            break;
                    }
                    break;
                case GraphicsContext3D.UNSIGNED_SHORT_4_4_4_4:
                    dstFormat = RGBA4444;
                    break;
                case GraphicsContext3D.UNSIGNED_SHORT_5_5_5_1:
                    dstFormat = RGBA5551;
                    break;
                case GraphicsContext3D.UNSIGNED_SHORT_5_6_5:
                    dstFormat = RGB565;
                    break;
                case GraphicsContext3D.HALF_FLOAT_OES: // OES_texture_half_float
                    switch (destinationFormat)
                    {
                        case GraphicsContext3D.RGB:
                            dstFormat = RGB16F;
                            break;
                        case GraphicsContext3D.RGBA:
                            dstFormat = RGBA16F;
                            break;
                        case GraphicsContext3D.ALPHA:
                            dstFormat = A16F;
                            break;
                        case GraphicsContext3D.LUMINANCE:
                            dstFormat = R16F;
                            break;
                        case GraphicsContext3D.LUMINANCE_ALPHA:
                            dstFormat = RA16F;
                            break;
                    }
                    break;
                case GraphicsContext3D.FLOAT: // OES_texture_float
                    switch (destinationFormat)
                    {
                        case GraphicsContext3D.RGB:
                            dstFormat = RGB32F;
                            break;
                        case GraphicsContext3D.RGBA:
                            dstFormat = RGBA32F;
                            break;
                        case GraphicsContext3D.ALPHA:
                            dstFormat = A32F;
                            break;
                        case GraphicsContext3D.LUMINANCE:
                            dstFormat = R32F;
                            break;
                        case GraphicsContext3D.LUMINANCE_ALPHA:
                            dstFormat = RA32F;
                            break;
                    }
                    break;
            }
            return dstFormat;
        }

        public static uint texelBytesForFormat(int format)
        {
            switch (format)
            {
                case R8:
                case A8:
                    return 1;
                case RA8:
                case AR8:
                case RGBA5551:
                case RGBA4444:
                case RGB565:
                case A16F:
                case R16F:
                    return 2;
                case RGB8:
                case BGR8:
                    return 3;
                case RGBA8:
                case ARGB8:
                case ABGR8:
                case BGRA8:
                case R32F:
                case A32F:
                case RA16F:
                    return 4;
                case RGB16F:
                    return 6;
                case RA32F:
                case RGBA16F:
                    return 8;
                case RGB32F:
                    return 12;
                case RGBA32F:
                    return 16;
                default:
                    return 0;
            }
        }
    }

    // ReSharper restore InconsistentNaming
}
