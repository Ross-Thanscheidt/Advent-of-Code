using System.Diagnostics;
using System.Text.RegularExpressions;
using Advent_of_Code.Year_2023_Day_11;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_11(StringReader input)
        {
            const long EXPAND_FACTOR = 2;
            const long EXPAND_FACTOR_OLD = 1_000_000;

            Stopwatch stopwatch = Stopwatch.StartNew();

            long totalSteps = 0;
            long totalStepsOld = 0;

            List<string> map = [];
            List<Position> galaxies = [];
            List<Position> galaxiesOld = [];

            long currentRow = 0;
            long currentRowOld = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                map.Add(line);
                if (line.Contains('#'))
                {
                    var galaxyColumns = Regex.Matches(line, "#").Cast<Match>().Select(m => m.Index).ToList();
                    foreach (var galaxyColumn in galaxyColumns)
                    {
                        galaxies.Add(new(galaxyColumn, currentRow));
                        galaxiesOld.Add(new(galaxyColumn, currentRowOld));
                    }
                }
                else
                {
                    // Expand rows without galaxies
                    currentRow += EXPAND_FACTOR - 1;
                    currentRowOld += EXPAND_FACTOR_OLD - 1;
                }

                currentRow++;
                currentRowOld++;
            }

            // Expand columns without galaxies
            long currentColumn = 0;
            long currentColumnOld = 0;

            for (var columnIndex = 0; columnIndex < map[0].Length; columnIndex++)
            {
                if (!map.Any(line => line[columnIndex] == '#'))
                {
                    // Expand this column by adding 1 to X of each galaxy to the right of the current column
                    galaxies.ForEach(position => position.X += position.X > currentColumn ? EXPAND_FACTOR - 1 : 0);
                    currentColumn += EXPAND_FACTOR - 1;

                    // Expand this column by adding 1,000,000 to X of each galaxy to the right of the current column
                    galaxiesOld.ForEach(position => position.X += position.X > currentColumnOld ? EXPAND_FACTOR_OLD - 1 : 0);
                    currentColumnOld += EXPAND_FACTOR_OLD - 1;
                }

                currentColumn++;
                currentColumnOld++;
            }

            // Count steps for Part One galaxies (each empty row/column expands by a factor of two)
            for (var index1 = 0; index1 < galaxies.Count - 1; index1++)
            {
                for (var index2 = index1 + 1; index2 < galaxies.Count; index2++)
                {
                    var galaxy1 = galaxies[index1];
                    var galaxy2 = galaxies[index2];

                    totalSteps += Math.Abs(galaxy2.X - galaxy1.X) + Math.Abs(galaxy2.Y - galaxy1.Y);
                }
            }

            // Count steps for Part Two galaxies (each empty row/column expands by a factor of 1 million)
            for (var index1 = 0; index1 < galaxiesOld.Count - 1; index1++)
            {
                for (var index2 = index1 + 1; index2 < galaxiesOld.Count; index2++)
                {
                    var galaxy1 = galaxiesOld[index1];
                    var galaxy2 = galaxiesOld[index2];

                    totalStepsOld += Math.Abs(galaxy2.X - galaxy1.X) + Math.Abs(galaxy2.Y - galaxy1.Y);
                }
            }

            stopwatch.Stop();

            return $"{totalSteps:N0} is the sum of the shortest lengths between every pair of galaxies\r\n" +
                   $"{totalStepsOld:N0} is the sum of the shortest lengths between every pair of old galaxies\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}