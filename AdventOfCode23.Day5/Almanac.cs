namespace AdventOfCode23.Day5;

public class Almanac {
	public uint[] Seeds { get; }
	public List<Ranges> SeedToSoilRanges { get; }
	public List<Ranges> SoilToFertilizerRanges { get; }
	public List<Ranges> FertilizerToWaterRanges { get; }
	public List<Ranges> WaterToLightRanges { get; }
	public List<Ranges> LightToTemperatureRanges { get; }
	public List<Ranges> TemperatureToHumidityRanges { get; }
	public List<Ranges> HumidityToLocationRanges { get; }

	public List<Map> SeedToSoilMaps { get; }
	public List<Map> SoilToFertilizerMaps { get; }
	public List<Map> FertilizerToWaterMaps { get; }
	public List<Map> WaterToLightMaps { get; }
	public List<Map> LightToTemperatureMaps { get; }
	public List<Map> TemperatureToHumidityMaps { get; }
	public List<Map> HumidityToLocationMaps { get; set; }

	readonly string[] input;

	public Almanac(string[] input) {
		this.input = input;
		this.Seeds = input[0][(input[0].IndexOf(':') + 2)..].Split(' ').Select(uint.Parse).ToArray();
		this.SeedToSoilRanges = new List<Ranges>();
		this.SoilToFertilizerRanges = new List<Ranges>();
		this.FertilizerToWaterRanges = new List<Ranges>();
		this.WaterToLightRanges = new List<Ranges>();
		this.LightToTemperatureRanges = new List<Ranges>();
		this.TemperatureToHumidityRanges = new List<Ranges>();
		this.HumidityToLocationRanges = new List<Ranges>();
		
		this.SeedToSoilMaps = new List<Map>();
		this.SoilToFertilizerMaps = new List<Map>();
		this.FertilizerToWaterMaps = new List<Map>();
		this.WaterToLightMaps = new List<Map>();
		this.LightToTemperatureMaps = new List<Map>();
		this.TemperatureToHumidityMaps = new List<Map>();
		this.HumidityToLocationMaps = new List<Map>();
	}
	
	public void Initialize() {
		this.SeedToSoilRanges.AddRange(GetRanges(3, 13));
		this.SoilToFertilizerRanges.AddRange(GetRanges(15, 31));
		this.FertilizerToWaterRanges.AddRange(GetRanges(33, 48));
		this.WaterToLightRanges.AddRange(GetRanges(50, 95));
		this.LightToTemperatureRanges.AddRange(GetRanges(97, 112));
		this.TemperatureToHumidityRanges.AddRange(GetRanges(114, 137));
		this.HumidityToLocationRanges.AddRange(GetRanges(139, 150));
		
		foreach (var seed in this.Seeds) {
			InitializeMap(seed, this.SeedToSoilRanges, this.SeedToSoilMaps);
		}

		foreach (var seedToSoilMap in this.SeedToSoilMaps) {
			InitializeMap(seedToSoilMap.Destination, this.SoilToFertilizerRanges, this.SoilToFertilizerMaps);
		}

		foreach (var soilToFertilizerMap in this.SoilToFertilizerMaps) {
			InitializeMap(soilToFertilizerMap.Destination, this.FertilizerToWaterRanges, this.FertilizerToWaterMaps);
		}

		foreach (var fertilizerToWaterMap in this.FertilizerToWaterMaps) {
			InitializeMap(fertilizerToWaterMap.Destination, this.WaterToLightRanges, this.WaterToLightMaps);
		}

		foreach (var waterToLightMap in this.WaterToLightMaps) {
			InitializeMap(waterToLightMap.Destination, this.LightToTemperatureRanges, this.LightToTemperatureMaps);
		}

		foreach (var lightToTemperatureMap in this.LightToTemperatureMaps) {
			InitializeMap(lightToTemperatureMap.Destination, this.TemperatureToHumidityRanges, this.TemperatureToHumidityMaps);
		}

		foreach (var temperatureToHumidityMap in this.TemperatureToHumidityMaps) {
			InitializeMap(temperatureToHumidityMap.Destination, this.HumidityToLocationRanges, this.HumidityToLocationMaps);
		}
	}

