using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using Spectre.Console;

namespace AdventOfCode24.Day3;

public partial class AdventOfCodeModule : IAdventOfCodeModule<long>
{
	public DateOnly AoCYear => DateOnly.Parse("2024-12-03");
	public int Day => 3;
	public string Name => "*** Advent Of Code 24 Day 3 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		AnsiConsole.MarkupLine("[yellow]Advent of Code 24: Day 3[/]\n");
		
		var totalSum = PartOne(input);
		AnsiConsole.MarkupLine($"[green]{totalSum}[/]\n");
		
		return ValueTask.CompletedTask;
	}

	[GeneratedRegex(@"mul\(([1-9]\d{0,2}),([1-9]\d{0,2})\)", RegexOptions.Compiled)]
	private static partial Regex MulRegex();
	
	public long PartOne(string[] input)
	{
		AnsiConsole.WriteLine("Part One");
		var totalSum = 0L;
		for (var i = 0; i < input.Length; i++)
		{
			var line = input[i];
			var matches = MulRegex().Matches(line);
			var sum = matches.Sum(match =>
			{
				var x = long.Parse(match.Groups[1].Value);
				var y = long.Parse(match.Groups[2].Value);
				return x * y;
			});

			AnsiConsole.MarkupLine($"The sum of all multiplications in line {i} is: [green]{sum}[/]");
			totalSum += sum;
		}

		return totalSum;
	}
	
	public long PartTwo(string[] input) => throw new NotImplementedException();
}