using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2025 : IYear12
    {
        public string Day_01(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long landingOnZeroCount = 0;
            long landingOnOrPassingZeroCount = 0;

            int dial = 50;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                int direction = line[0] == 'L' ? -1 : 1;
                int units = int.Parse(line[1..]);

                for (int count = units; count > 0; count--)
                {
                    dial += direction;

                    if (dial == -1)
                    {
                        dial = 99;
                    }
                    else if (dial == 100)
                    {
                        dial = 0;
                    }

                    if (dial == 0)
                    {
                        if (count == 1)
                        {
                            landingOnZeroCount++;
                        }

                        landingOnOrPassingZeroCount++;
                    }
                }
            }

            stopwatch.Stop();

            return $"{landingOnZeroCount:N0} is the actual password to open the door\r\n" +
                   $"{landingOnOrPassingZeroCount:N0} is the password to open the door using password method 0x434C49434B\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
