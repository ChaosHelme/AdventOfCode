// See https://aka.ms/new-console-template for more information
using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return -1;
}

var totalScore = 0;

foreach (var line in lines) {
	var cardNumber = line[..line.IndexOf(':')].Trim();
	var drawnWinningNumbers = line[(line.IndexOf(':')+1)..line.IndexOf('|')].Trim().Split(' ');
	var drawnNumbers = line[(line.IndexOf('|')+1)..].Trim().Split(' ');
	var winningNumbers = new List<int>(drawnWinningNumbers.Length);

	winningNumbers.AddRange(from drawnNumber in drawnNumbers 
		where !string.IsNullOrWhiteSpace(drawnNumber) 
		where drawnWinningNumbers.Contains(drawnNumber) 
		select int.Parse(drawnNumber));

	// The score is for one card one point and then doubled for each winning number
	var score = winningNumbers.Count > 0 ? 1 * Math.Pow(2, winningNumbers.Count - 1) : 0;
	totalScore += (int)score;
	
	Console.WriteLine($"{cardNumber} winning numbers: {string.Join(", ", winningNumbers)} - found {winningNumbers.Count} winning numbers score is {score}");
}

Console.WriteLine($"The total score is {totalScore}");

return 0;