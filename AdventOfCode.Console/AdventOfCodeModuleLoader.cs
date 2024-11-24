using System.Reflection;
using AdventOfCode.Shared;

namespace AdventOfCode.Console;

public static class AdventOfCodeModuleLoader
{
	public static Dictionary<int, IAdventOfCodeModule[]> LoadModules()
	{
		var moduleType = typeof(IAdventOfCodeModule);

		var assemblyFiles = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(), "AdventOfCode*.dll");
		var assemblies = assemblyFiles.Select(Assembly.LoadFrom);
        
		// Find all types implementing IAdventOfCodeModule
		var modules = assemblies
			.SelectMany(a => a.GetTypes())
			.Where(t => moduleType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
			.Select(t => (IAdventOfCodeModule)Activator.CreateInstance(t))
			.ToList();

		// Group modules by DateOnly
		// And Order them by Day
		return modules
			.OrderBy(d => d.Day)
			.GroupBy(m => m.AoCYear.Year)
			.ToDictionary(
				g => g.Key,
				g => g.ToArray()
			);
	}
}