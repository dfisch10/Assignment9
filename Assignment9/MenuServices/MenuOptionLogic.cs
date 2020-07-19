using System;
using System.Diagnostics.CodeAnalysis;

namespace Assignment9
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MenuOptionLogic
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginSelection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ScoreCardAccounts(out IUser user)
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
                            
                            return ScoreCardAccounts(out user);
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
                       
                        return ScoreCardAccounts(out user);
                    }
                    var existingUser = DataAccess.Instance.GetSingleUser(username);

                    currentSessionUser.Username = existingUser.Username;

                    user = currentSessionUser;
                    return false;
                case 3:
                    var leaderboardCount = 0;
                    var leaderboardList = DataAccess.Instance.GetUsers();

                    leaderboardList.Sort((pair1, pair2) => pair1.WinPercentage.CompareTo(pair2.WinPercentage));
                    leaderboardList.Reverse();

                    MessageDisplayUtilities.ScoreboardMessageDisplay("LEADERBOARD:\n");

                    foreach (var rankedUsers in leaderboardList)
                    {    
                        if (leaderboardCount < 5)
                        {
                            MessageDisplayUtilities.ScoreboardMessageDisplay(rankedUsers.Username + " - Win Rate: " + rankedUsers.WinPercentage + "%, Player Score: " + rankedUsers.Wins + ", AI Score: " + rankedUsers.Losses + ", Tied Games: " + rankedUsers.Draws);
                            leaderboardCount++;
                        }
                       
                    }
                    return ScoreCardAccounts(out user);
                case 4:
                    var allScoresList = DataAccess.Instance.GetUsers();

                    allScoresList.Sort((pair1, pair2) => pair1.WinPercentage.CompareTo(pair2.WinPercentage));
                    allScoresList.Reverse();

                    MessageDisplayUtilities.ScoreboardMessageDisplay("ALL USER SCOREBOARD:\n");

                    foreach (var rankedUsers in allScoresList)
                    {
                        MessageDisplayUtilities.ScoreboardMessageDisplay(rankedUsers.Username + " - Win Rate: " + rankedUsers.WinPercentage + "%, Player Score: " + rankedUsers.Wins + ", AI Score: " + rankedUsers.Losses + ", Tied Games: " + rankedUsers.Draws);                  
                    }

                    MessageDisplayUtilities.MenuMessageDisplay("Would you like to clear all scores from the database?");
                    MessageDisplayUtilities.MenuMessageDisplay("1) Yes");
                    MessageDisplayUtilities.MenuMessageDisplay("2) No");

                    var clearScoreResponse = Console.ReadLine();

                    if (clearScoreResponse == "1")
                    {
                        DataAccess.Instance.EraseAllUserData();
                        MessageDisplayUtilities.NegativeMessageDisplay("All User data has been deleted, I hope you are happy with yourself!");
                    }
                    else if (clearScoreResponse == "2")
                    {
                        MessageDisplayUtilities.PositiveMessageDisplay("No user data was harmed in the making of this decision.");
                    }
                    else
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("That is not a valid entry, only option '1' and '2' are accepted.");
                    }               

                    return ScoreCardAccounts(out user);
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
        /// 
        /// </summary>
        /// <param name="menuSelection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameModeSelection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool GameModes(IUser user)
        {
            var option = MenuUtilities.receiveMenuOptionOneThroughFive();
            var menuRecall = true;
            var tempScoreHolder = new User
            {
                Username = user.Username
            };

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
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user, tempScoreHolder);
                    }
                    else if (user.Losses == 1)
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("AI Won The Sudden Death");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user, tempScoreHolder);
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
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user, tempScoreHolder);
                    }
                    else if (user.Losses == 2)
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("AI Won The Triple Threat");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user, tempScoreHolder);
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
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user, tempScoreHolder);
                    }
                    else if (user.Losses == 3)
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("AI Won The Drive For Five");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user, tempScoreHolder);
                    }

                    break;
                case 4:
                    while ((user.Wins < 6 && user.Losses < 6) || (user.Wins == 5 && user.Losses == 5))
                    {
                        MenuUtilities.GameMenu(out menuRecall, user);
                    }

                    if (user.Wins == 6)
                    {
                        MessageDisplayUtilities.PositiveMessageDisplay("Player Won The Ten Tour!");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user, tempScoreHolder);
                    }
                    else if (user.Losses == 6)
                    {
                        MessageDisplayUtilities.NegativeMessageDisplay("AI Won The Ten Tour");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user, tempScoreHolder);
                    }
                    else if (user.Wins == 5 && user.Losses == 5)
                    {
                        MessageDisplayUtilities.MenuMessageDisplay("You and the AI have tied for The Ten Tour");
                        SessionControl.Session.UpdateDataBaseAndClearSessionScores(user, tempScoreHolder);
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

        #endregion
    }
}

