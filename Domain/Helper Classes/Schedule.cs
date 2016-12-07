using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Helper_Classes
{
    [Serializable]
    internal class Schedule
    {
        //Work in progress..

        private const int BYE = -1;

        private List<Match> AllMatchesDescending = new List<Match>();
        private List<Match> AllMatchesAscending = new List<Match>();
        private List<Match> AllMatches = new List<Match>();
        private List<Team> Teams = new List<Team>();
        private Dictionary<int, List<Match>> AllMatchesWithRounds = new Dictionary<int, List<Match>>();

        

        internal void GenerateSchedule(Series series)
        {
            int numberOfTeams = series.TeamIds.Count;

            if (numberOfTeams % 2 == 0)
            {
                foreach (var teamId in series.TeamIds)
                {
                    this.Teams.Add(DomainService.FindTeamById(teamId));
                }

                int[,] results = this.GenerateRoundRobin(numberOfTeams);

                //Generates the schedule for descending matches
                this.GenerateDescendingMatches(numberOfTeams, results, series);

                //Generates the schedule for ascending matches
                this.GenerateAscendingMatches(numberOfTeams, results, series);

                this.AllMatchesAscending.Reverse();
                this.AllMatches.AddRange(this.AllMatchesDescending);
                this.AllMatches.AddRange(this.AllMatchesAscending);

                //Generate dictionary with roundnumber as key(int) and matches for each round as value(List<Match>)
                this.GenerateRoundsWithMatches(numberOfTeams);
                series.Schedule = this.AllMatches;
            }
            else
            {
                throw new ArgumentException("Teams must be even numbers.");
            }
        }

        private void GenerateDescendingMatches(int numberOfTeams, int[,] results, Series series)
        {
            for (int round = 0; round <= results.GetUpperBound(1); round++)
            {
                for (int team = 0; team < numberOfTeams; team++)
                {
                    if (team < results[team, round] && round % 2 == 0)
                    {
                        Match match = new Match(this.Teams.ElementAt(team).ArenaName,
                            this.Teams.ElementAt(team).Id,
                            this.Teams.ElementAt(results[team, round]).Id,
                            series);

                        this.AllMatchesDescending.Add(match);
                    }
                    else if (team < results[team, round] && round % 2 == 1)
                    {
                        Match match = new Match(this.Teams.ElementAt(results[team, round]).ArenaName,
                            this.Teams.ElementAt(results[team, round]).Id,
                            this.Teams.ElementAt(team).Id,
                            series);

                        this.AllMatchesDescending.Add(match);
                    }
                }
            }
        }

        private void GenerateAscendingMatches(int numberOfTeams, int[,] results, Series series)
        {
            for (int round = 0; round <= results.GetUpperBound(1); round++)
            {
                for (int team = 0; team < numberOfTeams; team++)
                {
                    if (team < results[team, round] && round % 2 == 1)
                    {
                        Match match = new Match(this.Teams.ElementAt(team).ArenaName,
                            this.Teams.ElementAt(team).Id,
                            this.Teams.ElementAt(results[team, round]).Id,
                            series);

                        this.AllMatchesAscending.Add(match);
                    }
                    else if (team < results[team, round] && round % 2 == 0)
                    {
                        Match match = new Match(this.Teams.ElementAt(results[team, round]).ArenaName,
                            this.Teams.ElementAt(results[team, round]).Id,
                            this.Teams.ElementAt(team).Id,
                            series);

                        this.AllMatchesAscending.Add(match);
                    }
                }
            }
        }

        private Dictionary<int, List<Match>> GenerateRoundsWithMatches(int numberOfTeams)
        {
            int numberOfMatches = this.AllMatches.Count;

            int numberOfRounds = (numberOfMatches / numberOfTeams) * 2;
            int upperCounter = numberOfTeams / 2;

            for (int i = 0; i < numberOfRounds; i++)
            {
                if (i >= 1)
                {
                    upperCounter = upperCounter + numberOfTeams / 2;
                    int lowerCounter = upperCounter - numberOfTeams / 2;
                    this.AllMatchesWithRounds.Add(i, this.AllMatches.GetRange(lowerCounter, numberOfTeams / 2));
                }
                else
                {
                    this.AllMatchesWithRounds.Add(i, this.AllMatches.GetRange(i, upperCounter));
                }
            }
            for (int i = 0; i < AllMatchesWithRounds.Values.Count; i++)
            {
                var matchs = AllMatchesWithRounds[i];
                foreach (var match in matchs)
                {
                    match.Round = i + 1;
                }
            }

            return this.AllMatchesWithRounds;
        }

        private int[,] GenerateRoundRobin(int numberOfTeams)
        {
            if (numberOfTeams % 2 == 0)
            {
                return this.GenerateRoundRobinEvenTeams(numberOfTeams);
            }
            else
            {
                return this.RotateRounds(numberOfTeams);
            }
        }

        private int[,] RotateRounds(int numberOfTeams)
        {
            int n2 = (int)((numberOfTeams - 1) / 2);
            int[,] results = new int[numberOfTeams, numberOfTeams];

            int[] teams = new int[numberOfTeams];
            for (int i = 0; i < numberOfTeams; i++)
            {
                teams[i] = i;
            }

            for (int round = 0; round < numberOfTeams; round++)
            {
                for (int i = 0; i < n2; i++)
                {
                    int team1 = teams[n2 - i];
                    int team2 = teams[n2 + i + 1];
                    results[team1, round] = team2;
                    results[team2, round] = team1;
                }

                results[teams[0], round] = BYE;

                this.RotateArray(teams);
            }

            return results;
        }

        private void RotateArray(int[] teams)
        {
            int tmp = teams[teams.Length - 1];
            Array.Copy(teams, 0, teams, 1, teams.Length - 1);
            teams[0] = tmp;
        }

        private int[,] GenerateRoundRobinEvenTeams(int numberOfTeams)
        {
            int[,] results = this.RotateRounds(numberOfTeams - 1);

            int[,] results2 = new int[numberOfTeams, numberOfTeams - 1];
            for (int team = 0; team < numberOfTeams - 1; team++)
            {
                for (int round = 0; round < numberOfTeams - 1; round++)
                {
                    if (results[team, round] == BYE)
                    {
                        results2[team, round] = numberOfTeams - 1;
                        results2[numberOfTeams - 1, round] = team;
                    }
                    else
                    {
                        results2[team, round] = results[team, round];
                    }
                }
            }

            return results2;
        }
    }
}