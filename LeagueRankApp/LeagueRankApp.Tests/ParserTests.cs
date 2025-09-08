using LeagueRankApp.Core;

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

    #region Edge Case Tests
    
    [Fact]
    public void ParseGameResult_TeamNameWithNumbers_ShouldParseCorrectly()
    {
        // Arrange
        const string input = "Aston 2 Villa 1, FC Barcelona 0";
        var parser = new GameResultParser();
        
        // Act
        var result = parser.ParseGameResult(input);
        
        // Assert
        Assert.Equal("Aston 2 Villa", result.TeamOne.Name);
        Assert.Equal(1, result.TeamOne.Score);
        Assert.Equal("FC Barcelona", result.TeamTwo.Name);
        Assert.Equal(0, result.TeamTwo.Score);
    }
    
    [Fact]
    public void ParseGameResult_TeamNameWithNumericWords_ShouldParseCorrectly()
    {
        // Arrange
        const string input = "Twenty FC 1, Eleven United 0";
        var parser = new GameResultParser();
        
        // Act
        var result = parser.ParseGameResult(input);
        
        // Assert
        Assert.Equal("Twenty FC", result.TeamOne.Name);
        Assert.Equal(1, result.TeamOne.Score);
        Assert.Equal("Eleven United", result.TeamTwo.Name);
        Assert.Equal(0, result.TeamTwo.Score);
    }
    
    [Fact]
    public void ParseGameResult_MultipleConsecutiveSpaces_ShouldParseCorrectly()
    {
        // Arrange
        const string input = "Lions  3,   Snakes    1";
        var parser = new GameResultParser();
        
        // Act
        var result = parser.ParseGameResult(input);
        
        // Assert
        Assert.Equal("Lions", result.TeamOne.Name);
        Assert.Equal(3, result.TeamOne.Score);
        Assert.Equal("Snakes", result.TeamTwo.Name);
        Assert.Equal(1, result.TeamTwo.Score);
    }
    
    [Fact] 
    public void ParseGameResult_TeamNamesWithComplexStructure_ShouldParseCorrectly()
    {
        // Arrange
        const string input = "Leeds 2000 United 1, AC Milan 0";
        var parser = new GameResultParser();
        
        // Act
        var result = parser.ParseGameResult(input);
        
        // Assert
        Assert.Equal("Leeds 2000 United", result.TeamOne.Name);
        Assert.Equal(1, result.TeamOne.Score);
        Assert.Equal("AC Milan", result.TeamTwo.Name);
        Assert.Equal(0, result.TeamTwo.Score);
    }
    
    [Fact]
    public void ParseGameResult_ScoreEmbeddedInTeamName_ShouldParseCorrectly()
    {
        // Arrange
        const string input = "Bayern 04 Munich 2, Tottenham Hotspur 1";
        var parser = new GameResultParser();
        
        // Act
        var result = parser.ParseGameResult(input);
        
        // Assert
        Assert.Equal("Bayern 04 Munich", result.TeamOne.Name);
        Assert.Equal(2, result.TeamOne.Score);
        Assert.Equal("Tottenham Hotspur", result.TeamTwo.Name);
        Assert.Equal(1, result.TeamTwo.Score);
    }
    
    [Fact]
    public void ParseGameResult_TeamNameEndsWithNumber_ShouldParseCorrectly()
    {
        // Arrange
        const string input = "Arsenal FC2 1, Chelsea FC 0";
        var parser = new GameResultParser();
        
        // Act
        var result = parser.ParseGameResult(input);
        
        // Assert
        Assert.Equal("Arsenal FC2", result.TeamOne.Name);
        Assert.Equal(1, result.TeamOne.Score);
        Assert.Equal("Chelsea FC", result.TeamTwo.Name);
        Assert.Equal(0, result.TeamTwo.Score);
    }

    [Fact]
    public void ParseGameResult_NumbersInMiddleOfTeamName_WillFailWithSimpleParser()
    {
        // Arrange
        const string input = "Team 1 0 United 2, FC Barcelona 0";
        var parser = new GameResultParser();
        
        // Act
        var result = parser.ParseGameResult(input);
        
        // Assert
        Assert.Equal("Team 1 0 United", result.TeamOne.Name);
        Assert.Equal(2, result.TeamOne.Score);
        Assert.Equal("FC Barcelona", result.TeamTwo.Name);
        Assert.Equal(0, result.TeamTwo.Score);
    }
    
    [Fact]
    public void ParseGameResult_TeamNameIsJustNumbers_WillFailWithSimpleParser()
    {
        // Arrange  
        const string input = "1860 2, Barcelona 1";
        var parser = new GameResultParser();
        
        // Act
        var result = parser.ParseGameResult(input);
        
        // Assert
        Assert.Equal("1860", result.TeamOne.Name);
        Assert.Equal(2, result.TeamOne.Score);
        Assert.Equal("Barcelona", result.TeamTwo.Name);
        Assert.Equal(1, result.TeamTwo.Score);
    }
    
    #endregion
}