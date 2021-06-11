using System.Drawing;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class ImageData
    {
        private readonly int m_width;
        private readonly int m_height;
        private readonly Uint8ClampedArray m_data;

        public ImageData(Size size)
        {
            this.m_width = size.Width;
            this.m_height = size.Height;
            this.m_data = new Uint8ClampedArray(size.Width * size.Height * 4);
        }

        public ImageData(Size size, Uint8ClampedArray byteArray)
        {
            this.m_width = size.Width;
            this.m_height = size.Height;
            this.m_data = byteArray;
        }

        public int width()
        {
            return this.m_width;
        }

        public int height()
        {
            return this.m_height;
        }

        public Uint8ClampedArray data()
        {
            return this.m_data;
        }
    }

    // ReSharper restore InconsistentNaming
}
