namespace LeagueRankApp.Models.Incoming;

/// <summary>
/// Represents each team and the respective score at the end of the match.
/// </summary>
/// <param name="TeamOne"></param>
/// <param name="TeamTwo"></param>
public record GameResult(TeamResult TeamOne, TeamResult TeamTwo);