using System.Diagnostics;
using Position = (int X, int Y);

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_08(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long uniqueAntinodeLocations = 0;
            long uniqueAntinodeLocationsWithResonantHarmonics = 0;

            Dictionary<char, List<Position>> map = [];

            int rows = 0;
            int columns = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                columns = line.Length;

                for (int column = 0; column < columns; column++)
                {
                    char frequency = line[column];

                    if (frequency != '.')
                    {
                        if (map.TryGetValue(frequency, out List<Position>? positions))
                        {
                            positions.Add((column, rows));
                        }
                        else
                        {
                            map.Add(frequency, [(column, rows)]);
                        }
                    }
                }

                rows++;
            }

            List<Position> antinodes = [];
            List<Position> resonatingAntinodes = [];

            foreach (char frequency in map.Keys)
            {
                foreach (Position position in map[frequency])
                {
                    foreach (Position otherPosition in map[frequency].Where(p => p != position))
                    {
                        if (!resonatingAntinodes.Contains(position))
                        {
                            resonatingAntinodes.Add(position);
                        }

                        if (!resonatingAntinodes.Contains(otherPosition))
                        {
                            resonatingAntinodes.Add(otherPosition);
                        }

                        int dx = otherPosition.X - position.X;
                        int dy = otherPosition.Y - position.Y;

                        bool firstAntinode = true;
                        Position antinode = (position.X - dx, position.Y - dy);

                        while (antinode.X >= 0 && antinode.X < columns &&
                               antinode.Y >= 0 && antinode.Y < rows)
                        {
                            if (firstAntinode && !antinodes.Contains(antinode))
                            {
                                antinodes.Add(antinode);
                            }

                            if (!resonatingAntinodes.Contains(antinode))
                            {
                                resonatingAntinodes.Add(antinode);
                            }

                            firstAntinode = false;

                            antinode = (antinode.X - dx, antinode.Y - dy);
                        }
                    }
                }
            }

            uniqueAntinodeLocations = antinodes.Count;
            uniqueAntinodeLocationsWithResonantHarmonics = resonatingAntinodes.Count;

            stopwatch.Stop();

            return $"{uniqueAntinodeLocations:N0} unique locations that contain an antinode\r\n" +
                   $"{uniqueAntinodeLocationsWithResonantHarmonics:N0} unique locations that contain an antinode considering resonant harmonics\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
