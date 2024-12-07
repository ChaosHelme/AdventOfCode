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

		validReports = PartTwo(input);

		AnsiConsole.MarkupLine($"Total safe reports after using problem damper: [green]{validReports}[/]");
		return ValueTask.CompletedTask;
	}

	readonly HashSet<int> _unsafeReportLines = [];
	int _safeReportsPartOne;

	public int PartOne(string[] input)
	{
		AnsiConsole.MarkupLine("[yellow]Part One[/]");
		for (var i = 0; i < input.Length; i++)
		{
			var row = input[i];
			var report = new Report(row.AsSpan());
			var isValid = report.IsValid();
			if (isValid)
			{
				_safeReportsPartOne++;
			} else
			{
				_unsafeReportLines.Add(i);
			}

			AnsiConsole.MarkupLine($"Report on line {i} is: {(isValid ? "[green]Safe[/]" : "[red]Unsafe[/]")}");
		}

		return _safeReportsPartOne;
	}

	public int PartTwo(string[] input)
	{
		if (_unsafeReportLines.Count <= 0)
		{
			PartOne(input);
		}

		AnsiConsole.MarkupLine("[yellow]Part Two[/]");
		var safeReportsPartTwo = 0;
		foreach (var unsafeReportLine in _unsafeReportLines)
		{
			var report = new Report(input[unsafeReportLine].AsSpan());
			var isValid = report.IsValidWithRemoval();
			if (isValid)
				safeReportsPartTwo++;

			AnsiConsole.MarkupLine($"Report on line {unsafeReportLine} is now: {(isValid ? "[green]Safe[/]" : "[red]Unsafe[/]")}");
		}

		return safeReportsPartTwo + _safeReportsPartOne;
	}

	readonly ref struct Report(ReadOnlySpan<char> report)
	{
		readonly ReadOnlySpan<char> _report = report;

		public bool IsValid() => IsValidInternal(-1);

		public bool IsValidWithRemoval()
		{
			for (var i = 0; i < _report.Length; i++)
			{
				if (IsValidInternal(i))
					return true;
			}

			return false;
		}

		private bool IsValidInternal(int skipIndex)
		{
			Span<Range> reports = stackalloc Range[_report.Length];
			var ranges = _report.Split(reports, ' ', StringSplitOptions.RemoveEmptyEntries);

			if (ranges < 2)
				return false;

			var isIncreasing = false;
			var isDecreasing = false;
			int? previousValue = null;

			for (var i = 0; i < ranges; i++)
			{
				if (i == skipIndex)
					continue;

				var current = int.Parse(_report[reports[i]]);

				if (previousValue.HasValue)
				{
					var isValid = IsValid(previousValue.Value, current, ref isIncreasing, ref isDecreasing);
					if (!isValid)
						return false;
				}

				previousValue = current;
			}

			return (isIncreasing && !isDecreasing) || (!isIncreasing && isDecreasing);
		}

		static bool IsValid(int current, int next, ref bool isIncreasing, ref bool isDecreasing)
		{
			var sign = Math.Sign(next - current);
			var diff = Math.Abs(current - next);

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

			return true;
		}
	}
}