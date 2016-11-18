using Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Repositories
{
    public sealed class PlayerRepository
    {
        private List<Player> players;
        public static readonly PlayerRepository instance = new PlayerRepository();
        private IFormatter formatter;
        private string filePath;

        private PlayerRepository()
        {
            this.players = new List<Player>();
            this.formatter = new BinaryFormatter();
            this.filePath = @"..//..//players.bin";
            this.LoadData();
        }

        public void Add(Player player)
        {
            this.players.Add(player);
            
        }

        public IEnumerable<Player> GetAll()
        {
            return this.players;
        }

        public void SaveData()
        {
            try
            {
                using (
                    var streamWriter = new FileStream(this.filePath, FileMode.Create, 
                    FileAccess.Write, FileShare.None))
                {
                    this.formatter.Serialize(streamWriter, this.players);
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
            var players = new List<Player>();

            try
            {
                using (
                    var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.Read,
                        FileShare.Read))
                {
                    players = (List<Player>)this.formatter.Deserialize(streamReader);
                    this.players = players;
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