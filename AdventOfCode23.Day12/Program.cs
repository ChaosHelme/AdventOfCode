using System.Text;
using AdventOfCode.Shared;

namespace AdventOfCode23.Day12;

public sealed class AdventOfCodeModule : IAdventOfCodeModule
{
	public DateOnly AoCYear => DateOnly.Parse("2023-12-12");
	public int Day => 12;
	public string Name => "*** Advent Of Code 23 Day 12 ***";
	
	const int Mod = 1_000_000_007;

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		foreach (var line in input) {
			// A ? is an unknown spring, it can be either a # or a .Whereas a # is a damaged spring and a . is an operational spring.
			// Based on the contiguous group of damaged springs, we can determine the unknown springs
			ProcessRow(line);
		}
		
		return ValueTask.CompletedTask;
	}
	
	static void ProcessRow(string conditionRecord) {
		var parts = conditionRecord.Split(' ');
    
		var unknownPart = parts[0];
		var damagedGroupsStr = parts[1];
    
		var damagedGroups = damagedGroupsStr.Split(',').Select(int.Parse).ToList();
    
		CountValidArrangements(unknownPart, damagedGroups);
    
		// Console.WriteLine($"Possible arrangements for {conditionRecord}:");
		// foreach (var arrangement in arrangements) {
		// 	Console.WriteLine(new string(arrangement));
		// }
	}
    
	static void CountValidArrangements(string unknownPart, List<int> damagedGroups) {
		var sb = new StringBuilder(unknownPart);
		var startIndex = 0;
		foreach (var damagedGroup in damagedGroups) {
			sb.Replace('?', '#', startIndex, damagedGroup);
			startIndex += damagedGroup;
			sb.Replace('?', '.', startIndex, 1);
			startIndex++;
		}
		Console.WriteLine(sb.ToString());
	}
    
	static List<string> GenerateArrangements(int unknownCount, List<int> damagedGroups) {
		var arrangements = new List<string>();
		GenerateArrangementsHelper(arrangements, new char[unknownCount], 0, damagedGroups, 0);
		return arrangements;
	}
    
	static void GenerateArrangementsHelper(List<string> arrangements, char[] currentArrangement, int currentIndex, List<int> damagedGroups, int damagedIndex) {
		if (damagedIndex == damagedGroups.Count) {
			arrangements.Add(new string(currentArrangement));
			return;
		}
    
		var damagedGroupSize = damagedGroups[damagedIndex];
    
		for (var i = currentIndex; i <= currentArrangement.Length - damagedGroupSize; i++) {
			var validPlacement = true;
    
			for (var j = i; j < i + damagedGroupSize; j++) {
				if (currentArrangement[j] == '?')
					currentArrangement[j] = '#';
				else {
					validPlacement = false;
					break;
				}
			}
    
			if (validPlacement) {
				GenerateArrangementsHelper(arrangements, currentArrangement, i + damagedGroupSize + 1, damagedGroups,
					damagedIndex + 1);
    
				for (var j = i; j < i + damagedGroupSize; j++) {
					currentArrangement[j] = '.';
				}
			}
		}
	}
}