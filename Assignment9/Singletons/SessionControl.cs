using System.Diagnostics.CodeAnalysis;

namespace Assignment9
{
    /// <summary>
    /// This class is a singleton that creates a session for a the currently logged in user and handles all methods that interacts with that session, including updating the database and 
    /// resetting the current session scores after each game mode ends.
    /// </summary>
    public sealed class SessionControl
    {
        #region Fields

        // <summary>
        /// Initializes a readonly new SessionControl '_session'
        /// </summary>
        private static readonly SessionControl _session = new SessionControl();

        /// <summary>
        /// Creates a new User object called "user" which serves as a session instance of the logged in user.
        /// </summary>
        public User user = new User();

        #endregion

        #region Properties

        /// <summary>
        /// Assigns the local _session to the class Session.
        /// </summary>
        public static SessionControl Session
        {
            get
            {
                return _session;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Session Control Constructor
        /// </summary>
        private SessionControl()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets the values for the session users wins, losses, and draws, back to zero. Meant to be used at the conclusion of the various game modes are concluded.
        /// </summary>
        public void ResetSessionScore()
        {
            user.Wins = 0;
            user.Losses = 0;
            user.Draws = 0;
        }

        /// <summary>
        /// This method updates the database scores for the currently logged in user to reflect the additional wins/losses/etc. from the current session instance, and then resets the current 
        /// session scores back to zero.
        /// </summary>
        /// <param name="user">The currently logged in user.</param>
        [ExcludeFromCodeCoverage]
        public void UpdateDataBaseAndClearSessionScores(IUser user)
        {
            DataAccess.Instance.UpdateUser(user);
            SessionControl.Session.ResetSessionScore();
        }

        #endregion
    }
}
