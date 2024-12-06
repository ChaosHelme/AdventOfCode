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

	public int PartOne(string[] input)
	{
		return 0;
	}

	public int PartTwo(string[] input)
	{
		return 0;
	}

ValueTask RunAsync(string[] input, CancellationToken cancellationToken);
}