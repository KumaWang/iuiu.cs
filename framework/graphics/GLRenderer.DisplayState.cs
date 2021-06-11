using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using WebGL;

namespace engine.framework.graphics
{
    partial class GLRenderer
    {
        private const string Fragment =
            @"
            #ifdef GL_ES
            precision mediump float;
            #endif

            varying vec2 vTextureCoord;
            varying vec4 vColor;
            uniform bool uBlur;
            uniform sampler2D uSampler;

            void main(void) {
                if(uBlur) {
                    vec4 color = vec4(0.0);
	                float seg = 5.0;
	                float i = -seg;
	                float j = 0.0;
	                float f = 0.0;
	                float dv = 2.0 / 512.0;
	                float tot = 0.0;
	                for(; i <= seg; ++i)
	                {
		                for(j = -seg; j <= seg; ++j)
		                {
			                f = (1.1 - sqrt(i*i + j*j)/8.0);
			                f *= f;
			                tot += f;
			                color += texture2D(uSampler, vec2(vTextureCoord.x + j * dv, vTextureCoord.y + i * dv) ).rgba * vColor.rgba * f;
		                }
	                }
	                color /= tot;
                    gl_FragColor = color;
                } else {
                    gl_FragColor = texture2D(uSampler, vec2(vTextureCoord.s, vTextureCoord.t)).rgba * vColor.rgba;
                }
            }";

        private const string Vertex =
            @"attribute vec3 aVertexPosition;
            attribute vec2 aTextureCoord;
            attribute vec4 aColor;

            uniform mat4 uMVMatrix;
            uniform mat4 uPMatrix;

            varying vec2 vTextureCoord;
            varying vec4 vColor;

            void main(void) {
                gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0);
                vTextureCoord = aTextureCoord;
                vColor = aColor;
            }";


        #region Private Members

        private const int                       InitialBatchSize = 1024;

        private Effect                          spriteBatchEffect;
        private bool                            hasBegun;
        private DisplayState[]                  displayObjects;
        private int                             currentBatchPosition;
        private Buffer                          aVertexPosition;
        private Buffer                          aTextureCoord;
        private Buffer                          aColor;
        private IndexBuffer                     indexer;
        private float[]                         aVertexPositionWriter;
        private float[]                         aTextureCoordWriter;
        private float[]                         aColorWriter;
        private ushort[]                        indexers;

        private float                           mViewportWidth;
        private float                           mViewportHeight;
        private Matrix4x4                       uPMatrix;
        private GLRenderer                      mWindow;

        // clip
        private Stack<AABB>                     mClipStack;
        private bool                            mEnableClip;
        private AABB                            mClipRect;
        private AABB                            mLastClipRect;

        #endregion

        public Effect Effect { get { return spriteBatchEffect; } }

        public GLRenderer(GLRenderer renderer)                                                                                                                                                                                  
        {
            GraphicsDevice = renderer.GraphicsDevice;

            this.mWindow = renderer;
            this.mClipStack = new Stack<AABB>();
            this.spriteBatchEffect = new Effect(renderer.GraphicsDevice, Fragment, Vertex);
            this.displayObjects = new DisplayState[InitialBatchSize];
            this.InitializeBuffers(InitialBatchSize);
        }
        public void Begin()                                                                                                                                                                                                     
        {
            Begin(mWindow.Camera.Transform);
        }

