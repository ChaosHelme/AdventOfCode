// See https://aka.ms/new-console-template for more information
using AdventOfCode23.Shared;

var lines = await FileHelper.ValidateAndReadInputFileAsync("Input.txt");
if (lines.Length < 1) {
	return -1;
}

Part1(lines);
Console.WriteLine();
Part2(lines);

return 0;

static void Part1(IReadOnlyList<string> lines) {
	Console.WriteLine("Part 1:");
	Console.WriteLine("-------");
	
	var totalScore = 0;

	foreach (var line in lines) {
		var cardNumber = line[..line.IndexOf(':')].Trim();
		var winningNumbers = GetWinningNumbers(line);

		// The score is for one card one point and then doubled for each winning number
		var score = winningNumbers.Count > 0 ? 1 * Math.Pow(2, winningNumbers.Count - 1) : 0;
		totalScore += (int)score;
	
		Console.WriteLine($"{cardNumber} winning numbers: {string.Join(", ", winningNumbers)} - found {winningNumbers.Count} winning numbers score is {score}");
	}

	Console.WriteLine($"The total score is {totalScore}");
}

static void Part2(IReadOnlyList<string> lines) {
	Console.WriteLine("Part 2:");
	Console.WriteLine("-------");
	
	var cardAmounts = new Dictionary<int, int> {
		// We can add the first card directly cause there will not be any copies of it
		{0, 0},
	};

	// Loop through the cards and check how many copies of each card we have
	for (var cardNumber = 0; cardNumber < lines.Count; cardNumber++) {
		// Get the amount of winning numbers for the current card
		var winningNumbersCount = GetWinningNumbersCount(lines[cardNumber]);
		// Check if we have copies of the current card
		cardAmounts.TryGetValue(cardNumber, out var copiesOfCurrentCard);

		// Loop through the copies of the current card and increase the amount of cards for each winning number
		while (copiesOfCurrentCard >= 0) {
			for (var i = 1; i <= winningNumbersCount; i++) {
				if (!cardAmounts.TryAdd(cardNumber + i, 1)) {
					cardAmounts[cardNumber + i]++;
				}
			}

			copiesOfCurrentCard--;
		}
		
		// Add the original card itself
		if (!cardAmounts.TryAdd(cardNumber, 1)) {
			cardAmounts[cardNumber]++;	
		}
	}
	
	Console.WriteLine($"Total amount of cards: {cardAmounts.Values.Sum()}");
}

static List<int> GetWinningNumbers(string line) {
	var winningNumbers = new List<int>();
	var drawnWinningNumbers = line[(line.IndexOf(':')+1)..line.IndexOf('|')].Trim().Split(' ');
	var drawnNumbers = line[(line.IndexOf('|')+1)..].Trim().Split(' ');

	winningNumbers.AddRange(from drawnNumber in drawnNumbers 
		where !string.IsNullOrWhiteSpace(drawnNumber) 
		where drawnWinningNumbers.Contains(drawnNumber) 
		select int.Parse(drawnNumber));

	return winningNumbers;
}

static int GetWinningNumbersCount(string line) {
	var drawnWinningNumbers = line[(line.IndexOf(':')+1)..line.IndexOf('|')].Trim().Split(' ');
	var drawnNumbers = line[(line.IndexOf('|')+1)..].Trim().Split(' ');
	
	return (from drawnNumber in drawnNumbers 
		where !string.IsNullOrWhiteSpace(drawnNumber) 
		where drawnWinningNumbers.Contains(drawnNumber) 
		select drawnNumber).Count();
}