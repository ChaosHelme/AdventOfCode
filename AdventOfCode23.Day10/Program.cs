using System.Diagnostics;
using AdventOfCode23.Day10;
using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return -1;
}

var pipePath = new PipePath(lines);
var stopWatch = new Stopwatch();

stopWatch.Start();
var loop = pipePath.FindLoop();
stopWatch.Stop();

if (loop == null) {
	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine("No loop found.");
	return Int32.MinValue;
}

Console.WriteLine($"Loop containing {loop.Count} pipes found in {stopWatch.Elapsed.TotalMilliseconds} ms.");

var farthestPipe = pipePath.FindFarthestPipeWithDistance(loop);
pipePath.PrintLoopInGridHighlightingFarthestPipe(loop, (farthestPipe.Item1, farthestPipe.Item2));

Console.WriteLine($"First pipe position: {loop[0]} - Last pipe position: {loop[^1]}");
Console.WriteLine($"The farthest pipe is at position {farthestPipe} with a distance of {farthestPipe.Item3} steps from the start position.");



// stopWatch.Start();
// var longestLoop = pipePath.FindLongestLoop();
// stopWatch.Start();
// if (longestLoop != null) {
// 	pipePath.MarkLongestPathInGrid(longestLoop);
// 	//pipePath.PrintGrid();
// 	Console.WriteLine($"Longest loop found with a length of {longestLoop.Count} in {stopWatch.Elapsed.TotalSeconds} s.");
// }
// else
// {
// 	Console.WriteLine("No loop found.");
// }


return 0;
