namespace LeagueRankApp.Models.Incoming;

/// <summary>
/// Represents a team and its score at the end of a match
/// </summary>
/// <param name="Name"></param>
/// <param name="Score"></param>
public record TeamResult (string Name, int Score);