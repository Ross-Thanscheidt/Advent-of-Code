using System.Diagnostics;
using Advent_of_Code.Year_2023_Day_14;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_14(StringReader input)
        {
            const long NUMBER_OF_CYCLES = 1_000_000_000;
            const long PATTERN_SEARCH_MAX_CYCLES = 100;

            Stopwatch stopwatch = Stopwatch.StartNew();

            long totalNorthLoadTiltNorth = 0;
            long totalNorthLoadAfterCycles = 0;

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

            long cycles = 0;
            List<(long Cycles, long TotalNorthLoad)> recentCycles = [];

            bool patternFound = false;
            int lastOfPatternIndex = 0;
            int firstOfPatternIndex = 0;

            while (cycles < NUMBER_OF_CYCLES && !patternFound)
            {
                // Slide North
                foreach (var column in roundRocks.Select(r => r.X).Distinct())
                {
                    foreach (var roundRock in roundRocks.Where(r => r.X == column).OrderByDescending(r => r.Y))
                    {
                        var firstObstacleRow =
                            Math.Min(
                                roundRocks.Where(r => r.X == column && r.Y > roundRock.Y).OrderBy(r => r.Y).Select(r => r.Y).FirstOrDefault(rows + 1),
                                cubeRocks.Where(r => r.X == column && r.Y > roundRock.Y).OrderBy(r => r.Y).Select(r => r.Y).FirstOrDefault(rows + 1));

                        roundRock.Y = Math.Max(roundRock.Y, firstObstacleRow - 1);
                    }
                }

                if (totalNorthLoadTiltNorth == 0)
                {
                    totalNorthLoadTiltNorth += roundRocks.Select(r => r.Y).Sum();
                }

                // Slide West
                foreach (var row in roundRocks.Select(r => r.Y).Distinct())
                {
                    foreach (var roundRock in roundRocks.Where(r => r.Y == row).OrderBy(r => r.X))
                    {
                        var firstObstacleColumn =
                            Math.Max(
                                roundRocks.Where(r => r.X < roundRock.X && r.Y == row).OrderByDescending(r => r.X).Select(r => r.X).FirstOrDefault(0),
                                cubeRocks.Where(r => r.X < roundRock.X && r.Y == row).OrderByDescending(r => r.X).Select(r => r.X).FirstOrDefault(0));

                        roundRock.X = Math.Min(roundRock.X, firstObstacleColumn + 1);
                    }
                }

                // Slide South
                foreach (var column in roundRocks.Select(r => r.X).Distinct())
                {
                    foreach (var roundRock in roundRocks.Where(r => r.X == column).OrderBy(r => r.Y))
                    {
                        var firstObstacleRow =
                            Math.Max(
                                roundRocks.Where(r => r.X == column && r.Y < roundRock.Y).OrderByDescending(r => r.Y).Select(r => r.Y).FirstOrDefault(0),
                                cubeRocks.Where(r => r.X == column && r.Y < roundRock.Y).OrderByDescending(r => r.Y).Select(r => r.Y).FirstOrDefault(0));

                        roundRock.Y = Math.Min(roundRock.Y, firstObstacleRow + 1);
                    }
                }

                // Slide East
                foreach (var row in roundRocks.Select(r => r.Y).Distinct())
                {
                    foreach (var roundRock in roundRocks.Where(r => r.Y == row).OrderByDescending(r => r.X))
                    {
                        var firstObstacleColumn =
                            Math.Min(
                                roundRocks.Where(r => r.X > roundRock.X && r.Y == row).OrderBy(r => r.X).Select(r => r.X).FirstOrDefault(columns + 1),
                                cubeRocks.Where(r => r.X > roundRock.X && r.Y == row).OrderBy(r => r.X).Select(r => r.X).FirstOrDefault(columns + 1));

                        roundRock.X = Math.Max(roundRock.X, firstObstacleColumn - 1);
                    }
                }

                cycles++;
                totalNorthLoadAfterCycles = roundRocks.Select(r => r.Y).Sum();

                if (cycles > 100)
                {
                    recentCycles.Add((cycles, totalNorthLoadAfterCycles));

                    if (recentCycles.Count >= PATTERN_SEARCH_MAX_CYCLES)
                    {
                        while (recentCycles.Count > PATTERN_SEARCH_MAX_CYCLES)
                        {
                            recentCycles.RemoveAt(0);
                        }

                        lastOfPatternIndex = recentCycles.Count - 1;
                        firstOfPatternIndex = lastOfPatternIndex;

                        for (var recentCyclesIndex = lastOfPatternIndex - 1; recentCyclesIndex >= 0 && !patternFound; recentCyclesIndex--)
                        {
                            if (recentCycles[recentCyclesIndex].TotalNorthLoad == recentCycles[lastOfPatternIndex].TotalNorthLoad)
                            {
                                firstOfPatternIndex = recentCyclesIndex + 1;

                                patternFound = true;

                                for (var lastOfTestPattern = recentCyclesIndex; lastOfTestPattern >= 0 && patternFound; lastOfTestPattern -= (lastOfPatternIndex - firstOfPatternIndex + 1))
                                {
                                    for (
                                        var testPatternIndex = lastOfPatternIndex;
                                        testPatternIndex >= firstOfPatternIndex &&
                                            lastOfTestPattern - (lastOfPatternIndex - testPatternIndex) >= 0 &&
                                            patternFound;
                                        testPatternIndex--)
                                    {
                                        if (recentCycles[testPatternIndex].TotalNorthLoad != recentCycles[lastOfTestPattern - (lastOfPatternIndex - testPatternIndex)].TotalNorthLoad)
                                        {
                                            patternFound = false;
                                        }
                                    }
                                }

                                if (firstOfPatternIndex <= PATTERN_SEARCH_MAX_CYCLES / 2)
                                {
                                    patternFound = false;
                                }
                            }
                        }
                    }
                }
            }

            if (cycles < NUMBER_OF_CYCLES && patternFound)
            {
                var cyclesBeforePattern = recentCycles[firstOfPatternIndex].Cycles - 1;
                var cyclesInPattern = lastOfPatternIndex - firstOfPatternIndex + 1;
                var lastPatternCycle = recentCycles[firstOfPatternIndex].Cycles + ((NUMBER_OF_CYCLES - cyclesBeforePattern) / cyclesInPattern * cyclesInPattern);

                var patternIndex = firstOfPatternIndex;

                while (lastPatternCycle < NUMBER_OF_CYCLES)
                {
                    lastPatternCycle++;
                    patternIndex++;
                }

                totalNorthLoadAfterCycles = recentCycles[patternIndex].TotalNorthLoad;
            }

            stopwatch.Stop();

            return $"{totalNorthLoadTiltNorth:N0} is the total load on the north support beams\r\n" +
                   $"{totalNorthLoadAfterCycles:N0} is the total load on the north support beams after {NUMBER_OF_CYCLES:N0} cycles\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}