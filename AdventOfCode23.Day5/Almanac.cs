namespace AdventOfCode23.Day5;

public class Almanac {
	Dictionary<long, long> SeedRanges { get; }
	List<Ranges> SeedToSoilRanges { get; }
	List<Ranges> SoilToFertilizerRanges { get; }
	List<Ranges> FertilizerToWaterRanges { get; }
	List<Ranges> WaterToLightRanges { get; }
	List<Ranges> LightToTemperatureRanges { get; }
	List<Ranges> TemperatureToHumidityRanges { get; }
	List<Ranges> HumidityToLocationRanges { get; }
	
	readonly string[] input;
	
	long lowestLocation;

	public Almanac(string[] input) {
		this.input = input;
		this.lowestLocation = long.MaxValue;
		this.SeedRanges = SeedRangesSorted();
		this.SeedToSoilRanges = new List<Ranges>();
		this.SoilToFertilizerRanges = new List<Ranges>();
		this.FertilizerToWaterRanges = new List<Ranges>();
		this.WaterToLightRanges = new List<Ranges>();
		this.LightToTemperatureRanges = new List<Ranges>();
		this.TemperatureToHumidityRanges = new List<Ranges>();
		this.HumidityToLocationRanges = new List<Ranges>();
	}
	
	public void InitializeRanges() {
		this.SeedToSoilRanges.AddRange(Ranges("seed-to-soil map:").OrderBy(x => x.SourceRangeStart));
		this.SoilToFertilizerRanges.AddRange(Ranges("soil-to-fertilizer map:").OrderBy(x => x.SourceRangeStart));
		this.FertilizerToWaterRanges.AddRange(Ranges("fertilizer-to-water map:").OrderBy(x => x.SourceRangeStart));
		this.WaterToLightRanges.AddRange(Ranges("water-to-light map:").OrderBy(x => x.SourceRangeStart));
		this.LightToTemperatureRanges.AddRange(Ranges("light-to-temperature map:").OrderBy(x => x.SourceRangeStart));
		this.TemperatureToHumidityRanges.AddRange(Ranges("temperature-to-humidity map:").OrderBy(x => x.SourceRangeStart));
		this.HumidityToLocationRanges.AddRange(Ranges("humidity-to-location map:").OrderBy(x => x.SourceRangeStart));
	}

	public long ProcessAllSeeds() {
		foreach (var seedRange in this.SeedRanges) {
			var totalRange = seedRange.Key + seedRange.Value;
			var lowestLocation = FindLowestLocationForSeedRange(seedRange.Key, totalRange);
			if (lowestLocation < this.lowestLocation) {
				this.lowestLocation = lowestLocation;
			}
		}

		return this.lowestLocation;
	}

	public async Task<long> ProcessAllSeedsAsync() {
		var tasks = new List<Task<long>>();

		var results = await Task.WhenAll(tasks);

		return results.Min();
	}
	
	async Task<long> ProcessSeedRange(long start, long end) {
		return await Task.FromResult(0);
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
	
	Dictionary<long, long> SeedRangesSorted() {
		var seedRanges = new Dictionary<long, long>();
		var seedRangesString = this.input[0][(this.input[0].IndexOf(':') + 2)..].Split(' ');
		for (var i = 0; i < seedRangesString.Length; i+=2) {
			seedRanges.Add(long.Parse(seedRangesString[i]), long.Parse(seedRangesString[i +1]));
		}
		
		return seedRanges.OrderBy(x=> x.Key).ToDictionary(x => x.Key, x => x.Value);
	}
	
	List<Ranges> Ranges(string rangeIndicator) {
		var startIndex = (from line in this.input where line.StartsWith(rangeIndicator) select Array.IndexOf(this.input, line) + 1).FirstOrDefault();
		var endIndex = startIndex;
		for (var i = startIndex; i < this.input.Length; i++) {
			if (this.input[i].Trim() == string.Empty || i == this.input.Length - 1) {
				endIndex = i;
				break;
			}	
		}
		
		var ranges = new List<Ranges>();
		for (var i = startIndex; i < endIndex; i++) {
			var map = this.input[i].Split(' ').Select(long.Parse).ToArray();
			ranges.Add(new Ranges(map[0], map[1], map[2]));
		}

		return ranges;
	}
	
	
	long FindLowestLocationForSeedRange(long seedStartRange, long totalRange) {
		var lowestLocation = long.MaxValue;
		for (var i = seedStartRange; i < totalRange; i++) {
			var currentMinLocation = CalculateCurrentMinLocation(i);
			if (currentMinLocation < lowestLocation) {
				lowestLocation = currentMinLocation;
			}
		}
		return lowestLocation;
	}

	long CalculateCurrentMinLocation(long seed) {
		var location = long.MaxValue;
	
		var soilStartRange = FindCorrespondingDestination(seed, this.SeedToSoilRanges);
		var fertilizerStartRange = FindCorrespondingDestination(soilStartRange, this.SoilToFertilizerRanges);
		var waterStartRange = FindCorrespondingDestination(fertilizerStartRange, this.FertilizerToWaterRanges);
		var lightStartRange = FindCorrespondingDestination(waterStartRange, this.WaterToLightRanges);
		var temperatureStartRange = FindCorrespondingDestination(lightStartRange, this.LightToTemperatureRanges);
		var humidityStartRange = FindCorrespondingDestination(temperatureStartRange, this.TemperatureToHumidityRanges);
		var currentLocation = FindCorrespondingDestination(humidityStartRange, this.HumidityToLocationRanges);

		if (currentLocation < location) {
			location = currentLocation;
		}
	
		return location;
	}

	static long FindCorrespondingDestination(long start, List<Ranges> ranges) {
		var destination = -1L;
		foreach (var range in ranges) {
			if (start >= range.SourceRangeStart && start < range.SourceRangeStart + range.RangeLength) {
				destination = range.DestinationRangeStart + (start - range.SourceRangeStart);
				break;
			}
		}

		if (destination == -1) {
			destination = start;
		}

		return destination;
	}
}

public record struct Ranges(long DestinationRangeStart, long SourceRangeStart, long RangeLength);