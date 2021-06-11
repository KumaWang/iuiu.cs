using System;
using System.Collections.Generic;
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

    class WebGLTexture : WebGLSharedObject
    {
        private GLenum m_target;
        private GLenum m_minFilter;
        private GLenum m_magFilter;
        private GLenum m_wrapS;
        private GLenum m_wrapT;
        private bool m_isNPOT;
        private bool m_isComplete;
        private bool m_needToUseBlackTexture;
        private bool m_isCompressed;
        private bool m_isFloatType;
        private bool m_isHalfFloatType;
        private List<List<LevelInfo>> m_info;

        internal WebGLTexture(WebGLRenderingContext ctx) : base(ctx)
        {
            this.m_target = 0;
            this.m_minFilter = GraphicsContext3D.NEAREST_MIPMAP_LINEAR;
            this.m_magFilter = GraphicsContext3D.LINEAR;
            this.m_wrapS = GraphicsContext3D.REPEAT;
            this.m_wrapT = GraphicsContext3D.REPEAT;
            this.m_isNPOT = false;
            this.m_isComplete = false;
            this.m_needToUseBlackTexture = false;
            this.m_isCompressed = false;
            this.m_isFloatType = false;
            this.m_isHalfFloatType = false;

            this.setObject(ctx.graphicsContext3D().createTexture());
        }

        ~WebGLTexture()
        {
            this.deleteObject(null);
        }

        internal void setTarget(GLenum target, GLint maxLevel)
        {
            if (this.obj() == 0)
            {
                return;
            }
            // Target is finalized the first time bindTexture() is called.
            if (this.m_target != 0)
            {
                return;
            }
            switch (target)
            {
                case GraphicsContext3D.TEXTURE_2D:
                    this.m_target = target;
                    this.m_info = new List<List<LevelInfo>>(1);
                    this.m_info.Add(Enumerable.Range(0, maxLevel).Select(x => new LevelInfo()).ToList());
                    break;
                case GraphicsContext3D.TEXTURE_CUBE_MAP:
                    this.m_target = target;
                    this.m_info = new List<List<LevelInfo>>(6);
                    for (var ii = 0; ii < 6; ++ii)
                    {
                        this.m_info.Add(Enumerable.Range(0, maxLevel).Select(x => new LevelInfo()).ToList());
                    }
                    break;
            }
        }

        internal void setParameteri(GLenum pname, GLint param)
        {
            if (this.obj() == 0 || this.m_target == 0)
            {
                return;
            }
            switch (pname)
            {
                case GraphicsContext3D.TEXTURE_MIN_FILTER:
                    switch (param)
                    {
                        case GraphicsContext3D.NEAREST:
                        case GraphicsContext3D.LINEAR:
                        case GraphicsContext3D.NEAREST_MIPMAP_NEAREST:
                        case GraphicsContext3D.LINEAR_MIPMAP_NEAREST:
                        case GraphicsContext3D.NEAREST_MIPMAP_LINEAR:
                        case GraphicsContext3D.LINEAR_MIPMAP_LINEAR:
                            this.m_minFilter = (uint)param;
                            break;
                    }
                    break;
                case GraphicsContext3D.TEXTURE_MAG_FILTER:
                    switch (param)
                    {
                        case GraphicsContext3D.NEAREST:
                        case GraphicsContext3D.LINEAR:
                            this.m_magFilter = (uint)param;
                            break;
                    }
                    break;
                case GraphicsContext3D.TEXTURE_WRAP_S:
                    switch (param)
                    {
                        case GraphicsContext3D.CLAMP_TO_EDGE:
                        case GraphicsContext3D.MIRRORED_REPEAT:
                        case GraphicsContext3D.REPEAT:
                            this.m_wrapS = (uint)param;
                            break;
                    }
                    break;
                case GraphicsContext3D.TEXTURE_WRAP_T:
                    switch (param)
                    {
                        case GraphicsContext3D.CLAMP_TO_EDGE:
                        case GraphicsContext3D.MIRRORED_REPEAT:
                        case GraphicsContext3D.REPEAT:
                            this.m_wrapT = (uint)param;
                            break;
                    }
                    break;
                default:
                    return;
            }
            this.update();
        }

        internal void setParameterf(GLenum pname, GLfloat param)
        {
            if (this.obj() == 0 || this.m_target == 0)
            {
                return;
            }
            var iparam = (GLint)param;
            this.setParameteri(pname, iparam);
        }

        internal GLenum getTarget()
        {
            return this.m_target;
        }

        internal int getMinFilter()
        {
            return (int)this.m_minFilter;
        }

        internal void setLevelInfo(GLenum target, GLint level, GLenum internalFormat, GLsizei width, GLsizei height, GLenum type)
        {
            if (this.obj() == 0 || this.m_target == 0)
            {
                return;
            }
            // We assume level, internalFormat, width, height, and type have all been validated already.
            var index = this.mapTargetToIndex(target);
            if (index < 0)
            {
                return;
            }
            this.m_info[index][level].setInfo(internalFormat, width, height, type);
            this.update();
        }

        internal bool canGenerateMipmaps()
        {
            if (this.isNPOT())
            {
                return false;
            }
            var first = this.m_info[0][0];
            foreach (var t in this.m_info)
            {
                var info = t[0];
                if (!info.valid
                    || info.width != first.width || info.height != first.height
                    || info.internalFormat != first.internalFormat || info.type != first.type)
                {
                    return false;
                }
            }
            return true;
        }

        internal void generateMipmapLevelInfo()
        {
            if (this.obj() == 0 || this.m_target == 0)
            {
                return;
            }
            if (!this.canGenerateMipmaps())
            {
                return;
            }
            if (!this.m_isComplete)
            {
                foreach (var t in this.m_info)
                {
                    var info0 = t[0];
                    var width = info0.width;
                    var height = info0.height;
                    var levelCount = computeLevelCount(width, height);
                    for (var level = 1; level < levelCount; ++level)
                    {
                        width = Math.Max(1, width >> 1);
                        height = Math.Max(1, height >> 1);
                        var info = t[level];
                        info.setInfo(info0.internalFormat, width, height, info0.type);
                    }
                }
                this.m_isComplete = true;
            }
            this.m_needToUseBlackTexture = false;
        }

        internal GLenum getInternalFormat(GLenum target, GLint level)
        {
            var info = this.getLevelInfo(target, level);
            return info == null ? 0 : info.internalFormat;
        }

        internal GLenum getType(GLenum target, GLint level)
        {
            var info = this.getLevelInfo(target, level);
            return info == null ? 0 : info.type;
        }

        internal GLsizei getWidth(GLenum target, GLint level)
        {
            var info = this.getLevelInfo(target, level);
            return info == null ? 0 : info.width;
        }

        internal GLsizei getHeight(GLenum target, GLint level)
        {
            var info = this.getLevelInfo(target, level);
            return info == null ? 0 : info.height;
        }

        internal bool isValid(GLenum target, GLint level)
        {
            var info = this.getLevelInfo(target, level);
            return info != null && info.valid;
        }

        internal static bool isNPOT(GLsizei width, GLsizei height)
        {
            if (width == 0 || height == 0)
            {
                return false;
            }
            if ((width & (width - 1)) != 0 || (height & (height - 1)) != 0)
            {
                return true;
            }
            return false;
        }

        internal bool isNPOT()
        {
            return this.obj() != 0 && this.m_isNPOT;
        }

        internal bool needToUseBlackTexture(TextureExtensionFlag extensions)
        {
            if (this.obj() == 0)
            {
                return false;
            }
            if (this.m_needToUseBlackTexture)
            {
                return true;
            }
            if ((this.m_isFloatType && (extensions & TextureExtensionFlag.TextureExtensionFloatLinearEnabled) == 0)
                || (this.m_isHalfFloatType && (extensions & TextureExtensionFlag.TextureExtensionHalfFloatLinearEnabled) == 0))
            {
                if (this.m_magFilter != GraphicsContext3D.NEAREST || (this.m_minFilter != GraphicsContext3D.NEAREST && this.m_minFilter != GraphicsContext3D.NEAREST_MIPMAP_NEAREST))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool isCompressed()
        {
            return this.obj() != 0 && this.m_isCompressed;
        }

        internal void setCompressed()
        {
            this.m_isCompressed = true;
        }

        internal bool hasEverBeenBound()
        {
            return this.obj() != 0 && this.m_target != 0;
        }

        internal static GLint computeLevelCount(GLsizei width, GLsizei height)
        {
            var n = Math.Max(width, height);
            if (n <= 0)
            {
                return 0;
            }
            var log = 0;
            var value = n;
            for (var ii = 4; ii >= 0; --ii)
            {
                var shift = (1 << ii);
                var x = (value >> shift);
                if (x != 0)
                {
                    value = x;
                    log += shift;
                }
            }

            return log + 1;
        }

        internal override void deleteObjectImpl(GraphicsContext3D context3d, Platform3DObject @object)
        {
            context3d.deleteTexture(@object);
        }

        internal override bool isTexture()
        {
            return true;
        }

        private void update()
        {
            this.m_isNPOT = false;
            foreach (var t in this.m_info)
            {
                if (isNPOT(t[0].width, t[0].height))
                {
                    this.m_isNPOT = true;
                    break;
                }
            }
            this.m_isComplete = true;
            var first = this.m_info[0][0];
            var levelCount = computeLevelCount(first.width, first.height);
            if (levelCount < 1)
            {
                this.m_isComplete = false;
            }
            else
            {
                for (var ii = 0; ii < this.m_info.Count && this.m_isComplete; ++ii)
                {
                    var info0 = this.m_info[ii][0];
                    if (!info0.valid
                        || info0.width != first.width || info0.height != first.height
                        || info0.internalFormat != first.internalFormat || info0.type != first.type)
                    {
                        this.m_isComplete = false;
                        break;
                    }
                    var width = info0.width;
                    var height = info0.height;
                    for (var level = 1; level < levelCount; ++level)
                    {
                        width = Math.Max(1, width >> 1);
                        height = Math.Max(1, height >> 1);
                        var info = this.m_info[ii][level];
                        if (!info.valid
                            || info.width != width || info.height != height
                            || info.internalFormat != info0.internalFormat || info.type != info0.type)
                        {
                            this.m_isComplete = false;
                            break;
                        }
                    }
                }
            }

            this.m_isFloatType = false;
            if (this.m_isComplete)
            {
                this.m_isFloatType = this.m_info[0][0].type == GraphicsContext3D.FLOAT;
            }
            else
            {
                foreach (var t in this.m_info)
                {
                    if (t[0].type == GraphicsContext3D.FLOAT)
                    {
                        this.m_isFloatType = true;
                        break;
                    }
                }
            }

            this.m_isHalfFloatType = false;
            if (this.m_isComplete)
            {
                this.m_isHalfFloatType = this.m_info[0][0].type == GraphicsContext3D.HALF_FLOAT_OES;
            }
            else
            {
                foreach (var t in this.m_info)
                {
                    if (t[0].type == GraphicsContext3D.HALF_FLOAT_OES)
                    {
                        this.m_isHalfFloatType = true;
                        break;
                    }
                }
            }

            this.m_needToUseBlackTexture = false;
            // NPOT
            if (this.m_isNPOT && ((this.m_minFilter != GraphicsContext3D.NEAREST && this.m_minFilter != GraphicsContext3D.LINEAR)
                                  || this.m_wrapS != GraphicsContext3D.CLAMP_TO_EDGE || this.m_wrapT != GraphicsContext3D.CLAMP_TO_EDGE))
            {
                this.m_needToUseBlackTexture = true;
            }
            // Completeness
            if (!this.m_isComplete && this.m_minFilter != GraphicsContext3D.NEAREST && this.m_minFilter != GraphicsContext3D.LINEAR)
            {
                this.m_needToUseBlackTexture = true;
            }
        }

        private int mapTargetToIndex(GLenum target)
        {
            if (this.m_target == GraphicsContext3D.TEXTURE_2D)
            {
                if (target == GraphicsContext3D.TEXTURE_2D)
                {
                    return 0;
                }
            }
            else if (this.m_target == GraphicsContext3D.TEXTURE_CUBE_MAP)
            {
                switch (target)
                {
                    case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_X:
                        return 0;
                    case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_X:
                        return 1;
                    case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Y:
                        return 2;
                    case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Y:
                        return 3;
                    case GraphicsContext3D.TEXTURE_CUBE_MAP_POSITIVE_Z:
                        return 4;
                    case GraphicsContext3D.TEXTURE_CUBE_MAP_NEGATIVE_Z:
                        return 5;
                }
            }
            return -1;
        }

        private LevelInfo getLevelInfo(GLenum target, GLint level)
        {
            if (this.obj() == 0 || this.m_target == 0)
            {
                return null;
            }
            var targetIndex = this.mapTargetToIndex(target);
            if (targetIndex < 0 || targetIndex >= this.m_info.Count)
            {
                return null;
            }
            if (level < 0 || level >= this.m_info[targetIndex].Count)
            {
                return null;
            }
            return this.m_info[targetIndex][level];
        }

        [Flags]
        internal enum TextureExtensionFlag
        {
            TextureExtensionsDisabled = 0,
            TextureExtensionFloatLinearEnabled = 1,
            TextureExtensionHalfFloatLinearEnabled = 2
        }

        private class LevelInfo
        {
            internal bool valid;
            internal GLenum internalFormat;
            internal GLsizei width;
            internal GLsizei height;
            internal GLenum type;

            internal LevelInfo()
            {
                this.valid = false;
                this.internalFormat = 0;
                this.width = 0;
                this.height = 0;
                this.type = 0;
            }

            internal void setInfo(GLenum internalFmt, GLsizei w, GLsizei h, GLenum tp)
            {
                this.valid = true;
                this.internalFormat = internalFmt;
                this.width = w;
                this.height = h;
                this.type = tp;
            }
        }
    }

    // ReSharper restore InconsistentNaming
}
