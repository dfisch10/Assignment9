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

        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

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
        public void UpdateDataBaseAndClearSessionScores(IUser user, User tempScoreHolder)
        {
            tempScoreHolder.Wins += user.Wins;
            tempScoreHolder.Losses += user.Losses;

            DataAccess.Instance.UpdateUser(user);
            SessionControl.Session.ResetSessionScore();
        }

        #endregion
    }
}
