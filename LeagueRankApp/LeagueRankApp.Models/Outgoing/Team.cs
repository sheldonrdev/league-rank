namespace LeagueRankApp.Models.Outgoing;

/// <summary>
/// Represents a team and its match points.
/// </summary>
public class Team
{
    public string Name { get; }
    public int Points { get; private set; }

    public Team(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Team name cannot be null or empty", nameof(name));
        
        Name = name.Trim();
    }

    public void AddWin()
    {
        Points += 3;
    }

    public void AddDraw()
    {
        Points += 1;
    }

    public void AddLoss()
    {
        // Zero points
    }

    public override string ToString()
    {
        return $"{Name}: {Points} pts";
    }
}