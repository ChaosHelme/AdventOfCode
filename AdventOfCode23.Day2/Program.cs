// See https://aka.ms/new-console-template for more information
using AdventOfCode.Shared;

var lines = await FileHelper.ValidateAndReadInputFileAsync("Input.txt");
if (lines.Length < 1) {
	return;
}
var possibleGameIds = new List<int>();
var gameCubePowers = new List<int>();

const int maxAmountOfRedCubes = 12;
const int maxAmountOfBlueCubes = 14;
const int maxAmountOfGreenCubes = 13;

foreach (var line in lines) {
	var gameId = ValidateGamesForMaxCubes(line);
	gameCubePowers.Add(CalculatePowerOfCubes(line));
	if (!string.IsNullOrEmpty(gameId)) {
		possibleGameIds.Add(int.Parse(gameId));
	}
}

Console.WriteLine("Part 1");
Console.WriteLine("--------------------------------------------------");
Console.WriteLine($"Possible game Ids: {string.Join(',', possibleGameIds)}");
Console.WriteLine($"Sum of possible game Ids: {possibleGameIds.Sum()}");

Console.WriteLine();

Console.WriteLine("Part 2");
Console.WriteLine("--------------------------------------------------");
Console.WriteLine($"Cube powers: {string.Join(',', gameCubePowers)}");
Console.WriteLine($"Sum of cube powers: {gameCubePowers.Sum()}");

return;

static string GetGameId(string gameInput) {
	return gameInput[..gameInput.IndexOf(':')].Remove(0, 5);
}

static string GetGameOutput(string gameInput) {
	return gameInput[gameInput.IndexOf(':')..].Remove(0,2);
}

static string[] GetCubeSets(string gameOutput) {
	return gameOutput.Split("; ");
}

// Part 1
static string ValidateGamesForMaxCubes(string gameInput) {
	var gameId = GetGameId(gameInput);
	var gameOutput = GetGameOutput(gameInput);
	var cubeSets = GetCubeSets(gameOutput);
	
	foreach (var cubeSet in cubeSets) {
		var cubes = cubeSet.Split(", ");
		foreach (var cube in cubes) {
			var cubeAmount = int.Parse(cube[..cube.IndexOf(' ')]);
			var cubeColor = cube[cube.IndexOf(' ')..].Remove(0,1);
		
			switch (cubeColor) {
				case "red":
					if (cubeAmount > maxAmountOfRedCubes) {
						return string.Empty;
					}
					break;
				case "blue":
					if (cubeAmount > maxAmountOfBlueCubes) {
						return string.Empty;
					}
					break;
				case "green":
					if (cubeAmount > maxAmountOfGreenCubes) {
						return string.Empty;
					}
					break;
			}
		}
	}
	
	return gameId;
}

// Part 2
// Find the minimum set of cubes that can be used to build a game
// Return the multiplication of the cubes
static int CalculatePowerOfCubes(string gameInput) {
	var powerOfCubes = 0;
	var minAmountOfRedCubes = 0;
	var minAmountOfBlueCubes = 0;
	var minAmountOfGreenCubes = 0;
	var gameOutput = GetGameOutput(gameInput);
	var cubeSets = GetCubeSets(gameOutput);
	
	foreach (var cubeSet in cubeSets) {
		var cubes = cubeSet.Split(", ");
		foreach (var cube in cubes) {
			var cubeAmount = int.Parse(cube[..cube.IndexOf(' ')]);
			var cubeColor = cube[cube.IndexOf(' ')..].Remove(0,1);

			switch (cubeColor) {
				case "red":
					if (cubeAmount > minAmountOfRedCubes) {
						minAmountOfRedCubes = cubeAmount;
					}
					break;
				case "green":
					if (cubeAmount > minAmountOfGreenCubes) {
						minAmountOfGreenCubes = cubeAmount;
					}
					break;
				case "blue":
					if (cubeAmount > minAmountOfBlueCubes) {
						minAmountOfBlueCubes = cubeAmount;
					}
					break;
			}
		}
	}
	
	powerOfCubes = minAmountOfRedCubes * minAmountOfBlueCubes * minAmountOfGreenCubes;
	
	return powerOfCubes;
}