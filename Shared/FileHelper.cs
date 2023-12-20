namespace Shared;

public static class FileHelper {
	public static string[]? ValidateAndReadInputFile(string path) {
		var lines = File.ReadAllLines(path);

		if (lines.Length == 0) {
			Console.Error.WriteLine($"No input file found at path {path}");
			return null;
		}

		return lines;
	}
}