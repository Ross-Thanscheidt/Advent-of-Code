using System.Text.RegularExpressions;
using Advent_of_Code.Extensions.Year_2021.Day_05;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_05(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            Dictionary<(int,int), int> pointCountsNotDiagonal = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> pointCountsDiagonal = new Dictionary<(int, int), int>();
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var match = new Regex(@"(?<x1>\d+),(?<y1>\d+)\s*->\s*(?<x2>\d+),(?<y2>\d+)").Match(line);
                if (match.Success)
                {
                    var x1 = match.GetInt("x1");
                    var y1 = match.GetInt("y1");
                    var x2 = match.GetInt("x2");
                    var y2 = match.GetInt("y2");

                    var x = x1;
                    var y = y1;
                    var dx = x1 < x2 ? 1 : x1 > x2 ? -1 : 0;
                    var dy = y1 < y2 ? 1 : y1 > y2 ? -1 : 0;
                    for (var done = false; !done; x += dx, y += dy)
                    {
                        if (dx == 0 || dy == 0)
                        {
                            pointCountsNotDiagonal.IncrementPointCount(x, y);
                        }
                        pointCountsDiagonal.IncrementPointCount(x, y);
                        done = (x == x2) && (y == y2);
                    }
                }
            }

            var overlappedPointsNotDiagonal = pointCountsNotDiagonal.Where(c => c.Value > 1).Count();
            var overlappedPointsDiagonal = pointCountsDiagonal.Where(c => c.Value > 1).Count();

            var endTimestamp = DateTime.Now;

            return $"{overlappedPointsNotDiagonal:N0} Overlapped Points (Horizontal and Vertial)\r\n" +
                   $"{overlappedPointsDiagonal:N0} Overlapped Points (including Diagonal)\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}
