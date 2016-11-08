using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services.Tests
{
    [TestClass]
    public class PlayerServiceTests
    {
        private PlayerService playerService;
        private IEnumerable<Player> allPlayers;

        [TestInitialize]
        public void Init()
        {
            playerService = new PlayerService();
            allPlayers = playerService.GetAll();
        }

        [TestMethod]
        public void PlayerServiceCanFindPlayerFullName()
        {                  
            var expectedPlayerObj = playerService.FindPlayer("Zlatan Ibrahimovic", true).Select(x => x.Id).First();

            Assert.AreEqual(expectedPlayerObj, allPlayers.FirstOrDefault().Id);
        }

        [TestMethod]
        public void PlayerServiceCanFindPlayerCaseSensitive()
        {
            var expectedPlayerObj = playerService.FindPlayer("ZLatAn IBRaHimoVic", true).Select(x => x.Id).First();

            Assert.AreEqual(expectedPlayerObj, allPlayers.FirstOrDefault().Id);
        }

        [TestMethod]
        public void PlayerServiceCanFindPlayerPartOfFirstName()
        {
            var expectedPlayerId = playerService.FindPlayer("ZLat", true).Select(x => x.Id).First();

            Assert.AreEqual(expectedPlayerId, allPlayers.FirstOrDefault().Id);
        }

        [TestMethod]
        public void PlayerServiceCanFindPlayerPartOfLastName()
        {
            var expectedPlayerId = playerService.FindPlayer("Ibra", true).Select(x => x.Id).First();            
             
            Assert.AreEqual(expectedPlayerId, allPlayers.FirstOrDefault().Id);
        }

        [TestMethod]
        public void PlayerServiceCanFindPlayerPartOfFirstAndLastName()
        {
            var expectedPlayerId = playerService.FindPlayer("ZlAt Ibra", true).Select(x => x.Id).First();

            Assert.AreEqual(expectedPlayerId, allPlayers.FirstOrDefault().Id);
        }

        [TestMethod]

        public void GetAllPlayersThruPlayerService()
        {
            var getAllPlayers = playerService.GetAll();

            Assert.IsNotNull(getAllPlayers);
        }
    }        
}