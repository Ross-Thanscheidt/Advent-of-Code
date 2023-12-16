using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_14(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long totalLoad = 0;

            List<List<char>> map = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                map.Add([.. line.ToCharArray()]);
            }

            for (var columnIndex = 0; columnIndex < map[0].Count; columnIndex++)
            {
                for (var rowIndex = 0; rowIndex < map.Count; rowIndex++)
                {
                    if (map[rowIndex][columnIndex] == 'O')
                    {
                        var targetRowIndex = rowIndex - 1;

                        while (targetRowIndex >= 0 && map[targetRowIndex][columnIndex] == '.')
                        {
                            map[targetRowIndex][columnIndex] = 'O';
                            map[targetRowIndex+1][columnIndex] = '.';
                            targetRowIndex--;
                        }

                        totalLoad += map.Count - (targetRowIndex + 1);
                    }
                }
            }

            stopwatch.Stop();

            return $"{totalLoad:N0} is the total load on the north support beams\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}