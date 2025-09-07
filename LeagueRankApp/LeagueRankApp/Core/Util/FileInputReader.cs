namespace LeagueRankApp.Core.Util;

public static class FileInputReader
{
    /// <summary>
    /// Read lines from a file.
    /// </summary>
    /// <param name="inputFile">Path to input file</param>
    /// <returns>Enumerable of input lines</returns>
    public static IEnumerable<string> ReadLines(string inputFile)
    {
        if (string.IsNullOrWhiteSpace(inputFile))
            throw new ArgumentException("Input file path cannot be null or empty", nameof(inputFile));

        if (!File.Exists(inputFile))
            throw new FileNotFoundException($"Input file not found: {inputFile}");
        
        return File.ReadAllLines(inputFile);
    }
}