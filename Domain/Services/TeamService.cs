using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public class TeamService
    {
        private readonly TeamRepository repository = TeamRepository.Instance;

        public void AddTeam(Team team)
        {
           this.repository.Add(team);
        }

    }
}
