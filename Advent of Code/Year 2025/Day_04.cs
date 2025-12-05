using System.Diagnostics;
using Advent_of_Code.Year_2025_Day_04;

namespace Advent_of_Code
{
    public partial class Year_2025 : IYear12
    {
        public string Day_04(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long accessibleRollsCount = 0;
            long removableRollsCount = 0;

            List<Position> rollPositions = [];

            int row = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                for (int col = 0; col < line.Length; col++)
                {
                    if (line[col] == '@')
                    {
                        rollPositions.Add(new(col, row));
                    }
                }

                row++;
            }

            List<Position> removableRolls = [];
            Direction[] directions = [new(0, -1), new(1, -1), new(1, 0), new(1, 1), new(0, 1), new(-1, 1), new(-1, 0), new(-1, -1)];

            do
            {
                removableRolls.Clear();
                removableRolls.AddRange(rollPositions.Where(rollPosition => directions.Count(direction => rollPositions.Contains(rollPosition + direction)) < 4));
                rollPositions.RemoveAll(rollPosition => removableRolls.Contains(rollPosition));

                if (accessibleRollsCount == 0)
                {
                    accessibleRollsCount = removableRolls.Count;
                }

                removableRollsCount += removableRolls.Count;
            } while (removableRolls.Count > 0);

            stopwatch.Stop();

            return $"{accessibleRollsCount:N0} rolls of paper can be accessed by a forklift\r\n" +
                   $"{removableRollsCount:N0} rolls of paper can be removed by a forklift\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
