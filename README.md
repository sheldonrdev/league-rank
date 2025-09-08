# League Table Calculator

A production-ready, maintainable, testable command-line application that calculates ranking tables for a league based on match results.

## Features

- **File-based Input/Output**: Reads game results from a single text file and outputs a formatted league table.
- **Team Name Parsing**: Handles complex team names including numbers, spaces, and special characters.
- **League Scoring**: Win (3 pts), Draw (1 pt), Loss (0 pts).
- **Ranking**: Handles tied scores with rank skipping and alphabetical sorting.
- **Automated Testing**: Unit testing inclusive of edge cases.
- **Clean Architecture**: Separated concerns with utilities, core logic, and orchestration components.

This application has been written in C#. The solution may be open

## Specifications

- **.NET 8.0 SDK** or later
- **Language:**: C#
- **IDEs**: Rider, VS Studio, VS Code.
- **Operating System**: Windows, macOS, or Linux

## Installation

1. Clone the repository
2. Open `LeagueRankApp.sln` in one of the supported IDEs and run the application using `F5`

or
1. Clone the repository
2. Navigate to the project directory: `..LeagueRankApp/LeagueRankApp`
3. Build the project: `dotnet build`
4. Run the application: `dotnet run`

## Usage

The application automatically processes a sample data files:

```bash
dotnet run

# The application will:
# - Read from: LeagueRankData/sample.txt 
# - Write to: LeagueRankData/sampleOutput.txt
# - Display success message with file paths
```

## Input Format

Game results should be formatted as: `"TeamName1 Score1, TeamName2 Score2"`

### Supported Team Name Formats
The team names below, which exist in reality, have been modified to illustrate team name edge cases.

- Simple names: `"Lions 3, Snakes 1"`
- Names with numbers: `"Aston 2 Villa 1, FC Barcelona 0"`
- Names with embedded numbers: `"Bayern 04 Munich 2, Tottenham Hotspur 1"`
- Complex structures: `"Leeds 2000 United 1, AC Milan 0"`
- Names ending with numbers: `"Arsenal FC2 1, Chelsea FC 0"`

## Scoring Rules

- **Win**: 3 points
- **Draw (tie)**: 1 point  
- **Loss**: 0 points

## Ranking Rules

1. **Primary**: Teams are ranked by total points (highest to lowest)
2. **Ties**: Teams with the same points receive the same rank
3. **Tie-breaking**: Sorted alphabetically (ascending) by team name
4. **Rank jumping**: After the tied teams, the next rank accounts for tied positions

Example: If 3 teams tie for 2nd place, the next team gets 5th place.

## Project Structure

```
LeagueRankApp/
├── LeagueRankApp/                 # Main console application
│   ├── Core/                     # Core business logic
│   │   ├── GameResultParser.cs   # Parses game result strings
│   │   ├── LeagueTable.cs        # Manages teams and calculates rankings
│   │   └── Util/                 # Utility classes
│   │       ├── FileInputReader.cs  # File input operations
│   │       ├── FileOutputWriter.cs # File output operations
│   │       └── Extensions.cs      # Extension methods (table formatting)
│   ├── Models/                   # Data models
│   │   ├── Incoming/            # Input models
│   │   └── Outgoing/            # Output models  
│   ├── Orchestrator/            # Application orchestration
│   └── Program.cs               # Entry point
├── LeagueRankApp.Tests/         # Unit tests
└── LeagueRankApp.Models/        # Shared models project
```

## Testing

### Running Tests
```bash
cd LeagueRankApp
dotnet test
```

### Test Coverage
- **Parser Tests**: Game result parsing with various team name formats
- **League Table Tests**: Scoring, ranking, and tie-breaking logic
- **File I/O Tests**: Input reading and output writing functionality
- **Table Formatter Tests**: Output formatting with singular/plural points
- **Edge Case Tests**: Complex team names and formatting variations

## Sample Data
The sample data is located within the `LeagueRankData` folder within the root directory. 
This directory will house the input and generated output file. 
NB. As per OS requirements, only one file may be named `sample.txt`, therefore change the contents of the file as required.

### Basic Sample (`./LeagueRankData/sample.txt`)
```
Lions 3, Snakes 3
Tarantulas 1, FC Awesome 0
Lions 1, FC Awesome 1
Tarantulas 3, Snakes 1
Lions 4, Grouches 0
```

### Edge Case Sample (`./LeagueRankData/sampleEdgeCase.txt`)
An additional sample file is provided to demonstrate edge cases. You may rename this file to `sample.txt` or
change uncomment the path in the `Program.cs` file of the application. 

```
Aston 2 Villa 2, FC Barcelona 1
Twenty FC 0, Aston 2 Villa 3
Leeds 2000 United 1, Twenty FC 1
Bayern 04 Munich 2, Leeds 2000 United 0
Arsenal FC2 1, Bayern 04 Munich 1
FC Barcelona 3, Arsenal FC2 0
Lions 2, FC Barcelona 2
Aston 2 Villa 1, Lions 1
```

