// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("Input.txt");
var possibleGameIds = new List<int>();

const int maxAmountOfRedCubes = 12;
const int maxAmountOfBlueCubes = 14;
const int maxAmountOfGreenCubes = 13;

foreach (var line in lines) {
	var gameId = ValidateGame(line);
	if (!string.IsNullOrEmpty(gameId)) {
		possibleGameIds.Add(int.Parse(gameId));
	}
}

static string ValidateGame(string line) {
	var gameId = line[..line.IndexOf(':')].Remove(0, 5);
	var gameOutput = line[line.IndexOf(':')..].Remove(0,2);

	var cubeSets = gameOutput.Split("; ");
	
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

Console.WriteLine($"Possible game Ids: {string.Join(',', possibleGameIds)}");
Console.WriteLine($"Sum of possible game Ids: {possibleGameIds.Sum()}");