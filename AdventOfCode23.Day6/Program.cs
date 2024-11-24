// using AdventOfCode.Shared;
//
// var lines = await FileHelper.ValidateAndReadInputFileAsync("Input.txt");
// if (lines.Length < 1) {
// 	return -1;
// }
//
// Part1(lines);
// Console.WriteLine("");
// Part2(lines);
//
// return 0;
//
// static void Part1(IReadOnlyList<string> lines) {
// 	Console.WriteLine("Part 1");
// 	Console.WriteLine("----------");
// 	
// 	// Assuming that the first line is for time/duration
// 	var times = lines[0].Split(["Time:", " "], StringSplitOptions.RemoveEmptyEntries);
// 	var durations = Array.ConvertAll(times, int.Parse);
// 	// Assuming that the second line is for distances
// 	var distances = lines[1].Split(["Distance:", " "], StringSplitOptions.RemoveEmptyEntries);
// 	var recordDistances = Array.ConvertAll(distances, int.Parse);
//
// 	if (durations.Length != recordDistances.Length) {
// 		Console.ForegroundColor = ConsoleColor.Red;
// 		Console.Error.WriteLine("Need the same amount of times and distances to process the input");
// 		Console.ResetColor();
// 		return;
// 	}
//
// 	var races = new Dictionary<int, List<Race>>();
// 	for (var i = 0; i < durations.Length; i++) {
// 		var newRecordRaces = new List<Race>();
// 		var recordDistanceMm = recordDistances[i];
// 		var maxRaceDurationMs = durations[i];
//
// 		for (var buttonPressedMs = 0; buttonPressedMs <= maxRaceDurationMs; buttonPressedMs++) {
// 			var remainingTime = maxRaceDurationMs - buttonPressedMs;
// 			var boatTravelDistance = buttonPressedMs * remainingTime;
// 			if (boatTravelDistance > recordDistanceMm) {
// 				newRecordRaces.Add(new Race(buttonPressedMs, boatTravelDistance));
// 			}
// 		}
// 		races.Add(i+1, newRecordRaces);
// 	}
//
// 	var errorMargin = 1;
// 	foreach (var race in races) {
// 		Console.WriteLine($"Race {race.Key}:");
// 		Console.WriteLine($"Total amount of variants: {race.Value.Count}");
// 		foreach (var raceDetails in race.Value) {
// 			Console.WriteLine(raceDetails.ToString());
// 		}
// 		Console.WriteLine("--------------------------------------");
//
// 		errorMargin *= race.Value.Count;
// 	}
//
// 	Console.WriteLine($"Margin of error: {errorMargin}");
// }
//
// static void Part2(IReadOnlyList<string> lines) {
// 	Console.WriteLine("Part 2");
// 	Console.WriteLine("---------");
// 	
// 	// Assuming that the first line is for time/duration
// 	var times = lines[0].Split(["Time:", " "], StringSplitOptions.RemoveEmptyEntries);
// 	var timesMerged = string.Join("", times);
// 	// Assuming that the second line is for distances
// 	var distances = lines[1].Split(["Distance:", " "], StringSplitOptions.RemoveEmptyEntries);
// 	var distancesMerged = string.Join("", distances);
//
// 	var raceDuration = long.Parse(timesMerged);
// 	var recordDistance = long.Parse(distancesMerged);
//
//
// 	var newRecordRaces = 0;
//
// 	for (var buttonPressedMs = 0; buttonPressedMs <= raceDuration; buttonPressedMs++) {
// 		var remainingTime = raceDuration - buttonPressedMs;
// 		var boatTravelDistance = buttonPressedMs * remainingTime;
// 		if (boatTravelDistance > recordDistance) {
// 			newRecordRaces++;
// 		}
// 	}
//
// 	Console.WriteLine($"Total amount of variants: {newRecordRaces}");
// }
//
// internal record struct Race(int ButtonPressDurationInMs, int DistanceTraveledInMm);