using AdventOfCode23.Day9;
using AdventOfCode.Shared;

var lines = await FileHelper.ValidateAndReadInputFileAsync("Input.txt");
if (lines.Length < 1) {
	return -1;
}

var sequenceExtrapolator = new SequenceExtrapolator();
var sumOfAllNextValues = 0;
foreach (var line in lines) {
    var sequence = line.Split(' ').Select(int.Parse).ToList();
    
    sequenceExtrapolator.BuildHistories(sequence);
    
    var nextValue = sequenceExtrapolator.PredictNext();
    var previousValue = sequenceExtrapolator.PredictPrevious();
    sumOfAllNextValues += nextValue;
    
    Console.WriteLine($"The predicted next value for the history {line} is: {nextValue} the previous value is: {previousValue}");
    sequenceExtrapolator.PrintHistories();
}

Console.WriteLine($"The sum of all extrapolated values is: {sumOfAllNextValues}");

return 0;