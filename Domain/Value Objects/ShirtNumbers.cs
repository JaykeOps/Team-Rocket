using Domain.Entities;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Value_Objects
{
    public class ShirtNumbers : ValueObject<ShirtNumbers>
    {
        private Team Team { get; }
        private bool[] isAvailableShirtNumber;

        public ShirtNumber this[int number]
        {
            get
            {
                this.UpdateIsAvailableShirtNumber();
                if (this.isAvailableShirtNumber[number])
                {
                    this.isAvailableShirtNumber[number] = false;
                    return new ShirtNumber(this.Team.Id, number);
                }
                else
                {
                    return null;
                }
            }
        }

        private void UpdateIsAvailableShirtNumber()
        {
            var playerService = new PlayerService();
            var players = playerService.GetAll().ToList();
            var playersInTeam = new List<Player>();
            foreach (var playerId in this.Team.PlayerIds)
            {
                playersInTeam.Add(players.Find(x => x.TeamId.Equals(this.Team.Id)));
            }
            foreach (var player in playersInTeam)
            {
                this.isAvailableShirtNumber[player.ShirtNumber.Value] = false;
            }
        }

        public ShirtNumbers(Team team)
        {
            this.Team = team;
            this.isAvailableShirtNumber = new bool[99];
            for (int i = 0; i < 99; i++)
            {
                this.isAvailableShirtNumber[i] = true;
            }
        }
    }
}