using System.Collections.Concurrent;

namespace AdventOfCode23.Day10;

enum Connection {
	None,
	NorthAndSouth,
	NorthAndEast,
	NorthAndWest,
	EastAndWest,
	SouthAndWest,
	SouthAndEast,
}

public class PipePath(string[] pipeGrid) {
	// These are the symbols used for the pipes:
	//  | is a vertical pipe connecting north and south.
	//  - is a horizontal pipe connecting east and west.
	// 	L is a 90-degree bend connecting north and east.
	// 	J is a 90-degree bend connecting north and west.
	//  7 is a 90-degree bend connecting south and west.
	// 	F is a 90-degree bend connecting south and east.
	// 	. is ground; there is no pipe in this tile.
	readonly Dictionary<string, Connection> connections = new() {
		{ ".", Connection.None },
		{ "L", Connection.NorthAndEast },
		{ "J", Connection.NorthAndWest },
		{ "7", Connection.SouthAndWest },
		{ "F", Connection.SouthAndEast },
		{ "|", Connection.NorthAndSouth },
		{ "-", Connection.EastAndWest },
	};
	
	public void PrintGrid() {
		foreach (var line in pipeGrid) {
			Console.WriteLine(line);
		}
	}
	
	public void MarkLongestPathInGrid(List<(int, int)> longestPath) {
		var pathCounter = 0;
		for (var row = 0; row < pipeGrid.Length; row++) {
			var line = pipeGrid[row];
			for (var col = 0; col < line.Length; col++) {
				var index = longestPath.IndexOf((row, col));
				if (index >= 0) {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write($"{(index == 0 ? 'S' : 'X')}");
					Console.ResetColor();
					continue;
				}
				Console.Write($"{pipeGrid[row][col]}");
			}
			Console.WriteLine();
		}
		
		// foreach (var (row, col) in longestPath) {
		// 	var line = input[row];
		// 	var charArray = line.ToCharArray();
		// 	charArray[col] = row == 0 && col == 0 ? 'S' : 'X';
		// 	input[row] = new string(charArray);
		// }
	}

	public List<(int, int)>? FindGiantLoop() {
		var rows = pipeGrid.Length;
		var cols = pipeGrid[0].Length;

		// Find the starting position marked with 'S'
		var start = FindStartingPosition(rows, cols);
		if (start == (-1, -1)) {
			Console.WriteLine("Starting position 'S' not found.");
			return null;
		}

		// Call DFS for the starting position
		return DFS(pipeGrid, start.Item1, start.Item2);
	}

	(int, int) FindStartingPosition(int rows, int cols) {
		// We can use a simple linear search to find the starting position since the grid is 140x140
		for (var i = 0; i < rows; i++) {
			for (var j = 0; j < cols; j++) {
				if (pipeGrid[i][j] == 'S') {
					return (i, j);
				}
			}
		}

		return (-1, -1);
	}
	
	static bool IsPipe(char symbol) => symbol == '|' || symbol == '-' || symbol == 'L' || symbol == 'J' ||
	                                   symbol == '7' || symbol == 'F';

	static bool CanConnect(char pipe1, char pipe2) {
		if (!IsPipe(pipe1) || !IsPipe(pipe2)) {
			// Ground (.) cannot be connected to any pipe
			return false;
		}

		// Define the rules for connecting pipes
		switch (pipe1) {
			case '|':
				return pipe2 == '|' || pipe2 == 'L' || pipe2 == '7' || pipe2 == 'J' || pipe2 == 'F';
			case '-':
				return pipe2 == '-' || pipe2 == 'L' || pipe2 == '7' || pipe2 == 'J' || pipe2 == 'F';
			case 'L':
				return pipe2 == '|' || pipe2 == '-';
			case 'J':
				return pipe2 == '|' || pipe2 == '-';
			case '7':
				return pipe2 == '|' || pipe2 == '-';
			case 'F':
				return pipe2 == '|' || pipe2 == '-';
			default:
				return false;
		}
	}

	static List<(int, int)> DFS(string[] pipeGrid, int startRow, int startCol) {
		var rows = pipeGrid.Length;
		var cols = pipeGrid[0].Length;
		var visited = new bool[rows, cols];
		var loop = new List<(int, int)>();

		var stack = new Stack<(int, int)>(rows*cols);
		stack.Push((startRow, startCol));

		while (stack.Count > 0) {
			var current = stack.Pop();
			var row = current.Item1;
			var col = current.Item2;

			if (row < 0 || row >= rows || col < 0 || col >= cols || visited[row, col] || !IsPipe(pipeGrid[row][col])) {
				continue;
			}

			visited[row, col] = true;
			loop.Add((row, col));

			// Check to which direction we can connect the current pipe and add the next pipe to the stack
			if (row > 0 && CanConnect(pipeGrid[row][col], pipeGrid[row - 1][col])) {
				stack.Push((row - 1, col)); // Up
			}
			if (row + 1 < rows && CanConnect(pipeGrid[row][col], pipeGrid[row + 1][col])) {
				stack.Push((row + 1, col)); // Down
			}
			if (col > 0 && CanConnect(pipeGrid[row][col], pipeGrid[row][col - 1])) {
				stack.Push((row, col - 1)); // Left
			}
			if (col + 1 < cols && CanConnect(pipeGrid[row][col], pipeGrid[row][col + 1])) {
				stack.Push((row, col + 1)); // Right
			}
		}

		return loop;
	}
}
