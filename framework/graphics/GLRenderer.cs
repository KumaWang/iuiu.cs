using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace engine.framework.graphics
{
    public delegate void GLRendererPaintDelegate(GLRenderer render);

    public partial class GLRenderer
    {
        private Timer mTimer;
        private DateTime mLastUpdateTime;
        private Func<Size> mGetSizeFunc;
        private Dictionary<Bitmap, Texture2D> mCache;

        /// <summary>
        /// 最小显示的屏幕比例内容
        /// </summary>
        public float                MinDpi          { get; private set; }

        /// <summary>
        /// 获取摄像头
        /// </summary>
        public ICamera            Camera          { get; private set; }

        /// <summary>
        /// 渲染设备
        /// </summary>
        public GraphicsDevice       GraphicsDevice  { get; private set; }

        /// <summary>
        /// 渲染器
        /// </summary>
        public GLRenderer   Render          { get; private set; }

        public Color              ClearColor      { get; set; }

        public int UpdateInterval
        {
            get { return mTimer.Interval; }
            set { mTimer.Interval = value; }
        }

        public GLRenderer(IntPtr handle, Func<Size> getSizeFunc)                                                                                     
        {
            ClearColor = Color.CornflowerBlue;
            mGetSizeFunc = getSizeFunc;
            mLastUpdateTime = DateTime.Now;
            mCache = new Dictionary<Bitmap, Texture2D>();
            GraphicsDevice = new GraphicsDevice(handle, getSizeFunc);
            Camera = new Camera(this);
            Render = new GLRenderer(this);
        }

        public void Frame()
        {
            var elapseTime = (int)(DateTime.Now - mLastUpdateTime).TotalMilliseconds;
            mLastUpdateTime = DateTime.Now;

            var size = mGetSizeFunc();
            GraphicsDevice.Viewport = new AABB(0, 0, size.Width, size.Height);

            // 更新镜头
            Camera.Update(elapseTime);

            // draw
            GraphicsDevice.Clear(ClearColor);
            Render.Begin();

            Paint?.Invoke(Render);

            Render.End();

            GraphicsDevice.SwapBuffers();
        }

        protected internal virtual List<Vertices> Triangulate(Vertices vertices) 
        {
            throw new NotImplementedException();
        }

        #region Helper

        public Image ImageFromInculde(string inculde)
        {
            var texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead(inculde));

            // 返回图片
            return new Image()
            {
                Texture = texture,
                OffsetX = 0,
                OffsetY = 0,
                Width = texture.Width,
                Height = texture.Height
            };
        }

        public Image ImageFromTexture2D(Texture2D tex2d)                                                                                                            
        {
            return new Image(tex2d, tex2d.Width, tex2d.Height);
        }

        public Image ImageFromBitmap(Bitmap bmp)                                                                                                     
        {
            if (!mCache.ContainsKey(bmp))
            {
                mCache[bmp] = new Texture2D(GraphicsDevice, bmp.Width, bmp.Height, false);
                mCache[bmp].SetData(bmp);
            }

            return ImageFromTexture2D(mCache[bmp]);
        }

        public byte[] GetData(Image image, int level, Rectangle2D rect)                                                                                             
        {
            // 获得texture2d
            var tex2d = image.Texture; // Content.Load<Texture2D>(image.Inculde);
           
            // 创建结果
            var result = new byte[rect.Width * rect.Height];

            // 获得数据
            tex2d.GetData(level, rect, result, 0, rect.Width * rect.Height);

            // 返回结果
            return result;
        }

        #endregion

        public event GLRendererPaintDelegate Paint;
    }
}
