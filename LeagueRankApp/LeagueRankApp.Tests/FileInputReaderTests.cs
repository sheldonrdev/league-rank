using LeagueRankApp.Core.Util;

namespace LeagueRankApp.Tests;

public class FileInputReaderTests
{
    [Fact]
    public void ReadLines_WithValidFilePath_ReturnsFileContents()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        var expectedLines = new[]
        {
            "Lions 3, Snakes 1",
            "Tigers 2, Bears 0",
            "Eagles 1, Wolves 1"
        };
        
        try
        {
            File.WriteAllLines(tempFile, expectedLines);
            
            // Act
            var actualLines = FileInputReader.ReadLines(tempFile).ToArray();
            
            // Assert
            Assert.Equal(expectedLines.Length, actualLines.Length);
            for (int i = 0; i < expectedLines.Length; i++)
            {
                Assert.Equal(expectedLines[i], actualLines[i]);
            }
        }
        finally
        {
            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Fact]
    public void ReadLines_WithEmptyFile_ReturnsEmptyCollection()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        
        try
        {
            File.WriteAllText(tempFile, string.Empty);
            
            // Act
            var lines = FileInputReader.ReadLines(tempFile).ToArray();
            
            // Assert
            Assert.Empty(lines);
        }
        finally
        {
            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Fact]
    public void ReadLines_WithNonExistentFile_ThrowsFileNotFoundException()
    {
        // Arrange
        var nonExistentFile = Path.Combine(Path.GetTempPath(), "this_file_does_not_exist.txt");
        
        // Act & Assert
        var exception = Assert.Throws<FileNotFoundException>(() => 
            FileInputReader.ReadLines(nonExistentFile).ToArray());
            
        Assert.Contains("Input file not found", exception.Message);
        Assert.Contains(nonExistentFile, exception.Message);
    }

    [Fact]
    public void ReadLines_WithNullInputFile_ThrowsArgumentException()
    {
        // Arrange
        string? nullFile = null;
        
        // Act, Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            FileInputReader.ReadLines(nullFile).ToArray());
            
        Assert.Contains("Input file path cannot be null or empty", exception.Message);
    }

    [Fact]
    public void ReadLines_WithEmptyStringInputFile_ThrowsArgumentException()
    {
        // Arrange
        var emptyFile = string.Empty;
        
        // Act, Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            FileInputReader.ReadLines(emptyFile).ToArray());
            
        Assert.Contains("Input file path cannot be null or empty", exception.Message);
    }

    [Fact]
    public void ReadLines_WithFileContainingBlankLines_ReturnsAllLines()
    {
        // Arrange
        var tempFile = Path.GetTempFileName();
        var expectedLines = new[]
        {
            "Lions 3, Snakes 1",
            "",
            "Tigers 2, Bears 0",
            "   ",
            "Eagles 1, Wolves 1"
        };
        
        try
        {
            File.WriteAllLines(tempFile, expectedLines);
            
            // Act
            var actualLines = FileInputReader.ReadLines(tempFile).ToArray();
            
            // Assert
            Assert.Equal(expectedLines.Length, actualLines.Length);
            for (int i = 0; i < expectedLines.Length; i++)
            {
                Assert.Equal(expectedLines[i], actualLines[i]);
            }
        }
        finally
        {
            // Cleanup
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }
}