using System;
using Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Domain.Services;

namespace Domain.Repositories
{
    public sealed class TeamRepository
    {
        private HashSet<Team> teams;
        public static readonly TeamRepository instance = new TeamRepository();
        private IFormatter formatter;
        private string filePath;

        private TeamRepository()
        {
            this.teams = new HashSet<Team>();
            this.formatter = new BinaryFormatter();
            this.filePath = @"..//..//teams.bin";
            LoadData();
        }

        public void Add(Team newTeam)
        {
            
            Team teamInRepository;
            if (this.TryGetTeam(newTeam, out teamInRepository))
            {
                this.teams.Remove(teamInRepository);
                this.teams.Add(newTeam);
            }
            else
            {
                this.teams.Add(newTeam);
            }
        }

        public IEnumerable<Team> GetAll()
        {
            return this.teams;
        }

        public void SaveData()
        {
            try
            {
                using (
                    var streamWriter = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    this.formatter.Serialize(streamWriter, this.teams);
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File missing at {this.filePath}." +
                                                "Failed to save data to file!");
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            catch (SerializationException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public void LoadData()
        {
            var teams = new HashSet<Team>();

            try
            {
                using (
                    var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.Read,
                        FileShare.Read))
                {
                    teams = (HashSet<Team>)this.formatter.Deserialize(streamReader);
                    this.teams = teams;
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File missing at {this.filePath}." +
                                                "Load failed!");
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            catch (SerializationException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public void RemoveTeam(Guid teamId)
        {
            Team playerToRemove;

            if (this.TryGetTeam(DomainService.FindTeamById(teamId), out playerToRemove))
            {
                this.teams.Remove(playerToRemove);
            }
            else
            {
                throw new ArgumentException("Team doesn't exist.");
            }
        }

        private bool TryGetTeam(Team team, out Team teamInRepository)
        {
            teamInRepository = this.FindById(team.Id);
            return teamInRepository != null;
        }

        private Team FindById(Guid teamId)
        {
            return this.teams.FirstOrDefault(x => x.Id == teamId);
        }
        
    }
}