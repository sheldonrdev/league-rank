# League Table Calculator (C#)

A command-line (CLI) application written in C# which calculates a ranking table for a league based on match results.

## Features

## Requirements

- **.NET 8.0 SDK** or later
- **Operating System**: Windows, macOS, or Linux

## Installation


## Usage


## Input


## Rules

- **Win**: 3 points
- **Draw (tie)**: 1 point
- **Loss**: 0 points

## Project Structure

## Testing

### Pre-requisites

### Running Tests

## Data

### Sample Input (`./LeagueRankData/sample.txt`)
```
Lions 3, Snakes 3
Tarantulas 1, FC Awesome 0
Lions 1, FC Awesome 1
Tarantulas 3, Snakes 1
Lions 4, Grouches 0
```

### Expected Output
```
1. Tarantulas, 6 pts
2. Lions, 5 pts
3. FC Awesome, 1 pt
3. Snakes, 1 pt
5. Grouches, 0 pts
```

## Error Handling


## Performance


## Development

### Building for Development
```bash
# Debug build
dotnet build
```

## License