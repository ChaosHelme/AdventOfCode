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
			foreach (var seedToSoilRange in this.SeedToSoilRanges) {
				if (seed >= seedToSoilRange.SourceRangeStart && seed <= seedToSoilRange.SourceRangeStart + seedToSoilRange.RangeLength) {
					this.SeedToSoilMaps.Add(new Map(seed, seedToSoilRange.DestinationRangeStart + (seed - seedToSoilRange.SourceRangeStart)));
				}
			}

			if (this.SeedToSoilMaps.All(x => x.Source != seed)) {
				this.SeedToSoilMaps.Add(new Map(seed, seed));
			}
		}

		foreach (var seedToSoilMap in this.SeedToSoilMaps) {
			foreach (var soilToFertilizerRange in this.SoilToFertilizerRanges) {
				if (seedToSoilMap.Destination >= soilToFertilizerRange.SourceRangeStart && seedToSoilMap.Destination <= soilToFertilizerRange.SourceRangeStart + soilToFertilizerRange.RangeLength) {
					this.SoilToFertilizerMaps.Add(new Map(seedToSoilMap.Destination, soilToFertilizerRange.DestinationRangeStart + (seedToSoilMap.Destination - soilToFertilizerRange.SourceRangeStart)));
				}
			}

			if (this.SoilToFertilizerMaps.All(x => x.Source != seedToSoilMap.Destination)) {
				this.SoilToFertilizerMaps.Add(new Map(seedToSoilMap.Source, seedToSoilMap.Destination));
			}
		}

		foreach (var soilToFertilizerMap in this.SoilToFertilizerMaps) {
			foreach (var fertilizerToWaterRange in this.FertilizerToWaterRanges) {
				if (soilToFertilizerMap.Destination >= fertilizerToWaterRange.SourceRangeStart && soilToFertilizerMap.Destination <= fertilizerToWaterRange.SourceRangeStart + fertilizerToWaterRange.RangeLength) {
					this.FertilizerToWaterMaps.Add(new Map(soilToFertilizerMap.Destination, fertilizerToWaterRange.DestinationRangeStart + (soilToFertilizerMap.Destination - fertilizerToWaterRange.SourceRangeStart)));
				}
			}

			if (this.FertilizerToWaterMaps.All(x => x.Source != soilToFertilizerMap.Destination)) {
				this.FertilizerToWaterMaps.Add(new Map(soilToFertilizerMap.Source, soilToFertilizerMap.Destination));
			}
		}

		foreach (var fertilizerToWaterMap in this.FertilizerToWaterMaps) {
			foreach (var waterToLightRange in this.WaterToLightRanges) {
				if (fertilizerToWaterMap.Destination >= waterToLightRange.SourceRangeStart && fertilizerToWaterMap.Destination <= waterToLightRange.SourceRangeStart + waterToLightRange.RangeLength) {
					this.WaterToLightMaps.Add(new Map(fertilizerToWaterMap.Destination, waterToLightRange.DestinationRangeStart + (fertilizerToWaterMap.Destination - waterToLightRange.SourceRangeStart)));
				}
			}
			
			if (this.WaterToLightMaps.All(x => x.Source != fertilizerToWaterMap.Destination)) {
				this.WaterToLightMaps.Add(new Map(fertilizerToWaterMap.Source, fertilizerToWaterMap.Destination));
			}
		}

		foreach (var waterToLightMap in this.WaterToLightMaps) {
			foreach (var lightToTemperatureRange in this.LightToTemperatureRanges) {
				if (waterToLightMap.Destination >= lightToTemperatureRange.SourceRangeStart && waterToLightMap.Destination <= lightToTemperatureRange.SourceRangeStart + lightToTemperatureRange.RangeLength) {
					this.LightToTemperatureMaps.Add(new Map(waterToLightMap.Destination, lightToTemperatureRange.DestinationRangeStart + (waterToLightMap.Destination - lightToTemperatureRange.SourceRangeStart)));
				}
			}

			if (this.LightToTemperatureMaps.All(x => x.Source != waterToLightMap.Destination)) {
				this.LightToTemperatureMaps.Add(new Map(waterToLightMap.Source, waterToLightMap.Destination));
			}
		}

		foreach (var lightToTemperatureMap in this.LightToTemperatureMaps) {
			foreach (var temperatureToHumidityRange in this.TemperatureToHumidityRanges) {
				if (lightToTemperatureMap.Destination >= temperatureToHumidityRange.SourceRangeStart && lightToTemperatureMap.Destination <= temperatureToHumidityRange.SourceRangeStart + temperatureToHumidityRange.RangeLength) {
					this.TemperatureToHumidityMaps.Add(new Map(lightToTemperatureMap.Destination, temperatureToHumidityRange.DestinationRangeStart + (lightToTemperatureMap.Destination - temperatureToHumidityRange.SourceRangeStart)));
				}
			}

			if (this.TemperatureToHumidityMaps.All(x => x.Source != lightToTemperatureMap.Destination)) {
				this.TemperatureToHumidityMaps.Add(new Map(lightToTemperatureMap.Source, lightToTemperatureMap.Destination));
			}
		}

		foreach (var temperatureToHumidityMap in this.TemperatureToHumidityMaps) {
			foreach (var humidityToLocationRange in this.HumidityToLocationRanges) {
				if (temperatureToHumidityMap.Destination >= humidityToLocationRange.SourceRangeStart && temperatureToHumidityMap.Destination <= humidityToLocationRange.SourceRangeStart + humidityToLocationRange.RangeLength) {
					this.HumidityToLocationMaps.Add(new Map(temperatureToHumidityMap.Destination, humidityToLocationRange.DestinationRangeStart + (temperatureToHumidityMap.Destination - humidityToLocationRange.SourceRangeStart)));
				}
			}

			if (this.HumidityToLocationMaps.All(x => x.Source != temperatureToHumidityMap.Destination)) {
				this.HumidityToLocationMaps.Add(new Map(temperatureToHumidityMap.Source, temperatureToHumidityMap.Destination));
			}
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
}

public record struct Ranges(uint DestinationRangeStart, uint SourceRangeStart, uint RangeLength);

public record struct Map(uint Source, uint Destination);