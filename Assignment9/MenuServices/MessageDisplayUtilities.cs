using System;
using System.Diagnostics.CodeAnalysis;

namespace Assignment9
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    class MessageDisplayUtilities
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>       
        public static void NegativeMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void PositiveMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void MenuMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void ScoreboardMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void CurrentStatsMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        #endregion
    }
}
