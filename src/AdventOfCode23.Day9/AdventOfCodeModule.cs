using AdventOfCode.Shared;

namespace AdventOfCode23.Day9;

public sealed class AdventOfCodeModule : IAdventOfCodeModule
{
	public DateOnly AoCYear => DateOnly.Parse("2023-12-09");
	public int Day => 9;
	public string Name => "*** Advent Of Code 23 Day 9 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		if (input.Length <= 0)
		{
			return ValueTask.CompletedTask;
		}
		
		var sequenceExtrapolator = new SequenceExtrapolator();
		var sumOfAllNextValues = 0;
		foreach (var line in input) {
			var sequence = line.Split(' ').Select(int.Parse).ToList();
    
			sequenceExtrapolator.BuildHistories(sequence);
    
			var nextValue = sequenceExtrapolator.PredictNext();
			var previousValue = sequenceExtrapolator.PredictPrevious();
			sumOfAllNextValues += nextValue;
    
			Console.WriteLine($"The predicted next value for the history {line} is: {nextValue} the previous value is: {previousValue}");
			sequenceExtrapolator.PrintHistories();
		}

		Console.WriteLine($"The sum of all extrapolated values is: {sumOfAllNextValues}");
		
		return ValueTask.CompletedTask;
	}
}