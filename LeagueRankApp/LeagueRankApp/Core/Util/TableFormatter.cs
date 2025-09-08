using LeagueRankApp.Models.Outgoing;

namespace LeagueRankApp.Core.Util;

/// <summary>
/// Formats the league table for output.
/// </summary>
public static class TableFormatter
{
    public static IEnumerable<string> FormatTableLines(this IEnumerable<RankedTeam>? rankedTeams)
    {
        if (rankedTeams == null)
            yield break;

        foreach (var rankedTeam in rankedTeams)
        {
            var team = rankedTeam.Team;
            var points = team.Points == 1 ? "pt" : "pts";
            yield return $"{rankedTeam.Rank}. {team.Name}, {team.Points} {points}";
        }
    }
}