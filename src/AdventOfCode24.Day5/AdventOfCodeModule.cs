using System.Collections.Immutable;
using AdventOfCode.Shared;
using Spectre.Console;

namespace AdventOfCode24.Day5;

public class AdventOfCodeModule : IAdventOfCodeModule<int>
{
	public DateOnly AoCYear => DateOnly.Parse("2024-12-05");
	public int Day => 5;
	public string Name => "*** Advent Of Code 24 Day 5 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		var resultPartOne = PartOne(input);
		AnsiConsole.MarkupLine($"The sum of all correct update sequences is: [green]{resultPartOne}[/]");
		
		return ValueTask.CompletedTask;
	}
	
	public int PartOne(string[] input)
	{
		var updateOrders = ExtractUpdateOrdersFromInput(input);
		var updateSequences = ExtractUpdateSequencesFromInput(input, updateOrders.Count);

		var updateOrder = new ValueTuple<int, int>(0, 0);
		var sum = 0;
		foreach (var updateSequence in updateSequences)
		{
			var valid = true;
			for (var i = 1; i < updateSequence.Sequence.Length; i++)
			{
				updateOrder.Item1 = updateSequence.Sequence[i - 1];
				updateOrder.Item2 = updateSequence.Sequence[i];
				if (updateOrders.Contains(updateOrder))
					continue;

				valid = false;
				break;
			}

			if (!valid)
				continue;

			var middlePageNumber = updateSequence.Sequence[updateSequence.Sequence.Length / 2];
			sum += middlePageNumber;
			AnsiConsole.MarkupLine("Update Sequence {0} is [green]valid[/]", string.Join(", ", updateSequence.Sequence));
		}
		return sum;
	}

	public int PartTwo(string[] input)
	{
		return 0;
	}
	
	static UpdateSequence[] ExtractUpdateSequencesFromInput(string[] input, int updateOrdersLength) 
		=> input
			.TakeLast(input.Length - updateOrdersLength)
			.SkipWhile(line => !line.Contains(','))
			.Select(line => 
				new UpdateSequence
				{
					Sequence = line
						.Split(',', StringSplitOptions.RemoveEmptyEntries)
						.Select(int.Parse)
						.ToArray(),
				})
			.ToArray();

	static ImmutableHashSet<ValueTuple<int, int>> ExtractUpdateOrdersFromInput(string[] input) 
		=> input
			.TakeWhile(line => line.Contains('|'))
			.Select(line =>
			{
				var parts = line.Split('|');
				return new ValueTuple<int, int>(int.Parse(parts[0]), int.Parse(parts[1]));
			})
			.ToImmutableHashSet();

	record struct UpdateSequence(int[] Sequence);
}