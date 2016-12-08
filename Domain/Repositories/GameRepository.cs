using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Repositories
{
    internal sealed class GameRepository
    {
        private HashSet<Game> games;
        public static readonly GameRepository instance = new GameRepository();
        private IFormatter formatter;
        private readonly string filePath;

        private GameRepository()
        {
            this.formatter = new BinaryFormatter();
            this.filePath = @"games.bin";
            this.games = new HashSet<Game>();
            this.LoadData();
        }

        internal void SaveData()
        {
            try
            {
                using (
                    var streamWriter = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    this.formatter.Serialize(streamWriter, this.games);
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
                            var stream = new FileStream(this.filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                        {
                            this.formatter.Serialize(stream, this.games);
                        }
                        checkFile = false;
                    }
                }
            }
        }

        private void LoadData()
        {
            try
            {
                var games = new HashSet<Game>();
                using (var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate,
                    FileAccess.Read, FileShare.Read))
                {
                    games = (HashSet<Game>)this.formatter.Deserialize(streamReader);
                    this.games = games;
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

        internal void Add(Game newGame)
        {
            Game gameInRepository;
            if (this.TryGetGame(newGame, out gameInRepository))
            {
                this.games.Remove(gameInRepository);
                newGame.Protocol.UpdateGameResult();
                this.games.Add(newGame);
            }
            else
            {
                newGame.Protocol.UpdateGameResult();
                this.games.Add(newGame);
            }
            this.SaveData();
        }

        private bool TryGetGame(Game game, out Game gameInRepository)
        {
            gameInRepository = this.FindById(game.Id);
            return gameInRepository != null;
        }

        private Game FindById(Guid gameId)
        {
            return this.games.FirstOrDefault(x => x.Id == gameId);
        }

        public void RemoveGame(Game game)
        {
            this.games.Remove(game);
            this.SaveData();
        }

        public IEnumerable<Game> GetAll()
        {
            foreach (var game in this.games)
            {
                game.Protocol.UpdateGameResult();
            }
            return this.games;
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