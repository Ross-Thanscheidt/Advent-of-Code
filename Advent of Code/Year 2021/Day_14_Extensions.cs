using System.Text.RegularExpressions;

namespace Advent_of_Code.Extensions.Year_2021.Day_14
{
    public static class Day_14
    {
        public static void IncrementCount(this Dictionary<char, long> elementCounts, char key)
        {
            elementCounts.AddCount(key, 1);
        }

        public static void AddCount(this Dictionary<char, long> elementCounts, char key, long count)
        {
            if (elementCounts.ContainsKey(key))
                elementCounts[key] += count;
            else
                elementCounts.Add(key, count);
        }

        public static void IncrementCount(this Dictionary<string, long> elementPairCounts, string key)
        {
            elementPairCounts.AddCount(key, 1);
        }

        public static void AddCount(this Dictionary<string, long> elementPairCounts, string key, long count)
        {
            if (elementPairCounts.ContainsKey(key))
                elementPairCounts[key] += count;
            else
                elementPairCounts.Add(key, count);
        }
    }
}
