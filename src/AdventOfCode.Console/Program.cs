// See https://aka.ms/new-console-template for more information
using AdventOfCode.Console;
using AdventOfCode.Shared;
using Spectre.Console;

AnsiConsole.Write(new FigletText("*Advent of Code*")
	.Centered()
	.Color(Color.Green));

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;
Console.CancelKeyPress += (_, _) => cancellationTokenSource.Cancel();

var modules = AdventOfCodeModuleLoader.LoadModules();

while (!cancellationToken.IsCancellationRequested)
{
	try
	{
		var selectedAoCYear = AnsiConsole.Prompt(new SelectionPrompt<int>()
			.AddChoices(modules.Keys)
			.Title("Select which [green]year[/] of AoC you want to see:"));

		Console.WriteLine($"Selected Year: {selectedAoCYear}");

		var selectedModule = AnsiConsole.Prompt(new SelectionPrompt<IAdventOfCodeModule>()
			.Title("Select a module")
			.AddChoices(modules[selectedAoCYear])
			.UseConverter(m => m.Name));

		Console.WriteLine($"Selected Module: {selectedModule.Name}");

		var useIntegratedInputFile = AnsiConsole.Prompt(
			new TextPrompt<bool>("Use Integrated Input file?")
				.AddChoices([true, false])
				.DefaultValue(true)
				.WithConverter(choice => choice ? "y" : "n"));

		var inputFile = selectedModule.GetInput();
		if (!useIntegratedInputFile)
		{
			var filePath = AnsiConsole.Prompt(new TextPrompt<string>("Please specify an input file:")
				.Validate(path => !File.Exists(path) ? ValidationResult.Error($"File at '{path}' does not exist") : ValidationResult.Success()));
			inputFile = await FileHelper.ValidateAndReadInputFileAsync(filePath, cancellationToken);
		}

		await AnsiConsole.Status()
			.StartAsync("Running...", async ctx => 
			{
				ctx.Refresh();
				await selectedModule.RunAsync(inputFile, cancellationToken);
			});

		AnsiConsole.MarkupLine("[green]Finished![/]");
		var backToMenu = AnsiConsole.Prompt(new TextPrompt<bool>("Back to main menu or quit?")
			.AddChoices([true, false])
			.DefaultValue(true)
			.WithConverter(choice => choice ? "m" : "q"));

		if (!backToMenu)
		{
			Console.WriteLine("Exiting...");
			cancellationTokenSource.Cancel();
		}
	} catch (Exception e)
	{
		AnsiConsole.WriteException(e);
	}
}