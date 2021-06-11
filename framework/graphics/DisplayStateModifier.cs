namespace engine.framework.graphics
{
    public struct DisplayStateModifier
    {
        public float RFactor;
        public float GFactor;
        public float BFactor;
        public float AFactor;

        public float XFactor;
        public float YFactor;
        public float ScaleXFactor;
        public float ScaleYFactor;

        public float RotateFactor;

        public bool IsVaild
        {
            get
            {
                return RFactor != 1 || GFactor != 1 || BFactor != 1 ||
                       AFactor != 1 || XFactor != 1 || YFactor != 1 ||
                       ScaleXFactor != 1 || ScaleYFactor != 1 || RotateFactor != 1;
            }
        }

        public static DisplayStateModifier Create() 
        {
            var value = new DisplayStateModifier();
            value.RFactor = 1;
            value.GFactor = 1;
            value.BFactor = 1;
            value.AFactor = 1;

            value.XFactor = 1;
            value.YFactor = 1;
            value.ScaleXFactor = 1;
            value.ScaleYFactor = 1;
            value.RotateFactor = 1;
            return value;
        }
    }
}
