using LeagueRankApp.Models.Incoming;
using LeagueRankApp.Models.Outgoing;

namespace LeagueRankApp.Core;

public class LeagueTable : ILeagueTable
{
    private readonly Dictionary<string, Team> _teams = new();
    private readonly GameResultParser _resultParser = new();

    public IReadOnlyDictionary<string, Team> Teams => _teams;
   
    public void AddGameResult(string result)
    {
        var gameResult = _resultParser.ParseGameResult(result);

        AddTeamToLeague(gameResult.TeamOne.Name);
        AddTeamToLeague(gameResult.TeamTwo.Name);

        var team1 = _teams[gameResult.TeamOne.Name];
        var team2 = _teams[gameResult.TeamTwo.Name];

        if (gameResult.TeamOne.Score > gameResult.TeamTwo.Score)
        {
            team1.AddWin();
            team2.AddLoss();
        }
        else if (gameResult.TeamTwo.Score > gameResult.TeamOne.Score)
        {
            team2.AddWin();
            team1.AddLoss();
        }
        else
        {
            team1.AddDraw();
            team2.AddDraw();
        }
    }

    public List<RankedTeam> GetRankedTeams()
    {
        if (!_teams.Any())
            return new List<RankedTeam>();

        // Sort teams first DESC by points then ASC by name
        var sortedTeams = _teams.Values
            .OrderByDescending(t => t.Points)
            .ThenBy(t => t.Name)
            .ToList();

        // Assign ranks
        return AssignRanks(sortedTeams);
    }
    
    private void AddTeamToLeague(string teamName)
    {
        if (!_teams.ContainsKey(teamName))
            _teams[teamName] = new Team(teamName);
    }
    
    private List<RankedTeam> AssignRanks(List<Team> sortedTeams)
    {
        var rankedTeams = new List<RankedTeam>();
        var currentRank = 1;

        for (int teamIndex = 0; teamIndex < sortedTeams.Count; teamIndex++)
        {
            var currentTeam = sortedTeams[teamIndex];
        
            // Teams with same points get same rank; when points change, rank jumps to current position
            if (teamIndex > 0 && (currentTeam.Points != sortedTeams[teamIndex - 1].Points)) // previousTeam = sortedTeams[teamIndex - 1]
                currentRank = teamIndex + 1;

            rankedTeams.Add(new RankedTeam(currentTeam, currentRank));
        }
        
        return rankedTeams;
    }
}