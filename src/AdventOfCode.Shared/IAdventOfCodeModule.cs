using System.Resources;

namespace AdventOfCode.Shared;

public interface IAdventOfCodeModule
{
	DateOnly AoCYear { get; }
	int Day { get; }
	string Name { get; }
	string ResourceName => $"{this.GetType().Namespace}.Input.txt";

	public string[] GetInput()
	{
		var assembly = this.GetType().Assembly;
		using var stream = assembly.GetManifestResourceStream(ResourceName);
		if (stream == null)
			throw new MissingManifestResourceException($"Resource '{ResourceName}' not found.");

		using var reader = new StreamReader(stream);
		return reader.ReadToEnd().Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
	}
	
	ValueTask RunAsync(string[] input, CancellationToken cancellationToken);
}

public interface IAdventOfCodeModule<out T> : IAdventOfCodeModule
	where T : IComparable
{
	public T PartOne(string[] input)
	{
		return default!;
	}

	public T PartTwo(string[] input)
	{
		return default!;
	}

	
}