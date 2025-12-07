using System.Diagnostics;
using System.Text;
using Position = (int X, int Y);

namespace Advent_of_Code
{
    public partial class Year_2025 : IYear12
    {
        public string Day_07(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long splitCount = 0;
            long timelinesCount = 0;

            Position startPosition = (0, 0);
            List<Position> splitters = [];

            int rows = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                for (int column = 0; column < line.Length; column++)
                {
                    if (line[column] == 'S')
                    {
                        startPosition = (column, rows);
                    }
                    else if (line[column] == '^')
                    {
                        splitters.Add((column, rows));
                    }
                }

                rows++;
            }

            // Part One

            Queue<Position> lasers = [];
            lasers.Enqueue(startPosition);

            StringBuilder output = new();

            while (lasers.Count > 0)
            {
                Position laser = lasers.Dequeue();

                laser.Y++;

                if (laser.Y < rows)
                {
                    if (splitters.Contains(laser))
                    {
                        splitCount++;
                        
                        if (!lasers.Contains((laser.X - 1, laser.Y)))
                        {
                            lasers.Enqueue((laser.X - 1, laser.Y));
                        }

                        if (!lasers.Contains((laser.X + 1, laser.Y)))
                        {
                            lasers.Enqueue((laser.X + 1, laser.Y));
                        }
                    }
                    else
                    {
                        if (!lasers.Contains(laser))
                        {
                            lasers.Enqueue(laser);
                        }
                    }
                }
            }

            // Part Two

            List<(Position Position, long Count)> lasersWithCount = [];
            lasersWithCount.Add((startPosition, 1));

            while (lasersWithCount.Count > 0)
            {
                (Position laser, long count) = lasersWithCount.First();
                lasersWithCount.Remove((laser, count));

                laser.Y++;

                if (laser.Y < rows)
                {
                    if (splitters.Contains(laser))
                    {
                        int i = lasersWithCount.FindIndex(item => item.Position == (laser.X - 1, laser.Y));

                        if (i >= 0)
                        {
                            lasersWithCount[i] = (lasersWithCount[i].Position, lasersWithCount[i].Count + count);
                        }
                        else
                        {
                            lasersWithCount.Add(((laser.X - 1, laser.Y), count));
                        }

                        i = lasersWithCount.FindIndex(item => item.Position == (laser.X + 1, laser.Y));

                        if (i >= 0)
                        {
                            lasersWithCount[i] = (lasersWithCount[i].Position, lasersWithCount[i].Count + count);
                        }
                        else
                        {
                            lasersWithCount.Add(((laser.X + 1, laser.Y), count));
                        }
                    }
                    else
                    {
                        lasersWithCount.Add((laser, count));
                    }
                }
                else
                {
                    timelinesCount += count;
                }
            }

            stopwatch.Stop();

            return $"{splitCount:N0} is how many times the beam will be split\r\n" +
                   $"{timelinesCount:N0} is how many different timelines on whioh a single tachyon particle would end up\r\n" +
                   output +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
