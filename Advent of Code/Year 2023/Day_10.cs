using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_10(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long maxSteps = 0;

            List<string> lines = [];

            (int X, int Y) start = (-1, -1);

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    lines.Add(line);

                    int startIndex = line.IndexOf('S');

                    if (startIndex >= 0)
                    {
                        start.X = startIndex;
                        start.Y = lines.Count - 1;
                    }
                }
            }

            int columns = lines[0].Length;
            int rows = lines.Count;

            Debug.WriteLine($"Start at ({start.X},{start.Y})");

            (int X, int Y) current = start;
            (int X, int Y) previous = start;

            while (current != start || maxSteps == 0)
            {
                if (current == start)
                {
                    previous = current;

                    if (current.Y > 0 && "|7F".Contains(lines[current.Y - 1][current.X]))
                    {
                        current.Y--;
                    }
                    else if (current.X + 1 < columns && "-J7".Contains(lines[current.Y][current.X + 1]))
                    {
                        current.X++;
                    }
                    else if (current.Y + 1 < rows && "|JL".Contains(lines[current.Y + 1][current.X]))
                    {
                        current.Y++;
                    }
                    else if (current.X > 0 && "-LF".Contains(lines[current.Y][current.X - 1]))
                    {
                        current.X--;
                    }
                }
                else
                {
                    char tile = lines[current.Y][current.X];

                    if ("|JL".Contains(tile) && current.Y > 0 && previous != (current.X, current.Y - 1))
                    {
                        previous = current;
                        current.Y--;
                    }
                    else if ("-LF".Contains(tile) && current.X + 1 < columns && previous != (current.X + 1, current.Y))
                    {
                        previous = current;
                        current.X++;
                    }
                    else if ("|7F".Contains(tile) && current.Y + 1 < rows && previous != (current.X, current.Y + 1))
                    {
                        previous = current;
                        current.Y++;
                    }
                    else if ("-J7".Contains(tile) && current.X > 0 && previous != (current.X - 1, current.Y))
                    {
                        previous = current;
                        current.X--;
                    }
                }

                maxSteps++;
            }

            maxSteps = (maxSteps + 1) / 2;

            stopwatch.Stop();

            return $"{maxSteps:N0} steps to the point farthest from the starting position\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}
