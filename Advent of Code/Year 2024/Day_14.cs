using System.Diagnostics;
using System.Text;
using Position = (int X, int Y);
using Velocity = (int dx, int dy);

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_14(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long safetyFactor = 0;
            long secondsToEasterEgg = 0;
            StringBuilder treeDisplay = new();

            List<(Position Position, Velocity Velocity)> robotsInitialPositions = [];
            List<(Position Position, Velocity Velocity)> robots;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var positionXY = line.Split(' ')[0].Split('=')[1].Split(',');
                var velocityXY = line.Split("v=")[1].Split(',');

                Position position = (int.Parse(positionXY[0]), int.Parse(positionXY[1]));
                Velocity velocity = (int.Parse(velocityXY[0]), int.Parse(velocityXY[1]));

                robotsInitialPositions.Add((position, velocity));
            }

            (int columns, int rows) = robotsInitialPositions.Any(r => r.Position.Y > 6) ? (101, 103) : (11, 7);

            // Part One

            robots = [.. robotsInitialPositions];

            int seconds = 100;

            for (int idx = 0; idx < robots.Count; idx++)
            {
                (Position position, Velocity velocity) = robots[idx];

                position = (position.X + velocity.dx * seconds, position.Y + velocity.dy * seconds);

                if (position.X < 0)
                {
                    position.X += columns * ((Math.Abs(position.X) - 1) / columns + 1);
                }
                else if (position.X >= columns)
                {
                    position.X %= columns;
                }

                if (position.Y < 0)
                {
                    position.Y += rows * ((Math.Abs(position.Y) - 1) / rows + 1);
                }
                else if (position.Y >= rows)
                {
                    position.Y %= rows;
                }

                robots[idx] = (position, velocity);
            }

            int NW = robots.Count(r => r.Position.X < columns / 2 && r.Position.Y < rows / 2);
            int NE = robots.Count(r => r.Position.X >= columns - columns / 2 && r.Position.Y < rows / 2);
            int SW = robots.Count(r => r.Position.X < columns / 2 && r.Position.Y >= rows - rows / 2);
            int SE = robots.Count(r => r.Position.X >= columns - columns / 2 && r.Position.Y >= rows - rows / 2);

            safetyFactor = NW * NE * SW * SE;

            // Part Two

            robots = [.. robotsInitialPositions];

            if (robots.Count > 20)
            {
                bool found = false;

                while (!found)
                {
                    // Look for the Christmas Tree Easter Egg by looking for a row with at least 20 consecutive robots
                    var robotRows = robots
                        .GroupBy(r => r.Position.Y)
                        .Where(g => g.Count() > 20)
                        .Select(g => g.Key)
                        .ToList();

                    foreach (int row in robotRows)
                    {
                        List<int> robotsColumns = [.. robots.Where(r => r.Position.Y == row).Select(r => r.Position.X).Order()];

                        int consecutive = 1;
                        int maxConsecutive = 1;

                        for (int columnIndex = 1; columnIndex < robotsColumns.Count; columnIndex++)
                        {
                            if (robotsColumns[columnIndex] - robotsColumns[columnIndex - 1] == 1)
                            {
                                consecutive++;
                                maxConsecutive = Math.Max(maxConsecutive, consecutive);
                            }
                            else
                            {
                                consecutive = 1;
                            }
                        }

                        if (maxConsecutive > 20)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        for (int row = 0; row < rows; row++)
                        {
                            for (int column = 0; column < columns; column++)
                            {
                                treeDisplay.Append(robots.Any(r => r.Position.X == column && r.Position.Y == row) ? '*' : '.');
                            }

                            treeDisplay.AppendLine();
                        }
                    }
                    else
                    {
                        secondsToEasterEgg++;

                        for (int idx = 0; idx < robots.Count; idx++)
                        {
                            (Position position, Velocity velocity) = robots[idx];

                            position = (position.X + velocity.dx, position.Y + velocity.dy);

                            if (position.X < 0)
                            {
                                position.X += columns;
                            }
                            else if (position.X >= columns)
                            {
                                position.X -= columns;
                            }

                            if (position.Y < 0)
                            {
                                position.Y += rows;
                            }
                            else if (position.Y >= rows)
                            {
                                position.Y -= rows;
                            }

                            robots[idx] = (position, velocity);
                        }
                    }
                }
            }

            stopwatch.Stop();

            return $"{safetyFactor:N0} is the safety factor after 100 seconds\r\n" +
                   $"{secondsToEasterEgg:N0} is the fewest number of seconds for the robots to display the Easter egg\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)\r\n\r\n" +
                   $"{treeDisplay}\r\n";
        }
    }
}
