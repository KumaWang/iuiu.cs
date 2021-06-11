using System;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class JSDate
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double now()
        {
            return DateTime.UtcNow.Subtract(Epoch).TotalMilliseconds;
        }
    }

    // ReSharper restore InconsistentNaming
}
