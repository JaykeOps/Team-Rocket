using System;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
            //Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan");

            var matches = DomainService.GetAllMatches();

            for (int i = 0; i < 144; i++)
            {
                this.games.Add(new Game(matches.ElementAt(i)));
            }

            var team1 = DomainService.FindTeamById(this.games.ElementAt(0).HomeTeamId);
            var team2 = DomainService.FindTeamById(this.games.ElementAt(0).AwayTeamId);
            var team3 = DomainService.FindTeamById(this.games.ElementAt(1).HomeTeamId);
            var team4 = DomainService.FindTeamById(this.games.ElementAt(1).AwayTeamId);
            var team5 = DomainService.FindTeamById(this.games.ElementAt(2).HomeTeamId);
            var team6 = DomainService.FindTeamById(this.games.ElementAt(2).AwayTeamId);
            var team7 = DomainService.FindTeamById(this.games.ElementAt(3).HomeTeamId);
            var team8 = DomainService.FindTeamById(this.games.ElementAt(3).AwayTeamId);



            var game1 = this.games.ElementAt(0);
            var game2 = this.games.ElementAt(1);
            var game3 = this.games.ElementAt(2);
            var game4 = this.games.ElementAt(3);
            var game5 = this.games.ElementAt(4);
            var game6 = this.games.ElementAt(5);

            game1.Protocol.Goals.Add(new Goal(new MatchMinute(14), game1.HomeTeamId,
                team1.Players.ElementAt(0).Id));
            game1.Protocol.Assists.Add(new Assist(new MatchMinute(14),
                team1.Players.ElementAt(0).Id));
            game1.Protocol.Goals.Add(new Goal(new MatchMinute(82), game1.HomeTeamId,
                team1.Players.ElementAt(0).Id));
            game1.Protocol.Assists.Add(new Assist(new MatchMinute(82),
                team1.Players.ElementAt(0).Id));
            game1.Protocol.Cards.Add(new Card(new MatchMinute(75), team1.Players.ElementAt(0).Id,
                CardType.Red));

            game1.Protocol.Goals.Add(new Goal(new MatchMinute(28), game1.HomeTeamId,
               team1.Players.ElementAt(1).Id));
            game1.Protocol.Assists.Add(new Assist(new MatchMinute(28),
                team1.Players.ElementAt(1).Id));
            game1.Protocol.Goals.Add(new Goal(new MatchMinute(86), game1.HomeTeamId,
                team1.Players.ElementAt(1).Id));
            game1.Protocol.Assists.Add(new Assist(new MatchMinute(12),
                team1.Players.ElementAt(1).Id));
            game1.Protocol.Cards.Add(new Card(new MatchMinute(62), team2.Players.ElementAt(1).Id,
                CardType.Red));

            game2.Protocol.Goals.Add(new Goal(new MatchMinute(14), game1.HomeTeamId,
               team2.Players.ElementAt(0).Id));
            game2.Protocol.Assists.Add(new Assist(new MatchMinute(14),
                team2.Players.ElementAt(0).Id));
            game2.Protocol.Goals.Add(new Goal(new MatchMinute(82), game1.HomeTeamId,
                team2.Players.ElementAt(0).Id));
            game2.Protocol.Assists.Add(new Assist(new MatchMinute(82),
                team1.Players.ElementAt(0).Id));
            game2.Protocol.Cards.Add(new Card(new MatchMinute(75), team2.Players.ElementAt(0).Id,
                CardType.Red));

            game2.Protocol.Goals.Add(new Goal(new MatchMinute(28), game1.HomeTeamId,
               team2.Players.ElementAt(1).Id));
            game2.Protocol.Assists.Add(new Assist(new MatchMinute(28),
                team2.Players.ElementAt(1).Id));
            game2.Protocol.Goals.Add(new Goal(new MatchMinute(86), game1.HomeTeamId,
                team2.Players.ElementAt(1).Id));
            game2.Protocol.Assists.Add(new Assist(new MatchMinute(12),
                team1.Players.ElementAt(1).Id));
            game2.Protocol.Cards.Add(new Card(new MatchMinute(62), team2.Players.ElementAt(1).Id,
                CardType.Red));

            game3.Protocol.Goals.Add(new Goal(new MatchMinute(14), game1.HomeTeamId,
               team3.Players.ElementAt(0).Id));
            game3.Protocol.Assists.Add(new Assist(new MatchMinute(14),
                team3.Players.ElementAt(0).Id));
            game3.Protocol.Goals.Add(new Goal(new MatchMinute(82), game1.HomeTeamId,
                team3.Players.ElementAt(0).Id));
            game3.Protocol.Assists.Add(new Assist(new MatchMinute(82),
                team3.Players.ElementAt(0).Id));
            game3.Protocol.Cards.Add(new Card(new MatchMinute(75), team3.Players.ElementAt(0).Id,
                CardType.Red));

            game3.Protocol.Goals.Add(new Goal(new MatchMinute(28), game1.HomeTeamId,
               team3.Players.ElementAt(1).Id));
            game3.Protocol.Assists.Add(new Assist(new MatchMinute(28),
                team3.Players.ElementAt(1).Id));
            game3.Protocol.Goals.Add(new Goal(new MatchMinute(86), game1.HomeTeamId,
                team3.Players.ElementAt(1).Id));
            game3.Protocol.Assists.Add(new Assist(new MatchMinute(12),
                team3.Players.ElementAt(1).Id));
            game3.Protocol.Cards.Add(new Card(new MatchMinute(62), team3.Players.ElementAt(1).Id,
                CardType.Red));

            game4.Protocol.Goals.Add(new Goal(new MatchMinute(14), game1.HomeTeamId,
              team4.Players.ElementAt(0).Id));
            game4.Protocol.Assists.Add(new Assist(new MatchMinute(14),
                team4.Players.ElementAt(0).Id));
            game4.Protocol.Goals.Add(new Goal(new MatchMinute(82), game1.HomeTeamId,
                team4.Players.ElementAt(0).Id));
            game4.Protocol.Assists.Add(new Assist(new MatchMinute(82),
                team4.Players.ElementAt(0).Id));
            game4.Protocol.Cards.Add(new Card(new MatchMinute(75), team3.Players.ElementAt(0).Id,
                CardType.Red));

            game4.Protocol.Goals.Add(new Goal(new MatchMinute(28), game1.HomeTeamId,
               team4.Players.ElementAt(1).Id));
            game4.Protocol.Assists.Add(new Assist(new MatchMinute(28),
                team4.Players.ElementAt(1).Id));
            game4.Protocol.Goals.Add(new Goal(new MatchMinute(86), game1.HomeTeamId,
                team4.Players.ElementAt(1).Id));
            game4.Protocol.Assists.Add(new Assist(new MatchMinute(12),
                team4.Players.ElementAt(1).Id));
            game4.Protocol.Cards.Add(new Card(new MatchMinute(62), team3.Players.ElementAt(1).Id,
                CardType.Red));

            game5.Protocol.Goals.Add(new Goal(new MatchMinute(10), game1.HomeTeamId,
               team4.Players.ElementAt(4).Id));
            game5.Protocol.Goals.Add(new Goal(new MatchMinute(15), game1.HomeTeamId,
               team4.Players.ElementAt(4).Id));
            game5.Protocol.Goals.Add(new Goal(new MatchMinute(20), game1.HomeTeamId,
               team4.Players.ElementAt(4).Id));
            game5.Protocol.Goals.Add(new Goal(new MatchMinute(25), game1.HomeTeamId,
               team4.Players.ElementAt(4).Id));
            game6.Protocol.Goals.Add(new Goal(new MatchMinute(10), game1.HomeTeamId,
               team4.Players.ElementAt(4).Id));
            game6.Protocol.Goals.Add(new Goal(new MatchMinute(15), game1.HomeTeamId,
               team4.Players.ElementAt(4).Id));
            game6.Protocol.Goals.Add(new Goal(new MatchMinute(20), game1.HomeTeamId,
               team4.Players.ElementAt(4).Id));
            game6.Protocol.Goals.Add(new Goal(new MatchMinute(25), game1.HomeTeamId,
               team4.Players.ElementAt(4).Id));
            game6.Protocol.Goals.Add(new Goal(new MatchMinute(30), game1.HomeTeamId,
               team4.Players.ElementAt(4).Id));


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

        public void Add(Game game)
        {
            if (this.IsAdded(game))
            {
                throw new GameAlreadyAddedException();
            }
            else
            {
                this.games.Add(game);
            }
        }

        public bool IsAdded(Game newGame)
        {
            var isAdded = false;

            foreach (var game in this.games)
            {
                if (newGame.Id == game.Id)
                {
                    isAdded = true;
                }
            }

            return isAdded;
        }

        public IEnumerable<Game> GetAll()
        {
            return this.games;
        }
    }
}