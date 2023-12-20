// See https://aka.ms/new-console-template for more information
using Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
	return;
}
var numbers = new List<int>(lines.Length);
char[] digits = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

// Part 1
// numbers.AddRange(from line in lines
// 	let firstDigit = line[line.IndexOfAny(digits)]
// 	let lastDigit = line[line.LastIndexOfAny(digits)]
// 	select int.Parse($"{firstDigit}{lastDigit}"));
//
// Console.WriteLine(string.Join(" + ", numbers));
// Console.WriteLine($"The sum is: {numbers.Sum()}");

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

foreach (var line in lines) {
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