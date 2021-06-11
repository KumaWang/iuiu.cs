using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using WebGL;

namespace engine.framework.graphics
{
    public class Effect : IEnumerable<Uniform>
    {
        private uint mSampler2DBindCount;

        private WebGLProgram mProgram;
        private Dictionary<string, Uniform> mUniforms;
        private List<Texture2D> mUsingTextures;
        private Dictionary<string, WebGLUniformLocation> mUniformLocations;

        public GraphicsDevice GraphicsDevice { get; }

        public Uniform this[string name] 
        {
            get 
            {
                if (mUniforms.ContainsKey(name)) 
                {
                    return mUniforms[name];
                }

                return null;
            }
        }

        internal uint Sampler2DBindCount => mSampler2DBindCount;

        internal WebGLProgram Program => mProgram;

        public Effect(GraphicsDevice device, string fragment, string vertex) 
        {
            GraphicsDevice = device;

            mUniforms = new Dictionary<string, Uniform>();
            mUsingTextures = new List<Texture2D>();
            mUniformLocations = new Dictionary<string, WebGLUniformLocation>();

            var fragmentShader = GetShader(GraphicsDevice.Context, GraphicsDevice.Context.createShader(GraphicsDevice.Context.FRAGMENT_SHADER), fragment);
            var vertexShader = GetShader(GraphicsDevice.Context, GraphicsDevice.Context.createShader(GraphicsDevice.Context.VERTEX_SHADER), vertex);

            mProgram = GraphicsDevice.Context.createProgram();
            GraphicsDevice.Context.attachShader(mProgram, vertexShader);
            GraphicsDevice.Context.attachShader(mProgram, fragmentShader);
            GraphicsDevice.Context.linkProgram(mProgram);

            if (!GraphicsDevice.Context.getProgramParameter(mProgram, GraphicsDevice.Context.LINK_STATUS))
            {
                throw new Exception(@"Could not initialise shaders");
            }

            Apply();
            CreateUniforms(GLSLHelperParser.ParseUniforms(fragment).Union(GLSLHelperParser.ParseUniforms(vertex)));
        }

        private void CreateUniforms(IEnumerable<(string type, string name)> uniforms) 
        {
            var unis = uniforms.ToArray();
            foreach (var u in unis) 
            {
                Uniform uniform = null;
                switch (u.type.ToLower()) 
                {
                    case "bvec2":
                        uniform = new UniformbVec2(false, false);
                        break;
                    case "ivec2":
                        uniform = new UniformiVec2(0, 0);
                        break;
                    case "vec2":
                        uniform = new UniformVec2(0, 0);
                        break;
                    case "bvec3":
                        uniform = new UniformbVec3(false, false, false);
                        break;
                    case "ivec3":
                        uniform = new UniformiVec3(0, 0, 0);
                        break;
                    case "vec3":
                        uniform = new UniformVec3(0, 0, 0);
                        break;
                    case "bvec4":
                        uniform = new UniformbVec4(false, false, false, false);
                        break;
                    case "ivec4":
                        uniform = new UniformiVec4(0, 0, 0, 0);
                        break;
                    case "vec4":
                        uniform = new UniformVec4(0, 0, 0, 0);
                        break;
                    case "mat2":
                        uniform = new UniformMat2(0, 0, 0, 0);
                        break;
                    case "mat3":
                        uniform = new UniformMat3(0, 0, 0, 0, 0, 0, 0, 0, 0);
                        break;
                    case "mat4":
                        uniform = new UniformMat4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        break;
                    case "samplercube":
                        throw new NotImplementedException();
                    case "sampler2d":
                        uniform = new UniformSampler2D(null);
                        break;
                    case "bool":
                        uniform = new UniformBoolean(false);
                        break;
                    case "float":
                        uniform = new UniformFloat(0);
                        break;
                    case "int":
                        uniform = new UniformInteger(0);
                        break;
                }

                uniform.Name = u.name;
                mUniforms[u.name] = uniform;
            }
        }

        private WebGLShader GetShader(WebGLRenderingContext gl, WebGLShader shader, string str)
        {
            gl.shaderSource(shader, str);
            gl.compileShader(shader);

            if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS))
            {
                throw new Exception(gl.getShaderInfoLog(shader));
            }
            return shader;
        }

        public void Apply()
        {
            if (GraphicsDevice.Context.m_currentProgram != mProgram)
                GraphicsDevice.Context.useProgram(mProgram);
        }

        public void ApplyBuffer(Buffer buffer)
        {
            GraphicsDevice.Context.bindBuffer(GraphicsDevice.Context.ARRAY_BUFFER, buffer.WebGLBuffer);
            GraphicsDevice.Context.vertexAttribPointer(buffer.Location, buffer.ItemSize, GraphicsDevice.Context.FLOAT, false, 0, 0);
        }

        public void Uniforms(string name, bool value)
        {
            Uniforms(name, new UniformBoolean(value));
        }

        public void Uniforms(string v, Texture2D texture2d)
        {
            Uniforms(v, new UniformSampler2D(texture2d));
        }

        public void Uniforms(string name, Vector2 value)
        {
            Uniforms(name, new UniformbVec2(value));
        }

        public void Uniforms(string v, Matrix4x4 mt)
        {
            Uniforms(v, new UniformMat4(mt));
        }

        public void Uniforms(string name, Uniform uniform)
        {
            if (!mUniforms.ContainsKey(name))
                mUniforms[name] = uniform;

            if (uniform.Type != mUniforms[name].Type)
                throw new NotSupportedException();

            mUniforms[name] = uniform;
            uniform.Name = name;

            Apply();

            if (!mUniformLocations.ContainsKey(name))
                mUniformLocations[name] = GraphicsDevice.Context.getUniformLocation(mProgram, name);

            switch (uniform.Type) 
            {
                case UniformType.Sampler1D:
                case UniformType.Sampler3D:
                case UniformType.SamplerCube:
                    throw new NotImplementedException();
                case UniformType.Sampler2D:
                    var s2d = (uniform as UniformSampler2D).Texture;
                    if (!mUsingTextures.Contains(s2d))
                    {
                        mUsingTextures.Add(s2d);
                        mSampler2DBindCount++;
                        uniform.Set(mUniformLocations[name], this);
                    }
                    else 
                    {
                        throw new Exception($"Texture{mUniformLocations[name]} has already bind");
                    }
                    break;
                default:
                    uniform.Set(mUniformLocations[name], this);
                    break;
            }
        }

        public void Draw(IndexBuffer indexer, int offset, int count) 
        {
            GraphicsDevice.Context.bindBuffer(GraphicsDevice.Context.ELEMENT_ARRAY_BUFFER, indexer.WebGLBuffer);
            GraphicsDevice.Context.drawElements(GraphicsDevice.Context.TRIANGLES, count, GraphicsDevice.Context.UNSIGNED_SHORT, offset);

            mUsingTextures.Clear();

            mSampler2DBindCount = 0;
            for (uint i = 0; i < mSampler2DBindCount; i++)
                GraphicsDevice.Context.bindTexture(GraphicsDevice.Context.TEXTURE_2D + i, null);
        }

        public IEnumerator<Uniform> GetEnumerator()
        {
            return mUniforms.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
