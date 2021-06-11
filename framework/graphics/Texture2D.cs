using System;
using System.Drawing;
using System.IO;
using WebGL;

namespace engine.framework.graphics
{
    public sealed class Texture2D
    {
        private WebGLTexture id;
        private float oneOverWidth;
        private float oneOverHeight;

        public int Width { get; }

        public int Height { get; }

        public GraphicsDevice GraphicsDevice { get; }

        internal WebGLTexture WebGLTexture => id;

        public Texture2D(GraphicsDevice device, int width, int height, bool minimap)
        {
            this.oneOverWidth = 1.0f / width;
            this.oneOverHeight = 1.0f / height;

            Width = width;
            Height = height;
            GraphicsDevice = device;

            var gl = device.Context;
            var glExtensionTextureFilterAnisotropic = (WebGLExtension)(gl.getExtension("EXT_texture_filter_anisotropic") ??
                                                                      gl.getExtension("MOZ_EXT_texture_filter_anisotropic") ??
                                                                      gl.getExtension("WEBKIT_EXT_texture_filter_anisotropic"));

            id = device.Context.createTexture();
            gl.bindTexture(gl.TEXTURE_2D, id);
            gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, width, height, 0, gl.RGBA, gl.UNSIGNED_BYTE, new Uint8Array(new byte[width * height * 4]));
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_LINEAR);
            gl.generateMipmap(gl.TEXTURE_2D);
            gl.texParameterf(gl.TEXTURE_2D, glExtensionTextureFilterAnisotropic.TEXTURE_MAX_ANISOTROPY_EXT, 16);
            gl.bindTexture(gl.TEXTURE_2D, null);
        }

        public void GetData(byte[] result) 
        {
            GetData(0, new Rectangle2D(0, 0, Width, Height), result, 0, Width * Height);
        }

        public void GetData(int level, Rectangle2D rect, byte[] result, int index, int count)
        {
            var pixels = new Uint8Array(count);
            //gl.readPixels(0, 0, mCtrl.ClientSize.Width, mCtrl.ClientSize.Height, gl.RGBA, gl.UNSIGNED_BYTE, pixels);
        }

        public void SetData(Bitmap bitmap) 
        {
            var image = new WebGL.Image(bitmap);

            var gl = GraphicsDevice.Context;
            gl.bindTexture(gl.TEXTURE_2D, id);
            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image.imageData);
            gl.bindTexture(gl.TEXTURE_2D, null);
        }

        public static Texture2D FromStream(object graphicsDevice, Stream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}
