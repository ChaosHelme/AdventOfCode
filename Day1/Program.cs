// See https://aka.ms/new-console-template for more information

var lines = File.ReadAllLines("Input.txt");
var numbers = new List<int>(lines.Length);
char[] digits = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
foreach (var line in lines) {
	var firstDigit = line[line.IndexOfAny(digits)];
	var lastDigit = line[line.LastIndexOfAny(digits)];
	numbers.Add(int.Parse($"{firstDigit}{lastDigit}"));
}

Console.WriteLine(string.Join(" + ", numbers));
Console.WriteLine($"The sum is: {numbers.Sum()}");