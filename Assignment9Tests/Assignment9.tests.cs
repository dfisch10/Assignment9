using AutoFixture.Xunit2;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity;
using Xunit;

namespace Assignment9.tests
{
    [ExcludeFromCodeCoverage]
    public class Assignment9Tests
    {
        [Theory]
        [MemberData(nameof(UserData))]
        public void User_VariousQuanitityOfNumbersInputted_ReturnsValid(User expectedResult, string username, int wins, int losses, int draws)
        {
            var container = new UnityContainer();
            container.RegisterType<IUser, User>();
            var sut = container.Resolve<User>();

            sut.Username = username;
            sut.Wins = wins;
            sut.Losses = losses;
            sut.Draws = draws;

            expectedResult = new User
            {
                Username = username,
                Wins = wins,
                Losses = losses,
                Draws = draws,
            };

            var actual = sut;

            Assert.Equal(actual.Username, expectedResult.Username);
            Assert.Equal(actual.WinPercentage, expectedResult.WinPercentage);
            Assert.Equal(actual.Wins, expectedResult.Wins);
            Assert.Equal(actual.Losses, expectedResult.Losses);
            Assert.Equal(actual.Draws, expectedResult.Draws);
        }

        public static IEnumerable<object[]> UserData => new List<object[]>
        {
            new object[] { new User { Username = "test", Wins = 1, Losses = 1, Draws = 1}, "test", 1, 1, 1 },
            new object[] { new User { Username = "n@me", Wins = 154, Losses = 101, Draws = 15 }, "n@me", 154, 101, 15 },
            new object[] { new User { Username = "testing", Wins = 0, Losses = 0, Draws = 0}, "testing", null, null, null },
            new object[] { new User { Username = "", Wins = 1, Losses = 1, Draws = 1}, "", 1, 1, 1 },
            new object[] { new User { Username = "dAnIel", Wins = 0, Losses = 0, Draws = 0}, "dAnIel", 0, 0, 0 },
            new object[] { new User { Username = "LUCAS", Wins = 0, Losses = 0, Draws = 1}, "LUCAS", null, null, 1 },
            new object[] { new User { Username = "test@in9", Wins = 54, Losses = 23, Draws = 0}, "test@in9", 54, 23, null }
        };

        [Theory]
        [MemberData(nameof(UserData2))]
        public void GetSingleUserAndAddUser_VariousUserInfoInputted_ReturnsValid(string username, int wins, int losses, int draws)
        {
            var sut = new User
            {
                Username = username,
                Wins = wins,
                Losses = losses,
                Draws = draws
            };

            DataAccess.Instance.AddUser(sut);

            Assert.Equal(sut.Username, DataAccess.Instance.GetSingleUser(sut.Username).Username);
            Assert.Equal(sut.WinPercentage, DataAccess.Instance.GetSingleUser(sut.Username).WinPercentage);
            Assert.Equal(sut.Wins, DataAccess.Instance.GetSingleUser(sut.Username).Wins);
            Assert.Equal(sut.Losses, DataAccess.Instance.GetSingleUser(sut.Username).Losses);
            Assert.Equal(sut.Draws, DataAccess.Instance.GetSingleUser(sut.Username).Draws);
        }

        public static IEnumerable<object[]> UserData2 => new List<object[]>
        {
            new object[] {"test", 1, 1, 1 },
            new object[] { "n@me", 154, 101, 15 },
            new object[] { "testing", null, null, null },
            new object[] { "",1, 1, 1 },
            new object[] { "dAnIel", 0, 0, 0 },
            new object[] { "LUCAS", null, null, 1 },
            new object[] { "test@in", 54, 23, null }
        };

        [Theory]
        [MemberData(nameof(Usernames))]
        public void CreateNewUserAndGetUsers_VariousUsernamesInputtedAndPassedThroughFunction_ReturnsValid(string username)
        {
            var actual = "";
            DataAccess.Instance.EraseAllUserData();
            DataAccess.Instance.CreateNewUser(username);
            foreach (var item in DataAccess.Instance.GetUsers())
            {
                    actual = item.Username;
            }

            Assert.Equal(actual, username);
        }

        public static IEnumerable<object[]> Usernames => new List<object[]>
        {
            new object[] {"test" },
            new object[] { "n@me" },
            new object[] { "testing" },
            new object[] { "" },
            new object[] { "dAnIel" },
            new object[] { "LUCAS" },
            new object[] { "test@in9" }
        };

        [Theory]
        [MemberData(nameof(UserData4))]
        public void UpdateUser_VariousScoresAddedToExistingUsers_ReturnsValid(IUser user, User expectedResult)
        {
 
            DataAccess.Instance.EraseAllUserData();
            var sut = new User();
            DataAccess.Instance.AddUser(user);
            DataAccess.Instance.UpdateUser(user);

            foreach (var item in DataAccess.Instance.GetUsers())
            {
                if(item.Username == expectedResult.Username)
                {
                    sut = (User)item;
                }
            }

            Assert.Equal(expectedResult.Wins, sut.Wins);
            Assert.Equal(expectedResult.Losses, sut.Losses);
            Assert.Equal(expectedResult.Draws, sut.Draws);
            Assert.Equal(expectedResult.WinPercentage, sut.WinPercentage);
        }

