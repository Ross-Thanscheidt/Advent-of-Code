using System.Diagnostics;

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
            List<(string Source, string Destination, long DestRangeStart, long SourceRangeStart, long RangeLength)> map = [];

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
                        map.Add((sourceName, destinationName, numbers[0], numbers[1], numbers[2]));
                    }
                }
            }

            foreach (var seed in seeds)
            {
                sourceName = "seed";
                var sourceNumber = seed;

                while (sourceName != "location")
                {
                    var range = map
                        .Where(
                            m => m.Source == sourceName &&
                            sourceNumber >= m.SourceRangeStart &&
                            sourceNumber <= m.SourceRangeStart + m.RangeLength);

                    if (range.Any())
                    {
                        var (_, Destination, DestRangeStart, SourceRangeStart, _) = range.First();
                        sourceName = Destination;
                        sourceNumber = DestRangeStart + sourceNumber - SourceRangeStart;
                    }
                    else
                    {
                        sourceName = map.First(m => m.Source == sourceName).Destination;
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
