using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Value_Objects
{
    public class PlayerEvents : IPresentablePlayerEvents
    {
        private Guid playerId;
        private Guid teamId;
        private Guid seriesId;
       
        public string PlayerName
        {
            get
            {
                return DomainService.FindPlayerById(this.playerId).Name.ToString();
            }
        }

        public string TeamName
        {
            get
            {
                return DomainService.FindTeamById(this.teamId).Name.ToString();
            }
        }
        public IEnumerable<Game> Games
        {
            get
            {
                return DomainService.GetAllGames().Where(game => game.HomeTeamId == this.teamId || game.AwayTeamId == this.teamId).ToList();
            }
        }

        public IEnumerable<Goal> Goals
        {
            get
            {
                var allGames = DomainService.GetAllGames();
                return 
                    from game in allGames
                    from goal in game.Protocol.Goals
                    where goal.PlayerId == this.playerId
                    select goal;
            }
        }

        public IEnumerable<Assist> Assists
        {
            get
            {
                var allGames = DomainService.GetAllGames();
                return 
                    from game in allGames
                    from assist in game.Protocol.Assists
                    where assist.PlayerId == this.playerId
                    select assist;
            }
        }
        public IEnumerable<Card> Cards
        {
            get
            {
                var allGames = DomainService.GetAllGames();
                return 
                    from game in allGames
                    from card in game.Protocol.Cards
                    where card.PlayerId == this.playerId
                    select card;
            }
        }
        public IEnumerable<Penalty> Penalties
        {
            get
            {
                var allGames = DomainService.GetAllGames();
                return 
                    from game in allGames
                    from penalties in game.Protocol.Penalties
                    where penalties.PlayerId == this.playerId
                    select penalties;
            }
        }

        public PlayerEvents(Guid playerId, Guid teamId, Guid seriesId)
        {
           
            this.playerId = playerId;
            this.teamId = teamId;
            this.seriesId = seriesId;
        }
    }

}