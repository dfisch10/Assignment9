using System;

namespace Assignment9
{
    /// <summary>
    /// This class creates an object of type "User" and implementing "IUser" to represent a user in the rock, paper, scissors game.
    /// </summary>
    public class User: IUser
    {
        /// <summary>
        /// Gets and Sets the Username of the "User" object to either be created in the database, or loaded from the database.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets and Sets the amount of "Wins" of the "User" object to either be created in the database, or loaded from the database.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Gets and Sets the amount of "Losses" of the "User" object to either be created in the database, or loaded from the database.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Gets and Sets the amount of "Draws" (tied games) of the "User" object to either be created in the database, or loaded from the database.
        /// </summary>
        public int Draws { get; set; }

        /// <summary>
        /// Gets and Sets the rate of wins the user has based on their wins, losses, and draws, in order to either be created in the database, or loaded from the database.
        /// </summary>
        public int WinPercentage
        {
            get
            {
                var val = Losses + Wins + Draws;

                if (val == 0)
                {
                    return 0;
                }               

                return (Wins* 100) / val;
            }
        }
        ///public AiLogic Ai { get; set; }
    }
}