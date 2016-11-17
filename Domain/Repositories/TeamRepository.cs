using System;
using Domain.Entities;
using Domain.Value_Objects;
using System.Collections.Generic;
using System.Linq;
using Domain.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Domain.Repositories
{
    public sealed class TeamRepository
    {
        private List<Team> teams;
        public static readonly TeamRepository instance = new TeamRepository();
        private IFormatter formatter;
        private string filePath;

        private TeamRepository()
        {
            this.teams = new List<Team>();
            this.formatter = new BinaryFormatter();
            this.filePath = @"..//..//teams.bin";
            LoadData();
        }

        public void Add(Team team)
        {
            this.teams.Add(team);
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
                    formatter.Serialize(streamWriter, teams);
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
            var teams = new List<Team>();

            try
            {
                using (
                    var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.Read,
                        FileShare.Read))
                {
                    teams = (List<Team>)this.formatter.Deserialize(streamReader);
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