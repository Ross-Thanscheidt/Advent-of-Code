using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        private static int NumberOfDigits(long number)
        {
            int digits = 1;

            while ((number /= 10) > 0)
            {
                digits++;
            }


            return digits;
        }

        public string Day_11(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long numberOfStonesAfter25 = 0;
            long numberOfStonesAfter75 = 0;

            List<(long Number,long Count)> stones = [];
            
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                stones.AddRange(line.Split(' ').Select(s => (long.Parse(s), 1L)));
            }

            for (int blink = 0; blink < 75; blink++)
            {
                int stoneCount = stones.Count;

                for (int stone = 0; stone < stoneCount; stone++)
                {
                    if (stones[stone].Number == 0)
                    {
                        stones[stone] = (1, stones[stone].Count);
                    }
                    else {
                        int digits = NumberOfDigits(stones[stone].Number);

                        if (digits % 2 == 0)
                        {
                            long divisor = (long) Math.Pow(10, digits / 2);

                            long upperNumber = stones[stone].Number / divisor;
                            long lowerNumber = stones[stone].Number - upperNumber * divisor;
                            long count = stones[stone].Count;

                            stones[stone] = (upperNumber, count);
                            stones.Add((lowerNumber, count));
                        }
                        else
                        {
                            stones[stone] = (stones[stone].Number * 2024, stones[stone].Count);
                        }
                    }
                }

                stones = [.. stones.GroupBy(s => s.Number).Select(result => (result.Key, result.Select(s => s.Count).Sum()))];

                if (blink + 1 == 25)
                {
                    numberOfStonesAfter25 = stones.Sum(s => s.Count);
                }
            }

            numberOfStonesAfter75 = stones.Sum(s => s.Count);

            stopwatch.Stop();

            return $"{numberOfStonesAfter25:N0} stones after blinking 25 times\r\n" +
                   $"{numberOfStonesAfter75:N0} stones after blinking 75 times\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
