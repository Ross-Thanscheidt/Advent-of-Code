using System.Diagnostics;
using Advent_of_Code.Year_2023_Day_14;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_14(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long totalLoad = 0;

            List<List<char>> map = [];
            List<Position> roundRocks = [];
            List<Position> cubeRocks = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                map.Add([.. line.ToCharArray()]);
            }

            var rows = map.Count;
            var columns = map[0].Count;

            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < columns; columnIndex++)
                {
                    var item = map[rowIndex][columnIndex];
                    if (item == 'O')
                    {
                        roundRocks.Add(new(columnIndex + 1, rows - rowIndex));
                    }
                    else if (item == '#')
                    {
                        cubeRocks.Add(new(columnIndex + 1, rows - rowIndex));
                    }
                }
            }

            Debug.WriteLine($"{string.Join(" ", roundRocks.Select(r => $"({r.X},{r.Y})"))}");
            Debug.WriteLine($"{string.Join(" ", cubeRocks.Select(c => $"({c.X},{c.Y})"))}");

            foreach (var column in roundRocks.Select(r => r.X).Distinct())
            {
                //roundRocks.Where(r => r.X == column)
            }

            roundRocks
                .OrderByDescending(r => r.Y)
                .ToList()
                .ForEach(r => r.Y = Math.Max(
                    r.Y,
                    cubeRocks
                        .Where(c => c.X == r.X && c.Y > r.Y)
                        .OrderBy(c => c.Y)
                        .Select(c => c.Y)
                        .FirstOrDefault()));

            Debug.WriteLine($"{string.Join(" ", roundRocks.Select(r => $"({r.X},{r.Y})"))}");

            totalLoad = roundRocks.Select(r => r.Y).Sum();

            //for (var columnIndex = 0; columnIndex < map[0].Count; columnIndex++)
            //{
            //    for (var rowIndex = 0; rowIndex < map.Count; rowIndex++)
            //    {
            //        if (map[rowIndex][columnIndex] == 'O')
            //        {
            //            var targetRowIndex = rowIndex - 1;

            //            while (targetRowIndex >= 0 && map[targetRowIndex][columnIndex] == '.')
            //            {
            //                map[targetRowIndex][columnIndex] = 'O';
            //                map[targetRowIndex+1][columnIndex] = '.';
            //                targetRowIndex--;
            //            }

            //            totalLoad += map.Count - (targetRowIndex + 1);
            //        }
            //    }
            //}

            stopwatch.Stop();

            return $"{totalLoad:N0} is the total load on the north support beams\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}