// See https://aka.ms/new-console-template for more information
using AdventOfCode.Console;
using AdventOfCode.Shared;
using Spectre.Console;

AnsiConsole.MarkupLine("⭐️️⭐️️⭐️️[green]Welcome to Advent of Code[/]⭐️️⭐️️⭐️️");

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;
Console.CancelKeyPress += (_, _) => cancellationTokenSource.Cancel();

var modules = AdventOfCodeModuleLoader.LoadModules();

var selectedAoCYear = AnsiConsole.Prompt(new SelectionPrompt<int>()
	.AddChoices(modules.Keys)
	.Title("Select which [green]year[/] of AoC you want"));

var selectedModule = AnsiConsole.Prompt(new SelectionPrompt<IAdventOfCodeModule>()
	.Title("Select a module")
	.AddChoices(modules[selectedAoCYear])
	.UseConverter(m => m.Name));

var inputFile = await FileHelper.ValidateAndReadInputFileAsync("Input.txt", cancellationToken);
await selectedModule.RunAsync(inputFile, cancellationToken);