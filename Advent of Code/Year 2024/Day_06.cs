using System.Diagnostics;
using Direction = (int dx, int dy);
using Position = (int X, int Y);

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_06(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int positions = 0;
            int successfulObstructions = 0;

            Dictionary<Position, char> lab = [];
            Position guardStart = (0, 0);

            int rows = 0;
            int columns = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                columns = line.Length;

                for (int column = 0; column < columns; column++)
                {
                    lab.Add((column, rows), line[column]);

                    if (line[column] == '^')
                    {
                        guardStart = (column, rows);
                    }
                }

                rows++;
            }

            Position nextPos;
            Direction[] directions = [(0, -1), (1, 0), (0, 1), (-1, 0)];
            Direction direction;

            // Part One

            Position guard = guardStart;
            int directionsIndex = 0;

            while (guard.X >= 0 && guard.X < columns &&
                   guard.Y >= 0 && guard.Y < rows)
            {
                lab[guard] = 'X';

                direction = directions[directionsIndex];
                nextPos = (guard.X + direction.dx, guard.Y + direction.dy);

                if (nextPos.X < 0 || nextPos.X >= columns ||
                    nextPos.Y < 0 || nextPos.Y >= rows)
                {
                    guard = nextPos;
                }
                else
                {
                    if (lab[nextPos] == '#')
                    {
                        directionsIndex++;

                        if (directionsIndex == directions.Length)
                        {
                            directionsIndex = 0;
                        }
                    }
                    else
                    {
                        guard = nextPos;
                    }
                }
            }

            positions = lab.Count(kv => kv.Value == 'X');

            // Part Two

            Position obstacle;

            for (obstacle.Y = 0; obstacle.Y < rows; obstacle.Y++)
            {
                for (obstacle.X = 0; obstacle.X < columns; obstacle.X++)
                {
                    if (!"#^".Contains(lab[(obstacle.X, obstacle.Y)]))
                    {
                        lab[obstacle] = '#';

                        Dictionary<Position, List<Direction>> positionDirections = [];

                        guard = guardStart;
                        directionsIndex = 0;

                        while (guard.X >= 0 && guard.X < columns &&
                               guard.Y >= 0 && guard.Y < rows)
                        {
                            direction = directions[directionsIndex];

                            if (positionDirections.ContainsKey(guard) && positionDirections[guard].Any(d => d == direction))
                            {
                                successfulObstructions++;
                                break;
                            }

                            if (!positionDirections.TryGetValue(guard, out List<Direction>? guardDirections))
                            {
                                guardDirections = [];
                                positionDirections.Add(guard, guardDirections);
                            }

                            guardDirections.Add(direction);

                            nextPos = (guard.X + direction.dx, guard.Y + direction.dy);

                            if (nextPos.X < 0 || nextPos.X >= columns ||
                                nextPos.Y < 0 || nextPos.Y >= rows)
                            {
                                guard = nextPos;
                            }
                            else
                            {
                                if (lab[nextPos] == '#')
                                {
                                    directionsIndex++;

                                    if (directionsIndex == directions.Length)
                                    {
                                        directionsIndex = 0;
                                    }
                                }
                                else
                                {
                                    guard = nextPos;
                                }
                            }
                        }

                        lab[obstacle] = '.';
                    }
                }
            }

            stopwatch.Stop();

            return $"{positions:N0} distinct positions visited by the guard\r\n" +
                   $"{successfulObstructions:N0} different obstruction positions get the guard stuck in a loop\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
