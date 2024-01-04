namespace AdventOfCode23.Day10;

public class PipePath(string[] pipeGrid) {
	// These are the symbols used for the pipes:
	//  | is a vertical pipe connecting north and south.
	//  - is a horizontal pipe connecting east and west.
	// 	L is a 90-degree bend connecting north and east.
	// 	J is a 90-degree bend connecting north and west.
	//  7 is a 90-degree bend connecting south and west.
	// 	F is a 90-degree bend connecting south and east.
	// 	. is ground; there is no pipe in this tile.

	enum Direction {
		North,
		South,
		East,
		West,
	}

	public void PrintLongestLoopInGrid(List<(int, int)> longestPath) {
		for (var row = 0; row < pipeGrid.Length; row++) {
			var line = pipeGrid[row];
			for (var col = 0; col < line.Length; col++) {
				var index = longestPath.IndexOf((row, col));
				if (index >= 0) {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write($"{(index == 0 ? 'S' : pipeGrid[row][col])}");
					Console.ResetColor();
					continue;
				}

				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write($"{pipeGrid[row][col]}");
				Console.ResetColor();
			}

			Console.WriteLine();
		}
	}

	public List<(int, int)>? FindLongestLoop() {
		var rows = pipeGrid.Length;
		var cols = pipeGrid[0].Length;

		// Find the starting position marked with 'S'
		var start = FindStartingPosition(rows, cols);
		if (start == (-1, -1)) {
			Console.WriteLine("Starting position 'S' not found.");
			return null;
		}

		var startPositionPipeType = IdentifyPipeType(start.Item1, start.Item2);
		// Change the starting position to the correct pipe type
		pipeGrid[start.Item1] = pipeGrid[start.Item1].Remove(start.Item2, 1).Insert(start.Item2, startPositionPipeType.ToString());

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

	char IdentifyPipeType(int row, int col) {
		var rows = pipeGrid.Length;
		var cols = pipeGrid[0].Length;

		// Check the surrounding positions to determine the pipe type
		var above = (row > 0) ? pipeGrid[row - 1][col] : ' ';
		var below = (row < rows - 1) ? pipeGrid[row + 1][col] : ' ';
		var left = (col > 0) ? pipeGrid[row][col - 1] : ' ';
		var right = (col < cols - 1) ? pipeGrid[row][col + 1] : ' ';

		if (above == '|' || below == '|' || (above == 'F' && below == '7') || (above == '7' && below == 'F')) {
			return '|'; // Vertical pipe
		}

		if (left == '-' || right == '-') {
			return '-'; // Horizontal pipe
		}

		if (above == '|' && right == '-') {
			return 'L'; // 90-degree bend connecting north and east
		}

		if (above == '|' && left == '-' || (above == '7' && left == 'F') || (above == 'F' && left == 'L')) {
			return 'J'; // 90-degree bend connecting north and west
		}

		if (below == '|' && left == '-') {
			return '7'; // 90-degree bend connecting south and west
		}

		if (below == '|' && right == '-') {
			return 'F'; // 90-degree bend connecting south and east
		}

		return '.'; // Ground (no pipe)
	}

	static bool IsPipe(char symbol) => symbol == '|' || symbol == '-' || symbol == 'L' || symbol == 'J' ||
	                                   symbol == '7' || symbol == 'F';

	static bool CanConnect(char currentPipe, char nextPipe, Direction nextDirection) {
		switch (currentPipe) {
			case '|':
				return (nextPipe == '|' || nextPipe == 'L' || nextPipe == '7' || nextPipe == 'F' || nextPipe == 'J' ) &&
					(nextDirection == Direction.North || nextDirection == Direction.South);
			case '-':
				return (nextPipe == '-' || nextPipe == 'F' || nextPipe == 'J' || nextPipe == '7' || nextPipe == 'L') &&
					(nextDirection == Direction.East || nextDirection == Direction.West);
			case 'L':
				return (nextPipe == '|' || nextPipe == '-' || nextPipe == '7' || nextPipe == 'F' || nextPipe == 'J') &&
					(nextDirection == Direction.North || nextDirection == Direction.East);
			case 'J':
				return (nextPipe == '|' || nextPipe == '-' || nextPipe == 'F' || nextPipe == '7' || nextPipe == 'L') &&
					(nextDirection == Direction.North || nextDirection == Direction.West);
			case '7':
				return (nextPipe == '|' || nextPipe == '-' || nextPipe == 'J' || nextPipe == 'F' || nextPipe == 'L') &&
					(nextDirection == Direction.South || nextDirection == Direction.West);
			case 'F':
				return (nextPipe == '|' || nextPipe == '-' || nextPipe == 'L' || nextPipe == 'J' || nextPipe == '7') &&
					(nextDirection == Direction.South || nextDirection == Direction.East);
			case '.':
				return false; // Ground doesn't connect to anything
			default:
				return false; // Unknown pipe type
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
			if (row > 0 && CanConnect(pipeGrid[row][col], pipeGrid[row - 1][col], Direction.North)) {
				stack.Push((row - 1, col)); // Up
			}
			if (row + 1 < rows && CanConnect(pipeGrid[row][col], pipeGrid[row + 1][col], Direction.South)) {
				stack.Push((row + 1, col)); // Down
			}
			if (col > 0 && CanConnect(pipeGrid[row][col], pipeGrid[row][col - 1], Direction.West)) {
				stack.Push((row, col - 1)); // Left
			}
			if (col + 1 < cols && CanConnect(pipeGrid[row][col], pipeGrid[row][col + 1], Direction.East)) {
				stack.Push((row, col + 1)); // Right
			}
		}

		return loop;
	}
}
