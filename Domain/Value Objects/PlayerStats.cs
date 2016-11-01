using Domain.Value_Objects;
using System.Collections.Generic;

namespace DomainTests.Entities
{
    public class PlayerStats
    {
        public List<Goal> Goals { get; }
        public List<Assist> Assists { get; }
        public List<Card> Cards { get; }
        public List<Penalty> Penalties { get; }

        public PlayerStats()
        {
            this.Goals = new List<Goal>();
            this.Assists = new List<Assist>();
            this.Cards = new List<Card>();
            this.Penalties = new List<Penalty>();
        }
    }
}