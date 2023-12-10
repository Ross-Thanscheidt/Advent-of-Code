using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_09(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long extrapolatedSum = 0;
            long extrapolatedSumBackwards = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var values = line.Split(" ").Select(s => long.Parse(s)).ToList();

                int alternateSubtractAndAddForBackwards = 1;
                int lastValueIndex = values.Count - 1;
                while (values.Any(v => v != 0))
                {
                    extrapolatedSum += values[lastValueIndex];

                    extrapolatedSumBackwards += alternateSubtractAndAddForBackwards * values[0];
                    alternateSubtractAndAddForBackwards *= -1;

                    for (int valuesIndex = 0; valuesIndex < lastValueIndex; valuesIndex++)
                    {
                        values[valuesIndex] = values[valuesIndex+1] - values[valuesIndex];
                    }

                    values[lastValueIndex--] = 0;
                }
            }

            stopwatch.Stop();

            return $"{extrapolatedSum:N0} is the sum of the extrapolated values\r\n" +
                   $"{extrapolatedSumBackwards:N0} is the sum of the extrapolated values going backwards\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}
