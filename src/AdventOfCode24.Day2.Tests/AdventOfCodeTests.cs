using AdventOfCode.Shared;

namespace AdventOfCode24.Day2.Tests;

public class AdventOfCodeTests
{
	readonly IAdventOfCodeModule<int> _adventOfCodeModule = new AdventOfCodeModule();
	
	[TestCase((object) new []
	{
		"7 6 4 2 1",
		"1 2 7 8 9",
		"9 7 6 2 1",
		"1 3 2 4 5",
		"8 6 4 4 1",
		"1 3 6 7 9"
	}, ExpectedResult = 2 )]
	public int Part1(string[] input)
	{
		return _adventOfCodeModule.PartOne(input);
	}

	[TestCase((object) new []
	{
		"7 6 4 2 1",
		"1 2 7 8 9",
		"9 7 6 2 1",
		"1 3 2 4 5",
		"8 6 4 4 1",
		"1 3 6 7 9",
	}, ExpectedResult = 4 )]
	public int Part2(string[] input)
	{
		return _adventOfCodeModule.PartTwo(input);
	}
}