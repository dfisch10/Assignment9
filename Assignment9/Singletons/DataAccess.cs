using Castle.Core.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment9
{
    /// <summary>
    /// This class is a singleton that accesses the database and handles all methods that interacts with that database, including adding, erasing, updating, and pulling user information from the database for reference/logic.
    /// </summary>
    public sealed class DataAccess
    {
        #region Fields

        /// <summary>
        /// Initializes a readonly new DataAccess '_instance'
        /// </summary>
        private static readonly DataAccess _instance = new DataAccess();

        /// <summary>
        /// Creates a constant name 'FileName' which is the file path for all read/write purposes for the "database".
        /// </summary>
        private const string FileName = "RockPaperScissorsDB.txt";

        #endregion

        #region Properties

        /// <summary>
        /// Assigns the local instance to the class Instance.
        /// </summary>
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
        /// DataAccess constructor.
        /// </summary>
        private DataAccess()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method takes an object of type "IUser" and add it to the database.
        /// </summary>
        /// <param name="user">The object of type IUser that is attempting to be added to the database</param>
        public void AddUser(IUser user)
        {
            var users = GetUsers();
            var count = 0;

            if (users is null)
            {
                users = new List<User>();
            }

            for (var index = 0; index < users.Count; index++)
            {
                if (users[index]?.Username == user?.Username)
                {
                    count++;
                   // throw new ArgumentException("This username already exists in the database, please select a new username.");        
                }
            }
            if (count == 0)
            {
                users?.Add((User)user);
                var json = JsonConvert.SerializeObject(users);
                File.WriteAllText(FileName, json);
            }
        }

        /// <summary>
        /// This method creates a new User object and adds it to the database using the AddUser() method.
        /// </summary>
        /// <param name="newUser">The new user (username) which is attempting to be created/added to the database</param>
        /// <returns>returns the user that was created</returns>
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
        /// This method checks to see if the designated file exists, and if so, it pulls all the User information from the file and assembles it into a list of type "IUser".
        /// </summary>
        /// <returns>returns a list of IUser objects that exist in the database (file)</returns>
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
        /// Updates the scores of the user to add in their new wins/losses/draws to their existing scores after playing.
        /// </summary>
        /// <param name="user">The logged in User object that is currently accessed in the session of the game.</param>
        public void UpdateUser(IUser user)
        {
            var users = GetUsers();

            if(users is null)
            {
                users = new List<User>();
            }

            for (var index = 0; index < users?.Count; index++)
            {
                var item = users[index];
                if(item?.Username == user?.Username)
                {
                    //users[index] = user;
                    users[index].Wins += user.Wins;
                    users[index].Losses += user.Losses;
                    users[index].Draws += user.Draws;
                }
            }
            var json = JsonConvert.SerializeObject(users);
            File.WriteAllText(FileName, json);
        }

        /// <summary>
        /// Searches through the databases using the GetUsers() method, and then targets and returns the user from that list that contains the username specified.
        /// </summary>
        /// <param name="username">The username of the User object attempting to be retrieved</param>
        /// <returns>The User object associated with the specifed username</returns>
        public IUser GetSingleUser(string username)
        {
            var users = GetUsers();
            return users?.FirstOrDefault(m => m.Username == username);
        }      

        /// <summary>
        /// This deletes the database file containing all the user's information/scores/etc. 
        /// </summary>
        public void EraseAllUserData()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }        
        }

        #endregion
    }
}
