namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_01(StringReader input)
        {
            var startTimestamp = DateTime.Now;
            int calibrationSum = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                line = line
                    .Replace("one", "one1one")
                    .Replace("two", "two2two")
                    .Replace("three", "three3three")
                    .Replace("four", "four4four")
                    .Replace("five", "five5five")
                    .Replace("six", "six6six")
                    .Replace("seven", "seven7seven")
                    .Replace("eight", "eight8eight")
                    .Replace("nine", "nine9nine");

                char firstDigit = '0';
                char lastDigit = '0';

                foreach (char character in line)
                {
                    if (int.TryParse(character.ToString(), out int _))
                    {
                        if (firstDigit == '0') firstDigit = character;
                        lastDigit = character;
                    }
                }

                calibrationSum += int.Parse(firstDigit.ToString()) * 10 + int.Parse(lastDigit.ToString());
            }

            var endTimestamp = DateTime.Now;

            return $"{calibrationSum:N0} is the sum of all of the calibration values\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}
