namespace AdventOfCode23.Day11;

public record struct Universe(int UniverseId, int Row, int Column, List<int> PartnerUniverseIds, List<Step> Steps);

public record struct Step(int Row, int Column);