using System.Diagnostics;
using System.Text.RegularExpressions;
using Advent_of_Code.Year_2023_Day_11;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        [GeneratedRegex("#")]
        private static partial Regex Day_11_GalaxyRegex();

        public string Day_11(StringReader input)
        {
            long[] EXPAND_FACTOR = [2];

            long[] EXPAND_FACTOR_OLD_TEST = [10, 100];
            long[] EXPAND_FACTOR_OLD = [1_000_000];

            Stopwatch stopwatch = Stopwatch.StartNew();

            Dictionary<long, long> totalSteps = [];
            Dictionary<long, long> totalStepsOld = [];

            List<string> map = [];
            List<Position> galaxies = [];
            int currentRow = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                map.Add(line);
                if (line.Contains('#'))
                {
                    var galaxyColumns = Day_11_GalaxyRegex().Matches(line).Cast<Match>().Select(m => m.Index).ToList();
                    foreach (var galaxyColumn in galaxyColumns)
                    {
                        galaxies.Add(new(galaxyColumn, currentRow));
                    }
                }

                currentRow++;
            }

            bool testInput = map.Count <= 10;

            for (var part = 1; part <= 2; part++)
            {
                foreach (var expandFactor in part == 1 ? EXPAND_FACTOR : testInput ? EXPAND_FACTOR_OLD_TEST : EXPAND_FACTOR_OLD)
                {
                    List<Position> expandedGalaxies = galaxies.ConvertAll(galaxy => new Position(galaxy.X, galaxy.Y));

                    // Expand rows that do not have galaxies
                    long currentRowExpanded = 0;

                    for (currentRow = 0; currentRow < map.Count; currentRow++)
                    {
                        if (!map[currentRow].Contains('#'))
                        {
                            // Expand this row by increasing Y of each galaxy below this row
                            expandedGalaxies.ForEach(position => position.Y += position.Y > currentRowExpanded ? expandFactor - 1 : 0);
                            currentRowExpanded += expandFactor - 1;
                        }

                        currentRowExpanded++;
                    }

                    // Expand columns that do not have galaxies
                    long currentColumnExpanded = 0;

                    for (var currentColumn = 0; currentColumn < map[0].Length; currentColumn++)
                    {
                        if (!map.Any(line => line[currentColumn] == '#'))
                        {
                            // Expand this column by increasing X of each galaxy to the right of the current column
                            expandedGalaxies.ForEach(position => position.X += position.X > currentColumnExpanded ? expandFactor - 1 : 0);
                            currentColumnExpanded += expandFactor - 1;
                        }

                        currentColumnExpanded++;
                    }

                    // Count steps between each pair of expanded galaxies
                    for (var index1 = 0; index1 < expandedGalaxies.Count - 1; index1++)
                    {
                        for (var index2 = index1 + 1; index2 < expandedGalaxies.Count; index2++)
                        {
                            var galaxy1 = expandedGalaxies[index1];
                            var galaxy2 = expandedGalaxies[index2];

                            var steps = Math.Abs(galaxy2.X - galaxy1.X) + Math.Abs(galaxy2.Y - galaxy1.Y);

                            if (part == 1)
                            {
                                if (!totalSteps.ContainsKey(expandFactor))
                                {
                                    totalSteps.Add(expandFactor, 0);
                                }

                                totalSteps[expandFactor] += steps;
                            }
                            else
                            {
                                if (!totalStepsOld.ContainsKey(expandFactor))
                                {
                                    totalStepsOld.Add(expandFactor, 0);
                                }

                                totalStepsOld[expandFactor] += steps;
                            }
                        }
                    }
                }
            }

            stopwatch.Stop();

            return $"{string.Join("\r\n", totalSteps.Select(kv => $"{kv.Value:N0} is the sum of the shortest lengths between every pair of galaxies (expanded by {kv.Key:N0})"))}\r\n" +
                   $"{string.Join("\r\n", totalStepsOld.Select(kv => $"{kv.Value:N0} is the sum of the shortest lengths between every pair of old galaxies (expanded by {kv.Key:N0})"))}\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}