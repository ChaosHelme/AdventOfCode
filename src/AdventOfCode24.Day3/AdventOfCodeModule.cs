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
			var currentSum = 0L;
			var muleEnabled = true;
			
			do
			{
				currentSum += MulSum(input[i], ref startIndex);
			} while (startIndex > 0);
			
			AnsiConsole.MarkupLine($"The sum of all multiplications in line {i} is: [green]{currentSum}[/]");
			totalSum += currentSum;
		}

		return totalSum;
	}
	
	int _lastDoIndex = -1;
	int _lastDontIndex = -1;
	long MulSum(string line, ref int startIndex)
	{
		var mulMatch = MulRegex().Match(line, startIndex);
		var doMatch = DoRegex().Match(line, startIndex);
		var dontMatch = DontRegex().Match(line, startIndex);

		if (_lastDoIndex < doMatch.Index)
			_lastDoIndex = doMatch.Index;
		if (_lastDontIndex < dontMatch.Index)
			_lastDontIndex = dontMatch.Index;
		
		if (!mulMatch.Success)
		{
			startIndex = -1;
			return 0L;
		}
		startIndex = mulMatch.Index + mulMatch.Length;
		
		return mulMatch.Index < _lastDontIndex || (mulMatch.Index > _lastDoIndex && _lastDoIndex > _lastDontIndex)
			? int.Parse(mulMatch.Groups[1].Value) * int.Parse(mulMatch.Groups[2].Value) 
			: 0L; 
	}
}