namespace Domain.Interfaces
{
    public interface IPresentableTeamStats
    {
        string TeamName { get; }
        int GamesPlayed { get; }
        int Wins { get; }
        int Losses { get; }
        int Draws { get; }
        int GoalsFor { get; }
        int GoalsAgainst { get; }
        int GoalDifference { get; }
        int Points { get; }
    }
}