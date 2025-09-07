namespace LeagueRankApp.Core.Util;

public static class FileOutputWriter
{
    /// <summary>
    /// Write content to a file.
    /// </summary>
    /// <param name="content">Content to write</param>
    /// <param name="outputFile">Path to output file</param>
    public static void WriteOutput(string content, string outputFile)
    {
        if (string.IsNullOrWhiteSpace(outputFile))
            throw new ArgumentException("Output file path cannot be null or empty", nameof(outputFile));

        File.WriteAllText(outputFile, content);
        if (!string.IsNullOrEmpty(content))
            File.AppendAllText(outputFile, Environment.NewLine);
    }
}