        public void Begin(Matrix4x4 uMVMatrix)                                                                                                                                                                                  
        {
            if (this.hasBegun == true)
            {
                throw new Exception("End() has to be called before a new SpriteBatch can be started with Begin()");
            }

            spriteBatchEffect.Apply();

            hasBegun = true;

            if (uPMatrix == default ||
                GraphicsDevice.Viewport.Width != mViewportWidth ||
                GraphicsDevice.Viewport.Height != mViewportHeight)
            {
                mViewportWidth = GraphicsDevice.Viewport.Width;
                mViewportHeight = GraphicsDevice.Viewport.Height;

                GraphicsDevice.Context.viewport(0, 0, (int)mViewportWidth, (int)mViewportHeight);

                float ratiox = 1;
                float ratioy = -1;
                float guiScaleFactor = 1;

                //var unitX = 0;
                //var unitY = 0;
                //GraphicsDevice.Viewport = new AABB(unitX, unitY, GraphicsDevice.Viewport.Width - unitX * 2, GraphicsDevice.Viewport.Height - unitY * 2);

                uPMatrix = new Matrix4x4()
                {
                    M11 = 2f * (this.mViewportWidth > 0 ? ratiox / mViewportWidth : 0f) * guiScaleFactor,
                    M22 = 2f * (this.mViewportHeight > 0 ? -ratiox / mViewportHeight : 0f) * guiScaleFactor,
                    M33 = 1f,
                    M44 = 1f,
                    M41 = -1f,
                    M42 = 1f
                };

                uPMatrix.M41 -= uPMatrix.M11;
                uPMatrix.M42 -= uPMatrix.M22;
                spriteBatchEffect.Uniforms("uPMatrix", uPMatrix);
            }

            uMVMatrix.M41 = mWindow.Camera.Position.X;
            uMVMatrix.M42 = mWindow.Camera.Position.Y;

            spriteBatchEffect.Uniforms("uMVMatrix", uMVMatrix);
            GraphicsDevice.Context.enable(GraphicsDevice.Context.BLEND);
            GraphicsDevice.Context.blendFunc(GraphicsDevice.Context.SRC_ALPHA, GraphicsDevice.Context.ONE_MINUS_SRC_ALPHA);
        }

        public void End()                                                                                                                                                                                                       
        {
            if (hasBegun == false)
            {
                throw new Exception("Begin() has to be called before End()");
            }

            hasBegun = false;

            if (currentBatchPosition > 0)
            {
                int startOffset = 0;
                for (int i = 0; i < currentBatchPosition; i++)
                {
                    var current = displayObjects[i];
                    var next = displayObjects[i + 1];
                    if (next.Image != current.Image || i == currentBatchPosition - 1 ||
                        current.TileTexture != next.TileTexture || current.TileStartOffset != next.TileStartOffset || current.TileUVOffset != next.TileUVOffset || current.TileUVSize != next.TileUVSize || current.TileSize != next.TileSize || current.SoildColor != next.SoildColor)
                    {
                        BatchRender(startOffset, i - startOffset + 1);
                        startOffset = i + 1;
                    }
                }

                Flush();
            }
        }

        /// <summary>
        /// 开始裁剪
        /// </summary>
        public void Clip(int x, int y, int width, int height)                                                                                                                                                                   
        {
            if (x < 0)
            {
                width = width + x;
                x = 0;
            }

            if (y < 0)
            {
                height = height + y;
                y = 0;
            }

            if (mEnableClip)
            {
                mClipStack.Push(mClipRect);

                if (x < mLastClipRect.LowerBound.X)
                    x = (int)mLastClipRect.LowerBound.X;

                if (y < mLastClipRect.LowerBound.Y)
                    y = (int)mLastClipRect.LowerBound.Y;

                if (x + width > mLastClipRect.UpperBound.X)
                    width = (int)mLastClipRect.Width;

                if (y + height > mLastClipRect.UpperBound.Y)
                    height = (int)mLastClipRect.Height;

                mClipRect = new AABB(new Vector2(x, y), new Vector2(x + width, y + height));
            }
            else
            {
                mLastClipRect = mClipRect = new AABB(new Vector2(x, y), new Vector2(x + width, y + height));
                mEnableClip = true;
            }
        }

        /// <summary>
        /// 结束裁剪
        /// </summary>
        public void EndClip()                                                                                                                                                                                                   
        {
            if (mClipStack.Count > 0)
            {
                mClipRect = mClipStack.Pop();
            }
            else
            {
                mEnableClip = false;
            }
        }

