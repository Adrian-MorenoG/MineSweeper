using MineSweeper.Game.User;
using MineSweeper.Tests.Mocks;
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
            User user = new MockUser();
            user.SetName();
            Assert.AreEqual(user.GetName(), expected);

            user.SetName();
            Assert.AreNotEqual(user.GetName(), expected);
            expected = "Melonazo";
            Assert.AreEqual(user.GetName(), expected);
        }

        [Test]
        public void MakeAPlayTest()
        {
            User user = new MockUser();
            string[] plays =
            {
                "S 0 0",
                "F 0 1",
                "S 1 1",
                "S 2 2"
            };

            foreach (var p in plays)
            {
                Assert.AreEqual(user.MakeAPlay(), p);
            }
        }
    }
}