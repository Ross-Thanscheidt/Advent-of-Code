using Advent_of_Code.Year_2022_Day_12;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_12(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            // Get input lines
            var lines = new List<string>();
            for (var line = input.ReadLine()?.Trim(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    lines.Add(line);
                }
            }

            var originalStartLeastSteps = 0;
            var bestStartLeastSteps = 0;

            Position? originalStartPosition = null;
            var startingPositions = new Stack<Position>();

            while (originalStartPosition == null || startingPositions.Any())
            {
                // Load array of elevation levels into AreaMap object
                var areaMap = new AreaMap(lines.Count, lines[0].Length);
                foreach (var line in lines)
                {
                    areaMap.AddRow(line);
                }

                // Set these values after the areaMap is built the first time
                if (originalStartPosition == null)
                {
                    originalStartPosition = areaMap.StartPosition;
                    bestStartLeastSteps = areaMap.Rows * areaMap.Columns;
                    foreach (var position in areaMap.StartingPositions)
                    {
                        startingPositions.Push(position);
                    }
                }

                // Find the fewest steps from the next starting position

                var startingPosition = startingPositions.Pop();
                areaMap.StartPosition = startingPosition;

                var potentialMoves = new PriorityQueue<(Position Position, int Steps), int>();

                // First move is from the current starting position
                potentialMoves.Enqueue(new(areaMap.StartPosition, 0), 0);
                areaMap[areaMap.StartPosition].LeastStepsHere = 0;

                // Evaluate each remaining potential move
                while (potentialMoves.TryDequeue(out (Position Position, int Steps) currentMove, out int currentPriority))
                {
                    foreach (var nextPosition in areaMap[currentMove.Position].PossibleMoves
                        .Where(nextPosition =>
                            currentMove.Steps + 1 < areaMap[nextPosition].LeastStepsHere &&
                            currentMove.Steps + 1 < areaMap[areaMap.EndPosition].LeastStepsHere))
                    {
                        areaMap[nextPosition].LeastStepsHere = currentMove.Steps + 1;

                        if (nextPosition != areaMap.EndPosition)
                        {
                            var priority =
                                Math.Abs(areaMap.EndPosition.RowIndex - nextPosition.RowIndex) +
                                Math.Abs(areaMap.EndPosition.ColumnIndex - nextPosition.ColumnIndex) +
                                (areaMap[nextPosition].ElevationLevel - areaMap[currentMove.Position].ElevationLevel);
                            potentialMoves.Enqueue((nextPosition, currentMove.Steps + 1), priority);
                        }
                    }
                }

                if (startingPosition == originalStartPosition)
                {
                    originalStartLeastSteps = areaMap[areaMap.EndPosition].LeastStepsHere;
                }

                bestStartLeastSteps = Math.Min(bestStartLeastSteps, areaMap[areaMap.EndPosition].LeastStepsHere);
            }

            var endTimestamp = DateTime.Now;

            return $"{originalStartLeastSteps:N0} are the fewest steps required from the designated Start position\r\n" +
                   $"{bestStartLeastSteps:N0} are the fewest steps required from any position with the lowest elevation\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}
