namespace WebGL
{
    // ReSharper disable InconsistentNaming

    abstract class TypedArray : ArrayBufferView
    {
        public int bytesPerElement { get; internal set; }

        public int length { get; internal set; }
    }

    // ReSharper restore InconsistentNaming
}
