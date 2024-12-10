using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_07(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long totalCalibrationResult = 0;
            long totalCalibrationResultWithConcatenations = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                long testValue = long.Parse(line.Split(':')[0]);
                long[] numbers = line.Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();

                bool foundPossibleSolution = false;

                List<string> operationCombinations = [ "*", "+", "|" ];

                for (int idx = 1; idx < numbers.Length; idx++)
                {
                    List<string> newList = [];

                    foreach (string op in operationCombinations)
                    {
                        newList.Add(string.Concat(op, "*"));
                        newList.Add(string.Concat(op, "+"));
                        newList.Add(string.Concat(op, "|"));
                    }

                    operationCombinations = newList;
                }

                List<string> newListOrder = [];

                newListOrder.AddRange(operationCombinations.Where(s => !s.Contains('|')));
                newListOrder.AddRange(operationCombinations.Where(s => s.Contains('|')));

                operationCombinations = newListOrder;

                bool concatenationEncountered = false;

                foreach (string operations in operationCombinations)
                {
                    long result = numbers[0];

                    for (int idx = 1; idx < numbers.Length; idx++)
                    {
                        switch (operations[idx - 1])
                        {
                            case '+':
                                result += numbers[idx];
                                break;

                            case '*':
                                result *= numbers[idx];
                                break;

                            case '|':
                                concatenationEncountered = true;
                                result = long.Parse($"{result}{numbers[idx]}");
                                break;
                        }
                    }

                    if (result == testValue)
                    {
                        foundPossibleSolution = true;
                        break;
                    }
                }

                if (foundPossibleSolution)
                {
                    if (!concatenationEncountered)
                    {
                        totalCalibrationResult += testValue;
                    }

                    totalCalibrationResultWithConcatenations += testValue;
                }
            }

            stopwatch.Stop();

            return $"{totalCalibrationResult:N0} is the total calibration result with add and multiply operators\r\n" +
                   $"{totalCalibrationResultWithConcatenations:N0} is the total calibration result with concatenation operators\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
