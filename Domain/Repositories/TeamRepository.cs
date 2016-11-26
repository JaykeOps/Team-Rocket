using System;
using Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
            
        }

        public void Add(Team newTeam)
        {
            
            if (!this.IsExistingReference(newTeam))
            {
                Team teamInRepo;
                if (this.TryGetTeamWithDuplicateId(newTeam, out teamInRepo))
                {
                    this.teams.Remove(teamInRepo);
                    this.teams.Add(newTeam);
                }
                else
                {
                    this.teams.Add(newTeam);
                }
            }
            
            
        }

        private bool TryGetTeamWithDuplicateId(Team newTeam, out Team teamInRepo)
        {
            teamInRepo = this.FindById(newTeam.Id);
            return teamInRepo != null;
        }

        private Team FindById(Guid teamId)
        {
            return this.teams.FirstOrDefault(x => x.Id == teamId);
        }

        private bool IsExistingReference(Team team)
        {
            return this.teams.Contains(team);
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
    }
}