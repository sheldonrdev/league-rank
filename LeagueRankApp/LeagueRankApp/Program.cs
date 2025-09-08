using LeagueRankApp.Core;
using LeagueRankApp.Orchestrator;

namespace LeagueRankApp;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var dataDir = FindDataDirectory();
            
            var inputFile = Path.Combine(dataDir, "sample.txt");
            var outputFile = Path.Combine(dataDir, "sampleOutput.txt");
            // Alternative: Use edge case data, comment out the above and uncomment the below lines
            /*
            var inputFile = Path.Combine(dataDir, "sampleEdgeCase.txt");
            var outputFile = Path.Combine(dataDir, "sampleEdgeCaseOutput.txt");            
            */

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
    
    private static string FindDataDirectory()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var dir = new DirectoryInfo(currentDir);
        
        // Traverse root until desired data folder is located
        while (dir != null)
        {
            var dataPath = Path.Combine(dir.FullName, "LeagueRankData");
            if (Directory.Exists(dataPath))
            {
                return dataPath;
            }
            dir = dir.Parent;
        }
        
        throw new DirectoryNotFoundException("LeagueRankData directory not found. Please ensure it exists in the repository root with the sample input file (sample.txt).)");
    }
}