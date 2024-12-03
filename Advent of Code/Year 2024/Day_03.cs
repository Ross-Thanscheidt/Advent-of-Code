using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        [GeneratedRegex("do\\(\\)|don't\\(\\)|mul\\((?<num1>\\d{1,3}),(?<num2>\\d{1,3})\\)")]
        private static partial Regex Day_03_InstructionsRegex();

        public string Day_03(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long sumOfProducts = 0;
            long sumOfEnabledProducts = 0;

            var regex = Day_03_InstructionsRegex();
            bool enabled = true;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                sumOfProducts += regex.Matches(line).Cast<Match>()
                    .Sum(match => match.Value.StartsWith("mul(")
                        ? int.Parse(match.Groups["num1"].Value) * int.Parse(match.Groups["num2"].Value)
                        : 0);

                sumOfEnabledProducts += regex.Matches(line).Cast<Match>()
                    .Sum(match =>
                    {
                        if (match.Value == "do()")
                        {
                            enabled = true;
                            return 0;
                        }
                        else if (match.Value == "don't()")
                        {
                            enabled = false;
                            return 0;
                        }
                        else
                        {
                            return enabled ? int.Parse(match.Groups["num1"].Value) * int.Parse(match.Groups["num2"].Value) : 0;
                        }
                    });
            }

            stopwatch.Stop();

            return $"{sumOfProducts:N0} is the sum of the results of the multiplications\r\n" +
                   $"{sumOfEnabledProducts:N0} is the sum of the results of just the enabled multiplications\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
