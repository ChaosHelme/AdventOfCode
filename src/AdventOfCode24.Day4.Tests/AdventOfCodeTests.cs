using AdventOfCode.Shared;

namespace AdventOfCode24.Day4.Tests;

public class AdventOfCodeTests
{
	readonly IAdventOfCodeModule<int> _adventOfCodeModule = new AdventOfCodeModule();

	[TestCase( (object) new[]
	{
		"MMMSXXMASM",
		"MSAMXMSMSA",
		"AMXSXMAAMM",
		"MSAMASMSMX",
		"XMASAMXAMM",
		"XXAMMXXAMA",
		"SMSMSASXSS",
		"SAXAMASAAA",
		"MAMMMXMMMM",
		"MXMXAXMASX"
	}, ExpectedResult = 18)]
	public int PartOne(string[] input)
	{
		return _adventOfCodeModule.PartOne(input);
	}
	
	[TestCase( (object) new[]
	{
		"MMMSXXMASM",
		"MSAMXMSMSA",
		"AMXSXMAAMM",
		"MSAMASMSMX",
		"XMASAMXAMM",
		"XXAMMXXAMA",
		"SMSMSASXSS",
		"SAXAMASAAA",
		"MAMMMXMMMM",
		"MXMXAXMASX"
	}, ExpectedResult = 9)]
	public int PartTwo(string[] input)
	{
		return _adventOfCodeModule.PartTwo(input);
	}
}