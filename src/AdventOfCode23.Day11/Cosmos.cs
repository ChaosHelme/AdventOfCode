using System.Text;

namespace AdventOfCode23.Day11;

public class Cosmos {
	readonly IReadOnlyList<string> originalCosmos;
	readonly List<string> expandedCosmos;
	readonly List<Universe> universes;

	public Cosmos(IReadOnlyList<string> originalCosmos) {
		this.originalCosmos = originalCosmos;
		this.expandedCosmos = this.originalCosmos.ToList();
		this.universes = new List<Universe>();
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
	
	public void PrintUniverses() {
		foreach (var universe in this.universes) {
			Console.WriteLine($"Universe {universe.UniverseId} at ({universe.Row}, {universe.Column})");
			foreach (var partnerId in universe.PartnerUniverseIds) {
				Console.WriteLine($"  Partner {partnerId}");
			}
		}
	}
	
	public void PrintShortestDistanceBetweenUniverseCombinations() {
		Console.WriteLine("\nShortest distance between universe combinations:");
		// Print the whole cosmos with the shortest distance between each universe combination
		var rows = this.expandedCosmos.Count;
		var cols = this.expandedCosmos[0].Length;

		for (var row = 0; row < rows; row++) {
			var line = this.expandedCosmos[row];
			for (var col = 0; col < cols; col++) {
				var c = line[col];
				if (c == '#') {
					var universe = this.universes.First(t => t.Row == row && t.Column == col);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(universe.UniverseId);
				} else {
					var step = this.universes.SelectMany(u => u.Steps).FirstOrDefault(t => t.Row == row && t.Column == col);
					if (step.Row != 0 && step.Column != 0) {
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.Write('#');
					} else {
						Console.ResetColor();
						Console.Write(c);
					}
				}
			}
			Console.ResetColor();
			Console.WriteLine();
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

	public void AssignUniqueNumberToUniverses() {
		// Assign a unique number to each universe (#-Symbol)
		var universeNumber = 1;
		for (var row = 0; row < this.expandedCosmos.Count; row++) {
			var line = this.expandedCosmos[row];
			if (!line.Contains('#')) {
				continue;
			}

			for (var col = 0; col < line.Length; col++) {
				if (line[col] == '#') {
					this.universes.Add(new Universe(universeNumber, row, col, new List<int>(), new List<Step>()));
					universeNumber++;
				}
			}
		}

		for (var i = 0; i < this.universes.Count; i++) {
			var startUniverse = this.universes[i];
			for (var j = i+1; j < this.universes.Count; j++) {
				var endUniverse = this.universes[j];
				startUniverse.PartnerUniverseIds.Add(endUniverse.UniverseId);
				endUniverse.PartnerUniverseIds.Add(startUniverse.UniverseId);
				TraverseSteps(startUniverse, endUniverse);
			}
		}
	}
	
	public int SumOfShortestDistanceBetweenUniverseCombinations() {
		var sumOfDistances = 0;
		foreach (var universe in this.universes) {
			foreach (var partnerUniverseId in universe.PartnerUniverseIds) {
				
			}
		}
		// Calculate the shortest distance between each universe combination
		for (var i = 0; i < this.universes.Count; i++) {
			var universe1 = this.universes[i];
			for (var j = i + 1; j < this.universes.Count; j++) {
				var universe2 = this.universes[j];
				var distance = Math.Abs(universe1.Row - universe2.Row) + Math.Abs(universe1.Column - universe2.Column);
				sumOfDistances += distance;
			}
		}

		return sumOfDistances;
	}

	void TraverseSteps(Universe startUniverse, Universe endUniverse) {
		// Traverse the steps between the two universes
		var row = startUniverse.Row;
		var col = startUniverse.Column;
		var steps = new List<Step>();
		while (row != endUniverse.Row || col != endUniverse.Column) {
			if (row < endUniverse.Row) {
				row++;
				steps.Add(new Step(row, col));
			} else if (row > endUniverse.Row) {
				row--;
				steps.Add(new Step(row, col));
			} else if (col < endUniverse.Column) {
				col++;
				steps.Add(new Step(row, col));
			} else if (col > endUniverse.Column) {
				col--;
				steps.Add(new Step(row, col));
			}
		}

		startUniverse.Steps.AddRange(steps);
	}

	public long CalculateUniqueCombinationsOfUniverses() {
		return CalculateCombinations(this.universes.Count, 2);
	}
	
	// Use combination formula to calculate the amount of combinations
	// https://en.wikipedia.org/wiki/Combination
	// In order to prevent overflow, we cancel out common factors
	static long CalculateCombinations(int n, int k) {
		// Ensure k is not greater than n
		k = Math.Min(k, n - k);

		long result = 1;

		// Calculate C(n, k) by canceling out common factors
		for (var i = 0; i < k; i++) {
			result *= (n - i);
			result /= (i + 1);
		}

		return result;
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