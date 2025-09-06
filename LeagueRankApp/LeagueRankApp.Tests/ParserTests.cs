namespace LeagueRankApp.Tests;

public class ParserTests
{
    [Fact]
    public void ParseGameResult_ValidInput_ReturnsCorrectTeamNamesAndScores()
    {
        // Arrange
        var parser = new GameResultParser();
        
        // Act
        var result = parser.ParseGameResult("Lions 3, Snakes 3");
        
        // Assert
        Assert.Equal("Lions", result.TeamOne.Name);
        Assert.Equal(3, result.TeamOne.Score);
        Assert.Equal("Snakes", result.TeamTwo.Name);
        Assert.Equal(3, result.TeamTwo.Score);
    }
}