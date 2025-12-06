using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2025 : IYear12
    {
        [GeneratedRegex(@"\s+")]
        private static partial Regex SelectMultipleSpaces();

        public string Day_06(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long grandTotalRows = 0;
            long grandTotalColumns = 0;

            List<List<long>> operandRows = [];
            List<string> lines = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                lines.Add(line); // Save for Part Two

                // Part One

                string[] operandRow = SelectMultipleSpaces().Replace(line, " ").Trim().Split(' ');

                if (!"+*".Contains(operandRow[0]))
                {
                    operandRows.Add([.. operandRow.Select(operand => long.Parse(operand))]);
                }
                else
                {
                    for (int column = 0; column < operandRow.Length; column++)
                    {
                        long columnAnswer = operandRows[0][column];

                        for (int row = 1; row < operandRows.Count; row++)
                        {
                            if (operandRow[column] == "+")
                            {
                                columnAnswer += operandRows[row][column];
                            }
                            else
                            {
                                columnAnswer *= operandRows[row][column];
                            }
                        }

                        grandTotalRows += columnAnswer;
                    }
                }
            }

            // Part Two

            StringBuilder operandColumn = new();
            List<long> columnOperands = [];

            for (int column = lines[0].Length - 1; column >= 0; column--)
            {
                operandColumn.Clear();

                for (int row = 0; row < lines.Count - 1; row++)
                {
                    operandColumn.Append(lines[row][column]);
                }

                string operand = operandColumn.ToString().Trim();

                if (operand.Length > 0)
                {
                    columnOperands.Add(long.Parse(operand));
                }

                char operation = lines[^1][column];

                if ("+*".Contains(operation))
                {
                    long columnAnswer = columnOperands[0];

                    for (int i = 1; i < columnOperands.Count; i++)
                    {
                        if (operation == '+')
                        {
                            columnAnswer += columnOperands[i];
                        }
                        else
                        {
                            columnAnswer *= columnOperands[i];
                        }
                    }

                    columnOperands.Clear();

                    grandTotalColumns += columnAnswer;
                }
            }

            stopwatch.Stop();

            return $"{grandTotalRows:N0} is the grand total found by summing answers with horizontal digits\r\n" +
                   $"{grandTotalColumns:N0} is the grand total found by summing answers with vertical digits\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
