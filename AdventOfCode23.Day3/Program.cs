﻿// See https://aka.ms/new-console-template for more information
using AdventOfCode23.Shared;

var lines = FileHelper.ValidateAndReadInputFile("Input.txt");
if (lines == null) {
    return;
}
var amountOfRows = lines.Length;
// We assume that all rows have the same amount of columns (which is the case for the input)
var amountOfColumns = lines[0].Length;

Console.WriteLine($"{amountOfRows} rows and {amountOfColumns} columns");
if (amountOfRows != amountOfColumns) {
    Console.Error.WriteLine("The input is not a square");
    return;
}

var sum = CalculatePartNumberSum(lines);
Console.WriteLine("Part 1:");
Console.WriteLine("-------");
Console.WriteLine($"Sum of part numbers: {sum}");

Console.WriteLine();

var gearRatioSum = CalculateGearRatioSum(lines);
Console.WriteLine("Part 2:");
Console.WriteLine("-------");
Console.WriteLine($"Sum of gear ratios: {gearRatioSum}");

return;

static int CalculatePartNumberSum(IReadOnlyList<string> schematic) {
    var sum = 0;
    for (var row = 0; row < schematic.Count; row++) {
        for (var column = 0; column < schematic[row].Length; column++) {
            var currentChar = schematic[row][column];

            // Check if the current character is a digit
            if (char.IsDigit(currentChar) && IsAdjacentToSymbol(schematic, row, column)) {
                // Find the start and end indices of the current number
                var startIndex = FindStartIndexOfNumber(schematic[row], column);
                var endIndex = FindEndIndexOfNumber(schematic[row], column);

                // Extract the number substring and add to the sum
                var numberString = schematic[row][startIndex..(endIndex + 1)];
                sum += int.Parse(numberString);

                // Move the loop index to the end of the number
                column = endIndex;
            }
        }
    }

    return sum;
}

static int CalculateGearRatioSum(IReadOnlyList<string> schematic) {
    var sum = 0;
    for (var row = 0; row < schematic.Count; row++) {
        for (var column = 0; column < schematic[row].Length; column++) {
            if (schematic[row][column] == '*') {
                // Find two numbers adjacent to the * symbol
                var adjacentNumbers = GetAdjacentNumbers(schematic, row, column).ToArray();
                if (adjacentNumbers.Length == 2) {
                    sum += adjacentNumbers[0] * adjacentNumbers[1];
                }
            }
        }
    }
    return sum;
}

static HashSet<int> GetAdjacentNumbers(IReadOnlyList<string> schematics, int row, int col) {
    var adjacentNumbers = new HashSet<int>(2);

    // The direction offsets for rows and columns represent the eight possible directions (up, down, left, right, and diagonals)
    int[] dr = {-1, -1, -1, 0, 0, 1, 1, 1}; // Direction offsets for rows
    int[] dc = {-1, 0, 1, -1, 1, -1, 0, 1}; // Direction offsets for columns

    for (var k = 0; k < 8; k++) {
        var newRow = row + dr[k];
        var newCol = col + dc[k];

        // Check if the new position is within bounds
        if (newRow >= 0 && newRow < schematics.Count &&
            newCol >= 0 && newCol < schematics[newRow].Length) {
            var adjacentChar = schematics[newRow][newCol];

            // Check if the adjacent position contains a number
            if (char.IsDigit(adjacentChar)) {
                var startIndex = FindStartIndexOfNumber(schematics[newRow], newCol);
                var endIndex = FindEndIndexOfNumber(schematics[newRow], newCol);
                adjacentNumbers.Add(int.Parse(schematics[newRow][startIndex..(endIndex + 1)]));
            }
        }
    }

    return adjacentNumbers;
}

static bool IsAdjacentToSymbol(IReadOnlyList<string> schematic, int row, int col) {
    // The direction offsets for rows and columns represent the eight possible directions (up, down, left, right, and diagonals)
    int[] dr = {-1, -1, -1, 0, 0, 1, 1, 1}; // Direction offsets for rows
    int[] dc = {-1, 0, 1, -1, 1, -1, 0, 1}; // Direction offsets for columns

    for (var k = 0; k < 8; k++) {
        var newRow = row + dr[k];
        var newCol = col + dc[k];

        // Check if the new position is within bounds
        if (newRow >= 0 && newRow < schematic.Count &&
            newCol >= 0 && newCol < schematic[newRow].Length) {
            var adjacentChar = schematic[newRow][newCol];

            // Check if the adjacent position contains a symbol
            if (adjacentChar != '.' && !char.IsDigit(adjacentChar)) {
                return true;
            }
        }
    }

    return false;
}

static int FindStartIndexOfNumber(string line, int index) {
    var startIndex = index;

    // Find the start index of the current number
    while (startIndex - 1 >= 0 && char.IsDigit(line[startIndex - 1])) {
        startIndex--;
    }

    return startIndex;
}

static int FindEndIndexOfNumber(string line, int startIndex) {
    var endIndex = startIndex;

    // Find the end index of the current number
    while (endIndex + 1 < line.Length && char.IsDigit(line[endIndex + 1])) {
        endIndex++;
    }

    return endIndex;
}