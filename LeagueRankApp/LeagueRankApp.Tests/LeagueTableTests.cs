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

    [Fact]
    public void GetRankedTeams_SortsResultsByPointsDescThenNamesAsc_ReturnsSortedByPoints()
    {
        // Arrange
        var league = new LeagueTable();
        
        // Act
        league.AddGameResult("Tigers 3, Snakes 1");
        league.AddGameResult("Lions 3, Bears 1");
        var rankedTeams = league.GetRankedTeams();
        
        // Assert
        Assert.Equal(4, rankedTeams.Count);
        Assert.Equal("Lions", rankedTeams[0].Team.Name);
        Assert.Equal("Tigers", rankedTeams[1].Team.Name);  
        Assert.Equal("Bears", rankedTeams[2].Team.Name);
        Assert.Equal("Snakes", rankedTeams[3].Team.Name);
    }
    
    [Fact]
    public void AddGameResult_CaseInsensitiveTeamNames_TreatsAsSameTeam()
    {
        // Arrange
        var league = new LeagueTable();
    
        // Act
        league.AddGameResult("Lions 3, Snakes 1");
        league.AddGameResult("LIONS 2, SnAkeS 1");
        var rankedTeams = league.GetRankedTeams();
    
        // Assert
        Assert.Equal(2, rankedTeams.Count);
    
       // Lions = 6 points (2 wins)
        var lions = rankedTeams.First(rt => rt.Team.Name.Equals("Lions", StringComparison.OrdinalIgnoreCase));
        Assert.Equal(6, lions.Team.Points);
    
        // Snakes = 0 points (2 losses)  
        var snakes = rankedTeams.First(rt => rt.Team.Name.Equals("Snakes", StringComparison.OrdinalIgnoreCase));
        Assert.Equal(0, snakes.Team.Points);
    }
    
    [Fact]
    public void GetRankedTeams_SortsMoreResultsByPointsDescThenNamesAsc_ReturnsSortedByPoints()
    {
        // Arrange
        var league = new LeagueTable();
        
        // Act
        league.AddGameResult("Lions 3, Snakes 1");
        league.AddGameResult("Tigers 1, Lions 1");
        league.AddGameResult("Bears 2, Snakes 0");
        var rankedTeams = league.GetRankedTeams();
        
        // Assert
        Assert.Equal(4, rankedTeams.Count);
        Assert.Equal("Lions", rankedTeams[0].Team.Name); 
        Assert.Equal(4, rankedTeams[0].Team.Points); 
        Assert.Equal("Bears", rankedTeams[1].Team.Name); 
        Assert.Equal(3, rankedTeams[1].Team.Points); 
        Assert.Equal("Tigers", rankedTeams[2].Team.Name);
        Assert.Equal(1, rankedTeams[2].Team.Points); 
        Assert.Equal("Snakes", rankedTeams[3].Team.Name);
        Assert.Equal(0, rankedTeams[3].Team.Points);
    }
    
    [Fact]
    public void GetRankedTeams_SortsTeamsWithSamePoints_HaveSameRank()
    {
        // Arrange
        var league = new LeagueTable();
        
        // Act
        league.AddGameResult("Lions 1, Snakes 1");
        league.AddGameResult("Tigers 1, Bears 1");
        league.AddGameResult("Eagles 3, Wolves 0");
        var rankedTeams = league.GetRankedTeams();
        
        // Assert
        Assert.Equal(6, rankedTeams.Count);
        Assert.Equal("Eagles", rankedTeams[0].Team.Name);
        Assert.Equal(1, rankedTeams[0].Rank);
        
        // Teams with 1 pt should have rank = 2 and be sorted ASC by name
        Assert.Equal("Bears", rankedTeams[1].Team.Name);
        Assert.Equal(2, rankedTeams[1].Rank);
        Assert.Equal("Lions", rankedTeams[2].Team.Name);
        Assert.Equal(2, rankedTeams[2].Rank);
        Assert.Equal("Snakes", rankedTeams[3].Team.Name);
        Assert.Equal(2, rankedTeams[3].Rank);
        Assert.Equal("Tigers", rankedTeams[4].Team.Name);
        Assert.Equal(2, rankedTeams[4].Rank);
        
        // Team with 0 pts should have rank = 6
        Assert.Equal("Wolves", rankedTeams[5].Team.Name);
        Assert.Equal(6, rankedTeams[5].Rank);
    }
}