        private void BatchRender(int offset, int count)                                                                                                                                                                         
        {
            for (int i = 0; i < count; i++)
            {
                DisplayState currentSprite = this.displayObjects[i];

                aVertexPositionWriter[i * 9 + 0] = currentSprite.P1.Vertex.X;
                aVertexPositionWriter[i * 9 + 1] = currentSprite.P1.Vertex.Y;
                aVertexPositionWriter[i * 9 + 2] = currentSprite.P1.Vertex.Z;

                aColorWriter[i * 12 + 0] = currentSprite.P1.Color.R / 255f;
                aColorWriter[i * 12 + 1] = currentSprite.P1.Color.G / 255f;
                aColorWriter[i * 12 + 2] = currentSprite.P1.Color.B / 255f;
                aColorWriter[i * 12 + 3] = currentSprite.P1.Color.A / 255f;

                aTextureCoordWriter[i * 6 + 0] = currentSprite.P1.UV.X;
                aTextureCoordWriter[i * 6 + 1] = currentSprite.P1.UV.Y;

                aVertexPositionWriter[i * 9 + 3] = currentSprite.P2.Vertex.X;
                aVertexPositionWriter[i * 9 + 4] = currentSprite.P2.Vertex.Y;
                aVertexPositionWriter[i * 9 + 5] = currentSprite.P2.Vertex.Z;

                aColorWriter[i * 12 + 4] = currentSprite.P2.Color.R / 255f;
                aColorWriter[i * 12 + 5] = currentSprite.P2.Color.G / 255f;
                aColorWriter[i * 12 + 6] = currentSprite.P2.Color.B / 255f;
                aColorWriter[i * 12 + 7] = currentSprite.P2.Color.A / 255f;

                aTextureCoordWriter[i * 6 + 2] = currentSprite.P2.UV.X;
                aTextureCoordWriter[i * 6 + 3] = currentSprite.P2.UV.Y;

                aVertexPositionWriter[i * 9 + 6] = currentSprite.P3.Vertex.X;
                aVertexPositionWriter[i * 9 + 7] = currentSprite.P3.Vertex.Y;
                aVertexPositionWriter[i * 9 + 8] = currentSprite.P3.Vertex.Z;

                aColorWriter[i * 12 + 8] = currentSprite.P3.Color.R / 255f;
                aColorWriter[i * 12 + 9] = currentSprite.P3.Color.G / 255f;
                aColorWriter[i * 12 + 10] = currentSprite.P3.Color.B / 255f;
                aColorWriter[i * 12 + 11] = currentSprite.P3.Color.A / 255f;

                aTextureCoordWriter[i * 6 + 4] = currentSprite.P3.UV.X;
                aTextureCoordWriter[i * 6 + 5] = currentSprite.P3.UV.Y;
            }

            aVertexPosition.BindData(new Float32Array(aVertexPositionWriter));
            aColor.BindData(new Float32Array(aColorWriter));
            aTextureCoord.BindData(new Float32Array(aTextureCoordWriter));

            spriteBatchEffect.ApplyBuffer(this.aVertexPosition);
            spriteBatchEffect.ApplyBuffer(this.aColor);
            spriteBatchEffect.ApplyBuffer(this.aTextureCoord);

            spriteBatchEffect.Uniforms("uSampler", this.displayObjects[offset].Image.Texture);

            /*
            spriteBatchEffect.Uniforms("Soild", this.displayObjects[offset].SoildColor);
            spriteBatchEffect.Uniforms("Tile", this.displayObjects[offset].TileTexture);
            if (this.displayObjects[offset].TileTexture)
            {
                spriteBatchEffect.Uniforms("TileOffset", this.displayObjects[offset].TileStartOffset);
                spriteBatchEffect.Uniforms("TileSize", this.displayObjects[offset].TileSize);
                spriteBatchEffect.Uniforms("TileUVOffset", this.displayObjects[offset].TileUVOffset);
                spriteBatchEffect.Uniforms("TileUVSize", this.displayObjects[offset].TileUVSize);
            }
            */

            spriteBatchEffect.Draw(indexer, offset * 3, count * 3);
        }

        private void Flush()                                                                                                                                                                                                    
        {
            //GC.Collect(int.MaxValue, GCCollectionMode.Optimized, false, false);

            currentBatchPosition = 0;
        }

