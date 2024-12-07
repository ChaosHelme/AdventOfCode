using AdventOfCode.Shared;

namespace AdventOfCode24.Day3.Tests;

public class AdventOfCodeTests
{
	readonly IAdventOfCodeModule<long> _adventOfCodeModule = new AdventOfCodeModule();

	[TestCase("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))", ExpectedResult = 161)]
	public long PartOne(string input)
	{
		return _adventOfCodeModule.PartOne([input]);
	}
}