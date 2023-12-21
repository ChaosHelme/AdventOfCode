namespace AdventOfCode23.Day5;

public class Almanac {
	Dictionary<uint, uint> SeedRanges { get; }
	List<Ranges> SeedToSoilRanges { get; }
	List<Ranges> SoilToFertilizerRanges { get; }
	List<Ranges> FertilizerToWaterRanges { get; }
	List<Ranges> WaterToLightRanges { get; }
	List<Ranges> LightToTemperatureRanges { get; }
	List<Ranges> TemperatureToHumidityRanges { get; }
	List<Ranges> HumidityToLocationRanges { get; }

	Dictionary<uint, uint> SeedToSoilMaps { get; }
	Dictionary<uint, uint> SoilToFertilizerMaps { get; }
	Dictionary<uint, uint> FertilizerToWaterMaps { get; }
	Dictionary<uint, uint> WaterToLightMaps { get; }
	Dictionary<uint, uint> LightToTemperatureMaps { get; }
	Dictionary<uint, uint> TemperatureToHumidityMaps { get; }
	Dictionary<uint, uint> HumidityToLocationMaps { get; }

	readonly string[] input;

	public Almanac(string[] input) {
		this.input = input;
		this.SeedRanges = GetSeedRangesSorted();
		this.SeedToSoilRanges = new List<Ranges>();
		this.SoilToFertilizerRanges = new List<Ranges>();
		this.FertilizerToWaterRanges = new List<Ranges>();
		this.WaterToLightRanges = new List<Ranges>();
		this.LightToTemperatureRanges = new List<Ranges>();
		this.TemperatureToHumidityRanges = new List<Ranges>();
		this.HumidityToLocationRanges = new List<Ranges>();
		
		this.SeedToSoilMaps = new Dictionary<uint, uint>();
		this.SoilToFertilizerMaps = new Dictionary<uint, uint>();
		this.FertilizerToWaterMaps = new Dictionary<uint, uint>();
		this.WaterToLightMaps = new Dictionary<uint, uint>();
		this.LightToTemperatureMaps = new Dictionary<uint, uint>();
		this.TemperatureToHumidityMaps = new Dictionary<uint, uint>();
		this.HumidityToLocationMaps = new Dictionary<uint, uint>();
	}
	
	Dictionary<uint, uint> GetSeedRangesSorted() {
		var seedRanges = new Dictionary<uint, uint>();
		var seedRangesString = this.input[0][(this.input[0].IndexOf(':') + 2)..].Split(' ');
		for (var i = 0; i < seedRangesString.Length; i+=2) {
			seedRanges.Add(uint.Parse(seedRangesString[i]), uint.Parse(seedRangesString[i +1]));
		}
		
		return seedRanges.OrderBy(x=> x.Key).ToDictionary(x => x.Key, x => x.Value);
	}
	
