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
		var resultPartTwo = PartTwo(input);
		AnsiConsole.MarkupLine($"The sum of all updated sequences is: [green]{resultPartTwo}[/]");
		return ValueTask.CompletedTask;
	}

	readonly List<UpdateSequence> _invalidUpdateSequences = [];
	ImmutableHashSet<(int, int)> _updateOrders = [];

	public int PartOne(string[] input)
	{
		_updateOrders = ExtractUpdateOrdersFromInput(input);
		var updateSequences = ExtractUpdateSequencesFromInput(input, _updateOrders.Count);

		var updateOrder = ValueTuple.Create(0, 0);
		var sum = 0;
		foreach (var updateSequence in updateSequences)
		{
			var valid = true;
			for (var i = 1; i < updateSequence.Sequence.Length; i++)
			{
				updateOrder.Item1 = updateSequence.Sequence[i - 1];
				updateOrder.Item2 = updateSequence.Sequence[i];
				if (_updateOrders.Contains(updateOrder))
					continue;

				valid = false;
				_invalidUpdateSequences.Add(updateSequence);
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
		if (_invalidUpdateSequences.Count <= 0)
		{
			PartOne(input);
		}
		
		var dependencyGraph = BuildDependencyGraph(_updateOrders);

		return _invalidUpdateSequences
			.Select(u => u.Sequence)
			.Select(invalidUpdateSequence =>
				invalidUpdateSequence.OrderBy(x => x,
						new UpdateOrderComparer(dependencyGraph))
					.ToArray())
			.Select(x => x[x.Length / 2])
			.Sum();
	}
	
	static Dictionary<int, HashSet<int>> BuildDependencyGraph(ImmutableHashSet<(int fromNode, int toNode)> updateOrders)
	{
		var graph = new Dictionary<int, HashSet<int>>();

		foreach (var (fromNode, toNode) in updateOrders)
		{
			graph.TryAdd(fromNode, []);
			graph[fromNode].Add(toNode);
		}

		return graph;
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

	class UpdateOrderComparer(Dictionary<int, HashSet<int>> dependencyGraph) : IComparer<int>
	{
		public int Compare(int x, int y)
		{
			if (x == y) return 0;
			
			if (IsDependentOn(x, y)) return -1;

			return 1;
		}

		bool IsDependentOn(int x, int y)
		{
			return dependencyGraph.TryGetValue(x, out var value) && value.Any(neighbor => neighbor == y);
		}
	}
}