using System.Text;
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
	var result = input.ToList();
	
	// Expand columns if they don't contain a #
	ExpandColumns(result);
	ExpandRows(result);
	
	return result;
}

static void ExpandColumns(List<string> grid) {
	var cols = grid[0].Length;
	var expandedCols = new HashSet<int>();
	var sb = new StringBuilder();
	// Check if we need to expand the grid
	for (var col = 0; col < cols; col++) {
		var count = 0;
		for (var row = 0; row < grid.Count; row++) {
			if (grid[row][col] == '#') {
				count++;
			}
		}
		
		if (count == 0) {
			// Expand column
			expandedCols.Add(col);
		}
	}
	
	if (expandedCols.Count == 0) {
		return;
	}

	// Expand the grid
	for (var index = 0; index < grid.Count; index++) {
		sb.Clear();
		sb.Append(grid[index]);
		var count = 0;
		foreach (var col in expandedCols) {
			sb.Insert(col + count, '.');
			count++;
		}
		grid[index] = sb.ToString();
	}
}

static void ExpandRows(List<string> result) {
	for (var row = 0; row < result.Count; row++) {
		// Expand rows if they don't contain a #
		if (!result[row].Contains('#')) {
			result.Insert(row, result[row]);
			row++;
		}
	}
}