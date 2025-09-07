using LeagueRankApp.Core;

namespace LeagueRankApp.Tests;

public class LeagueTableTests
{
    [Fact]
    public void AddGameResult_TeamWins_Awards3Points()
    {
        // Arrange
        var league = new LeagueTable();
        
        // Act
        league.AddGameResult("Lions 3, Snakes 1");
        
        // Assert
        var teams = league.Teams;
        Assert.Equal(3, teams["Lions"].Points);
        Assert.Equal(0, teams["Snakes"].Points);
    }

    [Fact]
    public void AddGameResult_GameDraw_Awards1PointEach()
    {
        // Arrange
        var league = new LeagueTable();
        
        // Act
        league.AddGameResult("Lions 3, Snakes 3");
        
        // Assert
        var teams = league.Teams;
        Assert.Equal(1, teams["Lions"].Points);
        Assert.Equal(1, teams["Snakes"].Points);
    }

    [Fact]
    public void AddGameResult_TeamLoses_Awards0Points()
    {
        // Arrange
        var league = new LeagueTable();
        
        // Act
        league.AddGameResult("Lions 1, Snakes 3");
        
        // Assert
        var teams = league.Teams;
        Assert.Equal(0, teams["Lions"].Points);
        Assert.Equal(3, teams["Snakes"].Points);
    }

}