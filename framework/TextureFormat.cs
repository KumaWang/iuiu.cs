using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace engine.framework
{
    public enum TextureFormat
    {
        Alpha8 = 1,
        ARGB4444 = 2,
        RGB24 = 3,
        RGBA32 = 4,
        ARGB32 = 5,
        RGB565 = 7,
        DXT1 = 10,
        DXT5 = 12,
        RGBA4444 = 13,
        BGRA32 = 14,
        PVRTC_RGB2 = 30,
        [Obsolete("Use PVRTC_RGB2")]
        PVRTC_2BPP_RGB = 30,
        PVRTC_RGBA2 = 31,
        [Obsolete("Use PVRTC_RGBA2")]
        PVRTC_2BPP_RGBA = 31,
        PVRTC_RGB4 = 32,
        [Obsolete("Use PVRTC_RGB4")]
        PVRTC_4BPP_RGB = 32,
        [Obsolete("Use PVRTC_RGBA4")]
        PVRTC_4BPP_RGBA = 33,
        PVRTC_RGBA4 = 33,
        ETC_RGB4 = 34,
        ATC_RGB4 = 35,
        ATC_RGBA8 = 36,
        ATF_RGB_DXT1 = 38,
        ATF_RGBA_JPG = 39,
        ATF_RGB_JPG = 40,
        EAC_R = 41,
        EAC_R_SIGNED = 42,
        EAC_RG = 43,
        EAC_RG_SIGNED = 44,
        ETC2_RGB = 45,
        ETC2_RGBA1 = 46,
        ETC2_RGBA8 = 47,
        ASTC_RGB_4x4 = 48,
        ASTC_RGB_5x5 = 49,
        ASTC_RGB_6x6 = 50,
        ASTC_RGB_8x8 = 51,
        ASTC_RGB_10x10 = 52,
        ASTC_RGB_12x12 = 53,
        ASTC_RGBA_4x4 = 54,
        ASTC_RGBA_5x5 = 55,
        ASTC_RGBA_6x6 = 56,
        ASTC_RGBA_8x8 = 57,
        ASTC_RGBA_10x10 = 58,
        ASTC_RGBA_12x12 = 59,
    }
}
