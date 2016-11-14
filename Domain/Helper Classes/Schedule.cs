using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;

namespace RoundRobinTest
{
    class Schedule
    {
        //Work in progress..

        private const int BYE = -1;

        public List<Match> AllMatchesDescending = new List<Match>();
        public List<Match> AllMatchesAscending = new List<Match>();
        public List<Match> AllMatches = new List<Match>();
        public List<Team> Teams = new List<Team>();
        public Dictionary<int, List<Match>> AllMatchesWithRounds = new Dictionary<int, List<Match>>();


        public Dictionary<int, List<Match>> GenerateScheduleTest(int numberOfTeams)
        {
            var teamService = new TeamService();
            var seriesService = new SeriesService();

            var listOfAllTeams = teamService.GetAll();
            var listOfAllSeries = seriesService.GetAll();


            this.Teams.AddRange(listOfAllTeams);

            var testSerie = listOfAllSeries.ElementAt(0);


            int[,] results = GenerateRoundRobin(numberOfTeams);

            

            //Generates the schedule for descending matches
            for (int round = 0; round <= results.GetUpperBound(1); round++)
            {
                for (int team = 0; team < numberOfTeams; team++)
                {
                    if (team < results[team, round] && round % 2 == 0)
                    {
                        Match match = new Match(this.Teams.ElementAt(team).Arena, 
                            this.Teams.ElementAt(team).Id, 
                            this.Teams.ElementAt(results[team, round]).Id, 
                            testSerie);

                        this.AllMatchesDescending.Add(match);
                        

                    }

                    else if (team < results[team, round] && round % 2 == 1)
                    {
                        Match match = new Match(this.Teams.ElementAt(results[team, round]).Arena,
                            this.Teams.ElementAt(results[team, round]).Id,
                            this.Teams.ElementAt(team).Id,
                            testSerie);

                        this.AllMatchesDescending.Add(match);

                        
                    }

                }

            }

            //Generates the schedule for ascending matches
            for (int round = 0; round <= results.GetUpperBound(1); round++)
            {
                for (int team = 0; team < numberOfTeams; team++)
                {
                    if (team < results[team, round] && round % 2 == 1)
                    {
                        Match match = new Match(this.Teams.ElementAt(team).Arena,
                            this.Teams.ElementAt(team).Id,
                            this.Teams.ElementAt(results[team, round]).Id,
                            testSerie);

                        this.AllMatchesAscending.Add(match);

                    }

                    else if (team < results[team, round] && round % 2 == 0)
                    {
                        Match match = new Match(this.Teams.ElementAt(results[team, round]).Arena,
                            this.Teams.ElementAt(results[team, round]).Id,
                            this.Teams.ElementAt(team).Id,
                            testSerie);

                        this.AllMatchesAscending.Add(match);
                    }
                    
                }
                
            }

            this.AllMatchesAscending.Reverse();
            this.AllMatches.AddRange(this.AllMatchesDescending);
            this.AllMatches.AddRange(this.AllMatchesAscending);


            //Generate dictionary with roundnumber as key and matches for each round as value
            int numberOfMatches = this.AllMatches.Count;

            int numberOfRounds = (numberOfMatches / numberOfTeams) * 2;
            int upperCounter = numberOfTeams / 2;

            for (int i = 0; i < numberOfRounds; i++)
            {

                if (i >= 1)
                {
                    upperCounter = upperCounter + numberOfTeams/2;
                    int lowerCounter = upperCounter - numberOfTeams/2;
                    this.AllMatchesWithRounds.Add(i, AllMatches.GetRange(lowerCounter, numberOfTeams/2));
                }
                else
                {
                    this.AllMatchesWithRounds.Add(i, AllMatches.GetRange(i, upperCounter));
                }
            }

            return this.AllMatchesWithRounds;
        }

        private int[,] GenerateRoundRobin(int numberOfTeams)
        {
            if (numberOfTeams % 2 == 0)
                return GenerateRoundRobinEven(numberOfTeams);
            else
                return GenerateRoundRobinOdd(numberOfTeams);
        }

        private int[,] GenerateRoundRobinOdd(int numberOfTeams)
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

                RotateArray(teams);
            }

            return results;
        }

        private void RotateArray(int[] teams)
        {
            int tmp = teams[teams.Length - 1];
            Array.Copy(teams, 0, teams, 1, teams.Length - 1);
            teams[0] = tmp;
        }

        private int[,] GenerateRoundRobinEven(int numberOfTeams)
        {

            int[,] results = GenerateRoundRobinOdd(numberOfTeams - 1);

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
