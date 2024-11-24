// See https://aka.ms/new-console-template for more information
using AdventOfCode23.Day5;
using AdventOfCode.Shared;

var lines = await FileHelper.ValidateAndReadInputFileAsync("Input.txt");
if (lines.Length < 1) {
	return -1;
}

var cancellationTokenSource = new CancellationTokenSource();

var almanac = new Almanac(lines);
almanac.InitializeRanges();

var startTime = DateTime.Now;
Console.WriteLine($"Lowest location found synchronously: {almanac.ProcessAllSeeds()}");
var endTime = DateTime.Now;
Console.WriteLine($"Synchronous method ran {(endTime-startTime).TotalSeconds}s");

startTime = DateTime.Now;
var lowestLocation = await almanac.ProcessAllSeedsInParallelAsync(cancellationTokenSource.Token);
Console.WriteLine($"Lowest location found asynchronously: {lowestLocation}");
endTime = DateTime.Now;
Console.WriteLine($"Asynchronous method ran {(endTime-startTime).TotalSeconds}s");

return 0;