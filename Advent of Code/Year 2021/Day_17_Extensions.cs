using System.Text.RegularExpressions;

namespace Advent_of_Code.Extensions.Year_2021.Day_17
{
    public static class Day_17
    {
        public static int GetInt(this Match match, string groupname)
        {
            return match.Success ? int.Parse(match.Groups[groupname].Value) : 0;
        }
    }
}
