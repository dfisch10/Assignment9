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

            Assert.Equal(actual.ToString(), expectedResult.ToString());
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

            Assert.Equal(sut.ToString(), DataAccess.Instance.GetSingleUser(sut.Username).ToString());
        }

        public static IEnumerable<object[]> UserData2 => new List<object[]>
        {
            new object[] {"test", 1, 1, 1 },
            new object[] { "n@me", 154, 101, 15 },
            new object[] { "testing", null, null, null },
            new object[] { "",1, 1, 1 },
            new object[] { "dAnIel", 0, 0, 0 },
            new object[] { "LUCAS", null, null, 1 },
            new object[] { "test@in9", 54, 23, null }
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
            var sut = new User();
            DataAccess.Instance.AddUser(user);
            DataAccess.Instance.UpdateUser(user);

            foreach (var item in DataAccess.Instance.GetUsers())
            {
                if(item.Username == expectedResult.Username)
                {
                    sut = item;
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
            new object[] { new User { Username = "", Wins = 1, Losses = 2, Draws = 1}, new User { Username = "", Wins = 2, Losses = 3, Draws = 2} },
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

    }
}
