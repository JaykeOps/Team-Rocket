namespace Domain.Interfaces
{
    public interface IPresentablePlayerStats
    {
        string PlayerName { get; }
        int GoalCount { get; }
        int AssistCount { get; }
        int YellowCardCount { get; }
        int RedCardCount { get; }
        int PenaltyCount { get; }
        int GamesPlayedCount { get; }
    }
}