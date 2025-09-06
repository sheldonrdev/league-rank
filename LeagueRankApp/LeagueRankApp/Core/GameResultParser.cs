using System.Text.RegularExpressions;
using LeagueRankApp.Models;

namespace LeagueRankApp;

public class GameResultParser
{
    public GameResult ParseGameResult(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            throw new FormatException("Game result line cannot be empty");

        line = line.Trim();

        // Step 1: Extract team results
        var teams = line.Split(',');
        if (teams.Length != 2)
            throw new FormatException($"Unexpected number of teams: {teams.Length}");

        // Step 2: Parse each team result
        var teamOne = ParseTeamResult(teams[0].Trim(), "Team One");
        var teamTwo = ParseTeamResult(teams[1].Trim(), "Team Two");

        return new(teamOne, teamTwo);
    }
    
    private TeamResult ParseTeamResult(string teamResult, string teamDetail)
    {
        if (string.IsNullOrEmpty(teamResult))
            throw new FormatException($"Missing data for {teamDetail}");

        // Split team name part from score part
        var parts = teamResult.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
            throw new FormatException($"Invalid format for {teamDetail}: '{teamResult}'. Expected 'TeamName Score'");

        var teamName = ExtractTeamName(parts, teamDetail);
        var score = ExtractScore(parts, teamDetail);

        return new TeamResult(teamName, score);
    }

    private string ExtractTeamName(string[] parts, string teamDetail)
    {
        // Extract name by combining all elements except the last (score)
        var teamName = string.Join(" ", parts[..^1]);
    
        if (string.IsNullOrEmpty(teamName))
            throw new FormatException($"Team name cannot be empty for {teamDetail}");

        return teamName;
    }

    private int ExtractScore(string[] parts, string teamDetail)
    {
        // Extract score by taking the last element
        var scoreStr = parts[^1];
    
        if (!int.TryParse(scoreStr, out var score))
            throw new FormatException($"Invalid score for {teamDetail}: '{scoreStr}'. Must be a number in range [0-9]");

        if (score < 0)
            throw new FormatException($"Score cannot be negative for {teamDetail}: {score}. Must be a number in range [0-9]");

        return score;
    }
}



