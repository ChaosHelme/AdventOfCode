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
    		Console.WriteLine(line);
    	}
    }

	public void PrintExpandedCosmos() {
		foreach (var line in this.expandedCosmos) {
			Console.WriteLine(line);
		}
	}
    
	public (int, int) ExpandCosmos() {
    	var expandedColumns = ExpandColumns(this.expandedCosmos);
    	var expandedRows = ExpandRows(this.expandedCosmos);
    
    	return (expandedColumns, expandedRows);
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