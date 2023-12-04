using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        [GeneratedRegex(@"[.\d]")]
        private static partial Regex NonSymbolsRegex();

        [GeneratedRegex("(.*?(?<PartNumber>\\d+).*?)*")]
        private static partial Regex PartNumbersRegex();


        public string Day_03(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int partNumbersSum = 0;
            int gearRatiosSum = 0;

            var schematic = new List<string>();

            // Load the engine schematic
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    schematic.Add(line);
                }
            }

            int rows = schematic.Count;
            int columns = schematic[0].Length;
            int column;

            // Scan the schematic for Part Numbers and Gears
            for (int row = 0; row < rows; row++)
            {
                column = 0;

                while (column < columns)
                {
                    // Is it a Number?  If so, then is it a Part Number (indicated by an adjacent symbol)?
                    if (Char.IsDigit(schematic[row][column]))
                    {
                        // Get the Number
                        int firstDigitColumn = column;

                        while (column < columns && Char.IsDigit(schematic[row][column]))
                        {
                            column++;
                        }

                        int number = int.Parse(schematic[row][firstDigitColumn..column]);

                        // Get all of the adjacent characters
                        string adjacentCharacters =
                            (row > 0 ? schematic[row - 1][Math.Max(0, firstDigitColumn - 1)..Math.Min(columns, column + 1)] : "") +
                            (firstDigitColumn > 0 ? schematic[row].Substring(firstDigitColumn - 1, 1) : "") +
                            (column < columns ? schematic[row].Substring(column, 1) : "") +
                            (row + 1 < rows ? schematic[row + 1][Math.Max(0, firstDigitColumn - 1)..Math.Min(columns, column + 1)] : "");

                        // Remove everything but symbols
                        adjacentCharacters = NonSymbolsRegex().Replace(adjacentCharacters, "");

                        // If there is an adjacent symbol, it is a Part Number
                        if (adjacentCharacters.Length > 0)
                        {
                            partNumbersSum += number;
                        }
                    }
                    else
                    {
                        // Is it a Gear?  If so, are there exactly 2 Part Numbers adjacent to it?
                        if (schematic[row][column] == '*')
                        {
                            // Find all of the Part Numbers adjacent to the Gear
                            string adjacentCharacters = "";

                            for (int gearRow = Math.Max(0, row - 1); gearRow <= Math.Min(rows - 1, row + 1); gearRow++)
                            {
                                int startColumn = Math.Max(0, column - 1);
                                int endColumn = Math.Min(columns, column + 1);

                                while (Char.IsDigit(schematic[gearRow][startColumn]) && startColumn > 0 && Char.IsDigit(schematic[gearRow][startColumn - 1]))
                                {
                                    startColumn--;
                                }

                                while (Char.IsDigit(schematic[gearRow][endColumn]) && endColumn + 1 < columns && char.IsDigit(schematic[gearRow][endColumn + 1]))
                                {
                                    endColumn++;
                                }

                                adjacentCharacters += string.Concat(" ", schematic[gearRow].AsSpan(startColumn, endColumn - startColumn + 1));
                            }

                            var matchGroups = PartNumbersRegex().Match(adjacentCharacters).Groups;

                            var partNumbers = matchGroups["PartNumber"].Captures
                                .Select(partNumberCapture => int.Parse(partNumberCapture.Value))
                                .ToList();

                            if (partNumbers.Count == 2)
                            {
                                int gearRatioProduct = 1;
                                partNumbers.ForEach(partNumber => gearRatioProduct *= partNumber);
                                gearRatiosSum += gearRatioProduct;
                            }
                        }

                        // Look for the next Part Number or Gear
                        column++;
                    }
                }
            }

            stopwatch.Stop();

            return $"{partNumbersSum:N0} is the sum of all of the part numbers in the engine schematic\r\n" +
                   $"{gearRatiosSum:N0} is the sum of all of the gear ratios in the engine schematic\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
