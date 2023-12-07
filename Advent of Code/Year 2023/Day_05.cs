using System.Diagnostics;
using Advent_of_Code.Year_2023_Day_05;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_05(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long lowestLocationNumber = 0;

            string debug = "";

            List<long> seeds = [];
            string sourceName = "";
            string destinationName = "";
            List<Map> maps = [];

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

            //sourceName = "seed";
            //while (maps.Any(m => m.SourceName != "seed"))
            //{
            //    var seedMapDestinationName = maps.First(m => m.SourceName == "seed").DestinationName;
            //    var nextMapDestinationName = maps.First(m => m.SourceName == seedMapDestinationName).DestinationName;

            //    while (maps.Any(m => m.SourceName == "seed"))
            //    {
            //        var seedMap = maps.First(m => m.SourceName == "seed");

            //        var nextMap = maps.Find(m =>
            //            m.SourceName == seedMap.DestinationName &&
            //            seedMap.DestinationFirst <= m.SourceLast &&
            //            seedMap.DestinationLast >= m.SourceFirst &&
            //            m.Active);

            //        if (nextMap.SourceName == seedMap.DestinationName)
            //        {
            //            // Find seedMap range in nextMap
            //            var commonRangeFirst = Math.Max(seedMap.DestinationFirst, nextMap.SourceFirst);
            //            var commonRangeLast = Math.Min(seedMap.DestinationLast, nextMap.SourceLast);

            //            debug += $"Looking at {seedMap.SourceName}-to-{seedMap.DestinationName} map: ({seedMap.SourceFirst},{seedMap.SourceLast}) => ({seedMap.DestinationFirst},{seedMap.DestinationLast})\r\n";
            //            debug += $"Looking at {nextMap.SourceName}-to-{nextMap.DestinationName} map: ({nextMap.SourceFirst},{nextMap.SourceLast}) => ({nextMap.DestinationFirst},{nextMap.DestinationLast})\r\n";
            //            debug += $"Common Range is ({commonRangeFirst},{commonRangeLast})\r\n";

            //            // Add range nextMapping
            //            var map = new Map
            //            {
            //                SourceName = nextMap.SourceName,
            //                DestinationName = nextMap.DestinationName,
            //                SourceFirst = seedMap.SourceFirst + (commonRangeFirst - seedMap.DestinationFirst),
            //                SourceLast = seedMap.SourceFirst + (commonRangeLast - seedMap.DestinationFirst),
            //                DestinationFirst = nextMap.DestinationFirst + (commonRangeFirst - nextMap.SourceFirst),
            //                DestinationLast = nextMap.DestinationFirst + (commonRangeLast - nextMap.SourceFirst),
            //                Active = false
            //            };
            //            debug += $"Add {map.SourceName}-to-{map.DestinationName} map: ({map.SourceFirst},{map.SourceLast}) => ({map.DestinationFirst},{map.DestinationLast})\r\n";
            //            maps.Add(map);

            //            // Add unmatched ranges to nextMap
            //            if (seedMap.DestinationFirst < commonRangeFirst)
            //            {
            //                map = new Map
            //                {
            //                    SourceName = seedMap.SourceName,
            //                    DestinationName = seedMap.DestinationName,
            //                    SourceFirst = seedMap.SourceFirst,
            //                    SourceLast = seedMap.SourceFirst + (commonRangeFirst - seedMap.DestinationFirst) - 1,
            //                    DestinationFirst = seedMap.DestinationFirst,
            //                    DestinationLast = commonRangeFirst - 1,
            //                    Active = false
            //                };
            //                debug += $"Add {map.SourceName}-to-{map.DestinationName} map: ({map.SourceFirst},{map.SourceLast}) => ({map.DestinationFirst},{map.DestinationLast})\r\n";
            //                maps.Add(map);
            //            }

            //            if (seedMap.DestinationLast > commonRangeLast)
            //            {
            //                map = new Map
            //                {
            //                    SourceName = seedMap.SourceName,
            //                    DestinationName = seedMap.DestinationName,
            //                    SourceFirst = seedMap.SourceFirst + (commonRangeLast - seedMap.DestinationFirst) + 1,
            //                    SourceLast = seedMap.SourceLast,
            //                    DestinationFirst = commonRangeLast + 1,
            //                    DestinationLast = seedMap.DestinationLast,
            //                    Active = false
            //                };
            //                debug += $"Add {map.SourceName}-to-{map.DestinationName} map: ({map.SourceFirst},{map.SourceLast}) => ({map.DestinationFirst},{map.DestinationLast})\r\n";
            //                maps.Add(map);
            //            }

            //            debug += $"Remove {seedMap.SourceName}-to-{seedMap.DestinationName} map: ({seedMap.SourceFirst},{seedMap.SourceLast}) => ({seedMap.DestinationFirst},{seedMap.DestinationLast})\r\n";
            //            maps.Remove(seedMap);
            //        }
            //        else
            //        {
            //            var map = new Map
            //            {
            //                SourceName = seedMapDestinationName,
            //                DestinationName = nextMapDestinationName,
            //                SourceFirst = seedMap.SourceFirst,
            //                SourceLast = seedMap.SourceLast,
            //                DestinationFirst = seedMap.DestinationFirst,
            //                DestinationLast = seedMap.DestinationLast,
            //                Active = false
            //            };

            //            debug += $"Add {map.SourceName}-to-{map.DestinationName} map: ({map.SourceFirst},{map.SourceLast}) => ({map.DestinationFirst},{map.DestinationLast})\r\n";
            //            maps.Add(map);

            //            debug += $"Remove {seedMap.SourceName}-to-{seedMap.DestinationName} map: ({seedMap.SourceFirst},{seedMap.SourceLast}) => ({seedMap.DestinationFirst},{seedMap.DestinationLast})\r\n";
            //            maps.Remove(seedMap);
            //        }
            //    }

            //    while (maps.Any(m => m.SourceName == seedMapDestinationName))
            //    {
            //        var map = maps.First(m => m.SourceName == seedMapDestinationName);

            //        maps.Add(new Map
            //        {
            //            SourceName = "seed",
            //            DestinationName = map.DestinationName,
            //            SourceFirst = map.SourceFirst,
            //            SourceLast = map.SourceLast,
            //            DestinationFirst = map.DestinationFirst,
            //            DestinationLast = map.DestinationLast,
            //            Active = true
            //        });

            //        maps.Remove(map);
            //    }
            //}

            foreach (var seed in seeds)
            {
                sourceName = "seed";
                var sourceNumber = seed;

                while (sourceName != "location")
                {
                    var range = maps
                        .Where(
                            m => m.SourceName == sourceName &&
                            sourceNumber >= m.SourceFirst &&
                            sourceNumber <= m.SourceLast);

                    if (range.Any())
                    {
                        Map map = range.First();
                        sourceName = map.DestinationName;
                        sourceNumber = map.DestinationFirst + sourceNumber - map.SourceFirst;
                    }
                    else
                    {
                        sourceName = maps.First(m => m.SourceName == sourceName).DestinationName;
                    }
                }

                if (lowestLocationNumber == 0 || sourceNumber < lowestLocationNumber)
                {
                    lowestLocationNumber = sourceNumber;
                }
            }

            stopwatch.Stop();

            return $"{lowestLocationNumber:N0} is the lowest location number that corresponds to any of the initial seed numbers\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}