	public void Initialize() {
		this.SeedToSoilRanges.AddRange(GetRanges("seed-to-soil map:").OrderBy(x => x.SourceRangeStart));
		this.SoilToFertilizerRanges.AddRange(GetRanges("soil-to-fertilizer map:").OrderBy(x => x.SourceRangeStart));
		this.FertilizerToWaterRanges.AddRange(GetRanges("fertilizer-to-water map:").OrderBy(x => x.SourceRangeStart));
		this.WaterToLightRanges.AddRange(GetRanges("water-to-light map:").OrderBy(x => x.SourceRangeStart));
		this.LightToTemperatureRanges.AddRange(GetRanges("light-to-temperature map:").OrderBy(x => x.SourceRangeStart));
		this.TemperatureToHumidityRanges.AddRange(GetRanges("temperature-to-humidity map:").OrderBy(x => x.SourceRangeStart));
		this.HumidityToLocationRanges.AddRange(GetRanges("humidity-to-location map:").OrderBy(x => x.SourceRangeStart));
		
		
		foreach (var seedRange in this.SeedRanges) {
			for (var seed = seedRange.Key; seed < seedRange.Key + seedRange.Value; seed++) {
				InitializeMap(seed, this.SeedToSoilRanges, this.SeedToSoilMaps);
			}
		}

		foreach (var seedToSoilMap in this.SeedToSoilMaps) {
			InitializeMap(seedToSoilMap.Value, this.SoilToFertilizerRanges, this.SoilToFertilizerMaps);
		}

		foreach (var soilToFertilizerMap in this.SoilToFertilizerMaps) {
			InitializeMap(soilToFertilizerMap.Value, this.FertilizerToWaterRanges, this.FertilizerToWaterMaps);
		}

		foreach (var fertilizerToWaterMap in this.FertilizerToWaterMaps) {
			InitializeMap(fertilizerToWaterMap.Value, this.WaterToLightRanges, this.WaterToLightMaps);
		}

		foreach (var waterToLightMap in this.WaterToLightMaps) {
			InitializeMap(waterToLightMap.Value, this.LightToTemperatureRanges, this.LightToTemperatureMaps);
		}

		foreach (var lightToTemperatureMap in this.LightToTemperatureMaps) {
			InitializeMap(lightToTemperatureMap.Value, this.TemperatureToHumidityRanges, this.TemperatureToHumidityMaps);
		}

		foreach (var temperatureToHumidityMap in this.TemperatureToHumidityMaps) {
			InitializeMap(temperatureToHumidityMap.Value, this.HumidityToLocationRanges, this.HumidityToLocationMaps);
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
		foreach (var humidityToLocationMap in this.HumidityToLocationMaps) {
			var temperature = this.TemperatureToHumidityMaps.First(item => item.Value == humidityToLocationMap.Key);
			var light = this.LightToTemperatureMaps.First(item => item.Value == temperature.Key);
			var water = this.WaterToLightMaps.First(item => item.Value == light.Key);
			var fertilizer = this.FertilizerToWaterMaps.First(item => item.Value == water.Key);
			var soil = this.SoilToFertilizerMaps.First(item => item.Value == fertilizer.Key);
			var seed = this.SeedToSoilMaps.First(item => item.Value == soil.Key);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Seed: {seed.Value} -> Location: {humidityToLocationMap.Value}");
		}

		Console.ResetColor();
	}

	public uint FindLowestLocation() {
		var lowestLocation = uint.MaxValue;
		foreach (var humidityToLocationMap in this.HumidityToLocationMaps.Where(humidityToLocationMap => humidityToLocationMap.Value < lowestLocation)) {
			lowestLocation = humidityToLocationMap.Value;
		}

		return lowestLocation;
	}
	
	List<Ranges> GetRanges(string rangeIndicator) {
		var startIndex = (from line in this.input where line.StartsWith(rangeIndicator) select Array.IndexOf(this.input, line) + 1).FirstOrDefault();
		var endIndex = startIndex;
		for (var i = startIndex; i < this.input.Length; i++) {
			if (this.input[i].Trim() == string.Empty) {
				endIndex = i;
				break;
			}	
		}
		
		var ranges = new List<Ranges>();
		for (var i = startIndex; i < endIndex; i++) {
			var map = this.input[i].Split(' ').Select(uint.Parse).ToArray();
			ranges.Add(new Ranges(map[0], map[1], map[2]));
		}

		return ranges;
	}
	
	static void InitializeMap(uint source, List<Ranges> ranges, Dictionary<uint, uint> map) {
		foreach (var range in ranges) {
			if (source >= range.SourceRangeStart && source < range.SourceRangeStart + range.RangeLength) {
				map.TryAdd(source, range.DestinationRangeStart + (source - range.SourceRangeStart));
			}
		}
		
		map.TryAdd(source, source);
	}
}

public record struct Ranges(uint DestinationRangeStart, uint SourceRangeStart, uint RangeLength);