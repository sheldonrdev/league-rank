using LeagueRankApp.Models.Outgoing;

namespace LeagueRankApp.Core;

/// <summary>
/// Manages the league table and team rankings.
/// </summary>
public interface ILeagueTable
{
    /// <summary>
    /// Add a game result to the league table.
    /// </summary>
    /// <param name="result">Game result</param>
    /// <exception cref="FormatException">Invalid result</exception>
    void AddGameResult(string result);
    
    /// <summary>
    /// Get teams ranked by points.
    /// </summary>
    /// <returns>List of Ranked Teams</returns>
    List<RankedTeam> GetRankedTeams();
}