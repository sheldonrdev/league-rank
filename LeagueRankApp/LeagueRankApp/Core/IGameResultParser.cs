using LeagueRankApp.Models.Incoming;

namespace LeagueRankApp.Core;

public interface IGameResultParser
{
    /// <summary>
    /// Processes the raw game result and extracts teams and their respective scores.
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    GameResult ParseGameResult(string line);
}