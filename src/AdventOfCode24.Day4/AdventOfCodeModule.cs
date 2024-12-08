﻿using System.Text;
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
        AnsiConsole.MarkupLine($"[yellow]{Name}[/]");
        AnsiConsole.MarkupLine("[green]Part one[/]");
		PartOne(input);
        AnsiConsole.MarkupLine("[green]Part two[/]");
        PartTwo(input);
        
        return ValueTask.CompletedTask;
	}

	public int PartOne(string[] input)
	{
        var xmasPositions = FindAllXmas(input);
        AnsiConsole.MarkupLine($"Total XMAS occurrences: [green]{xmasPositions.Count}[/]");
        
        PrintResultGridPartOne(input, xmasPositions);
        
        return xmasPositions.Count;
	}

    public int PartTwo(string[] input)
    {
        var crossMasCount = PrintAndCountCrossMas(input);
        AnsiConsole.MarkupLine($"Total CrossMas occurrences: [green]{crossMasCount}[/]");
        return crossMasCount;
    }
	
	static List<(int, int, int, int)> FindAllXmas(string[] grid)
    {
        var positions = new List<(int, int, int, int)>();
        var rows = grid.Length;
        var cols = grid[0].Length;
        const string Target = "XMAS";

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                foreach (var (dr, dc) in Directions)
                {
                    if (CheckWord(grid, r, c, dr, dc, Target))
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
        => r >= 0 && r < grid.Length && c >= 0 && c < grid[0].Length;

    static IEnumerable<(int, int)> Directions 
        => [(0, 1), (1, 0), (1, 1), (-1, 1), (1, -1), (-1, -1), (-1, 0), (0, -1)];

    static void PrintResultGridPartOne(string[] grid, List<(int, int, int, int)> positions)
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

        var sb = new StringBuilder();
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[0].Length; j++)
                sb.Append(result[i, j]);
            sb.AppendLine();
        }
        AnsiConsole.WriteLine(sb.ToString());
    }
    
    static int PrintAndCountCrossMas(string[] grid)
    {
        var count = 0;
        var result = new char[grid.Length, grid[0].Length];
        string[] patterns = ["MSAMS", "SMASM", "SSAMM", "MMASS"];

        for (var r = 1; r < grid.Length - 1; r++)
        {
            for (var c = 1; c < grid[r].Length - 1; c++)
            {
                result[r, c] = '.';
                if (grid[r][c] != 'A')
                    continue;

                if (!patterns.Any(pattern => CheckPattern(grid, r, c, pattern)))
                    continue;

                count++;
                result[r, c] = 'A';
                result[r-1, c-1] = grid[r-1][c-1];
                result[r-1, c+1] = grid[r-1][c+1];
                result[r+1, c-1] = grid[r+1][c-1];
                result[r+1, c+1] = grid[r+1][c+1];
            }
        }

        var sb = new StringBuilder();
        for (var r = 0; r < grid.Length; r++)
        {
            for (var c = 0; c < grid[r].Length; c++)
                sb.Append(result[r, c]);
            sb.AppendLine();
        }
        AnsiConsole.WriteLine(sb.ToString());

        return count;
    }


    static bool CheckPattern(string[] grid, int row, int col, string pattern)
    {
        return grid[row-1][col-1] == pattern[0] &&
            grid[row-1][col+1] == pattern[1] &&
            grid[row+1][col-1] == pattern[3] &&
            grid[row+1][col+1] == pattern[4];
    }
}