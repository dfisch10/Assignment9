using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assignment9
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MenuUtilities
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRecall"></param>
        /// <param name="loginSelection"></param>
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

            loginRecall = menuLogic.ScoreCardAccounts(out user);

            var gameModeRecall = true;

            while (gameModeRecall)
            {
                MenuUtilities.GameModeMenu(out gameModeRecall, user);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameModeRecall"></param>
        /// <param name="gameModeSelection"></param>
        /// <param name="user"></param>
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
        /// 
        /// </summary>
        /// <param name="menuRecall"></param>
        /// <param name="menuSelection"></param>
        /// <param name="user"></param>
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
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static int receiveMenuOptionOneThroughFour()
        {
            Console.Write("\r\nPlease select an option: ");

            var inputOptions = new List<string> { "1", "2", "3", "4" };

            string input = Console.ReadLine();

            if (!CompareUserInputToAcceptedOptionsList(input, inputOptions))
            {
                MessageDisplayUtilities.NegativeMessageDisplay("The only valid inputs are 1-4, please try again.");

                return receiveMenuOptionOneThroughFive();
            }
            return ConvertStringToInt(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputOptions"></param>
        /// <returns></returns>
        public static bool CompareUserInputToAcceptedOptionsList(string input, List<string> inputOptions)
        {
            return inputOptions.Contains(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ConvertStringToInt(string input)
        {
            return Convert.ToInt32(input);
        }

        #endregion
    }
}

