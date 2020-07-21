using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assignment9
{
    /// <summary>
    /// This method contains the various switch method logic for the various game menus, such as the: login menu, game mode menu, and the game menu. As well as various supporting methods needed
    /// to carry out the logic.  
    /// </summary>
    public class MenuOptionLogic
    {
        #region Methods

        /// <summary>
        /// Carries out various function relating to the creation/loading of user's info, as well as the accessing and display of scoreboards based off of the input from the user.
        /// </summary>
        /// <param name="user">The currently logged in user</param>
        /// <returns>Returns true or false depending on the case.</returns>
        [ExcludeFromCodeCoverage]
        public bool LoginMenuLogic(out IUser user)
        {
            var option = MenuUtilities.receiveMenuOptionOneThroughFive();
            var currentSessionUser = SessionControl.Session.user;

            switch (option)
            {
                case 1:
                    Console.Write("Please enter a new username: ");
                    var username = Console.ReadLine().ToUpper();
                    foreach (var item in DataAccess.Instance.GetUsers())
                    {
                        if (item.Username == username)
                        {
                            MessageDisplayUtilities.NegativeMessageDisplay("This name has already been selected, please select a new name, or login as an existing user.");
                            return LoginMenuLogic(out user);
                        }
                    }
                    currentSessionUser.Username = username;
                    MessageDisplayUtilities.CurrentStatsMessageDisplay("\nYour username '" + currentSessionUser.Username + "', has been created!!");
                    user = currentSessionUser;

                    return false;
                case 2:

                    Console.WriteLine("Please enter your existing username.");
                    username = Console.ReadLine().ToUpper();

                    var countEntries = 0;

                    foreach (var item in DataAccess.Instance.GetUsers())
                    {
                        if (item.Username == username)
                        {
                            countEntries++;
                        }
                    }

                    if (countEntries == 0)
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("This username can not be found, please try again, or enter it as a new user.");
                        return LoginMenuLogic(out user);
                    }
                    var existingUser = DataAccess.Instance.GetSingleUser(username);
                    currentSessionUser.Username = existingUser.Username;
                    user = currentSessionUser;

                    return false;
                case 3:
                    TopFiveLeaderBoard();
                    return LoginMenuLogic(out user);
                case 4:
                    var allScoresList = DataAccess.Instance.GetUsers();
                    SortAndOrganizeScores(allScoresList);

                    MessageDisplayUtilities.ScoreboardMessageDisplay("ALL USER SCOREBOARD:\n");
                    foreach (var rankedUsers in allScoresList)
                    {
                        MessageDisplayUtilities.ScoreboardMessageDisplay(rankedUsers.Username + " - Win Rate: " + rankedUsers.WinPercentage + "%, Player Score: " + rankedUsers.Wins + ", AI Score: " + rankedUsers.Losses + ", Tied Games: " + rankedUsers.Draws);
                    }
                    MessageDisplayUtilities.MenuMessageDisplay("Would you like to clear all scores from the database?");
                    MessageDisplayUtilities.MenuMessageDisplay("1) Yes");
                    MessageDisplayUtilities.MenuMessageDisplay("2) No");
                    var clearScoreResponse = MenuUtilities.receiveMenuOptionOneOrTwo();

                    if (clearScoreResponse == 1)
                    {
                        DataAccess.Instance.EraseAllUserData();
                        MessageDisplayUtilities.NegativeMessageDisplay("All User data has been deleted, I hope you are happy with yourself!");
                    }
                    else if (clearScoreResponse == 2)
                    {
                        MessageDisplayUtilities.PositiveMessageDisplay("No user data was harmed in the making of this decision.");
                    }
                    
                    return LoginMenuLogic(out user);
                case 5:
                    user = null;
                    currentSessionUser = SessionControl.Session.user;

                    Console.WriteLine("Your current session win/loss/draw streak was: Wins: " + currentSessionUser.Wins + ", Losses: " + currentSessionUser.Losses + ", Draws: " + currentSessionUser.Draws);
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);

                    return false;
            }
            user = null;
            return true;
        }

        /// <summary>
        /// Pulls the list of users from the database, sorts them by their win percentages, and returns only the top 5 results.
        /// </summary>
        /// <returns>Returns the top 5 score results</returns>
        public List<IUser> TopFiveLeaderBoard()
        {
            var leaderboardCount = 0;
            var leaderboardList = DataAccess.Instance.GetUsers();
            var topFiveResult = new List<IUser>();
            SortAndOrganizeScores(leaderboardList);

            MessageDisplayUtilities.ScoreboardMessageDisplay("LEADERBOARD:\n");
            foreach (var rankedUsers in leaderboardList)
            {
                if (leaderboardCount < 5)
                {
                    MessageDisplayUtilities.ScoreboardMessageDisplay(rankedUsers.Username + " - Win Rate: " + rankedUsers.WinPercentage + "%, Player Score: " + rankedUsers.Wins + ", AI Score: " + rankedUsers.Losses + ", Tied Games: " + rankedUsers.Draws);
                    topFiveResult.Add(rankedUsers);
                    leaderboardCount++;
                }
            }
            return topFiveResult;
        }

        /// <summary>
        /// This method sorts and reverses a list of type "User" in order to display them in descending order.
        /// </summary>
        /// <param name="allScoresList">a list of type User</param>
        public List<User> SortAndOrganizeScores(List<User> allScoresList)
        {
            allScoresList.Sort((pair1, pair2) => pair1.WinPercentage.CompareTo(pair2.WinPercentage));
            allScoresList.Reverse();
            return allScoresList;
        }

        /// <summary>
        /// Game mode logic for the Game Mode Menu which dictates the mode winning/losing parameters for each mode, as well as the logic to enter/start whichever game mode the user specified
        /// based on their input.
        /// </summary>
        /// <param name="user">The currently logged in user</param>
        /// <returns>Returns true or false depending on the case.</returns>
        [ExcludeFromCodeCoverage]
        public bool GameModes(IUser user)
        {
            var option = MenuUtilities.receiveMenuOptionOneThroughFive();
            var menuRecall = true;

            switch (option)
            {
                case 1:
                    while (user.Wins < 1 && user.Losses < 1)
                    {
                        MenuUtilities.GameMenu(out menuRecall, user);
                    }

                    if (user.Wins == 1)
                    {
                        MessageDisplayUtilities.PositiveMessageDisplay("Player Won The Suddent Death!");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user);
                    }
                    else if (user.Losses == 1)
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("AI Won The Sudden Death");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user);
                    }

                    break;
                case 2:
                    while (user.Wins < 2 && user.Losses < 2)
                    {
                        MenuUtilities.GameMenu(out menuRecall, user);
                    }

                    if (user.Wins == 2)
                    {
                        MessageDisplayUtilities.PositiveMessageDisplay("Player Won The Triple Threat!");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user);
                    }
                    else if (user.Losses == 2)
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("AI Won The Triple Threat");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user);
                    }

                    break;
                case 3:
                    while (user.Wins < 3 && user.Losses < 3)
                    {
                        MenuUtilities.GameMenu(out menuRecall, user);
                    }

                    if (user.Wins == 3)
                    {
                        MessageDisplayUtilities.PositiveMessageDisplay("Player Won The Drive For Five!");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user);
                    }
                    else if (user.Losses == 3)
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("AI Won The Drive For Five");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user);
                    }

                    break;
                case 4:
                    while ((user.Wins < 6 && user.Losses < 6) || (user.Wins == 5 && user.Losses == 5))
                    {
                        MenuUtilities.GameMenu(out menuRecall, user);
                    }

                    if (user.Wins == 5 && user.Losses == 5)
                    {
                        MessageDisplayUtilities.MenuMessageDisplay("You and the AI have tied for The Ten Tour");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user);
                    }
                    else if(user.Wins == 6)
                    {
                        MessageDisplayUtilities.PositiveMessageDisplay("Player Won The Ten Tour!");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user);
                    }
                    else if (user.Losses == 6)
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("AI Won The Ten Tour");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user);
                    }
                    

                    break;
                case 5:
                    return false;
                default:
                    Console.WriteLine("You did not enter a valid entry, please try again using options 1-5.");
                    break;
            }
            return true;
        }

        /// <summary>
        /// Carries out various functions relating to the game menu. Dependent on if the user chose rock, paper, or scissors it will compare that choice against a randomly selected AI choice
        /// in order to determine the winner, at which point it will increment the appropriate wins/losses/draws properties for the user session based on the result. You also have the option 
        /// to view your current score/stats for the current match/mode you are playing.
        /// </summary>
        /// <param name="user">The currently logged in user</param>
        /// <returns>Returns true or false depending on the case.</returns>
        [ExcludeFromCodeCoverage]
        public bool PlayerVsAiChoiceLogic(IUser user)
        {
            var aiChoice = "";
            var randomNumber = new Random();
            var randomChoice = randomNumber.Next(1, 4);
            var option = MenuUtilities.receiveMenuOptionOneThroughFour();

            switch (option)
            {
                case 1:
                case 2:
                case 3:
                    switch (randomChoice)
                    {
                        case 1:
                            aiChoice = "ROCK";
                            Console.WriteLine("Computer chose {0}", aiChoice);
                            if (option == 1)
                            {
                                user.Draws++;
                                Console.WriteLine("TIE!!\n");
                            }
                            else if (option == 2)
                            {
                                user.Wins++;                             
                                MessageDisplayUtilities.PositiveMessageDisplay("You Win!!!\n");                             
                            }
                            else if (option == 3)
                            {
                                user.Losses++;
                                MessageDisplayUtilities.NegativeMessageDisplay("You Lose!\n");
                            }
                            break;
                        case 2:
                            aiChoice = "PAPER";
                            Console.WriteLine("Computer chose {0}", aiChoice);
                            if (option == 2)
                            {
                                user.Draws++;
                                Console.WriteLine("TIE!!\n");
                            }
                            else if (option == 3)
                            {
                                user.Wins++;
                                MessageDisplayUtilities.PositiveMessageDisplay("You Win!!!\n");
                            }
                            else if (option == 1)
                            {
                                user.Losses++;
                                MessageDisplayUtilities.NegativeMessageDisplay("You Lose!\n");
                            }
                            break;
                        case 3:
                            aiChoice = "SCISSORS";
                            Console.WriteLine("Computer chose {0}", aiChoice);
                            if (option == 3)
                            {
                                user.Draws++;
                                Console.WriteLine("TIE!!\n");
                            }
                            else if (option == 1)
                            {
                                user.Wins++;
                                MessageDisplayUtilities.PositiveMessageDisplay("You Win!!!\n");
                            }
                            else if (option == 2)
                            {
                                user.Losses++;                              
                                MessageDisplayUtilities.NegativeMessageDisplay("You Lose!\n");
                            }
                            break;
                    }
                    break;
                case 4:
                    DataAccess.Instance.UpdateUser(user);
                    MessageDisplayUtilities.CurrentStatsMessageDisplay("\nPlayer: " + user.Username + "-- Win Rate: " + user.WinPercentage  + "%, Player Score: " + user.Wins + ", AI Score: " + user.Losses + ", Tied Games: " + user.Draws);
                    break;
                default:
                    Console.WriteLine("You did not enter a valid entry, please try again using options 1-5.");
                    break;
            }
            return true;
        }

        #endregion
    }
}

