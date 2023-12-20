namespace AdventOfCode23.Shared;

public static class FileHelper {
	public static string[]? ValidateAndReadInputFile(string path) {
		try {
			var lines = File.ReadAllLines(path);

			if (lines.Length == 0) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Error.WriteLine($"Input file seems to contain no content: {path}");
				Console.ResetColor();
				return null;
			}

			return lines;
		} catch (FileNotFoundException fileNotFoundEx) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine(fileNotFoundEx);
			Console.ResetColor();
		} catch (Exception ex) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine(ex);
			Console.ResetColor();
		}

		return null;
	}
}