using System.Text;

namespace AdventOfCode23.Day11;

public class Cosmos {
	readonly IReadOnlyList<string> originalCosmos;
	readonly List<string> expandedCosmos;

	public Cosmos(IReadOnlyList<string> originalCosmos) {
		this.originalCosmos = originalCosmos;
		this.expandedCosmos = this.originalCosmos.ToList();
	}

	public void PrintOriginalCosmos() {
		foreach (var line in this.originalCosmos) {
			WriteLineWithColoredLetter(line, '#', ConsoleColor.Green);
		}
    }

	public void PrintExpandedCosmos() {
		foreach (var line in this.expandedCosmos) {
			WriteLineWithColoredLetter(line, '#', ConsoleColor.Green);
		}
	}
    
	public (int, int) ExpandCosmos() {
    	var expandedColumns = ExpandColumns(this.expandedCosmos);
    	var expandedRows = ExpandRows(this.expandedCosmos);
    
    	return (expandedColumns, expandedRows);
    }

	public int CountAmountOfUniverse() {
		return this.originalCosmos.SelectMany(t => t).Count(t => t == '#');
	}
	
	static void WriteLineWithColoredLetter(string letters, char c, ConsoleColor color) {
		var index = letters.IndexOf(c);
		var startIndex = 0;
		while (index > -1) {
			Console.Write(letters.Substring(startIndex, index - startIndex));
			Console.ForegroundColor = color;
			Console.Write(letters[letters.IndexOf(c)]);
			Console.ResetColor();
			startIndex = index + 1;
			index = letters.IndexOf(c, startIndex);
		}
		Console.WriteLine(letters[startIndex..]);
	}

	// Expand columns if they don't contain a #
    static int ExpandColumns(List<string> grid) {
    	var sb = new StringBuilder();
    	var expandedColumns = 0;
    	// Check if we need to expand the grid
    	for (var col = 0; col < grid[0].Length; col++) {
    		// ReSharper disable once AccessToModifiedClosure
    		var containsUniverse = grid.Any(t => t[col] == '#');
    
    		if (!containsUniverse) {
    			// Expand the grid
    			for (var index = 0; index < grid.Count; index++) {
    				sb.Clear();
    				sb.Append(grid[index]);
    				sb.Insert(col, '.');
    
    				grid[index] = sb.ToString();
    			}
    			col++;
    			expandedColumns++;
    		}
    	}
    
    	return expandedColumns;
    }
    
    static int ExpandRows(List<string> result) {
    	var expandedRows = 0;
    	for (var row = 0; row < result.Count; row++) {
    		// Expand rows if they don't contain a #
    		if (!result[row].Contains('#')) {
    			result.Insert(row, result[row]);
    			row++;
    			expandedRows++;
    		}
    	}
    
    	return expandedRows;
    }
}