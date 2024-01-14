using AdventOfCode23.Day11;
using AdventOfCode23.Shared;

var lines = await FileHelper.ValidateAndReadInputFileAsync("Input.txt");
if (lines.Length < 1) {
	return -1;
}

var cosmos = new Cosmos(lines);

Console.WriteLine("Original:");
cosmos.PrintOriginalCosmos();

var expanded = cosmos.ExpandCosmos();

Console.WriteLine($"\nExpanding columns by {expanded.Item1}");
Console.WriteLine($"Expanding rows by {expanded.Item2}");

Console.WriteLine("\nExpanded:");
cosmos.PrintExpandedCosmos();

Console.WriteLine($"\nAmount of universes: {cosmos.CountAmountOfUniverse()}");

cosmos.AssignUniqueNumberToUniverses();
cosmos.PrintUniverses();

Console.WriteLine($"\nCombination of universes: {cosmos.CalculateUniqueCombinationsOfUniverses()}");

return 0;