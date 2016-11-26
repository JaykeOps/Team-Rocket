using System;
using Domain.CustomExceptions;
using Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Repositories
{
    public sealed class GameRepository
    {
        private HashSet<Game> games;
        public static readonly GameRepository instance = new GameRepository();
        private IFormatter formatter;
        private string filePath;

        private GameRepository()
        {
            this.formatter = new BinaryFormatter();
            this.filePath = @"..//..//games.bin";
            this.games = new HashSet<Game>();
            
        }

        public void SaveData()
        {
            try
            {
                using (var streamWriter = new FileStream(this.filePath, FileMode.Create,
                    FileAccess.Write, FileShare.None))
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
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public void LoadData()
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
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public void Add(Game newGame)
        {
            Game gameInRepo;
            if (this.TryGetGame(newGame, out gameInRepo))
            {
                this.games.Remove(gameInRepo);
                newGame.Protocol.UpdateGameResult();
                this.games.Add(newGame);
            }
            else
            {
                newGame.Protocol.UpdateGameResult();
                this.games.Add(newGame);
            }
        }

        private bool TryGetGame(Game newGame, out Game gameInRepo)
        {
            gameInRepo = this.FindById(newGame.Id);
            return gameInRepo != null;
        }

        private Game FindById(Guid gameId)
        {
            return this.games.FirstOrDefault(x => x.Id == gameId);
        }

        public IEnumerable<Game> GetAll()
        {
            return this.games;
        }
    }
}