using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assignment9
{
    /// <summary>
    /// This class contains the various display methods for all the different menus in the game, as well as the methods needed to compare the user input to the confirm it is one of the accepted
    /// inputs, and return that input in the appropriate format.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MenuUtilities
    {
        #region Methods

        /// <summary>
        /// Displays the intitial Login Menu when starting the appliation, allowing users to see the initial menu options
        /// </summary>
        /// <param name="loginRecall">Determines if the while loop for this menu should be broken or continued.</param>
        [ExcludeFromCodeCoverage]
        public static void LoginMenu(out bool loginRecall, IUser user)
        {
            var menuLogic = new MenuOptionLogic();

            Console.WriteLine();
            MessageDisplayUtilities.MenuMessageDisplay("**********************************");
            MessageDisplayUtilities.MenuMessageDisplay("*                                *");
            MessageDisplayUtilities.MenuMessageDisplay("*  Make Your Selection:          *");
            MessageDisplayUtilities.MenuMessageDisplay("*  1) Create a new username      *");
            MessageDisplayUtilities.MenuMessageDisplay("*  2) Load an existing username  *");
            MessageDisplayUtilities.MenuMessageDisplay("*  3) View Leaderboard           *");
            MessageDisplayUtilities.MenuMessageDisplay("*  4) View All Scores            *");
            MessageDisplayUtilities.MenuMessageDisplay("*  5) Exit Application           *");
            MessageDisplayUtilities.MenuMessageDisplay("*                                *");
            MessageDisplayUtilities.MenuMessageDisplay("**********************************");

            loginRecall = menuLogic.LoginMenuLogic(out user);
            var gameModeRecall = true;
            while (gameModeRecall)
            {
                MenuUtilities.GameModeMenu(out gameModeRecall, user);
            }
        }

        /// <summary>
        /// Displays the Game mode menu, which is used to welcome the player depending on in if they are new, or a returning player. As well as displaying the various game modes/types 
        /// available for the user to select, including: Sudden Death(first win/loss determines victor), Triple Threat(best out of 3 mode), Drive For Five(best out of 5 mode), 
        /// and The Test Tour (best out of 10 mode).
        /// </summary>
        /// <param name="gameModeRecall">Determines if the while loop for this menu should be broken or continued.</param>
        /// <param name="user">The user that is currently logged in</param>
        [ExcludeFromCodeCoverage]
        public static void GameModeMenu(out bool gameModeRecall, IUser user)
        {
            var menuLogic = new MenuOptionLogic();
            var welcome = "";
            var count = 0;

            foreach (var item in DataAccess.Instance.GetUsers())
            {
                if (item.Username == user.Username)
                {
                    welcome = "Welcome back " + user.Username + ", please make your selection:";
                    count++;
                }
            }
            if (count == 0)
            {
                welcome = "Welcome " + user.Username + ", please make your selection:";
                DataAccess.Instance.CreateNewUser(user.Username);
            }

            Console.WriteLine();
            MessageDisplayUtilities.MenuMessageDisplay("**********************************************");
            Console.WriteLine();
            MessageDisplayUtilities.MenuMessageDisplay(welcome);
            MessageDisplayUtilities.MenuMessageDisplay("1) Sudden Death");
            MessageDisplayUtilities.MenuMessageDisplay("2) Triple Threat");
            MessageDisplayUtilities.MenuMessageDisplay("3) Drive For Five");
            MessageDisplayUtilities.MenuMessageDisplay("4) The Ten Tour");
            MessageDisplayUtilities.MenuMessageDisplay("5) Exit Application");
            Console.WriteLine();
            MessageDisplayUtilities.MenuMessageDisplay("**********************************************");

            gameModeRecall = menuLogic.GameModes(user);
        }

        /// <summary>
        /// Displays the game menu which shows the player the options available while in a particular game mode, such as: rock, paper, or scissors. As well as showing their current win/losses/draws for 
        /// that particular session of the game mode they are in.
        /// </summary>
        /// <param name="menuRecall">Determines if the while loop for this menu should be broken or continued.</param>
        /// <param name="user">The user that is currently logged in</param>
        [ExcludeFromCodeCoverage]
        public static void GameMenu(out bool menuRecall, IUser user)
        {
            var menuLogic = new MenuOptionLogic();

            Console.WriteLine();
            MessageDisplayUtilities.MenuMessageDisplay("**********************************************");
            Console.WriteLine();
            MessageDisplayUtilities.MenuMessageDisplay("Please make your selection: " + user.Username);
            MessageDisplayUtilities.MenuMessageDisplay("1) Rock");
            MessageDisplayUtilities.MenuMessageDisplay("2) Paper");
            MessageDisplayUtilities.MenuMessageDisplay("3) Scissors");
            MessageDisplayUtilities.MenuMessageDisplay("4) Current Score");
            Console.WriteLine();
            MessageDisplayUtilities.MenuMessageDisplay("**********************************************");

            menuRecall = menuLogic.PlayerVsAiChoiceLogic(user);
        }

        /// <summary>
        /// Takes the console readline input from the user after they were prompted, and confirms that it falls within the approved list of options, which is then returned if correct, or throws an error
        /// message if an invalid input was made.
        /// </summary>
        /// <returns>Returns the valid input, or a message stating that the input was invalid and listing the correct input options.</returns>
        public static int receiveMenuOptionOneThroughFive()
        {
            Console.Write("\r\nPlease select an option: ");
            var inputOptions = new List<string> { "1", "2", "3", "4", "5" };
            string input = Console.ReadLine();

            if (!CompareUserInputToAcceptedOptionsList(input, inputOptions))
            {
                MessageDisplayUtilities.NegativeMessageDisplay("The only valid inputs are 1-5, please try again.");

                return receiveMenuOptionOneThroughFive();
            }
            return ConvertStringToInt(input);
        }

        /// <summary>
        /// Takes the console readline input from the user after they were prompted, and confirms that it falls within the approved list of options, which is then returned if correct, or throws an error
        /// message if an invalid input was made.
        /// </summary>
        /// <returns>Returns the valid input, or a message stating that the input was invalid and listing the correct input options.</returns>
        public static int receiveMenuOptionOneThroughFour()
        {
            Console.Write("\r\nPlease select an option: ");
            var inputOptions = new List<string> { "1", "2", "3", "4" };
            string input = Console.ReadLine();

            if (!CompareUserInputToAcceptedOptionsList(input, inputOptions))
            {
                MessageDisplayUtilities.NegativeMessageDisplay("The only valid inputs are 1-4, please try again.");

                return receiveMenuOptionOneThroughFour();
            }
            return ConvertStringToInt(input);
        }

        /// <summary>
        /// Takes the console readline input from the user after they were prompted, and confirms that it falls within the approved list of options, which is then returned if correct, or throws an error
        /// message if an invalid input was made.
        /// </summary>
        /// <returns>Returns the valid input, or a message stating that the input was invalid and listing the correct input options.</returns>
        public static int receiveMenuOptionOneOrTwo()
        {
            Console.Write("\r\nPlease select an option: ");
            var inputOptions = new List<string> { "1", "2" };
            string input = Console.ReadLine();

            if (!CompareUserInputToAcceptedOptionsList(input, inputOptions))
            {
                MessageDisplayUtilities.NegativeMessageDisplay("The only valid inputs are 1-2, please try again.");

                return receiveMenuOptionOneOrTwo();
            }
            return ConvertStringToInt(input);
        }

        /// <summary>
        /// Compares the users input options to a list of approved input, and returns that value if it is contained within the list.
        /// </summary>
        /// <param name="input">The input the user typed in via console.readline()</param>
        /// <param name="inputOptions">A list of type string which contains the accepted inputs allowed.</param>
        /// <returns></returns>
        public static bool CompareUserInputToAcceptedOptionsList(string input, List<string> inputOptions)
        {
            return inputOptions.Contains(input);
        }

        /// <summary>
        /// Converts the user input from console.readling() from string to int format.
        /// </summary>
        /// <param name="input">The input the user typed in via console.readline()</param>
        /// <returns></returns>
        public static int ConvertStringToInt(string input)
        {
            return Convert.ToInt32(input);
        }

        #endregion
    }
}

