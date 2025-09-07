using LeagueRankApp.Core.Util;

namespace LeagueRankApp.Tests;

public class FileOutputWriterTests
{
    [Fact]
    public void WriteOutput_WithValidFilePath_WritesContentToFile()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        var expectedContent = @"1. Lions, 3 pts
2. Tigers, 1 pt
3. Bears, 0 pts";
        
        try
        {
            // Act
            FileOutputWriter.WriteOutput(expectedContent, tempFile);
            
            // Assert
            Assert.True(File.Exists(tempFile));
            var actualContent = File.ReadAllText(tempFile).TrimEnd();
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
    public void WriteOutput_WithEmptyContent_CreatesEmptyFile()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        var emptyContent = string.Empty;
        
        try
        {
            // Act
            FileOutputWriter.WriteOutput(emptyContent, tempFile);
            
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
    public void WriteOutput_OverwritesExistingFile()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        var originalContent = "Original content";
        var newContent = "New content";
        
        try
        {
            File.WriteAllText(tempFile, originalContent);
            Assert.Equal(originalContent, File.ReadAllText(tempFile));
            
            // Act
            FileOutputWriter.WriteOutput(newContent, tempFile);
            
            // Assert
            var actualContent = File.ReadAllText(tempFile).TrimEnd();
            Assert.Equal(newContent, actualContent);
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
    public void WriteOutput_WithNullOutputFile_ThrowsArgumentException()
    {
        // Arrange
        var content = "1. Lions, 3 pts";
        string? nullOutputFile = null;
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            FileOutputWriter.WriteOutput(content, nullOutputFile));
            
        Assert.Contains("Output file path cannot be null or empty", exception.Message);
    }

    [Fact]
    public void WriteOutput_WithEmptyStringOutputFile_ThrowsArgumentException()
    {
        // Arrange
        var content = "2. Tigers, 1 pt";
        var emptyOutputFile = string.Empty;
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            FileOutputWriter.WriteOutput(content, emptyOutputFile));
            
        Assert.Contains("Output file path cannot be null or empty", exception.Message);
    }
}