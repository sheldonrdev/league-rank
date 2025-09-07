using LeagueRankApp.Models.Outgoing;
using LeagueRankApp.Core.Util;

namespace LeagueRankApp.Tests;

public class TableFormatterTests
{
    [Fact]
    public void FormatTable_SingleTeamWithMultiplePoints_FormatsCorrectlyWithPluralPts()
    {
        // Arrange
        var team = new Team("Lions");
        team.AddWin();
        var rankedTeams = new List<RankedTeam> { new(team, 1) };
        
        // Act
        var result = rankedTeams.FormatTable();
        
        // Assert
        Assert.Equal("1. Lions, 3 pts", result);
    }

    [Fact] 
    public void FormatTable_SingleTeamWithOnePoint_FormatsCorrectlyWithSingularPt()
    {
        // Arrange
        var team = new Team("Tigers");
        team.AddDraw();
        var rankedTeams = new List<RankedTeam> { new(team, 1) };
        
        // Act
        var result = rankedTeams.FormatTable();
        
        // Assert
        Assert.Equal("1. Tigers, 1 pt", result);
    }
}