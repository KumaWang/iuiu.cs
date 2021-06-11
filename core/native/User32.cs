using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    [SuppressUnmanagedCodeSecurity]
    static class User32
    {
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern Int32 ReleaseDC(IntPtr hWnd, IntPtr hDC);
    }

    // ReSharper restore InconsistentNaming
}
