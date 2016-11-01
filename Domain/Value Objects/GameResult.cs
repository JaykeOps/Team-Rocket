namespace Domain.Value_Objects
{
    internal class GameResult
    {
        public TeamName HomeTeam_Name { get; } // Maybe a field instead?
        public TeamName AwayTeam_Name { get; } // Maybe a field instead?
        public int HomeTeam_Score { get; }     // Maybe a field instead?
        public int AwayTeam_Score { get; }     // Maybe a field instead?

        public GameResult(TeamName homeTeam_Name, TeamName awayTeam_Name, int homeTeam_Score, int awayTeam_Score)
        {
            this.HomeTeam_Name = homeTeam_Name;
            this.AwayTeam_Name = awayTeam_Name;
            this.HomeTeam_Score = homeTeam_Score;
            this.AwayTeam_Score = awayTeam_Score;
        }

        public override string ToString()
        {
            return $"{HomeTeam_Name.ToString()}  {HomeTeam_Score} : {AwayTeam_Score}  {AwayTeam_Name.ToString()}"; // E.g. "Hammarby  3 : 0  Malmö"
        }                                                                                                          // Format style can be discussed...
    }
}