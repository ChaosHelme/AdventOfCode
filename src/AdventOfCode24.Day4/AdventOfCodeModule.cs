using AdventOfCode.Shared;
using Spectre.Console;

namespace AdventOfCode24.Day4;

public class AdventOfCodeModule : IAdventOfCodeModule<int>
{
    public DateOnly AoCYear => DateOnly.Parse("2024-12-04");
    public int Day => 4;
    public string Name => "*** Advent Of Code 24 Day 4 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		PartOne(input);
        
        return ValueTask.CompletedTask;
	}

	public int PartOne(string[] input)
	{
        var xmasPositions = FindAllXmas(input);
        AnsiConsole.MarkupLine($"Total XMAS occurrences: [green]{xmasPositions.Count}[/]");
        
        PrintResultGrid(input, xmasPositions);
        
        return xmasPositions.Count;
	}
	
	static List<(int, int, int, int)> FindAllXmas(string[] grid)
    {
        var positions = new List<(int, int, int, int)>();
        var rows = grid.Length;
        var cols = grid[0].Length;
        var target = "XMAS";

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                foreach (var (dr, dc) in Directions)
                {
                    if (CheckWord(grid, r, c, dr, dc, target))
                    {
                        positions.Add((r, c, r + 3 * dr, c + 3 * dc));
                    }
                }
            }
        }

        return positions;
    }

    static bool CheckWord(string[] grid, int r, int c, int dr, int dc, string word)
    {
        if (!IsInBounds(grid, r + (word.Length - 1) * dr, c + (word.Length - 1) * dc))
            return false;

        return word.Select((ch, i) => grid[r + i * dr][c + i * dc] == ch).All(match => match);
    }

    static bool IsInBounds(string[] grid, int r, int c)
    {
        return r >= 0 && r < grid.Length && c >= 0 && c < grid[0].Length;
    }

    static IEnumerable<(int, int)> Directions 
        => [(0, 1), (1, 0), (1, 1), (-1, 1), (1, -1), (-1, -1), (-1, 0), (0, -1)];

    static void PrintResultGrid(string[] grid, List<(int, int, int, int)> positions)
    {
        var result = new char[grid.Length, grid[0].Length];
        for (var i = 0; i < grid.Length; i++)
            for (var j = 0; j < grid[0].Length; j++)
                result[i, j] = '.';

        foreach (var (startR, startC, endR, endC) in positions)
        {
            var dr = Math.Sign(endR - startR);
            var dc = Math.Sign(endC - startC);
            for (var i = 0; i < 4; i++)
                result[startR + i * dr, startC + i * dc] = grid[startR + i * dr][startC + i * dc];
        }

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[0].Length; j++)
                Console.Write(result[i, j]);
            AnsiConsole.WriteLine();
        }
    }
}