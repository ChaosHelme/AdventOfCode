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
var giantLoop = pipePath.FindGiantLoop();
stopWatch.Stop();

if (giantLoop == null) {
	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine("No giant loop found.");
	return Int32.MinValue;
}

Console.WriteLine($"Giant loop containing {giantLoop.Count} pipes found in {stopWatch.Elapsed.TotalMilliseconds} ms.");

stopWatch.Reset();




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
