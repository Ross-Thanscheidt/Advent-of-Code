using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        [GeneratedRegex("(.*?(?<Number>\\d+).*?)*")]
        private static partial Regex Day_06_NumbersRegex();

        public string Day_06(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long marginOfError = 1;
            long waysToWinLongRace = 0;

            List<long> raceTimes = [];
            List<long> bestRecordedDistances = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.StartsWith("Time:"))
                {
                    raceTimes = Day_06_NumbersRegex()
                        .Match(line.Split(":")[1])
                        .Groups["Number"]
                        .Captures
                        .Select(capture => long.Parse(capture.Value))
                        .ToList();
                }

                if (line.StartsWith("Distance:"))
                {
                    bestRecordedDistances = Day_06_NumbersRegex()
                        .Match(line.Split(":")[1])
                        .Groups["Number"]
                        .Captures
                        .Select(capture => long.Parse(capture.Value))
                        .ToList();
                }
            }

            for (int raceIndex = 0; raceIndex <= raceTimes.Count; raceIndex++)
            {
                long raceTime;
                long bestRecordedDistance;
                long holdTime;

                if (raceIndex < raceTimes.Count)
                {
                    raceTime = raceTimes[raceIndex];
                    bestRecordedDistance = bestRecordedDistances[raceIndex];
                }
                else
                {
                    raceTime = long.Parse(string.Join("", raceTimes));
                    bestRecordedDistance = long.Parse(string.Join("", bestRecordedDistances));
                }

                for (holdTime = 1; holdTime < raceTime && holdTime * (raceTime - holdTime) <= bestRecordedDistance; holdTime++)
                {
                }
                long firstWinTime = holdTime * (raceTime - holdTime) > bestRecordedDistance ? holdTime : 0;

                for (holdTime = raceTime - 1;  holdTime > 0 && holdTime * (raceTime - holdTime) <= bestRecordedDistance; holdTime--)
                {
                }
                long lastWinTime = holdTime * (raceTime - holdTime) > bestRecordedDistance ? holdTime : 0;

                var wins = lastWinTime - firstWinTime + 1;

                if (raceIndex < raceTimes.Count)
                {
                    marginOfError *= wins;
                }
                else
                {
                    waysToWinLongRace = wins;
                }
            }

            return $"{marginOfError:N0} is the product of the number of ways you can beat the record in each race\r\n" +
                   $"{waysToWinLongRace:N0} is the number of ways you can beat the record in one much longer race\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}
