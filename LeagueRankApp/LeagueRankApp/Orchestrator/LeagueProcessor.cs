using LeagueRankApp.Core;
using LeagueRankApp.Core.Util;

namespace LeagueRankApp.Orchestrator;

public class LeagueProcessor
{
    /// <summary>
    /// Orchestrates the league processing steps.
    /// </summary>
    /// <param name="inputFile">Path to input file</param>
    /// <param name="outputFile">Path to output file</param>
    public void RunApplication(string inputFile, string outputFile)
    {
        try
        {
            var inputLines = FileInputReader.ReadLines(inputFile);
            var result = ProcessLeague(inputLines);
            FileOutputWriter.WriteOutput(result, outputFile);
        }
        catch (Exception ex) when (!(ex is FormatException))
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
    
    /// <summary>
    /// Processes game results and returns formatted league table.
    /// </summary>
    /// <param name="gameResultLines">List of game result strings</param>
    /// <returns>Formatted league table string</returns>
    public string ProcessLeague(IEnumerable<string> gameResultLines)
    {
        var leagueTable = new LeagueTable();
        int lineNumber = 0;
        
        foreach (var line in gameResultLines)
        {
            lineNumber++;
            var trimmedLine = line?.Trim();
            
            if (string.IsNullOrEmpty(trimmedLine))
                continue;

            try
            {
                leagueTable.AddGameResult(trimmedLine);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Error on line {lineNumber}: {ex.Message}", ex);
            }
        }

        var rankedTeams = leagueTable.GetRankedTeams();
        return rankedTeams.FormatTable();
    }
}