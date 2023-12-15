using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        private List<string> _Day_13_Pattern = [];

        private int Day_13_FindHorizontalLine(List<string> pattern, bool withSmudge)
        {
            int foundRowsSum = 0;

            for (var rowIndex = 0; rowIndex + 1 < pattern.Count; rowIndex++)
            {
                var differences = pattern[rowIndex]
                    .Zip(pattern[rowIndex + 1], (c1, c2) => c1 == c2)
                    .Count(same => !same);

                if (differences == 0 || (withSmudge && differences == 1))
                {
                    bool foundHorizontalLine = true;
                    var pairs = Math.Min(rowIndex + 1, pattern.Count - rowIndex - 1);

                    for (var pairIndex = 1; foundHorizontalLine && pairIndex < pairs; pairIndex++)
                    {
                        differences += pattern[rowIndex - pairIndex]
                            .Zip(pattern[rowIndex + 1 + pairIndex], (c1, c2) => c1 == c2)
                            .Count(same => !same);

                        if ((withSmudge && differences > 1) || (!withSmudge && differences > 0))
                        {
                            foundHorizontalLine = false;
                        }
                    }

                    if (foundHorizontalLine && ((!withSmudge && differences == 0) || (withSmudge && differences == 1)))
                    {
                        foundRowsSum = rowIndex + 1;
                    }
                }
            }

            return foundRowsSum;
        }

        private long Day_13_SummarizePatterns(bool withSmudge)
        {
            long summarize = 0;

            if (_Day_13_Pattern.Count > 0)
            {
                List<string> pivotedPattern = [];

                for (var columnIndex = 0; columnIndex < _Day_13_Pattern[0].Length; columnIndex++)
                {
                    pivotedPattern.Add(string.Concat(_Day_13_Pattern.Select(row => row[columnIndex])));
                }

                summarize += Day_13_FindHorizontalLine(pivotedPattern, withSmudge);
                summarize += 100 * Day_13_FindHorizontalLine(_Day_13_Pattern, withSmudge);
            }

            return summarize;
        }

        public string Day_13(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long totalSum = 0;
            long totalSumSmudge = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    _Day_13_Pattern.Add(line);
                }
                else
                {
                    totalSum += Day_13_SummarizePatterns(withSmudge: false);
                    totalSumSmudge += Day_13_SummarizePatterns(withSmudge: true);

                    _Day_13_Pattern.Clear();
                }
            }

            totalSum += Day_13_SummarizePatterns(withSmudge: false);
            totalSumSmudge += Day_13_SummarizePatterns(withSmudge: true);

            stopwatch.Stop();

            return $"{totalSum:N0} is the number I get after summarizing all of my notes\r\n" +
                   $"{totalSumSmudge:N0} is the number I get after summarizing all of my notes with smudges\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}