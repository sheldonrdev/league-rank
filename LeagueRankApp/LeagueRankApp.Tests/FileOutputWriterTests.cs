using LeagueRankApp.Core.Util;

namespace LeagueRankApp.Tests;

public class FileOutputWriterTests
{
    [Fact]
    public void WriteLines_WithValidFilePath_WritesLinesToFile()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        var expectedLines = new[] { "1. Lions, 3 pts", "2. Tigers, 1 pt", "3. Bears, 0 pts" };
        var expectedContent = string.Join(Environment.NewLine, expectedLines) + Environment.NewLine;
        
        try
        {
            // Act
            FileOutputWriter.WriteLines(expectedLines, tempFile);
            
            // Assert
            Assert.True(File.Exists(tempFile));
            var actualContent = File.ReadAllText(tempFile);
            Assert.Equal(expectedContent, actualContent);
        }
        finally
        {
            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Fact]
    public void WriteLines_WithEmptyLines_CreatesEmptyFile()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        var emptyLines = new string[0];
        
        try
        {
            // Act
            FileOutputWriter.WriteLines(emptyLines, tempFile);
            
            // Assert
            Assert.True(File.Exists(tempFile));
            var actualContent = File.ReadAllText(tempFile);
            Assert.Equal(string.Empty, actualContent);
        }
        finally
        {
            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Fact]
    public void WriteLines_OverwritesExistingFile()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        var originalContent = "Original content";
        var newLines = new[] { "New content" };
        var expectedContent = "New content" + Environment.NewLine;
        
        try
        {
            File.WriteAllText(tempFile, originalContent);
            Assert.Equal(originalContent, File.ReadAllText(tempFile));
            
            // Act
            FileOutputWriter.WriteLines(newLines, tempFile);
            
            // Assert
            var actualContent = File.ReadAllText(tempFile);
            Assert.Equal(expectedContent, actualContent);
            Assert.DoesNotContain(originalContent, actualContent);
        }
        finally
        {
            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Fact]
    public void WriteLines_WithNullOutputFile_ThrowsArgumentException()
    {
        // Arrange
        var lines = new[] { "1. Lions, 3 pts" };
        string? nullOutputFile = null;
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            FileOutputWriter.WriteLines(lines, nullOutputFile));
            
        Assert.Contains("Output file path cannot be null or empty", exception.Message);
    }

    [Fact]
    public void WriteLines_WithEmptyStringOutputFile_ThrowsArgumentException()
    {
        // Arrange
        var lines = new[] { "2. Tigers, 1 pt" };
        var emptyOutputFile = string.Empty;
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            FileOutputWriter.WriteLines(lines, emptyOutputFile));
            
        Assert.Contains("Output file path cannot be null or empty", exception.Message);
    }
}