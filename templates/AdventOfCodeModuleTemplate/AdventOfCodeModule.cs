using AdventOfCode.Shared;

namespace AdventOfCodeModuleTemplate;

public class AdventOfCodeModule : IAdventOfCodeModule<int>
{
	public DateOnly AoCYear => DateOnly.Parse("YYYY-12-DD");
	public int Day => throw new NotImplementedException();
	public string Name => "*** Advent Of Code YEAR Day DAY ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken) => throw new NotImplementedException();

	public int PartOne(string[] input)
	{
		return 0;
	}

	public int PartTwo(string[] input)
	{
		return 0;
	}
}