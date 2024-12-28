using System.Diagnostics;
using Advent_of_Code.Year_2024_Day_18;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_18(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long minSteps = 0;
            string firstByte = "";

            List<Position> unsafePositionsPartOne = [];
            List<Position> unsafePositionsPartTwo = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                int[] lineParts = [.. line.Split(',').Select(s => int.Parse(s))];

                if (unsafePositionsPartOne.Count < 12 ||
                    (unsafePositionsPartOne.Max(p => p.Y) > 6 && unsafePositionsPartOne.Count < 1_024))
                {
                    unsafePositionsPartOne.Add(new(lineParts[0], lineParts[1]));
                }
                else
                {
                    unsafePositionsPartTwo.Add(new(lineParts[0], lineParts[1]));
                }
            }

            int rows = unsafePositionsPartOne.Max(p => p.Y) >= 7 ? 71 : 7;
            int columns = rows;

            Dictionary<Position, long> safePositions = [];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (!unsafePositionsPartOne.Contains(new(column, row)))
                    {
                        safePositions.Add(new(column, row), long.MaxValue);
                    }
                }
            }

            // Part One

            Position start = new(0, 0);
            Position end = new(columns - 1, rows - 1);

            Position position = start;
            long score = 0;
            long distance = position.Distance(end);

            Direction[] directions = [new(1, 0), new(0, 1), new(-1, 0), new(0, -1)];
            Position movePosition = new(0, 0);
            long moveDistance = 0;

            PriorityQueue<(Position Position, long Score), long> potentialMoves = new();
            potentialMoves.Enqueue((start, score), distance);

            while (potentialMoves.Count > 0)
            {
                (position, score) = potentialMoves.Dequeue();
                distance = position.Distance(end);

                if (score < safePositions[position])
                {
                    safePositions[position] = score;
                }

                if (position != end && score + distance <= safePositions[end])
                {
                    foreach (Direction moveDirection in directions)
                    {
                        movePosition = position + moveDirection;
                        moveDistance = end.Distance(movePosition);

                        if (safePositions.ContainsKey(movePosition))
                        {
                            if (score + 1 < safePositions[movePosition] && score + 1 + moveDistance <= safePositions[end])
                            {
                                potentialMoves.Enqueue((movePosition, score + 1), moveDistance);
                            }
                        }
                    }
                }
            }

            minSteps = safePositions[end];

            // Part Two

            List<Position> pathPositions = [];

            foreach (Position bytePosition in unsafePositionsPartTwo)
            {
                if (pathPositions.Count == 0 && safePositions[end] < long.MaxValue)
                {
                    potentialMoves = new();
                    potentialMoves.Enqueue((end, safePositions[end]), end.Distance(end));

                    while (potentialMoves.Count > 0)
                    {
                        (position, score) = potentialMoves.Dequeue();

                        pathPositions.Add(position);

                        foreach (Direction moveDirection in directions.Where(direction => safePositions.ContainsKey(position + direction)))
                        {
                            movePosition = position + moveDirection;

                            if (safePositions[movePosition] == score - 1)
                            {
                                potentialMoves.Enqueue((movePosition, score - 1), movePosition.Distance(end));
                            }
                        }
                    }
                }

                if (safePositions.ContainsKey(bytePosition))
                {
                    safePositions.Remove(bytePosition);

                    if (pathPositions.Contains(bytePosition))
                    {
                        pathPositions.Clear();

                        foreach ((Position key, long _) in safePositions)
                        {
                            safePositions[key] = long.MaxValue;
                        }

                        potentialMoves = new();
                        potentialMoves.Enqueue((start, 0), start.Distance(end));

                        while (potentialMoves.Count > 0)
                        {
                            (position, score) = potentialMoves.Dequeue();
                            distance = position.Distance(end);

                            if (score < safePositions[position])
                            {
                                safePositions[position] = score;
                            }

                            if (position != end && score + distance <= safePositions[end])
                            {
                                foreach (Direction moveDirection in directions.Where(direction => safePositions.ContainsKey(position + direction)))
                                {
                                    movePosition = position + moveDirection;
                                    moveDistance = end.Distance(movePosition);

                                    if (score + 1 < safePositions[movePosition] && score + 1 + moveDistance <= safePositions[end])
                                    {
                                        potentialMoves.Enqueue((movePosition, score + 1), moveDistance);
                                    }
                                }
                            }
                        }

                        if (safePositions[end] == long.MaxValue)
                        {
                            firstByte = $"{bytePosition.X},{bytePosition.Y}";
                            break;
                        }
                    }
                }
            }

            stopwatch.Stop();

            return $"{minSteps:N0} is the minimum number of steps needed to reach the exit\r\n" +
                   $"{firstByte} are the coordinates of the first byte that will prevent the exit from being reachable from the starting position\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
