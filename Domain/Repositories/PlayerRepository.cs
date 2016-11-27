using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Repositories
{
    public sealed class PlayerRepository
    {
        private HashSet<Player> players;
        public static readonly PlayerRepository instance = new PlayerRepository();
        private IFormatter formatter;
        private string filePath;

        private PlayerRepository()
        {
            this.players = new HashSet<Player>();
            this.formatter = new BinaryFormatter();
            this.filePath = @"..//..//players.bin";
        }

        public void Add(Player newPlayer)
        {
            Player playerInRepo;
            if (this.TryGetPlayer(newPlayer, out playerInRepo))
            {
                this.players.Remove(playerInRepo);
                this.players.Add(newPlayer);
            }
            else
            {
                this.players.Add(newPlayer);
            }
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
                    var streamWriter = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
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
            var players = new HashSet<Player>();

            try
            {
                using (
                    var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.Read,
                        FileShare.Read))
                {
                    players = (HashSet<Player>)this.formatter.Deserialize(streamReader);
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

        private bool TryGetPlayer(Player player, out Player repositoryPlayer)
        {
            repositoryPlayer = this.FindById(player.Id);
            return repositoryPlayer != null;
        }

        private Player FindById(Guid playerId)
        {
            return this.players.FirstOrDefault(x => x.Id == playerId);
        }
    }
}