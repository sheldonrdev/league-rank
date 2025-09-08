using LeagueRankApp.Models.Outgoing;
using LeagueRankApp.Core.Util;

namespace LeagueRankApp.Tests;

public class TableFormatterTests
{
    [Fact]
    public void FormatTableLines_SingleTeamWithMultiplePoints_FormatsCorrectlyWithPluralPts()
    {
        // Arrange
        var team = new Team("Lions");
        team.AddWin();
        var rankedTeams = new List<RankedTeam> { new(team, 1) };
        
        // Act
        var result = rankedTeams.FormatTableLines().ToList();
        
        // Assert
        Assert.Single(result);
        Assert.Equal("1. Lions, 3 pts", result[0]);
    }

    [Fact] 
    public void FormatTableLines_SingleTeamWithOnePoint_FormatsCorrectlyWithSingularPt()
    {
        // Arrange
        var team = new Team("Tigers");
        team.AddDraw();
        var rankedTeams = new List<RankedTeam> { new(team, 1) };
        
        // Act
        var result = rankedTeams.FormatTableLines().ToList();
        
        // Assert
        Assert.Single(result);
        Assert.Equal("1. Tigers, 1 pt", result[0]);
    }
}