using Advent_of_Code.Year_2021_Day_11;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_11(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            // Get input lines
            var lines = new List<string>();
            for (var line = input.ReadLine()?.Trim(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    lines.Add(line);
                }
            }

            // Load array of dumbo octopus energy levels into EnergyGrid object
            var energyGrid = new EnergyGrid(lines.Count, lines[0].Length);
            foreach (var line in lines)
            {
                var energyLevelsRow = line.Select(c => int.Parse(c.ToString())).ToList();
                energyGrid.AddRow(energyLevelsRow);
            }

            var flashesAfter100Steps = 0;
            var firstAllFlashStep = 0;
            for (var step = 1; step <= 100 || firstAllFlashStep == 0; step++)
            {
                for (var rowIndex = 0; rowIndex < energyGrid.Rows; rowIndex++)
                {
                    for (var columnIndex = 0; columnIndex < energyGrid.Columns; columnIndex++)
                    {
                        energyGrid[rowIndex, columnIndex]++;
                    }
                }

                if (energyGrid.ResetFlashEnergyLevels() == energyGrid.Rows * energyGrid.Columns &&
                    firstAllFlashStep == 0)
                {
                    firstAllFlashStep = step;
                }

                if (step == 100)
                {
                    flashesAfter100Steps = energyGrid.FlashCount;
                }
            }

            var endTimestamp = DateTime.Now;

            return $"{flashesAfter100Steps:N0} flashes after 100 steps\r\n" +
                   $"First step during which all octopuses flash is Step {firstAllFlashStep:N0}\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}