        private void InitializeBuffers(int size)
        {
            aVertexPosition = new Buffer(spriteBatchEffect, "aVertexPosition", 3, GraphicsDevice);
            aColor = new Buffer(spriteBatchEffect, "aColor", 4, GraphicsDevice);
            aTextureCoord = new Buffer(spriteBatchEffect, "aTextureCoord", 2, GraphicsDevice);

            aVertexPositionWriter = new float[3 * 3 * size];
            aColorWriter = new float[4 * 3 * size];
            aTextureCoordWriter = new float[2 * 3 * size];

            indexer = new IndexBuffer(spriteBatchEffect, GraphicsDevice);
            indexers = new ushort[size * 3];
            for (var i = 0; i < size; i++) 
            {
                var baseIndex = i * 3;
                indexers[baseIndex] = (ushort)baseIndex;
                indexers[baseIndex + 1] = (ushort)(baseIndex + 1);
                indexers[baseIndex + 2] = (ushort)(baseIndex + 2);
            }
            indexer.BindData(new Uint16Array(indexers));
        }

        public void Draw(Image image, Vector2 position, Color color)
        {
            Draw(image, position, color, 0, Vector2.Zero, Vector2.One, 0);
        }

        public void Draw(Image image, Vector2 position, Color color, float rotation)
        {
            Draw(image, position, color, rotation, Vector2.Zero, Vector2.One, 0);
        }

        public void Draw(Image image, Vector2 position, Color color, float rotation, Vector2 origin)
        {
            Draw(image, position, color, rotation, origin, Vector2.One, 0);
        }

        public void Draw(Image image, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale)
        {
            Draw(image, position, color, rotation, origin, scale, 0);
        }

        public void Draw(Image image, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, float layerDepth)
        {
            Draw(image, position, new AABB(0, 0, image.Width, image.Height), color, rotation, origin, scale, layerDepth);
        }

        public void Draw(Image texture, AABB rect, Color color)
        {
            Draw(texture, rect, new AABB(texture.OffsetX, texture.OffsetY, texture.Width, texture.Height), color);
        }

        public void Draw(Image texture, AABB rect, AABB? sourceRectangle, Color color)
        {
            Draw(texture, rect, sourceRectangle, color, 0, Vector2.Zero, 0);
        }

        public void Draw(Image texture, Vector2 position, AABB? sourceRectangle, Color color)
        {
            Draw(texture, position, sourceRectangle, color, 0, Vector2.Zero, 1, 0);
        }

        public void Draw(Image texture, AABB rect, AABB? sourceRectangle, Color color, float rotation, Vector2 origin, float layerDepth)
        {
            var scalex = sourceRectangle.HasValue ? rect.Width / sourceRectangle.Value.Width : rect.Width / texture.Width;
            var scaley = sourceRectangle.HasValue ? rect.Height / sourceRectangle.Value.Height : rect.Height / texture.Height;
            var position = new Vector2(rect.X, rect.Y);
            Draw(texture, position, sourceRectangle, color, rotation, origin, new Vector2(scalex, scaley), layerDepth);
        }

        public void Draw(Image texture, Vector2 position, AABB? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, float layerDepth)
        {
            Draw(texture, position, sourceRectangle, color, rotation, origin, new Vector2(scale, scale), layerDepth);
        }

        public void Draw(Image texture, Vector2 position, AABB? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, float layerDepth)
        {
            foreach (var state in DisplayStateRenderEx.GetDisplayStates(mWindow, texture, position, sourceRectangle, color, rotation, origin, scale, layerDepth)) Draw(state);
        }

