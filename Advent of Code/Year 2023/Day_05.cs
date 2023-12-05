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
                                Source = sourceName,
                                Destination = destinationName,
                                SourceFirst = sourceRangeStart,
                                SourceLast = sourceRangeStart + rangeLength - 1,
                                DestinationFirst = destinationRangeStart,
                                DestinationLast = destinationRangeStart + rangeLength - 1
                            });
                    }
                }
            }

            foreach (var seed in seeds)
            {
                sourceName = "seed";
                var sourceNumber = seed;

                while (sourceName != "location")
                {
                    var range = maps
                        .Where(
                            m => m.Source == sourceName &&
                            sourceNumber >= m.SourceFirst &&
                            sourceNumber <= m.SourceLast);

                    if (range.Any())
                    {
                        Map map = range.First();
                        sourceName = map.Destination;
                        sourceNumber = map.DestinationFirst + sourceNumber - map.SourceFirst;
                    }
                    else
                    {
                        sourceName = maps.First(m => m.Source == sourceName).Destination;
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
