using AdventOfCode.Shared;

namespace AdventOfCode23.Day11;

public sealed class AdventOfCodeModule : IAdventOfCodeModule
{
	public DateOnly AoCYear => DateOnly.Parse("2023-12-11");
	public int Day => 11;
	public string Name => "*** Advent Of Code 23 Day 11 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		if (input.Length <= 0)
		{
			return ValueTask.CompletedTask;
		}
		
		var cosmos = new Cosmos(input);

		Console.WriteLine("Original:");
		cosmos.PrintOriginalCosmos();

		var expanded = cosmos.ExpandCosmos();

		Console.WriteLine($"\nExpanding columns by {expanded.Item1}");
		Console.WriteLine($"Expanding rows by {expanded.Item2}");

		Console.WriteLine("\nExpanded:");
		cosmos.PrintExpandedCosmos();

		Console.WriteLine($"\nAmount of universes: {cosmos.CountAmountOfUniverse()}");

		cosmos.AssignUniqueNumberToUniverses();

		Console.WriteLine($"\nCombination of universes: {cosmos.CalculateUniqueCombinationsOfUniverses()}");
		Console.WriteLine($"\nSum of all shortest distance between universe combinations: {cosmos.SumOfShortestDistanceBetweenUniverseCombinations()}");

		cosmos.PrintShortestDistanceBetweenUniverseCombinations();
		
		return ValueTask.CompletedTask;
	}
}