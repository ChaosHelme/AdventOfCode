using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return -1;
}

var instructionSetString = lines[0];
var instructionSet = instructionSetString.Select(c => (Instruction)Enum.Parse(typeof(Instruction), c.ToString())).ToList();
FillElements(lines, out var elements);

var startElements = FindElementsEndWithA(elements);
var steps = GetTotalStepsForAllEndWithZ(instructionSet, elements, startElements);
Console.WriteLine($"Total steps: {steps}");

return 0;

static List<string> FindElementsEndWithA(Dictionary<string, (string, string)> elements) {
	var result = new List<string>();
	foreach (var element in elements) {
		if (element.Key.EndsWith("A")) {
			result.Add(element.Key);
		}
	}
	return result;
}

static long GetTotalStepsForAllEndWithZ(List<Instruction> instructionSet, Dictionary<string, (string, string)> elements, List<string> startElements) {
	var lengthList = new List<long>();
	var visited = new List<string>();
	foreach (var startElement in startElements) {
		visited.Clear();
		var currentElement = startElement;
		while (!currentElement.EndsWith("Z")) {
			var instruction = instructionSet[visited.Count % instructionSet.Count];
			
			var directions = elements[currentElement];
			var newElement = GetElementByInstruction(instruction, directions);
			currentElement = newElement;
			visited.Add(currentElement);
		}

		lengthList.Add(visited.Count);
	}

	// calculate LCM of all collected lengths
	var totalSteps = lengthList.Aggregate(Lcm);
	return totalSteps;
}

static long Gcd(long a, long b) {
	if (b == 0) {
		return a;
	}

	return Gcd(b, a % b);
}

static long Lcm(long a, long b) {
	return Math.Abs(a * b) / Gcd(a, b);
}

static string GetElementByInstruction(Instruction instruction, (string, string) directions) {
	return instruction == Instruction.L
		? directions.Item1
		: directions.Item2;
}

static void FillElements(string[] lines, out Dictionary<string, (string, string)> elements) {
	elements = new Dictionary<string, (string, string)>();
	foreach (var line in lines) {
		var parts = line.Split(new[] { ' ', '=', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);

		if (parts.Length >= 3) {
			elements[parts[0]] = (parts[1], parts[2]);
		}
	}
}

enum Instruction { L, R }