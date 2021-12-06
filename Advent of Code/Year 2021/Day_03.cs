using Advent_of_Code.Extensions.Year_2021.Day_03;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_03(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            int[]? bitCounts = null;
            List<string> oxygenNumbers = new List<string>();
            List<string> co2Numbers = new List<string>();

            // Process the input bits
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (bitCounts == null)
                {
                    bitCounts = new int[line.Length];
                }
                for (var bitIndex = 0; bitIndex < line.Length; bitIndex++)
                {
                    bitCounts[bitIndex] += (line[bitIndex] == '0' ? -1 : 1);
                }
                oxygenNumbers.Add(line);
                co2Numbers.Add(line);
            }

            // Determine Gamma Rate and Epsilon Rate
            int gammaRate = 0;
            int epsilonRate = 0;
            for (var bitIndex = 0; bitIndex < bitCounts?.Length; bitIndex++)
            {
                var bit = (bitCounts[bitIndex] > 0 ? 1 : 0);
                gammaRate = gammaRate * 2 + bit;
                epsilonRate = epsilonRate * 2 + bit ^ 1;
            }

            // Find Oxygen Generator Rating and CO2 Scrubber Rating
            for (var position = 0; (oxygenNumbers.Count > 1 || co2Numbers.Count > 1) && position < oxygenNumbers[0].Length; position++)
            {
                if (oxygenNumbers.Count > 1)
                {
                    oxygenNumbers.TrimByBit(position, oxygenNumbers.MostCommonBit(position) ^ 1);
                }

                if (co2Numbers.Count > 1)
                {
                    co2Numbers.TrimByBit(position, co2Numbers.MostCommonBit(position));
                }
            }

            // If found, convert binary string to integer
            var oxygenRating = oxygenNumbers.Count == 1 ? Convert.ToInt32(oxygenNumbers[0], 2) : -1;
            var co2Rating = co2Numbers.Count == 1 ? Convert.ToInt32(co2Numbers[0], 2) : -1;

            var endTimestamp = DateTime.Now;

            // Display Results
            return $"Gamma Rate is {gammaRate:N0}\r\n" +
                   $"Epsilon Rate is {epsilonRate:N0}\r\n" +
                   $"Power Consumption is {gammaRate * epsilonRate:N0}\r\n" +
                   $"\r\n" +
                   $"Oxygen Generator Rating is {oxygenRating:N0}\r\n" +
                   $"CO2 Scrubber Rating is {co2Rating:N0}\r\n" +
                   $"Life Support Rating is {oxygenRating * co2Rating:N0}\r\n" +
                   $"\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}
