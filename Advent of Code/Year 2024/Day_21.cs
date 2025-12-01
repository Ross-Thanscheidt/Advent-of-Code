using System.Diagnostics;
using System.Text;
using Advent_of_Code.Year_2024_Day_21;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_21(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long sumOfComplexities = 0;
            long part2 = 0;

            List<string> numericRows = ["789", "456", "123", " 0A"];
            List<string> directionalRows = [" ^A", "<v>"];

            Dictionary<(char Start, char End), List<string>> shortestMoves = [];
            List<Direction> directions = [new(1, 0), new(0, 1), new(-1, 0), new(0, -1)];
            List<char> directionalMovements = ['>', 'v', '<', '^'];

            // Add shortest moves for each pairing of numeric keys

            Dictionary<Position, char> numericPositions = [];

            foreach ((int row, string rowKeys) in numericRows.Index())
            {
                foreach ((int column, char key) in rowKeys.Index())
                {
                    if (key != ' ')
                    {
                        numericPositions.Add(new(column, row), key);
                    }
                }
            }

            List<char> numericKeys = [.. numericPositions.Select(p => p.Value)];

            foreach ((char startKey, char endKey) in numericKeys.SelectMany(key1 => numericKeys.Select(key2 => (key1, key2))))
            {
                Position endKeyPosition = numericPositions.Single(kv => kv.Value == endKey).Key;

                List<string> moves = [];

                Queue<string> movesInProgress = [];
                movesInProgress.Enqueue(startKey.ToString());

                while (movesInProgress.Count > 0)
                {
                    string move = movesInProgress.Dequeue();
                    char key = move[^1];

                    if (key == endKey)
                    {
                        StringBuilder moveDirectional = new();

                        for (int idx = 1; idx < move.Length; idx++)
                        {
                            Position current = numericPositions.Single(kv => kv.Value == move[idx - 1]).Key;
                            Position target = numericPositions.Single(kv => kv.Value == move[idx]).Key;
                            Direction direction = new(target.X - current.X, target.Y - current.Y);
                            char movement = directionalMovements[directions.IndexOf(direction)];
                            moveDirectional.Append(movement);
                        }

                        moveDirectional.Append('A');
                        moves.Add(moveDirectional.ToString());
                    }
                    else
                    {
                        Position position = numericPositions.Single(kv => kv.Value == key).Key;
                        int keyDistanceToEnd = position.Distance(endKeyPosition);

                        foreach (Direction direction in directions.Where(direction => numericPositions.ContainsKey(position + direction) && (position + direction).Distance(endKeyPosition) < keyDistanceToEnd))
                        {
                            movesInProgress.Enqueue($"{move}{numericPositions.Single(kv => kv.Key == position + direction).Value}");
                        }
                    }
                }

                shortestMoves.Add((startKey, endKey), moves);
            }

            // Add shortest moves for each pairing of directional keys

            Dictionary<Position, char> directionalPositions = [];

            foreach ((int row, string rowKeys) in directionalRows.Index())
            {
                foreach ((int column, char key) in rowKeys.Index())
                {
                    if (key != ' ')
                    {
                        directionalPositions.Add(new(column, row), key);
                    }
                }
            }

            List<char> directionalKeys = [.. directionalPositions.Select(p => p.Value)];

            foreach ((char startKey, char endKey) in directionalKeys.SelectMany(key1 => directionalKeys.Select(key2 => (key1, key2))))
            {
                Position endKeyPosition = directionalPositions.Single(kv => kv.Value == endKey).Key;

                List<string> moves = [];

                Queue<string> movesInProgress = [];
                movesInProgress.Enqueue(startKey.ToString());

                while (movesInProgress.Count > 0)
                {
                    string move = movesInProgress.Dequeue();
                    char key = move[^1];

                    if (key == endKey)
                    {
                        StringBuilder moveDirectional = new();

                        for (int idx = 1; idx < move.Length; idx++)
                        {
                            Position current = directionalPositions.Single(kv => kv.Value == move[idx - 1]).Key;
                            Position target = directionalPositions.Single(kv => kv.Value == move[idx]).Key;
                            Direction direction = new(target.X - current.X, target.Y - current.Y);
                            char movement = directionalMovements[directions.IndexOf(direction)];
                            moveDirectional.Append(movement);
                        }

                        moveDirectional.Append('A');
                        moves.Add(moveDirectional.ToString());
                    }
                    else
                    {
                        Position position = directionalPositions.Single(kv => kv.Value == key).Key;
                        int keyDistanceToEnd = position.Distance(endKeyPosition);

                        foreach (Direction direction in directions.Where(direction => directionalPositions.ContainsKey(position + direction) && (position + direction).Distance(endKeyPosition) < keyDistanceToEnd))
                        {
                            movesInProgress.Enqueue($"{move}{directionalPositions.Single(kv => kv.Key == position + direction).Value}");
                        }
                    }
                }

                shortestMoves.TryAdd((startKey, endKey), moves);
            }

            // There is 1 numeric keypad and 3 directional keypads

            StringBuilder sb = new();

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                sb.AppendLine($"line={line}");

                List<string> moves = [];

                char current = 'A';

                foreach (char key in line)
                {
                    if (moves.Count == 0)
                    {
                        moves.AddRange(shortestMoves[(current, key)]);
                    }
                    else
                    {
                        moves = [.. shortestMoves[(current, key)].SelectMany(move => moves, (move, previousMoves) => string.Concat(previousMoves, move))];
                    }

                    moves.RemoveAll(move => move.Length > moves.Min(s => s.Length));

                    current = key;
                }


                sb.AppendLine("First Level");
                foreach (string move in moves)
                {
                    sb.AppendLine(move);
                }

                for (int level = 0; level < 1; level++)
                {
                    List<string> newLevelMoves = [];

                    foreach (string previousLevelMoves in moves)
                    {
                        current = 'A';

                        foreach (char key in previousLevelMoves)
                        {
                            if (newLevelMoves.Count == 0)
                            {
                                newLevelMoves.AddRange(shortestMoves[(current, key)]);
                            }
                            else
                            {
                                newLevelMoves = [.. shortestMoves[(current, key)].SelectMany(move => newLevelMoves, (move, previousMoves) => string.Concat(previousMoves, move))];
                            }

                            newLevelMoves.RemoveAll(move => move.Length > newLevelMoves.Min(s => s.Length));

                            current = key;
                        }
                    }

                    moves = newLevelMoves;

                    sb.AppendLine($"Level {level}");
                    foreach (string move in moves)
                    {
                        sb.AppendLine(move);
                    }
                }
            }

            stopwatch.Stop();

            return $"{sumOfComplexities:N0}\r\n" +
                   $"{part2:N0}\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)" +
                   $"\r\n\r\n{sb}";
        }
    }
}
