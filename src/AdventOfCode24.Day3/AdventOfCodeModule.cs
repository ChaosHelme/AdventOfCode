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
		AnsiConsole.MarkupLine("[yellow]Advent of Code 24: Day 3[/]");
		
		var totalSum = PartOne(input);
		AnsiConsole.MarkupLine($"Total mul sum: [green]{totalSum}[/]");
		
		totalSum = PartTwo(input);
		AnsiConsole.MarkupLine($"Total fixed mul sum: [green]{totalSum}[/]");
		
		return ValueTask.CompletedTask;
	}

	[GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Compiled)]
	private static partial Regex MulRegex();
	
	[GeneratedRegex(@"do\(\)", RegexOptions.Compiled)]
	private static partial Regex DoRegex();

	[GeneratedRegex(@"don't\(\)", RegexOptions.Compiled)]
	private static partial Regex DontRegex();
	
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
				var x = int.Parse(match.Groups[1].Value);
				var y = int.Parse(match.Groups[2].Value);
				return x * y;
			});

			AnsiConsole.MarkupLine($"The sum of all multiplications in line {i} is: [green]{sum}[/]");
			totalSum += sum;
		}

		return totalSum;
	}

	public long PartTwo(string[] input)
	{
		var totalSum = 0L;
		for (var i = 0; i < input.Length; i++)
		{
			var startIndex = 0;
			var line = input[i];
			var currentSum = 0L;
			var muleEnabled = true;
			
			do
			{
				currentSum += MulSum(line, ref startIndex, ref muleEnabled);
			} while (startIndex > 0);
			
			AnsiConsole.MarkupLine($"The sum of all multiplications in line {i} is: [green]{currentSum}[/]");
			totalSum += currentSum;
		}

		return totalSum;
	}

	static long MulSum(string line, ref int startIndex, ref bool mulEnabled)
	{
		var mulMatch = MulRegex().Match(line, startIndex, line.Length - startIndex);
		var doMatch = DoRegex().Match(line, startIndex, line.Length - startIndex);
		var dontMatch = DontRegex().Match(line, startIndex, line.Length - startIndex);
		
		startIndex = mulMatch.Index + mulMatch.Length;
		mulEnabled = (mulMatch.Index < dontMatch.Index) || (!mulEnabled && doMatch.Index < mulMatch.Index && doMatch.Index > dontMatch.Index);
		if (!mulEnabled)
		{
			return 0;
		}
		
		return int.Parse(mulMatch.Groups[1].Value) * int.Parse(mulMatch.Groups[2].Value);
	}
}