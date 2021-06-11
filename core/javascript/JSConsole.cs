using System;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    static class JSConsole
    {
        public static void log(string msg)
        {
            writeLine(msg, ConsoleColor.Green);
        }

        public static void warn(string msg)
        {
            writeLine(msg, ConsoleColor.Yellow);
        }

        public static void error(string msg)
        {
            writeLine(msg, ConsoleColor.Red);
        }

        public static void debug(string msg)
        {
            writeLine(msg, ConsoleColor.DarkCyan);
        }

        private static void writeLine(string msg, ConsoleColor color)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = prevColor;
        }
    }

    // ReSharper restore InconsistentNaming
}