        public void Draw(DisplayState state)
        {
            if (state.IsEmpty)
            {
                // TODO 日志输出
                return;
            }

            if (currentBatchPosition >= displayObjects.Length)
            {
                int newSize = displayObjects.Length * 2;
                Array.Resize<DisplayState>(ref displayObjects, newSize);
                InitializeBuffers(newSize);
            }

            displayObjects[currentBatchPosition++] = state;
        }

    }

    public static class DisplayStateRenderEx
    {
        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image image, Vector2 position, Color color)                                                                                                
        {
            return GetDisplayStates(window, image, position, color, 0);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image image, Vector2 position, Color color, float rotation)                                                                                
        {
            return GetDisplayStates(window, image, position, color, rotation, Vector2.Zero);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image image, Vector2 position, Color color, float rotation, Vector2 origin)                                                                
        {
            return GetDisplayStates(window, image, position, color, rotation, origin, Vector2.One);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image image, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale)                                                 
        {
            return GetDisplayStates(window, image, position, color, rotation, origin, scale, 0);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image image, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, float layerDepth)                               
        {
            return GetDisplayStates(window, image, position, new AABB(0, 0, image.Width, image.Height), color, rotation, origin, scale, layerDepth);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image texture, AABB rect, Color color)                                                                                                     
        {
            return GetDisplayStates(window, texture, rect, null, color);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image texture, AABB rect, AABB? sourceRectangle, Color color)                                                                              
        {
            return GetDisplayStates(window, texture, rect, sourceRectangle, color, 0, Vector2.Zero, 0);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image texture, Vector2 position, AABB? sourceRectangle, Color color)                                                                       
        {
            return GetDisplayStates(window, texture, position, sourceRectangle, color, 0, Vector2.Zero, 1, 0);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image texture, AABB rect, AABB? sourceRectangle, Color color, float rotation, Vector2 origin, float layerDepth)                            
        {
            var scalex = sourceRectangle.HasValue ? rect.Width / sourceRectangle.Value.Width : rect.Width / texture.Width;
            var scaley = sourceRectangle.HasValue ? rect.Height / sourceRectangle.Value.Height : rect.Height / texture.Height;
            var position = new Vector2(rect.X, rect.Y);
            return GetDisplayStates(window, texture, position, sourceRectangle, color, rotation, origin, new Vector2(scalex, scaley), layerDepth);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image texture, Vector2 position, AABB? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, float layerDepth)        
        {
            return GetDisplayStates(window, texture, position, sourceRectangle, color, rotation, origin, new Vector2(scale, scale), layerDepth);
        }

        public static IEnumerable<DisplayState> GetDisplayStates(this GLRenderer window, Image texture, Vector2 position, AABB? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, float layerDepth)      
        {
            var texture2d = texture.Texture;
            var uv = sourceRectangle ?? new AABB(0, 0, texture.Width, texture.Height);
            float uvLeft = (float)uv.Left / texture2d.Width;
            float uvTop = (float)uv.Top / texture2d.Height;
            float uvRight = (float)uv.Right / texture2d.Width;
            float uvBottom = (float)uv.Bottom / texture2d.Height;

            var v1 = new Vector3(position.X, position.Y, layerDepth);
            var v2 = new Vector3(position.X + uv.Width * scale.X, position.Y, layerDepth);
            var v3 = new Vector3(position.X + uv.Width * scale.X, position.Y + uv.Height * scale.Y, layerDepth);
            var v4 = new Vector3(position.X, position.Y + uv.Height * scale.Y, layerDepth);

            var angle = rotation % 360;
            if (angle != 0)
            {
                var center = new Vector3(v1.X + origin.X, v1.Y + origin.Y, 0);
                if (center != v1)
                {
                    v1 = MathHelper.PointRotate(center, v1, angle);
                }

                v2 = MathHelper.PointRotate(center, v2, angle);
                v3 = MathHelper.PointRotate(center, v3, angle);
                v4 = MathHelper.PointRotate(center, v4, angle);
            }

            var leftTop = new VertexPositionColorTexture(v1, color, new Vector2(uvLeft, uvTop));
            var rightTop = new VertexPositionColorTexture(v2, color, new Vector2(uvRight, uvTop));
            var rightBottom = new VertexPositionColorTexture(v3, color, new Vector2(uvRight, uvBottom));
            var leftBottom = new VertexPositionColorTexture(v4, color, new Vector2(uvLeft, uvBottom));

            yield return new DisplayState()
            {
                Image = texture,
                P1 = leftTop,
                P2 = rightTop,
                P3 = rightBottom
            };

            yield return new DisplayState()
            {
                Image = texture,
                P1 = leftTop,
                P2 = rightBottom,
                P3 = leftBottom
            };
        }
    }
}
