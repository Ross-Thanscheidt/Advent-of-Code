using System.Diagnostics;
using Advent_of_Code.Year_2024_Day_20;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_20(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long cheatsMove2 = 0;
            long cheatsMove20 = 0;

            Dictionary<Position, int> map = [];
            Position start = new(0, 0);
            Position end = new(0, 0);

            int rows = 0;
            int columns = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                columns = line.Length;

                for (int column = 0; column < line.Length; column++)
                {
                    if (".SE".Contains(line[column]))
                    {
                        map.Add(new(column, rows), 0);

                        if (line[column] == 'S')
                        {
                            start = new(column, rows);
                        }
                        else if (line[column] == 'E')
                        {
                            end = new(column, rows);
                        }
                    }
                }

                rows++;
            }

            List<Direction> directions = [new(1, 0), new(0, 1), new(-1, 0), new(0, -1)];

            int step = 0;
            Position current = start;

            while (current != end)
            {
                foreach (Position next in directions.Select(direction => current + direction).Where(position => map.ContainsKey(position) && map[position] == 0 && position != start))
                {
                    map[next] = ++step;
                    current = next;
                }
            }

            // Part One

            int picosecondsToSave = rows > 20 ? 100 : 12;

            cheatsMove2 += map.Keys
                .SelectMany(
                    tile => directions,
                    (tile, direction) => (tile, move1: tile + direction, move2: tile + direction + direction))
                .Count(positions =>
                    !map.ContainsKey(positions.move1) &&
                    map.TryGetValue(positions.move2, out int move2steps) &&
                    map[positions.tile] < move2steps &&
                    map[positions.move2] - map[positions.tile] - 2 >= picosecondsToSave);

            // Part Two

            picosecondsToSave = rows > 20 ? 100 : 72;

            cheatsMove20 = map.Keys
                .SelectMany(tile =>
                    map.Where(target =>
                        tile.Distance(target.Key) <= 20 &&
                        target.Key != tile &&
                        target.Value > map[tile] &&
                        target.Value - map[tile] - tile.Distance(target.Key) >= picosecondsToSave))
                .Count();

            stopwatch.Stop();

            return $"{cheatsMove2:N0} cheats (2 picoseconds) would save you at least 100 picoseconds\r\n" +
                   $"{cheatsMove20:N0} cheats (up to 20 picoseconds) would save you at least 100 picoseconds\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
