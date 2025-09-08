namespace LeagueRankApp.Core.Util;

public static class FileInputReader
{
    /// <summary>
    /// Read lines from a file using streaming.
    /// </summary>
    /// <param name="inputFile">Path to input file</param>
    /// <returns>Enumerable of input lines streamed line-by-line from file</returns>
    public static IEnumerable<string> ReadLines(string inputFile)
    {
        if (string.IsNullOrWhiteSpace(inputFile))
            throw new ArgumentException("Input file path cannot be null or empty", nameof(inputFile));

        if (!File.Exists(inputFile))
            throw new FileNotFoundException($"Input file not found: {inputFile}");
        
        return StreamLines(inputFile);
    }
    
    private static IEnumerable<string> StreamLines(string inputFile)
    {
        using var reader = new StreamReader(inputFile);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            yield return line;
        }
    }
}