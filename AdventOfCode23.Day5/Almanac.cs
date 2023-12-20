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
	
	public void InitializeRanges() {
		this.SeedToSoilRanges.AddRange(GetRanges(3, 13));
		this.SoilToFertilizerRanges.AddRange(GetRanges(15, 31));
		this.FertilizerToWaterRanges.AddRange(GetRanges(33, 48));
		this.WaterToLightRanges.AddRange(GetRanges(50, 95));
		this.LightToTemperatureRanges.AddRange(GetRanges(97, 112));
		this.TemperatureToHumidityRanges.AddRange(GetRanges(114, 137));
		this.HumidityToLocationRanges.AddRange(GetRanges(139, 150));
	}

	public void InitializeMaps() {
		foreach (var seed in this.Seeds) {
			foreach (var seedToSoilRange in this.SeedToSoilRanges) {
				if (seed >= seedToSoilRange.sourceRangeStart && seed <= seedToSoilRange.sourceRangeStart + seedToSoilRange.rangeLength) {
					this.SeedToSoilMaps.Add(new Map(seed, seedToSoilRange.destinationRangeStart + (seed - seedToSoilRange.sourceRangeStart)));
				}
			}

			if (this.SeedToSoilMaps.All(x => x.source != seed)) {
				this.SeedToSoilMaps.Add(new Map(seed, seed));
			}
		}

		foreach (var seedToSoilMap in this.SeedToSoilMaps) {
			foreach (var soilToFertilizerRange in this.SoilToFertilizerRanges) {
				if (seedToSoilMap.destination >= soilToFertilizerRange.sourceRangeStart && seedToSoilMap.destination <= soilToFertilizerRange.sourceRangeStart + soilToFertilizerRange.rangeLength) {
					this.SoilToFertilizerMaps.Add(new Map(seedToSoilMap.destination, soilToFertilizerRange.destinationRangeStart + (seedToSoilMap.destination - soilToFertilizerRange.sourceRangeStart)));
				}
			}

			if (this.SoilToFertilizerMaps.All(x => x.source != seedToSoilMap.destination)) {
				this.SoilToFertilizerMaps.Add(new Map(seedToSoilMap.source, seedToSoilMap.destination));
			}
		}

		foreach (var soilToFertilizerMap in this.SoilToFertilizerMaps) {
			foreach (var fertilizerToWaterRange in this.FertilizerToWaterRanges) {
				if (soilToFertilizerMap.destination >= fertilizerToWaterRange.sourceRangeStart && soilToFertilizerMap.destination <= fertilizerToWaterRange.sourceRangeStart + fertilizerToWaterRange.rangeLength) {
					this.FertilizerToWaterMaps.Add(new Map(soilToFertilizerMap.destination, fertilizerToWaterRange.destinationRangeStart + (soilToFertilizerMap.destination - fertilizerToWaterRange.sourceRangeStart)));
				}
			}

			if (this.FertilizerToWaterMaps.All(x => x.source != soilToFertilizerMap.destination)) {
				this.FertilizerToWaterMaps.Add(new Map(soilToFertilizerMap.source, soilToFertilizerMap.destination));
			}
		}

		foreach (var fertilizerToWaterMap in this.FertilizerToWaterMaps) {
			foreach (var waterToLightRange in this.WaterToLightRanges) {
				if (fertilizerToWaterMap.destination >= waterToLightRange.sourceRangeStart && fertilizerToWaterMap.destination <= waterToLightRange.sourceRangeStart + waterToLightRange.rangeLength) {
					this.WaterToLightMaps.Add(new Map(fertilizerToWaterMap.destination, waterToLightRange.destinationRangeStart + (fertilizerToWaterMap.destination - waterToLightRange.sourceRangeStart)));
				}
			}
			
			if (this.WaterToLightMaps.All(x => x.source != fertilizerToWaterMap.destination)) {
				this.WaterToLightMaps.Add(new Map(fertilizerToWaterMap.source, fertilizerToWaterMap.destination));
			}
		}

		foreach (var waterToLightMap in this.WaterToLightMaps) {
			foreach (var lightToTemperatureRange in this.LightToTemperatureRanges) {
				if (waterToLightMap.destination >= lightToTemperatureRange.sourceRangeStart && waterToLightMap.destination <= lightToTemperatureRange.sourceRangeStart + lightToTemperatureRange.rangeLength) {
					this.LightToTemperatureMaps.Add(new Map(waterToLightMap.destination, lightToTemperatureRange.destinationRangeStart + (waterToLightMap.destination - lightToTemperatureRange.sourceRangeStart)));
				}
			}

			if (this.LightToTemperatureMaps.All(x => x.source != waterToLightMap.destination)) {
				this.LightToTemperatureMaps.Add(new Map(waterToLightMap.source, waterToLightMap.destination));
			}
		}

		foreach (var lightToTemperatureMap in this.LightToTemperatureMaps) {
			foreach (var temperatureToHumidityRange in this.TemperatureToHumidityRanges) {
				if (lightToTemperatureMap.destination >= temperatureToHumidityRange.sourceRangeStart && lightToTemperatureMap.destination <= temperatureToHumidityRange.sourceRangeStart + temperatureToHumidityRange.rangeLength) {
					this.TemperatureToHumidityMaps.Add(new Map(lightToTemperatureMap.destination, temperatureToHumidityRange.destinationRangeStart + (lightToTemperatureMap.destination - temperatureToHumidityRange.sourceRangeStart)));
				}
			}

			if (this.TemperatureToHumidityMaps.All(x => x.source != lightToTemperatureMap.destination)) {
				this.TemperatureToHumidityMaps.Add(new Map(lightToTemperatureMap.source, lightToTemperatureMap.destination));
			}
		}

		foreach (var temperatureToHumidityMap in this.TemperatureToHumidityMaps) {
			foreach (var humidityToLocationRange in this.HumidityToLocationRanges) {
				if (temperatureToHumidityMap.destination >= humidityToLocationRange.sourceRangeStart && temperatureToHumidityMap.destination <= humidityToLocationRange.sourceRangeStart + humidityToLocationRange.rangeLength) {
					this.HumidityToLocationMaps.Add(new Map(temperatureToHumidityMap.destination, humidityToLocationRange.destinationRangeStart + (temperatureToHumidityMap.destination - humidityToLocationRange.sourceRangeStart)));
				}
			}

			if (this.HumidityToLocationMaps.All(x => x.source != temperatureToHumidityMap.destination)) {
				this.HumidityToLocationMaps.Add(new Map(temperatureToHumidityMap.source, temperatureToHumidityMap.destination));
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
		foreach (var humidityToLocationMap in this.HumidityToLocationMaps.Where(humidityToLocationMap => humidityToLocationMap.destination > 0)) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Seed: {humidityToLocationMap.source} -> Location: {humidityToLocationMap.destination}");
		}

		Console.ResetColor();
	}

	public uint FindLowestLocation() {
		var lowestLocation = uint.MaxValue;
		foreach (var humidityToLocationMap in this.HumidityToLocationMaps) {
			if (humidityToLocationMap.destination < lowestLocation) {
				lowestLocation = humidityToLocationMap.destination;
			}
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

public record struct Ranges(uint destinationRangeStart, uint sourceRangeStart, uint rangeLength);

public record struct Map(uint source, uint destination);