using System;

namespace Assignment9
{
    /// <summary>
    /// 
    /// </summary>
    public class User: IUser
    {
        public string Username { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Draws { get; set; }

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