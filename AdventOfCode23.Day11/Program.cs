﻿using AdventOfCode23.Day11;
using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
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

return 0;