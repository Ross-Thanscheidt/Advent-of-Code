using Advent_of_Code.Extensions.Year_2022;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {

        public string Day_08(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var visibleTrees = 0;
            var maxScenicScore = 0;

            int[][] map = null;

            // Fill the map of trees with their heights
            var row = 0;
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    map ??= new int[line.Length][];
                    map[row++] = line.Select(c => c - '0').ToArray();
                }
            }

            if (map != null)
            {
                var rows = map.GetLength(0);
                var columns = map[0].GetLength(0);

                // All the trees around the edge are visible
                visibleTrees = 4 * (columns - 1);

                // Count the visible interior trees and find the best Scenic Score (all edge trees are 0)
                for (row = 1; row < rows - 1; row++)
                {
                    for (var col = 1; col < columns - 1; col++)
                    {
                        // Get the height for this tree
                        var height = map[row][col];

                        // This tree is visible if only shorter trees to the left, or to the right, or up, or down
                        if (!map[row][..col].Any(h => h >= height) ||
                            !map[row][(col+1)..].Any(h => h >= height) ||
                            !map[..row].Select(r => r[col]).Any(h => h >= height) ||
                            !map[(row+1)..].Select(r => r[col]).Any(h => h >= height))
                        {
                            visibleTrees++;
                        }

                        // Calculate the Scenic Score for this tree by multiplying left, right, up, and down counts
                        var scenicScore =
                            map[row][..col].Reverse().TakeUntilIncluding(h => h >= height).Count() *
                            map[row][(col+1)..].TakeUntilIncluding(h => h >= height).Count() *
                            map[..row].Select(r => r[col]).Reverse().TakeUntilIncluding(h => h >= height).Count() *
                            map[(row+1)..].Select(r => r[col]).TakeUntilIncluding(h => h >= height).Count();

                        // Keep the highest score
                        maxScenicScore = Math.Max(maxScenicScore, scenicScore);
                    }
                }
            }

            var endTimestamp = DateTime.Now;

            return $"{visibleTrees:N0} trees are visible from outside the grid\r\n" +
                   $"{maxScenicScore:N0} is the highest scenic score possible for any tree\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}
