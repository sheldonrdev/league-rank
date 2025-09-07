using LeagueRankApp.Models.Outgoing;

namespace LeagueRankApp.Core.Util;

/// <summary>
/// Formats the league table for output.
/// </summary>
public static class TableFormatter
{
    public static string FormatTable(this IEnumerable<RankedTeam>? rankedTeams)
    {
        if (rankedTeams == null)
            return string.Empty;

        var teamsList = rankedTeams.ToList();
        if (!teamsList.Any())
            return string.Empty;

        var lines = teamsList.Select(rankedTeam =>
        {
            var team = rankedTeam.Team;
            var points = team.Points == 1 ? "pt" : "pts";
            return $"{rankedTeam.Rank}. {team.Name}, {team.Points} {points}";
        });

        return string.Join(Environment.NewLine, lines);
    }
}