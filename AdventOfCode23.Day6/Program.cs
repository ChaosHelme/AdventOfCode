using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return -1;
}

var races = new List<Race>();

// Assuming that the first line is for time/duration
var times = lines[0].Split(["Time:", " "], StringSplitOptions.RemoveEmptyEntries);
var durations = Array.ConvertAll(times, int.Parse);
// Assuming that the second line is for distances
var distances = lines[1].Split(["Distance:", " "], StringSplitOptions.RemoveEmptyEntries);
var recordDistances = Array.ConvertAll(distances, int.Parse);

for (var i = 0; i < durations.Length; i++) {
	races.Add(new Race(durations[i], recordDistances[i]));
	Console.WriteLine(races[i].ToString());
}

return 0;

record struct Race(int durationInMs, int recordDistanceInMm);