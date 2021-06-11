using System;
using System.Numerics;

namespace engine.framework.graphics
{
    public struct DisplayState : IEquatable<DisplayState>
    {
        public Image Image;
        public DisplayStateBlendType BlendType;
        public VertexPositionColorTexture P1;
        public VertexPositionColorTexture P2;
        public VertexPositionColorTexture P3;
        public bool TileTexture;
        public Vector2 TileSize;
        public Vector2 TileStartOffset;
        public Vector2 TileUVOffset;
        public Vector2 TileUVSize;
        public bool SoildColor;
 
        public bool IsEmpty                         
        {
            get { return Image == null; }
        }

        public bool Equals(DisplayState other)      
        {
            return P1.Equals(other.P1) && P2.Equals(P2) && P3.Equals(other.P3);
        }

        public override int GetHashCode()           
        {
            unchecked
            {
                int result = P1.GetHashCode();
                result = (result * 397) ^ P2.GetHashCode();
                result = (result * 397) ^ P3.GetHashCode();
                return result;
            }
        }

        /*
        public Bitmap ToBitmap()                    
        {
            byte[] rawData = new byte[Texture.Width * Texture.Height];
            Texture.GetData<byte>(rawData);
            LockBitmap lockBitmap = new LockBitmap(Texture.Width, Texture.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            lockBitmap.LockBits();

            lockBitmap.Pixels = rawData;

            var bitmapRect = lockBitmap.GetRect(
                (int)(LeftTop.TextureCoordinate.X * Texture.Width),
                (int)(LeftTop.TextureCoordinate.Y * Texture.Height),
                (int)((RightTop.TextureCoordinate.X - LeftTop.TextureCoordinate.X) * Texture.Width),
                (int)((RightBottom.TextureCoordinate.Y - LeftTop.TextureCoordinate.Y) * Texture.Height),
                false,
                0,
                0,
                false);

            lockBitmap.UnlockBits();

            return bitmapRect.Bitmap;
        }

        public static DisplayObjectState operator *(DisplayObjectState state, Matrix matrix) 
        {
            var displayState = new DisplayObjectState();
            displayState.LeftTop = GetSlot(value, lastDisplayState.LeftTop, nextDisplayState.LeftTop);
            displayState.RightTop = GetSlot(value, lastDisplayState.RightTop, nextDisplayState.RightTop);
            displayState.RightBottom = GetSlot(value, lastDisplayState.RightBottom, nextDisplayState.RightBottom);
            displayState.LeftBottom = GetSlot(value, lastDisplayState.LeftBottom, nextDisplayState.LeftBottom);

            return 
        }
        */
    }
}
