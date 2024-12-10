using System.Diagnostics;
using Position = (int X, int Y);
using Direction = (int dx, int dy);

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_10(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long sumOfTrailheadScores = 0;
            long sumOfTrailheadRatings = 0;

            Dictionary<Position, int> map = [];
            Dictionary<Position, List<Position>> trailheads = [];

            int rows = 0;
            int columns = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                columns = line.Length;

                for (int column = 0; column < columns; column++)
                {
                    int height = line[column] - '0';

                    map.Add((column, rows), height);

                    if (height == 0)
                    {
                        trailheads.Add((column, rows), []);
                    }
                }

                rows++;
            }

            Direction[] directions = [(1, 0), (0, 1), (-1, 0), (0, -1)];

            foreach ((Position trailheadPosition, List<Position> trailheadTops) in trailheads)
            {
                List<Position> moves = [ trailheadPosition ];

                while (moves.Count > 0)
                {
                    Position position = moves[0];
                    moves.RemoveAt(0);

                    int height = map[position];

                    foreach (var (dx, dy) in directions)
                    {
                        Position nextPosition = (position.X + dx, position.Y + dy);

                        if (nextPosition.X >= 0 && nextPosition.X < columns &&
                            nextPosition.Y >= 0 && nextPosition.Y < rows)
                        {
                            int nextHeight = map[nextPosition];

                            if (nextHeight == height + 1)
                            {
                                if (nextHeight == 9)
                                {
                                    sumOfTrailheadRatings++;

                                    if (!trailheadTops.Contains(nextPosition))
                                    {
                                        trailheadTops.Add(nextPosition);
                                    }
                                }
                                else
                                {
                                    moves.Add(nextPosition);
                                }
                            }
                        }
                    }
                }
            }

            sumOfTrailheadScores = trailheads.Sum(t => t.Value.Count);

            stopwatch.Stop();

            return $"{sumOfTrailheadScores:N0} is the sum of the scores of all trailheads\r\n" +
                   $"{sumOfTrailheadRatings:N0} is the sum of the ratings of all trailheads\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
