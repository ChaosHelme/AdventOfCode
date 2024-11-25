using AdventOfCode.Shared;

namespace AdventOfCode23.Day10;

public sealed class AdventOfCodeModule : IAdventOfCodeModule
{
	public DateOnly AoCYear => DateOnly.Parse("2023-12-10");
	public int Day => 10;
	public string Name => "*** Advent Of Code 23 Day 10 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		if (input.Length <= 0)
		{
			return ValueTask.CompletedTask;
		}
		
		var pipePath = new PipePath(input);

		var loop = pipePath.FindLoop();

		if (loop == null) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine("No loop found.");
			return ValueTask.FromException(new InvalidOperationException("No loop found."));
		}

		Console.WriteLine($"Loop containing {loop.Count} pipes found.");

		var farthestPipe = pipePath.FindFarthestPipe(loop);
		pipePath.PrintLoopInGridHighlightingFarthestPipe(loop, (farthestPipe.Item1, farthestPipe.Item2));

		Console.WriteLine($"First pipe position: {loop[0]} - Last pipe position: {loop[^1]}");
		Console.WriteLine($"The farthest pipe is at position {farthestPipe} with a distance of {farthestPipe.Item3} steps from the start position.");


		// Console.WriteLine("\nYou can check if a given pipe is in the loop by inputting its position in the grid.");
		// Console.WriteLine("Enter 'q' to quit.");
		//
		// while (true) {
		// 	if (!TryGetUserInput(out var row, "Enter row") || !TryGetUserInput(out var column, "Enter column")) {
		// 		break;
		// 	}
		//
		// 	if (Int32.TryParse(row, out var rowNumber) && Int32.TryParse(column, out var columnNumber)) {
		// 		var pipe = (rowNumber, columnNumber);
		// 		var index = loop.IndexOf(pipe);
		// 		if (index >= 0) {
		// 			Console.WriteLine($"Pipe at position {pipe} is in the loop at index {index}");
		// 		} else {
		// 			Console.WriteLine($"Pipe at position {pipe} is not in the loop.");
		// 		}
		// 	}
		// }
		
		return ValueTask.CompletedTask;
	}
	
	static bool TryGetUserInput(out string userInput, string prompt) {
		Console.Write($"{prompt}: ");
		userInput = Console.ReadLine();

		return !userInput.ToLower().Equals("q");
	}
}