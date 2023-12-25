using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return -1;
}

var cardsAndWeight = new Dictionary<string, int> {
	{ "A", 0 },
	{ "K", 1 },
	{ "Q", 2 },
	{ "J", 3 },
	{ "T", 4 },
	{ "9", 5 },
	{ "8", 6 },
	{ "7", 7 },
	{ "6", 8 },
	{ "5", 9 },
	{ "4", 10 },
	{ "3", 11 },
	{ "2", 12 },
};

var handsAndBets = new List<(string, int)>(lines.Length);
handsAndBets.AddRange(lines.Select(line => (line.Split(' ')[0], int.Parse(line.Split(' ')[1]))));

foreach (var handAndBet in handsAndBets) {
	Console.WriteLine($"Hand: {handAndBet.Item1} / Bet: {handAndBet.Item2}");
}


return 0;