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

static void PrintGrid(string[] grid) {
	foreach (var line in grid) {
		Console.WriteLine(line);
	}
}

static string[] ExpandCosmos(string[] input) {
	var rows = input.Length;
	var cols = input[0].Length;

	var expandedRows = rows;
	var expandedCols = cols;

	for (var i = 0; i < rows; i++) {
		if (!input[i].Contains('#')) {
			expandedRows++;
		}
	}

	for (var j = 0; j < cols; j++) {
		if (!input.Any(row => row[j] == '#')) {
			expandedCols++;
		}
	}

	var result = input.Select(row => row.PadRight(expandedCols, '.')).ToList();
	

	for (int i = 0; i < expandedRows; i++) {
		if (!input[i].Contains('#')) {
			result[i+1] = input[i];
			i++;
		} else {
			result[i] = input[i];
		}
	}

	// for (var j = 0; j < expandedCols; j++) {
	// 	if (!input.Any(row => row[j] == '#')) {
	// 		for (var i = 0; i < expandedRows; i += 2) {
	// 			result[i] = result[i].Insert(j * 2, result[i][j * 2].ToString());
	// 		}
	// 	}
	// }

	return result.ToArray();
}