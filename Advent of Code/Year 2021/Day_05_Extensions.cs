using System.Text.RegularExpressions;

namespace Advent_of_Code.Extensions.Year_2021.Day_05
{
    public static class Day_05
    {
        public static int GetInt(this Match match, string groupname)
        {
            return match.Success ? int.Parse(match.Groups[groupname].Value) : 0;
        }

        public static void IncrementPointCount(this Dictionary<(int, int), int> pointCounts, int x, int y)
        {
            (int, int) point = (x, y);
            if (pointCounts.ContainsKey(point))
                pointCounts[point]++;
            else
                pointCounts.Add(point, 1);
        }

    }
}
