using System.Diagnostics;
using System.Text.RegularExpressions;
using Advent_of_Code.Year_2023_Day_18;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        [GeneratedRegex("(?<Direction>[DLRU])\\s*(?<Distance>\\d+)\\s*\\(#(?<HexDistance>[0-9a-f]{5})(?<HexDirection>[0-3])\\)")]
        private static partial Regex Day_18_DigPlanRegex();

        public string Day_18(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long totalCubicMeters = 0;

            Dictionary<(int X, int Y), Hole> holes = [];
            Hole hole;
            Hole previousHole = new();
            char previousDirection = '?';

            Position currentPosition = new(0, 0);

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var matchGroups = Day_18_DigPlanRegex().Match(line).Groups;

                var direction = matchGroups["Direction"].Captures[0].Value[0];
                var distance = int.Parse(matchGroups["Distance"].Captures[0].Value);
                var hexDistance = Convert.ToInt64(matchGroups["HexDistance"].Captures[0].Value, 16);
                var hexDirection = "RDLU"[int.Parse(matchGroups["HexDirection"].Captures[0].Value)];

                Debug.WriteLine($"{hexDirection} {hexDistance}");

                if ("UDLR".Contains(direction))
                {
                    while (distance > 0)
                    {
                        currentPosition.X += direction switch { 'L' => -1, 'R' => 1, _ => 0 };
                        currentPosition.Y += direction switch { 'U' => -1, 'D' => 1, _ => 0 };

                        if (previousDirection == 'U' && "LR".Contains(direction))
                        {
                            previousHole.North = direction == 'L' ? 'R' : 'L';
                        }
                        else if (previousDirection == 'D' && "LR".Contains(direction))
                        {
                            previousHole.South = direction;
                        }

                        hole = new()
                        {
                            Position = currentPosition,
                            North = direction switch { 'R' => 'L', 'L' => 'R', _ => 'U' },
                            South = direction switch { 'R' => 'R', 'L' => 'L', _ => 'U' }
                        };

                        if (direction == 'U' && "LR".Contains(previousDirection))
                        {
                            hole.South = previousDirection;
                        }
                        else if (direction == 'D' && "LR".Contains(previousDirection))
                        {
                            hole.North = previousDirection == 'L' ? 'R' : 'L';
                        }

                        holes.Add((currentPosition.X, currentPosition.Y), hole);
                        previousHole = hole;

                        distance--;
                    }

                    previousDirection = direction;
                }
            }

            var minX = holes.Min(h => h.Key.X);
            var maxX = holes.Max(h => h.Key.X);
            var minY = holes.Min(h => h.Key.Y);
            var maxY = holes.Max(h => h.Key.Y);

            for (var column = minX; column <= maxX; column++)
            {
                bool inLoop = false;

                for (
                    var row = holes.Where(h => h.Key.X == column).Min(h => h.Key.Y);
                    row <= holes.Where(h => h.Key.X == column).Max(h => h.Key.Y);
                    row++)
                {
                    if (holes.Any(h => h.Key.X == column && h.Key.Y == row))
                    {
                        totalCubicMeters++;

                        hole = holes.First(h => h.Key.X == column && h.Key.Y == row).Value;

                        if (hole.South == 'R')
                        {
                            inLoop = true;
                        }
                        else if (hole.South == 'L')
                        {
                            inLoop = false;
                        }
                    }
                    else if (inLoop)
                    {
                        totalCubicMeters++;
                    }
                }
            }

            stopwatch.Stop();

            return $"{totalCubicMeters:N0} cubic meters of lava could be held\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}