using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        readonly List<string> DIGIT_NAMES = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

        private int Day_01_Calibration_Value(string? line, bool spelledOutDigits)
        {
            char firstDigit = '0';
            char lastDigit = '0';

            if (line != null)
            {
                if (spelledOutDigits)
                {
                    int digitNumber = 1;
                    DIGIT_NAMES.ForEach(digitName => line = line.Replace(digitName, digitName + digitNumber++ + digitName));
                }

                foreach (char character in line.Where(c => Char.IsDigit(c)))
                {
                    if (firstDigit == '0') firstDigit = character;
                    lastDigit = character;
                }
            }

            return int.Parse(firstDigit.ToString()) * 10 + int.Parse(lastDigit.ToString());
        }

        public string Day_01(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int calibrationSumNumericOnly = 0;
            int calibrationSumIncludeDigitWords = 0;

            for (string? line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                calibrationSumNumericOnly += Day_01_Calibration_Value(line, false);
                calibrationSumIncludeDigitWords += Day_01_Calibration_Value(line, true);
            }

            stopwatch.Stop();

            return $"{calibrationSumNumericOnly:N0} is the sum of all of the calibration values (numeric only)\r\n" +
                   $"{calibrationSumIncludeDigitWords:N0} is the sum of all of the calibration values (numeric and spelled out)\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}
