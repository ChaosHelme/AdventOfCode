using AdventOfCode.Shared;

namespace AdventOfCode24.Day2.Tests;

public class AdventOfCodeTests
{
	readonly IAdventOfCodeModule _adventOfCodeModule = new AdventOfCodeModule();
	
	[TestCase((object) new []
	{
		"45 46 44 58 55",
		"7 6 4 2 1",
		"1 2 7 8 9",
		"9 7 6 2 1",
		"1 3 2 4 5",
		"8 6 4 4 1",
		"1 3 6 7 9",
		"10 11 12 13 14"
	}, ExpectedResult = 3 )]
	public int Part1(string[] input)
	{
		return this._adventOfCodeModule.PartOne(input);
	}
}