using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Repositories
{
    internal sealed class PlayerRepository
    {
        private HashSet<Player> players;
        public static readonly PlayerRepository instance = new PlayerRepository();
        private IFormatter formatter;
        private readonly string filePath;

        private PlayerRepository()
        {
            this.players = new HashSet<Player>();
            this.formatter = new BinaryFormatter();
            this.filePath = @"players.bin";
            this.LoadData();
        }

        internal void Add(Player newPlayer)
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
            this.SaveData();
        }

        internal IEnumerable<Player> GetAll()
        {
            return this.players;
        }

        internal void SaveData()
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
            catch (IOException)
            {
                bool checkFile = true;
                while (checkFile)
                {
                    if (IsFileReady(this.filePath))
                    {
                        using (
                            var streamWriter = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            this.formatter.Serialize(streamWriter, this.players);
                        }
                        checkFile = false;
                    }
                }
            }
        }

        private void LoadData()
        {
            var players = new HashSet<Player>();

            try
            {
                using (
                    var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
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

        internal void RemovePlayer(Guid playerId)
        {
            Player playerToRemove;

            if (this.TryGetPlayer(DomainService.FindPlayerById(playerId), out playerToRemove))
            {
                this.players.Remove(playerToRemove);
                this.SaveData();
            }
            else
            {
                throw new ArgumentException("Player doesn't exist.");
            }
        }

        private Player FindById(Guid playerId)
        {
            return this.players.FirstOrDefault(x => x.Id == playerId);
        }

        private static bool IsFileReady(string sFilename)
        {
            try
            {
                using (FileStream inputStream = File.Open(sFilename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    if (inputStream.Length > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}