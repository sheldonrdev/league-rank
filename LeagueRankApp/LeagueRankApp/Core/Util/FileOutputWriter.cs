namespace LeagueRankApp.Core.Util;

public static class FileOutputWriter
{
    /// <summary>
    /// Write lines to a file using simple streaming.
    /// </summary>
    /// <param name="lines">Lines to write to file</param>
    /// <param name="outputFile">Path to output file</param>
    public static void WriteLines(IEnumerable<string> lines, string outputFile)
    {
        if (string.IsNullOrWhiteSpace(outputFile))
            throw new ArgumentException("Output file path cannot be null or empty", nameof(outputFile));

        File.WriteAllLines(outputFile, lines);
    }
}