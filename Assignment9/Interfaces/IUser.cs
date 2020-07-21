using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment9
{
    /// <summary>
    /// This interface creates an object of type "IUser" which includes properties for that users wins, losses, draws, and winpercentage for whatever game it is implemented in.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets and Sets the Username of the "User" object to either be created in the database, or loaded from the database.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Gets and Sets the amount of "Wins" of the "User" object to either be created in the database, or loaded from the database.
        /// </summary>
        int Wins { get; set; }

        /// <summary>
        /// Gets and Sets the amount of "Losses" of the "User" object to either be created in the database, or loaded from the database.
        /// </summary>
        int Losses { get; set; }

        // <summary>
        /// Gets and Sets the amount of "Draws" (tied games) of the "User" object to either be created in the database, or loaded from the database.
        /// </summary>
        int Draws { get; set; }

        /// <summary>
        /// Gets and Sets the rate of wins the user has based on their wins, losses, and draws, in order to either be created in the database, or loaded from the database.
        /// </summary>
        int WinPercentage { get; }
    }
}
