namespace engine.framework.graphics
{
    public class Image
    {
        public Texture2D    Texture             { get; set; }
        public int          OffsetX             { get; set; }
        public int          OffsetY             { get; set; }
        public int          Width               { get; set; }
        public int          Height              { get; set; }

        public Image() 
        {
        }

        public Image(Texture2D texture, int width, int height)                                                                              
        {
            this.Texture = texture;
            this.Width = width;
            this.Height = height;
        }
    }
}
