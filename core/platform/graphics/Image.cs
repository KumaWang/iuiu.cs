using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class Image : JSEventDispatcher
    {
        private string m_src;
        private ImageData m_imageData;

        public Image() : this(64, 64)
        {
        }

        public Image(int width, int height)
        {
            this.m_imageData = new ImageData(new Size(width, height));
        }

        public Image(byte[] data, int width, int height)
        {
            this.m_imageData = new ImageData(new Size(width, height), new Uint8ClampedArray(data));
        }

        public Image(Bitmap bitmap)
        {
            this.processBitmap(bitmap);
        }

        public string src
        {
            get { return this.m_src; }
            set { this.loadImage(value); }
        }

        public int width
        {
            get { return this.m_imageData.width(); }
        }

        public int height
        {
            get { return this.m_imageData.height(); }
        }

        public Size size
        {
            get { return new Size(this.width, this.height); }
        }

        public ImageData imageData
        {
            get { return this.m_imageData; }
        }

        private void loadImage(string filename)
        {
            try
            {
                this.processBitmap(new Bitmap(this.m_src = filename));
                this.dispatchEvent(new JSEvent(this, "load"));
            }
            catch (Exception)
            {
                this.dispatchEvent(new JSEvent(this, "error"));
            }
        }

        private void processBitmap(Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var data = new byte[bitmap.Width * bitmap.Height * 4];
            Marshal.Copy(bitmapData.Scan0, data, 0, data.Length);
            bitmap.UnlockBits(bitmapData);
            this.m_imageData = new ImageData(bitmap.Size, new Uint8ClampedArray(toRGBA(data)));
        }

        private static byte[] toRGBA(byte[] data)
        {
            for (var i = 0; i < data.Length; i += 4)
            {
                var blu = data[i + 0];
                var grn = data[i + 1];
                var red = data[i + 2];
                var alp = data[i + 3];
                data[i + 0] = red;
                data[i + 1] = grn;
                data[i + 2] = blu;
                data[i + 3] = alp;
            }
            return data;
        }
    }

    // ReSharper restore InconsistentNaming
}
