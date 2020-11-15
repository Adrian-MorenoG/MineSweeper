using MineSweeper.Game.User;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MineSweeper.Tests.UnitTests
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void SetNameTest()
        {
            string expected = "Melon";
            User user = new User();
            user.SetName();
            Assert.Equals(user.GetName(), expected);

            user.SetName();
            Assert.AreNotEqual(user.GetName(), expected);
            expected = "Melonazo";
            Assert.Equals(user.GetName(), expected);
        }

        [Test]
        public void MakeAPlayTest()
        {
            User user = new User();
            string[] plays =
            {
                "S 0 0",
                "F 0 1",
                "S 1 1",
                "S 2 2"
            };

            foreach (var p in plays)
            {
                Assert.Equals(user.MakeAPlay(), p);
            }
        }
    }
}