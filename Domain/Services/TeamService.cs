using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;

namespace Domain.Services
{
    public class TeamService
    {
        public TeamService()
        {
            //Repository = TeamRepository.Instance;
        }

        public void AddTeam(Team team)
        {
            TeamRepository.Instance.Add(team);
        }

        public IEnumerable<Team> GetAll()
        {
            return TeamRepository.Instance.GetAll();
        }
    }
}