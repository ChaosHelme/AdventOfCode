using AdventOfCode.Shared;

namespace AdventOfCode23.Day1;

public sealed class AdventOfCodeModule : IAdventOfCodeModule
{
	public DateOnly AoCYear => DateOnly.Parse("2023-12-01");
	public int Day => 1;
	public string Name => "*** Advent Of Code 23 Day 1 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
        if (input.Length < 1) {
        	return ValueTask.CompletedTask;
        }
        var numbers = new List<int>(input.Length);
        char[] digits = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
        
        // Part 1
        numbers.AddRange(from line in input
        	let firstDigit = line[line.IndexOfAny(digits)]
        	let lastDigit = line[line.LastIndexOfAny(digits)]
        	select int.Parse($"{firstDigit}{lastDigit}"));
        
        Console.WriteLine(string.Join(" + ", numbers));
        Console.WriteLine($"The sum is: {numbers.Sum()}");
        numbers.Clear();
        
        // Part 2
        var digitAsWordToDigit = new Dictionary<string, int> {
        	{"one", 1},
        	{"two", 2},
        	{"three", 3},
        	{"four", 4},
        	{"five", 5},
        	{"six", 6},
        	{"seven", 7},
        	{"eight", 8},
        	{"nine", 9}
        };
        
        foreach (var line in input) {
        	var firstDigit = -1;
        	var lastDigit = -1;
        	var firstIndexOfDigit = line.IndexOfAny(digits);
        	var lastIndexOfDigit = line.LastIndexOfAny(digits);
        
        	firstDigit = int.Parse($"{line[firstIndexOfDigit]}");
        	lastDigit = int.Parse($"{line[lastIndexOfDigit]}");
        
        	var previousIndexOfDigitAsWord = int.MaxValue;
        	var previousLastIndexOfDigitAsWord = int.MinValue;
        	foreach (var digitAsWord in digitAsWordToDigit.Keys) {
        		var indexOfDigitAsWord = line.IndexOf(digitAsWord, StringComparison.Ordinal);
        		if (indexOfDigitAsWord > -1 && indexOfDigitAsWord < firstIndexOfDigit && indexOfDigitAsWord < previousIndexOfDigitAsWord) {
        			firstDigit = digitAsWordToDigit[digitAsWord];
        			previousIndexOfDigitAsWord = indexOfDigitAsWord;
        		}
        		
        		var lastIndexOfDigitAsWord = line.LastIndexOf(digitAsWord, StringComparison.Ordinal);
        		if (lastIndexOfDigitAsWord > -1 && lastIndexOfDigitAsWord > lastIndexOfDigit && lastIndexOfDigitAsWord > previousLastIndexOfDigitAsWord) {
        			lastDigit = digitAsWordToDigit[digitAsWord];
        			previousLastIndexOfDigitAsWord = lastIndexOfDigitAsWord;
        		}
        	}
        	
        	numbers.Add(int.Parse($"{firstDigit}{lastDigit}"));
        }
        
        Console.WriteLine(string.Join(" + ", numbers));
        Console.WriteLine($"The sum is: {numbers.Sum()}");
		return ValueTask.CompletedTask;
	}
}

