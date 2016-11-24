using Domain.Entities;
using System.Collections.Generic;
using System.IO;
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
            this.LoadData();
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
                
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
    }
}