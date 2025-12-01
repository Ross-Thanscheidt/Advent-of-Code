using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2025 : IYear12
    {
        public string Day_07(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long part1 = 0;
            long part2 = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
            }

            stopwatch.Stop();

            return $"{part1:N0}\r\n" +
                   $"{part2:N0}\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
