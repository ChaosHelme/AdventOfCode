﻿using AdventOfCode.Shared;

namespace AdventOfCode23.Day8;

public sealed class AdventOfCodeModule : IAdventOfCodeModule
{
	public DateOnly AoCYear => DateOnly.Parse("2023-12-08");
	public int Day => 8;
	public string Name => "*** Advent Of Code 23 Day 8 ***";

	public ValueTask RunAsync(string[] input, CancellationToken cancellationToken)
	{
		if (input.Length <= 0)
		{
			return ValueTask.CompletedTask;
		}
		
		var instructionSetString = input[0];
		var instructionSet = instructionSetString.Select(c => (Instruction)Enum.Parse(typeof(Instruction), c.ToString())).ToList();
		FillElements(input, out var elements);

		var startElements = FindElementsEndWithA(elements);
		var steps = GetTotalStepsForAllEndWithZ(instructionSet, elements, startElements);
		Console.WriteLine($"Total steps: {steps}");
		
		return ValueTask.CompletedTask;
	}
	
	static List<string> FindElementsEndWithA(Dictionary<string, (string, string)> elements) {
		var result = new List<string>();
		foreach (var element in elements) {
			if (element.Key.EndsWith("A")) {
				result.Add(element.Key);
			}
		}
		return result;
	}
    
	static long GetTotalStepsForAllEndWithZ(List<Instruction> instructionSet, Dictionary<string, (string, string)> elements, List<string> startElements) {
		var lengthList = new List<long>();
		var visited = new List<string>();
		foreach (var startElement in startElements) {
			visited.Clear();
			var currentElement = startElement;
			while (!currentElement.EndsWith("Z")) {
				var instruction = instructionSet[visited.Count % instructionSet.Count];
    			
				var directions = elements[currentElement];
				var newElement = GetElementByInstruction(instruction, directions);
				currentElement = newElement;
				visited.Add(currentElement);
			}
    
			lengthList.Add(visited.Count);
		}
    
		// calculate LCM of all collected lengths
		var totalSteps = lengthList.Aggregate(Lcm);
		return totalSteps;
	}
    
	static long Gcd(long a, long b) {
		return b == 0 ? a : Gcd(b, a % b);
	}
    
	static long Lcm(long a, long b) {
		return Math.Abs(a * b) / Gcd(a, b);
	}
    
	static string GetElementByInstruction(Instruction instruction, (string, string) directions) {
		return instruction == Instruction.L
			? directions.Item1
			: directions.Item2;
	}
    
	static void FillElements(string[] lines, out Dictionary<string, (string, string)> elements) {
		elements = new Dictionary<string, (string, string)>();
		foreach (var line in lines) {
			var parts = line.Split(new[] { ' ', '=', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
    
			if (parts.Length >= 3) {
				elements[parts[0]] = (parts[1], parts[2]);
			}
		}
	}
    
	enum Instruction { L, R }
}