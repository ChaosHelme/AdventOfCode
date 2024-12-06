using AdventOfCode.Shared;
using Spectre.Console;

namespace AdventOfCode24.Day2;

public class AdventOfCodeModule : IAdventOfCodeModule
{
	public DateOnly AoCYear => DateOnly.Parse("2024-12-02");
	public int Day => 2;
	public string Name => "*** Advent Of Code 24 Day 2 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		var validReports = PartOne(input);

		AnsiConsole.MarkupLine($"Total safe reports: [green]{validReports}[/]");
		return ValueTask.CompletedTask;
	}

	public int PartOne(string[] input)
	{
		var validReports = 0;
		for (var i = 0; i < input.Length; i++)
		{
			var row = input[i];
			var report = new Report(row.Split(' ').AsSpan());
			var isValid = report.IsValid();
			if (isValid)
				validReports++;
			
			AnsiConsole.MarkupLine($"Report on line {i} is: {(isValid ? "[green]Safe[/]" : "[red]Unsafe[/]")}");
		}
		return validReports;
	}
}

internal readonly ref struct Report(Span<string> report)
{
	readonly Span<string> _report = report;
    
	public bool IsValid()
	{
		if (_report.Length < 2) return false;

		var isIncreasing = false;
		var isDecreasing = false;
		
		for (var i = 1; i < _report.Length; i++)
		{
			var current = int.Parse(_report[i - 1]);
			var next = int.Parse(_report[i]);
			var sign = Math.Sign(next-current);
			var diff = Math.Abs(current-next);
			
			if (diff <= 0 || diff >= 4)
				return false; 
			
			isIncreasing |= sign == 1;
			isDecreasing |= sign == -1;
			
			switch (sign)
			{
				case 0:
				case < 0 when isIncreasing:
				case > 0 when isDecreasing:
					return false;
			}
		}

		return isIncreasing || isDecreasing;
	}
}