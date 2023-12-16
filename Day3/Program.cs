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

// Part 1
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

Console.WriteLine($"The sum of all part numbers is {partNumbers.Sum(p => p.Value)}");

var gearRatios = new List<int>();

// Part 2
for (var row = 0; row < amountOfRows; row++) {
	var startIndex = 0;
	while (startIndex < amountOfColumns) {
		var indexOfSymbol = lines[row].IndexOf('*', startIndex);
		if (indexOfSymbol < 0) {
			break;
			
		}
		startIndex = indexOfSymbol + 1;

		var firstNumber = 0;
		var secondNumber = 0;
		
		// Find number left of symbol
		var numberLeftOfSymbol = lines[row][indexOfSymbol - 1];
		if (digits.Contains(numberLeftOfSymbol)) {
			var numberLeftOfSymbolIndex = indexOfSymbol - 1;
			while (numberLeftOfSymbolIndex > 0 && digits.Contains(lines[row][numberLeftOfSymbolIndex - 1])) {
				numberLeftOfSymbolIndex--;
			}
			
			firstNumber = int.Parse(lines[row][numberLeftOfSymbolIndex..indexOfSymbol]);
		}
		
		// Find number right of symbol
		var numberRightOfSymbol = lines[row][indexOfSymbol + 1];
		if (digits.Contains(numberRightOfSymbol)) {
			var numberRightOfSymbolIndex = indexOfSymbol + 1;
			while (numberRightOfSymbolIndex < amountOfColumns && digits.Contains(lines[row][numberRightOfSymbolIndex + 1])) {
				numberRightOfSymbolIndex++;
			}
			
			secondNumber = int.Parse(lines[row][(indexOfSymbol + 1)..(numberRightOfSymbolIndex + 1)]);
		}

		if (firstNumber > 0 && secondNumber > 0) {
			gearRatios.Add(firstNumber * secondNumber);
			continue;
		}
		
		// Find number above symbol
		var numberAboveSymbol = lines[row > 0 ? row - 1 : 0][indexOfSymbol];
		if (digits.Contains(numberAboveSymbol)) {
			var indexLeftOfSymbol = indexOfSymbol - 1;
			var indexRightOfSymbol = indexOfSymbol + 1;
			while (row > 0 && digits.Contains(lines[row - 1][indexLeftOfSymbol - 1])) {
				indexLeftOfSymbol--;
			}

			while (row > 0 && digits.Contains(lines[row - 1][indexRightOfSymbol + 1])) {
				indexRightOfSymbol++;
			}
			
			firstNumber = int.Parse(lines[row - 1][indexLeftOfSymbol..indexRightOfSymbol]);
		}
		
		// Find number below symbol
		var numberBelowSymbol = lines[row < amountOfRows - 1 ? row + 1 : amountOfRows - 1][indexOfSymbol];
		if (digits.Contains(numberBelowSymbol)) {
			var numberBelowSymbolIndex = row < amountOfRows - 1 ? row + 1 : amountOfRows - 1;
			while (numberBelowSymbolIndex < amountOfRows - 1 && digits.Contains(lines[numberBelowSymbolIndex + 1][indexOfSymbol])) {
				numberBelowSymbolIndex++;
			}
			
			secondNumber = int.Parse(lines[numberBelowSymbolIndex][indexOfSymbol].ToString());
		}

		if (firstNumber > 0 && secondNumber > 0) {
			gearRatios.Add(firstNumber * secondNumber);
			continue;
		}
	}
}

Console.WriteLine($"The sum of all gear ratios is {gearRatios.Sum()}");

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