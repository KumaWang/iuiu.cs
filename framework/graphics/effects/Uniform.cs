using System.Numerics;
using WebGL;

namespace engine.framework.graphics
{
    public enum UniformType 
    {
        Integer,
        Float,
        Sampler1D,
        Sampler2D,
        Sampler3D,
        SamplerCube,
        bVec2,
        bVec3,
        bVec4,
        iVec2,
        iVec3,
        iVec4,
        Vec2,
        Vec3,
        Vec4,
        Mat3,
        Mat4,
        Mat2,
        Boolean
    }

    public abstract class Uniform
    {
        public string Name { get; internal set; }

        public abstract UniformType Type { get; }

        internal abstract void Set(WebGLUniformLocation location, Effect shader);
    }

    public class UniformBoolean : Uniform
    {
        public override UniformType Type => UniformType.Boolean;

        private bool mValue;

        public UniformBoolean()
            : this(false) 
        {
        }

        public UniformBoolean(bool value)
        {
            mValue = value;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform1f(location, mValue ? 1f : 0f);
        }

        public static implicit operator UniformBoolean(bool d)
        {
            return new UniformBoolean(d);
        }
    }

    public class UniformInteger : Uniform
    {
        public override UniformType Type => UniformType.Integer;

        private int mValue;

        public UniformInteger()
            : this(0) 
        {
        }

        public UniformInteger(int value) 
        {
            mValue = value;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform1i(location, mValue);
        }

        public static implicit operator UniformInteger(int d)
        {
            return new UniformInteger(d);
        }
    }

    public class UniformFloat : Uniform
    {
        public override UniformType Type => UniformType.Float;

        private float mValue;

        public UniformFloat()
            : this(0)
        {
        }

        public UniformFloat(float value)
        {
            mValue = value;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform1f(location, mValue);
        }

        public static implicit operator UniformFloat(float d)
        {
            return new UniformFloat(d);
        }
    }

    public class UniformVec2 : Uniform
    {
        public override UniformType Type => UniformType.Vec2;

        public float Value1 { get; }

        public float Value2 { get; }

        public UniformVec2()
            : this(0, 0)
        {
        }

        public UniformVec2(Vector2 v)
            : this(v.X, v.Y)
        {
        }

        public UniformVec2(float v1, float v2)
        {
            Value1 = v1;
            Value2 = v2;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform2f(location, Value1, Value2);
        }
    }

    public class UniformVec3 : Uniform
    {
        public override UniformType Type => UniformType.Vec3;

        public float Value1 { get; }

        public float Value2 { get; }

        public float Value3 { get; }

        public UniformVec3()
            : this(0, 0, 0)
        {
        }

        public UniformVec3(Vector3 v)
            : this(v.X, v.Y, v.Z)
        {
        }

        public UniformVec3(float v1, float v2, float v3)
        {
            Value1 = v1;
            Value2 = v2;
            Value3 = v3;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform3f(location, Value1, Value2, Value3);
        }
    }

    public class UniformVec4 : Uniform
    {
        public override UniformType Type => UniformType.Vec4;

        public float Value1 { get; }

        public float Value2 { get; }

        public float Value3 { get; }

        public float Value4 { get; }

        public UniformVec4()
            : this(0, 0, 0, 0) 
        {
        }

        public UniformVec4(Vector4 v)
            : this(v.X, v.Y, v.Z, v.W)
        {
        }

        public UniformVec4(float v1, float v2, float v3, float v4)
        {
            Value1 = v1;
            Value2 = v2;
            Value3 = v3;
            Value4 = v4;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform4f(location, Value1, Value2, Value3, Value4);
        }
    }

    public class UniformiVec2 : Uniform
    {
        public override UniformType Type => UniformType.iVec2;

        public int Value1 { get; }

        public int Value2 { get; }

        public UniformiVec2()
            : this(0, 0)
        {
        }

        public UniformiVec2(Vector2 v)
            : this((int)v.X, (int)v.Y)
        {
        }

        public UniformiVec2(int v1, int v2)
        {
            Value1 = v1;
            Value2 = v2;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform2i(location, Value1, Value2);
        }
    }

    public class UniformiVec3 : Uniform
    {
        public override UniformType Type => UniformType.iVec3;

        public int Value1 { get; }

        public int Value2 { get; }

        public int Value3 { get; }

        public UniformiVec3()
            : this(0, 0, 0)
        {
        }

        public UniformiVec3(Vector3 v)
            : this((int)v.X, (int)v.Y, (int)v.Z)
        {
        }

        public UniformiVec3(int v1, int v2, int v3)
        {
            Value1 = v1;
            Value2 = v2;
            Value3 = v3;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform3i(location, Value1, Value2, Value3);
        }
    }

    public class UniformiVec4 : Uniform
    {
        public override UniformType Type => UniformType.iVec4;

        public int Value1 { get; }

        public int Value2 { get; }

        public int Value3 { get; }

        public int Value4 { get; }

        public UniformiVec4()
            : this(0, 0, 0, 0) 
        {
        }

        public UniformiVec4(Vector4 v)
            : this((int)v.X, (int)v.Y, (int)v.Z, (int)v.W) 
        {
        }

        public UniformiVec4(int v1, int v2, int v3, int v4)
        {
            Value1 = v1;
            Value2 = v2;
            Value3 = v3;
            Value4 = v4;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform4i(location, Value1, Value2, Value3, Value4);
        }
    }

    public class UniformbVec2 : Uniform
    {
        public override UniformType Type => UniformType.bVec2;

        public bool Value1 { get; }

        public bool Value2 { get; }

        public UniformbVec2()
            : this(false, false)
        {
        }

        public UniformbVec2(Vector2 v)
            : this(v.X > 0 ? true : false, v.Y > 0 ? true : false)
        {
        }

