using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Repositories
{
    public sealed class MatchRepository
    {
        private HashSet<Match> matches;
        public static readonly MatchRepository instance = new MatchRepository();
        private IFormatter formatter;
        private string filePath;

        private MatchRepository()
        {
            this.matches = new HashSet<Match>(); ;
            this.formatter = new BinaryFormatter();
            this.filePath = @"..//..//matchs.bin";
            this.LoadData();
        }

        public void SaveData()
        {
            try
            {
                using (
                    var streamWriter = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    this.formatter.Serialize(streamWriter, this.matches);
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
            var matches = new HashSet<Match>();

            try
            {
                using (
                    var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.Read,
                        FileShare.Read))
                {
                    matches = (HashSet<Match>)this.formatter.Deserialize(streamReader);
                    this.matches = matches;
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

        public IEnumerable<Match> GetAll()
        {
            return this.matches;
        }

        public void AddMatch(Match newMatch)
        {
            Match matchInRepository;
            if (this.TryGetMatch(newMatch, out matchInRepository))
            {
                this.matches.Remove(matchInRepository);
                this.matches.Add(newMatch);
            }
            else
            {
                this.matches.Add(newMatch);
            }
            this.SaveData();
        }

        private bool TryGetMatch(Match match, out Match matchInRepository)
        {
            matchInRepository = this.FindById(match.Id);
            return matchInRepository != null;
        }

        private Match FindById(Guid matchId)
        {
            return this.matches.FirstOrDefault(x => x.Id == matchId);
        }
    }
}