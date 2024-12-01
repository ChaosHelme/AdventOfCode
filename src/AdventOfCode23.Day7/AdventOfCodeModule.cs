using AdventOfCode.Shared;

namespace AdventOfCode23.Day7;

public sealed class AdventOfCodeModule : IAdventOfCodeModule
{
	public DateOnly AoCYear => DateOnly.Parse("2023-12-07");
	public int Day => 7;
	public string Name => "*** Advent Of Code 23 Day 7 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		if (input.Length <= 0)
		{
			return ValueTask.CompletedTask;
		}
		
		var cardsAndWeight = new Dictionary<string, int> {
			{ "A", 13 },
			{ "K", 12 },
			{ "Q", 11 },
			{ "J", 10 },
			{ "T", 9 },
			{ "9", 8 },
			{ "8", 7 },
			{ "7", 6 },
			{ "6", 5 },
			{ "5", 4 },
			{ "4", 3 },
			{ "3", 2 },
			{ "2", 1 },
		};

		var handScores = new Dictionary<string, int> {
			["Five of a kind"] = 7,
			["Four of a kind"] = 6,
			["Full house"] = 5,
			["Three of a kind"] = 4,
			["Two pair"] = 3,
			["One pair"] = 2,
			["High card"] = 1
		};

		var handsAndBets = new List<(string, int)>(input.Length);
		handsAndBets.AddRange(input.Select(line => (line.Split(' ')[0], int.Parse(line.Split(' ')[1]))));

		SortCamelCardHands(ref handsAndBets, cardsAndWeight, handScores);
		var totalRanks = handsAndBets.Count;
		var totalWinnings = 0;
		foreach (var handAndBet in handsAndBets) {
			totalWinnings += totalRanks * handAndBet.Item2;
			Console.WriteLine($"Hand: {handAndBet.Item1} / Bet: {handAndBet.Item2} / Rank: {totalRanks--}");
		}
		Console.WriteLine($"Total winnings: {totalWinnings}");
		
		return ValueTask.CompletedTask;
	}
	
	static void SortCamelCardHands(ref List<(string, int)> handsAndBets, Dictionary<string, int> cardsAndWeight, Dictionary<string, int> handScores) {
		handsAndBets.Sort((hand1, hand2) => {
			var hand1Type = GetHandType(hand1.Item1);
			var hand2Type = GetHandType(hand2.Item1);
			var comparison = handScores[hand2Type].CompareTo(handScores[hand1Type]);
			if (comparison != 0) {
				return comparison;
			}
    
			if (hand1Type == hand2Type) {
				return CompareHandsCardByCard(hand1.Item1, hand2.Item1, cardsAndWeight);
			}
    
			// If hands are the same type, order by high card
			var hand1Weight = GetHandWeight(hand1.Item1, cardsAndWeight);
			var hand2Weight = GetHandWeight(hand2.Item1, cardsAndWeight);
    
			return hand2Weight.CompareTo(hand1Weight);
		});
	}
    
	static int CompareHandsCardByCard(string hand1, string hand2, Dictionary<string, int> cardsAndWeight) {
		var hand1CardWeights = hand1.Select(card => cardsAndWeight[card.ToString()]).ToList();
		var hand2CardWeights = hand2.Select(card => cardsAndWeight[card.ToString()]).ToList();
    
		for (var i = 0; i < hand1CardWeights.Count; i++) {
			var comparison = hand2CardWeights[i].CompareTo(hand1CardWeights[i]);
			if (comparison != 0) {
				return comparison;
			}
		}
    
		return 0;
	}
    
	// Helper method to calculate the total weight of a hand
	static long GetHandWeight(string hand, Dictionary<string, int> cardsAndWeight) => hand.Sum(card => cardsAndWeight[card.ToString()]);
    
	static string GetHandType(string hand) {
		var cardFrequencies = hand
			.GroupBy(card => card)
			.Select(group => group.Count())
			.OrderByDescending(count => count)
			.ToList();
    
		switch (cardFrequencies.Count) {
			case 5:
				return "High card";
			case 4:
				return "One pair";
			case 3:
				return cardFrequencies.Contains(2) ? "Two pair" : "Three of a kind";
			case 2:
				return cardFrequencies.Contains(3) ? "Full house" : "Four of a kind";
			case 1:
				return "Five of a kind";
			default:
				throw new Exception("Invalid hand");
		}
	}
}