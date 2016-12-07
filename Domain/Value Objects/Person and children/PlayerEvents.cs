using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public class PlayerEvents
    {
        private readonly Guid playerId;
        private readonly Guid teamId;
        private readonly Guid seriesId;
        private string playerName;
        private string teamName;
        private IEnumerable<Game> games;
        private IEnumerable<Goal> goals;
        private IEnumerable<Assist> assists;
        private IEnumerable<Card> cards;
        private IEnumerable<Penalty> penalties;

        public PlayerEvents(PlayerEvents copy)
        {
        }

        public string PlayerName
        {
            get { return this.playerName; }
        }

        public string TeamName
        {
            get { return this.teamName; }
        }

        public IEnumerable<Game> Games
        {
            get { return this.games; }
        }

        public IEnumerable<Goal> Goals
        {
            get { return this.goals; }
        }

        public IEnumerable<Assist> Assists
        {
            get { return this.assists; }
        }

        public IEnumerable<Card> Cards
        {
            get { return this.cards; }
        }

        public IEnumerable<Penalty> Penalties
        {
            get { return this.penalties; }
        }

        private void UpdatePlayerName()
        {
            this.playerName = DomainService.FindPlayerById(this.playerId).Name.ToString();
        }

        private void UpdateTeamName()
        {
            this.teamName = DomainService.FindTeamById(this.teamId).Name.ToString();
        }

        private void UpdateGames()
        {
            this.games = DomainService.GetPlayerPlayedGamesInSeries(this.playerId, this.seriesId);
        }

        private void UpdateGoals()
        {
            this.goals = DomainService.GetPlayersGoalsInSeries(this.playerId, this.seriesId);
        }

        private void UpdateAssists()
        {
            this.assists = DomainService.GetPlayerAssistInSeries(this.playerId, this.seriesId);
        }

        private void UpdateCards()
        {
            this.cards = DomainService.GetPlayerCardsInSeries(this.playerId, this.seriesId);
        }

        private void UpdatePenalties()
        {
            this.penalties = DomainService.GetPlayerPenaltiesInSeries(this.playerId, this.seriesId);
        }

        public void UpdateAllEvents()
        {
            this.UpdatePlayerName();
            this.UpdateTeamName();
            this.UpdateGames();
            this.UpdateGoals();
            this.UpdateAssists();
            this.UpdateCards();
            this.UpdatePenalties();
        }

        public PlayerEvents(Guid seriesId, Guid teamId, Guid playerId)
        {
            this.playerId = playerId;
            this.teamId = teamId;
            this.seriesId = seriesId;
            this.playerName = string.Empty;
            this.teamName = string.Empty;
            this.games = new List<Game>();
            this.goals = new List<Goal>();
            this.assists = new List<Assist>();
            this.cards = new List<Card>();
            this.penalties = new List<Penalty>();
        }
    }
}