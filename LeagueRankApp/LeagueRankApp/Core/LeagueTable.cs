using LeagueRankApp.Models.Outgoing;

namespace LeagueRankApp.Core;

public class LeagueTable : ILeagueTable
{
    private readonly Dictionary<string, Team> _teams = new();
    private readonly GameResultParser _resultParser = new();

    public IReadOnlyDictionary<string, Team> Teams => _teams;
   
    public void AddGameResult(string result)
    {
        throw new NotImplementedException();
    }

    public List<RankedTeam> GetRankedTeams()
    {
        throw new NotImplementedException();
    }
}