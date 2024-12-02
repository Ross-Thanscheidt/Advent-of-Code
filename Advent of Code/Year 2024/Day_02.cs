using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        private static bool SafeReport(List<int> levels)
        {
            bool safeReport = false;

            for (var level = 1; level < levels.Count; level++)
            {
                int difference = Math.Abs(levels[level] - levels[level - 1]);

                if (difference < 1 || difference > 3)
                {
                    break;
                }

                if (level > 1 &&
                    ((levels[level - 2] > levels[level - 1] && levels[level - 1] < levels[level]) ||
                     (levels[level - 2] < levels[level - 1] && levels[level - 1] > levels[level])))
                {
                    break;
                }

                if (level == levels.Count - 1)
                {
                    safeReport = true;
                }
            }

            return safeReport;
        }

        public string Day_02(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int safeReports = 0;
            int safeReportsProblemDampener = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                List<int> levels = line.Split(' ').Select(s => int.Parse(s)).ToList();

                if (SafeReport(levels))
                {
                    safeReports++;
                    safeReportsProblemDampener++;
                }
                else
                {
                    for (int idx = 0; idx < levels.Count; idx++)
                    {
                        List<int> levelsModified = [];
                        levelsModified.AddRange(levels);
                        levelsModified.RemoveAt(idx);

                        if (SafeReport(levelsModified))
                        {
                            safeReportsProblemDampener++;
                            break;
                        }
                    }
                }
            }

            stopwatch.Stop();

            return $"{safeReports:N0} reports are safe\r\n" +
                   $"{safeReportsProblemDampener:N0} reports are safe using the Problem Dampener\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}
