// See https://aka.ms/new-console-template for more information

using AdventOfCode23.Day3;

char[] digits = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
char[] symbols = {'#', '@', '/', '\\', '!', '$', '%', '&', '*', '?', '+', '-', '^', '~', '<', '>', '='};
char[] symbolsIncludingPoint = {'#', '@', '/', '\\', '!', '$', '%', '&', '*', '?', '+', '-', '^', '~', '<', '>', '=', '.'};
var lines = File.ReadAllLines("Input.txt");

if (!lines.Any()) {
	Console.WriteLine("No input");
	return;
}

var partNumbers = new List<PartNumber>();
var amountOfRows = lines.Length;
// We assume that all rows have the same amount of columns (which is the case for the input)
var amountOfColumns = lines[0].Length;

Console.WriteLine($"{amountOfRows} rows and {amountOfColumns} columns");

for (var row = 0; row < amountOfRows; row++) {
	var startIndex = 0;
	while (startIndex < amountOfColumns) {
		var startDigitIndex = lines[row].IndexOfAny(digits, startIndex);
		if (startDigitIndex < 0) {
			break;
		}

		var endDigitIndex = lines[row].IndexOfAny(symbolsIncludingPoint, startDigitIndex);
		if (endDigitIndex < 0) {
			endDigitIndex = amountOfColumns;
		}

		// Find the number which is ascendant of a symbol
		if (IsNumberRightOfSymbol(row, startDigitIndex, endDigitIndex) ||
			IsNumberLeftOfSymbol(row, startDigitIndex, endDigitIndex) ||
			IsNumberAboveSymbol(row, startDigitIndex, endDigitIndex) ||
			IsNumberBelowSymbol(row, startDigitIndex, endDigitIndex) ||
			IsNumberDiagonalOfSymbol(row, startDigitIndex, endDigitIndex)) {
			partNumbers.Add(new PartNumber(row, startDigitIndex, int.Parse(lines[row][startDigitIndex..endDigitIndex])));
		}

		startIndex = endDigitIndex + 1;
	}
}

foreach (var partNumber in partNumbers) {
	Console.WriteLine(partNumber.ToString());
}

Console.WriteLine($"The sum of all part numbers is {partNumbers.Sum(p => p.Value)}");

return;

// A number is ascendant of a symbol if it is directly above, below, left, right or diagonal of a symbol

bool IsNumberRightOfSymbol(int row, int startIndexColumn, int endIndexColumn) {
	for (var currentIndex = startIndexColumn; currentIndex <= endIndexColumn; currentIndex++) {
		if (symbols.Contains(lines[row][currentIndex > 0 ? currentIndex - 1 : 0])) {
			return true;
		}
	}

	return false;
}

bool IsNumberLeftOfSymbol(int row, int startIndexColumn, int endIndexColumn) {
	for (var currentIndex = startIndexColumn; currentIndex <= endIndexColumn; currentIndex++) {
		if (symbols.Contains(lines[row][currentIndex < amountOfColumns ? currentIndex : amountOfColumns - 1])) {
			return true;
		}
	}

	return false;
}

bool IsNumberAboveSymbol(int row, int startIndexColumn, int endIndexColumn) {
	for (var currentIndex = startIndexColumn; currentIndex <= endIndexColumn; currentIndex++) {
		if (symbols.Contains(lines[row > 0 ? row - 1 : 0][currentIndex < amountOfColumns ? currentIndex : amountOfColumns - 1])) {
			return true;
		}
	}

	return false;
}

bool IsNumberBelowSymbol(int row, int startIndexColumn, int endIndexColumn) {
	for (var currentIndex = startIndexColumn; currentIndex <= endIndexColumn; currentIndex++) {
		if (symbols.Contains(lines[row < amountOfRows - 1 ? row + 1 : amountOfRows - 1][currentIndex < amountOfColumns ? currentIndex : amountOfColumns - 1])) {
			return true;
		}
	}

	return false;
}

bool IsNumberDiagonalOfSymbol(int row, int startIndexColumn, int endIndexColumn) {
	for (var currentIndex = startIndexColumn; currentIndex <= endIndexColumn; currentIndex++) {
		if (symbols.Contains(lines[row > 0 ? row - 1 : 0][currentIndex > 0 ? currentIndex - 1 : 0]) ||
			symbols.Contains(lines[row > 0 ? row - 1 : 0][currentIndex < amountOfColumns ? currentIndex : amountOfColumns - 1]) ||
			symbols.Contains(lines[row < amountOfRows - 1 ? row + 1 : amountOfRows - 1][currentIndex > 0 ? currentIndex - 1 : 0]) ||
			symbols.Contains(lines[row < amountOfRows - 1 ? row + 1 : amountOfRows - 1][currentIndex < amountOfColumns ? currentIndex : amountOfColumns - 1])) {
			return true;
		}
	}

	return false;
}