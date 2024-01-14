using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return -1;
}

Console.WriteLine("Original:");
PrintGrid(lines);

var expanded = ExpandCosmos(lines);

Console.WriteLine("\nExpanded:");
PrintGrid(expanded);return 0;

static void PrintGrid(IEnumerable<string> grid) {
	foreach (var line in grid) {
		Console.WriteLine(line);
	}
}

static List<string> ExpandCosmos(string[] input) {
	var rows = input.Length;
	var cols = input[0].Length;

	var result = input.ToList();
	for (var i = 0; i < rows; i++) {
		if (!result[i].Contains('#')) {
			result.Insert(i, result[i]);
			rows++;
			i++;
		}
	}

	return result;
}