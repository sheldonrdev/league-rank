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
        throw new NotImplementedException();
    }
}