using AdventOfCode.Shared;
using Spectre.Console;

namespace AdventOfCode24.Day1;

public class AdventOfCodeModule : IAdventOfCodeModule
{
    public DateOnly AoCYear => DateOnly.Parse("2024-12-01");
    public int Day => 1;
    public string Name => "*** Advent Of Code 24 Day 1 ***";
    public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
    {
        // Input is an array of <Number> <Number>, we need two arrays, one which contains the numbers on the left side and one which contains the numbers on the right side
        var numbersLeft = input.Select(i => i.Split(' ').First()).Select(int.Parse).Order().ToArray();
        var numbersRight = input.Select(i => i.Split(' ').Last()).Select(int.Parse).Order().ToArray();

        if (numbersRight.Length != numbersLeft.Length)
            throw new InvalidOperationException("List of numbers have different length");

        var sum = numbersLeft.Select((t, i) => Math.Abs(t - numbersRight[i])).Sum();

        AnsiConsole.MarkupLine($"[green]Part one[/]: {sum}");
        
        var occurrenceDictionary = numbersRight
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count());

        sum = 0;
        foreach (var number in numbersLeft)
        {
            sum += (number * occurrenceDictionary.GetValueOrDefault(number, 0));
        }
        
        AnsiConsole.MarkupLine($"[green]Part two[/]: {sum}");
        return ValueTask.CompletedTask;
    }
}