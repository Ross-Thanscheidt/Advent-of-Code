using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_17(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long leastHeatLoss = 0;

            List<string> map = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    map.Add(line);
                }
            }

            var columns = map[0].Length;
            var rows = map.Count;

            stopwatch.Stop();

            return $"{leastHeatLoss:N0} is the least heat loss the crucible can incur\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}