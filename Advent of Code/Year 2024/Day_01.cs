using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {

        [GeneratedRegex(@"(?<id1>\d+)\s+(?<id2>\d+)")]
        private static partial Regex Day_01_LocationIDsRegex();

        public string Day_01(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long distance = 0;
            long similarityScore = 0;

            List<int> left = [];
            List<int> right = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var matchGroups = Day_01_LocationIDsRegex().Match(line).Groups;

                var id1 = int.Parse(matchGroups["id1"].Captures[0].Value);
                var id2 = int.Parse(matchGroups["id2"].Captures[0].Value);

                left.Add(id1);
                right.Add(id2);
            }

            left.Sort();
            right.Sort();

            for (int idx = 0; idx < left.Count; idx++)
            {
                distance += Math.Abs(left[idx] - right[idx]);
                similarityScore += left[idx] * right.Count(n => n == left[idx]);
            }

            stopwatch.Stop();

            return $"{distance:N0} is the total distance between the lists\r\n" +
                   $"{similarityScore:N0} is the similarity score\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