	public void PrintRanges() {
		Console.WriteLine($"Seed to soil map: {string.Join("\n", this.SeedToSoilRanges.Select(x => x.ToString()))}");
		Console.WriteLine($"\nSoil to fertilizer map: {string.Join("\n", this.SoilToFertilizerRanges.Select(x => x.ToString()))}");
		Console.WriteLine($"\nFertilizer to water map: {string.Join("\n", this.FertilizerToWaterRanges.Select(x => x.ToString()))}");
		Console.WriteLine($"\nWater to light map: {string.Join("\n", this.WaterToLightRanges.Select(x => x.ToString()))}");
		Console.WriteLine($"\nLight to temperature map: {string.Join("\n", this.LightToTemperatureRanges.Select(x => x.ToString()))}");
		Console.WriteLine($"\nTemperature to humidity map: {string.Join("\n", this.TemperatureToHumidityRanges.Select(x => x.ToString()))}");
		Console.WriteLine($"\nHumidity to location map: {string.Join("\n", this.HumidityToLocationRanges.Select(x => x.ToString()))}");
	}

	public void PrintMaps() {
		Console.WriteLine($"Seed to soil map: {string.Join("\n", this.SeedToSoilMaps.Select(x => x.ToString()))}");
		Console.WriteLine($"\nSoil to fertilizer map: {string.Join("\n", this.SoilToFertilizerMaps.Select(x => x.ToString()))}");
		Console.WriteLine($"\nFertilizer to water map: {string.Join("\n", this.FertilizerToWaterMaps.Select(x => x.ToString()))}");
		Console.WriteLine($"\nWater to light map: {string.Join("\n", this.WaterToLightMaps.Select(x => x.ToString()))}");
		Console.WriteLine($"\nLight to temperature map: {string.Join("\n", this.LightToTemperatureMaps.Select(x => x.ToString()))}");
		Console.WriteLine($"\nTemperature to humidity map: {string.Join("\n", this.TemperatureToHumidityMaps.Select(x => x.ToString()))}");
		Console.WriteLine($"\nHumidity to location map: {string.Join("\n", this.HumidityToLocationMaps.Select(x => x.ToString()))}");
	}

	public void PrintSeedsToLocations() {
		foreach (var humidityToLocationMap in this.HumidityToLocationMaps.Where(humidityToLocationMap => humidityToLocationMap.Destination > 0)) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Seed: {humidityToLocationMap.Source} -> Location: {humidityToLocationMap.Destination}");
		}

		Console.ResetColor();
	}

	public uint FindLowestLocation() {
		var lowestLocation = uint.MaxValue;
		foreach (var humidityToLocationMap in this.HumidityToLocationMaps.Where(humidityToLocationMap => humidityToLocationMap.Destination < lowestLocation)) {
			lowestLocation = humidityToLocationMap.Destination;
		}

		return lowestLocation;
	}
	
	List<Ranges> GetRanges(int startIndex, int endIndex) {
		var ranges = new List<Ranges>();
		for (var i = startIndex; i < endIndex; i++) {
			var map = this.input[i].Split(' ').Select(uint.Parse).ToArray();
			ranges.Add(new Ranges(map[0], map[1], map[2]));
		}

		return ranges;
	}
	
	static void InitializeMap(uint source, List<Ranges> ranges, List<Map> map) {
		map.AddRange(from range in ranges 
			where source >= range.SourceRangeStart && 
			      source <= range.SourceRangeStart + range.RangeLength
			select new Map(source, range.DestinationRangeStart + (source - range.SourceRangeStart)));

		if (map.All(x => x.Source != source)) {
			map.Add(new Map(source, source));
		}
	}
}

public record struct Ranges(uint DestinationRangeStart, uint SourceRangeStart, uint RangeLength);

public record struct Map(uint Source, uint Destination);