        public static IEnumerable<object[]> UserData4 => new List<object[]>
        {
            new object[] { new User { Username = "test", Wins = 1, Losses = 1, Draws = 1}, new User { Username = "test", Wins = 2, Losses = 2, Draws = 2} },
            new object[] { new User { Username = "n@me", Wins = 154, Losses = 101, Draws = 15 }, new User { Username = "n@me", Wins = 308, Losses = 202, Draws = 30 } },
            new object[] { new User { Username = "testing", Wins = 0, Losses = 0, Draws = 0}, new User { Username = "testing", Wins = 0, Losses = 0, Draws = 0} },
            new object[] { new User { Username = "", Wins = 1, Losses = 2, Draws = 1}, new User { Username = "", Wins = 2, Losses = 4, Draws = 2} },
            new object[] { new User { Username = "dAnIel", Wins = 0, Losses = 0, Draws = 0}, new User { Username = "dAnIel", Wins = 0, Losses = 0, Draws = 0} },
            new object[] { new User { Username = "LUCAS", Wins = 0, Losses = 0, Draws = 1}, new User { Username = "LUCAS", Wins = 0, Losses = 0, Draws = 2} },
            new object[] { new User { Username = null, Wins = 0, Losses = 0, Draws = 0}, new User { Username = null, Wins = 0, Losses = 0, Draws = 0} }
        };

        [Theory]
        [MemberData(nameof(UserData5))]
        public void ResetSessionScores_VariousScoresResetToZero_ReturnsValid(User user)
        {
            var currentSessionUser = SessionControl.Session.user;
            currentSessionUser.Wins = user.Wins;
            currentSessionUser.Losses = user.Losses;
            currentSessionUser.Draws = user.Draws;

            SessionControl.Session.ResetSessionScore();

            Assert.Equal(0, currentSessionUser.Wins);
            Assert.Equal(0, currentSessionUser.Losses);
            Assert.Equal(0, currentSessionUser.Draws);
        }

        public static IEnumerable<object[]> UserData5 => new List<object[]>
        {
            new object[] { new User { Username = "test", Wins = 1, Losses = 1, Draws = 1} },
            new object[] { new User { Username = "n@me", Wins = 154, Losses = 101, Draws = 15 } },
            new object[] { new User { Username = "testing", Wins = 0, Losses = 40, Draws = 0} },
            new object[] { new User { Username = "", Wins = 1, Losses = 2, Draws = 1} },
            new object[] { new User { Username = "dAnIel", Wins = 20, Losses = 0, Draws = 20} },
            new object[] { new User { Username = "LUCAS", Wins = 0, Losses = 0, Draws = 1} },
            new object[] { new User { Username = null, Wins = 10, Losses = 10, Draws = 10} }
        };

        [Fact]
        public void SortAndOrganizeScores_ComparingTwoUserBasedOffOfWinPercentage_ReturnsValid()
        {
            var result = 0;
            var result2 = 0;
            var list = DataAccess.Instance.GetUsers();
            var user1 = new User
            {
                Username = "user1",
                Wins = 10,
                Losses = 0,
                Draws = 0
            };
            var user2 = new User
            {
                Username = "user2",
                Wins = 5,
                Losses = 5,
                Draws = 0
            };
            var menuLogic = new MenuOptionLogic();
            var sut = menuLogic.SortAndOrganizeScores(list);

            foreach (var item in list)
            {
                result = item.WinPercentage;
            }

            foreach (var item in sut)
            {
                result2 = item.WinPercentage;
            }
            Assert.Equal(result2, result);
        }

        [Fact]
        public void TopFiveLeaderBoard_AddMoreThanFiveUsersToListToConfirmProperSortingAndDisplay_ReturnsValid()
        {
            DataAccess.Instance.EraseAllUserData();
            var actualList = DataAccess.Instance.GetUsers();
            List<User> controlList = new List<User>();
            var database = DataAccess.Instance;
            var sut = new MenuOptionLogic();

            var result1 = 0;
            var result2 = 0;

            // creating 6 users with various win rates
            var user1 = new User{ Username = "user1", Wins = 10, Losses = 0, Draws = 0 };
            var user2 = new User{ Username = "user2", Wins = 5, Losses = 5, Draws = 0 };
            var user3 = new User{ Username = "user3", Wins = 110, Losses = 20, Draws = 50 };
            var user4 = new User{ Username = "user4", Wins = 57, Losses = 15, Draws = 30 };
            var user5 = new User{ Username = "user5", Wins = 105, Losses = 120, Draws = 210 };
            var user6 = new User{ Username = "user6", Wins = 56, Losses = 51, Draws = 70 };

            //Adding users to database in improper order for method to be tested and return top five results
            database.AddUser(user1);
            database.AddUser(user2);
            database.AddUser(user3);
            database.AddUser(user4);
            database.AddUser(user5);
            database.AddUser(user6);

            foreach (var item in sut.TopFiveLeaderBoard())
            {
                result1 = item.WinPercentage;
            }

            //Adding Top 5 to list in proper order and adding to result2
            controlList.Add(user1);
            controlList.Add(user3);
            controlList.Add(user4);
            controlList.Add(user2);
            controlList.Add(user6);

            foreach (var item in controlList)
            {
                result2 = item.WinPercentage;
            }

            //test
            Assert.Equal(result1, result2);
        }
    }
}
