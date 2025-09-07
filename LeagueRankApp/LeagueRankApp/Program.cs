using LeagueRankApp.Core;
using LeagueRankApp.Orchestrator;

namespace LeagueRankApp;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var inputFile = Path.Combine("..", "..", "..", "..", "..", "LeagueRankData", "sample.txt");
            var outputFile = Path.Combine("..", "..", "..", "..", "..", "LeagueRankData", "sampleOutput.txt");

            var app = new LeagueProcessor();
            app.RunApplication(inputFile, outputFile);
            
            Console.WriteLine("League table processing: Complete.");
            Console.WriteLine($"Input: {inputFile}");
            Console.WriteLine($"Output: {outputFile}");
        }
        catch (FormatException ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}