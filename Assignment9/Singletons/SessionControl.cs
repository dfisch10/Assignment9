using System.Diagnostics.CodeAnalysis;

namespace Assignment9
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SessionControl
    {
        #region Fields

        private static readonly SessionControl _session = new SessionControl();

        public User user = new User();

        #endregion

        #region Properties

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        private SessionControl()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void ResetSessionScore()
        {
            user.Wins = 0;
            user.Losses = 0;
            user.Draws = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tempScoreHolder"></param>
        [ExcludeFromCodeCoverage]
        public void UpdateDataBaseAndClearSessionScores(IUser user)
        {
            DataAccess.Instance.UpdateUser(user);
            SessionControl.Session.ResetSessionScore();
        }

        #endregion
    }
}
