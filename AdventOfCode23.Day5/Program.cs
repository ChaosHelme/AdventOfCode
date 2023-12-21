// See https://aka.ms/new-console-template for more information
using AdventOfCode23.Day5;
using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("InputExample.txt");
if (lines == null) {
	return -1;
}

Part1(lines);

return 0;

static void Part1(string[] lines) {
	Console.WriteLine("Part 1:");
	Console.WriteLine("-------");
	
	var almanac = new Almanac(lines);
	almanac.Initialize();
	almanac.PrintMaps();
	
	almanac.PrintSeedsToLocations();
	
	Console.WriteLine($"Lowest location: {almanac.FindLowestLocation()}");
}