        public UniformbVec2(bool v1, bool v2) 
        {
            Value1 = v1;
            Value2 = v2;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform2f(location, Value1 ? 1f : 0f, Value2 ? 1f : 0f);
        }
    }

    public class UniformbVec3 : Uniform
    {
        public override UniformType Type => UniformType.bVec3;

        public bool Value1 { get; }

        public bool Value2 { get; }

        public bool Value3 { get; }

        public UniformbVec3()
            : this(false, false, false)
        {
        }

        public UniformbVec3(Vector3 v)
            : this(v.X > 0 ? true : false, v.Y > 0 ? true : false, v.Z > 0 ? true : false)
        {
        }

        public UniformbVec3(bool v1, bool v2, bool v3)
        {
            Value1 = v1;
            Value2 = v2;
            Value3 = v3;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform3f(location, Value1 ? 1f : 0f, Value2 ? 1f : 0f, Value3 ? 1f : 0f);
        }
    }

    public class UniformbVec4 : Uniform
    {
        public override UniformType Type => UniformType.bVec4;

        public bool Value1 { get; }

        public bool Value2 { get; }

        public bool Value3 { get; }

        public bool Value4 { get; }

        public UniformbVec4()
            : this(false, false, false, false) 
        {
        }

        public UniformbVec4(Vector4 v) 
            : this(v.X > 0 ? true : false, v.Y > 0 ? true : false, v.Z > 0 ? true : false, v.W > 0 ? true : false)
        {
        }

        public UniformbVec4(bool v1, bool v2, bool v3, bool v4)
        {
            Value1 = v1;
            Value2 = v2;
            Value3 = v3;
            Value4 = v4;
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniform4f(location, Value1 ? 1f : 0f, Value2 ? 1f : 0f, Value3 ? 1f : 0f, Value4 ? 1f : 0f);
        }
    }

    public class UniformMat2 : Uniform
    {
        public override UniformType Type => UniformType.Mat2;

        private float[] mValue;

        public UniformMat2()
            : this(Matrix4x4.Identity)
        {
        }

        public UniformMat2(Matrix4x4 matrix)
            : this(matrix.M11, matrix.M12,
                   matrix.M21, matrix.M22)
        {
        }

        public UniformMat2(float m11, float m12, float m21, float m22)
        {
            mValue = new float[4] 
            {
                m11, m12,
                m21, m22
            };
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniformMatrix2fv(location, false, mValue);
        }
    }

    public class UniformMat3 : Uniform
    {
        public override UniformType Type => UniformType.Mat3;

        private float[] mValue;

        public UniformMat3()
            : this(Matrix4x4.Identity)
        {
        }

        public UniformMat3(Matrix4x4 matrix)
            : this( matrix.M11, matrix.M12, matrix.M13,
                    matrix.M21, matrix.M22, matrix.M23,
                    matrix.M31, matrix.M32, matrix.M33)
        {

        }

        public UniformMat3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            mValue = new float[9] 
            {
                m11, m12, m13,
                m21, m22, m23,
                m31, m32, m33
            };
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniformMatrix3fv(location, false, mValue);
        }
    }

    public class UniformMat4 : Uniform
    {
        public override UniformType Type => UniformType.Mat4;

        private float[] mValue;

        public UniformMat4() 
            : this(Matrix4x4.Identity)
        {
        }

        public UniformMat4(Matrix4x4 matrix) 
            : this( matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                    matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                    matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                    matrix.M41, matrix.M42, matrix.M43, matrix.M44)
        {

        }

        public UniformMat4( float m11, float m12, float m13, float m14,
                            float m21, float m22, float m23, float m24,
                            float m31, float m32, float m33, float m34,
                            float m41, float m42, float m43, float m44)
        {
            mValue = new float[16]
            {
                m11, m12, m13, m14,
                m21, m22, m23, m24,
                m31, m32, m33, m34,
                m41, m42, m43, m44
            };
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.uniformMatrix4fv(location, false, mValue);
        }
    }

    public class UniformSampler2D : Uniform
    {
        public override UniformType Type => UniformType.Sampler2D;

        public Texture2D Texture { get; }

        public UniformSampler2D() 
        {
        }

        public UniformSampler2D(Texture2D texture) 
        {
            Texture = texture;

            /*
            mTexture = _gl.createTexture();
            _gl.bindTexture(_gl.TEXTURE_2D, _neheTexture);
            _gl.pixelStorei(_gl.UNPACK_FLIP_Y_WEBGL, 1);
            _gl.texImage2D(_gl.TEXTURE_2D, 0, _gl.RGBA, _gl.RGBA, _gl.UNSIGNED_BYTE, image.imageData);
            _gl.texParameteri(_gl.TEXTURE_2D, _gl.TEXTURE_MAG_FILTER, (int)_gl.LINEAR);
            _gl.texParameteri(_gl.TEXTURE_2D, _gl.TEXTURE_MIN_FILTER, (int)_gl.LINEAR_MIPMAP_LINEAR);
            _gl.generateMipmap(_gl.TEXTURE_2D);
            _gl.texParameterf(_gl.TEXTURE_2D, glExtensionTextureFilterAnisotropic.TEXTURE_MAX_ANISOTROPY_EXT, 16);
            _gl.bindTexture(_gl.TEXTURE_2D, null);
            */
        }

        internal override void Set(WebGLUniformLocation location, Effect shader)
        {
            shader.GraphicsDevice.Context.activeTexture(shader.GraphicsDevice.Context.TEXTURE0);
            shader.GraphicsDevice.Context.bindTexture(shader.GraphicsDevice.Context.TEXTURE_2D, Texture.WebGLTexture);
            shader.GraphicsDevice.Context.uniform1i(location, 0);
        }
    }
}
