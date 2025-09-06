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
    
    [Theory]
    [InlineData("Lions 3, Snakes 3", "Lions", 3, "Snakes", 3)]
    [InlineData("Tarantulas 1, FC Awesome 0", "Tarantulas", 1, "FC Awesome", 0)]
    [InlineData("Lions 1, FC Awesome 1", "Lions", 1, "FC Awesome", 1)]
    [InlineData("Tarantulas 3, Snakes 1", "Tarantulas", 3, "Snakes", 1)]
    [InlineData("Lions 4, Grouches 0", "Lions", 4, "Grouches", 0)]
    public void ParseGameResult_ValidInputs_ReturnsCorrectTeamNamesAndScores(
        string input,
        string expectedTeamOneName,
        int expectedTeamOneScore,
        string expectedTeamTwoName,
        int expectedTeamTwoScore)
    {
        // Arrange
        var parser = new GameResultParser();
    
        // Act
        var result = parser.ParseGameResult(input);
    
        // Assert
        Assert.Equal(expectedTeamOneName, result.TeamOne.Name);
        Assert.Equal(expectedTeamOneScore, result.TeamOne.Score);
        Assert.Equal(expectedTeamTwoName, result.TeamTwo.Name);
        Assert.Equal(expectedTeamTwoScore, result.TeamTwo.Score);
    }
}