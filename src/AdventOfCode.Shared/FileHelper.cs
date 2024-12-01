using System.Reflection;

namespace AdventOfCode.Shared;

public static class FileHelper {
	public static async Task<string[]> ValidateAndReadInputFileAsync(string path, CancellationToken cancellationToken) {
		try {
			var lines = await File.ReadAllLinesAsync(path, cancellationToken);

			if (lines.Length == 0) {
				Console.ForegroundColor = ConsoleColor.Red;
				await Console.Error.WriteLineAsync($"Input file seems to contain no content: {path}");
				Console.ResetColor();
				return [];
			}

			return lines;
		} catch (Exception ex) {
			Console.ForegroundColor = ConsoleColor.Red;
			await Console.Error.WriteLineAsync(ex.ToString());
			Console.ResetColor();
		}

		return [];
	}
}