using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment9
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DataAccess
    {
        #region Fields

        private static readonly DataAccess _instance = new DataAccess();

        private const string FileName = "RockPaperScissorsDB.txt";

        #endregion

        #region Properties

        public static DataAccess Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        private DataAccess()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            if (!File.Exists(FileName))
            {
                File.WriteAllText(FileName, "[]");
            }

            var content = File.ReadAllText(FileName);

            var users = JsonConvert.DeserializeObject<List<User>>(content);

            return users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(IUser user)
        {
            var users = GetUsers();
            var count = 0;

            if(users is null)
            {
                users = new List<User>();
            }

            for(var index = 0; index < users.Count; index++)
            {
                if(users[index].Username == user.Username)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                users.Add((User)user);

                var json = JsonConvert.SerializeObject(users);

                File.WriteAllText(FileName, json);
            }                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(IUser user)
        {
            var users = GetUsers();

            if(users is null)
            {
                Console.WriteLine("You currently do not have a user selected, please create a user, or access an existing user, and try again.");
            }

            for (var index = 0; index < users.Count; index++)
            {
                var item = users[index];
                if(item.Username == user.Username)
                {
                    users[index].Wins += user.Wins;
                    users[index].Losses += user.Losses;
                    users[index].Draws += user.Draws;
                }
            }

            var json = JsonConvert.SerializeObject(users);

            File.WriteAllText(FileName, json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IUser GetSingleUser(string username)
        {
            var users = GetUsers();

            return users?.FirstOrDefault(m => m.Username == username);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public IUser CreateNewUser(string newUser)
        {
            var user = new User
            {
                Username = newUser
            };

            DataAccess.Instance.AddUser(user);

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EraseAllUserData()
        {
            File.Delete(FileName);
        }

        #endregion
    }
}
