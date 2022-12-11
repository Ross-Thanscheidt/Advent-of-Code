using System.Diagnostics;
using System.Text;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_10(StringReader input)
        {
            const int PIXELS_PER_ROW = 40;
            const int CRT_ROWS = 6;

            var startTimestamp = DateTime.Now;

            var sumSignalStrengths = 0;
            var crtPixels = new char[CRT_ROWS * PIXELS_PER_ROW];

            var nextSignalStrengthCycleCheck = 20;

            var currentCycle = 0;
            var registerX = 1;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var instruction = line.Split(" ")[0];
                var cycles = instruction == "addx" ? 2 : 1;
                var value = instruction == "addx" ? int.Parse(line.Split(" ")[1]) : 0;

                if (currentCycle + 1 <= nextSignalStrengthCycleCheck &&
                    nextSignalStrengthCycleCheck  <= currentCycle + cycles)
                {
                    sumSignalStrengths += nextSignalStrengthCycleCheck * registerX;
                    nextSignalStrengthCycleCheck += 40;
                }

                for (var drawCycle = currentCycle; drawCycle < currentCycle + cycles; drawCycle++)
                {
                    crtPixels[drawCycle] =
                        (drawCycle % PIXELS_PER_ROW) >= registerX - 1 &&
                        (drawCycle % PIXELS_PER_ROW) <= registerX + 1
                        ? '#'
                        : '.';
                }

                currentCycle += cycles;
                registerX += value;
            }

            var crtLines = new StringBuilder();
            for (var crtLine = 0; crtLine < CRT_ROWS; crtLine++)
            {
                for (var idx = crtLine * PIXELS_PER_ROW; idx < (crtLine + 1) * PIXELS_PER_ROW; idx++)
                {
                    crtLines.Append(crtPixels[idx]);
                }
                crtLines.Append("\r\n");
            }

            var endTimestamp = DateTime.Now;

            return $"{sumSignalStrengths:N0} is the sum of the six signal strengths\r\n" +
                   $"{crtLines}" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}
