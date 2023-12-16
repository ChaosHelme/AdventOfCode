// See https://aka.ms/new-console-template for more information

using AdventOfCode23.Day3;

char[] digits = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
char[] symbols = {'#', '@', '/', '\\', '!', '$', '%', '&', '*', '?', '+', '-', '^', '~', '<', '>', '='};
char[] symbolsIncludingPoint = {'#', '@', '/', '\\', '!', '$', '%', '&', '*', '?', '+', '-', '^', '~', '<', '>', '=', '.'};
var lines = File.ReadAllLines("Input.txt");
var partNumbers = new List<PartNumber>();
var amountOfRows = lines.Length;
// We assume that all rows have the same amount of columns (which is the case for the input)
var amountOfColumns = lines[0].Length;

Console.WriteLine($"{amountOfRows} rows and {amountOfColumns} columns");

for (var row = 0; row < amountOfRows; row++) {
	var startIndex = 0;
	while (startIndex < amountOfColumns) {
		var indexOfDigit = lines[row].IndexOfAny(digits, startIndex);
		if (indexOfDigit < 0) {
			break;
		}
		
		var endIndexOfDigit = lines[row].IndexOfAny(symbolsIncludingPoint, indexOfDigit);
		if (endIndexOfDigit < 0) {
			endIndexOfDigit = amountOfColumns;
		}
		
		// Find the number which is ascendant of a symbol
		if (symbols.Contains(lines[row][indexOfDigit > 0 ? indexOfDigit -1 : 0]) || symbols.Contains(lines[row][endIndexOfDigit < amountOfColumns ? endIndexOfDigit : amountOfColumns - 1])) {
			partNumbers.Add(new PartNumber(row, indexOfDigit, int.Parse(lines[row][indexOfDigit..endIndexOfDigit])));
		}

		startIndex = endIndexOfDigit + 1;
	}
}

foreach (var partNumber in partNumbers) {
	Console.WriteLine(partNumber.ToString());
}

// var stringsEnumerable = lines.SelectMany(line => line.Split('.').Where(part => !string.IsNullOrEmpty(part)));
// foreach (var s in stringsEnumerable) {
// 	Console.WriteLine(s);
// }