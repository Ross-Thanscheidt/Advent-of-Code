using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2025 : IYear12
    {
        public string Day_03(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long joltage2 = 0;
            long joltage12 = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                foreach (int batteriesPerBank in new int[] { 2, 12 })
                {
                    long joltage = 0;
                    int currentLargest = 0;

                    for (int digitsLeft = batteriesPerBank; digitsLeft > 0; digitsLeft--)
                    {
                        for (int i = currentLargest + 1; i < line.Length - (digitsLeft - 1); i++)
                        {
                            if (line[i] > line[currentLargest])
                            {
                                currentLargest = i;
                            }
                        }

                        joltage = joltage * 10 + (line[currentLargest] - '0');
                        currentLargest++;
                    }

                    if (batteriesPerBank == 2)
                    {
                        joltage2 += joltage;
                    }
                    else
                    {
                        joltage12 += joltage;
                    }
                }
            }

            stopwatch.Stop();

            return $"{joltage2:N0} is the total output joltage with 2 batteries per bank\r\n" +
                   $"{joltage12:N0} is the total output joltage with 12 batteries per bank\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
