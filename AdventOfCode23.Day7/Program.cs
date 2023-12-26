using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return -1;
}

var cardsAndWeight = new Dictionary<string, long> {
	{ "A", 0xFFFFFFFFFFFF },
	{ "K", 0x0FFFFFFFFFFF },
	{ "Q", 0x00FFFFFFFFFF },
	{ "J", 0x000FFFFFFFFF },
	{ "T", 0x0000FFFFFFFF },
	{ "9", 0x00000FFFFFFF },
	{ "8", 0x000000FFFFFF },
	{ "7", 0x0000000FFFFF },
	{ "6", 0x00000000FFFF },
	{ "5", 0x000000000FFF },
	{ "4", 0x0000000000FF },
	{ "3", 0x00000000000F },
	{ "2", 0x000000000000 },
};

var handsAndBets = new List<(string, int)>(lines.Length);
handsAndBets.AddRange(lines.Select(line => (line.Split(' ')[0], int.Parse(line.Split(' ')[1]))));

SortHandsByWeight(cardsAndWeight, ref handsAndBets);
foreach (var handAndBet in handsAndBets) {
	Console.WriteLine($"Hand: {handAndBet.Item1} / Bet: {handAndBet.Item2}");
}

return 0;

static void SortHandsByWeight(Dictionary<string, long> cardsAndWeight, ref List<(string, int)> handsAndBets) {
	handsAndBets.Sort((x, y) => {
		var xMaxPairCount = GetMaxPairCount(x.Item1);
		var yMaxPairCount = GetMaxPairCount(y.Item1);
		if (xMaxPairCount != yMaxPairCount) {
			// Descending order of pair count
			return yMaxPairCount.CompareTo(xMaxPairCount);
		}

		var xWeight = GetHandWeight(x.Item1, cardsAndWeight);
		var yWeight = GetHandWeight(y.Item1, cardsAndWeight);

		// Descending order of weight
		var weightComparison = yWeight.CompareTo(xWeight);
		if (weightComparison != 0) {
			return weightComparison;
		}

		// Compare each card's weight in the hands
		return CompareHandsCardByCard(x.Item1, y.Item1, cardsAndWeight);
	});
}

static int CompareHandsCardByCard(string hand1, string hand2, Dictionary<string, long> cardsAndWeight) {
	var hand1CardWeights = hand1.Select(card => cardsAndWeight[card.ToString()]).OrderByDescending(weight => weight)
		.ToList();
	var hand2CardWeights = hand2.Select(card => cardsAndWeight[card.ToString()]).OrderByDescending(weight => weight)
		.ToList();

	for (var i = 0; i < hand1CardWeights.Count; i++) {
		var comparison = hand2CardWeights[i].CompareTo(hand1CardWeights[i]);
		if (comparison != 0) {
			return comparison;
		}
	}

	return 0;
}

// Helper method to get the count of the most common card in a hand
static int GetMaxPairCount(string hand) => hand.GroupBy(card => card).Max(group => group.Count());

// Helper method to calculate the total weight of a hand
static long GetHandWeight(string hand, Dictionary<string, long> cardsAndWeight) => hand.Sum(card => cardsAndWeight[card.ToString()]);