using System.Diagnostics;
using Advent_of_Code.Year_2023_Day_05;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_05(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long lowestLocationNumberSeed = 0;
            long lowestLocationNumberRange = 0;

            List<long> seeds = [];
            List<Map> maps = [];

            string sourceName = "";
            string destinationName = "";

            // Load the puzzle input into seeds and maps
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    if (line.StartsWith("seeds: "))
                    {
                        seeds = line["seeds: ".Length..]
                            .Split(" ")
                            .Select(s => long.Parse(s))
                            .ToList();
                    }
                    else if (line.Contains("map:"))
                    {
                        sourceName = line.Split(" ")[0].Split("-to-")[0];
                        destinationName = line.Split(" ")[0].Split("-to-")[1];
                    }
                    else if (Char.IsDigit(line[0]))
                    {
                        var numbers = line.Split(" ").Select(n => long.Parse(n)).ToList();

                        long destinationRangeStart = numbers[0];
                        long sourceRangeStart = numbers[1];
                        long rangeLength = numbers[2];

                        maps.Add(
                            new Map
                            {
                                SourceName = sourceName,
                                DestinationName = destinationName,
                                SourceFirst = sourceRangeStart,
                                SourceLast = sourceRangeStart + rangeLength - 1,
                                DestinationFirst = destinationRangeStart,
                                DestinationLast = destinationRangeStart + rangeLength - 1,
                                Active = true
                            });
                    }
                }
            }

            // Consolidate maps into one seed-to-location map
            while (maps.Any(m => m.SourceName != "seed"))
            {
                var seedMapDestinationName = maps.First(m => m.SourceName == "seed").DestinationName;
                var nextMapDestinationName = maps.First(m => m.SourceName == seedMapDestinationName).DestinationName;

                // Merge all seed mappings into the next mapping
                while (maps.Any(m => m.SourceName == "seed"))
                {
                    var seedMap = maps.First(m => m.SourceName == "seed");

                    var nextMap = maps.Find(m =>
                        m.SourceName == seedMap.DestinationName &&
                        seedMap.DestinationFirst <= m.SourceLast &&
                        seedMap.DestinationLast >= m.SourceFirst &&
                        m.Active);

                    // These names will match if a nextMap range was found
                    if (nextMap.SourceName == seedMap.DestinationName)
                    {
                        // Determine range that is in both seedMap and nextMap
                        var commonRangeFirst = Math.Max(seedMap.DestinationFirst, nextMap.SourceFirst);
                        var commonRangeLast = Math.Min(seedMap.DestinationLast, nextMap.SourceLast);

                        // Add range to next mapping (same mapping as nextMap)
                        var map = new Map
                        {
                            SourceName = nextMap.SourceName,
                            DestinationName = nextMap.DestinationName,
                            SourceFirst = seedMap.SourceFirst + (commonRangeFirst - seedMap.DestinationFirst),
                            SourceLast = seedMap.SourceFirst + (commonRangeLast - seedMap.DestinationFirst),
                            DestinationFirst = nextMap.DestinationFirst + (commonRangeFirst - nextMap.SourceFirst),
                            DestinationLast = nextMap.DestinationFirst + (commonRangeLast - nextMap.SourceFirst),
                            Active = false
                        };

                        maps.Add(map);

                        // Add unmatched range before commonRange to next mapping
                        if (seedMap.DestinationFirst < commonRangeFirst)
                        {
                            map = new Map
                            {
                                SourceName = seedMap.SourceName,
                                DestinationName = seedMap.DestinationName,
                                SourceFirst = seedMap.SourceFirst,
                                SourceLast = seedMap.SourceFirst + (commonRangeFirst - seedMap.DestinationFirst) - 1,
                                DestinationFirst = seedMap.DestinationFirst,
                                DestinationLast = commonRangeFirst - 1,
                                Active = false
                            };

                            maps.Add(map);
                        }

                        // Add unmatched range after commonRange to next mapping
                        if (seedMap.DestinationLast > commonRangeLast)
                        {
                            map = new Map
                            {
                                SourceName = seedMap.SourceName,
                                DestinationName = seedMap.DestinationName,
                                SourceFirst = seedMap.SourceFirst + (commonRangeLast - seedMap.DestinationFirst) + 1,
                                SourceLast = seedMap.SourceLast,
                                DestinationFirst = commonRangeLast + 1,
                                DestinationLast = seedMap.DestinationLast,
                                Active = false
                            };

                            maps.Add(map);
                        }
                    }
                    else
                    {
                        // Add this mapping to the next mapping
                        var map = new Map
                        {
                            SourceName = seedMapDestinationName,
                            DestinationName = nextMapDestinationName,
                            SourceFirst = seedMap.SourceFirst,
                            SourceLast = seedMap.SourceLast,
                            DestinationFirst = seedMap.DestinationFirst,
                            DestinationLast = seedMap.DestinationLast,
                            Active = false
                        };

                        maps.Add(map);
                    }

                    // This mapping has been merged into the next mapping and can be removed
                    maps.Remove(seedMap);
                }

                // The new mappings added to the next mapping are indicated by Active being false
                var newMaps = maps
                    .Where(m =>
                        m.SourceName == seedMapDestinationName &&
                        m.DestinationName == nextMapDestinationName &&
                        !m.Active)
                    .ToList();

                // Remove old Source ranges from the next mapping if new mappings with the same Source ranges were added
                foreach (var newMap in newMaps)
                {
                    // Deal with all old mappings whose Source range intersects with the new mapping
                    while (maps.Any(m =>
                        m.SourceName == newMap.SourceName &&
                        m.DestinationName == newMap.DestinationName &&
                        m.Active &&
                        newMap.SourceFirst <= m.SourceLast &&
                        newMap.SourceLast >= m.SourceFirst))
                    {
                        var oldMap = maps.First(m =>
                            m.SourceName == newMap.SourceName &&
                            m.DestinationName == newMap.DestinationName &&
                            m.Active &&
                            newMap.SourceFirst <= m.SourceLast &&
                            newMap.SourceLast >= m.SourceFirst);

                        var commonFirst = Math.Max(oldMap.SourceFirst, newMap.SourceFirst);
                        var commonLast = Math.Min(oldMap.SourceLast, newMap.SourceLast);

                        if (commonFirst == oldMap.SourceFirst && commonLast == oldMap.SourceLast)
                        {
                            maps.Remove(oldMap);
                        }
                        else {
                            var updateMap = new Map
                            {
                                SourceName = oldMap.SourceName,
                                DestinationName = oldMap.DestinationName,
                                SourceFirst = oldMap.SourceFirst,
                                SourceLast = oldMap.SourceLast,
                                DestinationFirst = oldMap.DestinationFirst,
                                DestinationLast = oldMap.DestinationLast,
                                Active = oldMap.Active
                            };

                            if (commonFirst == oldMap.SourceFirst)
                            {
                                // Remove matching beginning of the Source range
                                updateMap.SourceFirst = commonLast + 1;
                                updateMap.DestinationFirst += (commonLast + 1 - oldMap.SourceFirst);
                            }
                            else if (commonLast == oldMap.SourceLast)
                            {
                                // Remove matching end of the Source range
                                updateMap.SourceLast = commonFirst - 1;
                                updateMap.DestinationLast = oldMap.DestinationFirst + (commonFirst - 1 - oldMap.SourceFirst);
                            }
                            else
                            {
                                // Add mapping of unmatched range at the end of the Source range
                                var addMap = new Map
                                {
                                    SourceName = oldMap.SourceName,
                                    DestinationName = oldMap.DestinationName,
                                    SourceFirst = commonLast + 1,
                                    SourceLast = oldMap.SourceLast,
                                    DestinationFirst = oldMap.DestinationFirst + (commonLast + 1 - oldMap.SourceFirst),
                                    DestinationLast = oldMap.DestinationLast,
                                    Active = oldMap.Active
                                };

                                maps.Add(addMap);

                                // Add mapping of unmatched range at the beginning of the Source range
                                updateMap.SourceLast = commonFirst - 1;
                                updateMap.DestinationLast = oldMap.DestinationFirst + (commonLast - 1 - oldMap.SourceFirst);
                            }

                            // Update oldMap by removing it and adding updateMap
                            maps.Remove(oldMap);
                            maps.Add(updateMap);
                        }
                    }
                }

                // Rename SourceName of the next mapping to "seed"
                while (maps.Any(m => m.SourceName == seedMapDestinationName))
                {
                    var oldMap = maps.First(m => m.SourceName == seedMapDestinationName);

                    var newMap = new Map
                    {
                        SourceName = "seed",
                        DestinationName = oldMap.DestinationName,
                        SourceFirst = oldMap.SourceFirst,
                        SourceLast = oldMap.SourceLast,
                        DestinationFirst = oldMap.DestinationFirst,
                        DestinationLast = oldMap.DestinationLast,
                        Active = oldMap.Active
                    };

                    maps.Add(newMap);
                    maps.Remove(oldMap);
                }
            }

            // Use the merged maps for Part One
            foreach (var seed in seeds)
            {
                var sourceNumber = seed;

                var range = maps
                    .Where(m =>
                        sourceNumber >= m.SourceFirst &&
                        sourceNumber <= m.SourceLast);

                if (range.Any())
                {
                    sourceNumber = range.Select(m => m.DestinationFirst + sourceNumber - m.SourceFirst).First();
                }

                if (lowestLocationNumberSeed == 0 || sourceNumber < lowestLocationNumberSeed)
                {
                    lowestLocationNumberSeed = sourceNumber;
                }
            }

            // Use the merged maps for Part Two
            for (var index = 0; index + 1 < seeds.Count; index += 2)
            {
                var seedFirst = seeds[index];
                var seedLast = seedFirst + seeds[index + 1] - 1;

                var sourceNumber = seedFirst;

                var range = maps
                    .Where(m =>
                        seedFirst <= m.SourceLast &&
                        seedLast >= m.SourceFirst);

                if (range.Any())
                {
                    var lowestTranslatedSourceNumber = range.Min(m => m.DestinationFirst + Math.Max(seedFirst, m.SourceFirst) - m.SourceFirst);
                    sourceNumber = Math.Min(sourceNumber, lowestTranslatedSourceNumber);
                }

                if (lowestLocationNumberRange == 0 || sourceNumber < lowestLocationNumberRange)
                {
                    lowestLocationNumberRange = sourceNumber;
                }
            }

            stopwatch.Stop();

            return $"{lowestLocationNumberSeed:N0} is the lowest location number that corresponds to any of the initial seed numbers\r\n" +
                   $"{lowestLocationNumberRange:N0} is the lowest location number that corresponds to any of the initial seed numbers\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}