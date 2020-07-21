using System;
using System.Diagnostics.CodeAnalysis;

namespace Assignment9
{
    /// <summary>
    /// Various message display methods for displaying content in the appropriate foreground color, such as yellow for menu options, red for negative messages, green for good/positive messages, etc.
    /// </summary>
    [ExcludeFromCodeCoverage]
    class MessageDisplayUtilities
    {
        #region Methods

        /// <summary>
        /// Changes the color of the message to red for erorrs and negative messages.
        /// </summary>
        /// <param name="message">The message passed into the method.</param>       
        [ExcludeFromCodeCoverage]
        public static void NegativeMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Changes the color of the message to green for positive messages.
        /// </summary>
        /// <param name="message">The message passed into the method.</param> 
        [ExcludeFromCodeCoverage]
        public static void PositiveMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Changes the color of the message to yellow for menu options and display.
        /// </summary>
        /// <param name="message">The message passed into the method.</param> 
        [ExcludeFromCodeCoverage]
        public static void MenuMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Changes the color of the message to magenta when showing scores from the database.
        /// </summary>
        /// <param name="message">The message passed into the method.</param> 
        [ExcludeFromCodeCoverage]
        public static void ScoreboardMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Changes the color of the message to cyan when displaying current sessin scores/stats.
        /// </summary>
        /// <param name="message">The message passed into the method.</param> 
        [ExcludeFromCodeCoverage]
        public static void CurrentStatsMessageDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        #endregion
    }
}