### Expected Output Format
```
1. Tarantulas, 6 pts
2. Lions, 5 pts
3. FC Awesome, 1 pt
3. Snakes, 1 pt
5. Grouches, 0 pts
```

## Known Limitations

The current parser successfully handles standard league data formats including complex team names. Initially I had anticipated more complex parsing would be required for complex pattern matching (edge cases), potentially solved with regex and a strategy pattern to illustrate switch between both (demonstrative reasons), however the simple string-based parser is robust enough to cover most cases. Guided by YAGNI and time-constraints, the string-based parser was retained as the only parser.

**Successfully Handled Cases:**
- Teams with a number in name: `"Aston 2 Villa 1, FC Barcelona 0"`
- Teams with numbers in name: `"Bayern 04 Munich 2, Arsenal  FC 1"`  
- Complex team names: `"Leeds 2000 United FC 1, AC Milan 0"`
- Multiple spaces in name: `"Lions  3,   Snakes    1"`
- Names ending with numbers: `"Arsenal FC2 1, Chelsea FC 0"`

**Extreme Edge Cases (not in standard formats):**
- No spaces before scores: `"Arsenal2, Chelsea1"`
- Non-standard delimiters: `"Lions:3,Snakes:1"`  
- Mixed whitespace: `"Lions\t3,\tSnakes\t1"`

These extreme cases are not present in standard league data and would require regex-based parsing if the requirement arises. The edge cases are known limitations in the system and have not been accounted for given the unnecessary complexity. These cases will result in undesirable behavior within the application if used.

## Architecture Decisions

### String Parsing
Through test-driven development, I discovered that a simple approach using `String.Split()` operations handles all required cases and most edge cases effectively. This follows the **YAGNI principle** - avoiding over-engineering when simple solutions meet requirements. The extreme edge cases were documented above as known limitations.

### File-Only I/O
Chose file-based input/output over stdin/stdout for:
- **Simplicity**: Clearer, more testable code
- **Reliability**: Consistent behavior across platforms  
- **Requirements**: Requirements doc allows either approach.

### File I/O Implementation
Selected `File.ReadLines()` and `File.WriteAllLines()` over alternatives:
- **vs ReadAllLines/WriteAllText**: Streaming approach avoids loading entire files into memory
- **vs StreamReader/StreamWriter**: Simple API without manual resource management complexity
- **Right-sized solution**: Memory efficient for larger files expected for a standard league size while maintaining code simplicity over more elaborate streaming (StreamReader/Writer).
- **Perfect symmetry**: Clean `IEnumerable<string>` pipeline from input to output

### Cross-Platform Compatibility
Handled platform differences using .NET framework APIs:
- **Line endings**: `File.WriteAllLines()` automatically uses correct line endings (`\r\n` on Windows, `\n` on Unix/Linux)
- **Path separators**: `Path.Combine()` handles correct directory separators (`\` on Windows, `/` on Unix/Linux)
- **File encoding**: Framework defaults ensure consistent UTF-8 handling across platforms
- **No platform-specific code**: Single codebase runs identically on Windows, macOS, and Linux

## Error Handling

- **Invalid file paths**: Clear error messages with file path details
- **Malformed game results**: Intentional exceptions raised with detailed messages.
- **Missing data**: Descriptive validation messages
- **Parse failures**: Failures raised with detailed messages.

## Performance

- **O(n log n)** complexity for sorting teams by points and name
- **Efficient parsing** with single-pass string operations
- **Memory efficient** with streaming file operations rather than loading the entire file into memory.

## Development

### Building for Development
```bash
# Debug build
dotnet build

# Run with sample data
dotnet run

# Run tests
dotnet test

# Clean build artifacts
dotnet clean
```

### Code Quality
- **Test-Driven Development**: Comprehensive unit test coverage
- **SOLID Principles**: Clean architecture with separated concerns
- **Defensive Programming**: Input validation and error handling
- **Documentation**: Readme documentation with clear naming conventions and application limitations.

## AI Assistance

This project was developed without the assistance of an AI assistant (LLM, Agent or other) to transparently demonstrate my ability as a software developer.

Whilst I have not used an AI assistant in this assessment, I do use them in my day-to-day activities for:
- Proof reading documentation I write
- Summarising documents and emails
- Assisting with generating overviews from code
- Generating acceptance tests from requirements
- Unit Tests
- Technical debt
- Code review, etc.

Having shared the above detail, it's imperative to note, all design decisions, requirements interpretation, and final code review remains under my control. 
I strongly believe whilst AI may serve as a development accelerator I am still responsible for maintaining code quality and architectural integrity.

## Contributing

This project is for demonstration purposes.

## License

This project is for demonstration purposes.