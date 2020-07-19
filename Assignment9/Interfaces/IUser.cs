using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment9
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUser
    {
        string Username { get; set; }

        int Wins { get; set; }

        int Losses { get; set; }

        int Draws { get; set; }

        int WinPercentage { get; }
    }
}
