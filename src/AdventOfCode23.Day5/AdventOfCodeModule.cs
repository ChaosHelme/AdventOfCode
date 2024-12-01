using AdventOfCode.Shared;

namespace AdventOfCode23.Day5;

public sealed class AdventOfCodeModule : IAdventOfCodeModule
{
	public DateOnly AoCYear => DateOnly.Parse("2023-12-05");
	public int Day => 5;
	public string Name => "*** Advent Of Code 23 Day 5 ***";

	public async ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		if (input.Length <= 0)
		{
			return;
		}
		
		var almanac = new Almanac(input);
		almanac.InitializeRanges();

		var startTime = DateTime.Now;
		Console.WriteLine($"Lowest location found synchronously: {almanac.ProcessAllSeeds()}");
		var endTime = DateTime.Now;
		Console.WriteLine($"Synchronous method ran {(endTime-startTime).TotalSeconds}s");

		startTime = DateTime.Now;
		var lowestLocation = await almanac.ProcessAllSeedsInParallelAsync(cancellationToken);
		Console.WriteLine($"Lowest location found asynchronously: {lowestLocation}");
		endTime = DateTime.Now;
		Console.WriteLine($"Asynchronous method ran {(endTime-startTime).TotalSeconds}s");
	}
}