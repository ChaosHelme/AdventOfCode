namespace AdventOfCode.Shared;

public interface IAdventOfCodeModule
{
	DateOnly AoCYear { get; }
	int Day { get; }
	string Name { get; }
	ValueTask RunAsync(string[] input, CancellationToken cancellationToken);
}