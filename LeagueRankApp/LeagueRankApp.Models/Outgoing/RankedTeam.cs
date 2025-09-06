namespace LeagueRankApp.Models.Outgoing;

/// <summary>
/// Represents a teams ranked position.
/// </summary>
public class RankedTeam(Team team, int rank)
{
    public Team Team { get; } = team ?? throw new ArgumentNullException(nameof(team));
    public int Rank { get; } = rank;
}