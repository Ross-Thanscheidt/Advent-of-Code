using System.Diagnostics;
using Advent_of_Code.Year_2024_Day_16;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_16(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long lowestScorePossible;
            long tilesInBestPaths = 0;

            Dictionary<Position, long> maze = [];
            Position start = new(0, 0);
            Position end = new(0, 0);
            Direction initialDirection = new(1, 0);

            int rows = 0;
            int columns = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                columns = line.Length;

                foreach ((int column, char tile) in line.Index())
                {
                    if (".SE".Contains(tile))
                    {
                        maze.Add(new(column, rows), long.MaxValue);

                        if (tile == 'S')
                        {
                            start = new(column, rows);
                        }

                        if (tile == 'E')
                        {
                            end = new(column, rows);
                        }
                    }
                }

                rows++;
            }

            // Part One

            Position position = start;
            Direction direction = initialDirection;
            long score = 0;

            Direction[] directions = [new(1, 0), new(0, 1), new(-1, 0), new(0, -1)];
            Position movePosition = new(0, 0);
            long moveScore = 0;

            List<(Position Position, Direction Direction, long Score)> potentialMoves = [];
            potentialMoves.Add((start, direction, score));

            while (potentialMoves.Count > 0)
            {
                (position, direction, score) = potentialMoves[0];
                potentialMoves.RemoveAt(0);

                if (score < maze[position])
                {
                    maze[position] = score;
                }

                if (position != end)
                {
                    foreach (Direction moveDirection in directions)
                    {
                        movePosition = position + moveDirection;

                        if (maze.ContainsKey(movePosition))
                        {
                            moveScore = (moveDirection == direction ? 0 : 1_000) + 1;

                            if (score + moveScore < maze[movePosition])
                            {
                                potentialMoves.Add((movePosition, moveDirection, score + moveScore));
                            }
                        }
                    }
                }
            }

            lowestScorePossible = maze[end];

            // Part Two

            List<Position> bestPaths = [];

            position = end;
            direction = directions.Where(d => maze.ContainsKey(position + d) && maze[position + d] < maze[position]).OrderBy(d => maze[position + d]).First();
            score = maze[end];

            List<(Position Position, Direction Direction, long Score)> nextMoves = [(position, direction, score)];

            while (nextMoves.Count > 0)
            {
                (position, direction, score) = nextMoves[0];
                nextMoves.RemoveAt(0);

                if (!bestPaths.Contains(position))
                {
                    bestPaths.Add(position);
                }

                if (position != start)
                {
                    if (maze.ContainsKey(position + direction))
                    {
                        if (maze[position + direction] == score - 1)
                        {
                            nextMoves.Add((position + direction, direction, score - 1));
                        }
                        else if (maze[position + direction] == score - 1_000 - 1)
                        {
                            position = position + direction;

                            if (!bestPaths.Contains(position))
                            {
                                bestPaths.Add(position);
                            }

                            if (position != start)
                            {
                                if (maze.ContainsKey(position + direction) && maze[position + direction] == score - 1 - 1)
                                {
                                    nextMoves.Add((position + direction, direction, maze[position + direction]));
                                }

                                var directionIndex = directions.Index().Single(pair => pair.Item == direction).Index;

                                foreach (Direction moveDirection in new int[] { 3, 5 }.Select(index => directions[(directionIndex + index) % directions.Length]))
                                {
                                    movePosition = position + moveDirection;

                                    if (maze.TryGetValue(movePosition, out moveScore))
                                    {
                                        if (moveScore == score - 1_000 - 1 - 1)
                                        {
                                            nextMoves.Add((movePosition, moveDirection, moveScore));
                                        }
                                    }
                                }
                            }
                        }
                        else if (maze.ContainsKey(position + direction + direction) && maze[position + direction + direction] == score - 1 - 1)
                        {
                            if (!bestPaths.Contains(position + direction))
                            {
                                bestPaths.Add(position + direction);
                            }

                            nextMoves.Add((position + direction + direction, direction, maze[position + direction + direction]));
                        }
                    }
                }
            }

            tilesInBestPaths = bestPaths.Count;

            stopwatch.Stop();

            return $"{lowestScorePossible:N0} is the lowest score a Reindeer could possibly get\r\n" +
                   $"{tilesInBestPaths:N0} tiles are part of at least one of the best paths through the maze\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
