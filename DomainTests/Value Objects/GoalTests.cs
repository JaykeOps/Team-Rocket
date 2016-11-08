using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class GoalTests
    {
        private MatchMinute matchMinute = new MatchMinute(11);

        private Player player = new Player(new Name("John", "Doe"), new DateOfBirth("1975-04-18"),
            PlayerPosition.Defender, PlayerStatus.Available, new ShirtNumber(88));

        [TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        public void GoalCanBeAssignedMatchMinute()
        {
            var goal = new Goal(new MatchMinute(11), player);
            Assert.IsTrue(matchMinute.Value == 11);
        }
    }
}