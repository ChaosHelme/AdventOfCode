using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return -1;
}

var sumOfAllNextValues = 0;
foreach (var line in lines) {
    var sequence = line.Split(' ').Select(int.Parse).ToList();
    var nextValue = PredictNext(sequence);
    sumOfAllNextValues += nextValue;
    
    Console.WriteLine($"The predicted next value for the history {line} is: {nextValue}");
}

Console.WriteLine($"The sum of all extrapolated values is: {sumOfAllNextValues}");

return 0;

static List<int> CalculateDifferences(List<int> original) {
    var differences = new List<int>();
    for (var i = 1; i < original.Count; i++) {
        differences.Add(original[i] - original[i - 1]);
    }

    return differences;
}

static int PredictNext(List<int> sequence) {
    var differences = CalculateDifferences(sequence);

    if (differences.All(x => x == 0)) {
        return sequence.Last();
    }

    return sequence.Last() + PredictNext(differences);
}