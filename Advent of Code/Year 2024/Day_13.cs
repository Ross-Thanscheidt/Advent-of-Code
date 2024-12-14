using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_13(StringReader input)
        {
            // Solving these equations:
            //    ax + by = c
            //    dx + ey = f
            //
            // Solution:
            //    x = (ce - bf) / (ae - bd)
            //    y = (af - cd) / (ae - bd)
            //
            // Translated Solution:
            //    where x = numA, y = numB
            //    where a = AX, b = BX, c = PX
            //    where d = AY, e = BY, f = PY
            //
            //    numA = (BY * PX - BX * PY) / (AX * BY - AY * BX)
            //    numB = (AX * PY - AY * PX) / (AX * BY - AY * BX)

            Stopwatch stopwatch = Stopwatch.StartNew();

            long tokensForAllPartOne = 0;
            long tokensForAllPartTwo = 0;

            long AX = 0;
            long AY = 0;
            long BX = 0;
            long BY = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                string label = line.Split(":")[0];

                if (label == "Button A" || label == "Button B")
                {
                    long dx = long.Parse(line.Split('+')[1].Split(',')[0]);
                    long dy = long.Parse(line.Split('+')[2]);

                    if (label.EndsWith('A'))
                    {
                        AX = dx;
                        AY = dy;
                    }
                    else
                    {
                        BX = dx;
                        BY = dy;
                    }
                }
                else if (label == "Prize")
                {
                    long PX = long.Parse(line.Split('=')[1].Split(',')[0]);
                    long PY = long.Parse(line.Split('=')[2]);

                    long denominator = AX * BY - AY * BX;

                    if (denominator != 0)
                    {
                        long numA = (BY * PX - BX * PY) / denominator;
                        long numB = (AX * PY - AY * PX) / denominator;

                        // Part One

                        if (numA * AX + numB * BX == PX &&
                            numA * AY + numB * BY == PY &&
                            numA <= 100 &&
                            numB <= 100)
                        {
                            tokensForAllPartOne += numA * 3 + numB;
                        }

                        // Part Two

                        long offset = 10_000_000_000_000;

                        PX += offset;
                        PY += offset;

                        numA = (BY * PX - BX * PY) / denominator;
                        numB = (AX * PY - AY * PX) / denominator;

                        if (numA * AX + numB * BX == PX &&
                            numA * AY + numB * BY == PY)
                        {
                            tokensForAllPartTwo += numA * 3 + numB;
                        }
                    }
                }
            }

            stopwatch.Stop();

            return $"{tokensForAllPartOne:N0} is the fewest tokens you would have to spend to win all possible prizes pressing a button no more than 100 times\r\n" +
                   $"{tokensForAllPartTwo:N0} is the fewest tokens you would have to spend to win all possible prizes without the unit conversion error\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
