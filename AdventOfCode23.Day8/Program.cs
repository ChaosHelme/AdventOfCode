using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return -1;
}

var instructionSet = lines[0];
FillElements(lines, out var elements);

var steps = TraverseElements(instructionSet, elements);
Console.WriteLine($"Total steps: {steps}");

return 0;

static void FillElements(string[] lines, out Dictionary<string, (string, string)> elements) {
	elements = new Dictionary<string, (string, string)>();
	foreach (var str in lines) {
		var parts = str.Split(new[] { ' ', '=', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);

		if (parts.Length >= 3) {
			elements[parts[0]] = (parts[1], parts[2]);
		}
	}
}

static int TraverseElements(string instructionSet, Dictionary<string, (string, string)> elements) { 
	var steps = 0;
    var currentPosition = "AAA";

    while (currentPosition != "ZZZ") {
	    var instruction = instructionSet[steps % instructionSet.Length];
	    var directions = elements[currentPosition];
	    currentPosition = instruction == 'L' ? directions.Item1 : directions.Item2;
	    steps++;
    }

	return steps;
} 