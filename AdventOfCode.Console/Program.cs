// See https://aka.ms/new-console-template for more information
using AdventOfCode.Console;
using AdventOfCode.Shared;
using Spectre.Console;

AnsiConsole.MarkupLine("⭐️️⭐️️⭐️️[green]Welcome to Advent of Code[/]⭐️️⭐️️⭐️️");

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;
Console.CancelKeyPress += (_, _) => cancellationTokenSource.Cancel();

var modules = AdventOfCodeModuleLoader.LoadModules();

var selectedAoCYer = AnsiConsole.Prompt(new TextPrompt<int>("What would you like to do?")
	.AddChoices(modules.Keys));

var selectedModule = AnsiConsole.Prompt(new SelectionPrompt<IAdventOfCodeModule>()
	.AddChoices(modules[selectedAoCYer])
	.UseConverter(m => m.Name));

var inputFile = await FileHelper.ValidateAndReadInputFileAsync("", cancellationToken);
await selectedModule.RunAsync(inputFile, cancellationToken);